using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WHToolkit.Database;
using WHToolkit.Tests.Models;

namespace WHToolkit.Tests
{
    class DatabaseIntegrationTest
    {
        static void Main()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   WHToolkit Database Helper 통합 테스트               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            // 연결 문자열
            var connections = new Dictionary<string, (string Name, Func<string, IDisposable> Factory, string ConnStr)>
            {
                ["postgres"] = ("PostgreSQL", cs => new NpgHelper(cs), 
                    "Host=localhost;Port=5432;Database=testdb;Username=testuser;Password=Test1234!"),
                
                ["mysql"] = ("MySQL", cs => new MySqlHelper(cs), 
                    "Server=localhost;Port=3306;Database=testdb;Uid=testuser;Pwd=Test1234!"),
                
                ["mariadb"] = ("MariaDB", cs => new MariaDbHelper(cs), 
                    "Server=localhost;Port=3307;Database=testdb;Uid=testuser;Pwd=Test1234!"),
                
                ["mssql"] = ("MS SQL Server", cs => new MsSqlHelper(cs), 
                    "Server=localhost,1433;Database=testdb;User Id=sa;Password=Test1234!;TrustServerCertificate=True"),
                
                ["oracle"] = ("Oracle", cs => new OracleHelper(cs), 
                    "Data Source=localhost:1521/XEPDB1;User Id=testuser;Password=Test1234!")
            };

            int passed = 0, failed = 0;

            foreach (var (key, (name, factory, connStr)) in connections)
            {
                Console.WriteLine($"\n{'='} {name} 테스트 {'='}".PadRight(60, '='));
                
                try
                {
                    TestDatabase(name, key, factory, connStr);
                    passed++;
                    Console.WriteLine($"✅ {name} 모든 테스트 통과!\n");
                }
                catch (Exception ex)
                {
                    failed++;
                    Console.WriteLine($"❌ {name} 실패: {ex.Message}\n");
                }
            }

            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  테스트 결과: ✅ {passed}개 성공 / ❌ {failed}개 실패".PadRight(56) + "║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            
            Console.WriteLine("\n아무 키나 눌러 종료...");
            Console.ReadKey();
        }

