using HWH.Database;

namespace HWH.Framework.Examples;

/// <summary>
/// 모든 데이터베이스 예제를 실행하는 통합 클래스
/// </summary>
public class DatabaseExamplesRunner
{
    private readonly Dictionary<ProviderKind, string> _connectionStrings;

    public DatabaseExamplesRunner()
    {
        // 기본 연결 문자열들 (실제 환경에 맞게 수정 필요)
        _connectionStrings = new Dictionary<ProviderKind, string>
        {
            [ProviderKind.MSSQL] = "Server=localhost;Database=TestDB;Integrated Security=true;TrustServerCertificate=true",
            [ProviderKind.Oracle] = "Data Source=localhost:1521/XE;User Id=hr;Password=password;",
            [ProviderKind.MySQL] = "Server=localhost;Database=testdb;Uid=root;Pwd=password;Charset=utf8mb4;",
            [ProviderKind.PostgreSQL] = "Host=localhost;Database=testdb;Username=postgres;Password=password;"
        };
    }

    /// <summary>
    /// 연결 문자열 설정
    /// </summary>
    public void SetConnectionString(ProviderKind provider, string connectionString)
    {
        _connectionStrings[provider] = connectionString;
    }

    /// <summary>
    /// 모든 데이터베이스에 대한 연결 테스트
    /// </summary>
    public async Task RunConnectionTests()
    {
        Console.WriteLine("=== 데이터베이스 연결 테스트 ===");
        
        foreach (var connection in _connectionStrings)
        {
            try
            {
                using var db = await DBHelper.CreateConnectionAsync(connection.Key, connection.Value);
                
                if (db == null)
                {
                    Console.WriteLine($"❌ {connection.Key}: 연결 생성 실패");
                    continue;
                }

                // 간단한 연결 테스트
                var testQuery = connection.Key switch
                {
                    ProviderKind.MSSQL => "SELECT 1",
                    ProviderKind.MySQL => "SELECT 1",
                    ProviderKind.PostgreSQL => "SELECT 1",
                    ProviderKind.Oracle => "SELECT 1 FROM DUAL",
                    _ => "SELECT 1"
                };

                var result = await db.ExecuteScalarAsync(System.Data.CommandType.Text, testQuery);
                
                if (result != null && result.ToString() == "1")
                {
                    Console.WriteLine($"✅ {connection.Key}: 연결 성공");
                }
                else
                {
                    Console.WriteLine($"❌ {connection.Key}: 연결 테스트 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ {connection.Key}: 연결 실패 - {ex.Message}");
            }
        }
    }

    /// <summary>
    /// SQL Server 예제 실행
    /// </summary>
    public async Task RunSqlServerExamples()
    {
        if (!_connectionStrings.ContainsKey(ProviderKind.MSSQL))
        {
            Console.WriteLine("SQL Server 연결 문자열이 설정되지 않았습니다.");
            return;
        }

        var sqlServerExamples = new SqlServerExamples();
        
        try
        {
            Console.WriteLine("\n" + "=".PadRight(50, '='));
            Console.WriteLine("SQL SERVER 예제 실행");
            Console.WriteLine("=".PadRight(50, '='));

            await sqlServerExamples.BasicCrudExample();
            await sqlServerExamples.TransactionExample();
            await sqlServerExamples.StoredProcedureExample();
            await sqlServerExamples.BulkDataExample();
            await sqlServerExamples.SqlServerSpecificFeaturesExample();
            await sqlServerExamples.PerformanceExample();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SQL Server 예제 실행 중 오류: {ex.Message}");
        }
    }

    /// <summary>
    /// Oracle 예제 실행
    /// </summary>
    public async Task RunOracleExamples()
    {
        if (!_connectionStrings.ContainsKey(ProviderKind.Oracle))
        {
            Console.WriteLine("Oracle 연결 문자열이 설정되지 않았습니다.");
            return;
        }

        var oracleExamples = new OracleExamples();
        
        try
        {
            Console.WriteLine("\n" + "=".PadRight(50, '='));
            Console.WriteLine("ORACLE 예제 실행");
            Console.WriteLine("=".PadRight(50, '='));

            await oracleExamples.BasicCrudExample();
            await oracleExamples.TransactionExample();
            await oracleExamples.StoredProcedureExample();
            await oracleExamples.BulkDataExample();
            await oracleExamples.OracleSpecificFeaturesExample();
            await oracleExamples.PerformanceExample();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Oracle 예제 실행 중 오류: {ex.Message}");
        }
    }

    /// <summary>
    /// MySQL 예제 실행
    /// </summary>
    public async Task RunMySqlExamples()
    {
        if (!_connectionStrings.ContainsKey(ProviderKind.MySQL))
        {
            Console.WriteLine("MySQL 연결 문자열이 설정되지 않았습니다.");
            return;
        }

        var mysqlExamples = new MySqlExamples();
        
        try
        {
            Console.WriteLine("\n" + "=".PadRight(50, '='));
            Console.WriteLine("MYSQL 예제 실행");
            Console.WriteLine("=".PadRight(50, '='));

            await mysqlExamples.BasicCrudExample();
            await mysqlExamples.TransactionExample();
            await mysqlExamples.StoredProcedureExample();
            await mysqlExamples.BulkDataExample();
            await mysqlExamples.MySqlSpecificFeaturesExample();
            await mysqlExamples.PerformanceExample();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MySQL 예제 실행 중 오류: {ex.Message}");
        }
    }

    /// <summary>
    /// PostgreSQL 예제 실행
    /// </summary>
    public async Task RunPostgreSqlExamples()
    {
        if (!_connectionStrings.ContainsKey(ProviderKind.PostgreSQL))
        {
            Console.WriteLine("PostgreSQL 연결 문자열이 설정되지 않았습니다.");
            return;
        }

        var postgreSqlExamples = new PostgreSqlExamples();
        
        try
        {
            Console.WriteLine("\n" + "=".PadRight(50, '='));
            Console.WriteLine("POSTGRESQL 예제 실행");
            Console.WriteLine("=".PadRight(50, '='));

            await postgreSqlExamples.BasicCrudExample();
            await postgreSqlExamples.TransactionExample();
            await postgreSqlExamples.StoredFunctionExample();
            await postgreSqlExamples.BulkDataExample();
            await postgreSqlExamples.PostgreSqlSpecificFeaturesExample();
            await postgreSqlExamples.PerformanceExample();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"PostgreSQL 예제 실행 중 오류: {ex.Message}");
        }
    }

