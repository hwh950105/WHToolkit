using hwh.Core;
using hwh.Models;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using WHToolkit.IO;
using WHToolkit.Security;

namespace hwh.Forms
{
    public partial class FmLogin : Form
    {
        private string configIniFilePath = string.Empty;
        private IniHelper? iniHelper;

        public FmLogin()
        {
            InitializeComponent();
            InitializeConfiguration();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Paint += FmLogin_Paint;
        }

        private void FmLogin_Paint(object? sender, PaintEventArgs e)
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

        private void InitializeConfiguration()
        {
            configIniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HHconfig.ini");
            iniHelper = new IniHelper(configIniFilePath);

            if (!File.Exists(configIniFilePath))
            {
                iniHelper.Write("LoginSettings", "Username", "");
                iniHelper.Write("LoginSettings", "Password", "");
                iniHelper.Write("LoginSettings", "RememberMe", "0");
            }

            LoadLoginSettings();
        }

        private void LoadLoginSettings()
        {
            if (iniHelper == null) return;

            string username = iniHelper.Read("LoginSettings", "Username");
            string password = iniHelper.Read("LoginSettings", "Password");
            string rememberMe = iniHelper.Read("LoginSettings", "RememberMe");

            bool isRememberMe = rememberMe == "1";

            if (isRememberMe && !string.IsNullOrEmpty(username))
            {
                txtUsername.Text = username;
                txtPassword.Text = password;
                chkRememberMe.Checked = isRememberMe;
            }
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            PerformLogin();
        }

        private void PerformLogin()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBoxHelper.ShowWarning("아이디를 입력해주세요.", "로그인");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBoxHelper.ShowWarning("비밀번호를 입력해주세요.", "로그인");
                return;
            }

            var loginRequest = new LoginRequest
            {
                Username = txtUsername.Text.Trim(),
                Password = SecurityHelper.ComputeMD5Hash(txtPassword.Text.Trim())
            };

            if (DbCall.GetLogin(loginRequest))
            {
                SaveLoginSettings();
                OpenMainForm();
            }
            else
            {
                MessageBoxHelper.ShowError("아이디 또는 비밀번호가 일치하지 않습니다.", "로그인 실패");
            }
        }

        private void SaveLoginSettings()
        {
            if (iniHelper == null) return;

            if (chkRememberMe.Checked)
            {
                iniHelper.Write("LoginSettings", "Username", txtUsername.Text);
                iniHelper.Write("LoginSettings", "Password", txtPassword.Text);
                iniHelper.Write("LoginSettings", "RememberMe", "1");
            }
            else
            {
                iniHelper.Write("LoginSettings", "Username", "");
                iniHelper.Write("LoginSettings", "Password", "");
                iniHelper.Write("LoginSettings", "RememberMe", "0");
            }
        }

        private void OpenMainForm()
        {
            Globaldata.useremail = txtUsername.Text;

            Main mainForm = new Main();
            mainForm.Show();
            this.Hide();
        }

        public void PrepareForRelogin()
        {
            txtPassword.Text = string.Empty;
            if (!chkRememberMe.Checked)
            {
                txtUsername.Text = string.Empty;
            }

            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.CenterToScreen();
            txtUsername.Focus();
        }

        private void LnkRegister_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FmRegister registerForm = new FmRegister())
            {
                if (registerForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBoxHelper.ShowSuccess("회원가입이 완료되었습니다.\n로그인해주세요.", "환영합니다");
                }
            }
        }
    }
}

