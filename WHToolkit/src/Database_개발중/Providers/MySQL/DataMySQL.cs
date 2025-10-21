using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace WHToolkit.Database.Provider;

/// <summary>
/// MySQL database provider implementation
/// </summary>
internal class DataMySQL : DBHelper
{
    private MySqlConnection? _connection;
    private MySqlCommand? _command;
    private MySqlDataAdapter? _dataAdapter;

    public override string TextPrefix { get; set; } = "@";
    public override string InPrefix { get; set; } = "@";
    public override string OutPrefix { get; set; } = "@";

    protected override string SequenceNextFormat => "{0}.NEXTVAL";
    // MySQL supports parameterized queries, enabling for better security
    protected override bool AllowTextParameter => true;

    protected override IDbConnection Connection
    {
        get
        {
            _connection ??= new MySqlConnection(ConnectionString);
            return _connection;
        }
        set => _connection = value as MySqlConnection;
    }

    public override IDbCommand Command
    {
        get
        {
            if (_command == null)
            {
                _command = (MySqlCommand)Connection.CreateCommand();
                _command.CommandTimeout = CommandTimeout;
            }
            return _command;
        }
        set => _command = value as MySqlCommand;
    }

    protected override IDbDataAdapter DataAdapter
    {
        get
        {
            _dataAdapter ??= new MySqlDataAdapter((MySqlCommand)Command);
            return _dataAdapter;
        }
        set => _dataAdapter = value as MySqlDataAdapter;
    }

    public DataMySQL()
    {
        Environment.Provider = ProviderKind.MySQL;
    }

    protected override DbParameter ConvertParameter(DataParameter parameter)
    {
        string prefix = CurrentPrefix ?? string.Empty;
        
        string parameterName = parameter.ParameterName.StartsWith(prefix) 
            ? parameter.ParameterName 
            : prefix + parameter.ParameterName;

        var mysqlParam = new MySqlParameter
        {
            ParameterName = parameterName,
            Value = parameter.Value ?? DBNull.Value,
            Direction = parameter.Direction,
            Size = parameter.Size,
            IsNullable = parameter.IsNullable,
            SourceColumn = parameter.SourceColumn,
            SourceColumnNullMapping = parameter.SourceColumnNullMapping
        };

        // Set MySqlDbType based on DbType or infer from value
        if (parameter.DbType != default)
        {
            mysqlParam.DbType = parameter.DbType;
        }
        else if (parameter.Value != null)
        {
            mysqlParam.MySqlDbType = InferMySqlDbTypeFromValue(parameter.Value);
        }

        return mysqlParam;
    }

    private static MySqlDbType InferMySqlDbTypeFromValue(object value)
    {
        return value switch
        {
            string => MySqlDbType.VarChar,
            int => MySqlDbType.Int32,
            long => MySqlDbType.Int64,
            short => MySqlDbType.Int16,
            byte => MySqlDbType.Byte,
            bool => MySqlDbType.Bit,
            DateTime => MySqlDbType.DateTime,
            decimal => MySqlDbType.Decimal,
            double => MySqlDbType.Double,
            float => MySqlDbType.Float,
            byte[] => MySqlDbType.Blob,
            Guid => MySqlDbType.Guid,
            _ => MySqlDbType.VarChar
        };
    }
}