using System;
using System.Threading;
using System.Threading.Tasks;
using WHToolkit.Core;
using WHToolkit.Port;

namespace WHToolkit.DeviceSamples.Devices;

/// <summary>
/// Sample device demonstrating periodic polling.
/// </summary>
public sealed class SampleDevice3
{
    private readonly SerialPortClient _client;
    private readonly ILoggerAdapter _logger;

    public SampleDevice3(string portName, ILoggerAdapter? logger = null)
        : this(new PortOptions(portName), logger)
    {
    }

    public SampleDevice3(PortOptions options, ILoggerAdapter? logger = null, SerialPortClient? client = null)
    {
        var resolvedOptions = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLoggerAdapter.Instance;
        _client = client ?? new SerialPortClient(resolvedOptions, _logger);
    }

    public Task ConnectAsync(CancellationToken cancellationToken = default) =>
        _client.ConnectAsync(cancellationToken);

    public Task DisconnectAsync(CancellationToken cancellationToken = default) =>
        _client.DisconnectAsync(cancellationToken);

    public async Task<double?> PollAsync(CancellationToken cancellationToken = default)
    {
        await _client.WriteAsync("READ\r\n"u8.ToArray(), cancellationToken).ConfigureAwait(false);

        var buffer = new byte[32];
        var length = await _client.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
        var payload = System.Text.Encoding.ASCII.GetString(buffer, 0, length).Trim();

        return double.TryParse(payload, out var value) ? value : null;
    }
}
