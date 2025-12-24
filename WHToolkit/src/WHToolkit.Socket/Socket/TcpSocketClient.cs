using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using WHToolkit.Core;

namespace WHToolkit.Socket;

public sealed class TcpSocketClient : ISocketClient
{
    private readonly ILoggerAdapter _logger;
    private readonly RetryPolicy _retryPolicy;
    private TcpClient? _client;
    private NetworkStream? _stream;

    public TcpSocketClient(SocketOptions options, ILoggerAdapter? logger = null, RetryPolicy? retryPolicy = null)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLoggerAdapter.Instance;
        _retryPolicy = retryPolicy ?? new RetryPolicy();
    }

    public SocketOptions Options { get; }
    public ChannelState State { get; private set; } = ChannelState.Disconnected;

    public event EventHandler? Connected;
    public event EventHandler? Disconnected;
    public event EventHandler<Exception>? Faulted;

    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        if (State == ChannelState.Connected)
            return;

        State = ChannelState.Connecting;

        await _retryPolicy.ExecuteAsync(async ct =>
        {
            await DisconnectAsync(ct).ConfigureAwait(false);

            _client = new TcpClient
            {
                ReceiveBufferSize = Options.ReceiveBufferSize,
                SendBufferSize = Options.SendBufferSize,
                NoDelay = true,
            };

            await _client.ConnectAsync(Options.Host, Options.Port, ct).ConfigureAwait(false);
            _stream = _client.GetStream();
        }, cancellationToken).ConfigureAwait(false);

        State = ChannelState.Connected;
        Connected?.Invoke(this, EventArgs.Empty);
        _logger.Info($"TCP connected {Options.Host}:{Options.Port}");
    }

    public async Task DisconnectAsync(CancellationToken cancellationToken = default)
    {
        if (_stream != null)
        {
            await _stream.FlushAsync(cancellationToken).ConfigureAwait(false);
            await _stream.DisposeAsync().ConfigureAwait(false);
            _stream = null;
        }

        _client?.Dispose();
        _client = null;

        if (State != ChannelState.Disconnected)
        {
            State = ChannelState.Disconnected;
            Disconnected?.Invoke(this, EventArgs.Empty);
        }
    }

    public async Task SendAsync(ReadOnlyMemory<byte> payload, CancellationToken cancellationToken = default)
    {
        EnsureConnected();
        await _stream!.WriteAsync(payload, cancellationToken).ConfigureAwait(false);
    }

    public async ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        EnsureConnected();
        return await _stream!.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
    }

    private void EnsureConnected()
    {
        if (_stream is null || _client is null || !_client.Connected)
            throw new InvalidOperationException("Socket is not connected.");
    }

    public async ValueTask DisposeAsync()
    {
        await DisconnectAsync();
    }
}

