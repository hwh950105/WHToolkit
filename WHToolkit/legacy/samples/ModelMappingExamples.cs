using System.Data;
using HWH.Database;
using HWH.Database.Attributes;
using HWH.Database.Extensions;

namespace HWH.Framework.Examples;

/// <summary>
/// Example models for demonstrating model mapping functionality
/// </summary>

[Table("Users")]
public class User
{
    [Column("Id")]
    public int UserId { get; set; }

    [Column("Name")]
    public string UserName { get; set; } = string.Empty;

    [Column("Email")]
    public string Email { get; set; } = string.Empty;

    [Column("Age")]
    public int? Age { get; set; }

    [Column("IsActive")]
    public bool IsActive { get; set; }

    [Column("CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [Column("LastLoginDate")]
    public DateTime? LastLoginDate { get; set; }

    // This property will be ignored during mapping
    [Ignore]
    public string DisplayName => $"{UserName} ({Email})";
}

[Table("Products")]
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public bool InStock { get; set; }
    public DateTime CreatedDate { get; set; }
}

[Table("Orders")]
public class Order
{
    [Column("OrderId")]
    public Guid Id { get; set; }

    [Column("UserId")]
    public int UserId { get; set; }

    [Column("OrderDate")]
    public DateTime OrderDate { get; set; }

    [Column("TotalAmount")]
    public decimal TotalAmount { get; set; }

    [Column("Status")]
    public OrderStatus Status { get; set; }

    [Ignore]
    public List<OrderItem> Items { get; set; } = new();
}

public enum OrderStatus
{
    Pending = 0,
    Processing = 1,
    Shipped = 2,
    Delivered = 3,
    Cancelled = 4
}

public class OrderItem
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

/// <summary>
/// Examples demonstrating model mapping functionality
/// </summary>
public class ModelMappingExamples
{
    public async Task UserModelExamples()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL, 
            "Server=localhost;Database=TestDB;Integrated Security=true");

        if (db == null) return;

        // Execute query and return a single User object
        var parameters = new DataParameterCollection();
        parameters.Add("@UserId", 123);

        var user = await db.ExecuteEntityAsync<User>(
            "SELECT Id, Name, Email, Age, IsActive, CreatedDate, LastLoginDate FROM Users WHERE Id = @UserId", 
            parameters);

        if (user != null)
        {
            Console.WriteLine($"User: {user.UserName}, Email: {user.Email}, Active: {user.IsActive}");
            Console.WriteLine($"Display Name: {user.DisplayName}"); // Uses [Ignore] property
        }

        // Execute query and return a list of User objects
        var activeUsers = await db.ExecuteListAsync<User>(
            "SELECT Id, Name, Email, Age, IsActive, CreatedDate, LastLoginDate FROM Users WHERE IsActive = 1");

        Console.WriteLine($"Found {activeUsers.Count} active users:");
        foreach (var activeUser in activeUsers)
        {
            Console.WriteLine($"- {activeUser.UserName} ({activeUser.Email})");
        }

        // Using stored procedure
        var recentUsers = await db.ExecuteStoredProcedureListAsync<User>("GetRecentUsers", 
            new DataParameterCollection { {"@Days", 30} });

