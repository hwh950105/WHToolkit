namespace hwh.Controls.Win32Controls
{
    partial class MouseSimulatorControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpMove;
        private System.Windows.Forms.GroupBox grpClick;
        private System.Windows.Forms.GroupBox grpDrag;
        private System.Windows.Forms.Label lblMoveX;
        private System.Windows.Forms.Label lblMoveY;
        private System.Windows.Forms.TextBox txtMoveX;
        private System.Windows.Forms.TextBox txtMoveY;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Label lblClickX;
        private System.Windows.Forms.Label lblClickY;
        private System.Windows.Forms.TextBox txtClickX;
        private System.Windows.Forms.TextBox txtClickY;
        private System.Windows.Forms.Button btnLeftClick;
        private System.Windows.Forms.Button btnRightClick;
        private System.Windows.Forms.Label lblDragStart;
        private System.Windows.Forms.Label lblDragEnd;
        private System.Windows.Forms.TextBox txtDragStartX;
        private System.Windows.Forms.TextBox txtDragStartY;
        private System.Windows.Forms.TextBox txtDragEndX;
        private System.Windows.Forms.TextBox txtDragEndY;
        private System.Windows.Forms.Button btnDrag;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnGetCurrentPos;
        private System.Windows.Forms.Label lblInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            grpMove = new GroupBox();
            btnMove = new Button();
            txtMoveY = new TextBox();
            txtMoveX = new TextBox();
            lblMoveY = new Label();
            lblMoveX = new Label();
            grpClick = new GroupBox();
            btnRightClick = new Button();
            btnLeftClick = new Button();
            txtClickY = new TextBox();
            txtClickX = new TextBox();
            lblClickY = new Label();
            lblClickX = new Label();
            grpDrag = new GroupBox();
            btnDrag = new Button();
            txtDragEndY = new TextBox();
            txtDragEndX = new TextBox();
            lblDragEnd = new Label();
            txtDragStartY = new TextBox();
            txtDragStartX = new TextBox();
            lblDragStart = new Label();
            lblStatus = new Label();
            btnGetCurrentPos = new Button();
            lblInfo = new Label();
            grpMove.SuspendLayout();
            grpClick.SuspendLayout();
            grpDrag.SuspendLayout();
            SuspendLayout();
            // 
            // grpMove
            // 
            grpMove.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpMove.Controls.Add(btnMove);
            grpMove.Controls.Add(txtMoveY);
            grpMove.Controls.Add(txtMoveX);
            grpMove.Controls.Add(lblMoveY);
            grpMove.Controls.Add(lblMoveX);
            grpMove.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
            grpMove.Location = new Point(20, 70);
            grpMove.MinimumSize = new Size(900, 130);
            grpMove.Name = "grpMove";
            grpMove.Size = new Size(1754, 130);
            grpMove.TabIndex = 0;
            grpMove.TabStop = false;
            grpMove.Text = "마우스 이동";
            // 
            // btnMove
            // 
            btnMove.Location = new Point(620, 35);
            btnMove.Name = "btnMove";
            btnMove.Size = new Size(100, 35);
            btnMove.TabIndex = 4;
            btnMove.Text = "이동";
            btnMove.UseVisualStyleBackColor = true;
            btnMove.Click += btnMove_Click;
            // 
            // txtMoveY
            // 
            txtMoveY.Location = new Point(310, 38);
            txtMoveY.Name = "txtMoveY";
            txtMoveY.Size = new Size(100, 29);
            txtMoveY.TabIndex = 3;
            txtMoveY.Text = "300";
            // 
            // txtMoveX
            // 
            txtMoveX.Location = new Point(80, 38);
            txtMoveX.Name = "txtMoveX";
            txtMoveX.Size = new Size(100, 29);
            txtMoveX.TabIndex = 1;
            txtMoveX.Text = "500";
            // 
            // lblMoveY
            // 
            lblMoveY.AutoSize = true;
            lblMoveY.Location = new Point(270, 41);
            lblMoveY.Name = "lblMoveY";
            lblMoveY.Size = new Size(24, 21);
            lblMoveY.TabIndex = 2;
            lblMoveY.Text = "Y:";
            // 
            // lblMoveX
            // 
            lblMoveX.AutoSize = true;
            lblMoveX.Location = new Point(20, 41);
            lblMoveX.Name = "lblMoveX";
            lblMoveX.Size = new Size(24, 21);
            lblMoveX.TabIndex = 0;
            lblMoveX.Text = "X:";
            // 
            // grpClick
            // 
            grpClick.Controls.Add(btnRightClick);
            grpClick.Controls.Add(btnLeftClick);
            grpClick.Controls.Add(txtClickY);
            grpClick.Controls.Add(txtClickX);
            grpClick.Controls.Add(lblClickY);
            grpClick.Controls.Add(lblClickX);
            grpClick.Location = new Point(20, 200);
            grpClick.Name = "grpClick";
            grpClick.Size = new Size(750, 100);
            grpClick.TabIndex = 1;
            grpClick.TabStop = false;
            grpClick.Text = "클릭";
            // 
            // btnRightClick
            // 
            btnRightClick.Location = new Point(620, 50);
            btnRightClick.Name = "btnRightClick";
            btnRightClick.Size = new Size(100, 35);
            btnRightClick.TabIndex = 6;
            btnRightClick.Text = "오른쪽";
            btnRightClick.UseVisualStyleBackColor = true;
            btnRightClick.Click += btnRightClick_Click;
            // 
            // btnLeftClick
            // 
            btnLeftClick.Location = new Point(500, 50);
            btnLeftClick.Name = "btnLeftClick";
            btnLeftClick.Size = new Size(100, 35);
            btnLeftClick.TabIndex = 5;
            btnLeftClick.Text = "왼쪽";
            btnLeftClick.UseVisualStyleBackColor = true;
            btnLeftClick.Click += btnLeftClick_Click;
            // 
            // txtClickY
            // 
            txtClickY.Location = new Point(310, 38);
            txtClickY.Name = "txtClickY";
            txtClickY.Size = new Size(100, 29);
            txtClickY.TabIndex = 3;
            txtClickY.Text = "300";
            // 
            // txtClickX
            // 
            txtClickX.Location = new Point(80, 38);
            txtClickX.Name = "txtClickX";
            txtClickX.Size = new Size(100, 29);
            txtClickX.TabIndex = 1;
            txtClickX.Text = "500";
            // 
            // lblClickY
            // 
            lblClickY.AutoSize = true;
            lblClickY.Location = new Point(270, 41);
            lblClickY.Name = "lblClickY";
            lblClickY.Size = new Size(23, 21);
            lblClickY.TabIndex = 2;
            lblClickY.Text = "Y:";
            // 
            // lblClickX
            // 
            lblClickX.AutoSize = true;
            lblClickX.Location = new Point(20, 41);
            lblClickX.Name = "lblClickX";
            lblClickX.Size = new Size(24, 21);
            lblClickX.TabIndex = 0;
            lblClickX.Text = "X:";
            // 
            // grpDrag
            // 
            grpDrag.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDrag.Controls.Add(btnDrag);
            grpDrag.Controls.Add(txtDragEndY);
            grpDrag.Controls.Add(txtDragEndX);
            grpDrag.Controls.Add(lblDragEnd);
            grpDrag.Controls.Add(txtDragStartY);
            grpDrag.Controls.Add(txtDragStartX);
            grpDrag.Controls.Add(lblDragStart);
            grpDrag.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
            grpDrag.Location = new Point(20, 390);
            grpDrag.MinimumSize = new Size(900, 140);
            grpDrag.Name = "grpDrag";
            grpDrag.Size = new Size(1754, 140);
            grpDrag.TabIndex = 2;
            grpDrag.TabStop = false;
            grpDrag.Text = "드래그";
            // 
            // btnDrag
            // 
            btnDrag.Location = new Point(620, 40);
            btnDrag.Name = "btnDrag";
            btnDrag.Size = new Size(100, 60);
            btnDrag.TabIndex = 6;
            btnDrag.Text = "드래그";
            btnDrag.UseVisualStyleBackColor = true;
            btnDrag.Click += btnDrag_Click;
            // 
            // txtDragEndY
            // 
            txtDragEndY.Location = new Point(310, 73);
            txtDragEndY.Name = "txtDragEndY";
            txtDragEndY.Size = new Size(100, 29);
            txtDragEndY.TabIndex = 5;
            txtDragEndY.Text = "400";
            // 
            // txtDragEndX
            // 
            txtDragEndX.Location = new Point(110, 73);
            txtDragEndX.Name = "txtDragEndX";
            txtDragEndX.Size = new Size(100, 29);
            txtDragEndX.TabIndex = 4;
            txtDragEndX.Text = "700";
            // 
            // lblDragEnd
            // 
            lblDragEnd.AutoSize = true;
            lblDragEnd.Location = new Point(20, 76);
            lblDragEnd.Name = "lblDragEnd";
            lblDragEnd.Size = new Size(78, 21);
            lblDragEnd.TabIndex = 3;
            lblDragEnd.Text = "끝 (X, Y):";
            // 
            // txtDragStartY
            // 
            txtDragStartY.Location = new Point(310, 33);
            txtDragStartY.Name = "txtDragStartY";
            txtDragStartY.Size = new Size(100, 29);
            txtDragStartY.TabIndex = 2;
            txtDragStartY.Text = "200";
            // 
            // txtDragStartX
            // 
            txtDragStartX.Location = new Point(110, 33);
            txtDragStartX.Name = "txtDragStartX";
            txtDragStartX.Size = new Size(100, 29);
            txtDragStartX.TabIndex = 1;
            txtDragStartX.Text = "400";
            // 
            // lblDragStart
            // 
            lblDragStart.AutoSize = true;
            lblDragStart.Location = new Point(20, 36);
            lblDragStart.Name = "lblDragStart";
            lblDragStart.Size = new Size(94, 21);
            lblDragStart.TabIndex = 0;
            lblDragStart.Text = "시작 (X, Y):";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.Blue;
            lblStatus.Location = new Point(20, 460);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(56, 15);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "대기 중...";
            // 
            // btnGetCurrentPos
            // 
            btnGetCurrentPos.Location = new Point(20, 40);
            btnGetCurrentPos.Name = "btnGetCurrentPos";
            btnGetCurrentPos.Size = new Size(200, 30);
            btnGetCurrentPos.TabIndex = 4;
            btnGetCurrentPos.Text = "📍 현재 마우스 위치 가져오기";
            btnGetCurrentPos.UseVisualStyleBackColor = true;
            btnGetCurrentPos.Click += btnGetCurrentPos_Click;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("맑은 고딕", 9F);
            lblInfo.ForeColor = Color.DarkOrange;
            lblInfo.Location = new Point(20, 10);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(209, 15);
            lblInfo.TabIndex = 5;
            lblInfo.Text = "⚠️ 버튼 클릭 후 2초 후에 동작합니다.";
            // 
            // MouseSimulatorControl
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(lblInfo);
            Controls.Add(btnGetCurrentPos);
            Controls.Add(lblStatus);
            Controls.Add(grpDrag);
            Controls.Add(grpClick);
            Controls.Add(grpMove);
            Font = new Font("맑은 고딕", 12F);
            MinimumSize = new Size(950, 600);
            Name = "MouseSimulatorControl";
            Size = new Size(1854, 810);
            grpMove.ResumeLayout(false);
            grpMove.PerformLayout();
            grpClick.ResumeLayout(false);
            grpClick.PerformLayout();
            grpDrag.ResumeLayout(false);
            grpDrag.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

