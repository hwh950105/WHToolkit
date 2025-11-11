using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using hwh.Core;
using hwh.Services;

namespace hwh.Controls
{
    public partial class apiControl : UserControl
    {
        private string _apiKey = "";
        private string? _originalImagePath = null;
        private string? _resizedImagePath = null;
        private string? _resultImagePath = null;
        private bool _isProcessing = false;
        private const int TARGET_WIDTH = 1280;
        private const int TARGET_HEIGHT = 720;

        public apiControl()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 이미지 선택 버튼 클릭
        /// </summary>
        private void BtnSelectImage_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "차트 이미지 선택";
                openFileDialog.Filter = "이미지 파일 (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|모든 파일 (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _originalImagePath = openFileDialog.FileName;

                        // 원본 이미지 표시
                        pictureBoxOriginal.Image?.Dispose();
                        pictureBoxOriginal.Image = Image.FromFile(_originalImagePath);

                        // 결과 이미지 초기화
                        pictureBoxResult.Image?.Dispose();
                        pictureBoxResult.Image = null;

                        // 상태 업데이트
                        lblStatus.Text = $"✅ 선택됨: {Path.GetFileName(_originalImagePath)} ({pictureBoxOriginal.Image.Width} × {pictureBoxOriginal.Image.Height})";
                        btnApply.Enabled = true;

