using System;
using System.Threading;
using System.Threading.Tasks;
using WHToolkit.Core;
using WHToolkit.Port;

namespace WHToolkit.DeviceSamples.Devices;

/// <summary>
/// Sample device demonstrating continuous streaming scenario.
/// </summary>
public sealed class SampleDevice5 : IAsyncDisposable
{
    private readonly SerialPortClient _client;
    private readonly ILoggerAdapter _logger;

    public SampleDevice5(string portName, ILoggerAdapter? logger = null)
        : this(new PortOptions(portName), logger)
    {
    }

    public SampleDevice5(PortOptions options, ILoggerAdapter? logger = null, SerialPortClient? client = null)
    {
        var resolvedOptions = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLoggerAdapter.Instance;
        _client = client ?? new SerialPortClient(resolvedOptions, _logger);
    }

    public Task ConnectAsync(CancellationToken cancellationToken = default) =>
        _client.ConnectAsync(cancellationToken);

    public Task DisconnectAsync(CancellationToken cancellationToken = default) =>
        _client.DisconnectAsync(cancellationToken);

    public async Task SubscribeAsync(Func<string, Task> onMessage, CancellationToken cancellationToken = default)
    {
        var buffer = new byte[512];
        while (!cancellationToken.IsCancellationRequested)
        {
            var length = await _client.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
            var payload = System.Text.Encoding.ASCII.GetString(buffer, 0, length);
            await onMessage(payload).ConfigureAwait(false);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _client.DisposeAsync();
    }
}
