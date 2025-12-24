using WHToolkit.IO;

namespace WHToolkit.Samples
{
    /// <summary>
    /// JsonHelper와 IniHelper 사용 예제
    /// </summary>
    public class IOHelperExamples
    {
        public static async Task RunJsonExamples()
        {
            Console.WriteLine("=== JsonHelper 사용 예제 ===\n");

            // 1. 객체 정의
            var appConfig = new AppConfig
            {
                AppName = "WHToolkit Demo",
                Version = "2.0.0",
                DatabaseSettings = new DatabaseConfig
                {
                    Server = "localhost",
                    Port = 1433,
                    Database = "TestDB",
                    UseSSL = true
                },
                Features = new List<string> { "Logging", "Caching", "Security" }
            };

            string jsonPath = "config.json";

            // 2. JSON 파일로 저장
            await JsonHelper.WriteAsync(jsonPath, appConfig);
            Console.WriteLine($"✅ JSON 파일 저장 완료: {jsonPath}");

            // 3. JSON 파일 읽기
            var loaded = await JsonHelper.ReadAsync<AppConfig>(jsonPath);
            Console.WriteLine($"✅ JSON 파일 읽기: {loaded?.AppName} v{loaded?.Version}");

            // 4. JSON 문자열 직렬화
            string jsonString = JsonHelper.Serialize(appConfig);
            Console.WriteLine($"\n📄 JSON 문자열:\n{jsonString}\n");

            // 5. 기본값으로 읽기 (파일이 없을 때)
            var defaultConfig = await JsonHelper.ReadOrDefaultAsync(
                "notexist.json",
                new AppConfig { AppName = "Default App" });
            Console.WriteLine($"✅ 기본값 사용: {defaultConfig.AppName}");

            // 6. 파일이 없으면 생성
            var config = await JsonHelper.ReadOrCreateAsync(
                "auto-created.json",
                new AppConfig { AppName = "Auto Created", Version = "1.0.0" });
            Console.WriteLine($"✅ 자동 생성된 설정: {config.AppName}");

            Console.WriteLine("\n" + new string('=', 50) + "\n");
        }

        public static void RunIniExamples()
        {
            Console.WriteLine("=== IniHelper 사용 예제 ===\n");

            string iniPath = "config.ini";
            var ini = new IniHelper(iniPath);

            // 1. 값 쓰기
            ini.Write("Application", "Name", "WHToolkit Demo");
            ini.Write("Application", "Version", "2.0.0");
            ini.Write("Application", "Debug", true);

            ini.Write("Database", "Server", "localhost");
            ini.Write("Database", "Port", 1433);
            ini.Write("Database", "Timeout", 30.5);
            ini.Write("Database", "UseSSL", true);

            ini.Write("Logging", "Level", "Information");
            ini.Write("Logging", "Path", "logs/app.log");
            Console.WriteLine($"✅ INI 파일 저장 완료: {iniPath}");

            // 2. 값 읽기
            string appName = ini.Read("Application", "Name");
            string version = ini.Read("Application", "Version");
            bool debug = ini.ReadBool("Application", "Debug");
            Console.WriteLine($"✅ Application: {appName} v{version} (Debug: {debug})");

            // 3. 다양한 타입 읽기
            string server = ini.Read("Database", "Server");
            int port = ini.ReadInt("Database", "Port");
            double timeout = ini.ReadDouble("Database", "Timeout");
            bool useSSL = ini.ReadBool("Database", "UseSSL");
            Console.WriteLine($"✅ Database: {server}:{port} (SSL: {useSSL}, Timeout: {timeout}s)");

            // 4. 모든 섹션 가져오기
            var sections = ini.GetSections();
            Console.WriteLine($"\n📋 섹션 목록: {string.Join(", ", sections)}");

            // 5. 섹션의 모든 키 가져오기
            var dbKeys = ini.GetKeys("Database");
            Console.WriteLine($"📋 Database 섹션의 키: {string.Join(", ", dbKeys)}");

            // 6. 섹션의 모든 키-값 쌍 가져오기
            var appSection = ini.GetSection("Application");
            Console.WriteLine("\n📄 Application 섹션 내용:");
            foreach (var kvp in appSection)
            {
                Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
            }

            // 7. 기본값 사용
            string notExist = ini.Read("NotExist", "Key", "Default Value");
            Console.WriteLine($"\n✅ 없는 키 읽기 (기본값): {notExist}");

            // 8. 파일 확인
            Console.WriteLine($"\n📁 INI 파일 존재: {ini.Exists}");
            Console.WriteLine($"📁 INI 파일 경로: {ini.FilePath}");

            Console.WriteLine("\n" + new string('=', 50) + "\n");
        }

        public static async Task RunAllExamples()
        {
            try
            {
                await RunJsonExamples();
                RunIniExamples();

                Console.WriteLine("✅ 모든 예제 실행 완료!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 오류 발생: {ex.Message}");
            }
        }
    }

    // 예제용 모델 클래스들
    public class AppConfig
    {
        public string AppName { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public DatabaseConfig? DatabaseSettings { get; set; }
        public List<string>? Features { get; set; }
    }

    public class DatabaseConfig
    {
        public string Server { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Database { get; set; } = string.Empty;
        public bool UseSSL { get; set; }
    }
}
