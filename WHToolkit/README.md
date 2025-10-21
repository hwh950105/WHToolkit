# WHToolkit

A personal .NET 8 utility toolkit for multi-database support, SQLite operations, and TCP networking by Woohyun Hwang.

## Supported Database Providers

- **Microsoft SQL Server** - Using Microsoft.Data.SqlClient
- **Oracle Database** - Using Oracle.ManagedDataAccess.Core
- **MySQL** - Using MySql.Data
- **PostgreSQL** - Using Npgsql
- **SQLite** - Using System.Data.SQLite.Core

## Key Features

- **Multi-database support** with provider abstraction
- **Async/await pattern** for modern .NET applications
- **Model mapping** - Automatic object-relational mapping with DataTableMapper
- **Connection pooling and management**
- **Transaction support** with automatic rollback
- **Parameter binding** with type safety
- **Built-in caching** for performance optimization
- **Retry mechanism** for handling transient failures
- **Flexible mapping attributes** for customization
- **Query building utilities** with WhereItem and ComparisonOperator
- **Stored procedure support** with parameter mapping
- **Extension methods** for enhanced functionality
- **TCP networking** - Async TCP client with modern event patterns

## Quick Start

### Installation

Reference the DLL in your project:

```bash
dotnet add reference path/to/WHToolkit.dll
```

### Basic Usage

```csharp
using WHToolkit.Database;
using WHToolkit.Database.Extensions;

// Create database connection
using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL,
    "Server=localhost;Database=TestDB;Integrated Security=true");

if (db == null) return; // Connection failed

// Execute query with parameters
var parameters = new DataParameterCollection();
parameters.Add("@Name", "John Doe");
parameters.Add("@Age", 30);

var result = await db.ExecuteDataSetAsync(CommandType.Text,
    "SELECT * FROM Users WHERE Name = @Name AND Age > @Age", parameters);

// Execute non-query
var rowsAffected = await db.ExecuteNonQueryAsync(CommandType.Text,
    "UPDATE Users SET LastLogin = GETDATE() WHERE Id = @Id",
    new DataParameterCollection { {"@Id", userId} });

// Execute scalar
var count = await db.ExecuteScalarAsync(CommandType.Text,
    "SELECT COUNT(*) FROM Users WHERE Active = 1");
```

### Model Mapping Usage

```csharp
using WHToolkit.Database;
using WHToolkit.Database.Attributes;
using WHToolkit.Database.Mapping;

// Define your model
[Table("Users")]
public class User
{
    [Column("Id")]
    public int UserId { get; set; }

    [Column("Name")]
    public string UserName { get; set; }

    public string Email { get; set; }
    public int? Age { get; set; }
    public bool IsActive { get; set; }

    [Ignore]
    public string DisplayName => $"{UserName} ({Email})";
}

// Use DataTableMapper for object mapping
var dataTable = await db.ExecuteDataTableAsync(CommandType.Text,
    "SELECT Id, Name, Email, Age, IsActive FROM Users WHERE IsActive = 1");

var users = DataTableMapper.MapToList<User>(dataTable);

// Map single object
var singleUserTable = await db.ExecuteDataTableAsync(CommandType.Text,
    "SELECT Id, Name, Email, Age, IsActive FROM Users WHERE Id = @Id",
    new DataParameterCollection { {"@Id", 123} });

var user = DataTableMapper.MapToObject<User>(singleUserTable);

// Map parameters from object
var newUser = new User { UserName = "John", Email = "john@example.com", Age = 30 };
var parameters = DataTableMapper.MapToParameters(newUser);
```

### Transaction Support

```csharp
using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL, connectionString);

try
{
    await db.TransactionBeginAsync();

    await db.ExecuteNonQueryAsync(CommandType.Text,
        "INSERT INTO Users (Name) VALUES (@Name)",
        new DataParameterCollection { {"@Name", "John"} });

    await db.ExecuteNonQueryAsync(CommandType.Text,
        "INSERT INTO UserRoles (UserId, Role) VALUES (@UserId, @Role)",
        new DataParameterCollection { {"@UserId", 1}, {"@Role", "Admin"} });

    await db.TransactionCommitAsync();
}
catch
{
    await db.TransactionRollbackAsync();
    throw;
}
```

### TCP Client Usage

```csharp
using WHToolkit.Network;

var client = new TcpClientH();
client.Host = "192.168.1.100";
client.Port = 8080;

// Subscribe to events
client.ClientConnected += (sender, e) => Console.WriteLine($"Connected to {e.Host}:{e.Port}");
client.DataReceived += (sender, e) => Console.WriteLine($"Received: {e.Text}");
client.ClientDisconnected += (sender, e) => Console.WriteLine($"Disconnected: {e.Reason}");

// Connect and send data
await client.ConnectAsync();
await client.SendAsync("Hello Server!");
```

### Using Different Database Providers

```csharp
// SQL Server
using var sqlDb = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL,
    "Server=localhost;Database=MyDB;Integrated Security=true;TrustServerCertificate=true");

// Oracle (using sequences and :parameter syntax)
using var oracleDb = await DBHelper.CreateConnectionAsync(ProviderKind.Oracle,
    "Data Source=localhost:1521/XE;User Id=hr;Password=password;");

// MySQL (using p_ parameter prefix)
using var mysqlDb = await DBHelper.CreateConnectionAsync(ProviderKind.MySQL,
    "Server=localhost;Database=mydb;Uid=root;Pwd=password;Charset=utf8mb4;");

// PostgreSQL (using @ parameter syntax)
using var pgDb = await DBHelper.CreateConnectionAsync(ProviderKind.PostgreSQL,
    "Host=localhost;Database=mydb;Username=postgres;Password=password");

// SQLite
using var sqliteDb = new SQLiteDbHelper.DbHelper("mydata.db");
var users = await sqliteDb.ExecuteListAsync<User>("SELECT * FROM Users");
```

## Project Structure

```
WHToolkit/
├── src/
│   ├── Core/              # Core abstractions and models
│   ├── Database/          # Multi-database support
│   │   ├── Providers/     # Database provider implementations
│   │   ├── Extensions/    # Extension methods
│   │   └── Mapping/       # Object-relational mapping
│   └── Network/           # TCP networking
└── WHToolkit.dll          # Compiled toolkit
```

## Author

**Woohyun Hwang**
Personal utility toolkit for .NET development

## License

This project is licensed under the MIT License - see the LICENSE file for details.
