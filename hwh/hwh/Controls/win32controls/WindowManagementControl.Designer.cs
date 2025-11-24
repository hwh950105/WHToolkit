namespace hwh.Controls.Win32Controls
{
    partial class WindowManagementControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpWindowList;
        private System.Windows.Forms.ListView listWindows;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colProcess;
        private System.Windows.Forms.ColumnHeader colPID;
        private System.Windows.Forms.ColumnHeader colHandle;
        private System.Windows.Forms.ColumnHeader colPorts;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox grpFind;
        private System.Windows.Forms.Label lblWindowTitle;
        private System.Windows.Forms.TextBox txtWindowTitle;
        private System.Windows.Forms.Button btnFindByTitle;
        private System.Windows.Forms.GroupBox grpControl;
        private System.Windows.Forms.Button btnShowWindow;
        private System.Windows.Forms.Button btnHideWindow;
        private System.Windows.Forms.Button btnSetTopMost;
        private System.Windows.Forms.Button btnUnsetTopMost;
        private System.Windows.Forms.Button btnGetRect;
        private System.Windows.Forms.Label lblWindowInfo;
        private System.Windows.Forms.Label lblStatus;

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
            this.grpWindowList = new GroupBox();
            this.btnRefresh = new Button();
            this.listWindows = new ListView();
            this.colTitle = new ColumnHeader();
            this.colProcess = new ColumnHeader();
            this.colPID = new ColumnHeader();
            this.colHandle = new ColumnHeader();
            this.colPorts = new ColumnHeader();
            this.grpFind = new GroupBox();
            this.btnFindByTitle = new Button();
            this.txtWindowTitle = new TextBox();
            this.lblWindowTitle = new Label();
            this.grpControl = new GroupBox();
            this.lblWindowInfo = new Label();
            this.btnGetRect = new Button();
            this.btnUnsetTopMost = new Button();
            this.btnSetTopMost = new Button();
            this.btnHideWindow = new Button();
            this.btnShowWindow = new Button();
            this.lblStatus = new Label();
            this.grpWindowList.SuspendLayout();
            this.grpFind.SuspendLayout();
            this.grpControl.SuspendLayout();
            this.SuspendLayout();
            
            // grpWindowList
            this.grpWindowList.Controls.Add(this.btnRefresh);
            this.grpWindowList.Controls.Add(this.listWindows);
            this.grpWindowList.Location = new System.Drawing.Point(20, 20);
            this.grpWindowList.Size = new System.Drawing.Size(750, 250);
            this.grpWindowList.Text = "윈도우 목록";
            
            // listWindows
            this.listWindows.Columns.AddRange(new ColumnHeader[] {
                this.colTitle, this.colProcess, this.colPID, this.colHandle, this.colPorts});
            this.listWindows.FullRowSelect = true;
            this.listWindows.Location = new System.Drawing.Point(10, 30);
            this.listWindows.MultiSelect = false;
            this.listWindows.Size = new System.Drawing.Size(730, 170);
            this.listWindows.View = View.Details;
            this.listWindows.SelectedIndexChanged += listWindows_SelectedIndexChanged;
            
            this.colTitle.Text = "제목";
            this.colTitle.Width = 250;
            this.colProcess.Text = "프로세스";
            this.colProcess.Width = 120;
            this.colPID.Text = "PID";
            this.colPID.Width = 60;
            this.colHandle.Text = "핸들";
            this.colHandle.Width = 100;
            this.colPorts.Text = "포트";
            this.colPorts.Width = 150;
            
            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(10, 210);
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.Text = "🔄 새로고침";
            this.btnRefresh.Click += btnRefresh_Click;
            
            // grpFind
            this.grpFind.Controls.Add(this.btnFindByTitle);
            this.grpFind.Controls.Add(this.txtWindowTitle);
            this.grpFind.Controls.Add(this.lblWindowTitle);
            this.grpFind.Location = new System.Drawing.Point(20, 280);
            this.grpFind.Size = new System.Drawing.Size(750, 80);
            this.grpFind.Text = "윈도우 찾기";
            
            this.lblWindowTitle.Location = new System.Drawing.Point(10, 35);
            this.lblWindowTitle.Size = new System.Drawing.Size(60, 20);
            this.lblWindowTitle.Text = "제목:";
            
            this.txtWindowTitle.Location = new System.Drawing.Point(80, 32);
            this.txtWindowTitle.Size = new System.Drawing.Size(500, 27);
            
            this.btnFindByTitle.Location = new System.Drawing.Point(600, 30);
            this.btnFindByTitle.Size = new System.Drawing.Size(130, 30);
            this.btnFindByTitle.Text = "🔍 찾기";
            this.btnFindByTitle.Click += btnFindByTitle_Click;
            
            // grpControl
            this.grpControl.Controls.Add(this.lblWindowInfo);
            this.grpControl.Controls.Add(this.btnGetRect);
            this.grpControl.Controls.Add(this.btnUnsetTopMost);
            this.grpControl.Controls.Add(this.btnSetTopMost);
            this.grpControl.Controls.Add(this.btnHideWindow);
            this.grpControl.Controls.Add(this.btnShowWindow);
            this.grpControl.Location = new System.Drawing.Point(20, 370);
            this.grpControl.Size = new System.Drawing.Size(750, 100);
            this.grpControl.Text = "윈도우 제어";
            
            this.btnShowWindow.Location = new System.Drawing.Point(10, 30);
            this.btnShowWindow.Size = new System.Drawing.Size(100, 30);
            this.btnShowWindow.Text = "표시";
            this.btnShowWindow.Click += btnShowWindow_Click;
            
            this.btnHideWindow.Location = new System.Drawing.Point(120, 30);
            this.btnHideWindow.Size = new System.Drawing.Size(100, 30);
            this.btnHideWindow.Text = "숨기기";
            this.btnHideWindow.Click += btnHideWindow_Click;
            
            this.btnSetTopMost.Location = new System.Drawing.Point(230, 30);
            this.btnSetTopMost.Size = new System.Drawing.Size(130, 30);
            this.btnSetTopMost.Text = "항상 위";
            this.btnSetTopMost.Click += btnSetTopMost_Click;
            
            this.btnUnsetTopMost.Location = new System.Drawing.Point(370, 30);
            this.btnUnsetTopMost.Size = new System.Drawing.Size(130, 30);
            this.btnUnsetTopMost.Text = "항상 위 해제";
            this.btnUnsetTopMost.Click += btnUnsetTopMost_Click;
            
            this.btnGetRect.Location = new System.Drawing.Point(510, 30);
            this.btnGetRect.Size = new System.Drawing.Size(130, 30);
            this.btnGetRect.Text = "정보 가져오기";
            this.btnGetRect.Click += btnGetRect_Click;
            
            this.lblWindowInfo.AutoSize = true;
            this.lblWindowInfo.Location = new System.Drawing.Point(10, 70);
            this.lblWindowInfo.Text = "윈도우 정보: -";
            
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(20, 480);
            this.lblStatus.Text = "대기 중...";
            
            // WindowManagementControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.grpFind);
            this.Controls.Add(this.grpWindowList);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.Name = "WindowManagementControl";
            this.Load += WindowManagementControl_Load;
            
            this.grpWindowList.ResumeLayout(false);
            this.grpFind.ResumeLayout(false);
            this.grpFind.PerformLayout();
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

