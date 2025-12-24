using System.IO.Ports;

namespace WHToolkit.Port;

/// <summary>
/// Describes how to connect to a physical serial/PLC port.
/// </summary>
public sealed record PortOptions(
    string PortName,
    int BaudRate = 9600,
    Parity Parity = Parity.None,
    int DataBits = 8,
    StopBits StopBits = StopBits.One,
    Handshake Handshake = Handshake.None);

