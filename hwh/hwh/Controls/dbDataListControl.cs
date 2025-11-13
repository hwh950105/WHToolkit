using System;
using System.Windows.Forms;
using hwh.Core;

namespace hwh.Controls
{
    public partial class dbDataListControl : UserControl
    {
        public dbDataListControl()
        {
            InitializeComponent();
            LoadUserList();
        }
        private void LoadUserList()
        {
            try
            {

                // DB 로직은 DbCall에서 처리
                var dt = DbCall.GetAllUsersAsDataTable();

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();


                // DataGridView에 바로 바인딩
                dataGridView1.DataSource = dt;

                // 보기 좋게 옵션 설정
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;

                // // 컬럼 너비 수동 설정 (성능 최적화)
                // if (dataGridView1.Columns.Count > 0)
                // {
                //     dataGridView1.Columns["ID"].Width = 60;
                //     dataGridView1.Columns["아이디"].Width = 120;
                //     dataGridView1.Columns["이름"].Width = 100;
                //     dataGridView1.Columns["이메일"].Width = 180;
                //     dataGridView1.Columns["연락처"].Width = 130;
                //     dataGridView1.Columns["권한"].Width = 80;
                //     dataGridView1.Columns["상태"].Width = 80;
                //     dataGridView1.Columns["가입일"].Width = 150;
                // }

                lblCount.Text = $"총 {dt.Rows.Count}명의 사용자";
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError($"사용자 목록을 불러오는데 실패했습니다.\n{ex.Message}", "오류");
                lblCount.Text = "총 0명의 사용자";
                dataGridView1.DataSource = null;
            }
        }
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            postdbcall.postdbcallmain();
        }
        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBoxHelper.ShowWarning("삭제할 사용자를 선택해주세요.", "알림");
                return;
            }

            var selectedRow = dataGridView1.SelectedRows[0];

            // DataSource 바인딩 방식에서는 컬럼명으로 접근
            int userId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
            string username = selectedRow.Cells["아이디"].Value?.ToString() ?? "";

            var result = MessageBoxHelper.ShowQuestion(
                $"'{username}' 사용자를 삭제하시겠습니까?\n(상태가 '삭제됨'으로 변경됩니다)",
                "사용자 삭제"
            );

            if (result == DialogResult.Yes)
            {
                if (DbCall.DeleteUser(userId))
                {
                    MessageBoxHelper.ShowSuccess("사용자가 삭제되었습니다.", "완료");
                    LoadUserList();
                }
                else
                {
                    MessageBoxHelper.ShowError("사용자 삭제에 실패했습니다.", "오류");
                }
            }
        }

    }
}

