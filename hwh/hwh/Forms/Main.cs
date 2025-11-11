using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using hwh.Core;
using hwh.Models;
using hwh.Services;

namespace hwh.Forms
{
    public partial class Main : Form
    {
        private TabManager? _tabManager;
        private Panel? _sidebarHeader;
        private Panel? _sidebarMenuContainer;
        private Panel? _sidebarBottomContainer;
        private Button? _toggleButton;
        private System.Windows.Forms.Timer? _sidebarAnimationTimer;
        
        private bool _isSidebarExpanded = true;
        private bool _isLogoutClosing = false;
        private const int SIDEBAR_EXPANDED_WIDTH = 280;
        private const int SIDEBAR_COLLAPSED_WIDTH = 60;
        private const int ANIMATION_STEP = 30;

        public Main()
        {
            InitializeComponent();
            InitializeSidebarToggle();
            InitializeSidebar();
            InitializeMenus();
            InitializeFormClosing();
        }

        private void InitializeFormClosing()
        {
            // 폼 종료 시 확인 메시지
            this.FormClosing += Main_FormClosing;
        }

        private void Main_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_isLogoutClosing)
            {
                return;
            }

            // 사용자가 X 버튼을 클릭한 경우만 확인
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBoxHelper.ShowQuestion(
                    "프로그램을 종료하시겠습니까?",
                    "종료 확인"
                );

                if (result != DialogResult.Yes)
                {
                    e.Cancel = true; // 종료 취소
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        public void CloseForLogout()
        {
            _isLogoutClosing = true;
            Close();
        }

        private void InitializeSidebarToggle()
        {
            // 토글 버튼 생성 (팩토리 패턴)
            _toggleButton = SidebarMenuFactory.CreateToggleButton(ToggleButton_Click);
            
            hopeForm1.Controls.Add(_toggleButton);
            _toggleButton.BringToFront();

            // 애니메이션 타이머 초기화
            _sidebarAnimationTimer = new System.Windows.Forms.Timer
            {
                Interval = 32
            };
            _sidebarAnimationTimer.Tick += SidebarAnimationTimer_Tick;
        }

        private void InitializeSidebar()
        {
            // 사이드바 헤더 생성
            _sidebarHeader = SidebarMenuFactory.CreateSidebarHeader("Dashboard");

            // 하단 메뉴 컨테이너
            _sidebarBottomContainer = new Panel
            {
                Dock = DockStyle.Bottom,
                BackColor = Theme.SidebarBg,
                AutoSize = true,
                Padding = new Padding(Theme.SpacingM, 0, Theme.SpacingM, Theme.SpacingM)
            };

            // 일반 메뉴 컨테이너
            _sidebarMenuContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Theme.SidebarBg,
                AutoScroll = true,
                Padding = new Padding(Theme.SpacingM, Theme.SpacingS, Theme.SpacingM, Theme.SpacingL)
            };

            panelSidebar.Controls.Add(_sidebarMenuContainer);
            panelSidebar.Controls.Add(_sidebarBottomContainer);
            panelSidebar.Controls.Add(_sidebarHeader);
        }

        private void InitializeMenus()
        {
            // 기본 메뉴 등록
            MenuRegistry.RegisterDefaultMenus();

            // 탭 매니저 초기화
            _tabManager = new TabManager(panelTabBar, panelContent);

            // 사이드바 메뉴 버튼 생성
            CreateSidebarMenus();
            
            if (_tabManager == null)
            {
                throw new InvalidOperationException("TabManager가 초기화되지 않았습니다.");
            }
        }

