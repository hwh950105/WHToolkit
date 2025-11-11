using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace hwh.Core
{
	/// <summary>
	/// System.Drawing을 사용한 차트 시각화 클래스
	/// </summary>
	public static class ChartVisualizer
	{
		public static void DrawPrediction(string imagePath, string savePath, int supportY, int resistanceY, List<Point> trendPoints, string nextDirection = "unknown", int probability = 0)
		{
			// 원본 차트 로드 (이미 1280x720으로 리사이즈된 상태)
			using (var baseImage = Image.FromFile(imagePath))
			using (var canvas = new Bitmap(baseImage.Width, baseImage.Height))
			using (var graphics = Graphics.FromImage(canvas))
			{
				// 고품질 렌더링 설정
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

				// 원본 이미지 그리기
				graphics.DrawImage(baseImage, 0, 0);

				int width = baseImage.Width;
				int height = baseImage.Height;

				// 펜과 브러시 (더 굵고 선명하게)
				using var supportPen = new Pen(Color.FromArgb(0, 255, 0), 3);
				using var resistancePen = new Pen(Color.FromArgb(255, 0, 0), 3);
				using var trendPen = new Pen(Color.FromArgb(255, 255, 0), 4);
				using var supportBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
				using var resistanceBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
				using var trendBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
				using var whiteBrush = new SolidBrush(Color.White);
				using var blackBrush = new SolidBrush(Color.Black);
				using var fontSmall = new Font("Segoe UI", 11, FontStyle.Bold);
				using var fontMedium = new Font("Segoe UI", 13, FontStyle.Bold);
				using var fontTime = new Font("Segoe UI", 10, FontStyle.Regular);

				// Y 좌표 보정 (이미지 크기가 다를 수 있으므로)
				float scaleY = (float)height / 720f;
				int adjustedSupportY = (int)(supportY * scaleY);
				int adjustedResistanceY = (int)(resistanceY * scaleY);

				// 마진 계산
				int leftMargin = (int)(width * 0.05f);
				int rightMargin = (int)(width * 0.95f);

				// 지지선 (초록) - 텍스트 배경 추가
				graphics.DrawLine(supportPen, new Point(leftMargin, adjustedSupportY), new Point(rightMargin, adjustedSupportY));
				
				string supportText = "지지선 (매수세 유입 구간)";
				var supportTextSize = graphics.MeasureString(supportText, fontSmall);
				var supportTextRect = new RectangleF(leftMargin + 10, Math.Max(5, adjustedSupportY - 30), 
					supportTextSize.Width + 10, supportTextSize.Height + 5);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, 0, 0, 0)), supportTextRect);
				graphics.DrawString(supportText, fontSmall, supportBrush, 
					new PointF(leftMargin + 15, Math.Max(7, adjustedSupportY - 28)));

				// 저항선 (빨강) - 텍스트 배경 추가
				graphics.DrawLine(resistancePen, new Point(leftMargin, adjustedResistanceY), new Point(rightMargin, adjustedResistanceY));
				
				string resistanceText = "저항선 (매도세 강한 구간)";
				var resistanceTextSize = graphics.MeasureString(resistanceText, fontSmall);
				var resistanceTextRect = new RectangleF(leftMargin + 10, Math.Max(5, adjustedResistanceY - 30), 
					resistanceTextSize.Width + 10, resistanceTextSize.Height + 5);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, 0, 0, 0)), resistanceTextRect);
				graphics.DrawString(resistanceText, fontSmall, resistanceBrush, 
					new PointF(leftMargin + 15, Math.Max(7, adjustedResistanceY - 28)));

				// 예측 추세선 (노랑)
				if (trendPoints != null && trendPoints.Count >= 2)
				{
					// 좌표 스케일 조정
					var adjustedPoints = trendPoints.Select(p => 
						new Point((int)(p.X * width / 1280f), (int)(p.Y * scaleY))).ToArray();
					
					graphics.DrawLines(trendPen, adjustedPoints);
					
					string trendText = "예상 추세선 (AI 시뮬레이션)";
					var trendTextSize = graphics.MeasureString(trendText, fontMedium);
					var trendTextRect = new RectangleF(width - trendTextSize.Width - 30, 20, 
						trendTextSize.Width + 10, trendTextSize.Height + 5);
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)), trendTextRect);
					graphics.DrawString(trendText, fontMedium, trendBrush, 
						new PointF(width - trendTextSize.Width - 25, 22));
				}

				// 다음 캔들 예측 표시 (우측 상단)
				if (!string.IsNullOrEmpty(nextDirection) && nextDirection != "unknown" && probability > 0)
				{
					string directionIcon = nextDirection.ToLower() == "up" ? "🔺" : "🔻";
					Color predictionColor = nextDirection.ToLower() == "up" ? Color.FromArgb(0, 255, 0) : Color.FromArgb(255, 0, 0);
					
					string predictionText = $"{directionIcon} 다음 캔들: {nextDirection.ToUpper()}";
					string probabilityText = $"확률: {probability}%";
					
					using var predictionFont = new Font("Segoe UI", 14, FontStyle.Bold);
					using var probabilityFont = new Font("Segoe UI", 12, FontStyle.Regular);
					using var predictionBrush = new SolidBrush(predictionColor);
					
					var predTextSize = graphics.MeasureString(predictionText, predictionFont);
					var probTextSize = graphics.MeasureString(probabilityText, probabilityFont);
					
					float maxWidth = Math.Max(predTextSize.Width, probTextSize.Width);
					var bgRect = new RectangleF(width - maxWidth - 40, 70, maxWidth + 20, 70);
					
					// 반투명 배경
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)), bgRect);
					
					// 테두리
					using var borderPen = new Pen(predictionColor, 2);
					graphics.DrawRectangle(borderPen, Rectangle.Round(bgRect));
					
					// 텍스트
					graphics.DrawString(predictionText, predictionFont, predictionBrush, 
						new PointF(width - maxWidth - 30, 80));
					graphics.DrawString(probabilityText, probabilityFont, whiteBrush, 
						new PointF(width - maxWidth - 30, 110));
				}

				// 예측 시점 표시 (배경 추가)
				string timeText = DateTime.Now.ToString("예측 시점: yyyy-MM-dd HH:mm");
				var timeTextSize = graphics.MeasureString(timeText, fontTime);
				var timeTextRect = new RectangleF(leftMargin + 10, height - 35, 
					timeTextSize.Width + 10, timeTextSize.Height + 5);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(180, 0, 0, 0)), timeTextRect);
				graphics.DrawString(timeText, fontTime, whiteBrush, new PointF(leftMargin + 15, height - 33));

				// 저장
				canvas.Save(savePath, ImageFormat.Png);
				Console.WriteLine($"✅ 예측 시각화 완료 → {savePath}");
			}
		}
	}
}


