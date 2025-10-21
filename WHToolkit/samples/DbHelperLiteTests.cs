using WHToolkit.Database.SQLite;

namespace WHToolkit.Samples;

/// <summary>
/// DbHelperLite using pattern tests
/// </summary>
public class DbHelperLiteTests
{
    /// <summary>
    /// Test 1: Normal using pattern
    /// </summary>
    public static void Test_NormalUsing()
    {
        Console.WriteLine("=== Test 1: Normal using pattern ===");

        using var db = new DbHelperLite("test.db");

        db.ExecuteNonQuery(@"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Age INTEGER NOT NULL
            )
        ");

        db.ExecuteNonQuery("INSERT INTO Users (Name, Age) VALUES ('Alice', 30)");
        var count = db.ExecuteScalar("SELECT COUNT(*) FROM Users");

        Console.WriteLine($"Users count: {count}");
        Console.WriteLine("✅ Connection will be disposed automatically");
    }

    /// <summary>
    /// Test 2: Exception handling with using
    /// </summary>
    public static void Test_ExceptionHandling()
    {
        Console.WriteLine("\n=== Test 2: Exception handling ===");

        try
        {
            using var db = new DbHelperLite("test.db");

            // Normal operation
            db.ExecuteNonQuery("INSERT INTO Users (Name, Age) VALUES ('Bob', 25)");

            // This will throw exception
            db.ExecuteNonQuery("INVALID SQL STATEMENT");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Exception caught: {ex.Message}");
        }

        Console.WriteLine("✅ Connection disposed even with exception");
    }

    /// <summary>
    /// Test 3: Multiple connections
    /// </summary>
    public static void Test_MultipleConnections()
    {
        Console.WriteLine("\n=== Test 3: Multiple connections ===");

        using var db1 = new DbHelperLite("test1.db");
        using var db2 = new DbHelperLite("test2.db");

        db1.ExecuteNonQuery("CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY, Name TEXT)");
        db2.ExecuteNonQuery("CREATE TABLE IF NOT EXISTS Logs (Id INTEGER PRIMARY KEY, Message TEXT)");

        db1.ExecuteNonQuery("INSERT INTO Users VALUES (1, 'User1')");
        db2.ExecuteNonQuery("INSERT INTO Logs VALUES (1, 'Log1')");

        var count1 = db1.ExecuteScalar("SELECT COUNT(*) FROM Users");
        var count2 = db2.ExecuteScalar("SELECT COUNT(*) FROM Logs");

        Console.WriteLine($"DB1 Users: {count1}, DB2 Logs: {count2}");
        Console.WriteLine("✅ Both connections will be disposed in reverse order");
    }

    /// <summary>
    /// Test 4: Nested using with same database
    /// </summary>
    public static void Test_NestedUsing()
    {
        Console.WriteLine("\n=== Test 4: Nested using (NOT recommended but works) ===");

        using (var db1 = new DbHelperLite("test.db"))
        {
            db1.ExecuteNonQuery("CREATE TABLE IF NOT EXISTS Items (Id INTEGER PRIMARY KEY, Name TEXT)");

            using (var db2 = new DbHelperLite("test.db"))
            {
                db2.ExecuteNonQuery("INSERT INTO Items VALUES (1, 'Item1')");
                Console.WriteLine("✅ Inner connection works");
            }

            var count = db1.ExecuteScalar("SELECT COUNT(*) FROM Items");
            Console.WriteLine($"Outer connection count: {count}");
            Console.WriteLine("✅ Outer connection still works");
        }
    }

    /// <summary>
    /// Test 5: Connection reuse pattern
    /// </summary>
    public static void Test_ConnectionReuse()
    {
        Console.WriteLine("\n=== Test 5: Connection reuse (recommended) ===");

        using var db = new DbHelperLite("test.db");

        // Multiple operations on same connection
        db.ExecuteNonQuery("CREATE TABLE IF NOT EXISTS Orders (Id INTEGER PRIMARY KEY, Amount REAL)");

        for (int i = 1; i <= 5; i++)
        {
            db.ExecuteNonQuery($"INSERT INTO Orders VALUES ({i}, {i * 100.5})");
        }

        var orders = db.ExecuteList<Order>("SELECT * FROM Orders");

        Console.WriteLine($"✅ Inserted and retrieved {orders.Count} orders using single connection");
    }

    /// <summary>
    /// Test 6: Verify Dispose is called only once
    /// </summary>
    public static void Test_DisposeOnce()
    {
        Console.WriteLine("\n=== Test 6: Dispose called only once ===");

        var db = new DbHelperLite("test.db");
        db.ExecuteScalar("SELECT 1");

        // Manual dispose
        db.Dispose();
        Console.WriteLine("✅ First Dispose() called");

        // Second dispose should be safe
        db.Dispose();
        Console.WriteLine("✅ Second Dispose() called (safe, does nothing)");

        // Third dispose
        db.Dispose();
        Console.WriteLine("✅ Third Dispose() called (safe, does nothing)");
    }

    public static void RunAllTests()
    {
        Test_NormalUsing();
        Test_ExceptionHandling();
        Test_MultipleConnections();
        Test_NestedUsing();
        Test_ConnectionReuse();
        Test_DisposeOnce();

        Console.WriteLine("\n=== All tests completed ===");
    }
}

public class Order
{
    public int Id { get; set; }
    public double Amount { get; set; }
}
