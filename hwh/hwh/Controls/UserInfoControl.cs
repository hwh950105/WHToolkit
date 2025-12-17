using System;
using System.Linq;
using System.Windows.Forms;
using hwh.Core;
using hwh.Forms;
using hwh.Models;
using WHToolkit.Security;

namespace hwh.Controls
{
    public partial class UserInfoControl : UserControl
    {
        private UserModel? _currentUser;

        public UserInfoControl()
        {
            InitializeComponent();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            try
            {
                string username = Globaldata.useremail;
                if (string.IsNullOrEmpty(username))
                {
                    MessageBoxHelper.ShowError("로그인 정보를 찾을 수 없습니다.", "오류");
                    return;
                }

                _currentUser = DbCall.GetUserByUsername(username);
                if (_currentUser == null)
                {
                    MessageBoxHelper.ShowError("사용자 정보를 불러올 수 없습니다.", "오류");
                    return;
                }

                // UI에 정보 표시
                lblUsername.Text = _currentUser.Username;
                tbName.Text = _currentUser.Name ?? "";
                tbEmail.Text = _currentUser.Email ?? "";
                tbPhone.Text = _currentUser.Phone ?? "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, "사용자 정보 로드 실패");
                MessageBoxHelper.ShowError($"사용자 정보를 불러오는데 실패했습니다.\n{ex.Message}", "오류");
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBoxHelper.ShowError("사용자 정보를 찾을 수 없습니다.", "오류");
                return;
            }

            // 이름 필수
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBoxHelper.ShowWarning("이름을 입력해주세요.", "입력 확인");
                return;
            }

            // 비밀번호 변경 로직
            bool isPasswordChange = !string.IsNullOrWhiteSpace(tbCurrentPassword.Text);
            
            if (isPasswordChange)
            {
                // 현재 비밀번호 확인
                string currentPasswordHash = SecurityHelper.ComputeMD5Hash(tbCurrentPassword.Text.Trim());
                if (currentPasswordHash != _currentUser.Password)
                {
                    MessageBoxHelper.ShowError("현재 비밀번호가 일치하지 않습니다.", "비밀번호 오류");
                    return;
                }

                // 새 비밀번호 유효성 검사
                if (string.IsNullOrWhiteSpace(tbNewPassword.Text))
                {
                    MessageBoxHelper.ShowWarning("새 비밀번호를 입력해주세요.", "입력 확인");
                    return;
                }

                if (tbNewPassword.Text.Trim().Length < 6)
                {
                    MessageBoxHelper.ShowWarning("새 비밀번호는 6자 이상이어야 합니다.", "입력 확인");
                    return;
                }

                if (tbNewPassword.Text != tbNewPasswordConfirm.Text)
                {
                    MessageBoxHelper.ShowError("새 비밀번호가 일치하지 않습니다.", "비밀번호 오류");
                    return;
                }
            }

            // 업데이트 수행
            try
            {
                string? newPasswordHash = null;
                if (isPasswordChange)
                {
                    newPasswordHash = SecurityHelper.ComputeMD5Hash(tbNewPassword.Text.Trim());
                }

                bool success = DbCall.UpdateUser(
                    _currentUser.USER_ID,
                    tbName.Text.Trim(),
                    tbEmail.Text.Trim(),
                    tbPhone.Text.Trim(),
                    newPasswordHash
                );

                if (success)
                {
                    MessageBoxHelper.ShowSuccess("정보가 성공적으로 수정되었습니다.", "저장 완료");
                    
                    // 비밀번호 필드 초기화
                    tbCurrentPassword.Text = "";
                    tbNewPassword.Text = "";
                    tbNewPasswordConfirm.Text = "";
                    
                    // 정보 다시 로드
                    LoadUserInfo();
                }
                else
                {
                    MessageBoxHelper.ShowError("정보 수정에 실패했습니다.", "오류");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, "사용자 정보 수정 실패");
                MessageBoxHelper.ShowError($"정보 수정 중 오류가 발생했습니다.\n{ex.Message}", "오류");
            }
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            try
            {
                // 로그인 정보 초기화
                Globaldata.useremail = string.Empty;

                // 메뉴 레지스트리 초기화
                MenuRegistry.Clear();

                // 로그인 폼 가져오기 (기존 폼 재사용)
                var loginForm = Application.OpenForms
                    .OfType<FmLogin>()
                    .FirstOrDefault();

                if (loginForm == null)
                {
                    loginForm = new FmLogin();
                }

                loginForm.PrepareForRelogin();
                loginForm.Show();
                loginForm.BringToFront();
                loginForm.Activate();

                // 메인 폼 닫기 (확인 메시지 없이)
                if (Application.OpenForms
                        .OfType<Form>()
                        .FirstOrDefault(f => f is Main) is Main mainForm)
                {
                    mainForm.CloseForLogout();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, "로그아웃 처리 실패");
                MessageBoxHelper.ShowError($"로그아웃 중 오류가 발생했습니다.\n{ex.Message}", "오류");
            }
        }
    }
}

