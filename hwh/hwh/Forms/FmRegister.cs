using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using hwh.Core;
using hwh.Models;
using WHToolkit.Security;

namespace hwh.Forms
{
    public partial class FmRegister : Form
    {
        public FmRegister()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Paint += FmRegister_Paint;
        }

        private void FmRegister_Paint(object? sender, PaintEventArgs e)
        {
            // 둥근 모서리
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            using (var path = GetRoundedRectanglePath(this.ClientRectangle, 15))
            {
                this.Region = new Region(path);
            }
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            var registerRequest = new RegisterRequest
            {
                Username = txtUsername.Text.Trim(),
                Password = SecurityHelper.ComputeMD5Hash(txtPassword.Text.Trim()),
                PasswordConfirm = SecurityHelper.ComputeMD5Hash(txtPasswordConfirm.Text.Trim()),
                Name = txtName.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim()
            };

            if (DbCall.RegisterUser(registerRequest))
            {
                MessageBoxHelper.ShowSuccess("회원가입이 완료되었습니다.", "성공");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBoxHelper.ShowError("회원가입에 실패했습니다.\n아이디가 이미 존재할 수 있습니다.", "오류");
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBoxHelper.ShowWarning("아이디를 입력해주세요.", "입력 확인");
                txtUsername.Focus();
                return false;
            }

            if (txtUsername.Text.Length < 4)
            {
                MessageBoxHelper.ShowWarning("아이디는 4자 이상이어야 합니다.", "입력 확인");
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBoxHelper.ShowWarning("비밀번호를 입력해주세요.", "입력 확인");
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBoxHelper.ShowWarning("비밀번호는 6자 이상이어야 합니다.", "입력 확인");
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text != txtPasswordConfirm.Text)
            {
                MessageBoxHelper.ShowWarning("비밀번호가 일치하지 않습니다.", "입력 확인");
                txtPasswordConfirm.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBoxHelper.ShowWarning("이름을 입력해주세요.", "입력 확인");
                txtName.Focus();
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

