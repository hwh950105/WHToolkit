using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection;
using System.Data.Common;
using WHToolkit.Database.Common;
using WHToolkit.Security;

namespace WHToolkit.Database
{
    /// <summary>
    /// MySQL 데이터베이스 헬퍼 클래스
    /// </summary>
    public class MySqlHelper : IDisposable
    {
        private static readonly string connectionString = SecurityHelper.DecryptAES(Commoncode.GetConfigValue("MySqlDatabase"));
        private readonly Lazy<MySqlConnection> _dbConnection;
        private MySqlTransaction _transaction;

        private string InPrefix = "@";
        private string OutPrefix = "@";

        /// <summary>
        /// MySQL 연결 객체
        /// </summary>
        public MySqlConnection Connection => _dbConnection.Value;

        /// <summary>
        /// 입력 파라미터 컬렉션
        /// </summary>
        public DataParameterCollection Parameters = new DataParameterCollection();
        private DataParameterCollection OldParameters = new DataParameterCollection();

        /// <summary>
        /// 기본 연결 문자열로 초기화합니다
        /// </summary>
        public MySqlHelper()
        {
            _dbConnection = new(() => new MySqlConnection(connectionString));
        }

        /// <summary>
        /// 사용자 지정 연결 문자열로 초기화합니다
        /// </summary>
        /// <param name="customConnectionString">연결 문자열</param>
        public MySqlHelper(string customConnectionString)
        {
            _dbConnection = new(() => new MySqlConnection(customConnectionString));
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
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            _transaction = Connection.BeginTransaction();
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
        public List<T> ExecuteList<T>(CommandType type, string query) where T : new()
        {
            var result = new List<T>();
            bool autoTransaction = _transaction == null;

            lock (Connection)
            {
                try
                {
                    EnsureConnectionOpen();

                    if (autoTransaction)
                    {
                        _transaction = Connection.BeginTransaction();
                    }

                    using (var command = CreateCommand(query, type))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            result = MapReaderToList<T>(reader);
                        }
                    }

                    if (autoTransaction)
                    {
                        _transaction.Commit();
                    }
                }
                catch
                {
                    if (autoTransaction && _transaction != null)
                    {
                        _transaction.Rollback();
                    }
                    throw;
                }
                finally
                {
                    if (autoTransaction && _transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    CloseTransactionIfNecessary();
                }
            }
            return result;
        }

        /// <summary>
        /// 쿼리를 실행하고 Output 파라미터를 수집합니다
        /// </summary>
        /// <param name="type">명령 타입</param>
        /// <param name="query">실행할 쿼리</param>
        /// <returns>영향받은 행 수</returns>
        public int ExecuteNonQuery(CommandType type, string query)
        {
            lock (Connection)
            {
                try
                {
                    EnsureConnectionOpen();

                    using (var command = CreateCommand(query, type))
                    {
                        var result = command.ExecuteNonQuery();

                        OldParameters = new DataParameterCollection();
                        foreach (MySqlParameter param in command.Parameters)
                        {
                            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                            {
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

        /// <summary>
        /// Output 파라미터 없이 쿼리를 실행합니다
        /// </summary>
        /// <param name="type">명령 타입</param>
        /// <param name="query">실행할 쿼리</param>
        /// <returns>영향받은 행 수</returns>
        public int ExecuteNonQuery2(CommandType type, string query)
        {
            lock (Connection)
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
        /// 쿼리를 실행하고 DataSet을 반환합니다
        /// </summary>
        /// <param name="type">명령 타입</param>
        /// <param name="query">실행할 쿼리</param>
        /// <returns>결과 DataSet</returns>
        public DataSet ExecuteDataSet(CommandType type, string query)
        {
            lock (Connection)
            {
                try
                {
                    EnsureConnectionOpen();

                    using (var command = CreateCommand(query, type))
                    using (var adapter = new MySql.Data.MySqlClient.MySqlDataAdapter((MySqlCommand)command))
                    {
                        var dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        return dataSet;
                    }
                }
                finally
                {
                    CloseTransactionIfNecessary();
                }
            }
        }

        /// <summary>
        /// 쿼리를 실행하고 DataTable을 반환합니다
        /// </summary>
        /// <param name="type">명령 타입</param>
        /// <param name="query">실행할 쿼리</param>
        /// <returns>결과 DataTable</returns>
        public DataTable ExecuteDataTable(CommandType type, string query)
        {
            lock (Connection)
            {
                try
                {
                    EnsureConnectionOpen();

                    using (var command = CreateCommand(query, type))
                    using (var adapter = new MySql.Data.MySqlClient.MySqlDataAdapter((MySqlCommand)command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
                finally
                {
                    CloseTransactionIfNecessary();
                }
            }
        }

        /// <summary>
        /// 쿼리를 실행하고 단일 값을 반환합니다
        /// </summary>
        /// <param name="type">명령 타입</param>
        /// <param name="query">실행할 쿼리</param>
        /// <returns>쿼리 결과의 첫 번째 행의 첫 번째 열 값</returns>
        public object ExecuteScalar(CommandType type, string query)
        {
            lock (Connection)
            {
                try
                {
                    EnsureConnectionOpen();

                    using (var command = CreateCommand(query, type))
                    {
                        return command.ExecuteScalar();
                    }
                }
                finally
                {
                    CloseTransactionIfNecessary();
                }
            }
        }

        private MySqlCommand CreateCommand(string query, CommandType commandType)
        {
            var command = new MySqlCommand(query, Connection)
            {
                CommandType = commandType,
                Transaction = _transaction
            };

            foreach (var param in Parameters)
            {
                MySqlParameter mysqlParameter;
                if (param.Direction == ParameterDirection.Output)
                {
                    mysqlParameter = command.Parameters.AddWithValue(OutPrefix + param.ParameterName, param.Value ?? DBNull.Value);
                }
                else
                {
                    mysqlParameter = command.Parameters.AddWithValue(InPrefix + param.ParameterName, param.Value ?? DBNull.Value);
                }

                mysqlParameter.Direction = param.Direction;
                mysqlParameter.Size = param.Size;
            }

            return command;
        }

        private List<T> MapReaderToList<T>(IDataReader reader) where T : new()
        {
            var result = new List<T>();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

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
                        var columnName = columnNames[propertyNameLower];
                        if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                        {
                            var value = reader[columnName];
                            var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            property.SetValue(item, Convert.ChangeType(value, targetType));
                        }
                    }
                }
                result.Add(item);
            }

            return result;
        }

        private void EnsureConnectionOpen()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
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

            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            if (_dbConnection.IsValueCreated && Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
            _dbConnection.Value.Dispose();
        }
    }
}
