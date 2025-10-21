using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sampleapp.UI.UserControls;

namespace sampleapp.UI
{
    public partial class Fmmain : Form
    {
        // 페이지 캐싱을 위한 Dictionary (확장성 고려)
        private readonly Dictionary<string, UserControl> _pages = new Dictionary<string, UserControl>();
        private UserControl? _currentPage;
        private ReaLTaiizor.Controls.HopeButton? _currentButton;

        // 타이머
        private System.Windows.Forms.Timer? _statusTimer;

        // 사용자 정보
        private string _userName = "관리자";
        private string _userRole = "Admin";

        public Fmmain()
        {
            InitializeComponent();
            InitializeNavigation();
            InitializeMainFeatures();
        }

        /// <summary>
        /// 메인 화면 기능 초기화
        /// </summary>
        private void InitializeMainFeatures()
        {
            // 사용자 정보 표시
            UpdateUserInfo();
            // 사용자 프로필 버튼 이벤트
            btnUserProfile.Click += BtnUserProfile_Click;
            // 설정 버튼 이벤트
            btnSettings.Click += BtnSettings_Click;
            // 로그아웃 버튼 이벤트
            btnLogout.Click += BtnLogout_Click;
            // 상태바 타이머 시작
            InitializeStatusTimer();
        }
        /// <summary>
        /// 네비게이션 초기화 - 이벤트 핸들러 등록
        /// </summary>
        private void InitializeNavigation()
        {
            // 메뉴 버튼 이벤트 등록
            btnDashboard.Click += (s, e) => NavigateToPage("Dashboard", btnDashboard);
            btnUserlist.Click += (s, e) => NavigateToPage("userlist", btnUserlist);
            btnDBsample.Click += (s, e) => NavigateToPage("DBsample", btnDBsample);
            btnIOsample.Click += (s, e) => NavigateToPage("IOsample", btnIOsample);

            btopcsample.Click += (s, e) => NavigateToPage("opcsample", btopcsample);
            bttcpsample.Click += (s, e) => NavigateToPage("tcpsample", bttcpsample);
            bt3.Click += (s, e) => NavigateToPage("bt3", bt3);

            bt4.Click += (s, e) => NavigateToPage("bt4", bt4);
            bt5.Click += (s, e) => NavigateToPage("bt5", bt5);
            bt6.Click += (s, e) => NavigateToPage("bt6", bt6);

            // 초기 페이지 로드
            NavigateToPage("Dashboard", btnDashboard);
        }

