using System;
using System.Data;
using System.Windows.Forms;
using WHToolkit.Database;
using WHToolkit.Database.Common;

namespace sampleapp.UI.UserControls
{
    /// <summary>
    /// WHToolkit Database 기능 샘플 컨트롤
    /// </summary>
    public partial class DBControl : UserControl
    {
        private dynamic? _currentHelper;

        public DBControl()
        {
            InitializeComponent();
            InitializeDbTypes();
        }

        /// <summary>
        /// DB 타입 콤보박스 초기화
        /// </summary>
        private void InitializeDbTypes()
        {
            cmbDbType.SelectedIndex = 0;
            UpdateDefaultConnectionString();
        }

        /// <summary>
        /// DB 타입 변경 시 기본 연결 문자열 업데이트
        /// </summary>
        private void CmbDbType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateDefaultConnectionString();
        }

        /// <summary>
        /// 선택된 DB에 맞는 기본 연결 문자열 표시
        /// </summary>
        private void UpdateDefaultConnectionString()
        {
            txtConnectionString.Text = cmbDbType.SelectedIndex switch
            {
                0 => "Host=localhost;Port=5432;Database=testdb;Username=postgres;Password=password",
                1 => "Server=localhost;Database=testdb;User Id=sa;Password=password;TrustServerCertificate=True",
                2 => "Server=localhost;Database=testdb;Uid=root;Pwd=password;",
                3 => "Server=localhost;Database=testdb;Uid=root;Pwd=password;",
                4 => "Data Source=localhost:1521/testdb;User Id=system;Password=password;",
                _ => ""
            };
        }

        /// <summary>
        /// DB 연결 버튼 클릭
        /// </summary>
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // 이전 연결 정리
                if (_currentHelper is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                string connectionString = txtConnectionString.Text;
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    MessageBox.Show("연결 문자열을 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // DB 타입에 따라 적절한 Helper 생성
                _currentHelper = cmbDbType.SelectedIndex switch
                {
                    0 => new NpgHelper(connectionString),
                    1 => new MsSqlHelper(connectionString),
                    2 => new MySqlHelper(connectionString),
                    3 => new MariaDbHelper(connectionString),
                    4 => new OracleHelper(connectionString),
                    _ => throw new NotSupportedException("지원하지 않는 DB 타입입니다.")
                };

                // 간단한 연결 테스트 쿼리 실행
                TestConnection();

                lblStatus.Text = "연결됨 ●";
                lblStatus.ForeColor = System.Drawing.Color.Green;

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnExecute.Enabled = true;
                btnExecuteNonQuery.Enabled = true;
                btnExecuteScalar.Enabled = true;

                MessageBox.Show("데이터베이스 연결에 성공했습니다!", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "연결 안됨 ●";
                lblStatus.ForeColor = System.Drawing.Color.FromArgb(245, 108, 108);
                MessageBox.Show($"연결 실패: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 연결 해제 버튼 클릭
        /// </summary>
        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            if (_currentHelper is IDisposable disposable)
            {
                disposable.Dispose();
            }
            _currentHelper = null;

            lblStatus.Text = "연결 안됨 ●";
            lblStatus.ForeColor = System.Drawing.Color.FromArgb(245, 108, 108);

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnExecute.Enabled = false;
            btnExecuteNonQuery.Enabled = false;
            btnExecuteScalar.Enabled = false;

            gridResults.DataSource = null;
            MessageBox.Show("연결이 해제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Execute 버튼 클릭 - 실제 쿼리 실행 및 결과 표시
        /// </summary>
        private void BtnExecute_Click(object sender, EventArgs e)
        {
            if (_currentHelper == null)
            {
                MessageBox.Show("먼저 데이터베이스에 연결해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string query = txtQuery.Text.Trim();
                if (string.IsNullOrWhiteSpace(query))
                {
                    MessageBox.Show("쿼리를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Execute로 DataTable 가져오기
                var dt = _currentHelper.Execute<DataTable>(CommandType.Text, query);

                gridResults.DataSource = dt;
                MessageBox.Show($"{dt.Rows.Count}개의 행이 조회되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"쿼리 실행 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ExecuteNonQuery 버튼 클릭 - INSERT/UPDATE/DELETE 실행
        /// </summary>
        private void BtnExecuteNonQuery_Click(object sender, EventArgs e)
        {
            if (_currentHelper == null)
            {
                MessageBox.Show("먼저 데이터베이스에 연결해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string query = txtQuery.Text.Trim();
                if (string.IsNullOrWhiteSpace(query))
                {
                    MessageBox.Show("쿼리를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int affected = _currentHelper.ExecuteNonQuery(CommandType.Text, query);
                MessageBox.Show($"{affected}개의 행이 영향을 받았습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"쿼리 실행 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ExecuteScalar 버튼 클릭 - 단일 값 반환
        /// </summary>
        private void BtnExecuteScalar_Click(object sender, EventArgs e)
        {
            if (_currentHelper == null)
            {
                MessageBox.Show("먼저 데이터베이스에 연결해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string query = txtQuery.Text.Trim();
                if (string.IsNullOrWhiteSpace(query))
                {
                    MessageBox.Show("쿼리를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = _currentHelper.ExecuteScalar<object>(CommandType.Text, query);
                MessageBox.Show($"결과: {result}", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"쿼리 실행 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 연결 테스트 쿼리 실행
        /// </summary>
        private void TestConnection()
        {
            string testQuery = cmbDbType.SelectedIndex switch
            {
                0 => "SELECT 1", // PostgreSQL
                1 => "SELECT 1", // MS SQL
                2 => "SELECT 1", // MySQL
                3 => "SELECT 1", // MariaDB
                4 => "SELECT 1 FROM DUAL", // Oracle
                _ => "SELECT 1"
            };

            _currentHelper.Execute<TestResult>(CommandType.Text, testQuery);
        }


        /// <summary>
        /// 테스트용 결과 클래스
        /// </summary>
        private class TestResult
        {
            public int Column1 { get; set; }
        }
    }
}
