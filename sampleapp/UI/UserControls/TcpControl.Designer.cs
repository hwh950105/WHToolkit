namespace sampleapp.UI.UserControls
{
    partial class TcpControl
    {
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitle = new ReaLTaiizor.Controls.BigLabel();
            panelContent = new Panel();
            txtLog = new TextBox();
            panelButtons = new Panel();
            btnClearLog = new ReaLTaiizor.Controls.HopeButton();
            btnSendData = new ReaLTaiizor.Controls.HopeButton();
            txtSendData = new TextBox();
            lblSendData = new Label();
            btnDisconnect = new ReaLTaiizor.Controls.HopeButton();
            btnConnect = new ReaLTaiizor.Controls.HopeButton();
            panelConnection = new Panel();
            chkAutoReconnect = new CheckBox();
            lblStatus = new Label();
            txtPort = new TextBox();
            lblPort = new Label();
            txtHost = new TextBox();
            lblHost = new Label();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            panelButtons.SuspendLayout();
            panelConnection.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(20, 20, 20, 10);
            panelHeader.Size = new Size(1140, 80);
            panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(64, 158, 255);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(256, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "TCP 사용법 예시";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.Transparent;
            panelContent.Controls.Add(txtLog);
            panelContent.Controls.Add(panelButtons);
            panelContent.Controls.Add(panelConnection);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 80);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(20);
            panelContent.Size = new Size(1140, 460);
            panelContent.TabIndex = 1;
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.White;
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 9F);
            txtLog.Location = new Point(20, 260);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(1100, 180);
            txtLog.TabIndex = 2;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.White;
            panelButtons.Controls.Add(btnClearLog);
            panelButtons.Controls.Add(btnSendData);
            panelButtons.Controls.Add(txtSendData);
            panelButtons.Controls.Add(lblSendData);
            panelButtons.Controls.Add(btnDisconnect);
            panelButtons.Controls.Add(btnConnect);
            panelButtons.Dock = DockStyle.Top;
            panelButtons.Location = new Point(20, 150);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(20);
            panelButtons.Size = new Size(1100, 110);
            panelButtons.TabIndex = 1;
            // 
            // btnClearLog
            // 
            btnClearLog.BorderColor = Color.FromArgb(220, 223, 230);
            btnClearLog.ButtonType = ReaLTaiizor.Util.HopeButtonType.Warning;
            btnClearLog.Cursor = Cursors.Hand;
            btnClearLog.DangerColor = Color.FromArgb(245, 108, 108);
            btnClearLog.DefaultColor = Color.FromArgb(255, 255, 255);
            btnClearLog.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearLog.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnClearLog.InfoColor = Color.FromArgb(144, 147, 153);
            btnClearLog.Location = new Point(890, 57);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnClearLog.Size = new Size(180, 35);
            btnClearLog.SuccessColor = Color.FromArgb(103, 194, 58);
            btnClearLog.TabIndex = 5;
            btnClearLog.Text = "로그 지우기";
            btnClearLog.TextColor = Color.White;
            btnClearLog.WarningColor = Color.FromArgb(230, 162, 60);
            btnClearLog.Click += BtnClearLog_Click;
            // 
            // btnSendData
            // 
            btnSendData.BorderColor = Color.FromArgb(220, 223, 230);
            btnSendData.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnSendData.Cursor = Cursors.Hand;
            btnSendData.DangerColor = Color.FromArgb(245, 108, 108);
            btnSendData.DefaultColor = Color.FromArgb(255, 255, 255);
            btnSendData.Enabled = false;
            btnSendData.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSendData.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnSendData.InfoColor = Color.FromArgb(144, 147, 153);
            btnSendData.Location = new Point(690, 57);
            btnSendData.Name = "btnSendData";
            btnSendData.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnSendData.Size = new Size(180, 35);
            btnSendData.SuccessColor = Color.FromArgb(103, 194, 58);
            btnSendData.TabIndex = 4;
            btnSendData.Text = "데이터 전송";
            btnSendData.TextColor = Color.White;
            btnSendData.WarningColor = Color.FromArgb(230, 162, 60);
            btnSendData.Click += BtnSendData_Click;
            // 
            // txtSendData
            // 
            txtSendData.Font = new Font("Segoe UI", 10F);
            txtSendData.Location = new Point(120, 62);
            txtSendData.Name = "txtSendData";
            txtSendData.Size = new Size(550, 25);
            txtSendData.TabIndex = 3;
            txtSendData.Text = "Hello Server!";
            // 
            // lblSendData
            // 
            lblSendData.AutoSize = true;
            lblSendData.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSendData.ForeColor = Color.FromArgb(80, 80, 80);
            lblSendData.Location = new Point(23, 65);
            lblSendData.Name = "lblSendData";
            lblSendData.Size = new Size(87, 19);
            lblSendData.TabIndex = 2;
            lblSendData.Text = "전송 데이터:";
            // 
            // btnDisconnect
            // 
            btnDisconnect.BorderColor = Color.FromArgb(220, 223, 230);
            btnDisconnect.ButtonType = ReaLTaiizor.Util.HopeButtonType.Danger;
            btnDisconnect.Cursor = Cursors.Hand;
            btnDisconnect.DangerColor = Color.FromArgb(245, 108, 108);
            btnDisconnect.DefaultColor = Color.FromArgb(255, 255, 255);
            btnDisconnect.Enabled = false;
            btnDisconnect.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDisconnect.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnDisconnect.InfoColor = Color.FromArgb(144, 147, 153);
            btnDisconnect.Location = new Point(260, 20);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnDisconnect.Size = new Size(220, 35);
            btnDisconnect.SuccessColor = Color.FromArgb(103, 194, 58);
            btnDisconnect.TabIndex = 1;
            btnDisconnect.Text = "연결 해제";
            btnDisconnect.TextColor = Color.White;
            btnDisconnect.WarningColor = Color.FromArgb(230, 162, 60);
            btnDisconnect.Click += BtnDisconnect_Click;
            // 
            // btnConnect
            // 
            btnConnect.BorderColor = Color.FromArgb(220, 223, 230);
            btnConnect.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success;
            btnConnect.Cursor = Cursors.Hand;
            btnConnect.DangerColor = Color.FromArgb(245, 108, 108);
            btnConnect.DefaultColor = Color.FromArgb(255, 255, 255);
            btnConnect.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnConnect.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnConnect.InfoColor = Color.FromArgb(144, 147, 153);
            btnConnect.Location = new Point(23, 20);
            btnConnect.Name = "btnConnect";
            btnConnect.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnConnect.Size = new Size(220, 35);
            btnConnect.SuccessColor = Color.FromArgb(103, 194, 58);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "서버 연결";
            btnConnect.TextColor = Color.White;
            btnConnect.WarningColor = Color.FromArgb(230, 162, 60);
            btnConnect.Click += BtnConnect_Click;
            // 
            // panelConnection
            // 
            panelConnection.BackColor = Color.White;
            panelConnection.Controls.Add(chkAutoReconnect);
            panelConnection.Controls.Add(lblStatus);
            panelConnection.Controls.Add(txtPort);
            panelConnection.Controls.Add(lblPort);
            panelConnection.Controls.Add(txtHost);
            panelConnection.Controls.Add(lblHost);
            panelConnection.Dock = DockStyle.Top;
            panelConnection.Location = new Point(20, 20);
            panelConnection.Name = "panelConnection";
            panelConnection.Padding = new Padding(20);
            panelConnection.Size = new Size(1100, 130);
            panelConnection.TabIndex = 0;
            // 
            // chkAutoReconnect
            // 
            chkAutoReconnect.AutoSize = true;
            chkAutoReconnect.Font = new Font("Segoe UI", 10F);
            chkAutoReconnect.Location = new Point(420, 65);
            chkAutoReconnect.Name = "chkAutoReconnect";
            chkAutoReconnect.Size = new Size(102, 23);
            chkAutoReconnect.TabIndex = 5;
            chkAutoReconnect.Text = "자동 재연결";
            chkAutoReconnect.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(245, 108, 108);
            lblStatus.Location = new Point(23, 100);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(81, 19);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "연결 안됨 ●";
            // 
            // txtPort
            // 
            txtPort.Font = new Font("Segoe UI", 10F);
            txtPort.Location = new Point(120, 62);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(250, 25);
            txtPort.TabIndex = 3;
            txtPort.Text = "8080";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPort.ForeColor = Color.FromArgb(80, 80, 80);
            lblPort.Location = new Point(23, 65);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(42, 19);
            lblPort.TabIndex = 2;
            lblPort.Text = "Port:";
            // 
            // txtHost
            // 
            txtHost.Font = new Font("Segoe UI", 10F);
            txtHost.Location = new Point(120, 22);
            txtHost.Name = "txtHost";
            txtHost.Size = new Size(250, 25);
            txtHost.TabIndex = 1;
            txtHost.Text = "localhost";
            // 
            // lblHost
            // 
            lblHost.AutoSize = true;
            lblHost.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHost.ForeColor = Color.FromArgb(80, 80, 80);
            lblHost.Location = new Point(23, 25);
            lblHost.Name = "lblHost";
            lblHost.Size = new Size(44, 19);
            lblHost.TabIndex = 0;
            lblHost.Text = "Host:";
            // 
            // TcpControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Name = "TcpControl";
            Size = new Size(1140, 540);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
            panelButtons.ResumeLayout(false);
            panelButtons.PerformLayout();
            panelConnection.ResumeLayout(false);
            panelConnection.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private ReaLTaiizor.Controls.BigLabel lblTitle;
        private Panel panelContent;
        private Panel panelConnection;
        private Label lblHost;
        private TextBox txtHost;
        private Label lblPort;
        private TextBox txtPort;
        private Label lblStatus;
        private CheckBox chkAutoReconnect;
        private Panel panelButtons;
        private ReaLTaiizor.Controls.HopeButton btnConnect;
        private ReaLTaiizor.Controls.HopeButton btnDisconnect;
        private Label lblSendData;
        private TextBox txtSendData;
        private ReaLTaiizor.Controls.HopeButton btnSendData;
        private ReaLTaiizor.Controls.HopeButton btnClearLog;
        private TextBox txtLog;
    }
}
