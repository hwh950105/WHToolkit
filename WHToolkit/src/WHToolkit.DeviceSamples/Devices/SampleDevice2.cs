using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WHToolkit.Core;
using WHToolkit.Port;

namespace WHToolkit.DeviceSamples.Devices;

/// <summary>
/// Sample wrapper that parses numeric measurements (e.g., weight or length).
/// </summary>
public sealed class SampleDevice2
{
    private readonly SerialPortClient _client;
    private readonly ILoggerAdapter _logger;

    public SampleDevice2(string portName, ILoggerAdapter? logger = null)
        : this(new PortOptions(portName), logger)
    {
    }

    public SampleDevice2(PortOptions options, ILoggerAdapter? logger = null, SerialPortClient? client = null)
    {
        var resolvedOptions = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLoggerAdapter.Instance;
        _client = client ?? new SerialPortClient(resolvedOptions, _logger);
    }

    public Task ConnectAsync(CancellationToken cancellationToken = default) =>
        _client.ConnectAsync(cancellationToken);

    public Task DisconnectAsync(CancellationToken cancellationToken = default) =>
        _client.DisconnectAsync(cancellationToken);

    public async Task<double?> ReadMeasurementAsync(CancellationToken cancellationToken = default)
    {
        var buffer = new byte[64];
        var length = await _client.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);

        var payload = Encoding.ASCII.GetString(buffer, 0, length).Trim();
        if (double.TryParse(payload, out var value))
        {
            _logger.Info($"[SampleDevice2] measurement={value}");
            return value;
        }

        _logger.Warn($"[SampleDevice2] invalid payload: {payload}");
        return null;
    }
}
