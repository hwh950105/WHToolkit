namespace hwh.Controls.Win32Controls
{
    partial class HotkeyControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpRegister;
        private System.Windows.Forms.Label lblModifiers;
        private System.Windows.Forms.ComboBox comboModifiers;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.ComboBox comboKey;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.GroupBox grpHotkeys;
        private System.Windows.Forms.ListBox listHotkeys;
        private System.Windows.Forms.Button btnUnregister;
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
            grpRegister = new GroupBox();
            btnRegister = new Button();
            comboKey = new ComboBox();
            lblKey = new Label();
            comboModifiers = new ComboBox();
            lblModifiers = new Label();
            grpHotkeys = new GroupBox();
            btnUnregister = new Button();
            listHotkeys = new ListBox();
            lblStatus = new Label();
            lblInfo = new Label();
            grpRegister.SuspendLayout();
            grpHotkeys.SuspendLayout();
            SuspendLayout();
            // 
            // grpRegister
            // 
            grpRegister.Controls.Add(btnRegister);
            grpRegister.Controls.Add(comboKey);
            grpRegister.Controls.Add(lblKey);
            grpRegister.Controls.Add(comboModifiers);
            grpRegister.Controls.Add(lblModifiers);
            grpRegister.Font = new Font("맑은 고딕", 11F);
            grpRegister.Location = new Point(20, 70);
            grpRegister.Name = "grpRegister";
            grpRegister.Size = new Size(900, 150);
            grpRegister.TabIndex = 0;
            grpRegister.TabStop = false;
            grpRegister.Text = "핫키 등록";
            // 
            // btnRegister
            // 
            btnRegister.Font = new Font("맑은 고딕", 11F);
            btnRegister.Location = new Point(700, 55);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(170, 60);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "등록";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // comboKey
            // 
            comboKey.DropDownStyle = ComboBoxStyle.DropDownList;
            comboKey.Font = new Font("맑은 고딕", 11F);
            comboKey.FormattingEnabled = true;
            comboKey.Location = new Point(500, 70);
            comboKey.Name = "comboKey";
            comboKey.Size = new Size(180, 28);
            comboKey.TabIndex = 3;
            // 
            // lblKey
            // 
            lblKey.AutoSize = true;
            lblKey.Font = new Font("맑은 고딕", 11F);
            lblKey.Location = new Point(440, 73);
            lblKey.Name = "lblKey";
            lblKey.Size = new Size(27, 20);
            lblKey.TabIndex = 2;
            lblKey.Text = "키:";
            // 
            // comboModifiers
            // 
            comboModifiers.DropDownStyle = ComboBoxStyle.DropDownList;
            comboModifiers.Font = new Font("맑은 고딕", 11F);
            comboModifiers.FormattingEnabled = true;
            comboModifiers.Location = new Point(150, 70);
            comboModifiers.Name = "comboModifiers";
            comboModifiers.Size = new Size(250, 28);
            comboModifiers.TabIndex = 1;
            // 
            // lblModifiers
            // 
            lblModifiers.AutoSize = true;
            lblModifiers.Font = new Font("맑은 고딕", 11F);
            lblModifiers.Location = new Point(25, 73);
            lblModifiers.Name = "lblModifiers";
            lblModifiers.Size = new Size(77, 20);
            lblModifiers.TabIndex = 0;
            lblModifiers.Text = "수정자 키:";
            // 
            // grpHotkeys
            // 
            grpHotkeys.Controls.Add(btnUnregister);
            grpHotkeys.Controls.Add(listHotkeys);
            grpHotkeys.Font = new Font("맑은 고딕", 11F);
            grpHotkeys.Location = new Point(20, 240);
            grpHotkeys.Name = "grpHotkeys";
            grpHotkeys.Size = new Size(900, 250);
            grpHotkeys.TabIndex = 1;
            grpHotkeys.TabStop = false;
            grpHotkeys.Text = "등록된 핫키";
            // 
            // btnUnregister
            // 
            btnUnregister.Font = new Font("맑은 고딕", 11F);
            btnUnregister.Location = new Point(700, 50);
            btnUnregister.Name = "btnUnregister";
            btnUnregister.Size = new Size(170, 60);
            btnUnregister.TabIndex = 1;
            btnUnregister.Text = "해제";
            btnUnregister.UseVisualStyleBackColor = true;
            btnUnregister.Click += btnUnregister_Click;
            // 
            // listHotkeys
            // 
            listHotkeys.Font = new Font("맑은 고딕", 10F);
            listHotkeys.FormattingEnabled = true;
            listHotkeys.ItemHeight = 17;
            listHotkeys.Location = new Point(25, 40);
            listHotkeys.Name = "listHotkeys";
            listHotkeys.Size = new Size(655, 174);
            listHotkeys.TabIndex = 0;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            lblStatus.ForeColor = Color.Blue;
            lblStatus.Location = new Point(20, 510);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(71, 20);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "대기 중...";
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("맑은 고딕", 10F);
            lblInfo.ForeColor = Color.DarkOrange;
            lblInfo.Location = new Point(20, 20);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(579, 19);
            lblInfo.TabIndex = 3;
            lblInfo.Text = "⚠️ 핫키는 애플리케이션이 실행 중일 때만 동작합니다. 중복된 핫키는 등록할 수 없습니다.";
            // 
            // HotkeyControl
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(lblInfo);
            Controls.Add(lblStatus);
            Controls.Add(grpHotkeys);
            Controls.Add(grpRegister);
            Font = new Font("맑은 고딕", 11F);
            Name = "HotkeyControl";
            Size = new Size(1854, 810);
            Load += HotkeyControl_Load;
            grpRegister.ResumeLayout(false);
            grpRegister.PerformLayout();
            grpHotkeys.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

