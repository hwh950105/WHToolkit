using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace WHToolkit.Database.Provider;

/// <summary>
/// Microsoft SQL Server database provider implementation
/// </summary>
internal class DataMSSQL : DBHelper
{
    private SqlConnection? _connection;
    private SqlCommand? _command;
    private SqlDataAdapter? _dataAdapter;

    public override string TextPrefix { get; set; } = "@";
    public override string InPrefix { get; set; } = "@";
    public override string OutPrefix { get; set; } = "@";

    protected override string SequenceNextFormat => "NEXT VALUE FOR {0}";

    protected override IDbConnection Connection
    {
        get
        {
            _connection ??= new SqlConnection(ConnectionString);
            return _connection;
        }
        set => _connection = value as SqlConnection;
    }

    public override IDbCommand Command
    {
        get
        {
            if (_command == null)
            {
                _command = (SqlCommand)Connection.CreateCommand();
                _command.CommandTimeout = CommandTimeout;
            }
            return _command;
        }
        set => _command = value as SqlCommand;
    }

    protected override IDbDataAdapter DataAdapter
    {
        get
        {
            _dataAdapter ??= new SqlDataAdapter((SqlCommand)Command);
            return _dataAdapter;
        }
        set => _dataAdapter = value as SqlDataAdapter;
    }

    public DataMSSQL()
    {
        Environment.Provider = ProviderKind.MSSQL;
    }

    protected override DbParameter ConvertParameter(DataParameter parameter)
    {
        string prefix = CurrentPrefix ?? string.Empty;
        
        string parameterName = parameter.ParameterName.StartsWith(prefix) 
            ? parameter.ParameterName 
            : prefix + parameter.ParameterName;

        var sqlParam = new SqlParameter
        {
            ParameterName = parameterName,
            Value = parameter.Value ?? DBNull.Value,
            Direction = parameter.Direction,
            Size = parameter.Size,
            IsNullable = parameter.IsNullable,
            SourceColumn = parameter.SourceColumn,
            SourceColumnNullMapping = parameter.SourceColumnNullMapping
        };

        // Set SqlDbType based on DbType or infer from value
        if (parameter.DbType != default)
        {
            sqlParam.DbType = parameter.DbType;
        }
        else if (parameter.Value != null)
        {
            sqlParam.DbType = InferDbTypeFromValue(parameter.Value);
        }

        return sqlParam;
    }

    private static DbType InferDbTypeFromValue(object value)
    {
        return value switch
        {
            string => DbType.String,
            int => DbType.Int32,
            long => DbType.Int64,
            short => DbType.Int16,
            byte => DbType.Byte,
            bool => DbType.Boolean,
            DateTime => DbType.DateTime,
            decimal => DbType.Decimal,
            double => DbType.Double,
            float => DbType.Single,
            Guid => DbType.Guid,
            byte[] => DbType.Binary,
            _ => DbType.String
        };
    }
}