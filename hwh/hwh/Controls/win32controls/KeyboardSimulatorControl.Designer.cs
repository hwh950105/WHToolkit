namespace hwh.Controls.Win32Controls
{
    partial class KeyboardSimulatorControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpSingleKey;
        private System.Windows.Forms.GroupBox grpText;
        private System.Windows.Forms.GroupBox grpShortcut;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.ComboBox comboKey;
        private System.Windows.Forms.Button btnSendKey;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Button btnSendText;
        private System.Windows.Forms.Button btnCtrlC;
        private System.Windows.Forms.Button btnCtrlV;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpSingleKey = new System.Windows.Forms.GroupBox();
            this.btnSendKey = new System.Windows.Forms.Button();
            this.comboKey = new System.Windows.Forms.ComboBox();
            this.lblKey = new System.Windows.Forms.Label();
            this.grpText = new System.Windows.Forms.GroupBox();
            this.btnSendText = new System.Windows.Forms.Button();
            this.txtText = new System.Windows.Forms.TextBox();
            this.lblText = new System.Windows.Forms.Label();
            this.grpShortcut = new System.Windows.Forms.GroupBox();
            this.btnCtrlV = new System.Windows.Forms.Button();
            this.btnCtrlC = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpSingleKey.SuspendLayout();
            this.grpText.SuspendLayout();
            this.grpShortcut.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSingleKey
            // 
            this.grpSingleKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSingleKey.Controls.Add(this.btnSendKey);
            this.grpSingleKey.Controls.Add(this.comboKey);
            this.grpSingleKey.Controls.Add(this.lblKey);
            this.grpSingleKey.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.grpSingleKey.Location = new System.Drawing.Point(20, 70);
            this.grpSingleKey.MinimumSize = new System.Drawing.Size(900, 140);
            this.grpSingleKey.Name = "grpSingleKey";
            this.grpSingleKey.Size = new System.Drawing.Size(1100, 140);
            this.grpSingleKey.TabIndex = 0;
            this.grpSingleKey.TabStop = false;
            this.grpSingleKey.Text = "단일 키 전송";
            // 
            // btnSendKey
            // 
            this.btnSendKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendKey.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.btnSendKey.Location = new System.Drawing.Point(940, 50);
            this.btnSendKey.Name = "btnSendKey";
            this.btnSendKey.Size = new System.Drawing.Size(140, 60);
            this.btnSendKey.TabIndex = 2;
            this.btnSendKey.Text = "전송";
            this.btnSendKey.UseVisualStyleBackColor = true;
            this.btnSendKey.Click += new System.EventHandler(this.btnSendKey_Click);
            // 
            // comboKey
            // 
            this.comboKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboKey.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.comboKey.FormattingEnabled = true;
            this.comboKey.Location = new System.Drawing.Point(100, 55);
            this.comboKey.Name = "comboKey";
            this.comboKey.Size = new System.Drawing.Size(820, 36);
            this.comboKey.TabIndex = 1;
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.lblKey.Location = new System.Drawing.Point(25, 58);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(42, 28);
            this.lblKey.TabIndex = 0;
            this.lblKey.Text = "키:";
            // 
            // grpText
            // 
            this.grpText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpText.Controls.Add(this.btnSendText);
            this.grpText.Controls.Add(this.txtText);
            this.grpText.Controls.Add(this.lblText);
            this.grpText.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.grpText.Location = new System.Drawing.Point(20, 230);
            this.grpText.MinimumSize = new System.Drawing.Size(900, 140);
            this.grpText.Name = "grpText";
            this.grpText.Size = new System.Drawing.Size(1100, 140);
            this.grpText.TabIndex = 1;
            this.grpText.TabStop = false;
            this.grpText.Text = "텍스트 입력";
            // 
            // btnSendText
            // 
            this.btnSendText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendText.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.btnSendText.Location = new System.Drawing.Point(940, 50);
            this.btnSendText.Name = "btnSendText";
            this.btnSendText.Size = new System.Drawing.Size(140, 60);
            this.btnSendText.TabIndex = 2;
            this.btnSendText.Text = "입력";
            this.btnSendText.UseVisualStyleBackColor = true;
            this.btnSendText.Click += new System.EventHandler(this.btnSendText_Click);
            // 
            // txtText
            // 
            this.txtText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtText.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.txtText.Location = new System.Drawing.Point(150, 55);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(770, 34);
            this.txtText.TabIndex = 1;
            this.txtText.Text = "Hello, Win32 API!";
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.lblText.Location = new System.Drawing.Point(25, 58);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(102, 28);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "텍스트:";
            // 
            // grpShortcut
            // 
            this.grpShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpShortcut.Controls.Add(this.btnCtrlV);
            this.grpShortcut.Controls.Add(this.btnCtrlC);
            this.grpShortcut.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.grpShortcut.Location = new System.Drawing.Point(20, 390);
            this.grpShortcut.MinimumSize = new System.Drawing.Size(900, 140);
            this.grpShortcut.Name = "grpShortcut";
            this.grpShortcut.Size = new System.Drawing.Size(1100, 140);
            this.grpShortcut.TabIndex = 2;
            this.grpShortcut.TabStop = false;
            this.grpShortcut.Text = "단축키";
            // 
            // btnCtrlV
            // 
            this.btnCtrlV.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnCtrlV.Location = new System.Drawing.Point(220, 50);
            this.btnCtrlV.Name = "btnCtrlV";
            this.btnCtrlV.Size = new System.Drawing.Size(180, 65);
            this.btnCtrlV.TabIndex = 1;
            this.btnCtrlV.Text = "Ctrl+V";
            this.btnCtrlV.UseVisualStyleBackColor = true;
            this.btnCtrlV.Click += new System.EventHandler(this.btnCtrlV_Click);
            // 
            // btnCtrlC
            // 
            this.btnCtrlC.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnCtrlC.Location = new System.Drawing.Point(30, 50);
            this.btnCtrlC.Name = "btnCtrlC";
            this.btnCtrlC.Size = new System.Drawing.Size(180, 65);
            this.btnCtrlC.TabIndex = 0;
            this.btnCtrlC.Text = "Ctrl+C";
            this.btnCtrlC.UseVisualStyleBackColor = true;
            this.btnCtrlC.Click += new System.EventHandler(this.btnCtrlC_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(20, 550);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(116, 28);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "대기 중...";
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.lblInfo.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblInfo.Location = new System.Drawing.Point(20, 20);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(1100, 35);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "⚠️ 버튼 클릭 후 3초 후에 동작합니다. 테스트할 프로그램(메모장 등)을 미리 열어두세요.";
            // 
            // KeyboardSimulatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.grpShortcut);
            this.Controls.Add(this.grpText);
            this.Controls.Add(this.grpSingleKey);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "KeyboardSimulatorControl";
            this.Load += new System.EventHandler(this.KeyboardSimulatorControl_Load);
            this.grpSingleKey.ResumeLayout(false);
            this.grpSingleKey.PerformLayout();
            this.grpText.ResumeLayout(false);
            this.grpText.PerformLayout();
            this.grpShortcut.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

