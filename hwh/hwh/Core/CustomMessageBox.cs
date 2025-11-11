using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using hwh.Core;

namespace hwh.Core
{
    /// <summary>
    /// 커스텀 메시지박스 타입
    /// </summary>
    public enum MessageBoxType
    {
        Info,
        Success,
        Warning,
        Error,
        Question
    }

    /// <summary>
    /// 커스텀 메시지박스 버튼 타입
    /// </summary>
    public enum MessageBoxButtons
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }

    /// <summary>
    /// 테마에 맞는 커스텀 메시지박스
    /// </summary>
    public partial class CustomMessageBox : Form
    {
        public DialogResult Result { get; private set; }

        private Panel panelTop = null!;
        private Panel panelContent = null!;
        private Panel panelButtons = null!;
        private Label lblIcon = null!;
        private Label lblTitle = null!;
        private Label lblMessage = null!;

        public CustomMessageBox(string title, string message, MessageBoxType type, MessageBoxButtons buttons)
        {
            InitializeComponents();
            SetupMessageBox(title, message, type, buttons);
        }

        private void InitializeComponents()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(500, 280);
            this.BackColor = Color.White;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            
            // 그림자 효과를 위한 드롭 쉐도우
            this.Padding = new Padding(5);

            // 상단 패널
            panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Theme.Primary,
                Padding = new Padding(20, 15, 20, 15)
            };
            panelTop.Paint += PanelTop_Paint;

            // 아이콘
            lblIcon = new Label
            {
                AutoSize = false,
                Size = new Size(40, 40),
                Location = new Point(20, 10),
                Font = new Font("Segoe UI Emoji", 20F),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Theme.TextWhite
            };

            // 제목
            lblTitle = new Label
            {
                AutoSize = false,
                Size = new Size(400, 40),
                Location = new Point(70, 10),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Theme.TextWhite
            };

            panelTop.Controls.Add(lblIcon);
            panelTop.Controls.Add(lblTitle);

            // 콘텐츠 패널
            panelContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(30, 20, 30, 20)
            };

            // 메시지
            lblMessage = new Label
            {
                Dock = DockStyle.Fill,
                Font = Theme.FontRegular,
                ForeColor = Theme.TextPrimary,
                TextAlign = ContentAlignment.TopLeft,
                AutoSize = false
            };

            panelContent.Controls.Add(lblMessage);

            // 버튼 패널
            panelButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.White,
                Padding = new Padding(30, 15, 30, 15)
            };

            this.Controls.Add(panelContent);
            this.Controls.Add(panelButtons);
            this.Controls.Add(panelTop);

            // 둥근 모서리
            this.Paint += CustomMessageBox_Paint;
        }

        private void SetupMessageBox(string title, string message, MessageBoxType type, MessageBoxButtons buttons)
        {
            lblTitle.Text = title;
            lblMessage.Text = message;

            // 타입에 따른 아이콘 및 색상
            switch (type)
            {
                case MessageBoxType.Info:
                    lblIcon.Text = "ℹ️";
                    panelTop.BackColor = Theme.Primary;
                    break;
                case MessageBoxType.Success:
                    lblIcon.Text = "✓";
                    panelTop.BackColor = Color.FromArgb(34, 197, 94); // Green
                    break;
                case MessageBoxType.Warning:
                    lblIcon.Text = "⚠️";
                    panelTop.BackColor = Color.FromArgb(245, 158, 11); // Orange
                    break;
                case MessageBoxType.Error:
                    lblIcon.Text = "✕";
                    panelTop.BackColor = Color.FromArgb(239, 68, 68); // Red
                    break;
                case MessageBoxType.Question:
                    lblIcon.Text = "❓";
                    panelTop.BackColor = Theme.Primary;
                    break;
            }

            // 버튼 생성
            CreateButtons(buttons);
        }

        private void CreateButtons(MessageBoxButtons buttons)
        {
            panelButtons.Controls.Clear();

            int buttonWidth = 100;
            int buttonHeight = 36;
            int spacing = 10;
            int rightPos = panelButtons.Width - 30;

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    var btnOk = CreateButton("확인", DialogResult.OK, rightPos - buttonWidth, buttonWidth, buttonHeight, true);
                    panelButtons.Controls.Add(btnOk);
                    break;

                case MessageBoxButtons.OKCancel:
                    var btnCancel1 = CreateButton("취소", DialogResult.Cancel, rightPos - buttonWidth, buttonWidth, buttonHeight, false);
                    var btnOk1 = CreateButton("확인", DialogResult.OK, rightPos - (buttonWidth * 2) - spacing, buttonWidth, buttonHeight, true);
                    panelButtons.Controls.Add(btnCancel1);
                    panelButtons.Controls.Add(btnOk1);
                    break;

                case MessageBoxButtons.YesNo:
                    var btnNo = CreateButton("아니오", DialogResult.No, rightPos - buttonWidth, buttonWidth, buttonHeight, false);
                    var btnYes = CreateButton("예", DialogResult.Yes, rightPos - (buttonWidth * 2) - spacing, buttonWidth, buttonHeight, true);
                    panelButtons.Controls.Add(btnNo);
                    panelButtons.Controls.Add(btnYes);
                    break;

                case MessageBoxButtons.YesNoCancel:
                    var btnCancel2 = CreateButton("취소", DialogResult.Cancel, rightPos - buttonWidth, buttonWidth, buttonHeight, false);
                    var btnNo2 = CreateButton("아니오", DialogResult.No, rightPos - (buttonWidth * 2) - spacing, buttonWidth, buttonHeight, false);
                    var btnYes2 = CreateButton("예", DialogResult.Yes, rightPos - (buttonWidth * 3) - (spacing * 2), buttonWidth, buttonHeight, true);
                    panelButtons.Controls.Add(btnCancel2);
                    panelButtons.Controls.Add(btnNo2);
                    panelButtons.Controls.Add(btnYes2);
                    break;
            }
        }

        private Button CreateButton(string text, DialogResult result, int x, int width, int height, bool isPrimary)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(width, height),
                Location = new Point(x, 10),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TabStop = true
            };

            if (isPrimary)
            {
                button.BackColor = Theme.Primary;
                button.ForeColor = Theme.TextWhite;
                button.FlatAppearance.BorderSize = 0;
            }
            else
            {
                button.BackColor = Color.Transparent;
                button.ForeColor = Theme.TextSecondary;
                button.FlatAppearance.BorderColor = Theme.BorderLight;
                button.FlatAppearance.BorderSize = 1;
            }

            button.FlatAppearance.MouseOverBackColor = isPrimary ? Theme.PrimaryHover : Theme.ContentHover;

            button.Click += (s, e) =>
            {
                Result = result;
                this.Close();
            };

            // 둥근 모서리
            button.Paint += (s, e) =>
            {
                using (var path = GetRoundedRectanglePath(button.ClientRectangle, 6))
                {
                    button.Region = new Region(path);
                }
            };

            return button;
        }

        private void PanelTop_Paint(object? sender, PaintEventArgs e)
        {
            // 상단 패널 그림자
            using (var brush = new LinearGradientBrush(
                new Rectangle(0, panelTop.Height - 3, panelTop.Width, 3),
                Color.FromArgb(50, 0, 0, 0),
                Color.Transparent,
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, 0, panelTop.Height - 3, panelTop.Width, 3);
            }
        }

        private void CustomMessageBox_Paint(object? sender, PaintEventArgs e)
        {
            // 둥근 모서리 및 그림자
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // 메인 배경
            using (var path = GetRoundedRectanglePath(this.ClientRectangle, 10))
            using (var brush = new SolidBrush(Color.White))
            {
                this.Region = new Region(path);
                e.Graphics.FillPath(brush, path);
            }

            // 진한 외곽 테두리 (3px)
            using (var path = GetRoundedRectanglePath(new Rectangle(1, 1, this.Width - 3, this.Height - 3), 10))
            using (var pen = new Pen(Theme.Primary, 3))
            {
                e.Graphics.DrawPath(pen, path);
            }

            // 안쪽 강조 테두리
            using (var path = GetRoundedRectanglePath(new Rectangle(4, 4, this.Width - 9, this.Height - 9), 8))
            using (var pen = new Pen(Color.FromArgb(50, Theme.Primary.R, Theme.Primary.G, Theme.Primary.B), 1))
            {
                e.Graphics.DrawPath(pen, path);
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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
                Result = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}

