using System.Data;
using System.Data.SQLite;
using System.Reflection;

namespace WHToolkit.Database.SQLite;

/// <summary>
/// Simple SQLite database helper with connection management
/// </summary>
public class DbHelperLite : IDisposable
{
    private readonly string _connectionString;
    private SQLiteConnection? _connection;

    /// <summary>
    /// Gets the current SQLite connection
    /// </summary>
    public SQLiteConnection? Connection => _connection;

    /// <summary>
    /// Initializes a new instance with database path
    /// </summary>
    public DbHelperLite(string databasePath = "app.db")
    {
        _connectionString = $"Data Source={databasePath};Version=3;";
        try
        {
            _connection = new SQLiteConnection(_connectionString);
            _connection.Open();
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite connection error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Executes a non-query SQL statement
    /// </summary>
    public int ExecuteNonQuery(string query)
    {
        try
        {
            using var command = new SQLiteCommand(query, _connection);
            return command.ExecuteNonQuery();
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Executes a query and returns a DataSet
    /// </summary>
    public DataSet ExecuteDataSet(string query)
    {
        try
        {
            using var command = new SQLiteCommand(query, _connection);
            using var adapter = new SQLiteDataAdapter(command);
            var dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Executes a query and returns a DataTable
    /// </summary>
    public DataTable ExecuteDataTable(string query)
    {
        try
        {
            using var command = new SQLiteCommand(query, _connection);
            using var adapter = new SQLiteDataAdapter(command);
            var dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Executes a query and returns a list of objects
    /// </summary>
    public List<T> ExecuteList<T>(string query) where T : new()
    {
        var result = new List<T>();

        try
        {
            using var command = new SQLiteCommand(query, _connection);
            using var reader = command.ExecuteReader();

            var properties = typeof(T).GetProperties();
            var columnNames = Enumerable.Range(0, reader.FieldCount)
                .Select(reader.GetName)
                .ToList();

            while (reader.Read())
            {
                var item = new T();
                foreach (var property in properties)
                {
                    if (columnNames.Contains(property.Name) &&
                        !Equals(reader[property.Name], DBNull.Value))
                    {
                        property.SetValue(item,
                            Convert.ChangeType(reader[property.Name], property.PropertyType),
                            null);
                    }
                }
                result.Add(item);
            }
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }

        return result;
    }

    /// <summary>
    /// Executes a scalar query and returns single value
    /// </summary>
    public object? ExecuteScalar(string query)
    {
        try
        {
            using var command = new SQLiteCommand(query, _connection);
            return command.ExecuteScalar();
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Disposes the database connection
    /// </summary>
    public void Dispose()
    {
        if (_connection != null)
        {
            try
            {
                _connection.Close();
                _connection.Dispose();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite dispose error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected dispose error: {ex.Message}");
            }
            finally
            {
                _connection = null;
            }
        }

        GC.SuppressFinalize(this);
    }
}
