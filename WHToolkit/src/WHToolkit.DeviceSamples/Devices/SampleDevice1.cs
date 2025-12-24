using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WHToolkit.Core;
using WHToolkit.Port;

namespace WHToolkit.DeviceSamples.Devices;

/// <summary>
/// Sample device that exchanges ASCII commands over a serial port.
/// </summary>
public sealed class SampleDevice1
{
    private readonly SerialPortClient _client;
    private readonly ILoggerAdapter _logger;

    public SampleDevice1(string portName, ILoggerAdapter? logger = null)
        : this(new PortOptions(portName), logger)
    {
    }

    public SampleDevice1(PortOptions options, ILoggerAdapter? logger = null, SerialPortClient? client = null)
    {
        var resolvedOptions = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLoggerAdapter.Instance;
        _client = client ?? new SerialPortClient(resolvedOptions, _logger);
    }

    public Task ConnectAsync(CancellationToken cancellationToken = default) =>
        _client.ConnectAsync(cancellationToken);

    public Task DisconnectAsync(CancellationToken cancellationToken = default) =>
        _client.DisconnectAsync(cancellationToken);

    public async Task SendCommandAsync(string command, CancellationToken cancellationToken = default)
    {
        var payload = Encoding.ASCII.GetBytes(command + "\r\n");
        await _client.WriteAsync(payload, cancellationToken).ConfigureAwait(false);
        _logger.Trace($"[SampleDevice1] TX: {command}");
    }

    public async Task<string> ReadResponseAsync(CancellationToken cancellationToken = default)
    {
        var buffer = new byte[256];
        var length = await _client.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
        var text = Encoding.ASCII.GetString(buffer, 0, length);
        _logger.Trace($"[SampleDevice1] RX: {text.Trim()}");
        return text;
    }
}
