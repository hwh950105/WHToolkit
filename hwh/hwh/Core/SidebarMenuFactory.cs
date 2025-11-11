using System;
using System.Drawing;
using System.Windows.Forms;
using hwh.Models;

namespace hwh.Core
{
    /// <summary>
    /// 사이드바 메뉴 버튼 생성 팩토리
    /// </summary>
    public static class SidebarMenuFactory
    {
        /// <summary>
        /// 메뉴 버튼 패널 생성
        /// </summary>
        public static Panel CreateMenuButton(MenuItem menu, Action<MenuItem, Panel> onClickHandler)
        {
            var panel = new Panel
            {
                Size = new Size(256, 44),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Tag = menu
            };

            // 선택 인디케이터 (왼쪽 바)
            var indicator = new Panel
            {
                Width = 3,
                Height = 44,
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Name = "indicator"
            };

            // 아이콘 라벨
            var iconLabel = new Label
            {
                Text = menu.Icon,
                Font = new Font("Segoe UI Emoji", 20F, FontStyle.Regular),
                ForeColor = Theme.TextLight,
                AutoSize = false,
                Size = new Size(52, 44),
                Location = new Point(0, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                Name = "icon"
            };

            // 메뉴 라벨
            var label = new Label
            {
                Text = menu.MenuName,
                Font = Theme.FontMenu,
                ForeColor = Theme.TextLight,
                AutoSize = false,
                Size = new Size(200, 44),
                Location = new Point(52, 0),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand,
                Name = "label"
            };

            panel.Controls.Add(indicator);
            panel.Controls.Add(iconLabel);
            panel.Controls.Add(label);

            // 이벤트 핸들러
            panel.Click += (s, e) => onClickHandler(menu, panel);
            iconLabel.Click += (s, e) => onClickHandler(menu, panel);
            label.Click += (s, e) => onClickHandler(menu, panel);
            
            // 호버 효과
            panel.MouseEnter += (s, e) =>
            {
                if (panel.BackColor != Theme.Primary)
                {
                    panel.BackColor = Theme.SidebarHover;
                    iconLabel.ForeColor = Theme.TextWhite;
                    label.ForeColor = Theme.TextWhite;
                }
            };
            
            panel.MouseLeave += (s, e) =>
            {
                if (panel.BackColor != Theme.Primary)
                {
                    panel.BackColor = Color.Transparent;
                    iconLabel.ForeColor = Theme.TextLight;
                    label.ForeColor = Theme.TextLight;
                }
            };

            return panel;
        }

        /// <summary>
        /// 메뉴 스타일 설정
        /// </summary>
        public static void SetActiveMenuStyle(Panel activePanel, Panel menuContainer)
        {
            // 모든 메뉴 초기화
            foreach (Control control in menuContainer.Controls)
            {
                if (control is Panel p && p.Tag is MenuItem)
                {
                    p.BackColor = Color.Transparent;
                    
                    foreach (Control child in p.Controls)
                    {
                        if (child.Name == "indicator")
                        {
                            child.BackColor = Color.Transparent;
                        }
                        else if (child is Label lbl)
                        {
                            if (child.Name == "icon")
                            {
                                lbl.ForeColor = Theme.TextLight;
                            }
                            else if (child.Name == "label")
                            {
                                lbl.ForeColor = Theme.TextLight;
                                lbl.Font = Theme.FontMenu;
                            }
                        }
                    }
                }
            }
            
            // 선택된 메뉴 활성화
            activePanel.BackColor = Theme.Primary;
            
            foreach (Control child in activePanel.Controls)
            {
                if (child.Name == "indicator")
                {
                    child.BackColor = Theme.Accent;
                }
                else if (child is Label lbl)
                {
                    if (child.Name == "icon")
                    {
                        lbl.ForeColor = Theme.TextWhite;
                    }
                    else if (child.Name == "label")
                    {
                        lbl.ForeColor = Theme.TextWhite;
                        lbl.Font = Theme.FontMenuBold;
                    }
                }
            }
        }

        /// <summary>
        /// 사이드바 헤더 생성
        /// </summary>
        public static Panel CreateSidebarHeader(string title)
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Theme.SidebarBg,
                Padding = new Padding(Theme.SpacingXL, Theme.SpacingL, Theme.SpacingXL, Theme.SpacingL)
            };

            var brandLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Theme.TextWhite,
                AutoSize = true,
                Location = new Point(Theme.SpacingXL, 28)
            };

            header.Controls.Add(brandLabel);

            return header;
        }

        /// <summary>
        /// 사이드바 토글 버튼 생성
        /// </summary>
        public static Button CreateToggleButton(EventHandler clickHandler)
        {
            var toggleButton = new Button
            {
                Size = new Size(40, 40),
                Location = new Point(10, 0),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Theme.TextWhite,
                Font = new Font("Segoe UI", 18F, FontStyle.Regular),
                Text = "☰",
                Cursor = Cursors.Hand,
                TabStop = false
            };
            
            toggleButton.FlatAppearance.BorderSize = 0;
            toggleButton.FlatAppearance.MouseOverBackColor = Theme.SidebarHover;
            toggleButton.Click += clickHandler;

            return toggleButton;
        }
    }
}

