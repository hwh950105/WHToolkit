using System.Buffers;
using System.Net.Sockets;
using System.Text;

namespace WHToolkit.Network.TcpClient;

/// <summary>
/// 경량 .NET 8.0 TCP 클라이언트 (async/await 지원, 이벤트 기반 통신)
/// </summary>
public class TcpClientH : IDisposable, IAsyncDisposable
{
    #region Event Args Classes

    /// <summary>
    /// 연결 이벤트 인자
    /// </summary>
    public class TcpConnectedEventArgs : EventArgs
    {
        public int InstanceId { get; init; }
        public string Host { get; init; } = string.Empty;
        public int Port { get; init; }
        public DateTime ConnectedAt { get; init; }
    }

    /// <summary>
    /// 연결 해제 이벤트 인자
    /// </summary>
    public class TcpDisconnectedEventArgs : EventArgs
    {
        public int InstanceId { get; init; }
        public string? Reason { get; init; }
        public DateTime DisconnectedAt { get; init; }
    }

    /// <summary>
    /// 데이터 수신 이벤트 인자
    /// </summary>
    public class TcpDataReceivedEventArgs : EventArgs
    {
        public string Text { get; init; } = string.Empty;
        public byte[] Data { get; init; } = Array.Empty<byte>();
        public string HexString { get; init; } = string.Empty;
        public int BytesReceived { get; init; }
        public DateTime ReceivedAt { get; init; }
    }

    /// <summary>
    /// 오류 이벤트 인자
    /// </summary>
    public class TcpErrorEventArgs : EventArgs
    {
        public string ErrorMessage { get; init; } = string.Empty;
        public Exception? Exception { get; init; }
        public DateTime OccurredAt { get; init; }
    }

    #endregion

    #region Legacy Delegates (for backward compatibility)

    /// <summary>
    /// 연결 이벤트용 레거시 델리게이트 
    /// </summary>
    public delegate void OnConnectDelegate(object sender);

    /// <summary>
    /// 연결 해제 이벤트용 레거시 델리게이트
    /// </summary>
    public delegate void OnDisconnectDelegate(object sender);

    /// <summary>
    /// 데이터 수신 이벤트용 레거시 델리게이트
    /// </summary>
    public delegate void OnDataAvailableDelegate(object sender, string data, byte[] _data, string byteString);

    /// <summary>
    /// 오류 이벤트용 레거시 델리게이트
    /// </summary>
    public delegate void OnErrorDelegate(object sender, string errorMessage);

    #endregion

    #region Fields

    private System.Net.Sockets.TcpClient? _client;
    private CancellationTokenSource? _receiveCts;
    private Task? _receiveTask;
    private bool _disposed;
    private readonly SemaphoreSlim _sendLock = new(1, 1);

    private static int _nextInstanceId;
    private static long _classInstance_count;

    #endregion

    #region Properties

    /// <summary>
    /// 인스턴스 ID
    /// </summary>
    public int InstanceId { get; }

    /// <summary>
    /// 현재 클래스 인스턴스 수
    /// </summary>
    public static long InstanceCount => _classInstance_count;

    /// <summary>
    /// 서버 호스트 주소
    /// </summary>
    public string Host { get; set; } = "192.168.20.1";

    /// <summary>
    /// 서버 포트 번호
    /// </summary>
    public int Port { get; set; } = 8080;

    /// <summary>
    /// 문자열 인코딩 (기본: UTF-8)
    /// </summary>
    public Encoding Encoding { get; set; } = Encoding.UTF8;

    /// <summary>
    /// 수신 버퍼 크기 (기본: 8192)
    /// </summary>
    public int BufferSize { get; set; } = 8192;

    /// <summary>
    /// 연결 타임아웃 (기본: 10초)
    /// </summary>
    public TimeSpan ConnectTimeout { get; set; } = TimeSpan.FromSeconds(10);

    /// <summary>
    /// 자동 재연결 사용 여부 (기본: false)
    /// </summary>
    public bool AutoReconnect { get; set; } = false;

    /// <summary>
    /// 재연결 대기 시간 (기본: 3초)
    /// </summary>
    public TimeSpan ReconnectDelay { get; set; } = TimeSpan.FromSeconds(3);

    /// <summary>
    /// 연결 상태
    /// </summary>
    public bool Connected => _client?.Client?.Connected ?? false;

    /// <summary>
    /// 총 전송 바이트 수
    /// </summary>
    public long TotalBytesSent { get; private set; }

    /// <summary>
    /// 총 수신 바이트 수
    /// </summary>
    public long TotalBytesReceived { get; private set; }

    #endregion

    #region Modern Events

    /// <summary>
    /// 클라이언트가 연결되었을 때 발생
    /// </summary>
    public event EventHandler<TcpConnectedEventArgs>? ClientConnected;

    /// <summary>
    /// 클라이언트가 연결이 끊겼을 때 발생
    /// </summary>
    public event EventHandler<TcpDisconnectedEventArgs>? ClientDisconnected;

    /// <summary>
    /// 데이터가 수신되었을 때 발생
    /// </summary>
    public event EventHandler<TcpDataReceivedEventArgs>? DataReceived;