                        MessageBoxHelper.ShowSuccess(
                            $"이미지가 선택되었습니다.\n\n" +
                            $"파일명: {Path.GetFileName(_originalImagePath)}\n" +
                            $"크기: {pictureBoxOriginal.Image.Width} × {pictureBoxOriginal.Image.Height}",
                            "이미지 선택 완료"
                        );
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.ShowError($"이미지를 불러오는데 실패했습니다.\n\n{ex.Message}", "오류");
                        _originalImagePath = null!;
                        btnApply.Enabled = false;
                        lblStatus.Text = "❌ 이미지 로드 실패";
                    }
                }
            }
        }

        /// <summary>
        /// 이미지를 1280x720으로 리사이즈 (전체 채우기 - AI 좌표 정확도 향상)
        /// </summary>
        private string ResizeImageTo1280x720(string sourcePath)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), $"chart_resized_{Guid.NewGuid()}.png");

            using (var sourceImage = Image.FromFile(sourcePath))
            {
                Console.WriteLine($"📐 원본 이미지: {sourceImage.Width}×{sourceImage.Height}");

                // 1280x720으로 늘려서 채우기 (비율 무시)
                // 이렇게 하면 AI가 반환한 좌표가 정확히 맞음
                using (var canvas = new Bitmap(TARGET_WIDTH, TARGET_HEIGHT))
                using (var graphics = Graphics.FromImage(canvas))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;

                    // 전체 영역에 이미지 그리기 (늘림)
                    graphics.DrawImage(sourceImage, 0, 0, TARGET_WIDTH, TARGET_HEIGHT);
                    canvas.Save(tempPath, ImageFormat.Png);
                }

                Console.WriteLine($"✅ 리사이즈 완료: {TARGET_WIDTH}×{TARGET_HEIGHT}");
                Console.WriteLine($"📁 임시 파일: {tempPath}");
            }

            return tempPath;
        }

        /// <summary>
        /// 차트 분석 및 시각화 실행
        /// </summary>
        private async void btnApply_Click(object sender, EventArgs e)
        {
            if (_isProcessing)
            {
                MessageBoxHelper.ShowWarning("이미 처리 중입니다. 잠시만 기다려주세요.", "처리 중");
                return;
            }

            if (string.IsNullOrEmpty(_originalImagePath) || !File.Exists(_originalImagePath))
            {
                MessageBoxHelper.ShowWarning("먼저 이미지를 선택해주세요.", "이미지 없음");
                return;
            }

            try
            {
                _isProcessing = true;
                UpdateButtonState(false);
                lblStatus.Text = "🔄 이미지 리사이징 중...";
                Application.DoEvents();

                // 1. API 키 검증
                if (!ValidateApiKey())
                {
                    return;
                }

                // 2. 이미지를 1280x720으로 리사이즈
                _resizedImagePath = ResizeImageTo1280x720(_originalImagePath);
                lblStatus.Text = "🤖 AI 분석 요청 중...";
                Application.DoEvents();

                // 3. AI 분석 실행
                var prediction = await AnalyzeChartAsync();

                if (prediction == null)
                {
                    MessageBoxHelper.ShowError("차트 분석에 실패했습니다.", "분석 실패");
                    lblStatus.Text = "❌ 분석 실패";
                    return;
                }

                lblStatus.Text = "🎨 시각화 처리 중...";
                Application.DoEvents();

                // 4. 시각화 실행
                _resultImagePath = Path.Combine(Path.GetTempPath(), $"chart_result_{Guid.NewGuid()}.png");
                VisualizeChart(prediction, _resizedImagePath, _resultImagePath);

                // 5. 결과 이미지 표시
                pictureBoxResult.Image?.Dispose();
                pictureBoxResult.Image = Image.FromFile(_resultImagePath);

                lblStatus.Text = "✅ 분석 완료!";

                // 완료 메시지
                string directionIcon = prediction.NextDirection.ToLower() == "up" ? "🔺" : "🔻";
                string directionText = prediction.NextDirection.ToUpper();

                MessageBoxHelper.ShowSuccess(
                    $"AI 예측이 완료되었습니다!\n\n" +
                    $"📊 지지선: {prediction.Support}px\n" +
                    $"📈 저항선: {prediction.Resistance}px\n" +
                    $"📍 추세점: {prediction.Trend.Count}개\n\n" +
                    $"{directionIcon} 다음 캔들: {directionText}\n" +
                    $"🎯 확률: {prediction.Probability}%",
                    "분석 완료"
                );
            }
            catch (FileNotFoundException)
            {
                MessageBoxHelper.ShowError("이미지 파일을 찾을 수 없습니다.", "파일 오류");
                lblStatus.Text = "❌ 파일 오류";
            }
            catch (UnauthorizedAccessException)
            {
                MessageBoxHelper.ShowError("파일에 접근할 수 없습니다.\n권한을 확인해주세요.", "권한 오류");
                lblStatus.Text = "❌ 권한 오류";
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError($"예상치 못한 오류가 발생했습니다.\n\n{ex.Message}", "오류");
                lblStatus.Text = "❌ 오류 발생";
            }
            finally
            {
                _isProcessing = false;
                UpdateButtonState(true);
            }
        }

        /// <summary>
        /// API 키 유효성 검증
        /// </summary>
        private bool ValidateApiKey()
        {
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                MessageBoxHelper.ShowWarning(
                    "OpenAI API 키가 설정되지 않았습니다.\n\n" +
                    "설정 메뉴에서 API 키를 등록해주세요.",
                    "API 키 필요"
                );
                return false;
            }
            return true;
        }

        /// <summary>
        /// AI 차트 분석 실행
        /// </summary>
        private async Task<ChartPredictionResult?> AnalyzeChartAsync()
        {
            var openAI = new OpenAIService(_apiKey);
            return await openAI.AnalyzeChartAsync(_resizedImagePath, "gpt-4o-mini");
        }

        /// <summary>
        /// 차트 시각화 실행
        /// </summary>
        private void VisualizeChart(ChartPredictionResult prediction, string sourcePath, string outputPath)
        {
            ChartVisualizer.DrawPrediction(
                sourcePath,
                outputPath,
                prediction.Support,
                prediction.Resistance,
                prediction.Trend,
                prediction.NextDirection,
                prediction.Probability
            );
        }

        /// <summary>
        /// 버튼 상태 업데이트
        /// </summary>
        private void UpdateButtonState(bool enabled)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateButtonState(enabled)));
                return;
            }

            btnSelectImage.Enabled = enabled;

            // 이미지가 선택되어 있을 때만 분석 버튼 활성화
            if (enabled && !string.IsNullOrEmpty(_originalImagePath))
            {
                btnApply.Enabled = true;
                btnApply.Text = "🚀 AI 분석 시작";
            }
            else
            {
                btnApply.Enabled = false;
                btnApply.Text = enabled ? "🚀 AI 분석 시작" : "⏳ 처리 중...";
            }
        }

        /// <summary>
        /// 임시 파일 정리
        /// </summary>
        private void CleanupTempFiles()
        {
            try
            {
                if (!string.IsNullOrEmpty(_resizedImagePath) && File.Exists(_resizedImagePath))
                {
                    File.Delete(_resizedImagePath);
                }
                // 결과 파일은 PictureBox가 사용 중이므로 제거하지 않음
            }
            catch
            {
                // 정리 실패해도 무시
            }
        }

        /// <summary>
        /// 원본 이미지 클릭 - 확대 표시
        /// </summary>
        private void PictureBoxOriginal_Click(object? sender, EventArgs e)
        {
            if (pictureBoxOriginal.Image != null)
            {
                ShowImageFullScreen(pictureBoxOriginal.Image, "원본 이미지");
            }
        }

        /// <summary>
        /// 결과 이미지 클릭 - 확대 표시
        /// </summary>
        private void PictureBoxResult_Click(object? sender, EventArgs e)
        {
            if (pictureBoxResult.Image != null)
            {
                ShowImageFullScreen(pictureBoxResult.Image, "AI 분석 결과");
            }
        }

        /// <summary>
        /// 이미지를 전체 화면으로 표시
        /// </summary>
        private void ShowImageFullScreen(Image image, string title)
        {
            var imageForm = new Form
            {
                Text = title,
                BackColor = Color.Black,
                FormBorderStyle = FormBorderStyle.Sizable,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new Size(Math.Min(image.Width + 20, 1400), Math.Min(image.Height + 60, 900)),
                KeyPreview = true
            };

            var pictureBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand
            };

            // ESC 키나 클릭으로 닫기
            imageForm.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    imageForm.Close();
                }
            };

            pictureBox.Click += (s, e) => imageForm.Close();

            // 더블클릭으로 실제 크기 보기
            pictureBox.DoubleClick += (s, e) =>
            {
                if (pictureBox.SizeMode == PictureBoxSizeMode.Zoom)
                {
                    pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox.Dock = DockStyle.None;
                }
                else
                {
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Dock = DockStyle.Fill;
                }
            };

            imageForm.Controls.Add(pictureBox);
            imageForm.ShowDialog();
        }

        private void aloneTextBox1_TextChanged(object sender, EventArgs e)
        {
            _apiKey = aloneTextBox1.Text;
        }
    }
}
