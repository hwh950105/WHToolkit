using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace hwh.Services
{
	/// <summary>
	/// OpenAI REST API를 사용한 차트 분석 서비스
	/// </summary>
	public class OpenAIService
	{
		private readonly string _apiKey;
		private readonly HttpClient _httpClient;
		private const string API_ENDPOINT = "https://api.openai.com/v1/chat/completions";

		public OpenAIService(string apiKey)
		{
			_apiKey = apiKey;
			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
		}

		/// <summary>
		/// OpenAI Vision API를 사용하여 차트 이미지 분석
		/// </summary>
		public async Task<ChartPredictionResult> AnalyzeChartAsync(string chartPath, string modelName = "gpt-5o")
		{
			// 2️⃣ 실제 OpenAI API 호출
			try
			{
	

				// 이미지 → Base64 인코딩
				string base64Image = ConvertImageToBase64(chartPath);
				string imageDataUrl = $"data:image/png;base64,{base64Image}";

				// 메시지 구성
				var requestBody = new
				{
					model = modelName,
					messages = new object[]
					{
					new
					{
						role = "system",
						content =
							"You are a professional chart analysis AI. Analyze this 1280x720 pixel trading chart image.\n\n" +
							"CRITICAL - COORDINATE SYSTEM:\n" +
							"- Image dimensions: 1280 pixels (width) × 720 pixels (height)\n" +
							"- X axis: 0 (left edge) to 1280 (right edge)\n" +
							"- Y axis: 0 (TOP of image) to 720 (BOTTOM of image)\n" +
							"- Chart area typically spans Y: 50-670\n\n" +
							"TASK:\n" +
							"1. Support line: Find the LOWEST price level with strong buying pressure\n" +
							"   - Y coordinate should be LARGE (400-650) because low prices are at BOTTOM\n" +
							"2. Resistance line: Find the HIGHEST price level with strong selling pressure\n" +
							"   - Y coordinate should be SMALL (50-300) because high prices are at TOP\n" +
							"3. Trend line: Predict 5-7 points showing future price movement\n" +
							"   - X coordinates: evenly distributed from 100 to 1200\n" +
							"   - Y coordinates: between support and resistance lines\n" +
							"4. Next candle prediction:\n" +
							"   - Analyze the last candle and technical indicators\n" +
							"   - Predict if next candle will go UP or DOWN\n" +
							"   - Provide confidence probability (0-100%)\n\n" +
							"REQUIRED OUTPUT (JSON only, no markdown, no explanations):\n" +
							"{\n" +
							"  \"support\": <integer 400-670>,\n" +
							"  \"resistance\": <integer 50-350>,\n" +
							"  \"trend\": [[x1,y1], [x2,y2], [x3,y3], [x4,y4], [x5,y5]],\n" +
							"  \"nextDirection\": \"up\" or \"down\",\n" +
							"  \"probability\": <integer 0-100>\n" +
							"}\n\n" +
							"EXAMPLE (DO NOT copy, analyze the actual chart):\n" +
							"{\n" +
							"  \"support\": 550,\n" +
							"  \"resistance\": 180,\n" +
							"  \"trend\": [[150,200], [350,280], [550,320], [750,380], [950,420], [1150,450]],\n" +
							"  \"nextDirection\": \"up\",\n" +
							"  \"probability\": 68\n" +
							"}"
					},
					new
					{
						role = "user",
						content = new object[]
						{
							new { type = "text", text = "Analyze this chart and return the prediction result in JSON format. Remember: Y=0 is TOP, Y=720 is BOTTOM. Support line should have LARGE Y value (bottom), Resistance line should have SMALL Y value (top). Based on the chart pattern, momentum, and trend, predict if the NEXT CANDLE will go UP or DOWN and provide confidence probability (0-100%)." },
							new { type = "image_url", image_url = new { url = imageDataUrl } }
						}
					}
					},
					max_tokens = 500
				};

				// JSON 직렬화 및 요청
				var jsonRequest = JsonSerializer.Serialize(requestBody);
				var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

				// API 호출
				var response = await _httpClient.PostAsync(API_ENDPOINT, content);

				if (!response.IsSuccessStatusCode)
				{
					Console.WriteLine($"⚠️ API 응답 오류: {(int)response.StatusCode} {response.ReasonPhrase}");
					if ((int)response.StatusCode == 429)
						Console.WriteLine("❗ 요청이 너무 많습니다. 잠시 후 다시 시도하세요.");
					return null;
				}

				// 응답 내용 파싱
				var responseContent = await response.Content.ReadAsStringAsync();
				using var apiResponse = JsonDocument.Parse(responseContent);

				// 안전한 추출
				string jsonText = "{}";
				if (apiResponse.RootElement.TryGetProperty("choices", out var choices)
					&& choices.GetArrayLength() > 0)
				{
					var message = choices[0].GetProperty("message");
					jsonText = message.GetProperty("content").GetString() ?? "{}";
				}

				// Markdown ```json 제거
				jsonText = Regex.Replace(jsonText, @"```json|```", "").Trim();

				Console.WriteLine("📥 AI 응답 원문:");
				Console.WriteLine(jsonText);
				Console.WriteLine();

				// JSON 파싱 시도
				var result = ParsePredictionJson(jsonText);
				

				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"❌ OpenAI API 호출 실패: {ex.Message}");
				return null;
			}
		}

		/// <summary>
		/// 이미지 파일을 Base64 문자열로 변환
		/// </summary>
		private string ConvertImageToBase64(string imagePath)
		{
			byte[] imageBytes = File.ReadAllBytes(imagePath);
			return Convert.ToBase64String(imageBytes);
		}

		/// <summary>
		/// JSON 응답을 ChartPredictionResult로 파싱
		/// </summary>
		private ChartPredictionResult ParsePredictionJson(string jsonText)
		{
			try
			{
				var parsed = JsonDocument.Parse(jsonText).RootElement;

				int supportY = parsed.GetProperty("support").GetInt32();
				int resistanceY = parsed.GetProperty("resistance").GetInt32();

				var trendArray = parsed.GetProperty("trend");
				var trendPoints = new List<Point>();

				foreach (var point in trendArray.EnumerateArray())
				{
					int x = point[0].GetInt32();
					int y = point[1].GetInt32();
					trendPoints.Add(new Point(x, y));
				}

				// 다음 캔들 방향 및 확률 파싱
				string nextDirection = "unknown";
				int probability = 0;

				if (parsed.TryGetProperty("nextDirection", out var directionElement))
				{
					nextDirection = directionElement.GetString()?.ToLower() ?? "unknown";
				}

				if (parsed.TryGetProperty("probability", out var probabilityElement))
				{
					probability = probabilityElement.GetInt32();
					probability = Math.Clamp(probability, 0, 100); // 0-100 범위 제한
				}

				Console.WriteLine($"✅ JSON 파싱 완료 - 지지선: {supportY}, 저항선: {resistanceY}, 추세점: {trendPoints.Count}개");
				Console.WriteLine($"   다음 캔들 예측: {nextDirection.ToUpper()} ({probability}%)");

				return new ChartPredictionResult
				{
					Support = supportY,
					Resistance = resistanceY,
					Trend = trendPoints,
					NextDirection = nextDirection,
					Probability = probability
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine($"⚠️ JSON 파싱 실패: {ex.Message}");
				return null;
			}
		}

		/// <summary>
		/// 샘플 예측 데이터 반환 (API 실패 시 폴백)
		/// </summary>
		private ChartPredictionResult GetSamplePrediction()
		{
	
			return new ChartPredictionResult
			{
				Support = 600,
				Resistance = 200,
				Trend = new List<Point>
				{
					new Point(150, 500),
					new Point(400, 420),
					new Point(650, 380),
					new Point(900, 360),
					new Point(1150, 340)
				},
				NextDirection = "down",
				Probability = 65
			};
		}
	}

	/// <summary>
	/// 차트 예측 결과 데이터 모델
	/// </summary>
	public class ChartPredictionResult
	{
		public int Support { get; set; }
		public int Resistance { get; set; }
		public List<Point> Trend { get; set; } = new List<Point>();
		public string NextDirection { get; set; } = "unknown"; // "up", "down", "unknown"
		public int Probability { get; set; } = 0; // 0-100%
	}
}
