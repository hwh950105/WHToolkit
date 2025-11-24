using System;
using System.Drawing;
using System.Windows.Forms;
using Win32.Wrapper;

namespace hwh.Controls.Win32Controls
{
    /// <summary>
    /// 화면 캡처 테스트 컨트롤
    /// </summary>
    public partial class ScreenCaptureControl : UserControl
    {
        private Bitmap? capturedImage;

        public ScreenCaptureControl()
        {
            InitializeComponent();
            LoadMonitorList();
        }

        private void LoadMonitorList()
        {
            comboMonitor.Items.Clear();
            var screens = Screen.AllScreens;

            for (int i = 0; i < screens.Length; i++)
            {
                var screen = screens[i];
                string displayName = $"모니터 {i + 1}: {screen.Bounds.Width}x{screen.Bounds.Height}";
                if (screen.Primary)
                    displayName += " (주)";

                comboMonitor.Items.Add(displayName);
            }

            if (comboMonitor.Items.Count > 0)
                comboMonitor.SelectedIndex = 0;

            lblMonitorCount.Text = $"총 {screens.Length}개의 모니터가 감지되었습니다.";
        }

        private void btnCaptureScreen_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "전체 화면을 캡처하는 중...";
                Application.DoEvents();

                capturedImage?.Dispose();
                capturedImage = ScreenCapture.CaptureScreen();

                DisplayImage(capturedImage);
                lblStatus.Text = $"전체 화면 캡처 완료! 크기: {capturedImage.Width}x{capturedImage.Height}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "캡처 실패!";
            }
        }

        private void btnCaptureSelectedMonitor_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboMonitor.SelectedIndex < 0)
                {
                    MessageBox.Show("모니터를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int monitorIndex = comboMonitor.SelectedIndex;
                lblStatus.Text = $"모니터 {monitorIndex + 1}을(를) 캡처하는 중...";
                Application.DoEvents();

                capturedImage?.Dispose();
                capturedImage = ScreenCapture.CaptureMonitor(monitorIndex);

                DisplayImage(capturedImage);
                lblStatus.Text = $"모니터 {monitorIndex + 1} 캡처 완료! 크기: {capturedImage.Width}x{capturedImage.Height}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "캡처 실패!";
            }
        }

        private void btnCaptureMouseMonitor_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "마우스가 있는 모니터를 캡처하는 중...";
                Application.DoEvents();

                capturedImage?.Dispose();
                capturedImage = ScreenCapture.CaptureMonitorWithMouse();

                // 마우스가 있는 모니터 인덱스 확인
                var mousePosition = Cursor.Position;
                var screen = Screen.FromPoint(mousePosition);
                int monitorIndex = Array.IndexOf(Screen.AllScreens, screen);

                DisplayImage(capturedImage);
                lblStatus.Text = $"마우스 모니터 (모니터 {monitorIndex + 1}) 캡처 완료! 크기: {capturedImage.Width}x{capturedImage.Height}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "캡처 실패!";
            }
        }

        private void btnCaptureRegion_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtX.Text, out int x) ||
                    !int.TryParse(txtY.Text, out int y) ||
                    !int.TryParse(txtWidth.Text, out int width) ||
                    !int.TryParse(txtHeight.Text, out int height))
                {
                    MessageBox.Show("올바른 좌표와 크기를 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblStatus.Text = "영역을 캡처하는 중...";
                Application.DoEvents();

                Rectangle region = new Rectangle(x, y, width, height);
                capturedImage?.Dispose();
                capturedImage = ScreenCapture.CaptureRegion(region);

                DisplayImage(capturedImage);
                lblStatus.Text = $"영역 캡처 완료! 크기: {capturedImage.Width}x{capturedImage.Height}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "캡처 실패!";
            }
        }

        private void btnCaptureActiveWindow_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "2초 후 활성 윈도우를 캡처합니다...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(2000);

                capturedImage?.Dispose();
                capturedImage = ScreenCapture.CaptureActiveWindow();

                DisplayImage(capturedImage);
                lblStatus.Text = $"활성 윈도우 캡처 완료! 크기: {capturedImage.Width}x{capturedImage.Height}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "캡처 실패!";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (capturedImage == null)
            {
                MessageBox.Show("먼저 화면을 캡처하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "PNG 이미지|*.png|JPEG 이미지|*.jpg|비트맵 이미지|*.bmp";
                dlg.FileName = $"capture_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var format = System.Drawing.Imaging.ImageFormat.Png;
                        if (dlg.FileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                            format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        else if (dlg.FileName.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                            format = System.Drawing.Imaging.ImageFormat.Bmp;

                        capturedImage.Save(dlg.FileName, format);
                        lblStatus.Text = $"이미지 저장 완료: {dlg.FileName}";
                        MessageBox.Show("이미지가 저장되었습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"저장 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnGetPixel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtPixelX.Text, out int x) || !int.TryParse(txtPixelY.Text, out int y))
                {
                    MessageBox.Show("올바른 좌표를 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Color color = PixelReader.GetPixel(x, y);
                panelColor.BackColor = color;
                lblColorInfo.Text = $"RGB({color.R}, {color.G}, {color.B}) | Hex: #{color.R:X2}{color.G:X2}{color.B:X2}";
                lblStatus.Text = $"({x}, {y}) 픽셀 색상을 가져왔습니다.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayImage(Bitmap bitmap)
        {
            if (pictureBox.Image != null && pictureBox.Image != capturedImage)
            {
                pictureBox.Image.Dispose();
            }

            // 픽처박스 크기에 맞게 이미지 조정
            pictureBox.Image = bitmap;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                capturedImage?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

