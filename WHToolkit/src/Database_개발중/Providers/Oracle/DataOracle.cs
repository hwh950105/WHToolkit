using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

namespace WHToolkit.Database.Provider;

/// <summary>
/// Oracle database provider implementation
/// </summary>
internal class DataOracle : DBHelper
{
    private OracleConnection? _connection;
    private OracleCommand? _command;
    private OracleDataAdapter? _dataAdapter;

    public override string TextPrefix { get; set; } = ":";
    public override string InPrefix { get; set; } = "i_";
    public override string OutPrefix { get; set; } = "o_";

    protected override string SequenceNextFormat => "{0}.NEXTVAL";
    protected override bool AllowAutoParameterMapping => false;

    protected override IDbConnection Connection
    {
        get
        {
            _connection ??= new OracleConnection(ConnectionString);
            return _connection;
        }
        set => _connection = value as OracleConnection;
    }

    public override IDbCommand Command
    {
        get
        {
            if (_command == null)
            {
                _command = (OracleCommand)Connection.CreateCommand();
                _command.CommandTimeout = CommandTimeout;
                _command.BindByName = true;
                _command.InitialLONGFetchSize = -1;
            }
            return _command;
        }
        set => _command = value as OracleCommand;
    }

    protected override IDbDataAdapter DataAdapter
    {
        get
        {
            _dataAdapter ??= new OracleDataAdapter((OracleCommand)Command);
            return _dataAdapter;
        }
        set => _dataAdapter = value as OracleDataAdapter;
    }

    public DataOracle()
    {
        Environment.Provider = ProviderKind.Oracle;
    }

    protected override DbParameter ConvertParameter(DataParameter parameter)
    {
        string prefix = CurrentPrefix ?? string.Empty;
        
        string parameterName = parameter.ParameterName.StartsWith(prefix) 
            ? parameter.ParameterName 
            : prefix + parameter.ParameterName;

        var oracleParam = new OracleParameter
        {
            ParameterName = parameterName,
            Value = parameter.Value ?? DBNull.Value,
            Direction = parameter.Direction,
            Size = parameter.Size,
            IsNullable = parameter.IsNullable,
            SourceColumn = parameter.SourceColumn,
            SourceColumnNullMapping = parameter.SourceColumnNullMapping
        };

        // Set OracleDbType based on DbType or infer from value
        if (parameter.DbType != default)
        {
            oracleParam.DbType = parameter.DbType;
        }
        else if (parameter.Value != null)
        {
            oracleParam.OracleDbType = InferOracleDbTypeFromValue(parameter.Value);
        }

        return oracleParam;
    }

    private static OracleDbType InferOracleDbTypeFromValue(object value)
    {
        return value switch
        {
            string => OracleDbType.Varchar2,
            int => OracleDbType.Int32,
            long => OracleDbType.Int64,
            short => OracleDbType.Int16,
            byte => OracleDbType.Byte,
            bool => OracleDbType.Byte, // Oracle doesn't have boolean, use byte
            DateTime => OracleDbType.Date,
            decimal => OracleDbType.Decimal,
            double => OracleDbType.Double,
            float => OracleDbType.Single,
            byte[] => OracleDbType.Blob,
            _ => OracleDbType.Varchar2
        };
    }
}