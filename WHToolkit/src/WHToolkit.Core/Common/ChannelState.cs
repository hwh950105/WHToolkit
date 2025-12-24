namespace WHToolkit.Core;

/// <summary>
/// Represents the lifecycle state of a communication channel (port or socket).
/// </summary>
public enum ChannelState
{
    Disconnected,
    Connecting,
    Connected,
    Faulted,
}

