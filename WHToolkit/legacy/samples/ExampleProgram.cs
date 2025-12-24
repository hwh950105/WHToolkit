using HWH.Database;

namespace HWH.Framework.Examples;

/// <summary>
/// HWH.Framework 예제 실행을 위한 메인 프로그램
/// </summary>
public class ExampleProgram
{
    /// <summary>
    /// 예제 프로그램 진입점
    /// </summary>
    public static async Task Main(string[] args)
    {
        var runner = new DatabaseExamplesRunner();
        
        Console.WriteLine("HWH.Framework Database Examples");
        Console.WriteLine("================================");
        Console.WriteLine();

        // 연결 문자열 설정 (실제 환경에 맞게 수정)
        SetupConnectionStrings(runner);

        if (args.Length == 0)
        {
            await ShowMainMenu(runner);
        }
        else
        {
            await ProcessCommandLineArgs(runner, args);
        }
    }

    /// <summary>
    /// 연결 문자열 설정
    /// </summary>
    private static void SetupConnectionStrings(DatabaseExamplesRunner runner)
    {
        // 환경 변수에서 연결 문자열 읽기 (선택사항)
        var sqlServerCs = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING");
        var oracleCs = Environment.GetEnvironmentVariable("ORACLE_CONNECTION_STRING");
        var mysqlCs = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
        var postgresCs = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

        // 환경 변수가 설정되어 있으면 사용
        if (!string.IsNullOrEmpty(sqlServerCs))
            runner.SetConnectionString(ProviderKind.MSSQL, sqlServerCs);
        
        if (!string.IsNullOrEmpty(oracleCs))
            runner.SetConnectionString(ProviderKind.Oracle, oracleCs);
        
        if (!string.IsNullOrEmpty(mysqlCs))
            runner.SetConnectionString(ProviderKind.MySQL, mysqlCs);
        
        if (!string.IsNullOrEmpty(postgresCs))
            runner.SetConnectionString(ProviderKind.PostgreSQL, postgresCs);
    }

