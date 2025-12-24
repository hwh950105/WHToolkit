using System.Threading;
using System.Threading.Tasks;
using WHToolkit.Core;

namespace WHToolkit.Port;

public interface IPortClient : IChannelLifecycle
{
    PortOptions Options { get; }
    Task WriteAsync(byte[] data, CancellationToken cancellationToken = default);
    Task<int> ReadAsync(byte[] buffer, CancellationToken cancellationToken = default);
}

