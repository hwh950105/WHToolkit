using System.Drawing;
using System.Windows.Forms;
using hwh.Core;

namespace hwh.Forms
{
    partial class FmLogin
    {
        private System.ComponentModel.IContainer components = null;

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
            this.panelLeft = new Panel();
            this.lblWelcome = new Label();
            this.lblSubtitle = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.chkRememberMe = new CheckBox();
            this.btnLogin = new Button();
            this.lnkRegister = new LinkLabel();
            this.lblUsernameLabel = new Label();
            this.lblPasswordLabel = new Label();
            this.panelRight = new Panel();
            this.lblAppName = new Label();
            this.lblVersion = new Label();

            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();

            // 
            // FmLogin
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 600);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Name = "FmLogin";
            this.Text = "로그인";

            // 
            // panelLeft
            // 
            this.panelLeft.Dock = DockStyle.Left;
            this.panelLeft.Width = 400;
            this.panelLeft.BackColor = Color.White;
            this.panelLeft.Padding = new Padding(50, 80, 50, 80);
            this.panelLeft.Controls.Add(this.lnkRegister);
            this.panelLeft.Controls.Add(this.btnLogin);
            this.panelLeft.Controls.Add(this.chkRememberMe);
            this.panelLeft.Controls.Add(this.txtPassword);
            this.panelLeft.Controls.Add(this.lblPasswordLabel);
            this.panelLeft.Controls.Add(this.txtUsername);
            this.panelLeft.Controls.Add(this.lblUsernameLabel);
            this.panelLeft.Controls.Add(this.lblSubtitle);
            this.panelLeft.Controls.Add(this.lblWelcome);

            // 
            // lblWelcome
            // 
            this.lblWelcome.Location = new Point(50, 80);
            this.lblWelcome.Size = new Size(300, 50);
            this.lblWelcome.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblWelcome.ForeColor = Theme.TextPrimary;
            this.lblWelcome.Text = "환영합니다";

            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Location = new Point(50, 135);
            this.lblSubtitle.Size = new Size(300, 30);
            this.lblSubtitle.Font = Theme.FontBody;
            this.lblSubtitle.ForeColor = Theme.TextSecondary;
            this.lblSubtitle.Text = "계속하려면 로그인하세요";

            // 
            // lblUsernameLabel
            // 
            this.lblUsernameLabel.Location = new Point(50, 200);
            this.lblUsernameLabel.Size = new Size(300, 25);
            this.lblUsernameLabel.Font = Theme.FontBodyBold;
            this.lblUsernameLabel.ForeColor = Theme.TextPrimary;
            this.lblUsernameLabel.Text = "아이디";

            // 
            // txtUsername
            // 
            this.txtUsername.Location = new Point(50, 225);
            this.txtUsername.Size = new Size(300, 40);
            this.txtUsername.Font = new Font("Segoe UI", 11F);
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;
            this.txtUsername.BackColor = Color.White;
            this.txtUsername.ForeColor = Theme.TextPrimary;

            // 
            // lblPasswordLabel
            // 
            this.lblPasswordLabel.Location = new Point(50, 285);
            this.lblPasswordLabel.Size = new Size(300, 25);
            this.lblPasswordLabel.Font = Theme.FontBodyBold;
            this.lblPasswordLabel.ForeColor = Theme.TextPrimary;
            this.lblPasswordLabel.Text = "비밀번호";

            // 
            // txtPassword
            // 
            this.txtPassword.Location = new Point(50, 310);
            this.txtPassword.Size = new Size(300, 40);
            this.txtPassword.Font = new Font("Segoe UI", 11F);
            this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.BackColor = Color.White;
            this.txtPassword.ForeColor = Theme.TextPrimary;

            // 
            // chkRememberMe
            // 
            this.chkRememberMe.Location = new Point(50, 365);
            this.chkRememberMe.Size = new Size(150, 25);
            this.chkRememberMe.Font = Theme.FontBody;
            this.chkRememberMe.ForeColor = Theme.TextSecondary;
            this.chkRememberMe.Text = "로그인 정보 저장";
            this.chkRememberMe.Cursor = Cursors.Hand;

            // 
            // btnLogin
            // 
            this.btnLogin.Location = new Point(50, 410);
            this.btnLogin.Size = new Size(300, 45);
            this.btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnLogin.BackColor = Theme.Primary;
            this.btnLogin.ForeColor = Theme.TextWhite;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.Text = "로그인";
            this.btnLogin.Click += BtnLogin_Click;

            // 
            // lnkRegister
            // 
            this.lnkRegister.Location = new Point(50, 470);
            this.lnkRegister.Size = new Size(300, 25);
            this.lnkRegister.Font = Theme.FontBody;
            this.lnkRegister.LinkColor = Theme.Primary;
            this.lnkRegister.Text = "계정이 없으신가요? 회원가입";
            this.lnkRegister.TextAlign = ContentAlignment.MiddleCenter;
            this.lnkRegister.Cursor = Cursors.Hand;
            this.lnkRegister.LinkBehavior = LinkBehavior.HoverUnderline;
            this.lnkRegister.LinkClicked += LnkRegister_LinkClicked;

            // 
            // panelRight
            // 
            this.panelRight.Dock = DockStyle.Fill;
            this.panelRight.BackColor = Theme.Primary;
            this.panelRight.Controls.Add(this.lblVersion);
            this.panelRight.Controls.Add(this.lblAppName);

            // 
            // lblAppName
            // 
            this.lblAppName.Dock = DockStyle.Fill;
            this.lblAppName.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            this.lblAppName.ForeColor = Theme.TextWhite;
            this.lblAppName.Text = "HWH\nSystem";
            this.lblAppName.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblVersion
            // 
            this.lblVersion.Dock = DockStyle.Bottom;
            this.lblVersion.Height = 50;
            this.lblVersion.Font = Theme.FontBody;
            this.lblVersion.ForeColor = Theme.TextLight;
            this.lblVersion.Text = "Version 1.0.0";
            this.lblVersion.TextAlign = ContentAlignment.MiddleCenter;

            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Panel panelLeft;
        private Label lblWelcome;
        private Label lblSubtitle;
        private Label lblUsernameLabel;
        private TextBox txtUsername;
        private Label lblPasswordLabel;
        private TextBox txtPassword;
        private CheckBox chkRememberMe;
        private Button btnLogin;
        private LinkLabel lnkRegister;
        private Panel panelRight;
        private Label lblAppName;
        private Label lblVersion;
    }
}