    /// <summary>
    /// 모든 데이터베이스 예제 실행
    /// </summary>
    public async Task RunAllExamples()
    {
        Console.WriteLine("HWH.Framework 데이터베이스 예제 실행 시작");
        Console.WriteLine($"실행 시간: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        
        // 연결 테스트 먼저 실행
        await RunConnectionTests();

        // 각 데이터베이스별 예제 실행
        await RunSqlServerExamples();
        await RunOracleExamples();
        await RunMySqlExamples();
        await RunPostgreSqlExamples();

        Console.WriteLine("\n" + "=".PadRight(50, '='));
        Console.WriteLine("모든 예제 실행 완료");
        Console.WriteLine("=".PadRight(50, '='));
    }

    /// <summary>
    /// 특정 데이터베이스의 특정 예제만 실행
    /// </summary>
    public async Task RunSpecificExample(ProviderKind provider, string exampleMethod)
    {
        try
        {
            Console.WriteLine($"=== {provider} - {exampleMethod} 예제 실행 ===");

            switch (provider)
            {
                case ProviderKind.MSSQL:
                    var sqlServerExamples = new SqlServerExamples();
                    await ExecuteExampleMethod(sqlServerExamples, exampleMethod);
                    break;

                case ProviderKind.Oracle:
                    var oracleExamples = new OracleExamples();
                    await ExecuteExampleMethod(oracleExamples, exampleMethod);
                    break;

                case ProviderKind.MySQL:
                    var mysqlExamples = new MySqlExamples();
                    await ExecuteExampleMethod(mysqlExamples, exampleMethod);
                    break;

                case ProviderKind.PostgreSQL:
                    var postgreSqlExamples = new PostgreSqlExamples();
                    await ExecuteExampleMethod(postgreSqlExamples, exampleMethod);
                    break;

                default:
                    Console.WriteLine($"지원하지 않는 데이터베이스 프로바이더: {provider}");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"예제 실행 중 오류: {ex.Message}");
        }
    }

    /// <summary>
    /// 리플렉션을 사용하여 특정 예제 메서드 실행
    /// </summary>
    private async Task ExecuteExampleMethod(object exampleInstance, string methodName)
    {
        var method = exampleInstance.GetType().GetMethod(methodName);
        
        if (method == null)
        {
            Console.WriteLine($"메서드 '{methodName}'을 찾을 수 없습니다.");
            return;
        }

        if (method.ReturnType == typeof(Task))
        {
            var task = (Task?)method.Invoke(exampleInstance, null);
            if (task != null)
            {
                await task;
            }
        }
        else
        {
            method.Invoke(exampleInstance, null);
        }
    }

    /// <summary>
    /// 사용 가능한 예제 메서드 목록 표시
    /// </summary>
    public void ShowAvailableExamples()
    {
        Console.WriteLine("=== 사용 가능한 예제 목록 ===");
        
        var exampleTypes = new Dictionary<ProviderKind, Type>
        {
            [ProviderKind.MSSQL] = typeof(SqlServerExamples),
            [ProviderKind.Oracle] = typeof(OracleExamples),
            [ProviderKind.MySQL] = typeof(MySqlExamples),
            [ProviderKind.PostgreSQL] = typeof(PostgreSqlExamples)
        };

        foreach (var exampleType in exampleTypes)
        {
            Console.WriteLine($"\n{exampleType.Key}:");
            var methods = exampleType.Value.GetMethods()
                .Where(m => m.IsPublic && !m.IsStatic && m.DeclaringType == exampleType.Value)
                .Where(m => m.ReturnType == typeof(Task))
                .Select(m => m.Name);

            foreach (var method in methods)
            {
                Console.WriteLine($"  - {method}");
            }
        }
    }

    /// <summary>
    /// 크로스 데이터베이스 호환성 테스트
    /// </summary>
    public async Task RunCompatibilityTest()
    {
        Console.WriteLine("\n=== 크로스 데이터베이스 호환성 테스트 ===");

        var testQuery = "SELECT 1 as test_column";
        
        foreach (var connection in _connectionStrings)
        {
            try
            {
                using var db = await DBHelper.CreateConnectionAsync(connection.Key, connection.Value);
                
                if (db == null) continue;

                // Oracle의 경우 FROM DUAL 추가
                var query = connection.Key == ProviderKind.Oracle 
                    ? "SELECT 1 as test_column FROM DUAL" 
                    : testQuery;

                var result = await db.ExecuteDataSetAsync(System.Data.CommandType.Text, query);
                
                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    var value = result.Tables[0].Rows[0]["test_column"];
                    Console.WriteLine($"✅ {connection.Key}: 호환성 테스트 성공 (결과: {value})");
                }
                else
                {
                    Console.WriteLine($"❌ {connection.Key}: 결과 없음");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ {connection.Key}: 호환성 테스트 실패 - {ex.Message}");
            }
        }
    }
}