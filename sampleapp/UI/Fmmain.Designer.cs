namespace sampleapp.UI
{
    partial class Fmmain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            hopeForm = new ReaLTaiizor.Forms.HopeForm();
            panelTop = new Panel();
            btnUserProfile = new ReaLTaiizor.Controls.HopeRoundButton();
            lblUserInfo = new Label();
            lblTitle = new ReaLTaiizor.Controls.BigLabel();
            panelSideMenu = new Panel();
            panelMenuFooter = new Panel();
            btnLogout = new ReaLTaiizor.Controls.HopeButton();
            btnSettings = new ReaLTaiizor.Controls.HopeButton();
            panelMenuItems = new Panel();
            bt6 = new ReaLTaiizor.Controls.HopeButton();
            bt5 = new ReaLTaiizor.Controls.HopeButton();
            bt4 = new ReaLTaiizor.Controls.HopeButton();
            bt3 = new ReaLTaiizor.Controls.HopeButton();
            bttcpsample = new ReaLTaiizor.Controls.HopeButton();
            btopcsample = new ReaLTaiizor.Controls.HopeButton();
            btnIOsample = new ReaLTaiizor.Controls.HopeButton();
            btnDBsample = new ReaLTaiizor.Controls.HopeButton();
            btnUserlist = new ReaLTaiizor.Controls.HopeButton();
            btnDashboard = new ReaLTaiizor.Controls.HopeButton();
            panelMenuHeader = new Panel();
            lblMenuTitle = new ReaLTaiizor.Controls.BigLabel();
            panelMain = new Panel();
            lblSubtitle = new Label();
            lblWelcome = new ReaLTaiizor.Controls.BigLabel();
            panelStatus = new Panel();
            lblStatus = new Label();
            lblDateTime = new Label();
            panelTop.SuspendLayout();
            panelSideMenu.SuspendLayout();
            panelMenuFooter.SuspendLayout();
            panelMenuItems.SuspendLayout();
            panelMenuHeader.SuspendLayout();
            panelMain.SuspendLayout();
            panelStatus.SuspendLayout();
            SuspendLayout();
            // 
            // hopeForm
            // 
            hopeForm.ControlBoxColorH = Color.FromArgb(228, 231, 237);
            hopeForm.ControlBoxColorHC = Color.FromArgb(245, 108, 108);
            hopeForm.ControlBoxColorN = Color.White;
            hopeForm.Dock = DockStyle.Top;
            hopeForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            hopeForm.ForeColor = Color.FromArgb(242, 246, 252);
            hopeForm.Image = null;
            hopeForm.Location = new Point(0, 0);
            hopeForm.Name = "hopeForm";
            hopeForm.Size = new Size(1493, 40);
            hopeForm.TabIndex = 0;
            hopeForm.Text = "WHToolkit 샘플";
            hopeForm.ThemeColor = Color.FromArgb(64, 158, 255);
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.BorderStyle = BorderStyle.FixedSingle;
            panelTop.Controls.Add(btnUserProfile);
            panelTop.Controls.Add(lblUserInfo);
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 40);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1493, 70);
            panelTop.TabIndex = 1;
            // 
            // btnUserProfile
            // 
            btnUserProfile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUserProfile.BorderColor = Color.FromArgb(220, 223, 230);
            btnUserProfile.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnUserProfile.Cursor = Cursors.Hand;
            btnUserProfile.DangerColor = Color.FromArgb(245, 108, 108);
            btnUserProfile.DefaultColor = Color.FromArgb(255, 255, 255);
            btnUserProfile.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnUserProfile.HoverTextColor = Color.FromArgb(255, 255, 255);
            btnUserProfile.InfoColor = Color.FromArgb(144, 147, 153);
            btnUserProfile.Location = new Point(1403, 15);
            btnUserProfile.Name = "btnUserProfile";
            btnUserProfile.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnUserProfile.Size = new Size(50, 50);
            btnUserProfile.SuccessColor = Color.FromArgb(103, 194, 58);
            btnUserProfile.TabIndex = 2;
            btnUserProfile.Text = "👤";
            btnUserProfile.TextColor = Color.White;
            btnUserProfile.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // lblUserInfo
            // 
            lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserInfo.Font = new Font("Segoe UI", 10F);
            lblUserInfo.ForeColor = Color.FromArgb(100, 100, 100);
            lblUserInfo.Location = new Point(1203, 30);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(190, 20);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "관리자 (Admin)";
            lblUserInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(64, 158, 255);
            lblTitle.Location = new Point(280, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(99, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Toolkit ";
            // 
            // panelSideMenu
            // 
            panelSideMenu.BackColor = Color.FromArgb(45, 52, 64);
            panelSideMenu.Controls.Add(panelMenuFooter);
            panelSideMenu.Controls.Add(panelMenuItems);
            panelSideMenu.Controls.Add(panelMenuHeader);
            panelSideMenu.Dock = DockStyle.Left;
            panelSideMenu.Location = new Point(0, 110);
            panelSideMenu.Name = "panelSideMenu";
            panelSideMenu.Size = new Size(260, 683);
            panelSideMenu.TabIndex = 2;
            // 
            // panelMenuFooter
            // 
            panelMenuFooter.Controls.Add(btnLogout);
            panelMenuFooter.Controls.Add(btnSettings);
            panelMenuFooter.Dock = DockStyle.Bottom;
            panelMenuFooter.Location = new Point(0, 583);
            panelMenuFooter.Name = "panelMenuFooter";
            panelMenuFooter.Size = new Size(260, 100);
            panelMenuFooter.TabIndex = 2;
            // 
            // btnLogout
            // 
            btnLogout.BorderColor = Color.FromArgb(220, 223, 230);
            btnLogout.ButtonType = ReaLTaiizor.Util.HopeButtonType.Danger;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.DangerColor = Color.FromArgb(245, 108, 108);
            btnLogout.DefaultColor = Color.FromArgb(255, 255, 255);
            btnLogout.Dock = DockStyle.Top;
            btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogout.HoverTextColor = Color.FromArgb(255, 255, 255);
            btnLogout.InfoColor = Color.FromArgb(144, 147, 153);
            btnLogout.Location = new Point(0, 45);
            btnLogout.Name = "btnLogout";
            btnLogout.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnLogout.Size = new Size(260, 45);
            btnLogout.SuccessColor = Color.FromArgb(103, 194, 58);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "🚪  로그아웃";
            btnLogout.TextColor = Color.White;
            btnLogout.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // btnSettings
            // 
            btnSettings.BorderColor = Color.FromArgb(220, 223, 230);
            btnSettings.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
            btnSettings.Cursor = Cursors.Hand;
            btnSettings.DangerColor = Color.FromArgb(245, 108, 108);
            btnSettings.DefaultColor = Color.FromArgb(55, 62, 74);
            btnSettings.Dock = DockStyle.Top;
            btnSettings.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSettings.HoverTextColor = Color.FromArgb(64, 158, 255);
            btnSettings.InfoColor = Color.FromArgb(144, 147, 153);
            btnSettings.Location = new Point(0, 0);
            btnSettings.Name = "btnSettings";
            btnSettings.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnSettings.Size = new Size(260, 45);
            btnSettings.SuccessColor = Color.FromArgb(103, 194, 58);
            btnSettings.TabIndex = 0;
            btnSettings.Text = "시스템 설정";
            btnSettings.TextColor = Color.FromArgb(200, 200, 200);
            btnSettings.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // panelMenuItems
            // 
            panelMenuItems.AutoScroll = true;
            panelMenuItems.Controls.Add(bt6);
            panelMenuItems.Controls.Add(bt5);
            panelMenuItems.Controls.Add(bt4);
            panelMenuItems.Controls.Add(bt3);
            panelMenuItems.Controls.Add(bttcpsample);
            panelMenuItems.Controls.Add(btopcsample);
            panelMenuItems.Controls.Add(btnIOsample);
            panelMenuItems.Controls.Add(btnDBsample);
            panelMenuItems.Controls.Add(btnUserlist);
            panelMenuItems.Controls.Add(btnDashboard);
            panelMenuItems.Dock = DockStyle.Fill;
            panelMenuItems.Location = new Point(0, 80);
            panelMenuItems.Name = "panelMenuItems";
            panelMenuItems.Size = new Size(260, 603);
            panelMenuItems.TabIndex = 1;
            // 
            // bt6
            // 
            bt6.BorderColor = Color.FromArgb(220, 223, 230);
            bt6.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
            bt6.Cursor = Cursors.Hand;
            bt6.DangerColor = Color.FromArgb(245, 108, 108);
            bt6.DefaultColor = Color.FromArgb(45, 52, 64);
            bt6.Dock = DockStyle.Top;
            bt6.Font = new Font("Segoe UI", 10F);
            bt6.HoverTextColor = Color.FromArgb(64, 158, 255);
            bt6.InfoColor = Color.FromArgb(144, 147, 153);
            bt6.Location = new Point(0, 450);
            bt6.Name = "bt6";
            bt6.PrimaryColor = Color.FromArgb(64, 158, 255);
            bt6.Size = new Size(260, 50);
            bt6.SuccessColor = Color.FromArgb(103, 194, 58);
            bt6.TabIndex = 9;
            bt6.Text = "미정";
            bt6.TextColor = Color.FromArgb(200, 200, 200);
            bt6.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // bt5
            // 
            bt5.BorderColor = Color.FromArgb(220, 223, 230);
            bt5.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
            bt5.Cursor = Cursors.Hand;
            bt5.DangerColor = Color.FromArgb(245, 108, 108);
            bt5.DefaultColor = Color.FromArgb(45, 52, 64);
            bt5.Dock = DockStyle.Top;
            bt5.Font = new Font("Segoe UI", 10F);
            bt5.HoverTextColor = Color.FromArgb(64, 158, 255);
            bt5.InfoColor = Color.FromArgb(144, 147, 153);
            bt5.Location = new Point(0, 400);
            bt5.Name = "bt5";
            bt5.PrimaryColor = Color.FromArgb(64, 158, 255);
            bt5.Size = new Size(260, 50);
            bt5.SuccessColor = Color.FromArgb(103, 194, 58);
            bt5.TabIndex = 8;
            bt5.Text = "미정";
            bt5.TextColor = Color.FromArgb(200, 200, 200);
            bt5.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // bt4
            // 
            bt4.BorderColor = Color.FromArgb(220, 223, 230);
            bt4.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
            bt4.Cursor = Cursors.Hand;
            bt4.DangerColor = Color.FromArgb(245, 108, 108);
            bt4.DefaultColor = Color.FromArgb(45, 52, 64);
            bt4.Dock = DockStyle.Top;
            bt4.Font = new Font("Segoe UI", 10F);
            bt4.HoverTextColor = Color.FromArgb(64, 158, 255);
            bt4.InfoColor = Color.FromArgb(144, 147, 153);
            bt4.Location = new Point(0, 350);
            bt4.Name = "bt4";
            bt4.PrimaryColor = Color.FromArgb(64, 158, 255);
            bt4.Size = new Size(260, 50);
            bt4.SuccessColor = Color.FromArgb(103, 194, 58);
            bt4.TabIndex = 7;
            bt4.Text = "미정";
            bt4.TextColor = Color.FromArgb(200, 200, 200);
            bt4.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // bt3
            // 
            bt3.BorderColor = Color.FromArgb(220, 223, 230);
            bt3.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
            bt3.Cursor = Cursors.Hand;
            bt3.DangerColor = Color.FromArgb(245, 108, 108);
            bt3.DefaultColor = Color.FromArgb(45, 52, 64);
            bt3.Dock = DockStyle.Top;
            bt3.Font = new Font("Segoe UI", 10F);
            bt3.HoverTextColor = Color.FromArgb(64, 158, 255);
            bt3.InfoColor = Color.FromArgb(144, 147, 153);
            bt3.Location = new Point(0, 300);
            bt3.Name = "bt3";
            bt3.PrimaryColor = Color.FromArgb(64, 158, 255);
            bt3.Size = new Size(260, 50);
            bt3.SuccessColor = Color.FromArgb(103, 194, 58);
            bt3.TabIndex = 6;
            bt3.Text = "미정";
            bt3.TextColor = Color.FromArgb(200, 200, 200);
            bt3.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // bttcpsample
            // 
            bttcpsample.BorderColor = Color.FromArgb(220, 223, 230);
            bttcpsample.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
            bttcpsample.Cursor = Cursors.Hand;
            bttcpsample.DangerColor = Color.FromArgb(245, 108, 108);
            bttcpsample.DefaultColor = Color.FromArgb(45, 52, 64);
            bttcpsample.Dock = DockStyle.Top;
            bttcpsample.Font = new Font("Segoe UI", 10F);
            bttcpsample.HoverTextColor = Color.FromArgb(64, 158, 255);
            bttcpsample.InfoColor = Color.FromArgb(144, 147, 153);
            bttcpsample.Location = new Point(0, 250);
            bttcpsample.Name = "bttcpsample";
            bttcpsample.PrimaryColor = Color.FromArgb(64, 158, 255);
            bttcpsample.Size = new Size(260, 50);
            bttcpsample.SuccessColor = Color.FromArgb(103, 194, 58);
            bttcpsample.TabIndex = 5;
            bttcpsample.Text = "TCP 예시";
            bttcpsample.TextColor = Color.FromArgb(200, 200, 200);
            bttcpsample.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // btopcsample
            // 
            btopcsample.BorderColor = Color.FromArgb(220, 223, 230);
            btopcsample.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
            btopcsample.Cursor = Cursors.Hand;
            btopcsample.DangerColor = Color.FromArgb(245, 108, 108);
            btopcsample.DefaultColor = Color.FromArgb(45, 52, 64);
            btopcsample.Dock = DockStyle.Top;
            btopcsample.Font = new Font("Segoe UI", 10F);
            btopcsample.HoverTextColor = Color.FromArgb(64, 158, 255);
            btopcsample.InfoColor = Color.FromArgb(144, 147, 153);
            btopcsample.Location = new Point(0, 200);
            btopcsample.Name = "btopcsample";
            btopcsample.PrimaryColor = Color.FromArgb(64, 158, 255);
            btopcsample.Size = new Size(260, 50);
            btopcsample.SuccessColor = Color.FromArgb(103, 194, 58);
            btopcsample.TabIndex = 4;
            btopcsample.Text = "OPCUA 예시";
            btopcsample.TextColor = Color.FromArgb(200, 200, 200);
            btopcsample.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // btnIOsample
            // 
            btnIOsample.BorderColor = Color.FromArgb(220, 223, 230);
            btnIOsample.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
            btnIOsample.Cursor = Cursors.Hand;
            btnIOsample.DangerColor = Color.FromArgb(245, 108, 108);
            btnIOsample.DefaultColor = Color.FromArgb(45, 52, 64);
            btnIOsample.Dock = DockStyle.Top;
            btnIOsample.Font = new Font("Segoe UI", 10F);
            btnIOsample.HoverTextColor = Color.FromArgb(64, 158, 255);
            btnIOsample.InfoColor = Color.FromArgb(144, 147, 153);
            btnIOsample.Location = new Point(0, 150);
            btnIOsample.Name = "btnIOsample";
            btnIOsample.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnIOsample.Size = new Size(260, 50);
            btnIOsample.SuccessColor = Color.FromArgb(103, 194, 58);
            btnIOsample.TabIndex = 3;
            btnIOsample.Text = "파일 예시";
            btnIOsample.TextColor = Color.FromArgb(200, 200, 200);
            btnIOsample.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // btnDBsample
            // 
            btnDBsample.BorderColor = Color.FromArgb(220, 223, 230);
            btnDBsample.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
            btnDBsample.Cursor = Cursors.Hand;
            btnDBsample.DangerColor = Color.FromArgb(245, 108, 108);
            btnDBsample.DefaultColor = Color.FromArgb(45, 52, 64);
            btnDBsample.Dock = DockStyle.Top;
            btnDBsample.Font = new Font("Segoe UI", 10F);
            btnDBsample.HoverTextColor = Color.FromArgb(64, 158, 255);
            btnDBsample.InfoColor = Color.FromArgb(144, 147, 153);
            btnDBsample.Location = new Point(0, 100);
            btnDBsample.Name = "btnDBsample";
            btnDBsample.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnDBsample.Size = new Size(260, 50);
            btnDBsample.SuccessColor = Color.FromArgb(103, 194, 58);
            btnDBsample.TabIndex = 2;
            btnDBsample.Text = "DB 예시";
            btnDBsample.TextColor = Color.FromArgb(200, 200, 200);
            btnDBsample.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // btnUserlist
            // 
            btnUserlist.BorderColor = Color.FromArgb(220, 223, 230);
            btnUserlist.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
            btnUserlist.Cursor = Cursors.Hand;
            btnUserlist.DangerColor = Color.FromArgb(245, 108, 108);
            btnUserlist.DefaultColor = Color.FromArgb(45, 52, 64);
            btnUserlist.Dock = DockStyle.Top;
            btnUserlist.Font = new Font("Segoe UI", 10F);
            btnUserlist.HoverTextColor = Color.FromArgb(64, 158, 255);
            btnUserlist.InfoColor = Color.FromArgb(144, 147, 153);
            btnUserlist.Location = new Point(0, 50);
            btnUserlist.Name = "btnUserlist";
            btnUserlist.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnUserlist.Size = new Size(260, 50);
            btnUserlist.SuccessColor = Color.FromArgb(103, 194, 58);
            btnUserlist.TabIndex = 1;
            btnUserlist.Text = "사용자 목록";
            btnUserlist.TextColor = Color.FromArgb(200, 200, 200);
            btnUserlist.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // btnDashboard
            // 
            btnDashboard.BorderColor = Color.FromArgb(220, 223, 230);
            btnDashboard.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnDashboard.Cursor = Cursors.Hand;
            btnDashboard.DangerColor = Color.FromArgb(245, 108, 108);
            btnDashboard.DefaultColor = Color.FromArgb(45, 52, 64);
            btnDashboard.Dock = DockStyle.Top;
            btnDashboard.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDashboard.HoverTextColor = Color.FromArgb(255, 255, 255);
            btnDashboard.InfoColor = Color.FromArgb(144, 147, 153);
            btnDashboard.Location = new Point(0, 0);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnDashboard.Size = new Size(260, 50);
            btnDashboard.SuccessColor = Color.FromArgb(103, 194, 58);
            btnDashboard.TabIndex = 0;
            btnDashboard.Text = "대시보드";
            btnDashboard.TextColor = Color.White;
            btnDashboard.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // panelMenuHeader
            // 
            panelMenuHeader.Controls.Add(lblMenuTitle);
            panelMenuHeader.Dock = DockStyle.Top;
            panelMenuHeader.Location = new Point(0, 0);
            panelMenuHeader.Name = "panelMenuHeader";
            panelMenuHeader.Size = new Size(260, 80);
            panelMenuHeader.TabIndex = 0;
            // 
            // lblMenuTitle
            // 
            lblMenuTitle.AutoSize = true;
            lblMenuTitle.BackColor = Color.Transparent;
            lblMenuTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblMenuTitle.ForeColor = Color.White;
            lblMenuTitle.Location = new Point(60, 25);
            lblMenuTitle.Name = "lblMenuTitle";
            lblMenuTitle.Size = new Size(144, 30);
            lblMenuTitle.TabIndex = 0;
            lblMenuTitle.Text = "📑 메인 메뉴";
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(245, 247, 250);
            panelMain.Controls.Add(lblSubtitle);
            panelMain.Controls.Add(lblWelcome);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(260, 110);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(30);
            panelMain.Size = new Size(1233, 683);
            panelMain.TabIndex = 3;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 14F);
            lblSubtitle.ForeColor = Color.FromArgb(120, 120, 120);
            lblSubtitle.Location = new Point(280, 245);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(0, 25);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Click += lblSubtitle_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.BackColor = Color.Transparent;
            lblWelcome.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(64, 158, 255);
            lblWelcome.Location = new Point(200, 180);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(0, 51);
            lblWelcome.TabIndex = 0;
            // 
            // panelStatus
            // 
            panelStatus.BackColor = Color.FromArgb(64, 158, 255);
            panelStatus.Controls.Add(lblStatus);
            panelStatus.Controls.Add(lblDateTime);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Location = new Point(0, 793);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(1493, 30);
            panelStatus.TabIndex = 4;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(270, 7);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(157, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "✓ 시스템 상태: 정상 | 연결됨";
            // 
            // lblDateTime
            // 
            lblDateTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDateTime.AutoSize = true;
            lblDateTime.Font = new Font("Segoe UI", 9F);
            lblDateTime.ForeColor = Color.White;
            lblDateTime.Location = new Point(1293, 7);
            lblDateTime.Name = "lblDateTime";
            lblDateTime.Size = new Size(157, 15);
            lblDateTime.TabIndex = 1;
            lblDateTime.Text = "2025-10-16 17:45:00 (수요일)";
            // 
            // Fmmain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1493, 823);
            Controls.Add(panelMain);
            Controls.Add(panelSideMenu);
            Controls.Add(panelStatus);
            Controls.Add(panelTop);
            Controls.Add(hopeForm);
            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = new Size(2560, 1392);
            MinimumSize = new Size(190, 40);
            Name = "Fmmain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WHToolkit MES System";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelSideMenu.ResumeLayout(false);
            panelMenuFooter.ResumeLayout(false);
            panelMenuItems.ResumeLayout(false);
            panelMenuHeader.ResumeLayout(false);
            panelMenuHeader.PerformLayout();
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            panelStatus.ResumeLayout(false);
            panelStatus.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Forms.HopeForm hopeForm;
        private Panel panelTop;
        private ReaLTaiizor.Controls.BigLabel lblTitle;
        private Label lblUserInfo;
        private ReaLTaiizor.Controls.HopeRoundButton btnUserProfile;
        private Panel panelSideMenu;
        private Panel panelMenuHeader;
        private ReaLTaiizor.Controls.BigLabel lblMenuTitle;
        private Panel panelMenuItems;
        private ReaLTaiizor.Controls.HopeButton btnDashboard;
        private ReaLTaiizor.Controls.HopeButton btnUserlist;
        private ReaLTaiizor.Controls.HopeButton btnDBsample;
        private ReaLTaiizor.Controls.HopeButton btnIOsample;
        private ReaLTaiizor.Controls.HopeButton btopcsample;
        private ReaLTaiizor.Controls.HopeButton bttcpsample;
        private ReaLTaiizor.Controls.HopeButton bt3;
        private ReaLTaiizor.Controls.HopeButton bt4;
        private ReaLTaiizor.Controls.HopeButton bt5;
        private ReaLTaiizor.Controls.HopeButton bt6;
        private Panel panelMenuFooter;
        private ReaLTaiizor.Controls.HopeButton btnSettings;
        private ReaLTaiizor.Controls.HopeButton btnLogout;
        private Panel panelMain;
        private ReaLTaiizor.Controls.BigLabel lblWelcome;
        private Label lblSubtitle;
        private Panel panelStatus;
        private Label lblStatus;
        private Label lblDateTime;
    }
}