    /// <summary>
    /// 메인 메뉴 표시 및 사용자 입력 처리
    /// </summary>
    private static async Task ShowMainMenu(DatabaseExamplesRunner runner)
    {
        while (true)
        {
            Console.WriteLine("다음 중 선택하세요:");
            Console.WriteLine("1. 연결 테스트");
            Console.WriteLine("2. 모든 예제 실행");
            Console.WriteLine("3. SQL Server 예제");
            Console.WriteLine("4. Oracle 예제");
            Console.WriteLine("5. MySQL 예제");
            Console.WriteLine("6. PostgreSQL 예제");
            Console.WriteLine("7. 호환성 테스트");
            Console.WriteLine("8. 사용 가능한 예제 목록");
            Console.WriteLine("9. 특정 예제 실행");
            Console.WriteLine("0. 종료");
            Console.Write("선택: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await runner.RunConnectionTests();
                        break;
                    case "2":
                        await runner.RunAllExamples();
                        break;
                    case "3":
                        await runner.RunSqlServerExamples();
                        break;
                    case "4":
                        await runner.RunOracleExamples();
                        break;
                    case "5":
                        await runner.RunMySqlExamples();
                        break;
                    case "6":
                        await runner.RunPostgreSqlExamples();
                        break;
                    case "7":
                        await runner.RunCompatibilityTest();
                        break;
                    case "8":
                        runner.ShowAvailableExamples();
                        break;
                    case "9":
                        await RunSpecificExampleInteractive(runner);
                        break;
                    case "0":
                        Console.WriteLine("프로그램을 종료합니다.");
                        return;
                    default:
                        Console.WriteLine("올바른 번호를 선택해주세요.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생: {ex.Message}");
            }

            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    /// <summary>
    /// 특정 예제 실행을 위한 대화형 메뉴
    /// </summary>
    private static async Task RunSpecificExampleInteractive(DatabaseExamplesRunner runner)
    {
        Console.WriteLine("데이터베이스 선택:");
        Console.WriteLine("1. SQL Server");
        Console.WriteLine("2. Oracle");
        Console.WriteLine("3. MySQL");
        Console.WriteLine("4. PostgreSQL");
        Console.Write("선택: ");

        var dbChoice = Console.ReadLine();
        ProviderKind provider;

        switch (dbChoice)
        {
            case "1":
                provider = ProviderKind.MSSQL;
                break;
            case "2":
                provider = ProviderKind.Oracle;
                break;
            case "3":
                provider = ProviderKind.MySQL;
                break;
            case "4":
                provider = ProviderKind.PostgreSQL;
                break;
            default:
                Console.WriteLine("올바른 데이터베이스를 선택해주세요.");
                return;
        }

        Console.WriteLine($"\n{provider} 사용 가능한 예제 메서드:");
        Console.WriteLine("1. BasicCrudExample");
        Console.WriteLine("2. TransactionExample");
        Console.WriteLine("3. StoredProcedureExample (또는 StoredFunctionExample)");
        Console.WriteLine("4. BulkDataExample");
        Console.WriteLine("5. 특화 기능 예제");
        Console.WriteLine("6. PerformanceExample");
        Console.Write("선택: ");

        var methodChoice = Console.ReadLine();
        string methodName;

        switch (methodChoice)
        {
            case "1":
                methodName = "BasicCrudExample";
                break;
            case "2":
                methodName = "TransactionExample";
                break;
            case "3":
                methodName = provider == ProviderKind.PostgreSQL ? "StoredFunctionExample" : "StoredProcedureExample";
                break;
            case "4":
                methodName = "BulkDataExample";
                break;
            case "5":
                methodName = provider switch
                {
                    ProviderKind.MSSQL => "SqlServerSpecificFeaturesExample",
                    ProviderKind.Oracle => "OracleSpecificFeaturesExample",
                    ProviderKind.MySQL => "MySqlSpecificFeaturesExample",
                    ProviderKind.PostgreSQL => "PostgreSqlSpecificFeaturesExample",
                    _ => "SpecificFeaturesExample"
                };
                break;
            case "6":
                methodName = "PerformanceExample";
                break;
            default:
                Console.WriteLine("올바른 예제를 선택해주세요.");
                return;
        }

        await runner.RunSpecificExample(provider, methodName);
    }

    /// <summary>
    /// 명령줄 인수 처리
    /// </summary>
    private static async Task ProcessCommandLineArgs(DatabaseExamplesRunner runner, string[] args)
    {
        var command = args[0].ToLower();

        switch (command)
        {
            case "test":
                await runner.RunConnectionTests();
                break;

            case "all":
                await runner.RunAllExamples();
                break;

            case "sqlserver":
                await runner.RunSqlServerExamples();
                break;

            case "oracle":
                await runner.RunOracleExamples();
                break;

            case "mysql":
                await runner.RunMySqlExamples();
                break;

            case "postgresql":
            case "postgres":
                await runner.RunPostgreSqlExamples();
                break;

            case "compatibility":
                await runner.RunCompatibilityTest();
                break;

            case "list":
                runner.ShowAvailableExamples();
                break;

            case "run":
                if (args.Length >= 3)
                {
                    if (Enum.TryParse<ProviderKind>(args[1], true, out var provider))
                    {
                        await runner.RunSpecificExample(provider, args[2]);
                    }
                    else
                    {
                        Console.WriteLine($"지원하지 않는 데이터베이스: {args[1]}");
                        ShowUsage();
                    }
                }
                else
                {
                    Console.WriteLine("사용법: run <provider> <method>");
                    ShowUsage();
                }
                break;

            case "help":
            case "--help":
            case "-h":
                ShowUsage();
                break;

            default:
                Console.WriteLine($"알 수 없는 명령: {command}");
                ShowUsage();
                break;
        }
    }

    /// <summary>
    /// 사용법 표시
    /// </summary>
    private static void ShowUsage()
    {
        Console.WriteLine("HWH.Framework Database Examples");
        Console.WriteLine("사용법:");
        Console.WriteLine("  ExampleProgram [command] [options]");
        Console.WriteLine();
        Console.WriteLine("명령:");
        Console.WriteLine("  test                     - 모든 데이터베이스 연결 테스트");
        Console.WriteLine("  all                      - 모든 예제 실행");
        Console.WriteLine("  sqlserver                - SQL Server 예제 실행");
        Console.WriteLine("  oracle                   - Oracle 예제 실행");
        Console.WriteLine("  mysql                    - MySQL 예제 실행");
        Console.WriteLine("  postgresql               - PostgreSQL 예제 실행");
        Console.WriteLine("  compatibility            - 크로스 데이터베이스 호환성 테스트");
        Console.WriteLine("  list                     - 사용 가능한 예제 목록 표시");
        Console.WriteLine("  run <provider> <method>  - 특정 예제 실행");
        Console.WriteLine("  help                     - 이 도움말 표시");
        Console.WriteLine();
        Console.WriteLine("예시:");
        Console.WriteLine("  ExampleProgram test");
        Console.WriteLine("  ExampleProgram run MSSQL BasicCrudExample");
        Console.WriteLine("  ExampleProgram sqlserver");
        Console.WriteLine();
        Console.WriteLine("환경 변수:");
        Console.WriteLine("  SQLSERVER_CONNECTION_STRING  - SQL Server 연결 문자열");
        Console.WriteLine("  ORACLE_CONNECTION_STRING     - Oracle 연결 문자열");
        Console.WriteLine("  MYSQL_CONNECTION_STRING      - MySQL 연결 문자열");
        Console.WriteLine("  POSTGRES_CONNECTION_STRING   - PostgreSQL 연결 문자열");
    }
}