using System;
using System.Data;
using WHToolkit.Database;

class QuickTest
{
    static void Main()
    {
        Console.WriteLine("=== WHToolkit Database Helper 테스트 ===\n");

        // PostgreSQL
        TestDB("PostgreSQL", () => {
            using var db = new NpgHelper("Host=localhost;Port=5432;Database=testdb;Username=testuser;Password=Test1234!");
            var result = db.ExecuteDataTable(CommandType.Text, "SELECT version() as Version, current_timestamp as Time");
            Console.WriteLine($"  Version: {result.Rows[0]["Version"]}\n  Time: {result.Rows[0]["Time"]}");
        });

        // MySQL
        TestDB("MySQL", () => {
            using var db = new MySqlHelper("Server=localhost;Port=3306;Database=testdb;Uid=testuser;Pwd=Test1234!");
            var result = db.ExecuteDataTable(CommandType.Text, "SELECT VERSION() as Version, NOW() as Time");
            Console.WriteLine($"  Version: {result.Rows[0]["Version"]}\n  Time: {result.Rows[0]["Time"]}");
        });

        // MariaDB
        TestDB("MariaDB", () => {
            using var db = new MariaDbHelper("Server=localhost;Port=3307;Database=testdb;Uid=testuser;Pwd=Test1234!");
            var result = db.ExecuteDataTable(CommandType.Text, "SELECT VERSION() as Version, NOW() as Time");
            Console.WriteLine($"  Version: {result.Rows[0]["Version"]}\n  Time: {result.Rows[0]["Time"]}");
        });

        // MS SQL Server
        TestDB("MS SQL", () => {
            using var db = new MsSqlHelper("Server=localhost,1433;Database=master;User Id=sa;Password=Test1234!;TrustServerCertificate=True");
            var result = db.ExecuteDataTable(CommandType.Text, "SELECT @@VERSION as Version, GETDATE() as Time");
            Console.WriteLine($"  Version: {result.Rows[0]["Version"].ToString().Substring(0, 50)}...\n  Time: {result.Rows[0]["Time"]}");
        });

        // Oracle
        TestDB("Oracle", () => {
            using var db = new OracleHelper("Data Source=localhost:1521/XEPDB1;User Id=testuser;Password=Test1234!");
            var result = db.ExecuteDataTable(CommandType.Text, "SELECT * FROM V$VERSION WHERE ROWNUM = 1");
            Console.WriteLine($"  Version: {result.Rows[0][0]}\n  Time: {DateTime.Now}");
        });

        Console.WriteLine("\n✅ 모든 테스트 완료!");
        Console.ReadKey();
    }

    static void TestDB(string name, Action test)
    {
        Console.WriteLine($"[ {name} 테스트 ]");
        try
        {
            test();
            Console.WriteLine($"  ✅ 성공!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ❌ 실패: {ex.Message}\n");
        }
    }
}
