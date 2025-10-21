using System;
using System.Text;
using System.Windows.Forms;
using WHToolkit.Network.TcpClient;

namespace sampleapp.UI.UserControls
{
    /// <summary>
    /// WHToolkit TCP Client 기능 샘플 컨트롤
    /// </summary>
    public partial class TcpControl : UserControl
    {
        private TcpClientH? _tcpClient;

        public TcpControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 서버 연결 버튼 클릭
        /// </summary>
        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string host = txtHost.Text;
                if (!int.TryParse(txtPort.Text, out int port))
                {
                    MessageBox.Show("올바른 포트 번호를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 기존 클라이언트 정리
                if (_tcpClient != null)
                {
                    await _tcpClient.DisconnectAsync();
                    _tcpClient.Dispose();
                }

                // 새 클라이언트 생성
                _tcpClient = new TcpClientH
                {
                    Host = host,
                    Port = port,
                    AutoReconnect = chkAutoReconnect.Checked
                };

                // 이벤트 구독
                _tcpClient.ClientConnected += OnConnected;
                _tcpClient.ClientDisconnected += OnDisconnected;
                _tcpClient.DataReceived += OnDataReceived;
                _tcpClient.ErrorOccurred += OnError;

                // 연결 시도
                await _tcpClient.ConnectAsync();

                AddLog($"연결 시도 중: {host}:{port}");
            }
            catch (Exception ex)
            {
                AddLog($"연결 오류: {ex.Message}");
                MessageBox.Show($"연결 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 연결 해제 버튼 클릭
        /// </summary>
        private async void BtnDisconnect_Click(object sender, EventArgs e)
        {
            if (_tcpClient != null)
            {
                await _tcpClient.DisconnectAsync();
                AddLog("연결 해제 요청");
            }
        }

        /// <summary>
        /// 데이터 전송 버튼 클릭
        /// </summary>
        private async void BtnSendData_Click(object sender, EventArgs e)
        {
            if (_tcpClient == null || !_tcpClient.Connected)
            {
                MessageBox.Show("먼저 서버에 연결해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string data = txtSendData.Text;
                await _tcpClient.SendAsync(data);
                AddLog($"전송: {data}");
            }
            catch (Exception ex)
            {
                AddLog($"전송 오류: {ex.Message}");
                MessageBox.Show($"전송 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 로그 지우기 버튼 클릭
        /// </summary>
        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        /// <summary>
        /// 연결됨 이벤트
        /// </summary>
        private void OnConnected(object? sender, TcpClientH.TcpConnectedEventArgs e)
        {
            this.Invoke(() =>
            {
                AddLog($"연결 성공: {e.Host}:{e.Port}");
                lblStatus.Text = "연결됨 ●";
                lblStatus.ForeColor = System.Drawing.Color.Green;

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnSendData.Enabled = true;
            });
        }

        /// <summary>
        /// 연결 해제됨 이벤트
        /// </summary>
        private void OnDisconnected(object? sender, TcpClientH.TcpDisconnectedEventArgs e)
        {
            this.Invoke(() =>
            {
                AddLog($"연결 해제: {e.Reason}");
                lblStatus.Text = "연결 안됨 ●";
                lblStatus.ForeColor = System.Drawing.Color.FromArgb(245, 108, 108);

                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
                btnSendData.Enabled = false;
            });
        }

        /// <summary>
        /// 데이터 수신 이벤트
        /// </summary>
        private void OnDataReceived(object? sender, TcpClientH.TcpDataReceivedEventArgs e)
        {
            this.Invoke(() =>
            {
                AddLog($"수신: {e.Text}");
            });
        }

        /// <summary>
        /// 오류 이벤트
        /// </summary>
        private void OnError(object? sender, TcpClientH.TcpErrorEventArgs e)
        {
            this.Invoke(() =>
            {
                AddLog($"오류: {e.ErrorMessage}");
            });
        }

        /// <summary>
        /// 로그 추가
        /// </summary>
        private void AddLog(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
        }

        private void ProductionControl_Load(object sender, EventArgs e)
        {
            AddLog("TCP Client 샘플 컨트롤 로드됨");
            AddLog("Host와 Port를 입력하고 '서버 연결' 버튼을 클릭하세요.");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_tcpClient != null)
                {
                    _tcpClient.DisconnectAsync().Wait();
                    _tcpClient.Dispose();
                }
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
