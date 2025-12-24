using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using Win32.Wrapper;

namespace hwh.Controls.Win32Controls
{
    /// <summary>
    /// 윈도우 관리 테스트 컨트롤
    /// </summary>
    public partial class WindowManagementControl : UserControl
    {
        private IntPtr targetWindowHandle = IntPtr.Zero;
        private Dictionary<int, List<int>> processPortMap = new Dictionary<int, List<int>>();

        public WindowManagementControl()
        {
            InitializeComponent();
        }

        private void WindowManagementControl_Load(object sender, EventArgs e)
        {
            RefreshWindowList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshWindowList();
        }

        private void RefreshWindowList()
        {
            listWindows.Items.Clear();
            
            // netstat을 사용하여 프로세스별 포트 정보 가져오기
            BuildProcessPortMap();
            
            // 실행 중인 모든 프로세스의 메인 윈도우를 목록에 추가
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    if (process.MainWindowHandle != IntPtr.Zero && !string.IsNullOrWhiteSpace(process.MainWindowTitle))
                    {
                        var item = new ListViewItem(process.MainWindowTitle);
                        item.SubItems.Add(process.ProcessName);
                        item.SubItems.Add(process.Id.ToString());
                        item.SubItems.Add(process.MainWindowHandle.ToString("X"));
                        
                        // 프로세스가 사용하는 포트 찾기
                        string ports = GetProcessPorts(process.Id);
                        item.SubItems.Add(ports);
                        
                        item.Tag = process.MainWindowHandle;
                        listWindows.Items.Add(item);
                    }
                }
                catch
                {
                    // 접근 권한이 없는 프로세스는 건너뛰기
                }
            }

            lblStatus.Text = $"총 {listWindows.Items.Count}개의 윈도우를 찾았습니다.";
        }

        private void BuildProcessPortMap()
        {
            processPortMap.Clear();
            
            try
            {
                // netstat -ano를 실행하여 프로세스별 포트 정보 가져오기
                var startInfo = new ProcessStartInfo
                {
                    FileName = "netstat",
                    Arguments = "-ano",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(startInfo))
                {
                    if (process != null)
                    {
                        string output = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();

                        // 출력 파싱
                        var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var line in lines)
                        {
                            // TCP나 UDP로 시작하는 라인만 처리
                            if (line.Trim().StartsWith("TCP") || line.Trim().StartsWith("UDP"))
                            {
                                var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                if (parts.Length >= 4)
                                {
                                    // 마지막 부분이 PID
                                    if (int.TryParse(parts[parts.Length - 1], out int pid))
                                    {
                                        // 두 번째 부분이 로컬 주소:포트
                                        var addressPort = parts[1].Split(':');
                                        if (addressPort.Length >= 2 && int.TryParse(addressPort[addressPort.Length - 1], out int port))
                                        {
                                            if (!processPortMap.ContainsKey(pid))
                                            {
                                                processPortMap[pid] = new List<int>();
                                            }
                                            if (!processPortMap[pid].Contains(port))
                                            {
                                                processPortMap[pid].Add(port);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // netstat 실행 실패 시 무시
            }
        }

        private string GetProcessPorts(int processId)
        {
            try
            {
                if (processPortMap.ContainsKey(processId) && processPortMap[processId].Count > 0)
                {
                    var ports = processPortMap[processId].OrderBy(p => p).Take(5).Select(p => p.ToString());
                    if (processPortMap[processId].Count > 5)
                    {
                        return string.Join(", ", ports) + "...";
                    }
                    else
                    {
                        return string.Join(", ", ports);
                    }
                }
                return "-";
            }
            catch
            {
                return "-";
            }
        }

        private void listWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listWindows.SelectedItems.Count > 0)
            {
                var selectedItem = listWindows.SelectedItems[0];
                if (selectedItem.Tag is IntPtr handle && handle != IntPtr.Zero)
                {
                    targetWindowHandle = handle;
                }
                else
                {
                    targetWindowHandle = IntPtr.Zero;
                }

                txtWindowTitle.Text = selectedItem.Text;
                lblStatus.Text = $"선택된 윈도우: {selectedItem.Text}";
            }
        }

        private void btnFindByTitle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtWindowTitle.Text))
                {
                    MessageBox.Show("윈도우 제목을 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                targetWindowHandle = WindowUtil.FindWindowByTitle(txtWindowTitle.Text);
                
                if (targetWindowHandle == IntPtr.Zero)
                {
                    lblStatus.Text = $"'{txtWindowTitle.Text}' 윈도우를 찾을 수 없습니다.";
                    MessageBox.Show("해당 제목의 윈도우를 찾을 수 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = $"윈도우를 찾았습니다. 핸들: 0x{targetWindowHandle:X}";
                    
                    // 윈도우 정보 가져오기
                    Rectangle rect = WindowUtil.GetWindowRect(targetWindowHandle);
                    lblWindowInfo.Text = $"위치: ({rect.X}, {rect.Y}), 크기: {rect.Width}x{rect.Height}";
                }
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "윈도우 찾기 실패");
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowWindow_Click(object sender, EventArgs e)
        {
            if (targetWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("먼저 윈도우를 선택하거나 찾으세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                WindowUtil.ShowWindow(targetWindowHandle);
                lblStatus.Text = "윈도우를 표시했습니다.";
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "윈도우 표시 실패");
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHideWindow_Click(object sender, EventArgs e)
        {
            if (targetWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("먼저 윈도우를 선택하거나 찾으세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                WindowUtil.HideWindow(targetWindowHandle);
                lblStatus.Text = "윈도우를 숨겼습니다.";
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "윈도우 숨기기 실패");
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSetTopMost_Click(object sender, EventArgs e)
        {
            if (targetWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("먼저 윈도우를 선택하거나 찾으세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                WindowUtil.SetTopMost(targetWindowHandle, true);
                lblStatus.Text = "윈도우를 항상 위로 설정했습니다.";
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "항상 위 설정 실패");
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUnsetTopMost_Click(object sender, EventArgs e)
        {
            if (targetWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("먼저 윈도우를 선택하거나 찾으세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                WindowUtil.SetTopMost(targetWindowHandle, false);
                lblStatus.Text = "윈도우의 항상 위 설정을 해제했습니다.";
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "항상 위 해제 실패");
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetRect_Click(object sender, EventArgs e)
        {
            if (targetWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("먼저 윈도우를 선택하거나 찾으세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Rectangle rect = WindowUtil.GetWindowRect(targetWindowHandle);
                lblWindowInfo.Text = $"위치: ({rect.X}, {rect.Y}), 크기: {rect.Width}x{rect.Height}";
                lblStatus.Text = "윈도우 정보를 가져왔습니다.";
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "윈도우 정보 가져오기 실패");
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

