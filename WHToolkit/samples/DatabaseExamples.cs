using WHToolkit.Database.SQLite;

namespace WHToolkit.Samples;

/// <summary>
/// Simple database usage examples
/// </summary>
public class DatabaseExamples
{
    /// <summary>
    /// Basic query example
    /// </summary>
    public static void SimpleQueryExample()
    {
        // Open connection in constructor
        using var db = new DbHelperLite("sample.db");

        // Execute query
        var users = db.ExecuteList<User>("SELECT * FROM Users WHERE Age > 18");

        foreach (var user in users)
        {
            Console.WriteLine($"{user.Name}: {user.Age}");
        }

        // Connection automatically closed and disposed when 'using' ends
    }

    /// <summary>
    /// Insert/Update/Delete example
    /// </summary>
    public static void ModifyDataExample()
    {
        using var db = new DbHelperLite("sample.db");

        // Insert
        int inserted = db.ExecuteNonQuery(
            "INSERT INTO Users (Name, Age) VALUES ('John', 25)"
        );
        Console.WriteLine($"Inserted {inserted} rows");

        // Update
        int updated = db.ExecuteNonQuery(
            "UPDATE Users SET Age = 26 WHERE Name = 'John'"
        );
        Console.WriteLine($"Updated {updated} rows");

        // Delete
        int deleted = db.ExecuteNonQuery(
            "DELETE FROM Users WHERE Name = 'John'"
        );
        Console.WriteLine($"Deleted {deleted} rows");
    }

    /// <summary>
    /// DataTable example
    /// </summary>
    public static void DataTableExample()
    {
        using var db = new DbHelperLite("sample.db");

        var dataTable = db.ExecuteDataTable("SELECT * FROM Users");

        foreach (var row in dataTable.Rows.Cast<System.Data.DataRow>())
        {
            Console.WriteLine($"{row["Name"]}: {row["Age"]}");
        }
    }

    /// <summary>
    /// Scalar query example
    /// </summary>
    public static void ScalarExample()
    {
        using var db = new DbHelperLite("sample.db");

        var count = db.ExecuteScalar("SELECT COUNT(*) FROM Users");
        Console.WriteLine($"Total users: {count}");

        var maxAge = db.ExecuteScalar("SELECT MAX(Age) FROM Users");
        Console.WriteLine($"Max age: {maxAge}");
    }

    /// <summary>
    /// Create table example
    /// </summary>
    public static void CreateTableExample()
    {
        using var db = new DbHelperLite("sample.db");

        db.ExecuteNonQuery(@"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Age INTEGER NOT NULL
            )
        ");

        Console.WriteLine("Table created successfully");
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}
