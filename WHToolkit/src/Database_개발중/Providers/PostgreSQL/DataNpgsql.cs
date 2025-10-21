using System.Data;
using System.Data.Common;
using Npgsql;
using NpgsqlTypes;

namespace WHToolkit.Database.Provider;

/// <summary>
/// PostgreSQL database provider implementation
/// </summary>
internal class DataNpgsql : DBHelper
{
    private NpgsqlConnection? _connection;
    private NpgsqlCommand? _command;
    private NpgsqlDataAdapter? _dataAdapter;

    public override string TextPrefix { get; set; } = "";
    public override string InPrefix { get; set; } = "";
    public override string OutPrefix { get; set; } = "";

    protected override string SequenceNextFormat => "nextval('{0}')";

    protected override IDbConnection Connection
    {
        get
        {
            _connection ??= new NpgsqlConnection(ConnectionString);
            return _connection;
        }
        set => _connection = value as NpgsqlConnection;
    }

    public override IDbCommand Command
    {
        get
        {
            if (_command == null)
            {
                _command = (NpgsqlCommand)Connection.CreateCommand();
                _command.CommandTimeout = CommandTimeout;
            }
            return _command;
        }
        set => _command = value as NpgsqlCommand;
    }

    protected override IDbDataAdapter DataAdapter
    {
        get
        {
            _dataAdapter ??= new NpgsqlDataAdapter((NpgsqlCommand)Command);
            return _dataAdapter;
        }
        set => _dataAdapter = value as NpgsqlDataAdapter;
    }

    public DataNpgsql()
    {
        Environment.Provider = ProviderKind.PostgreSQL;
    }

    protected override DbParameter ConvertParameter(DataParameter parameter)
    {
        string prefix = CurrentPrefix ?? string.Empty;
        
        if (string.IsNullOrEmpty(prefix))
        {
            prefix = parameter.Direction switch
            {
                ParameterDirection.Input => InPrefix,
                ParameterDirection.Output => OutPrefix,
                ParameterDirection.InputOutput => InPrefix,
                _ => string.Empty
            };
        }

        string parameterName = parameter.ParameterName.StartsWith(prefix) 
            ? parameter.ParameterName 
            : prefix + parameter.ParameterName;

        var npgsqlParam = new NpgsqlParameter
        {
            ParameterName = parameterName,
            Value = parameter.Value ?? DBNull.Value,
            Direction = parameter.Direction,
            Size = parameter.Size,
            IsNullable = parameter.IsNullable,
            SourceColumn = parameter.SourceColumn,
            SourceColumnNullMapping = parameter.SourceColumnNullMapping
        };

        // Set NpgsqlDbType based on DbType or infer from value
        if (parameter.DbType != default)
        {
            npgsqlParam.DbType = parameter.DbType;
        }
        else if (parameter.Value != null)
        {
            npgsqlParam.NpgsqlDbType = InferNpgsqlDbTypeFromValue(parameter.Value);
        }

        return npgsqlParam;
    }

    private static NpgsqlDbType InferNpgsqlDbTypeFromValue(object value)
    {
        return value switch
        {
            string => NpgsqlDbType.Text,
            int => NpgsqlDbType.Integer,
            long => NpgsqlDbType.Bigint,
            short => NpgsqlDbType.Smallint,
            byte => NpgsqlDbType.Smallint,
            bool => NpgsqlDbType.Boolean,
            DateTime => NpgsqlDbType.Timestamp,
            decimal => NpgsqlDbType.Numeric,
            double => NpgsqlDbType.Double,
            float => NpgsqlDbType.Real,
            byte[] => NpgsqlDbType.Bytea,
            Guid => NpgsqlDbType.Uuid,
            _ => NpgsqlDbType.Text
        };
    }
}