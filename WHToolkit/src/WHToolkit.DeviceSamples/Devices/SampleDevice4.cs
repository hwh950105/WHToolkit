using System;
using System.Threading;
using System.Threading.Tasks;
using WHToolkit.Core;
using WHToolkit.Port;

namespace WHToolkit.DeviceSamples.Devices;

/// <summary>
/// Sample device implementing a simple request-response protocol.
/// </summary>
public sealed class SampleDevice4
{
    private readonly SerialPortClient _client;
    private readonly ILoggerAdapter _logger;

    public SampleDevice4(string portName, ILoggerAdapter? logger = null)
        : this(new PortOptions(portName), logger)
    {
    }

    public SampleDevice4(PortOptions options, ILoggerAdapter? logger = null, SerialPortClient? client = null)
    {
        var resolvedOptions = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLoggerAdapter.Instance;
        _client = client ?? new SerialPortClient(resolvedOptions, _logger);
    }

    public Task ConnectAsync(CancellationToken cancellationToken = default) =>
        _client.ConnectAsync(cancellationToken);

    public Task DisconnectAsync(CancellationToken cancellationToken = default) =>
        _client.DisconnectAsync(cancellationToken);

    public async Task<string> QueryAsync(string request, CancellationToken cancellationToken = default)
    {
        await _client.WriteAsync(System.Text.Encoding.ASCII.GetBytes(request + "\r\n"), cancellationToken)
            .ConfigureAwait(false);

        var buffer = new byte[128];
        var length = await _client.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
        return System.Text.Encoding.ASCII.GetString(buffer, 0, length).Trim();
    }
}
