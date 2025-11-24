using System;
using System.Diagnostics;
using System.Windows.Forms;
using Win32.Wrapper;

namespace hwh.Controls.Win32Controls
{
    /// <summary>
    /// 프로세스 관리 테스트 컨트롤
    /// </summary>
    public partial class ProcessManagementControl : UserControl
    {
        public ProcessManagementControl()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtExePath.Text))
                {
                    MessageBox.Show("실행 파일 경로를 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                uint pid = ProcessUtil.Run(txtExePath.Text, txtArgs.Text);
                lblStatus.Text = $"프로세스 실행 완료! PID: {pid}";
                MessageBox.Show($"프로세스가 실행되었습니다.\nPID: {pid}", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "실행 파일|*.exe|모든 파일|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtExePath.Text = dlg.FileName;
                }
            }
        }

        private void btnCheckRunning_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProcessName.Text))
                {
                    MessageBox.Show("프로세스 이름을 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool isRunning = ProcessUtil.IsRunning(txtProcessName.Text);
                lblStatus.Text = isRunning 
                    ? $"'{txtProcessName.Text}' 프로세스가 실행 중입니다." 
                    : $"'{txtProcessName.Text}' 프로세스가 실행 중이 아닙니다.";
                
                MessageBox.Show(lblStatus.Text, "확인", MessageBoxButtons.OK, 
                    isRunning ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProcessName.Text))
                {
                    MessageBox.Show("프로세스 이름을 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show(
                    $"'{txtProcessName.Text}' 프로세스를 종료하시겠습니까?", 
                    "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    ProcessUtil.Kill(txtProcessName.Text);
                    lblStatus.Text = $"'{txtProcessName.Text}' 프로세스를 종료했습니다.";
                    MessageBox.Show(lblStatus.Text, "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            RefreshProcessList();
        }

        private void RefreshProcessList()
        {
            listProcesses.Items.Clear();
            
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    var item = new ListViewItem(process.ProcessName);
                    item.SubItems.Add(process.Id.ToString());
                    item.SubItems.Add(process.MainWindowTitle);
                    try
                    {
                        item.SubItems.Add((process.WorkingSet64 / 1024 / 1024).ToString() + " MB");
                    }
                    catch
                    {
                        item.SubItems.Add("N/A");
                    }
                    listProcesses.Items.Add(item);
                }
                catch
                {
                    // 접근 권한이 없는 프로세스는 건너뛰기
                }
            }
            
            lblStatus.Text = $"총 {listProcesses.Items.Count}개의 프로세스를 찾았습니다.";
        }

        private void ProcessManagementControl_Load(object sender, EventArgs e)
        {
            RefreshProcessList();
        }

        private void listProcesses_DoubleClick(object sender, EventArgs e)
        {
            if (listProcesses.SelectedItems.Count > 0)
            {
                txtProcessName.Text = listProcesses.SelectedItems[0].Text;
            }
        }
    }
}

