using Npgsql;
using System.Data;
using System.Reflection;
using NpgsqlTypes;
using System.Data.Common;
using WHToolkit.Database.Common;
using WHToolkit.Security;

namespace WHToolkit.Database
{
    /// <summary>
    /// PostgreSQL 데이터베이스 헬퍼 클래스
    /// </summary>
    public class NpgHelper : IDisposable
    {
        private static readonly string connectionString = SecurityHelper.DecryptAES(Commoncode.GetConfigValue("MariaDatabase"));
        private readonly Lazy<NpgsqlConnection> _dbConnection;
        private NpgsqlTransaction _transaction;

        private string InPrefix = "";
        private string OutPrefix = "";
        private string Refcursor = "refcursor";

        /// <summary>
        /// PostgreSQL 연결 객체
        /// </summary>
        public NpgsqlConnection Npgsql => _dbConnection.Value;

        /// <summary>
        /// 입력 파라미터 컬렉션
        /// </summary>
        public DataParameterCollection Parameters = new DataParameterCollection();

        private DataParameterCollection OldParameters = new DataParameterCollection();


        /// <summary>
        /// 기본 연결 문자열로 초기화합니다
        /// </summary>
        public NpgHelper()
        {
            _dbConnection = new(() => new NpgsqlConnection(connectionString));
        }

        /// <summary>
        /// 사용자 지정 연결 문자열로 초기화합니다
        /// </summary>
        /// <param name="customConnectionString">연결 문자열</param>
        public NpgHelper(string customConnectionString)
        {
            _dbConnection = new(() => new NpgsqlConnection(customConnectionString));
        }




        /// <summary>
        /// Output 파라미터 값을 가져옵니다
        /// </summary>
        /// <param name="parameterName">파라미터 이름</param>
        /// <returns>파라미터 값</returns>
        public string GetOutputParameterValue(string parameterName)
        {
            var parameter = OldParameters.FirstOrDefault(p => p.ParameterName == parameterName);
            return parameter?.Value?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// 트랜잭션을 시작합니다
        /// </summary>
        public void TransactionBegin()
        {
            if (Npgsql.State != ConnectionState.Open)
            {
                Npgsql.Open();
            }
            _transaction = Npgsql.BeginTransaction();
        }

        /// <summary>
        /// 트랜잭션을 커밋합니다
        /// </summary>
        public void TransactionCommit()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }

