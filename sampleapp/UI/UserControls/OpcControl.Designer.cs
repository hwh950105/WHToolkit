namespace sampleapp.UI.UserControls
{
    partial class OpcControl
    {
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitle = new ReaLTaiizor.Controls.BigLabel();
            panelContent = new Panel();
            panelReadWrite = new Panel();
            gridTags = new DataGridView();
            panelRWButtons = new Panel();
            btnUnsubscribe = new ReaLTaiizor.Controls.HopeButton();
            btnSubscribe = new ReaLTaiizor.Controls.HopeButton();
            btnWrite = new ReaLTaiizor.Controls.HopeButton();
            btnRead = new ReaLTaiizor.Controls.HopeButton();
            txtWriteValue = new TextBox();
            lblWriteValue = new Label();
            txtNodeId = new TextBox();
            lblNodeId = new Label();
            panelButtons = new Panel();
            btnDisconnect = new ReaLTaiizor.Controls.HopeButton();
            btnConnect = new ReaLTaiizor.Controls.HopeButton();
            lblStatus = new Label();
            panelConnection = new Panel();
            txtServerUrl = new TextBox();
            lblServerUrl = new Label();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            panelReadWrite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridTags).BeginInit();
            panelRWButtons.SuspendLayout();
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
            lblTitle.Size = new Size(262, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "OPC 사용법 예시";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.Transparent;
            panelContent.Controls.Add(panelReadWrite);
            panelContent.Controls.Add(panelButtons);
            panelContent.Controls.Add(panelConnection);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 80);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(20);
            panelContent.Size = new Size(1140, 460);
            panelContent.TabIndex = 1;
            // 
            // panelReadWrite
            // 
            panelReadWrite.BackColor = Color.White;
            panelReadWrite.Controls.Add(gridTags);
            panelReadWrite.Controls.Add(panelRWButtons);
            panelReadWrite.Dock = DockStyle.Fill;
            panelReadWrite.Location = new Point(20, 220);
            panelReadWrite.Name = "panelReadWrite";
            panelReadWrite.Padding = new Padding(20);
            panelReadWrite.Size = new Size(1100, 220);
            panelReadWrite.TabIndex = 2;
            // 
            // gridTags
            // 
            gridTags.AllowUserToAddRows = false;
            gridTags.AllowUserToDeleteRows = false;
            gridTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridTags.BackgroundColor = Color.White;
            gridTags.BorderStyle = BorderStyle.None;
            gridTags.ColumnHeadersHeight = 40;
            gridTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridTags.Dock = DockStyle.Fill;
            gridTags.Location = new Point(20, 120);
            gridTags.Name = "gridTags";
            gridTags.ReadOnly = true;
            gridTags.RowHeadersVisible = false;
            gridTags.RowTemplate.Height = 35;
            gridTags.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridTags.Size = new Size(1060, 80);
            gridTags.TabIndex = 1;
            // 
            // panelRWButtons
            // 
            panelRWButtons.BackColor = Color.White;
            panelRWButtons.Controls.Add(btnUnsubscribe);
            panelRWButtons.Controls.Add(btnSubscribe);
            panelRWButtons.Controls.Add(btnWrite);
            panelRWButtons.Controls.Add(btnRead);
            panelRWButtons.Controls.Add(txtWriteValue);
            panelRWButtons.Controls.Add(lblWriteValue);
            panelRWButtons.Controls.Add(txtNodeId);
            panelRWButtons.Controls.Add(lblNodeId);
            panelRWButtons.Dock = DockStyle.Top;
            panelRWButtons.Location = new Point(20, 20);
            panelRWButtons.Name = "panelRWButtons";
            panelRWButtons.Size = new Size(1060, 100);
            panelRWButtons.TabIndex = 0;
            // 
            // btnUnsubscribe
            // 
            btnUnsubscribe.BorderColor = Color.FromArgb(220, 223, 230);
            btnUnsubscribe.ButtonType = ReaLTaiizor.Util.HopeButtonType.Warning;
            btnUnsubscribe.Cursor = Cursors.Hand;
            btnUnsubscribe.DangerColor = Color.FromArgb(245, 108, 108);
            btnUnsubscribe.DefaultColor = Color.FromArgb(255, 255, 255);
            btnUnsubscribe.Enabled = false;
            btnUnsubscribe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUnsubscribe.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnUnsubscribe.InfoColor = Color.FromArgb(144, 147, 153);
            btnUnsubscribe.Location = new Point(820, 50);
            btnUnsubscribe.Name = "btnUnsubscribe";
            btnUnsubscribe.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnUnsubscribe.Size = new Size(110, 35);
            btnUnsubscribe.SuccessColor = Color.FromArgb(103, 194, 58);
            btnUnsubscribe.TabIndex = 7;
            btnUnsubscribe.Text = "구독 해제";
            btnUnsubscribe.TextColor = Color.White;
            btnUnsubscribe.WarningColor = Color.FromArgb(230, 162, 60);
            btnUnsubscribe.Click += BtnUnsubscribe_Click;
            // 
            // btnSubscribe
            // 
            btnSubscribe.BorderColor = Color.FromArgb(220, 223, 230);
            btnSubscribe.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success;
            btnSubscribe.Cursor = Cursors.Hand;
            btnSubscribe.DangerColor = Color.FromArgb(245, 108, 108);
            btnSubscribe.DefaultColor = Color.FromArgb(255, 255, 255);
            btnSubscribe.Enabled = false;
            btnSubscribe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSubscribe.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnSubscribe.InfoColor = Color.FromArgb(144, 147, 153);
            btnSubscribe.Location = new Point(690, 50);
            btnSubscribe.Name = "btnSubscribe";
            btnSubscribe.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnSubscribe.Size = new Size(110, 35);
            btnSubscribe.SuccessColor = Color.FromArgb(103, 194, 58);
            btnSubscribe.TabIndex = 6;
            btnSubscribe.Text = "구독";
            btnSubscribe.TextColor = Color.White;
            btnSubscribe.WarningColor = Color.FromArgb(230, 162, 60);
            btnSubscribe.Click += BtnSubscribe_Click;
            // 
            // btnWrite
            // 
            btnWrite.BorderColor = Color.FromArgb(220, 223, 230);
            btnWrite.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnWrite.Cursor = Cursors.Hand;
            btnWrite.DangerColor = Color.FromArgb(245, 108, 108);
            btnWrite.DefaultColor = Color.FromArgb(255, 255, 255);
            btnWrite.Enabled = false;
            btnWrite.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnWrite.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnWrite.InfoColor = Color.FromArgb(144, 147, 153);
            btnWrite.Location = new Point(560, 50);
            btnWrite.Name = "btnWrite";
            btnWrite.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnWrite.Size = new Size(110, 35);
            btnWrite.SuccessColor = Color.FromArgb(103, 194, 58);
            btnWrite.TabIndex = 5;
            btnWrite.Text = "쓰기";
            btnWrite.TextColor = Color.White;
            btnWrite.WarningColor = Color.FromArgb(230, 162, 60);
            btnWrite.Click += BtnWrite_Click;
            // 
            // btnRead
            // 
            btnRead.BorderColor = Color.FromArgb(220, 223, 230);
            btnRead.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnRead.Cursor = Cursors.Hand;
            btnRead.DangerColor = Color.FromArgb(245, 108, 108);
            btnRead.DefaultColor = Color.FromArgb(255, 255, 255);
            btnRead.Enabled = false;
            btnRead.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRead.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnRead.InfoColor = Color.FromArgb(144, 147, 153);
            btnRead.Location = new Point(430, 50);
            btnRead.Name = "btnRead";
            btnRead.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnRead.Size = new Size(110, 35);
            btnRead.SuccessColor = Color.FromArgb(103, 194, 58);
            btnRead.TabIndex = 4;
            btnRead.Text = "읽기";
            btnRead.TextColor = Color.White;
            btnRead.WarningColor = Color.FromArgb(230, 162, 60);
            btnRead.Click += BtnRead_Click;
            // 
            // txtWriteValue
            // 
            txtWriteValue.Font = new Font("Segoe UI", 10F);
            txtWriteValue.Location = new Point(120, 56);
            txtWriteValue.Name = "txtWriteValue";
            txtWriteValue.Size = new Size(280, 25);
            txtWriteValue.TabIndex = 3;
            txtWriteValue.Text = "100";
            // 
            // lblWriteValue
            // 
            lblWriteValue.AutoSize = true;
            lblWriteValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblWriteValue.ForeColor = Color.FromArgb(80, 80, 80);
            lblWriteValue.Location = new Point(23, 59);
            lblWriteValue.Name = "lblWriteValue";
            lblWriteValue.Size = new Size(45, 19);
            lblWriteValue.TabIndex = 2;
            lblWriteValue.Text = "쓸 값:";
            // 
            // txtNodeId
            // 
            txtNodeId.Font = new Font("Segoe UI", 10F);
            txtNodeId.Location = new Point(120, 20);
            txtNodeId.Name = "txtNodeId";
            txtNodeId.Size = new Size(810, 25);
            txtNodeId.TabIndex = 1;
            txtNodeId.Text = "ns=2;s=Demo.Dynamic.Scalar.Int32";
            // 
            // lblNodeId
            // 
            lblNodeId.AutoSize = true;
            lblNodeId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNodeId.ForeColor = Color.FromArgb(80, 80, 80);
            lblNodeId.Location = new Point(23, 23);
            lblNodeId.Name = "lblNodeId";
            lblNodeId.Size = new Size(68, 19);
            lblNodeId.TabIndex = 0;
            lblNodeId.Text = "Node ID:";
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.White;
            panelButtons.Controls.Add(btnDisconnect);
            panelButtons.Controls.Add(btnConnect);
            panelButtons.Controls.Add(lblStatus);
            panelButtons.Dock = DockStyle.Top;
            panelButtons.Location = new Point(20, 130);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(20);
            panelButtons.Size = new Size(1100, 90);
            panelButtons.TabIndex = 1;
            // 
            // btnDisconnect
            // 
            btnDisconnect.BorderColor = Color.FromArgb(220, 223, 230);
            btnDisconnect.ButtonType = ReaLTaiizor.Util.HopeButtonType.Danger;
            btnDisconnect.Cursor = Cursors.Hand;
            btnDisconnect.DangerColor = Color.FromArgb(245, 108, 108);
            btnDisconnect.DefaultColor = Color.FromArgb(255, 255, 255);
            btnDisconnect.Enabled = false;
            btnDisconnect.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDisconnect.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnDisconnect.InfoColor = Color.FromArgb(144, 147, 153);
            btnDisconnect.Location = new Point(260, 20);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnDisconnect.Size = new Size(200, 40);
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
            btnConnect.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConnect.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnConnect.InfoColor = Color.FromArgb(144, 147, 153);
            btnConnect.Location = new Point(23, 20);
            btnConnect.Name = "btnConnect";
            btnConnect.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnConnect.Size = new Size(200, 40);
            btnConnect.SuccessColor = Color.FromArgb(103, 194, 58);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "연결";
            btnConnect.TextColor = Color.White;
            btnConnect.WarningColor = Color.FromArgb(230, 162, 60);
            btnConnect.Click += BtnConnect_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(245, 108, 108);
            lblStatus.Location = new Point(490, 29);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(86, 20);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "연결 안됨 ●";
            // 
            // panelConnection
            // 
            panelConnection.BackColor = Color.White;
            panelConnection.Controls.Add(txtServerUrl);
            panelConnection.Controls.Add(lblServerUrl);
            panelConnection.Dock = DockStyle.Top;
            panelConnection.Location = new Point(20, 20);
            panelConnection.Name = "panelConnection";
            panelConnection.Padding = new Padding(20);
            panelConnection.Size = new Size(1100, 110);
            panelConnection.TabIndex = 0;
            // 
            // txtServerUrl
            // 
            txtServerUrl.Font = new Font("Segoe UI", 10F);
            txtServerUrl.Location = new Point(150, 40);
            txtServerUrl.Name = "txtServerUrl";
            txtServerUrl.Size = new Size(800, 25);
            txtServerUrl.TabIndex = 1;
            txtServerUrl.Text = "opc.tcp://localhost:4840";
            // 
            // lblServerUrl
            // 
            lblServerUrl.AutoSize = true;
            lblServerUrl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblServerUrl.ForeColor = Color.FromArgb(80, 80, 80);
            lblServerUrl.Location = new Point(23, 43);
            lblServerUrl.Name = "lblServerUrl";
            lblServerUrl.Size = new Size(96, 19);
            lblServerUrl.TabIndex = 0;
            lblServerUrl.Text = "OPC UA URL:";
            // 
            // OpcControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Name = "OpcControl";
            Size = new Size(1140, 540);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContent.ResumeLayout(false);
            panelReadWrite.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridTags).EndInit();
            panelRWButtons.ResumeLayout(false);
            panelRWButtons.PerformLayout();
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
        private Label lblServerUrl;
        private TextBox txtServerUrl;
        private Panel panelButtons;
        private ReaLTaiizor.Controls.HopeButton btnConnect;
        private ReaLTaiizor.Controls.HopeButton btnDisconnect;
        private Label lblStatus;
        private Panel panelReadWrite;
        private DataGridView gridTags;
        private Panel panelRWButtons;
        private TextBox txtNodeId;
        private Label lblNodeId;
        private TextBox txtWriteValue;
        private Label lblWriteValue;
        private ReaLTaiizor.Controls.HopeButton btnRead;
        private ReaLTaiizor.Controls.HopeButton btnWrite;
        private ReaLTaiizor.Controls.HopeButton btnSubscribe;
        private ReaLTaiizor.Controls.HopeButton btnUnsubscribe;
    }
}
