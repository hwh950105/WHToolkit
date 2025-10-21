namespace sampleapp
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FmLogin = new ReaLTaiizor.Forms.HopeForm();
            panelMain = new Panel();
            btnLogin = new ReaLTaiizor.Controls.HopeButton();
            Tbpassword = new ReaLTaiizor.Controls.HopeTextBox();
            Tbid = new ReaLTaiizor.Controls.HopeTextBox();
            Lbpassword = new ReaLTaiizor.Controls.HeaderLabel();
            Lbid = new ReaLTaiizor.Controls.HeaderLabel();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // FmLogin
            // 
            FmLogin.ControlBoxColorH = Color.FromArgb(228, 231, 237);
            FmLogin.ControlBoxColorHC = Color.FromArgb(245, 108, 108);
            FmLogin.ControlBoxColorN = Color.White;
            FmLogin.Dock = DockStyle.Top;
            FmLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            FmLogin.ForeColor = Color.FromArgb(242, 246, 252);
            FmLogin.Image = null;
            FmLogin.Location = new Point(0, 0);
            FmLogin.Name = "FmLogin";
            FmLogin.Size = new Size(450, 40);
            FmLogin.TabIndex = 0;
            FmLogin.Text = "로그인";
            FmLogin.ThemeColor = Color.FromArgb(92, 173, 255);
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(btnLogin);
            panelMain.Controls.Add(Tbpassword);
            panelMain.Controls.Add(Tbid);
            panelMain.Controls.Add(Lbpassword);
            panelMain.Controls.Add(Lbid);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 40);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(450, 360);
            panelMain.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.BorderColor = Color.FromArgb(220, 223, 230);
            btnLogin.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.DangerColor = Color.FromArgb(245, 108, 108);
            btnLogin.DefaultColor = Color.FromArgb(255, 255, 255);
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnLogin.InfoColor = Color.FromArgb(144, 147, 153);
            btnLogin.Location = new Point(85, 231);
            btnLogin.Name = "btnLogin";
            btnLogin.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnLogin.Size = new Size(280, 50);
            btnLogin.SuccessColor = Color.FromArgb(103, 194, 58);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "로그인";
            btnLogin.TextColor = Color.White;
            btnLogin.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // Tbpassword
            // 
            Tbpassword.BackColor = Color.White;
            Tbpassword.BaseColor = Color.FromArgb(44, 55, 66);
            Tbpassword.BorderColorA = Color.FromArgb(64, 158, 255);
            Tbpassword.BorderColorB = Color.FromArgb(220, 223, 230);
            Tbpassword.Font = new Font("Segoe UI", 11F);
            Tbpassword.ForeColor = Color.FromArgb(48, 49, 51);
            Tbpassword.Hint = "비밀번호를 입력하세요";
            Tbpassword.Location = new Point(85, 166);
            Tbpassword.MaxLength = 32767;
            Tbpassword.Multiline = false;
            Tbpassword.Name = "Tbpassword";
            Tbpassword.PasswordChar = '●';
            Tbpassword.ScrollBars = ScrollBars.None;
            Tbpassword.SelectedText = "";
            Tbpassword.SelectionLength = 0;
            Tbpassword.SelectionStart = 0;
            Tbpassword.Size = new Size(280, 36);
            Tbpassword.TabIndex = 2;
            Tbpassword.TabStop = false;
            Tbpassword.UseSystemPasswordChar = true;
            // 
            // Tbid
            // 
            Tbid.BackColor = Color.White;
            Tbid.BaseColor = Color.FromArgb(44, 55, 66);
            Tbid.BorderColorA = Color.FromArgb(64, 158, 255);
            Tbid.BorderColorB = Color.FromArgb(220, 223, 230);
            Tbid.Font = new Font("Segoe UI", 11F);
            Tbid.ForeColor = Color.FromArgb(48, 49, 51);
            Tbid.Hint = "아이디를 입력하세요";
            Tbid.Location = new Point(85, 86);
            Tbid.MaxLength = 32767;
            Tbid.Multiline = false;
            Tbid.Name = "Tbid";
            Tbid.PasswordChar = '\0';
            Tbid.ScrollBars = ScrollBars.None;
            Tbid.SelectedText = "";
            Tbid.SelectionLength = 0;
            Tbid.SelectionStart = 0;
            Tbid.Size = new Size(280, 36);
            Tbid.TabIndex = 1;
            Tbid.TabStop = false;
            Tbid.UseSystemPasswordChar = false;
            // 
            // Lbpassword
            // 
            Lbpassword.AutoSize = true;
            Lbpassword.BackColor = Color.Transparent;
            Lbpassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Lbpassword.ForeColor = Color.FromArgb(80, 80, 80);
            Lbpassword.Location = new Point(85, 141);
            Lbpassword.Name = "Lbpassword";
            Lbpassword.Size = new Size(65, 19);
            Lbpassword.TabIndex = 0;
            Lbpassword.Text = "비밀번호";
            // 
            // Lbid
            // 
            Lbid.AutoSize = true;
            Lbid.BackColor = Color.Transparent;
            Lbid.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Lbid.ForeColor = Color.FromArgb(80, 80, 80);
            Lbid.Location = new Point(85, 61);
            Lbid.Name = "Lbid";
            Lbid.Size = new Size(51, 19);
            Lbid.TabIndex = 0;
            Lbid.Text = "아이디";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(450, 400);
            Controls.Add(panelMain);
            Controls.Add(FmLogin);
            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = new Size(2560, 1392);
            MinimumSize = new Size(190, 40);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += Form1_Load;
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Forms.HopeForm FmLogin;
        private Panel panelMain;
        private ReaLTaiizor.Controls.HopeButton btnLogin;
        private ReaLTaiizor.Controls.HopeTextBox Tbpassword;
        private ReaLTaiizor.Controls.HopeTextBox Tbid;
        private ReaLTaiizor.Controls.HeaderLabel Lbpassword;
        private ReaLTaiizor.Controls.HeaderLabel Lbid;
    }
}
