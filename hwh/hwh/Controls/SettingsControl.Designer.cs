namespace hwh.Controls
{
    partial class SettingsControl
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
            lblTitle2 = new ReaLTaiizor.Controls.MaterialLabel();
            SuspendLayout();
            // 
            // lblTitle2
            // 
            lblTitle2.AutoSize = true;
            lblTitle2.Depth = 0;
            lblTitle2.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTitle2.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.H6;
            lblTitle2.ForeColor = Color.FromArgb(33, 37, 41);
            lblTitle2.HighEmphasis = true;
            lblTitle2.Location = new Point(32, 32);
            lblTitle2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblTitle2.Name = "lblTitle2";
            lblTitle2.Size = new Size(31, 24);
            lblTitle2.TabIndex = 0;
            lblTitle2.Text = "설정";
            // 
            // SettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 255);
            Controls.Add(lblTitle2);
            Name = "SettingsControl";
            Padding = new Padding(32);
            Size = new Size(800, 600);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.MaterialLabel lblTitle2;
    }
}

