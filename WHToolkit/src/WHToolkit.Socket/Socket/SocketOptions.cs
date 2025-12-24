using System.Net;

namespace WHToolkit.Socket;

public sealed record SocketOptions(
    string Host,
    int Port,
    bool UseSsl = false,
    int ReceiveBufferSize = 8 * 1024,
    int SendBufferSize = 8 * 1024,
    bool AutoReconnect = true)
{
    public IPEndPoint EndPoint => new(IPAddress.Parse(Host), Port);
}