        private void PanelTabBar_Paint(object? sender, PaintEventArgs e)
        {
            // 탭 바 하단 그림자 효과
            using (var shadowBrush = new LinearGradientBrush(
                new Rectangle(0, panelTabBar.Height - 3, panelTabBar.Width, 3),
                Theme.ShadowLight,
                Color.Transparent,
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(shadowBrush, 0, panelTabBar.Height - 3, panelTabBar.Width, 3);
            }
        }

        private void PanelSidebar_Paint(object? sender, PaintEventArgs e)
        {
            // 사이드바 오른쪽 구분선
            using (var pen = new Pen(Theme.BorderDark, 1))
            {
                e.Graphics.DrawLine(pen, panelSidebar.Width - 1, 0, panelSidebar.Width - 1, panelSidebar.Height);
            }
        }

        private void CreateSidebarMenus()
        {
            if (_sidebarMenuContainer == null || _sidebarBottomContainer == null) return;

            // 일반 메뉴 생성
            var normalMenus = MenuRegistry.GetNormalMenus().ToList();
            int yPos = 0;
            
            foreach (var menu in normalMenus)
            {
                var menuPanel = SidebarMenuFactory.CreateMenuButton(menu, OnMenuClick);
                menuPanel.Location = new Point(0, yPos);
                _sidebarMenuContainer.Controls.Add(menuPanel);
                yPos += menuPanel.Height + Theme.SpacingXS;
            }

            // 하단 고정 메뉴 생성
            var bottomMenus = MenuRegistry.GetBottomMenus().ToList();
            yPos = 0;

            foreach (var menu in bottomMenus)
            {
                var menuPanel = SidebarMenuFactory.CreateMenuButton(menu, OnMenuClick);
                menuPanel.Location = new Point(0, yPos);
                _sidebarBottomContainer.Controls.Add(menuPanel);
                yPos += menuPanel.Height + Theme.SpacingXS;
            }
        }

        private void OnMenuClick(MenuItem menu, Panel panel)
        {
            if (_tabManager != null)
            {
                _tabManager.OpenOrActivateTab(menu);
                SetActiveMenuStyle(panel);
            }
        }

        private void SetActiveMenuStyle(Panel activePanel)
        {
            if (_sidebarMenuContainer == null || _sidebarBottomContainer == null) return;
            
            // 일반 메뉴와 하단 메뉴 모두에서 스타일 업데이트
            SidebarMenuFactory.SetActiveMenuStyle(activePanel, _sidebarMenuContainer);
            SidebarMenuFactory.SetActiveMenuStyle(activePanel, _sidebarBottomContainer);
        }

        private void ToggleButton_Click(object? sender, EventArgs e)
        {
            if (_sidebarAnimationTimer != null && !_sidebarAnimationTimer.Enabled)
            {
                _sidebarAnimationTimer.Start();
            }
        }

        private void SidebarAnimationTimer_Tick(object? sender, EventArgs e)
        {
            // 레이아웃 업데이트 중단 (성능 최적화)
            panelContent.SuspendLayout();
            
            if (_isSidebarExpanded)
            {
                // 축소 애니메이션
                if (panelSidebar.Width > SIDEBAR_COLLAPSED_WIDTH)
                {
                    panelSidebar.Width -= ANIMATION_STEP;
                    if (panelSidebar.Width <= SIDEBAR_COLLAPSED_WIDTH)
                    {
                        panelSidebar.Width = SIDEBAR_COLLAPSED_WIDTH;
                        _isSidebarExpanded = false;
                        _sidebarAnimationTimer?.Stop();
                        UpdateSidebarContent();
                        panelContent.ResumeLayout(true);  // 완료 시 레이아웃 재개
                        return;
                    }
                }
            }
            else
            {
                // 확장 애니메이션
                if (panelSidebar.Width < SIDEBAR_EXPANDED_WIDTH)
                {
                    panelSidebar.Width += ANIMATION_STEP;
                    if (panelSidebar.Width >= SIDEBAR_EXPANDED_WIDTH)
                    {
                        panelSidebar.Width = SIDEBAR_EXPANDED_WIDTH;
                        _isSidebarExpanded = true;
                        _sidebarAnimationTimer?.Stop();
                        UpdateSidebarContent();
                        panelContent.ResumeLayout(true);  // 완료 시 레이아웃 재개
                        return;
                    }
                }
            }
            
            // 애니메이션 진행 중에는 레이아웃 업데이트 안 함
            panelContent.ResumeLayout(false);
        }

        private void UpdateSidebarContent()
        {
            if (_sidebarHeader == null || _sidebarMenuContainer == null || _sidebarBottomContainer == null) return;

            // 헤더 텍스트 표시/숨김
            foreach (Control control in _sidebarHeader.Controls)
            {
                if (control is Label)
                {
                    control.Visible = _isSidebarExpanded;
                }
            }

            // 일반 메뉴와 하단 메뉴 모두 업데이트
            UpdateMenuContainerContent(_sidebarMenuContainer);
            UpdateMenuContainerContent(_sidebarBottomContainer);
        }

        private void UpdateMenuContainerContent(Panel container)
        {
            // 메뉴 텍스트 및 아이콘 위치 조정
            foreach (Control control in container.Controls)
            {
                if (control is Panel menuPanel)
                {
                    foreach (Control child in menuPanel.Controls)
                    {
                        if (child is Label label)
                        {
                            if (child.Name == "label")
                            {
                                // 텍스트는 접었을 때 숨김
                                label.Visible = _isSidebarExpanded;
                            }
                            else if (child.Name == "icon")
                            {
                                // 아이콘은 항상 표시, 크기 조정
                                if (_isSidebarExpanded)
                                {
                                    label.Size = new Size(52, 44);
                                    label.Location = new Point(0, 0);
                                }
                                else
                                {
                                    label.Size = new Size(60, 44);
                                    label.Location = new Point(0, 0);
                                }
                                label.TextAlign = ContentAlignment.MiddleCenter;
                            }
                        }
                    }
                    
                    // 패널 너비 조정
                    menuPanel.Width = _isSidebarExpanded ? 256 : 60;
                }
            }
        }
    }
}
