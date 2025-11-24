namespace hwh.Controls.Win32Controls
{
    partial class ScreenCaptureControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpCapture;
        private System.Windows.Forms.Button btnCaptureScreen;
        private System.Windows.Forms.Button btnCaptureActiveWindow;
        private System.Windows.Forms.Button btnCaptureRegion;
        private System.Windows.Forms.Label lblMonitorSelect;
        private System.Windows.Forms.ComboBox comboMonitor;
        private System.Windows.Forms.Button btnCaptureSelectedMonitor;
        private System.Windows.Forms.Button btnCaptureMouseMonitor;
        private System.Windows.Forms.Label lblMonitorCount;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grpPixel;
        private System.Windows.Forms.Label lblPixelX;
        private System.Windows.Forms.Label lblPixelY;
        private System.Windows.Forms.TextBox txtPixelX;
        private System.Windows.Forms.TextBox txtPixelY;
        private System.Windows.Forms.Button btnGetPixel;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.Label lblColorInfo;

        private void InitializeComponent()
        {
            grpCapture = new GroupBox();
            lblMonitorCount = new Label();
            btnCaptureMouseMonitor = new Button();
            btnCaptureSelectedMonitor = new Button();
            comboMonitor = new ComboBox();
            lblMonitorSelect = new Label();
            txtHeight = new TextBox();
            txtWidth = new TextBox();
            txtY = new TextBox();
            txtX = new TextBox();
            lblHeight = new Label();
            lblWidth = new Label();
            lblY = new Label();
            lblX = new Label();
            btnCaptureRegion = new Button();
            btnCaptureActiveWindow = new Button();
            btnCaptureScreen = new Button();
            pictureBox = new PictureBox();
            btnSave = new Button();
            lblStatus = new Label();
            grpPixel = new GroupBox();
            lblColorInfo = new Label();
            panelColor = new Panel();
            btnGetPixel = new Button();
            txtPixelY = new TextBox();
            txtPixelX = new TextBox();
            lblPixelY = new Label();
            lblPixelX = new Label();
            grpCapture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            grpPixel.SuspendLayout();
            SuspendLayout();
            // 
            // grpCapture
            // 
            grpCapture.Controls.Add(lblMonitorCount);
            grpCapture.Controls.Add(btnCaptureMouseMonitor);
            grpCapture.Controls.Add(btnCaptureSelectedMonitor);
            grpCapture.Controls.Add(comboMonitor);
            grpCapture.Controls.Add(lblMonitorSelect);
            grpCapture.Controls.Add(txtHeight);
            grpCapture.Controls.Add(txtWidth);
            grpCapture.Controls.Add(txtY);
            grpCapture.Controls.Add(txtX);
            grpCapture.Controls.Add(lblHeight);
            grpCapture.Controls.Add(lblWidth);
            grpCapture.Controls.Add(lblY);
            grpCapture.Controls.Add(lblX);
            grpCapture.Controls.Add(btnCaptureRegion);
            grpCapture.Controls.Add(btnCaptureActiveWindow);
            grpCapture.Controls.Add(btnCaptureScreen);
            grpCapture.Location = new Point(20, 20);
            grpCapture.Name = "grpCapture";
            grpCapture.Size = new Size(450, 500);
            grpCapture.TabIndex = 0;
            grpCapture.TabStop = false;
            grpCapture.Text = "화면 캡처";
            // 
            // lblMonitorCount
            // 
            lblMonitorCount.AutoSize = true;
            lblMonitorCount.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            lblMonitorCount.ForeColor = Color.Green;
            lblMonitorCount.Location = new Point(20, 465);
            lblMonitorCount.Name = "lblMonitorCount";
            lblMonitorCount.Size = new Size(150, 15);
            lblMonitorCount.TabIndex = 15;
            lblMonitorCount.Text = "모니터 정보 로딩 중...";
            // 
            // btnCaptureMouseMonitor
            // 
            btnCaptureMouseMonitor.Location = new Point(20, 420);
            btnCaptureMouseMonitor.Name = "btnCaptureMouseMonitor";
            btnCaptureMouseMonitor.Size = new Size(410, 35);
            btnCaptureMouseMonitor.TabIndex = 14;
            btnCaptureMouseMonitor.Text = "🖱️ 마우스가 있는 모니터 캡처";
            btnCaptureMouseMonitor.UseVisualStyleBackColor = true;
            btnCaptureMouseMonitor.Click += btnCaptureMouseMonitor_Click;
            // 
            // btnCaptureSelectedMonitor
            // 
            btnCaptureSelectedMonitor.Location = new Point(20, 375);
            btnCaptureSelectedMonitor.Name = "btnCaptureSelectedMonitor";
            btnCaptureSelectedMonitor.Size = new Size(410, 35);
            btnCaptureSelectedMonitor.TabIndex = 13;
            btnCaptureSelectedMonitor.Text = "🖥️ 선택한 모니터 캡처";
            btnCaptureSelectedMonitor.UseVisualStyleBackColor = true;
            btnCaptureSelectedMonitor.Click += btnCaptureSelectedMonitor_Click;
            // 
            // comboMonitor
            // 
            comboMonitor.DropDownStyle = ComboBoxStyle.DropDownList;
            comboMonitor.FormattingEnabled = true;
            comboMonitor.Location = new Point(20, 335);
            comboMonitor.Name = "comboMonitor";
            comboMonitor.Size = new Size(410, 33);
            comboMonitor.TabIndex = 12;
            // 
            // lblMonitorSelect
            // 
            lblMonitorSelect.AutoSize = true;
            lblMonitorSelect.Location = new Point(20, 308);
            lblMonitorSelect.Name = "lblMonitorSelect";
            lblMonitorSelect.Size = new Size(90, 25);
            lblMonitorSelect.TabIndex = 11;
            lblMonitorSelect.Text = "모니터 선택:";
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(290, 215);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(100, 31);
            txtHeight.TabIndex = 10;
            txtHeight.Text = "300";
            // 
            // txtWidth
            // 
            txtWidth.Location = new Point(110, 215);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(100, 31);
            txtWidth.TabIndex = 9;
            txtWidth.Text = "400";
            // 
            // txtY
            // 
            txtY.Location = new Point(290, 165);
            txtY.Name = "txtY";
            txtY.Size = new Size(100, 31);
            txtY.TabIndex = 8;
            txtY.Text = "100";
            // 
            // txtX
            // 
            txtX.Location = new Point(110, 165);
            txtX.Name = "txtX";
            txtX.Size = new Size(100, 31);
            txtX.TabIndex = 7;
            txtX.Text = "100";
            // 
            // lblHeight
            // 
            lblHeight.AutoSize = true;
            lblHeight.Location = new Point(230, 218);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(52, 25);
            lblHeight.TabIndex = 6;
            lblHeight.Text = "높이:";
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(20, 218);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(85, 25);
            lblWidth.TabIndex = 5;
            lblWidth.Text = "너비(폭):";
            // 
            // lblY
            // 
            lblY.AutoSize = true;
            lblY.Location = new Point(255, 168);
            lblY.Name = "lblY";
            lblY.Size = new Size(25, 25);
            lblY.TabIndex = 4;
            lblY.Text = "Y:";
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.Location = new Point(75, 168);
            lblX.Name = "lblX";
            lblX.Size = new Size(26, 25);
            lblX.TabIndex = 3;
            lblX.Text = "X:";
            // 
            // btnCaptureRegion
            // 
            btnCaptureRegion.Location = new Point(20, 255);
            btnCaptureRegion.Name = "btnCaptureRegion";
            btnCaptureRegion.Size = new Size(410, 35);
            btnCaptureRegion.TabIndex = 2;
            btnCaptureRegion.Text = "📐 영역 캡처";
            btnCaptureRegion.UseVisualStyleBackColor = true;
            btnCaptureRegion.Click += btnCaptureRegion_Click;
            // 
            // btnCaptureActiveWindow
            // 
            btnCaptureActiveWindow.Location = new Point(20, 80);
            btnCaptureActiveWindow.Name = "btnCaptureActiveWindow";
            btnCaptureActiveWindow.Size = new Size(410, 40);
            btnCaptureActiveWindow.TabIndex = 1;
            btnCaptureActiveWindow.Text = "\U0001fa9f 활성 윈도우 캡처 (2초 후)";
            btnCaptureActiveWindow.UseVisualStyleBackColor = true;
            btnCaptureActiveWindow.Click += btnCaptureActiveWindow_Click;
            // 
            // btnCaptureScreen
            // 
            btnCaptureScreen.Location = new Point(20, 30);
            btnCaptureScreen.Name = "btnCaptureScreen";
            btnCaptureScreen.Size = new Size(410, 40);
            btnCaptureScreen.TabIndex = 0;
            btnCaptureScreen.Text = "🌐 전체 화면 캡처 (모든 모니터)";
            btnCaptureScreen.UseVisualStyleBackColor = true;
            btnCaptureScreen.Click += btnCaptureScreen_Click;
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(490, 20);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(500, 500);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 1;
            pictureBox.TabStop = false;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(490, 530);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(500, 40);
            btnSave.TabIndex = 2;
            btnSave.Text = "💾 이미지 저장";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            lblStatus.ForeColor = Color.Blue;
            lblStatus.Location = new Point(20, 730);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(68, 19);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "대기 중...";
            // 
            // grpPixel
            // 
            grpPixel.Controls.Add(lblColorInfo);
            grpPixel.Controls.Add(panelColor);
            grpPixel.Controls.Add(btnGetPixel);
            grpPixel.Controls.Add(txtPixelY);
            grpPixel.Controls.Add(txtPixelX);
            grpPixel.Controls.Add(lblPixelY);
            grpPixel.Controls.Add(lblPixelX);
            grpPixel.Location = new Point(20, 540);
            grpPixel.Name = "grpPixel";
            grpPixel.Size = new Size(450, 180);
            grpPixel.TabIndex = 4;
            grpPixel.TabStop = false;
            grpPixel.Text = "픽셀 색상 읽기";
            // 
            // lblColorInfo
            // 
            lblColorInfo.AutoSize = true;
            lblColorInfo.Font = new Font("맑은 고딕", 10F);
            lblColorInfo.Location = new Point(20, 145);
            lblColorInfo.Name = "lblColorInfo";
            lblColorInfo.Size = new Size(42, 19);
            lblColorInfo.TabIndex = 6;
            lblColorInfo.Text = "색상:";
            // 
            // panelColor
            // 
            panelColor.BorderStyle = BorderStyle.FixedSingle;
            panelColor.Location = new Point(20, 90);
            panelColor.Name = "panelColor";
            panelColor.Size = new Size(410, 45);
            panelColor.TabIndex = 5;
            // 
            // btnGetPixel
            // 
            btnGetPixel.Location = new Point(310, 40);
            btnGetPixel.Name = "btnGetPixel";
            btnGetPixel.Size = new Size(120, 35);
            btnGetPixel.TabIndex = 4;
            btnGetPixel.Text = "색상 읽기";
            btnGetPixel.UseVisualStyleBackColor = true;
            btnGetPixel.Click += btnGetPixel_Click;
            // 
            // txtPixelY
            // 
            txtPixelY.Location = new Point(195, 42);
            txtPixelY.Name = "txtPixelY";
            txtPixelY.Size = new Size(100, 31);
            txtPixelY.TabIndex = 3;
            txtPixelY.Text = "100";
            // 
            // txtPixelX
            // 
            txtPixelX.Location = new Point(55, 42);
            txtPixelX.Name = "txtPixelX";
            txtPixelX.Size = new Size(100, 31);
            txtPixelX.TabIndex = 2;
            txtPixelX.Text = "100";
            // 
            // lblPixelY
            // 
            lblPixelY.AutoSize = true;
            lblPixelY.Location = new Point(165, 45);
            lblPixelY.Name = "lblPixelY";
            lblPixelY.Size = new Size(25, 25);
            lblPixelY.TabIndex = 1;
            lblPixelY.Text = "Y:";
            // 
            // lblPixelX
            // 
            lblPixelX.AutoSize = true;
            lblPixelX.Location = new Point(20, 45);
            lblPixelX.Name = "lblPixelX";
            lblPixelX.Size = new Size(26, 25);
            lblPixelX.TabIndex = 0;
            lblPixelX.Text = "X:";
            // 
            // ScreenCaptureControl
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(grpPixel);
            Controls.Add(lblStatus);
            Controls.Add(btnSave);
            Controls.Add(pictureBox);
            Controls.Add(grpCapture);
            Font = new Font("맑은 고딕", 11F);
            Name = "ScreenCaptureControl";
            Size = new Size(1050, 800);
            grpCapture.ResumeLayout(false);
            grpCapture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            grpPixel.ResumeLayout(false);
            grpPixel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

