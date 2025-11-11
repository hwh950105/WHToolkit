using System.Drawing;
using System.Windows.Forms;
using hwh.Core;

namespace hwh.Forms
{
    partial class FmRegister
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
            this.panelMain = new Panel();
            this.panelHeader = new Panel();
            this.lblTitle = new Label();
            this.lblSubtitle = new Label();
            this.panelContent = new Panel();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblPasswordConfirm = new Label();
            this.txtPasswordConfirm = new TextBox();
            this.lblName = new Label();
            this.txtName = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblPhone = new Label();
            this.txtPhone = new TextBox();
            this.panelButtons = new Panel();
            this.btnCancel = new Button();
            this.btnRegister = new Button();

            this.panelMain.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();

            // 
            // FmRegister
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.Controls.Add(this.panelMain);
            this.Name = "FmRegister";
            this.Text = "회원가입";

            // 
            // panelMain
            // 
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.BackColor = Color.White;
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelButtons);
            this.panelMain.Controls.Add(this.panelHeader);

            // 
            // panelHeader
            // 
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 100;
            this.panelHeader.BackColor = Theme.Primary;
            this.panelHeader.Padding = new Padding(30, 20, 30, 20);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);

            // 
            // lblTitle
            // 
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitle.ForeColor = Theme.TextWhite;
            this.lblTitle.Text = "회원가입";
            this.lblTitle.Height = 40;
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Dock = DockStyle.Bottom;
            this.lblSubtitle.Font = Theme.FontRegular;
            this.lblSubtitle.ForeColor = Theme.TextLight;
            this.lblSubtitle.Text = "새 계정을 만들어보세요";
            this.lblSubtitle.Height = 25;
            this.lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // panelContent
            // 
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.White;
            this.panelContent.Padding = new Padding(30, 20, 30, 20);
            this.panelContent.AutoScroll = true;
            this.panelContent.Controls.Add(this.txtPhone);
            this.panelContent.Controls.Add(this.lblPhone);
            this.panelContent.Controls.Add(this.txtEmail);
            this.panelContent.Controls.Add(this.lblEmail);
            this.panelContent.Controls.Add(this.txtName);
            this.panelContent.Controls.Add(this.lblName);
            this.panelContent.Controls.Add(this.txtPasswordConfirm);
            this.panelContent.Controls.Add(this.lblPasswordConfirm);
            this.panelContent.Controls.Add(this.txtPassword);
            this.panelContent.Controls.Add(this.lblPassword);
            this.panelContent.Controls.Add(this.txtUsername);
            this.panelContent.Controls.Add(this.lblUsername);

            // 
            // lblUsername
            // 
            this.lblUsername.Location = new Point(50, 10);
            this.lblUsername.Size = new Size(380, 25);
            this.lblUsername.Font = Theme.FontBodyBold;
            this.lblUsername.ForeColor = Theme.TextPrimary;
            this.lblUsername.Text = "아이디 *";

            // 
            // txtUsername
            // 
            this.txtUsername.Location = new Point(50, 35);
            this.txtUsername.Size = new Size(380, 35);
            this.txtUsername.Font = Theme.FontBody;
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;
            this.txtUsername.BackColor = Color.White;

            // 
            // lblPassword
            // 
            this.lblPassword.Location = new Point(50, 80);
            this.lblPassword.Size = new Size(380, 25);
            this.lblPassword.Font = Theme.FontBodyBold;
            this.lblPassword.ForeColor = Theme.TextPrimary;
            this.lblPassword.Text = "비밀번호 *";

            // 
            // txtPassword
            // 
            this.txtPassword.Location = new Point(50, 105);
            this.txtPassword.Size = new Size(380, 35);
            this.txtPassword.Font = Theme.FontBody;
            this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.BackColor = Color.White;

            // 
            // lblPasswordConfirm
            // 
            this.lblPasswordConfirm.Location = new Point(50, 150);
            this.lblPasswordConfirm.Size = new Size(380, 25);
            this.lblPasswordConfirm.Font = Theme.FontBodyBold;
            this.lblPasswordConfirm.ForeColor = Theme.TextPrimary;
            this.lblPasswordConfirm.Text = "비밀번호 확인 *";

            // 
            // txtPasswordConfirm
            // 
            this.txtPasswordConfirm.Location = new Point(50, 175);
            this.txtPasswordConfirm.Size = new Size(380, 35);
            this.txtPasswordConfirm.Font = Theme.FontBody;
            this.txtPasswordConfirm.BorderStyle = BorderStyle.FixedSingle;
            this.txtPasswordConfirm.UseSystemPasswordChar = true;
            this.txtPasswordConfirm.BackColor = Color.White;

            // 
            // lblName
            // 
            this.lblName.Location = new Point(50, 220);
            this.lblName.Size = new Size(380, 25);
            this.lblName.Font = Theme.FontBodyBold;
            this.lblName.ForeColor = Theme.TextPrimary;
            this.lblName.Text = "이름 *";

            // 
            // txtName
            // 
            this.txtName.Location = new Point(50, 245);
            this.txtName.Size = new Size(380, 35);
            this.txtName.Font = Theme.FontBody;
            this.txtName.BorderStyle = BorderStyle.FixedSingle;
            this.txtName.BackColor = Color.White;

            // 
            // lblEmail
            // 
            this.lblEmail.Location = new Point(50, 290);
            this.lblEmail.Size = new Size(380, 25);
            this.lblEmail.Font = Theme.FontBodyBold;
            this.lblEmail.ForeColor = Theme.TextPrimary;
            this.lblEmail.Text = "이메일 (선택)";

            // 
            // txtEmail
            // 
            this.txtEmail.Location = new Point(50, 315);
            this.txtEmail.Size = new Size(380, 35);
            this.txtEmail.Font = Theme.FontBody;
            this.txtEmail.BorderStyle = BorderStyle.FixedSingle;
            this.txtEmail.BackColor = Color.White;

            // 
            // lblPhone
            // 
            this.lblPhone.Location = new Point(50, 360);
            this.lblPhone.Size = new Size(380, 25);
            this.lblPhone.Font = Theme.FontBodyBold;
            this.lblPhone.ForeColor = Theme.TextPrimary;
            this.lblPhone.Text = "연락처 (선택)";

            // 
            // txtPhone
            // 
            this.txtPhone.Location = new Point(50, 385);
            this.txtPhone.Size = new Size(380, 35);
            this.txtPhone.Font = Theme.FontBody;
            this.txtPhone.BorderStyle = BorderStyle.FixedSingle;
            this.txtPhone.BackColor = Color.White;

            // 
            // panelButtons
            // 
            this.panelButtons.Dock = DockStyle.Bottom;
            this.panelButtons.Height = 80;
            this.panelButtons.BackColor = Color.White;
            this.panelButtons.Padding = new Padding(30, 15, 30, 15);
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnRegister);

            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(170, 15);
            this.btnCancel.Size = new Size(120, 40);
            this.btnCancel.Font = Theme.FontBodyBold;
            this.btnCancel.BackColor = Color.Transparent;
            this.btnCancel.ForeColor = Theme.TextSecondary;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = Theme.BorderLight;
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.Text = "취소";
            this.btnCancel.Click += BtnCancel_Click;

            // 
            // btnRegister
            // 
            this.btnRegister.Location = new Point(300, 15);
            this.btnRegister.Size = new Size(130, 40);
            this.btnRegister.Font = Theme.FontBodyBold;
            this.btnRegister.BackColor = Theme.Primary;
            this.btnRegister.ForeColor = Theme.TextWhite;
            this.btnRegister.FlatStyle = FlatStyle.Flat;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.Cursor = Cursors.Hand;
            this.btnRegister.Text = "가입하기";
            this.btnRegister.Click += BtnRegister_Click;

            this.panelMain.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Panel panelMain;
        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel panelContent;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblPasswordConfirm;
        private TextBox txtPasswordConfirm;
        private Label lblName;
        private TextBox txtName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhone;
        private TextBox txtPhone;
        private Panel panelButtons;
        private Button btnCancel;
        private Button btnRegister;
    }
}

