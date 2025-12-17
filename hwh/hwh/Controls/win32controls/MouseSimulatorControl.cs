using System;
using System.Drawing;
using System.Windows.Forms;
using Win32.Wrapper;

namespace hwh.Controls.Win32Controls
{
    /// <summary>
    /// 마우스 시뮬레이션 테스트 컨트롤
    /// </summary>
    public partial class MouseSimulatorControl : UserControl
    {
        public MouseSimulatorControl()
        {
            InitializeComponent();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMoveX.Text, out int x) || !int.TryParse(txtMoveY.Text, out int y))
                {
                    MessageBox.Show("올바른 좌표를 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblStatus.Text = "2초 후 마우스를 이동합니다...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(2000);

                MouseSimulator.Move(x, y);
                lblStatus.Text = $"마우스를 ({x}, {y})로 이동했습니다.";
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "마우스 이동 실패");
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLeftClick_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtClickX.Text, out int x) || !int.TryParse(txtClickY.Text, out int y))
                {
                    MessageBox.Show("올바른 좌표를 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblStatus.Text = "2초 후 왼쪽 클릭합니다...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(2000);

                MouseSimulator.LeftClick(x, y);
                lblStatus.Text = $"({x}, {y})에서 왼쪽 클릭했습니다.";
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "마우스 왼쪽 클릭 실패");
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRightClick_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtClickX.Text, out int x) || !int.TryParse(txtClickY.Text, out int y))
                {
                    MessageBox.Show("올바른 좌표를 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblStatus.Text = "2초 후 오른쪽 클릭합니다...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(2000);

                MouseSimulator.RightClick(x, y);
                lblStatus.Text = $"({x}, {y})에서 오른쪽 클릭했습니다.";
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "마우스 오른쪽 클릭 실패");
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDrag_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtDragStartX.Text, out int x1) || 
                    !int.TryParse(txtDragStartY.Text, out int y1) ||
                    !int.TryParse(txtDragEndX.Text, out int x2) || 
                    !int.TryParse(txtDragEndY.Text, out int y2))
                {
                    MessageBox.Show("올바른 좌표를 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lblStatus.Text = "2초 후 드래그를 시작합니다...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(2000);

                MouseSimulator.Drag(x1, y1, x2, y2);
                lblStatus.Text = $"({x1}, {y1})에서 ({x2}, {y2})로 드래그했습니다.";
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "마우스 드래그 실패");
                MessageBox.Show($"오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetCurrentPos_Click(object sender, EventArgs e)
        {
            var pos = Cursor.Position;
            txtMoveX.Text = pos.X.ToString();
            txtMoveY.Text = pos.Y.ToString();
            txtClickX.Text = pos.X.ToString();
            txtClickY.Text = pos.Y.ToString();
            lblStatus.Text = $"현재 마우스 위치: ({pos.X}, {pos.Y})";
        }
    }
}