    /// <summary>
    /// 오류가 발생했을 때 발생
    /// </summary>
    public event EventHandler<TcpErrorEventArgs>? ErrorOccurred;

    #endregion

    #region Legacy Events (for backward compatibility)

    /// <summary>
    /// 연결 이벤트(레거시)
    /// </summary>
    public event OnConnectDelegate? OnConnect;

    /// <summary>
    /// 연결 해제 이벤트(레거시)
    /// </summary>
    public event OnDisconnectDelegate? OnDisconnect;

    /// <summary>
    /// 데이터 수신 이벤트(레거시)
    /// </summary>
    public event OnDataAvailableDelegate? OnDataAvailable;

    /// <summary>
    /// 오류 이벤트(레거시)
    /// </summary>
    public event OnErrorDelegate? OnError;

    #endregion

    #region Constructor & Destructor

    /// <summary>
    /// TcpClientH 생성자: 인스턴스 초기화
    /// </summary>
    public TcpClientH()
    {
        InstanceId = Interlocked.Increment(ref _nextInstanceId);
        Interlocked.Increment(ref _classInstance_count);
    }

    /// <summary>
    /// 소멸자
    /// </summary>
    ~TcpClientH()
    {
        Dispose(false);
        Interlocked.Decrement(ref _classInstance_count);
    }

    #endregion

    #region Connection Methods

    /// <summary>
    /// 비동기 연결 시도
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (Connected)
        {
            RaiseError("Already connected", null);
            return;
        }

