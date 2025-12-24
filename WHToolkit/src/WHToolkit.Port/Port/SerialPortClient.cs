using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using WHToolkit.Core;

namespace WHToolkit.Port;

public sealed class SerialPortClient : IPortClient
{
    private readonly ILoggerAdapter _logger;
    private readonly RetryPolicy _retryPolicy;
    private SerialPort? _serialPort;

    public SerialPortClient(PortOptions options, ILoggerAdapter? logger = null, RetryPolicy? retryPolicy = null)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLoggerAdapter.Instance;
        _retryPolicy = retryPolicy ?? new RetryPolicy();
    }

    public PortOptions Options { get; }
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
            await Task.Yield();
            _serialPort?.Dispose();
            _serialPort = new SerialPort(Options.PortName, Options.BaudRate, Options.Parity, Options.DataBits, Options.StopBits)
            {
                Handshake = Options.Handshake,
                ReadTimeout = 1000,
                WriteTimeout = 1000
            };
            _serialPort.Open();
        }, cancellationToken).ConfigureAwait(false);

        State = ChannelState.Connected;
        Connected?.Invoke(this, EventArgs.Empty);
        _logger.Info($"Serial port connected: {Options.PortName}");
    }

    public Task DisconnectAsync(CancellationToken cancellationToken = default)
    {
        if (_serialPort is { IsOpen: true })
        {
            _serialPort.Close();
            _serialPort.Dispose();
            _serialPort = null;
            State = ChannelState.Disconnected;
            Disconnected?.Invoke(this, EventArgs.Empty);
        }

        return Task.CompletedTask;
    }

    public async Task WriteAsync(byte[] data, CancellationToken cancellationToken = default)
    {
        if (data == null || data.Length == 0)
            return;

        if (_serialPort is not { IsOpen: true })
            throw new InvalidOperationException("Serial port is not connected.");

        await Task.Run(() => _serialPort.Write(data, 0, data.Length), cancellationToken).ConfigureAwait(false);
    }

    public async Task<int> ReadAsync(byte[] buffer, CancellationToken cancellationToken = default)
    {
        if (buffer == null)
            throw new ArgumentNullException(nameof(buffer));

        if (_serialPort is not { IsOpen: true })
            throw new InvalidOperationException("Serial port is not connected.");

        return await Task.Run(() => _serialPort.Read(buffer, 0, buffer.Length), cancellationToken).ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        await DisconnectAsync();
    }
}

