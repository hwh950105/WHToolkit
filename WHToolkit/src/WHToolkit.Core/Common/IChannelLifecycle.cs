using System;
using System.Threading;
using System.Threading.Tasks;

namespace WHToolkit.Core;

/// <summary>
/// Basic abstraction shared by port and socket clients.
/// </summary>
public interface IChannelLifecycle : IAsyncDisposable
{
    ChannelState State { get; }
    event EventHandler? Connected;
    event EventHandler? Disconnected;
    event EventHandler<Exception>? Faulted;

    Task ConnectAsync(CancellationToken cancellationToken = default);
    Task DisconnectAsync(CancellationToken cancellationToken = default);
}