        static void TestDatabase(string name, string type, Func<string, IDisposable> factory, string connStr)
        {
            dynamic helper = factory(connStr);

            try
            {
                // 1. ExecuteList 테스트
                Console.WriteLine("  📋 [1/5] ExecuteList 테스트...");
                List<User> users = null;
                
                if (type == "postgres")
                    users = ((NpgHelper)helper).ExecuteList<User>(CommandType.Text, "SELECT * FROM users ORDER BY id");
                else if (type == "mysql")
                    users = ((MySqlHelper)helper).ExecuteList<User>(CommandType.Text, "SELECT * FROM users ORDER BY id");
                else if (type == "mariadb")
                    users = ((MariaDbHelper)helper).ExecuteList<User>(CommandType.Text, "SELECT * FROM users ORDER BY id");
                else if (type == "mssql")
                    users = ((MsSqlHelper)helper).ExecuteList<User>(CommandType.Text, "SELECT * FROM users ORDER BY id");
                else if (type == "oracle")
                    users = ((OracleHelper)helper).ExecuteList<User>(CommandType.Text, "SELECT * FROM users ORDER BY id");

                Console.WriteLine($"      ✓ {users.Count}명의 사용자 조회 성공");
                foreach (var user in users)
                {
                    Console.WriteLine($"        - {user}");
                }

                // 2. ExecuteDataTable 테스트
                Console.WriteLine("\n  📊 [2/5] ExecuteDataTable 테스트...");
                DataTable dt = null;
                
                if (type == "postgres")
                    dt = ((NpgHelper)helper).ExecuteDataTable(CommandType.Text, "SELECT COUNT(*) as total, AVG(age) as avg_age FROM users");
                else if (type == "mysql")
                    dt = ((MySqlHelper)helper).ExecuteDataTable(CommandType.Text, "SELECT COUNT(*) as total, AVG(age) as avg_age FROM users");
                else if (type == "mariadb")
                    dt = ((MariaDbHelper)helper).ExecuteDataTable(CommandType.Text, "SELECT COUNT(*) as total, AVG(age) as avg_age FROM users");
                else if (type == "mssql")
                    dt = ((MsSqlHelper)helper).ExecuteDataTable(CommandType.Text, "SELECT COUNT(*) as total, AVG(age) as avg_age FROM users");
                else if (type == "oracle")
                    dt = ((OracleHelper)helper).ExecuteDataTable(CommandType.Text, "SELECT COUNT(*) as total, AVG(age) as avg_age FROM users");

                Console.WriteLine($"      ✓ 총 사용자: {dt.Rows[0]["total"]}, 평균 나이: {Convert.ToDecimal(dt.Rows[0]["avg_age"]):F1}");

                // 3. ExecuteDataSet 테스트
                Console.WriteLine("\n  📦 [3/5] ExecuteDataSet 테스트...");
                string multiQuery = type == "oracle" 
                    ? "SELECT * FROM users WHERE is_active = 1"  // Oracle은 멀티쿼리 지원 안함
                    : "SELECT * FROM users WHERE is_active = 1";
                
                DataSet ds = null;
                if (type == "postgres")
                    ds = ((NpgHelper)helper).ExecuteDataSet(CommandType.Text, multiQuery);
                else if (type == "mysql")
                    ds = ((MySqlHelper)helper).ExecuteDataSet(CommandType.Text, multiQuery);
                else if (type == "mariadb")
                    ds = ((MariaDbHelper)helper).ExecuteDataSet(CommandType.Text, multiQuery);
                else if (type == "mssql")
                    ds = ((MsSqlHelper)helper).ExecuteDataSet(CommandType.Text, multiQuery);
                else if (type == "oracle")
                    ds = ((OracleHelper)helper).ExecuteDataSet(CommandType.Text, multiQuery);

                Console.WriteLine($"      ✓ DataSet 테이블 수: {ds.Tables.Count}, 활성 사용자: {ds.Tables[0].Rows.Count}명");

                // 4. ExecuteNonQuery 테스트 (INSERT)
                Console.WriteLine("\n  ➕ [4/5] ExecuteNonQuery (INSERT) 테스트...");
                
                if (type == "postgres")
                {
                    var pg = (NpgHelper)helper;
                    pg.Parameters.Add("name", "테스트사용자");
                    pg.Parameters.Add("email", $"test{DateTime.Now.Ticks}@example.com");
                    pg.Parameters.Add("age", 40);
                    int affected = pg.ExecuteNonQuery(CommandType.Text, 
                        "INSERT INTO users (name, email, age) VALUES (@name, @email, @age)");
                    Console.WriteLine($"      ✓ {affected}개 행 삽입 성공");
                }
                else if (type == "mysql" || type == "mariadb")
                {
                    dynamic db = helper;
                    db.Parameters.Add("@name", "테스트사용자");
                    db.Parameters.Add("@email", $"test{DateTime.Now.Ticks}@example.com");
                    db.Parameters.Add("@age", 40);
                    int affected = db.ExecuteNonQuery(CommandType.Text, 
                        "INSERT INTO users (name, email, age) VALUES (@name, @email, @age)");
                    Console.WriteLine($"      ✓ {affected}개 행 삽입 성공");
                }
                else if (type == "mssql")
                {
                    var ms = (MsSqlHelper)helper;
                    ms.Parameters.Add("@name", "테스트사용자");
                    ms.Parameters.Add("@email", $"test{DateTime.Now.Ticks}@example.com");
                    ms.Parameters.Add("@age", 40);
                    int affected = ms.ExecuteNonQuery(CommandType.Text, 
                        "INSERT INTO users (name, email, age) VALUES (@name, @email, @age)");
                    Console.WriteLine($"      ✓ {affected}개 행 삽입 성공");
                }
                else if (type == "oracle")
                {
                    var ora = (OracleHelper)helper;
                    ora.Parameters.Add("name", "테스트사용자");
                    ora.Parameters.Add("email", $"test{DateTime.Now.Ticks}@example.com");
                    ora.Parameters.Add("age", 40);
                    int affected = ora.ExecuteNonQuery(CommandType.Text, 
                        "INSERT INTO users (name, email, age) VALUES (:name, :email, :age)");
                    Console.WriteLine($"      ✓ {affected}개 행 삽입 성공");
                }

                // 5. 최종 카운트 확인
                Console.WriteLine("\n  🔢 [5/5] 최종 데이터 확인...");
                DataTable finalCount = null;
                
                if (type == "postgres")
                    finalCount = ((NpgHelper)helper).ExecuteDataTable(CommandType.Text, "SELECT COUNT(*) as total FROM users");
                else if (type == "mysql")
                    finalCount = ((MySqlHelper)helper).ExecuteDataTable(CommandType.Text, "SELECT COUNT(*) as total FROM users");
                else if (type == "mariadb")
                    finalCount = ((MariaDbHelper)helper).ExecuteDataTable(CommandType.Text, "SELECT COUNT(*) as total FROM users");
                else if (type == "mssql")
                    finalCount = ((MsSqlHelper)helper).ExecuteDataTable(CommandType.Text, "SELECT COUNT(*) as total FROM users");
                else if (type == "oracle")
                    finalCount = ((OracleHelper)helper).ExecuteDataTable(CommandType.Text, "SELECT COUNT(*) as total FROM users");

                Console.WriteLine($"      ✓ 최종 사용자 수: {finalCount.Rows[0]["total"]}명");
            }
            finally
            {
                helper?.Dispose();
            }
        }
    }
}