        Console.WriteLine($"Found {recentUsers.Count} recent users");
    }

    public async Task ProductModelExamples()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL,
            "Server=localhost;Database=TestDB;Integrated Security=true");

        if (db == null) return;

        // Get products with price range
        var parameters = new DataParameterCollection();
        parameters.Add("@MinPrice", 10.00m);
        parameters.Add("@MaxPrice", 100.00m);

        var products = await db.ExecuteListAsync<Product>(
            "SELECT Id, Name, Price, Description, InStock, CreatedDate FROM Products WHERE Price BETWEEN @MinPrice AND @MaxPrice",
            parameters);

        Console.WriteLine($"Products in price range: {products.Count}");
        foreach (var product in products)
        {
            Console.WriteLine($"- {product.Name}: ${product.Price:F2} (In Stock: {product.InStock})");
        }

        // Get single product by ID
        var product1 = await db.ExecuteEntityAsync<Product>(
            "SELECT Id, Name, Price, Description, InStock, CreatedDate FROM Products WHERE Id = @Id",
            new DataParameterCollection { {"@Id", 1} });

        if (product1 != null)
        {
            Console.WriteLine($"Product: {product1.Name} - ${product1.Price:F2}");
        }
    }

    public async Task OrderModelExamples()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL,
            "Server=localhost;Database=TestDB;Integrated Security=true");

        if (db == null) return;

        // Get orders with enum mapping
        var orders = await db.ExecuteListAsync<Order>(
            "SELECT OrderId as Id, UserId, OrderDate, TotalAmount, Status FROM Orders WHERE OrderDate >= @StartDate",
            new DataParameterCollection { {"@StartDate", DateTime.Now.AddDays(-7)} });

        Console.WriteLine($"Orders from last 7 days: {orders.Count}");
        foreach (var order in orders)
        {
            Console.WriteLine($"Order {order.Id}: ${order.TotalAmount:F2} - Status: {order.Status}");
        }

        // Get single order
        var orderId = Guid.NewGuid(); // Example order ID
        var singleOrder = await db.ExecuteEntityAsync<Order>(
            "SELECT OrderId as Id, UserId, OrderDate, TotalAmount, Status FROM Orders WHERE OrderId = @OrderId",
            new DataParameterCollection { {"@OrderId", orderId} });

        if (singleOrder != null)
        {
            Console.WriteLine($"Order details: {singleOrder.Id}, Total: ${singleOrder.TotalAmount:F2}, Status: {singleOrder.Status}");
        }
    }

    public async Task InsertWithModelMappingExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL,
            "Server=localhost;Database=TestDB;Integrated Security=true");

        if (db == null) return;

        // Create a new user
        var newUser = new User
        {
            UserName = "John Doe",
            Email = "john.doe@example.com",
            Age = 25,
            IsActive = true,
            CreatedDate = DateTime.Now
        };

        // Map model to parameters (excluding Id which is auto-generated)
        var insertSql = @"
            INSERT INTO Users (Name, Email, Age, IsActive, CreatedDate) 
            VALUES (@Name, @Email, @Age, @IsActive, @CreatedDate);
            SELECT SCOPE_IDENTITY();";

        var parameters = new DataParameterCollection();
        parameters.Add("@Name", newUser.UserName);
        parameters.Add("@Email", newUser.Email);
        parameters.Add("@Age", newUser.Age);
        parameters.Add("@IsActive", newUser.IsActive);
        parameters.Add("@CreatedDate", newUser.CreatedDate);

        var newUserId = await db.ExecuteScalarAsync(CommandType.Text, insertSql, parameters);
        Console.WriteLine($"New user created with ID: {newUserId}");

        // Alternative: Use mapping helper for parameters
        // var allParameters = DataTableMapper.MapToParameters(newUser);
        // Remove the Id parameter since it's auto-generated
        // var insertParameters = new DataParameterCollection();
        // foreach (var param in allParameters.Where(p => p.ParameterName != "Id"))
        // {
        //     insertParameters.Add(param);
        // }
    }

    public async Task ComplexQueryExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL,
            "Server=localhost;Database=TestDB;Integrated Security=true");

        if (db == null) return;

        // Complex query with JOINs - create a custom result class
        var query = @"
            SELECT 
                u.Id as UserId,
                u.Name as UserName,
                u.Email,
                COUNT(o.OrderId) as OrderCount,
                ISNULL(SUM(o.TotalAmount), 0) as TotalSpent,
                MAX(o.OrderDate) as LastOrderDate
            FROM Users u
            LEFT JOIN Orders o ON u.Id = o.UserId
            WHERE u.IsActive = 1
            GROUP BY u.Id, u.Name, u.Email
            ORDER BY TotalSpent DESC";

        // Define a custom result class for this query
        var userSummaries = await db.ExecuteListAsync<UserOrderSummary>(query);

        Console.WriteLine("User Order Summary:");
        foreach (var summary in userSummaries)
        {
            Console.WriteLine($"User: {summary.UserName} ({summary.Email})");
            Console.WriteLine($"  Orders: {summary.OrderCount}, Total Spent: ${summary.TotalSpent:F2}");
            Console.WriteLine($"  Last Order: {summary.LastOrderDate?.ToString("yyyy-MM-dd") ?? "Never"}");
        }
    }
}

/// <summary>
/// Custom result class for complex query
/// </summary>
public class UserOrderSummary
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int OrderCount { get; set; }
    public decimal TotalSpent { get; set; }
    public DateTime? LastOrderDate { get; set; }
}