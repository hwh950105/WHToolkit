using System;

namespace WHToolkit.Core;

/// <summary>
/// Lightweight logger abstraction to avoid forcing a specific logging implementation.
/// </summary>
public interface ILoggerAdapter
{
    void Trace(string message);
    void Info(string message);
    void Warn(string message);
    void Error(string message, Exception? ex = null);
}

public sealed class NullLoggerAdapter : ILoggerAdapter
{
    public static readonly NullLoggerAdapter Instance = new();

    private NullLoggerAdapter() { }

    public void Trace(string message) { }
    public void Info(string message) { }
    public void Warn(string message) { }
    public void Error(string message, Exception? ex = null) { }
}

