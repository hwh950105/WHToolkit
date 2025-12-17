using NLog;
using NLog.Config;
using NLog.Targets;

namespace hwh.Core
{
    /// <summary>
    /// NLog 기반 로깅 헬퍼 클래스
    /// 로그 파일 경로: 실행위치\Log\yyyy\MM\dd.txt
    /// </summary>
    public static class LogHelper
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static bool _isInitialized = false;

        /// <summary>
        /// NLog 초기화 (애플리케이션 시작 시 한 번 호출)
        /// </summary>
        public static void Initialize()
        {
            if (_isInitialized) return;

            var config = new LoggingConfiguration();

            // 파일 타겟 설정: Log\yyyy\MM\dd.txt
            var fileTarget = new FileTarget("file")
            {
                FileName = "${basedir}/Log/${date:format=yyyy}/${date:format=MM}/${date:format=dd}.txt",
                Layout = "${longdate} [${level:uppercase=true}] ${message}${onexception:inner=${newline}${exception:format=tostring}}",
                Encoding = System.Text.Encoding.UTF8,
                CreateDirs = true,
                KeepFileOpen = false
            };

            // 콘솔 타겟 (디버깅용)
#if DEBUG
            var consoleTarget = new ConsoleTarget("console")
            {
                Layout = "${date:format=HH\\:mm\\:ss} [${level:uppercase=true}] ${message}"
            };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);
#endif

            // 파일에는 Info 이상 기록
            config.AddRule(LogLevel.Info, LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;
            _isInitialized = true;

            Info("로깅 시스템 초기화 완료");
        }

        /// <summary>
        /// 정보 로그
        /// </summary>
        public static void Info(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// 정보 로그 (포맷 지원)
        /// </summary>
        public static void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        /// <summary>
        /// 경고 로그
        /// </summary>
        public static void Warn(string message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// 경고 로그 (포맷 지원)
        /// </summary>
        public static void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        /// <summary>
        /// 에러 로그
        /// </summary>
        public static void Error(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// 에러 로그 (예외 포함)
        /// </summary>
        public static void Error(Exception ex, string message)
        {
            _logger.Error(ex, message);
        }

        /// <summary>
        /// 에러 로그 (예외 + 포맷 지원)
        /// </summary>
        public static void Error(Exception ex, string message, params object[] args)
        {
            _logger.Error(ex, message, args);
        }

        /// <summary>
        /// 디버그 로그
        /// </summary>
        public static void Debug(string message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// 치명적 에러 로그
        /// </summary>
        public static void Fatal(Exception ex, string message)
        {
            _logger.Fatal(ex, message);
        }

        /// <summary>
        /// NLog 종료 (애플리케이션 종료 시 호출)
        /// </summary>
        public static void Shutdown()
        {
            Info("애플리케이션 종료");
            LogManager.Shutdown();
        }
    }
}

