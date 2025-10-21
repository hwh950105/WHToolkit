using System.Data;
using HWH.Database;

namespace HWH.Framework.Examples;

/// <summary>
/// Basic usage examples for HWH.Framework database functionality
/// </summary>
public class BasicUsageExamples
{
    public async Task SqlServerExample()
    {
        // Create SQL Server connection
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL, 
            "Server=localhost;Database=TestDB;Integrated Security=true");

        if (db == null) return;

        // Execute DataSet query with parameters
        var parameters = new DataParameterCollection();
        parameters.Add("@MinAge", 18);
        parameters.Add("@Status", "Active");

        var dataSet = await db.ExecuteDataSetAsync(CommandType.Text,
            "SELECT Id, Name, Email, Age FROM Users WHERE Age >= @MinAge AND Status = @Status",
            parameters);

        if (dataSet.Tables.Count > 0)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Console.WriteLine($"User: {row["Name"]}, Email: {row["Email"]}, Age: {row["Age"]}");
            }
        }

        // Execute scalar query
        var totalUsers = await db.ExecuteScalarAsync(CommandType.Text,
            "SELECT COUNT(*) FROM Users WHERE Status = 'Active'");
        Console.WriteLine($"Total active users: {totalUsers}");

        // Execute non-query (INSERT/UPDATE/DELETE)
        var newUserParams = new DataParameterCollection();
        newUserParams.Add("@Name", "John Doe");
        newUserParams.Add("@Email", "john.doe@example.com");
        newUserParams.Add("@Age", 25);

        var rowsAffected = await db.ExecuteNonQueryAsync(CommandType.Text,
            "INSERT INTO Users (Name, Email, Age, Status, CreatedDate) VALUES (@Name, @Email, @Age, 'Active', GETDATE())",
            newUserParams);
        Console.WriteLine($"Rows affected: {rowsAffected}");
    }

    public async Task TransactionExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL,
            "Server=localhost;Database=TestDB;Integrated Security=true");

        if (db == null) return;

        try
        {
            // Begin transaction
            await db.TransactionBeginAsync();

            // First operation
            var userParams = new DataParameterCollection();
            userParams.Add("@Name", "Jane Smith");
            userParams.Add("@Email", "jane.smith@example.com");

            await db.ExecuteNonQueryAsync(CommandType.Text,
                "INSERT INTO Users (Name, Email, Status, CreatedDate) VALUES (@Name, @Email, 'Active', GETDATE())",
                userParams);

            // Get the inserted user ID
            var userId = await db.ExecuteScalarAsync(CommandType.Text,
                "SELECT SCOPE_IDENTITY()");

            // Second operation - add user role
            var roleParams = new DataParameterCollection();
            roleParams.Add("@UserId", userId);
            roleParams.Add("@RoleName", "User");

            await db.ExecuteNonQueryAsync(CommandType.Text,
                "INSERT INTO UserRoles (UserId, RoleName) VALUES (@UserId, @RoleName)",
                roleParams);

            // Commit transaction
            await db.TransactionCommitAsync();
            Console.WriteLine("Transaction completed successfully");
        }
        catch (Exception ex)
        {
            // Rollback on error
            await db.TransactionRollbackAsync();
            Console.WriteLine($"Transaction rolled back: {ex.Message}");
            throw;
        }
    }

    public async Task MultiProviderExample()
    {
        // Different connection strings for different providers
        var connections = new Dictionary<ProviderKind, string>
        {
            [ProviderKind.MSSQL] = "Server=localhost;Database=TestDB;Integrated Security=true",
            [ProviderKind.MySQL] = "Server=localhost;Database=testdb;Uid=root;Pwd=password;",
            [ProviderKind.PostgreSQL] = "Host=localhost;Database=testdb;Username=postgres;Password=password",
            [ProviderKind.Oracle] = "Data Source=localhost:1521/XE;User Id=hr;Password=password;"
        };

        foreach (var connection in connections)
        {
            try
            {
                using var db = await DBHelper.CreateConnectionAsync(connection.Key, connection.Value);
                
                if (db == null) continue;

                // Test basic connectivity
                var result = await TestDatabaseConnection(db, connection.Key);
                Console.WriteLine($"{connection.Key}: {(result ? "Connected" : "Failed")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{connection.Key}: Error - {ex.Message}");
            }
        }
    }

    private async Task<bool> TestDatabaseConnection(DBHelper db, ProviderKind provider)
    {
        try
        {
            string testQuery = provider switch
            {
                ProviderKind.MSSQL => "SELECT 1",
                ProviderKind.MySQL => "SELECT 1",
                ProviderKind.PostgreSQL => "SELECT 1",
                ProviderKind.Oracle => "SELECT 1 FROM DUAL",
                _ => "SELECT 1"
            };

            var result = await db.ExecuteScalarAsync(CommandType.Text, testQuery);
            return result != null && result.ToString() == "1";
        }
        catch
        {
            return false;
        }
    }

    public async Task StoredProcedureExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL,
            "Server=localhost;Database=TestDB;Integrated Security=true");

        if (db == null) return;

        // Call stored procedure with input and output parameters
        var parameters = new DataParameterCollection();
        parameters.Add(ParameterDirection.Input, "@UserId", 123);
        parameters.Add(ParameterDirection.Input, "@StartDate", DateTime.Now.AddDays(-30));
        parameters.Add(ParameterDirection.Output, DbType.Int32, "@TotalCount", null);

        var result = await db.ExecuteDataSetAsync(CommandType.StoredProcedure,
            "GetUserActivityReport", parameters);

        // Access output parameter value (this would need to be implemented in the framework)
        // var totalCount = parameters["@TotalCount"].Value;
        
        Console.WriteLine($"Retrieved {result.Tables[0].Rows.Count} rows from stored procedure");
    }
}