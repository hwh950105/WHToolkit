using Npgsql;
using System.Data;
using System.Reflection;
using NpgsqlTypes;
using System.Data.Common;

namespace Hyundai.wia.Utility.Commons
{
    public class NpgHelper : IDisposable
    {
       // private static readonly string connectionString = SecurityHelper.DecryptAES(WaiCommon.GetConfigValue("NpgDatabase"));
        private readonly Lazy<NpgsqlConnection> _dbConnection = new(() => new NpgsqlConnection("Host=172.23.53.146;Port=5432;Database=postgres;Username=postgres;Password=q1w2e3r4!@"));
        private NpgsqlTransaction _transaction;


        private string InPrefix = "";

        private string OutPrefix = "";

        private string Refcursor = "refcursor";


        public NpgsqlConnection Npgsql => _dbConnection.Value;

        public DataParameterCollection Parameters = new DataParameterCollection();
        private DataParameterCollection OldParameters = new DataParameterCollection();
        public string GetOutputParameterValue(string parameterName)
        {
            var parameter = OldParameters.FirstOrDefault(p => p.ParameterName == parameterName);
            return parameter?.Value?.ToString() ?? string.Empty; // 기본값을 빈 문자열로 반환
        }

        public void TransactionBegin()
        {
            if (Npgsql.State != ConnectionState.Open)
            {
                Npgsql.Open();
            }
            _transaction = Npgsql.BeginTransaction();
        }

        public void TransactionCommit()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }

        public void TransactionRollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

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
        /// 아웃 못잡아서 수정함
        /// </summary>
        /// <param name="type"></param>
        /// <param name="query"></param>
        /// <returns></returns>
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

                        // 음 이거 맞나 ?
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

    public class DataParameter : DbParameter
    {
        public override int Size { get; set; }
        public override DbType DbType { get; set; }
        public override ParameterDirection Direction { get; set; }
        public override bool IsNullable { get; set; } = true;
        public override string ParameterName { get; set; }
        public override string SourceColumn { get; set; }
        public override object Value { get; set; }
        public override bool SourceColumnNullMapping { get; set; }

        public DataParameter(ParameterDirection Direction, string ParameterName, object Value)
        {
            this.Direction = Direction;
            this.ParameterName = ParameterName;
            this.Value = Value;
            if (Value != null && Value.GetType().Name == "DateTime")
            {
                DbType = DbType.DateTime;
            }
        }

        public DataParameter(ParameterDirection Direction, string ParameterName, object Value, int Size)
        {
            this.Direction = Direction;
            this.ParameterName = ParameterName;
            this.Value = Value;
            this.Size = Size;
            if (Value != null && Value.GetType().Name == "DateTime")
            {
                DbType = DbType.DateTime;
            }
        }

        public DataParameter(ParameterDirection Direction, DbType DbType, string ParameterName, object Value)
        {
            this.Direction = Direction;
            this.DbType = DbType;
            this.ParameterName = ParameterName;
            this.Value = Value;
        }

        public override void ResetDbType()
        {
            DbType = DbType.String;
        }
    }

    public class DataParameterCollection : List<DataParameter>
    {
        public void Add(string parameterName, object value)
        {
            this.Add(new DataParameter(ParameterDirection.Input, parameterName, value));
        }

        public void Add(string parameterName, object value, int size)
        {
            this.Add(new DataParameter(ParameterDirection.Input, parameterName, value, size));
        }

        public void Add(DbType dbType, string parameterName, object value)
        {
            this.Add(new DataParameter(ParameterDirection.Input, dbType, parameterName, value));
        }

        public void Add(ParameterDirection direction, string parameterName, object value)
        {
            this.Add(new DataParameter(direction, parameterName, value));
        }

        public void Add(ParameterDirection direction, string parameterName, object value, int size)
        {
            this.Add(new DataParameter(direction, parameterName, value, size));
        }

        public void Add(ParameterDirection direction, DbType dbType, string parameterName, object value)
        {
            this.Add(new DataParameter(direction, dbType, parameterName, value));
        }
    }
}
