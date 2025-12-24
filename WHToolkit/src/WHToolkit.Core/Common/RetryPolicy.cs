using System;
using System.Threading;
using System.Threading.Tasks;

namespace WHToolkit.Core;

/// <summary>
/// Simple async retry helper with backoff.
/// </summary>
public sealed class RetryPolicy
{
    public int MaxAttemptCount { get; }
    public TimeSpan InitialDelay { get; }
    public double BackoffFactor { get; }

    public RetryPolicy(int maxAttemptCount = 5, TimeSpan? initialDelay = null, double backoffFactor = 2.0)
    {
        if (maxAttemptCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxAttemptCount));
        if (backoffFactor < 1)
            throw new ArgumentOutOfRangeException(nameof(backoffFactor));

        MaxAttemptCount = maxAttemptCount;
        InitialDelay = initialDelay ?? TimeSpan.FromMilliseconds(500);
        BackoffFactor = backoffFactor;
    }

    public async Task ExecuteAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default)
    {
        var delay = InitialDelay;

        for (var attempt = 1; attempt <= MaxAttemptCount; attempt++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                await action(cancellationToken).ConfigureAwait(false);
                return;
            }
            catch when (attempt < MaxAttemptCount)
            {
                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                delay = TimeSpan.FromMilliseconds(delay.TotalMilliseconds * BackoffFactor);
            }
        }
    }
}