        /// <summary>
        /// 트랜잭션을 롤백합니다
        /// </summary>
        public void TransactionRollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        /// <summary>
        /// 쿼리를 실행하고 결과를 리스트로 반환합니다
        /// </summary>
        /// <typeparam name="T">반환할 객체 타입</typeparam>
        /// <param name="type">명령 타입</param>
        /// <param name="query">실행할 쿼리</param>
        /// <returns>결과 리스트</returns>
        public List<T> Execute<T>(CommandType type, string query) where T : new()
        {
            var result = new List<T>();
            lock (Npgsql)
            {
                try
                {
                    EnsureConnectionOpen();

                    // 트랜잭션 시작
                    if (_transaction == null)
                    {
                        _transaction = Npgsql.BeginTransaction();
                    }

                    using (var command = CreateCommand(query, type))
                    {
                        if (type == CommandType.StoredProcedure)
                        {
                            // 뺄까???
                            /*            if (!command.Parameters.Contains(Refcursor))
                                        {
                                            command.Parameters.Add(new NpgsqlParameter(Refcursor, NpgsqlDbType.Refcursor)
                                            {
                                                Direction = ParameterDirection.Output
                                            });
                                        }*/

                            command.ExecuteNonQuery();

                            var cursorName = command.Parameters[Refcursor].Value?.ToString();
                            if (!string.IsNullOrEmpty(cursorName))
                            {

                                using (var fetchCommand = new NpgsqlCommand($"FETCH ALL IN \"{cursorName}\";", Npgsql)) // 슈발 왜 커서쓰냐
                                using (var reader = fetchCommand.ExecuteReader())
                                {
                                    result = MapReaderToList<T>(reader);
                                }
                            }
                        }
                        else
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                result = MapReaderToList<T>(reader);
                            }
                        }
                    }
                }
                finally
                {
                    CloseTransactionIfNecessary();
                }
            }
            return result;
        }


        /// <summary>
        /// Output 파라미터 없이 쿼리를 실행합니다
        /// </summary>
        /// <param name="type">명령 타입</param>
        /// <param name="query">실행할 쿼리</param>
        /// <returns>영향받은 행 수</returns>
        public int ExecuteNonQuery2(CommandType type, string query)
        {
            lock (Npgsql)
            {
                try
                {
                    EnsureConnectionOpen();

                    using (var command = CreateCommand(query, type))
                    {
                        return command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    CloseTransactionIfNecessary();
                }
            }
        }

        /// <summary>
        /// 쿼리를 실행하고 Output 파라미터를 수집합니다
        /// </summary>
        /// <param name="type">명령 타입</param>
        /// <param name="query">실행할 쿼리</param>
        /// <returns>영향받은 행 수</returns>
        public int ExecuteNonQuery(CommandType type, string query)
        {
            lock (Npgsql)
            {
                try
                {
                    EnsureConnectionOpen();

                    using (var command = CreateCommand(query, type))
                    {
                        var result = command.ExecuteNonQuery();

                        foreach (NpgsqlParameter param in command.Parameters)
                        {
                            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                            {
                                OldParameters = new DataParameterCollection();
                                var existingParam = OldParameters.FirstOrDefault(p => p.ParameterName == param.ParameterName);
                                if (existingParam != null)
                                {
                                    existingParam.Value = param.Value;
                                }
                                else
                                {
                                    OldParameters.Add(new DataParameter(param.Direction, param.ParameterName, param.Value));
                                }
                            }
                        }

                        return result;
                    }
                }
                finally
                {
                    CloseTransactionIfNecessary();
                }
            }
        }


        private NpgsqlCommand CreateCommand(string query, CommandType commandType)
        {
            var command = new NpgsqlCommand(query, Npgsql)
            {
                CommandType = commandType,
                Transaction = _transaction
            };

            foreach (var param in Parameters)
            {

                NpgsqlParameter npgsqlParameter;
                if (param.Direction == ParameterDirection.Output) { npgsqlParameter = command.Parameters.AddWithValue(OutPrefix + param.ParameterName, param.Value ?? DBNull.Value); }
                else { npgsqlParameter = command.Parameters.AddWithValue(InPrefix + param.ParameterName, param.Value ?? DBNull.Value); }

                npgsqlParameter.Direction = param.Direction;
                npgsqlParameter.Size = param.Size;
            }

            return command;
        }


        private List<T> MapReaderToList<T>(IDataReader reader) where T : new()
        {
            var result = new List<T>();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // 컬럼 이름 딕셔너리 (대소문자 무시)
            var columnNames = Enumerable.Range(0, reader.FieldCount)
                                        .Select(reader.GetName)
                                        .ToDictionary(name => name.ToLower(), name => name);

            while (reader.Read())
            {
                var item = new T();
                foreach (var property in properties)
                {
                    var propertyNameLower = property.Name.ToLower();
                    if (columnNames.ContainsKey(propertyNameLower))
                    {
                        var columnName = columnNames[propertyNameLower]; // 실제 컬럼 이름
                        if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                        {
                            property.SetValue(item, Convert.ChangeType(reader[columnName], property.PropertyType));
                        }
                    }
                }
                result.Add(item);
            }

            return result;
        }


        private void EnsureConnectionOpen()
        {
            if (Npgsql.State != ConnectionState.Open)
            {
                Npgsql.Open();
            }
        }

        private void CloseTransactionIfNecessary()
        {

            if (Parameters.Count > 0)
            {
                Parameters.Clear();
            }

            if (_transaction != null && _transaction.Connection != null)
            {
                return;
            }

            // 트랜잭션이 없고 연결이 열린 상태에서만 연결 닫기
            if (Npgsql.State == ConnectionState.Open)
            {
                Npgsql.Close();
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            if (_dbConnection.IsValueCreated && Npgsql.State == ConnectionState.Open)
            {
                Npgsql.Close();
            }
            _dbConnection.Value.Dispose();
        }
    }
}
