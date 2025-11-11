namespace hwh.Controls
{
    partial class UserInfoControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle1 = new ReaLTaiizor.Controls.MaterialLabel();
            lblUsernameTitle = new ReaLTaiizor.Controls.MaterialLabel();
            lblUsername = new ReaLTaiizor.Controls.MaterialLabel();
            materialLabel1 = new ReaLTaiizor.Controls.MaterialLabel();
            materialLabel2 = new ReaLTaiizor.Controls.MaterialLabel();
            materialLabel3 = new ReaLTaiizor.Controls.MaterialLabel();
            lblPasswordTitle = new ReaLTaiizor.Controls.MaterialLabel();
            tbName = new ReaLTaiizor.Controls.HopeTextBox();
            tbEmail = new ReaLTaiizor.Controls.HopeTextBox();
            tbPhone = new ReaLTaiizor.Controls.HopeTextBox();
            tbCurrentPassword = new ReaLTaiizor.Controls.HopeTextBox();
            tbNewPassword = new ReaLTaiizor.Controls.HopeTextBox();
            tbNewPasswordConfirm = new ReaLTaiizor.Controls.HopeTextBox();
            lblNewPassword = new ReaLTaiizor.Controls.MaterialLabel();
            lblNewPasswordConfirm = new ReaLTaiizor.Controls.MaterialLabel();
            btnSave = new ReaLTaiizor.Controls.MaterialButton();
            btnLogout = new ReaLTaiizor.Controls.MaterialButton();
            SuspendLayout();
            // 
            // lblTitle1
            // 
            lblTitle1.AutoSize = true;
            lblTitle1.Depth = 0;
            lblTitle1.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTitle1.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.H6;
            lblTitle1.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitle1.HighEmphasis = true;
            lblTitle1.Location = new Point(32, 20);
            lblTitle1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblTitle1.Name = "lblTitle1";
            lblTitle1.Size = new Size(86, 24);
            lblTitle1.TabIndex = 0;
            lblTitle1.Text = "내 정보 수정";
            // 
            // lblUsernameTitle
            // 
            lblUsernameTitle.AutoSize = true;
            lblUsernameTitle.Depth = 0;
            lblUsernameTitle.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblUsernameTitle.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle2;
            lblUsernameTitle.ForeColor = Color.FromArgb(73, 80, 87);
            lblUsernameTitle.Location = new Point(32, 70);
            lblUsernameTitle.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblUsernameTitle.Name = "lblUsernameTitle";
            lblUsernameTitle.Size = new Size(31, 17);
            lblUsernameTitle.TabIndex = 8;
            lblUsernameTitle.Text = "아이디";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Depth = 0;
            lblUsername.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblUsername.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle1;
            lblUsername.ForeColor = Color.FromArgb(33, 37, 41);
            lblUsername.HighEmphasis = true;
            lblUsername.Location = new Point(200, 70);
            lblUsername.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(5, 19);
            lblUsername.TabIndex = 9;
            lblUsername.Text = "-";
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel1.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle2;
            materialLabel1.ForeColor = Color.FromArgb(73, 80, 87);
            materialLabel1.Location = new Point(32, 120);
            materialLabel1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(21, 17);
            materialLabel1.TabIndex = 5;
            materialLabel1.Text = "이름";
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel2.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle2;
            materialLabel2.ForeColor = Color.FromArgb(73, 80, 87);
            materialLabel2.Location = new Point(32, 180);
            materialLabel2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(31, 17);
            materialLabel2.TabIndex = 6;
            materialLabel2.Text = "이메일";
            // 
            // materialLabel3
            // 
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel3.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle2;
            materialLabel3.ForeColor = Color.FromArgb(73, 80, 87);
            materialLabel3.Location = new Point(32, 240);
            materialLabel3.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new Size(41, 17);
            materialLabel3.TabIndex = 7;
            materialLabel3.Text = "전화번호";
            // 
            // lblPasswordTitle
            // 
            lblPasswordTitle.AutoSize = true;
            lblPasswordTitle.Depth = 0;
            lblPasswordTitle.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblPasswordTitle.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle2;
            lblPasswordTitle.ForeColor = Color.FromArgb(73, 80, 87);
            lblPasswordTitle.Location = new Point(32, 310);
            lblPasswordTitle.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblPasswordTitle.Name = "lblPasswordTitle";
            lblPasswordTitle.Size = new Size(64, 17);
            lblPasswordTitle.TabIndex = 10;
            lblPasswordTitle.Text = "현재 비밀번호";
            // 
            // tbName
            // 
            tbName.BackColor = Color.White;
            tbName.BaseColor = Color.White;
            tbName.BorderColorA = Color.FromArgb(13, 110, 253);
            tbName.BorderColorB = Color.FromArgb(222, 226, 230);
            tbName.Font = new Font("Segoe UI", 11F);
            tbName.ForeColor = Color.FromArgb(33, 37, 41);
            tbName.Hint = "";
            tbName.Location = new Point(200, 110);
            tbName.Margin = new Padding(3, 4, 3, 4);
            tbName.MaxLength = 128;
            tbName.Multiline = false;
            tbName.Name = "tbName";
            tbName.PasswordChar = '\0';
            tbName.ScrollBars = ScrollBars.None;
            tbName.SelectedText = "";
            tbName.SelectionLength = 0;
            tbName.SelectionStart = 0;
            tbName.Size = new Size(400, 36);
            tbName.TabIndex = 1;
            tbName.TabStop = false;
            tbName.UseSystemPasswordChar = false;
            // 
            // tbEmail
            // 
            tbEmail.BackColor = Color.White;
            tbEmail.BaseColor = Color.White;
            tbEmail.BorderColorA = Color.FromArgb(13, 110, 253);
            tbEmail.BorderColorB = Color.FromArgb(222, 226, 230);
            tbEmail.Font = new Font("Segoe UI", 11F);
            tbEmail.ForeColor = Color.FromArgb(33, 37, 41);
            tbEmail.Hint = "";
            tbEmail.Location = new Point(200, 170);
            tbEmail.Margin = new Padding(3, 4, 3, 4);
            tbEmail.MaxLength = 128;
            tbEmail.Multiline = false;
            tbEmail.Name = "tbEmail";
            tbEmail.PasswordChar = '\0';
            tbEmail.ScrollBars = ScrollBars.None;
            tbEmail.SelectedText = "";
            tbEmail.SelectionLength = 0;
            tbEmail.SelectionStart = 0;
            tbEmail.Size = new Size(400, 36);
            tbEmail.TabIndex = 2;
            tbEmail.TabStop = false;
            tbEmail.UseSystemPasswordChar = false;
            // 
            // tbPhone
            // 
            tbPhone.BackColor = Color.White;
            tbPhone.BaseColor = Color.White;
            tbPhone.BorderColorA = Color.FromArgb(13, 110, 253);
            tbPhone.BorderColorB = Color.FromArgb(222, 226, 230);
            tbPhone.Font = new Font("Segoe UI", 11F);
            tbPhone.ForeColor = Color.FromArgb(33, 37, 41);
            tbPhone.Hint = "";
            tbPhone.Location = new Point(200, 230);
            tbPhone.Margin = new Padding(3, 4, 3, 4);
            tbPhone.MaxLength = 128;
            tbPhone.Multiline = false;
            tbPhone.Name = "tbPhone";
            tbPhone.PasswordChar = '\0';
            tbPhone.ScrollBars = ScrollBars.None;
            tbPhone.SelectedText = "";
            tbPhone.SelectionLength = 0;
            tbPhone.SelectionStart = 0;
            tbPhone.Size = new Size(400, 36);
            tbPhone.TabIndex = 3;
            tbPhone.TabStop = false;
            tbPhone.UseSystemPasswordChar = false;
            // 
            // tbCurrentPassword
            // 
            tbCurrentPassword.BackColor = Color.White;
            tbCurrentPassword.BaseColor = Color.White;
            tbCurrentPassword.BorderColorA = Color.FromArgb(13, 110, 253);
            tbCurrentPassword.BorderColorB = Color.FromArgb(222, 226, 230);
            tbCurrentPassword.Font = new Font("Segoe UI", 11F);
            tbCurrentPassword.ForeColor = Color.FromArgb(33, 37, 41);
            tbCurrentPassword.Hint = "변경하려면 입력";
            tbCurrentPassword.Location = new Point(200, 300);
            tbCurrentPassword.Margin = new Padding(3, 4, 3, 4);
            tbCurrentPassword.MaxLength = 128;
            tbCurrentPassword.Multiline = false;
            tbCurrentPassword.Name = "tbCurrentPassword";
            tbCurrentPassword.PasswordChar = '●';
            tbCurrentPassword.ScrollBars = ScrollBars.None;
            tbCurrentPassword.SelectedText = "";
            tbCurrentPassword.SelectionLength = 0;
            tbCurrentPassword.SelectionStart = 0;
            tbCurrentPassword.Size = new Size(400, 36);
            tbCurrentPassword.TabIndex = 4;
            tbCurrentPassword.TabStop = false;
            tbCurrentPassword.UseSystemPasswordChar = false;
            // 
            // tbNewPassword
            // 
            tbNewPassword.BackColor = Color.White;
            tbNewPassword.BaseColor = Color.White;
            tbNewPassword.BorderColorA = Color.FromArgb(13, 110, 253);
            tbNewPassword.BorderColorB = Color.FromArgb(222, 226, 230);
            tbNewPassword.Font = new Font("Segoe UI", 11F);
            tbNewPassword.ForeColor = Color.FromArgb(33, 37, 41);
            tbNewPassword.Hint = "6자 이상";
            tbNewPassword.Location = new Point(200, 360);
            tbNewPassword.Margin = new Padding(3, 4, 3, 4);
            tbNewPassword.MaxLength = 128;
            tbNewPassword.Multiline = false;
            tbNewPassword.Name = "tbNewPassword";
            tbNewPassword.PasswordChar = '●';
            tbNewPassword.ScrollBars = ScrollBars.None;
            tbNewPassword.SelectedText = "";
            tbNewPassword.SelectionLength = 0;
            tbNewPassword.SelectionStart = 0;
            tbNewPassword.Size = new Size(400, 36);
            tbNewPassword.TabIndex = 5;
            tbNewPassword.TabStop = false;
            tbNewPassword.UseSystemPasswordChar = false;
            // 
            // tbNewPasswordConfirm
            // 
            tbNewPasswordConfirm.BackColor = Color.White;
            tbNewPasswordConfirm.BaseColor = Color.White;
            tbNewPasswordConfirm.BorderColorA = Color.FromArgb(13, 110, 253);
            tbNewPasswordConfirm.BorderColorB = Color.FromArgb(222, 226, 230);
            tbNewPasswordConfirm.Font = new Font("Segoe UI", 11F);
            tbNewPasswordConfirm.ForeColor = Color.FromArgb(33, 37, 41);
            tbNewPasswordConfirm.Hint = "비밀번호 재입력";
            tbNewPasswordConfirm.Location = new Point(200, 420);
            tbNewPasswordConfirm.Margin = new Padding(3, 4, 3, 4);
            tbNewPasswordConfirm.MaxLength = 128;
            tbNewPasswordConfirm.Multiline = false;
            tbNewPasswordConfirm.Name = "tbNewPasswordConfirm";
            tbNewPasswordConfirm.PasswordChar = '●';
            tbNewPasswordConfirm.ScrollBars = ScrollBars.None;
            tbNewPasswordConfirm.SelectedText = "";
            tbNewPasswordConfirm.SelectionLength = 0;
            tbNewPasswordConfirm.SelectionStart = 0;
            tbNewPasswordConfirm.Size = new Size(400, 36);
            tbNewPasswordConfirm.TabIndex = 6;
            tbNewPasswordConfirm.TabStop = false;
            tbNewPasswordConfirm.UseSystemPasswordChar = false;
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Depth = 0;
            lblNewPassword.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblNewPassword.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle2;
            lblNewPassword.ForeColor = Color.FromArgb(73, 80, 87);
            lblNewPassword.Location = new Point(32, 370);
            lblNewPassword.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(54, 17);
            lblNewPassword.TabIndex = 11;
            lblNewPassword.Text = "새 비밀번호";
            // 
            // lblNewPasswordConfirm
            // 
            lblNewPasswordConfirm.AutoSize = true;
            lblNewPasswordConfirm.Depth = 0;
            lblNewPasswordConfirm.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblNewPasswordConfirm.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle2;
            lblNewPasswordConfirm.ForeColor = Color.FromArgb(73, 80, 87);
            lblNewPasswordConfirm.Location = new Point(32, 430);
            lblNewPasswordConfirm.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblNewPasswordConfirm.Name = "lblNewPasswordConfirm";
            lblNewPasswordConfirm.Size = new Size(77, 17);
            lblNewPasswordConfirm.TabIndex = 12;
            lblNewPasswordConfirm.Text = "새 비밀번호 확인";
            // 
            // btnSave
            // 
            btnSave.AutoSize = false;
            btnSave.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSave.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnSave.Cursor = Cursors.Hand;
            btnSave.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnSave.Depth = 0;
            btnSave.Font = new Font("Segoe UI", 11F);
            btnSave.HighEmphasis = true;
            btnSave.Icon = null;
            btnSave.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnSave.Location = new Point(200, 490);
            btnSave.Margin = new Padding(4, 8, 4, 8);
            btnSave.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnSave.Name = "btnSave";
            btnSave.NoAccentTextColor = Color.Empty;
            btnSave.Size = new Size(150, 42);
            btnSave.TabIndex = 7;
            btnSave.Text = "💾 저장";
            btnSave.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            btnSave.UseAccentColor = true;
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // btnLogout
            // 
            btnLogout.AutoSize = false;
            btnLogout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnLogout.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnLogout.Depth = 0;
            btnLogout.Font = new Font("Segoe UI", 11F);
            btnLogout.HighEmphasis = true;
            btnLogout.Icon = null;
            btnLogout.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnLogout.Location = new Point(370, 490);
            btnLogout.Margin = new Padding(4, 8, 4, 8);
            btnLogout.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnLogout.Name = "btnLogout";
            btnLogout.NoAccentTextColor = Color.Empty;
            btnLogout.Size = new Size(150, 42);
            btnLogout.TabIndex = 8;
            btnLogout.Text = "🚪 로그아웃";
            btnLogout.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnLogout.UseAccentColor = false;
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += BtnLogout_Click;
            // 
            // UserInfoControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 255);
            Controls.Add(btnLogout);
            Controls.Add(lblNewPasswordConfirm);
            Controls.Add(lblNewPassword);
            Controls.Add(lblPasswordTitle);
            Controls.Add(materialLabel3);
            Controls.Add(materialLabel2);
            Controls.Add(materialLabel1);
            Controls.Add(lblUsername);
            Controls.Add(lblUsernameTitle);
            Controls.Add(btnSave);
            Controls.Add(tbNewPasswordConfirm);
            Controls.Add(tbNewPassword);
            Controls.Add(tbCurrentPassword);
            Controls.Add(tbPhone);
            Controls.Add(tbEmail);
            Controls.Add(tbName);
            Controls.Add(lblTitle1);
            Name = "UserInfoControl";
            Padding = new Padding(32);
            Size = new Size(800, 600);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.MaterialLabel lblTitle1;
        private ReaLTaiizor.Controls.MaterialLabel lblUsernameTitle;
        private ReaLTaiizor.Controls.MaterialLabel lblUsername;
        private ReaLTaiizor.Controls.HopeTextBox tbName;
        private ReaLTaiizor.Controls.HopeTextBox tbEmail;
        private ReaLTaiizor.Controls.HopeTextBox tbPhone;
        private ReaLTaiizor.Controls.MaterialLabel lblPasswordTitle;
        private ReaLTaiizor.Controls.HopeTextBox tbCurrentPassword;
        private ReaLTaiizor.Controls.HopeTextBox tbNewPassword;
        private ReaLTaiizor.Controls.HopeTextBox tbNewPasswordConfirm;
        private ReaLTaiizor.Controls.MaterialLabel lblNewPassword;
        private ReaLTaiizor.Controls.MaterialLabel lblNewPasswordConfirm;
        private ReaLTaiizor.Controls.MaterialButton btnSave;
        private ReaLTaiizor.Controls.MaterialButton btnLogout;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel1;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel2;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel3;
    }
}

