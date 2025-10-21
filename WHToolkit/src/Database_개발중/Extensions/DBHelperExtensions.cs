using System.Data;
using WHToolkit.Database.Mapping;

namespace WHToolkit.Database.Extensions;

/// <summary>
/// Extension methods for DBHelper to support model mapping
/// </summary>
public static class DBHelperExtensions
{
    /// <summary>
    /// Execute query and return a single model object
    /// </summary>
    public static async Task<T?> ExecuteEntityAsync<T>(this DBHelper dbHelper, 
        CommandType commandType, string commandText, DataParameterCollection? parameters = null) 
        where T : class, new()
    {
        var dataSet = await dbHelper.ExecuteDataSetAsync(commandType, commandText, parameters);
        
        if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
            return null;

        return DataTableMapper.MapToEntity<T>(dataSet.Tables[0].Rows[0]);
    }

    /// <summary>
    /// Execute query and return a list of model objects
    /// </summary>
    public static async Task<List<T>> ExecuteListAsync<T>(this DBHelper dbHelper, 
        CommandType commandType, string commandText, DataParameterCollection? parameters = null) 
        where T : class, new()
    {
        var dataSet = await dbHelper.ExecuteDataSetAsync(commandType, commandText, parameters);
        
        if (dataSet.Tables.Count == 0)
            return new List<T>();

        return DataTableMapper.MapToList<T>(dataSet.Tables[0]);
    }

    /// <summary>
    /// Execute query and return a single model object (Text command type)
    /// </summary>
    public static async Task<T?> ExecuteEntityAsync<T>(this DBHelper dbHelper, 
        string commandText, DataParameterCollection? parameters = null) 
        where T : class, new()
    {
        return await dbHelper.ExecuteEntityAsync<T>(CommandType.Text, commandText, parameters);
    }

    /// <summary>
    /// Execute query and return a list of model objects (Text command type)
    /// </summary>
    public static async Task<List<T>> ExecuteListAsync<T>(this DBHelper dbHelper, 
        string commandText, DataParameterCollection? parameters = null) 
        where T : class, new()
    {
        return await dbHelper.ExecuteListAsync<T>(CommandType.Text, commandText, parameters);
    }

    /// <summary>
    /// Execute stored procedure and return a single model object
    /// </summary>
    public static async Task<T?> ExecuteStoredProcedureEntityAsync<T>(this DBHelper dbHelper, 
        string procedureName, DataParameterCollection? parameters = null) 
        where T : class, new()
    {
        return await dbHelper.ExecuteEntityAsync<T>(CommandType.StoredProcedure, procedureName, parameters);
    }

    /// <summary>
    /// Execute stored procedure and return a list of model objects
    /// </summary>
    public static async Task<List<T>> ExecuteStoredProcedureListAsync<T>(this DBHelper dbHelper, 
        string procedureName, DataParameterCollection? parameters = null) 
        where T : class, new()
    {
        return await dbHelper.ExecuteListAsync<T>(CommandType.StoredProcedure, procedureName, parameters);
    }

    /// <summary>
    /// Execute query and return DataTable
    /// </summary>
    public static async Task<DataTable?> ExecuteDataTableAsync(this DBHelper dbHelper,
        CommandType commandType, string commandText, DataParameterCollection? parameters = null)
    {
        var dataSet = await dbHelper.ExecuteDataSetAsync(commandType, commandText, parameters);
        return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
    }

    /// <summary>
    /// Execute query and return DataTable (Text command type)
    /// </summary>
    public static async Task<DataTable?> ExecuteDataTableAsync(this DBHelper dbHelper,
        string commandText, DataParameterCollection? parameters = null)
    {
        return await dbHelper.ExecuteDataTableAsync(CommandType.Text, commandText, parameters);
    }

    /// <summary>
    /// Execute query and return first row as DataRow
    /// </summary>
    public static async Task<DataRow?> ExecuteDataRowAsync(this DBHelper dbHelper,
        CommandType commandType, string commandText, DataParameterCollection? parameters = null)
    {
        var dataTable = await dbHelper.ExecuteDataTableAsync(commandType, commandText, parameters);
        return dataTable?.Rows.Count > 0 ? dataTable.Rows[0] : null;
    }

    /// <summary>
    /// Execute query and return first row as DataRow (Text command type)
    /// </summary>
    public static async Task<DataRow?> ExecuteDataRowAsync(this DBHelper dbHelper,
        string commandText, DataParameterCollection? parameters = null)
    {
        return await dbHelper.ExecuteDataRowAsync(CommandType.Text, commandText, parameters);
    }
}