        try
        {
            _client = new System.Net.Sockets.TcpClient();

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(ConnectTimeout);

            await _client.ConnectAsync(Host, Port, cts.Token).ConfigureAwait(false);

            // 수신 루프 시작
            _receiveCts = new CancellationTokenSource();
            _receiveTask = ReceiveLoopAsync(_receiveCts.Token);

            RaiseConnected();
        }
        catch (Exception ex)
        {
            RaiseError($"Connection failed: {ex.Message}", ex);
            _client?.Dispose();
            _client = null;
            throw;
        }
    }

    /// <summary>
    /// 동기 연결 (레거시)
    /// </summary>
    public void Connect()
    {
        try
        {
            ConnectAsync().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            RaiseError($"Connection failed: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// 비동기 연결 해제
    /// </summary>
    public async Task DisconnectAsync()
    {
        if (_client == null || !Connected)
        {
            return;
        }

        try
        {
            // 수신 루프 취소
            _receiveCts?.Cancel();

            // 수신 태스크 완료 대기
            if (_receiveTask != null)
            {
                try
                {
                    await _receiveTask.ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // 예상된 취소
                }
            }

            // 클라이언트 종료
            _client?.Close();

            RaiseDisconnected("Manual disconnection");
        }
        catch (Exception ex)
        {
            RaiseError($"Disconnection error: {ex.Message}", ex);
        }
        finally
        {
            _receiveCts?.Dispose();
            _receiveCts = null;
            _receiveTask = null;
        }
    }

    /// <summary>
    /// 동기 연결 해제 (레거시)
    /// </summary>
    public void Disconnect()
    {
        try
        {
            DisconnectAsync().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            RaiseError($"Disconnection error: {ex.Message}", ex);
        }
    }

    #endregion

    #region Receive Methods

    private async Task ReceiveLoopAsync(CancellationToken cancellationToken)
    {
        var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);

        try
        {
            var stream = _client?.GetStream();
            if (stream == null)
            {
                return;
            }

            while (!cancellationToken.IsCancellationRequested && Connected)
            {
                try
                {
                    var bytesRead = await stream.ReadAsync(buffer.AsMemory(0, BufferSize), cancellationToken).ConfigureAwait(false);

                    if (bytesRead == 0)
                    {
                        // 원격 호스트가 연결을 닫음
                        RaiseDisconnected("Remote host closed connection");
                        break;
                    }

                    TotalBytesReceived += bytesRead;

                    // 수신 데이터 복사
                    var data = new byte[bytesRead];
                    Array.Copy(buffer, 0, data, 0, bytesRead);

                    // 텍스트로 변환
                    var text = Encoding.GetString(data);

                    // 16진수 문자열로 변환
                    var hexString = BitConverter.ToString(data).Replace("-", " ");

                    RaiseDataReceived(text, data, hexString, bytesRead);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (IOException ioEx) when (ioEx.InnerException is SocketException)
                {
                    RaiseDisconnected($"Socket error: {ioEx.Message}");
                    break;
                }
                catch (Exception ex)
                {
                    RaiseError($"Receive error: {ex.Message}", ex);
                    RaiseDisconnected($"Error during receive: {ex.Message}");
                    break;
                }
            }
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);

            // 자동 재연결 설정되어 있으면 재연결 시도
            if (AutoReconnect && !cancellationToken.IsCancellationRequested && !_disposed)
            {
                _ = Task.Run(async () =>
                {
                    await Task.Delay(ReconnectDelay, CancellationToken.None).ConfigureAwait(false);
                    try
                    {
                        await ConnectAsync(CancellationToken.None).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        RaiseError($"Auto-reconnect failed: {ex.Message}", ex);
                    }
                }, CancellationToken.None);
            }
        }
    }

    #endregion

    #region Send Methods

    /// <summary>
    /// 문자열을 비동기 전송
    /// </summary>
    /// <param name="data">전송할 문자열</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public async Task SendAsync(string data, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(data);
        ObjectDisposedException.ThrowIf(_disposed, this);

        var bytes = Encoding.GetBytes(data);
        await SendBytesAsync(bytes, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 문자열을 동기 전송 (레거시)
    /// </summary>
    /// <param name="data">전송할 문자열</param>
    public void Send(string data)
    {
        try
        {
            SendAsync(data).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            RaiseError($"Send error: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// 바이트 배열을 비동기 전송
    /// </summary>
    /// <param name="data">전송할 바이트 배열</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public async Task SendBytesAsync(byte[] data, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(data);
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (!Connected || _client?.GetStream() == null)
        {
            RaiseError("Not connected", null);
            return;
        }

        await _sendLock.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var stream = _client.GetStream();
            await stream.WriteAsync(data, cancellationToken).ConfigureAwait(false);
            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);

            TotalBytesSent += data.Length;
        }
        catch (Exception ex)
        {
            RaiseError($"Send error: {ex.Message}", ex);
            throw;
        }
        finally
        {
            _sendLock.Release();
        }
    }

    /// <summary>
    /// 바이트 배열을 동기 전송 (레거시)
    /// </summary>
    /// <param name="data">전송할 바이트 배열</param>
    public void SendBytes(byte[] data)
    {
        try
        {
            SendBytesAsync(data).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            RaiseError($"Send error: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// 16진수 문자열을 비동기 전송 (예: \"48 65 6C 6C 6F\")
    /// </summary>
    /// <param name="hexString">16진수 문자열</param>
    /// <param name="cancellationToken">취소 토큰</param>
    public async Task SendHexStringAsync(string hexString, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(hexString);

        var data = hexString.Replace(" ", string.Empty).Replace("-", string.Empty);

        if (data.Length % 2 != 0)
        {
            throw new ArgumentException("Hex string length must be even", nameof(hexString));
        }

        var bytes = new byte[data.Length / 2];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(data.Substring(i * 2, 2), 16);
        }

        await SendBytesAsync(bytes, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 16진수 문자열을 동기 전송 (레거시)
    /// </summary>
    /// <param name="hexString">16진수 문자열</param>
    public void SendByteString(string hexString)
    {
        try
        {
            SendHexStringAsync(hexString).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            RaiseError($"Send error: {ex.Message}", ex);
        }
    }

    #endregion

    #region Event Raising Methods

    private void RaiseConnected()
    {
        var args = new TcpConnectedEventArgs
        {
            InstanceId = InstanceId,
            Host = Host,
            Port = Port,
            ConnectedAt = DateTime.Now
        };

        ClientConnected?.Invoke(this, args);
        OnConnect?.Invoke(this);
    }

    private void RaiseDisconnected(string? reason)
    {
        var args = new TcpDisconnectedEventArgs
        {
            InstanceId = InstanceId,
            Reason = reason,
            DisconnectedAt = DateTime.Now
        };

        ClientDisconnected?.Invoke(this, args);
        OnDisconnect?.Invoke(this);
    }

    private void RaiseDataReceived(string text, byte[] data, string hexString, int bytesReceived)
    {
        var args = new TcpDataReceivedEventArgs
        {
            Text = text,
            Data = data,
            HexString = hexString,
            BytesReceived = bytesReceived,
            ReceivedAt = DateTime.Now
        };

        DataReceived?.Invoke(this, args);
        OnDataAvailable?.Invoke(this, text, data, hexString);
    }

    private void RaiseError(string errorMessage, Exception? exception)
    {
        var args = new TcpErrorEventArgs
        {
            ErrorMessage = errorMessage,
            Exception = exception,
            OccurredAt = DateTime.Now
        };

        ErrorOccurred?.Invoke(this, args);
        OnError?.Invoke(this, errorMessage);
    }

    #endregion

    #region Dispose Pattern

    /// <summary>
    /// IDisposable 구현
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// IAsyncDisposable 구현
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        Dispose(false);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose 패턴 구현
    /// </summary>
    /// <param name="disposing">관리 자원 해제 여부</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            try
            {
                DisconnectAsync().GetAwaiter().GetResult();
            }
            catch
            {
                // 무시
            }

            _client?.Dispose();
            _receiveCts?.Dispose();
            _sendLock.Dispose();
        }

        _disposed = true;
    }

    /// <summary>
    /// 비동기 Dispose 핵심 처리
    /// </summary>
    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_disposed)
        {
            return;
        }

        try
        {
            await DisconnectAsync().ConfigureAwait(false);
        }
        catch
        {
            // 무시
        }

        _client?.Dispose();
        _receiveCts?.Dispose();
        _sendLock.Dispose();

        _disposed = true;
    }

    #endregion
}
