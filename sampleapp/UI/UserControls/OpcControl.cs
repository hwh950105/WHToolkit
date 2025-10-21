using System;
using System.Data;
using System.Windows.Forms;
using Opc.Ua;
using Opc.Ua.Client;
using WHToolkit.Network.OpcUa_Client;

namespace sampleapp.UI.UserControls
{
    /// <summary>
    /// WHToolkit OPC UA Client 기능 샘플 컨트롤
    /// </summary>
    public partial class OpcControl : UserControl
    {
        private OpcUaClient? _opcClient;
        private DataTable _subscriptionData;

        public OpcControl()
        {
            InitializeComponent();
            InitializeSubscriptionGrid();
        }

        /// <summary>
        /// 구독 데이터 그리드 초기화
        /// </summary>
        private void InitializeSubscriptionGrid()
        {
            _subscriptionData = new DataTable();
            _subscriptionData.Columns.Add("NodeId", typeof(string));
            _subscriptionData.Columns.Add("Value", typeof(string));
            _subscriptionData.Columns.Add("StatusCode", typeof(string));
            _subscriptionData.Columns.Add("Timestamp", typeof(string));
            gridTags.DataSource = _subscriptionData;
        }

        /// <summary>
        /// 연결 버튼 클릭
        /// </summary>
        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string serverUrl = txtServerUrl.Text.Trim();
                if (string.IsNullOrWhiteSpace(serverUrl))
                {
                    MessageBox.Show("OPC UA 서버 URL을 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 이전 연결 정리
                _opcClient?.Dispose();

                // OPC UA 클라이언트 생성
                _opcClient = new OpcUaClient(serverUrl, "WHToolkit Sample App");

                // 이벤트 핸들러 등록
                _opcClient.Reconnected += OnOpcReconnected;
                _opcClient.ReconnectFailed += OnOpcReconnectFailed;

                // 연결 초기화
                bool connected = await _opcClient.InitializeAsync();

                if (connected && _opcClient.IsConnected)
                {
                    lblStatus.Text = "연결됨 ●";
                    lblStatus.ForeColor = System.Drawing.Color.Green;

                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                    btnRead.Enabled = true;
                    btnWrite.Enabled = true;
                    btnSubscribe.Enabled = true;

                    MessageBox.Show("OPC UA 서버에 성공적으로 연결되었습니다!", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("OPC UA 서버 연결에 실패했습니다.\n서버가 실행 중인지 확인해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "연결 안됨 ●";
                lblStatus.ForeColor = System.Drawing.Color.FromArgb(245, 108, 108);
                MessageBox.Show($"연결 실패: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 연결 해제 버튼 클릭
        /// </summary>
        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _opcClient?.Disconnect();
                _opcClient?.Dispose();
                _opcClient = null;

                lblStatus.Text = "연결 안됨 ●";
                lblStatus.ForeColor = System.Drawing.Color.FromArgb(245, 108, 108);

                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
                btnRead.Enabled = false;
                btnWrite.Enabled = false;
                btnSubscribe.Enabled = false;
                btnUnsubscribe.Enabled = false;

                _subscriptionData.Clear();

                MessageBox.Show("연결이 해제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"연결 해제 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 읽기 버튼 클릭
        /// </summary>
        private void BtnRead_Click(object sender, EventArgs e)
        {
            if (_opcClient == null || !_opcClient.IsConnected)
            {
                MessageBox.Show("먼저 OPC UA 서버에 연결해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string nodeId = txtNodeId.Text.Trim();
                if (string.IsNullOrWhiteSpace(nodeId))
                {
                    MessageBox.Show("Node ID를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 태그 값 읽기
                var data = _opcClient.ReadTagOpcData(nodeId);

                MessageBox.Show($"Node ID: {data.NodeId}\n" +
                    $"값: {data.Value}\n" +
                    $"상태: {data.strbStatusCode}\n" +
                    $"타임스탬프: {data.strSourceTimestamp}",
                    "읽기 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"읽기 실패: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 쓰기 버튼 클릭
        /// </summary>
        private void BtnWrite_Click(object sender, EventArgs e)
        {
            if (_opcClient == null || !_opcClient.IsConnected)
            {
                MessageBox.Show("먼저 OPC UA 서버에 연결해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string nodeId = txtNodeId.Text.Trim();
                if (string.IsNullOrWhiteSpace(nodeId))
                {
                    MessageBox.Show("Node ID를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string valueStr = txtWriteValue.Text.Trim();
                if (string.IsNullOrWhiteSpace(valueStr))
                {
                    MessageBox.Show("쓸 값을 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 값 파싱 시도
                object value = valueStr;
                if (int.TryParse(valueStr, out int intValue))
                {
                    value = intValue;
                }
                else if (double.TryParse(valueStr, out double doubleValue))
                {
                    value = doubleValue;
                }
                else if (bool.TryParse(valueStr, out bool boolValue))
                {
                    value = boolValue;
                }

                // 태그에 값 쓰기
                _opcClient.WriteTag(nodeId, value);

                MessageBox.Show($"노드 {nodeId}에 값 {value}를 성공적으로 작성했습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"쓰기 실패: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 구독 버튼 클릭
        /// </summary>
        private void BtnSubscribe_Click(object sender, EventArgs e)
        {
            if (_opcClient == null || !_opcClient.IsConnected)
            {
                MessageBox.Show("먼저 OPC UA 서버에 연결해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string nodeId = txtNodeId.Text.Trim();
                if (string.IsNullOrWhiteSpace(nodeId))
                {
                    MessageBox.Show("Node ID를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 이미 구독 중인지 확인
                if (_opcClient.SubscribedTags.Contains(nodeId))
                {
                    MessageBox.Show("이미 구독 중인 태그입니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 구독 생성
                var subscription = _opcClient.SubscribeToTag(nodeId, 1000, OnDataChanged);

                if (subscription != null)
                {
                    btnUnsubscribe.Enabled = true;
                    MessageBox.Show($"노드 {nodeId}에 대한 구독이 생성되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 초기 값 읽어서 그리드에 추가
                    try
                    {
                        var data = _opcClient.ReadTagOpcData(nodeId);
                        AddOrUpdateSubscriptionData(data);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"구독 실패: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 구독 해제 버튼 클릭
        /// </summary>
        private void BtnUnsubscribe_Click(object sender, EventArgs e)
        {
            if (_opcClient == null || !_opcClient.IsConnected)
            {
                MessageBox.Show("먼저 OPC UA 서버에 연결해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                _opcClient.UnsubscribeAll();
                _subscriptionData.Clear();
                btnUnsubscribe.Enabled = false;
                MessageBox.Show("모든 구독이 해제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"구독 해제 실패: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 데이터 변경 이벤트 핸들러
        /// </summary>
        private void OnDataChanged(MonitoredItem item, MonitoredItemNotificationEventArgs e)
        {
            if (e.NotificationValue is MonitoredItemNotification notification && notification.Value != null)
            {
                var nodeId = item.StartNodeId.ToString();
                var value = notification.Value.Value?.ToString() ?? "";
                var statusCode = notification.Value.StatusCode.ToString();
                var timestamp = notification.Value.SourceTimestamp.ToString("yyyy-MM-dd HH:mm:ss.fff");

                // UI 스레드에서 실행
                this.Invoke(() =>
                {
                    var data = new OPCDataValue
                    {
                        NodeId = nodeId,
                        Value = value,
                        StatusCode = notification.Value.StatusCode,
                        SourceTimestamp = notification.Value.SourceTimestamp
                    };
                    AddOrUpdateSubscriptionData(data);
                });
            }
        }

        /// <summary>
        /// 구독 데이터 추가 또는 업데이트
        /// </summary>
        private void AddOrUpdateSubscriptionData(OPCDataValue data)
        {
            var rows = _subscriptionData.Select($"NodeId = '{data.NodeId.Replace("'", "''")}'");
            if (rows.Length > 0)
            {
                // 기존 행 업데이트
                rows[0]["Value"] = data.Value?.ToString() ?? "";
                rows[0]["StatusCode"] = data.strbStatusCode;
                rows[0]["Timestamp"] = data.strSourceTimestamp;
            }
            else
            {
                // 새 행 추가
                _subscriptionData.Rows.Add(
                    data.NodeId,
                    data.Value?.ToString() ?? "",
                    data.strbStatusCode,
                    data.strSourceTimestamp
                );
            }
        }

        /// <summary>
        /// 재연결 성공 이벤트
        /// </summary>
        private void OnOpcReconnected(object? sender, EventArgs e)
        {
            this.Invoke(() =>
            {
                lblStatus.Text = "연결됨 ●";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                MessageBox.Show("OPC UA 서버에 재연결되었습니다!", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        /// <summary>
        /// 재연결 실패 이벤트
        /// </summary>
        private void OnOpcReconnectFailed(object? sender, EventArgs e)
        {
            this.Invoke(() =>
            {
                MessageBox.Show("OPC UA 서버 재연결에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _opcClient?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
