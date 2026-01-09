using hwh.Core;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hwh.Controls
{
    public partial class Maketest : UserControl
    {
        private static readonly HttpClient s_httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(120) // OpenAI 호출로 오래 걸릴 수 있으니 여유
        };

        private CancellationTokenSource? _cts;

        public Maketest()
        {
            InitializeComponent();
        }

        private async void airButton1_Click(object sender, EventArgs e)
        {
            airButton1.Enabled = false;
            richTextBox1.Clear();

            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            WaitForm? wait = null;

            try
            {
                // 대기창 띄우기 (부모 폼 기준 가운데)
                var parentForm = this.FindForm();
                wait = new WaitForm("요청 처리중...\n(완료되면 자동으로 닫힘)");
                if (parentForm != null) wait.Show(parentForm);
                else wait.Show();

                // UI가 바로 그려지게 잠깐 양보
                await Task.Yield();

                var json = await SendTestJsonAsync("ghkddngus77@naver.com", _cts.Token);
                richTextBox1.Text = json;
            }
            catch (OperationCanceledException)
            {
                richTextBox1.Text = "요청 취소됨";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (wait != null && !wait.IsDisposed) wait.Close();
                airButton1.Enabled = true;
            }
        }

        public async Task<string> SendTestJsonAsync(string email, CancellationToken ct = default)
        {
            var url = "https://hook.eu1.make.com/hi95xopvy1a8c8ykqgxrwcm1xj661ybb";

            var payload = new { test = email };
            var bodyJson = JsonSerializer.Serialize(payload);

            using var body = new StringContent(bodyJson, Encoding.UTF8, "application/json");

            using var response = await s_httpClient.PostAsync(url, body, ct).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}\n{content}");

            // JSON이면 예쁘게 포맷해서 리턴
            if (TryFormatJson(content, out var formatted))
                return formatted;

            return content;
        }

        private static bool TryFormatJson(string content, out string formatted)
        {
            try
            {
                using var doc = JsonDocument.Parse(content);
                formatted = JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions { WriteIndented = true });
                return true;
            }
            catch
            {
                formatted = content;
                return false;
            }
        }


    }
}