        /// <summary>
        /// 페이지 이동 로직 - 확장성을 위한 팩토리 패턴
        /// </summary>
        private void NavigateToPage(string pageName, ReaLTaiizor.Controls.HopeButton button)
        {
            try
            {
                 // 이전 버튼 스타일 초기화
                 if (_currentButton != null && _currentButton != button)
                {
                    ResetButtonStyle(_currentButton);
                }

                // 현재 버튼 활성화
                SetActiveButtonStyle(button);
                _currentButton = button;

                // 페이지 캐시 확인 및 생성
                if (!_pages.ContainsKey(pageName))
                {
                    UserControl? newPage = CreatePage(pageName);
                    if (newPage != null)
                    {
                        _pages[pageName] = newPage;
                    }
                    else
                    {
                        MessageBox.Show($"{pageName} 페이지는 아직 구현되지 않았습니다.", "알림",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                // 페이지 전환
                UserControl targetPage = _pages[pageName];
                if (_currentPage != targetPage)
                {
                    // 이전 페이지 제거
                    if (_currentPage != null)
                    {
                        panelMain.Controls.Remove(_currentPage);
                    }

                    // 새 페이지 추가
                    _currentPage = targetPage;
                    _currentPage.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(_currentPage);
                    _currentPage.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"페이지 로드 중 오류: {ex.Message}", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 페이지 팩토리 메서드 - 새 페이지 추가 시 여기에 케이스 추가
        /// </summary>
        private UserControl? CreatePage(string pageName)
        {
            return pageName switch
            {
                "Dashboard" => new DashboardControl(),
                "userlist" => new UserlistControl(),
                "DBsample" => new DBControl(),
                "IOsample" => new IOControl(),
                "opcsample" => new OpcControl(),
                "tcpsample" => new TcpControl(),
                "bt3" => null,
                "bt4" => null,
                "bt5" => null,
                "bt6" => null,
                _ => null
            };
        }

        /// <summary>
        /// 버튼 스타일 초기화 (비활성 상태)
        /// </summary>
        private void ResetButtonStyle(ReaLTaiizor.Controls.HopeButton button)
        {
            button.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
            button.TextColor = Color.FromArgb(200, 200, 200);
        }

        /// <summary>
        /// 버튼 활성화 스타일 설정
        /// </summary>
        private void SetActiveButtonStyle(ReaLTaiizor.Controls.HopeButton button)
        {
            button.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            button.TextColor = Color.White;
        }

        /// <summary>
        /// 현재 페이지 새로고침 (필요 시 사용)
        /// </summary>
        public void RefreshCurrentPage()
        {
            _currentPage?.Refresh();
        }

        /// <summary>
        /// 특정 페이지 캐시 제거 (데이터 갱신 필요 시)
        /// </summary>
        public void ClearPageCache(string pageName)
        {
            if (_pages.ContainsKey(pageName))
            {
                _pages[pageName]?.Dispose();
                _pages.Remove(pageName);
            }
        }

        /// <summary>
        /// 사용자 정보 업데이트
        /// </summary>
        private void UpdateUserInfo()
        {
            lblUserInfo.Text = $"{_userName} ({_userRole})";
        }

        /// <summary>
        /// 사용자 프로필 버튼 클릭
        /// </summary>
        private void BtnUserProfile_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show(
                $"사용자: {_userName}\n" +
                $"권한: {_userRole}\n" +
                $"로그인 시간: {DateTime.Now:yyyy-MM-dd HH:mm}\n\n" +
                "프로필을 수정하시겠습니까?",
                "사용자 프로필",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("프로필 편집 기능은 추후 구현 예정입니다.", "알림",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 시스템 설정 버튼 클릭
        /// </summary>
        private void BtnSettings_Click(object? sender, EventArgs e)
        {
            var settingsForm = new Form
            {
                Text = "시스템 설정",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F)
            };

            // 일반 설정 탭
            var tabGeneral = new TabPage("일반");
            var lblGeneral = new Label
            {
                Text = "• 시스템 언어: 한국어\n• 테마: 라이트 모드\n• 자동 저장: 활성화\n• 세션 유지 시간: 30분",
                Location = new Point(20, 20),
                Size = new Size(500, 200),
                Font = new Font("Segoe UI", 11F)
            };
            tabGeneral.Controls.Add(lblGeneral);

            // 알림 설정 탭
            var tabNotifications = new TabPage("알림");
            var lblNotifications = new Label
            {
                Text = "• 생산 완료 알림: 활성화\n• 품질 이상 알림: 활성화\n• 재고 부족 알림: 활성화\n• 설비 오류 알림: 활성화",
                Location = new Point(20, 20),
                Size = new Size(500, 200),
                Font = new Font("Segoe UI", 11F)
            };
            tabNotifications.Controls.Add(lblNotifications);

            // 보안 설정 탭
            var tabSecurity = new TabPage("보안");
            var lblSecurity = new Label
            {
                Text = "• 비밀번호 변경 주기: 90일\n• 2단계 인증: 비활성화\n• 로그인 세션 타임아웃: 30분\n• 접근 로그 기록: 활성화",
                Location = new Point(20, 20),
                Size = new Size(500, 200),
                Font = new Font("Segoe UI", 11F)
            };
            tabSecurity.Controls.Add(lblSecurity);

            tabControl.TabPages.Add(tabGeneral);
            tabControl.TabPages.Add(tabNotifications);
            tabControl.TabPages.Add(tabSecurity);

            settingsForm.Controls.Add(tabControl);
            settingsForm.ShowDialog(this);
        }

        /// <summary>
        /// 로그아웃 버튼 클릭
        /// </summary>
        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "로그아웃 하시겠습니까?",
                "로그아웃 확인",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 타이머 정지
                _statusTimer?.Stop();
                _statusTimer?.Dispose();

                // 페이지 캐시 정리
                foreach (var page in _pages.Values)
                {
                    page?.Dispose();
                }
                _pages.Clear();

                // 로그인 폼으로 이동
                this.Hide();
                var loginForm = new Login();
                loginForm.FormClosed += (s, args) => this.Close();
                loginForm.Show();
            }
        }

        /// <summary>
        /// 상태바 타이머 초기화
        /// </summary>
        private void InitializeStatusTimer()
        {
            _statusTimer = new System.Windows.Forms.Timer();
            _statusTimer.Interval = 1000; // 1초마다 업데이트
            _statusTimer.Tick += StatusTimer_Tick;
            _statusTimer.Start();

            // 초기 상태 설정
            UpdateStatusBar();
        }

        /// <summary>
        /// 상태바 타이머 틱 이벤트
        /// </summary>
        private void StatusTimer_Tick(object? sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        /// <summary>
        /// 상태바 정보 업데이트
        /// </summary>
        private void UpdateStatusBar()
        {
            // 현재 시간 표시
            lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // 시스템 상태 표시
            var uptime = DateTime.Now - Process.GetCurrentProcess().StartTime;
            lblStatus.Text = $"시스템 정상 | 가동 시간: {uptime.Hours:00}:{uptime.Minutes:00}:{uptime.Seconds:00} | 연결: 정상";
        }

        /// <summary>
        /// 폼 종료 시 리소스 정리
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // 타이머 정리
            _statusTimer?.Stop();
            _statusTimer?.Dispose();

            // 페이지 캐시 정리
            foreach (var page in _pages.Values)
            {
                page?.Dispose();
            }
            _pages.Clear();
        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {

        }
    }
}
