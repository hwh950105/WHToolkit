using System.Buffers;
using System.Threading;
using System.Threading.Tasks;
using WHToolkit.Core;

namespace WHToolkit.Socket;

public interface ISocketClient : IChannelLifecycle
{
    SocketOptions Options { get; }
    Task SendAsync(ReadOnlyMemory<byte> payload, CancellationToken cancellationToken = default);
    ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken = default);
}

