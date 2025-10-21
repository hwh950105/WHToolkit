using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WHToolkit.IO;

namespace sampleapp.UI.UserControls
{
    /// <summary>
    /// WHToolkit IO 기능 샘플 컨트롤 (JSON, INI 파일 처리)
    /// </summary>
    public partial class IOControl : UserControl
    {
        private string _currentFilePath = "sample_data.json";

        public IOControl()
        {
            InitializeComponent();
            InitializeUI();
        }

        /// <summary>
        /// UI 초기화
        /// </summary>
        private void InitializeUI()
        {
            cmbFileType.SelectedIndex = 0;
            SetupDataGridView();
        }

        /// <summary>
        /// 파일 타입 변경 이벤트
        /// </summary>
        private void CmbFileType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbFileType.SelectedIndex == 0) // JSON
            {
                txtFilePath.Text = "sample_data.json";
                SetupDataGridViewForJson();
            }
            else // INI
            {
                txtFilePath.Text = "sample_config.ini";
                SetupDataGridViewForIni();
            }
            _currentFilePath = txtFilePath.Text;
        }

        /// <summary>
        /// DataGridView 컬럼 설정 - JSON용
        /// </summary>
        private void SetupDataGridView()
        {
            SetupDataGridViewForJson();
        }

        /// <summary>
        /// DataGridView 컬럼 설정 - JSON용
        /// </summary>
        private void SetupDataGridViewForJson()
        {
            gridData.Columns.Clear();
            gridData.Columns.Add("Name", "이름");
            gridData.Columns.Add("Email", "이메일");
            gridData.Columns.Add("Age", "나이");
            gridData.Columns.Add("JoinDate", "가입일");
        }

        /// <summary>
        /// DataGridView 컬럼 설정 - INI용
        /// </summary>
        private void SetupDataGridViewForIni()
        {
            gridData.Columns.Clear();
            gridData.Columns.Add("Section", "섹션");
            gridData.Columns.Add("Key", "키");
            gridData.Columns.Add("Value", "값");
            gridData.Columns.Add("Note", "비고");
        }

        /// <summary>
        /// JSON 읽기 버튼 클릭
        /// </summary>
        private async void BtnJsonRead_Click(object? sender, EventArgs e)
        {
            try
            {
                _currentFilePath = txtFilePath.Text;

                // JSON 파일이 없으면 샘플 데이터 생성
                if (!JsonHelper.Exists(_currentFilePath))
                {
                    await CreateSampleJsonData();
                }

                // JSON 파일 읽기
                var users = await JsonHelper.ReadAsync<List<UserData>>(_currentFilePath);

                if (users != null)
                {
                    // DataGridView에 표시
                    gridData.Rows.Clear();
                    foreach (var user in users)
                    {
                        gridData.Rows.Add(
                            user.Name,
                            user.Email,
                            user.Age,
                            user.JoinDate.ToString("yyyy-MM-dd")
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"JSON 파일 읽기 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// JSON 쓰기 버튼 클릭
        /// </summary>
        private async void BtnJsonWrite_Click(object? sender, EventArgs e)
        {
            try
            {
                _currentFilePath = txtFilePath.Text;
                await CreateSampleJsonData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"JSON 파일 쓰기 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// INI 읽기 버튼 클릭
        /// </summary>
        private void BtnIniRead_Click(object? sender, EventArgs e)
        {
            try
            {
                _currentFilePath = txtFilePath.Text;

                // INI 파일이 없으면 샘플 데이터 생성
                if (!System.IO.File.Exists(_currentFilePath))
                {
                    CreateSampleIniData();
                }

                // INI 파일 읽기
                var iniHelper = new IniHelper(_currentFilePath);

                // DataGridView에 표시
                gridData.Rows.Clear();

                // Database 섹션 읽기
                var dbSection = iniHelper.GetSection("Database");
                if (dbSection.Count > 0)
                {
                    foreach (var kvp in dbSection)
                    {
                        gridData.Rows.Add("Database", kvp.Key, kvp.Value, GetKeyDescription(kvp.Key));
                    }
                }

                // Settings 섹션 읽기
                var settingsSection = iniHelper.GetSection("Settings");
                if (settingsSection.Count > 0)
                {
                    foreach (var kvp in settingsSection)
                    {
                        gridData.Rows.Add("Settings", kvp.Key, kvp.Value, GetKeyDescription(kvp.Key));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"INI 파일 읽기 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 키 설명 반환
        /// </summary>
        private string GetKeyDescription(string key)
        {
            return key switch
            {
                "Host" => "서버 주소",
                "Port" => "포트 번호",
                "Database" => "데이터베이스명",
                "User" => "사용자명",
                "AppName" => "애플리케이션명",
                "Version" => "버전",
                "Enabled" => "활성화 여부",
                _ => ""
            };
        }

        /// <summary>
        /// INI 쓰기 버튼 클릭
        /// </summary>
        private void BtnIniWrite_Click(object? sender, EventArgs e)
        {
            try
            {
                _currentFilePath = txtFilePath.Text;
                CreateSampleIniData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"INI 파일 쓰기 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 샘플 JSON 데이터 생성
        /// </summary>
        private async Task CreateSampleJsonData()
        {
            var sampleUsers = new List<UserData>
            {
                new UserData { Name = "김철수", Email = "kim@example.com", Age = 30, JoinDate = DateTime.Now.AddDays(-100) },
                new UserData { Name = "이영희", Email = "lee@example.com", Age = 25, JoinDate = DateTime.Now.AddDays(-50) },
                new UserData { Name = "박민수", Email = "park@example.com", Age = 35, JoinDate = DateTime.Now.AddDays(-200) },
                new UserData { Name = "최지훈", Email = "choi@example.com", Age = 28, JoinDate = DateTime.Now.AddDays(-150) }
            };

            await JsonHelper.WriteAsync(_currentFilePath, sampleUsers);
        }

        /// <summary>
        /// 샘플 INI 데이터 생성
        /// </summary>
        private void CreateSampleIniData()
        {
            var iniHelper = new IniHelper(_currentFilePath);

            // Database 섹션
            iniHelper.Write("Database", "Host", "localhost");
            iniHelper.Write("Database", "Port", "5432");
            iniHelper.Write("Database", "Database", "mydb");
            iniHelper.Write("Database", "User", "postgres");

            // Settings 섹션
            iniHelper.Write("Settings", "AppName", "WHToolkit Sample");
            iniHelper.Write("Settings", "Version", "1.0.0");
            iniHelper.Write("Settings", "Enabled", "true");
        }

        private void ProductionControl_Load(object sender, EventArgs e)
        {
            // 로드 시 아무 작업 없음
        }

        /// <summary>
        /// 사용자 데이터 모델
        /// </summary>
        public class UserData
        {
            public string Name { get; set; } = "";
            public string Email { get; set; } = "";
            public int Age { get; set; }
            public DateTime JoinDate { get; set; }
        }
    }
}
