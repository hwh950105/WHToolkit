using System;
using System.Windows.Forms;
using Win32.Wrapper;
using static Win32.Win32Enums;

namespace hwh.Controls.Win32Controls
{
    /// <summary>
    /// 키보드 시뮬레이션 테스트 컨트롤
    /// </summary>
    public partial class KeyboardSimulatorControl : UserControl
    {
        public KeyboardSimulatorControl()
        {
            InitializeComponent();
        }

        private void btnSendKey_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboKey.SelectedItem == null)
                {
                    MessageBox.Show("키를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var key = (VirtualKey)comboKey.SelectedItem;
                
                // 3초 후 키 누르기
                lblStatus.Text = "3초 후 키를 누릅니다...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(3000);

                KeyboardSimulator.KeyPress(key);
                lblStatus.Text = $"키 '{key}' 전송 완료!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSendText_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtText.Text))
                {
                    MessageBox.Show("텍스트를 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3초 후 텍스트 입력
                lblStatus.Text = "3초 후 텍스트를 입력합니다... (메모장 등을 활성화하세요)";
                Application.DoEvents();
                System.Threading.Thread.Sleep(3000);

                KeyboardSimulator.SendString(txtText.Text);
                lblStatus.Text = "텍스트 전송 완료!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCtrlC_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "3초 후 Ctrl+C를 누릅니다...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(3000);

                KeyboardSimulator.Shortcut(VirtualKey.Control, VirtualKey.C);
                lblStatus.Text = "Ctrl+C 전송 완료!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCtrlV_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "3초 후 Ctrl+V를 누릅니다...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(3000);

                KeyboardSimulator.Shortcut(VirtualKey.Control, VirtualKey.V);
                lblStatus.Text = "Ctrl+V 전송 완료!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KeyboardSimulatorControl_Load(object sender, EventArgs e)
        {
            // VirtualKey enum 값들을 콤보박스에 추가
            comboKey.Items.AddRange(new object[]
            {
                VirtualKey.Enter,
                VirtualKey.Space,
                VirtualKey.Tab,
                VirtualKey.Escape,
                VirtualKey.A,
                VirtualKey.B,
                VirtualKey.C,
                VirtualKey.D,
                VirtualKey.E,
                VirtualKey.F,
                VirtualKey.F1,
                VirtualKey.F2,
                VirtualKey.F5,
                VirtualKey.Left,
                VirtualKey.Right,
                VirtualKey.Up,
                VirtualKey.Down
            });
            comboKey.SelectedIndex = 0;
        }
    }
}

