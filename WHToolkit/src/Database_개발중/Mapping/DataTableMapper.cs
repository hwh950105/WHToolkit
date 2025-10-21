using System.Data;
using System.Reflection;
using WHToolkit.Database.Attributes;

namespace WHToolkit.Database.Mapping;

/// <summary>
/// Provides mapping functionality between DataTable/DataRow and model objects
/// </summary>
public static class DataTableMapper
{
    /// <summary>
    /// Maps a DataRow to a model object
    /// </summary>
    public static T MapToEntity<T>(DataRow dataRow) where T : class, new()
    {
        var entity = new T();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite && !HasIgnoreAttribute(p))
            .ToArray();

        foreach (var property in properties)
        {
            var columnName = GetColumnName(property);
            
            if (dataRow.Table.Columns.Contains(columnName) && dataRow[columnName] != DBNull.Value)
            {
                var value = dataRow[columnName];
                var convertedValue = ConvertValue(value, property.PropertyType);
                property.SetValue(entity, convertedValue);
            }
        }

        return entity;
    }

    /// <summary>
    /// Maps a DataTable to a list of model objects
    /// </summary>
    public static List<T> MapToList<T>(DataTable dataTable) where T : class, new()
    {
        var results = new List<T>();
        
        foreach (DataRow row in dataTable.Rows)
        {
            results.Add(MapToEntity<T>(row));
        }

        return results;
    }

    /// <summary>
    /// Maps a model object to DataParameterCollection for database operations
    /// </summary>
    public static DataParameterCollection MapToParameters<T>(T entity, bool useColumnNames = true) where T : class
    {
        var parameters = new DataParameterCollection();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && !HasIgnoreAttribute(p))
            .ToArray();

        foreach (var property in properties)
        {
            var parameterName = useColumnNames ? GetColumnName(property) : property.Name;
            var value = property.GetValue(entity);
            parameters.Add(parameterName, value);
        }

        return parameters;
    }

    /// <summary>
    /// Gets the column name for a property, using ColumnAttribute if present
    /// </summary>
    private static string GetColumnName(PropertyInfo property)
    {
        var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();
        return columnAttribute?.ColumnName ?? property.Name;
    }

    /// <summary>
    /// Checks if a property has the IgnoreAttribute
    /// </summary>
    private static bool HasIgnoreAttribute(PropertyInfo property)
    {
        return property.GetCustomAttribute<IgnoreAttribute>() != null;
    }

    /// <summary>
    /// Converts a database value to the target property type
    /// </summary>
    private static object? ConvertValue(object value, Type targetType)
    {
        if (value == null || value == DBNull.Value)
            return GetDefaultValue(targetType);

        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (underlyingType == typeof(string))
            return value.ToString();

        if (underlyingType == typeof(bool))
            return ConvertToBoolean(value);

        if (underlyingType == typeof(DateTime))
            return ConvertToDateTime(value);

        if (underlyingType == typeof(Guid))
            return ConvertToGuid(value);

        if (underlyingType.IsEnum)
            return ConvertToEnum(value, underlyingType);

        // Handle numeric types
        if (IsNumericType(underlyingType))
            return Convert.ChangeType(value, underlyingType);

        // Default conversion
        return Convert.ChangeType(value, underlyingType);
    }

    private static object? GetDefaultValue(Type type)
    {
        if (type.IsValueType && Nullable.GetUnderlyingType(type) == null)
            return Activator.CreateInstance(type);
        
        return null;
    }

    private static bool ConvertToBoolean(object value)
    {
        return value switch
        {
            bool b => b,
            string s => bool.Parse(s),
            int i => i != 0,
            byte b => b != 0,
            _ => Convert.ToBoolean(value)
        };
    }

    private static DateTime ConvertToDateTime(object value)
    {
        return value switch
        {
            DateTime dt => dt,
            string s => DateTime.Parse(s),
            _ => Convert.ToDateTime(value)
        };
    }

    private static Guid ConvertToGuid(object value)
    {
        return value switch
        {
            Guid g => g,
            string s => Guid.Parse(s),
            byte[] bytes => new Guid(bytes),
            _ => Guid.Parse(value.ToString() ?? string.Empty)
        };
    }

    private static object ConvertToEnum(object value, Type enumType)
    {
        return value switch
        {
            string s => Enum.Parse(enumType, s),
            _ => Enum.ToObject(enumType, value)
        };
    }

    private static bool IsNumericType(Type type)
    {
        return type == typeof(byte) || type == typeof(sbyte) ||
               type == typeof(short) || type == typeof(ushort) ||
               type == typeof(int) || type == typeof(uint) ||
               type == typeof(long) || type == typeof(ulong) ||
               type == typeof(float) || type == typeof(double) ||
               type == typeof(decimal);
    }
}