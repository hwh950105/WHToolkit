using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace WHToolkit.Core.Logging;

/// <summary>
/// NLog 기반 공용 로깅 도우미.
/// 로그 파일은 실행 경로 기준 <c>Log\yyyy\MM\dd.txt</c>에 저장된다.
/// </summary>
public static class LogHelper
{
    private static readonly object InitLock = new();
    private static Logger? _logger;
    private static bool _initialized;

    static LogHelper()
    {
        Initialize();
    }

    private static Logger Logger
    {
        get
        {
            EnsureInitialized();
            return _logger!;
        }
    }

    public static void Initialize()
    {
        if (_initialized)
            return;

        lock (InitLock)
        {
            if (_initialized)
                return;

            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget("file")
            {
                FileName = "${basedir}/Log/${date:format=yyyy}/${date:format=MM}/${date:format=dd}.txt",
                Layout = "${longdate} [${level:uppercase=true}] ${message}${onexception:inner=${newline}${exception:format=tostring}}",
                Encoding = Encoding.UTF8,
                CreateDirs = true,
                KeepFileOpen = false
            };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, fileTarget);

#if DEBUG
            var consoleTarget = new ConsoleTarget("console")
            {
                Layout = "${date:format=HH\\:mm\\:ss} [${level:uppercase=true}] ${message}"
            };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);
#endif

            LogManager.Configuration = config;
            _logger = LogManager.GetLogger("WHToolkit");
            _initialized = true;

            _logger.Info("WHToolkit LogHelper initialized.");
        }
    }

    private static void EnsureInitialized()
    {
        if (!_initialized)
            Initialize();
    }

    public static void Shutdown()
    {
        if (!_initialized)
            return;

        Info("WHToolkit logging shutdown.");
        LogManager.Shutdown();
        _initialized = false;
    }

    public static void Info(string message)
    {
        Logger.Info(message);
    }

    public static void Info(string message, params object[] args)
    {
        Logger.Info(message, args);
    }

    public static void Warn(string message)
    {
        Logger.Warn(message);
    }

    public static void Warn(string message, params object[] args)
    {
        Logger.Warn(message, args);
    }

    public static void Debug(string message)
    {
        Logger.Debug(message);
    }

    public static void Error(string message)
    {
        Logger.Error(message);
    }

    public static void Error(Exception exception, string message)
    {
        Logger.Error(exception, message);
    }

    public static void Error(Exception exception, string message, params object[] args)
    {
        Logger.Error(exception, message, args);
    }

    public static void Fatal(Exception exception, string message)
    {
        Logger.Fatal(exception, message);
    }
}

