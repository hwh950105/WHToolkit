using hwh.Core;

namespace hwh.Controls
{
    partial class apiControl
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
            
            // 이미지 리소스 정리
            if (disposing)
            {
                pictureBoxOriginal.Image?.Dispose();
                pictureBoxResult.Image?.Dispose();
                CleanupTempFiles();
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
            btnSelectImage = new ReaLTaiizor.Controls.MaterialButton();
            btnApply = new ReaLTaiizor.Controls.MaterialButton();
            pictureBoxOriginal = new PictureBox();
            pictureBoxResult = new PictureBox();
            lblOriginal = new Label();
            lblResult = new Label();
            lblStatus = new Label();
            panelButtons = new Panel();
            aloneTextBox1 = new ReaLTaiizor.Controls.AloneTextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            panelLeft = new Panel();
            panelRight = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxResult).BeginInit();
            panelButtons.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panelLeft.SuspendLayout();
            panelRight.SuspendLayout();
            SuspendLayout();
            // 
            // btnSelectImage
            // 
            btnSelectImage.AutoSize = false;
            btnSelectImage.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSelectImage.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnSelectImage.Cursor = Cursors.Hand;
            btnSelectImage.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnSelectImage.Depth = 0;
            btnSelectImage.Font = new Font("Segoe UI", 11F);
            btnSelectImage.HighEmphasis = true;
            btnSelectImage.Icon = null;
            btnSelectImage.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnSelectImage.Location = new Point(13, 14);
            btnSelectImage.Margin = new Padding(4, 8, 4, 8);
            btnSelectImage.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.NoAccentTextColor = Color.Empty;
            btnSelectImage.Size = new Size(180, 42);
            btnSelectImage.TabIndex = 0;
            btnSelectImage.Text = "📁 주식차트 이미지 선택";
            btnSelectImage.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnSelectImage.UseAccentColor = false;
            btnSelectImage.UseVisualStyleBackColor = true;
            btnSelectImage.Click += BtnSelectImage_Click;
            // 
            // btnApply
            // 
            btnApply.AutoSize = false;
            btnApply.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnApply.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnApply.Cursor = Cursors.Hand;
            btnApply.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnApply.Depth = 0;
            btnApply.Enabled = false;
            btnApply.Font = new Font("Segoe UI", 11F);
            btnApply.HighEmphasis = true;
            btnApply.Icon = null;
            btnApply.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnApply.Location = new Point(200, 14);
            btnApply.Margin = new Padding(4, 8, 4, 8);
            btnApply.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnApply.Name = "btnApply";
            btnApply.NoAccentTextColor = Color.Empty;
            btnApply.Size = new Size(180, 42);
            btnApply.TabIndex = 1;
            btnApply.Text = "🚀 AI 차트 분석 시작";
            btnApply.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            btnApply.UseAccentColor = true;
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // pictureBoxOriginal
            // 
            pictureBoxOriginal.BackColor = Color.FromArgb(17, 24, 39);
            pictureBoxOriginal.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxOriginal.Cursor = Cursors.Hand;
            pictureBoxOriginal.Dock = DockStyle.Fill;
            pictureBoxOriginal.Location = new Point(0, 0);
            pictureBoxOriginal.Name = "pictureBoxOriginal";
            pictureBoxOriginal.Size = new Size(369, 352);
            pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxOriginal.TabIndex = 3;
            pictureBoxOriginal.TabStop = false;
            pictureBoxOriginal.Click += PictureBoxOriginal_Click;
            // 
            // pictureBoxResult
            // 
            pictureBoxResult.BackColor = Color.FromArgb(17, 24, 39);
            pictureBoxResult.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxResult.Cursor = Cursors.Hand;
            pictureBoxResult.Dock = DockStyle.Fill;
            pictureBoxResult.Location = new Point(5, 0);
            pictureBoxResult.Name = "pictureBoxResult";
            pictureBoxResult.Size = new Size(369, 352);
            pictureBoxResult.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxResult.TabIndex = 5;
            pictureBoxResult.TabStop = false;
            pictureBoxResult.Click += PictureBoxResult_Click;
            // 
            // lblOriginal
            // 
            lblOriginal.AutoSize = true;
            lblOriginal.Dock = DockStyle.Top;
            lblOriginal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOriginal.ForeColor = Color.FromArgb(17, 24, 39);
            lblOriginal.Location = new Point(0, 0);
            lblOriginal.Name = "lblOriginal";
            lblOriginal.Padding = new Padding(0, 0, 0, 5);
            lblOriginal.Size = new Size(83, 24);
            lblOriginal.TabIndex = 2;
            lblOriginal.Text = "원본 이미지";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Dock = DockStyle.Top;
            lblResult.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblResult.ForeColor = Color.FromArgb(17, 24, 39);
            lblResult.Location = new Point(5, 0);
            lblResult.Name = "lblResult";
            lblResult.Padding = new Padding(0, 0, 0, 5);
            lblResult.Size = new Size(69, 24);
            lblResult.TabIndex = 4;
            lblResult.Text = "분석 결과";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Font = new Font("Segoe UI", 9.75F);
            lblStatus.ForeColor = Color.FromArgb(107, 114, 128);
            lblStatus.Location = new Point(20, 458);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(0, 5, 0, 0);
            lblStatus.Size = new Size(145, 22);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "이미지를 선택해주세요.";
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.FromArgb(249, 250, 251);
            panelButtons.Controls.Add(aloneTextBox1);
            panelButtons.Controls.Add(btnSelectImage);
            panelButtons.Controls.Add(btnApply);
            panelButtons.Dock = DockStyle.Top;
            panelButtons.Location = new Point(20, 20);
            panelButtons.MaximumSize = new Size(0, 70);
            panelButtons.MinimumSize = new Size(0, 70);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(10);
            panelButtons.Size = new Size(760, 70);
            panelButtons.TabIndex = 0;
            // 
            // aloneTextBox1
            // 
            aloneTextBox1.BackColor = Color.Transparent;
            aloneTextBox1.EnabledCalc = true;
            aloneTextBox1.Font = new Font("Segoe UI", 9F);
            aloneTextBox1.ForeColor = Color.FromArgb(124, 133, 142);
            aloneTextBox1.Location = new Point(399, 27);
            aloneTextBox1.MaxLength = 32767;
            aloneTextBox1.MultiLine = false;
            aloneTextBox1.Name = "aloneTextBox1";
            aloneTextBox1.ReadOnly = false;
            aloneTextBox1.Size = new Size(338, 29);
            aloneTextBox1.TabIndex = 2;
            aloneTextBox1.Text = "openai_api_key";
            aloneTextBox1.TextAlign = HorizontalAlignment.Left;
            aloneTextBox1.UseSystemPasswordChar = false;
            aloneTextBox1.UseWaitCursor = true;
            aloneTextBox1.TextChanged += aloneTextBox1_TextChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panelLeft, 0, 0);
            tableLayoutPanel1.Controls.Add(panelRight, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(20, 90);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(0, 10, 0, 0);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(760, 368);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(lblOriginal);
            panelLeft.Controls.Add(pictureBoxOriginal);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Location = new Point(3, 13);
            panelLeft.Name = "panelLeft";
            panelLeft.Padding = new Padding(0, 0, 5, 0);
            panelLeft.Size = new Size(374, 352);
            panelLeft.TabIndex = 0;
            // 
            // panelRight
            // 
            panelRight.Controls.Add(lblResult);
            panelRight.Controls.Add(pictureBoxResult);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(383, 13);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(5, 0, 0, 0);
            panelRight.Size = new Size(374, 352);
            panelRight.TabIndex = 1;
            // 
            // apiControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 250, 251);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(lblStatus);
            Controls.Add(panelButtons);
            MinimumSize = new Size(600, 400);
            Name = "apiControl";
            Padding = new Padding(20);
            Size = new Size(800, 500);
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxResult).EndInit();
            panelButtons.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ReaLTaiizor.Controls.MaterialButton btnSelectImage;
        private ReaLTaiizor.Controls.MaterialButton btnApply;
        private PictureBox pictureBoxOriginal;
        private PictureBox pictureBoxResult;
        private Label lblOriginal;
        private Label lblResult;
        private Label lblStatus;
        private Panel panelButtons;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panelLeft;
        private Panel panelRight;
        private ReaLTaiizor.Controls.AloneTextBox aloneTextBox1;
    }
}

