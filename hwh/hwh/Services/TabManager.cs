using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using hwh.Models;
using hwh.Core;

namespace hwh.Services
{
    /// <summary>
    /// 탭 정보를 담는 클래스
    /// </summary>
    public class TabInfo
    {
        public string MenuName { get; set; } = string.Empty;
        public string TabId { get; set; } = string.Empty;
        public Panel TabPanel { get; set; } = null!;
        public Label TabLabel { get; set; } = null!;
        public Button CloseButton { get; set; } = null!;
        public UserControl ContentControl { get; set; } = null!;
        public MenuItem MenuItem { get; set; } = null!;
    }

    /// <summary>
    /// 탭 관리 서비스 클래스
    /// </summary>
    public class TabManager
    {
        private readonly Panel _tabBar;
        private readonly Panel _contentArea;
        private readonly List<TabInfo> _tabs = new List<TabInfo>();
        private TabInfo? _activeTab = null;

        public TabManager(Panel tabBar, Panel contentArea)
        {
            _tabBar = tabBar;
            _contentArea = contentArea;
        }

        /// <summary>
        /// 탭을 열거나 활성화합니다.
        /// </summary>
        public void OpenOrActivateTab(MenuItem menuItem)
        {
            // 이미 존재하는 탭인지 확인
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == menuItem.TabId);

            if (existingTab != null)
            {
                // 이미 존재하면 해당 탭 활성화
                ActivateTab(existingTab);
            }
            else
            {
                // 새 탭 추가
                AddTab(menuItem);
            }
        }

        /// <summary>
        /// 새 탭을 추가합니다.
        /// </summary>
        private void AddTab(MenuItem menuItem)
        {
            // UserControl 생성
            var control = menuItem.ControlFactory();

            // 탭 패널 생성
            var tabPanel = new Panel
            {
                Size = new Size(200, 44),
                BackColor = Theme.TabInactive,
                Location = new Point(_tabs.Count * 205 + 8, 6),
                Padding = new Padding(0),
                Margin = new Padding(0),
                Cursor = Cursors.Hand
            };

            // 탭 패널 보더 그리기
            tabPanel.Paint += (s, e) =>
            {
                // 둥근 모서리 효과
                var rect = new Rectangle(0, 0, tabPanel.Width - 1, tabPanel.Height - 1);
                using (var path = GetRoundedRectPath(rect, 8))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    
                    // 배경
                    using (var brush = new SolidBrush(tabPanel.BackColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                    
                    // 테두리
                    using (var pen = new Pen(Theme.Border, 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            // 탭 라벨 생성
            var tabLabel = new Label
            {
                Text = menuItem.MenuName,
                Font = Theme.FontBody,
                ForeColor = Theme.TextSecondary,
                AutoSize = false,
                Size = new Size(165, 44),
                Location = new Point(12, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };

            // 닫기 버튼 생성
            var closeButton = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Theme.TextSecondary,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(26, 26),
                Location = new Point(168, 9),
                Cursor = Cursors.Hand,
                TabStop = false
            };

            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseOverBackColor = Color.Transparent;

            var tabInfo = new TabInfo
            {
                MenuName = menuItem.MenuName,
                TabId = menuItem.TabId,
                TabPanel = tabPanel,
                TabLabel = tabLabel,
                CloseButton = closeButton,
                ContentControl = control,
                MenuItem = menuItem
            };

            // 이벤트 핸들러
            tabPanel.Click += (s, e) => ActivateTab(tabInfo);
            tabLabel.Click += (s, e) => ActivateTab(tabInfo);
            closeButton.Click += (s, e) =>
            {
                CloseTab(tabInfo.TabId);
            };

            // 마우스 호버 효과
            tabPanel.MouseEnter += (s, e) =>
            {
                if (_activeTab != tabInfo)
                {
                    tabPanel.BackColor = Theme.TabHover;
                    tabLabel.ForeColor = Theme.TextPrimary;
                }
            };
            tabPanel.MouseLeave += (s, e) =>
            {
                if (_activeTab != tabInfo)
                {
                    tabPanel.BackColor = Theme.TabInactive;
                    tabLabel.ForeColor = Theme.TextSecondary;
                }
            };

            closeButton.MouseEnter += (s, e) =>
            {
                closeButton.ForeColor = Theme.Error;
            };
            closeButton.MouseLeave += (s, e) =>
            {
                closeButton.ForeColor = Theme.TextSecondary;
            };

            // 패널에 컨트롤 추가
            tabPanel.Controls.Add(tabLabel);
            tabPanel.Controls.Add(closeButton);

            _tabs.Add(tabInfo);

            // 탭 패널을 탭 바에 추가
            _tabBar.Controls.Add(tabPanel);

            // 콘텐츠 영역에 추가
            control.Dock = DockStyle.Fill;
            _contentArea.Controls.Add(control);
            control.BringToFront();

            // 새 탭 활성화
            ActivateTab(tabInfo);
        }

        /// <summary>
        /// 둥근 사각형 경로 생성
        /// </summary>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
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

        /// <summary>
        /// 탭을 활성화합니다.
        /// </summary>
        private void ActivateTab(TabInfo tab)
        {
            if (_activeTab == tab) return;

            // 모든 탭 비활성화 스타일
            foreach (var t in _tabs)
            {
                t.TabPanel.BackColor = Theme.TabInactive;
                t.TabLabel.ForeColor = Theme.TextSecondary;
                t.TabLabel.Font = Theme.FontBody;
                t.CloseButton.ForeColor = Theme.TextSecondary;
                t.ContentControl.Visible = false;
                t.TabPanel.Invalidate(); // 다시 그리기
            }

            // 선택된 탭 활성화 스타일
            tab.TabPanel.BackColor = Theme.TabActive;
            tab.TabLabel.ForeColor = Theme.Primary;
            tab.TabLabel.Font = Theme.FontBodyBold;
            tab.CloseButton.ForeColor = Theme.TextSecondary;
            tab.ContentControl.Visible = true;
            tab.ContentControl.BringToFront();
            tab.TabPanel.Invalidate(); // 다시 그리기

            _activeTab = tab;
        }

        /// <summary>
        /// 모든 탭을 닫습니다.
        /// </summary>
        public void CloseAllTabs()
        {
            foreach (var tab in _tabs)
            {
                _tabBar.Controls.Remove(tab.TabPanel);
                _contentArea.Controls.Remove(tab.ContentControl);
                tab.TabPanel.Dispose();
                tab.ContentControl.Dispose();
            }
            _tabs.Clear();
            _activeTab = null;
        }

        /// <summary>
        /// 특정 탭을 닫습니다.
        /// </summary>
        public void CloseTab(string tabId)
        {
            var tab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (tab == null) return;

            _tabBar.Controls.Remove(tab.TabPanel);
            _contentArea.Controls.Remove(tab.ContentControl);
            tab.TabPanel.Dispose();
            tab.ContentControl.Dispose();
            _tabs.Remove(tab);

            // 닫은 탭이 활성 탭이었다면 다른 탭 활성화
            if (_activeTab == tab)
            {
                _activeTab = null;
                if (_tabs.Count > 0)
                {
                    ActivateTab(_tabs.Last());
                }
            }

            // 탭 버튼 위치 재조정
            ReorderTabButtons();
        }

        /// <summary>
        /// 탭 버튼 위치를 재조정합니다.
        /// </summary>
        private void ReorderTabButtons()
        {
            for (int i = 0; i < _tabs.Count; i++)
            {
                _tabs[i].TabPanel.Location = new Point(i * 205 + 8, 6);
            }
        }
    }
}
