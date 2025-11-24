namespace hwh.Controls.Win32Controls
{
    partial class ProcessManagementControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpRun;
        private System.Windows.Forms.GroupBox grpManage;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.Label lblExePath;
        private System.Windows.Forms.Label lblArgs;
        private System.Windows.Forms.TextBox txtExePath;
        private System.Windows.Forms.TextBox txtArgs;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblProcessName;
        private System.Windows.Forms.TextBox txtProcessName;
        private System.Windows.Forms.Button btnCheckRunning;
        private System.Windows.Forms.Button btnKill;
        private System.Windows.Forms.ListView listProcesses;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colPID;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colMemory;
        private System.Windows.Forms.Button btnRefreshList;
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
            grpRun = new GroupBox();
            btnRun = new Button();
            btnBrowse = new Button();
            txtArgs = new TextBox();
            txtExePath = new TextBox();
            lblArgs = new Label();
            lblExePath = new Label();
            grpManage = new GroupBox();
            btnKill = new Button();
            btnCheckRunning = new Button();
            txtProcessName = new TextBox();
            lblProcessName = new Label();
            grpList = new GroupBox();
            btnRefreshList = new Button();
            listProcesses = new ListView();
            colName = new ColumnHeader();
            colPID = new ColumnHeader();
            colTitle = new ColumnHeader();
            colMemory = new ColumnHeader();
            lblStatus = new Label();
            grpRun.SuspendLayout();
            grpManage.SuspendLayout();
            grpList.SuspendLayout();
            SuspendLayout();
            // 
            // grpRun
            // 
            grpRun.Controls.Add(btnRun);
            grpRun.Controls.Add(btnBrowse);
            grpRun.Controls.Add(txtArgs);
            grpRun.Controls.Add(txtExePath);
            grpRun.Controls.Add(lblArgs);
            grpRun.Controls.Add(lblExePath);
            grpRun.Location = new Point(20, 20);
            grpRun.Name = "grpRun";
            grpRun.Size = new Size(750, 120);
            grpRun.TabIndex = 3;
            grpRun.TabStop = false;
            grpRun.Text = "프로세스 실행";
            // 
            // btnRun
            // 
            btnRun.Location = new Point(610, 70);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(120, 30);
            btnRun.TabIndex = 0;
            btnRun.Text = "▶️ 실행";
            btnRun.Click += btnRun_Click;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(610, 30);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(120, 30);
            btnBrowse.TabIndex = 1;
            btnBrowse.Text = "📁 찾아보기";
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtArgs
            // 
            txtArgs.Location = new Point(100, 72);
            txtArgs.Name = "txtArgs";
            txtArgs.Size = new Size(500, 27);
            txtArgs.TabIndex = 2;
            // 
            // txtExePath
            // 
            txtExePath.Location = new Point(100, 32);
            txtExePath.Name = "txtExePath";
            txtExePath.Size = new Size(500, 27);
            txtExePath.TabIndex = 3;
            txtExePath.Text = "notepad.exe";
            // 
            // lblArgs
            // 
            lblArgs.Location = new Point(10, 75);
            lblArgs.Name = "lblArgs";
            lblArgs.Size = new Size(80, 20);
            lblArgs.TabIndex = 4;
            lblArgs.Text = "인자:";
            // 
            // lblExePath
            // 
            lblExePath.Location = new Point(10, 35);
            lblExePath.Name = "lblExePath";
            lblExePath.Size = new Size(80, 20);
            lblExePath.TabIndex = 5;
            lblExePath.Text = "실행 파일:";
            // 
            // grpManage
            // 
            grpManage.Controls.Add(btnKill);
            grpManage.Controls.Add(btnCheckRunning);
            grpManage.Controls.Add(txtProcessName);
            grpManage.Controls.Add(lblProcessName);
            grpManage.Location = new Point(20, 150);
            grpManage.Name = "grpManage";
            grpManage.Size = new Size(750, 80);
            grpManage.TabIndex = 2;
            grpManage.TabStop = false;
            grpManage.Text = "프로세스 관리";
            // 
            // btnKill
            // 
            btnKill.Location = new Point(610, 30);
            btnKill.Name = "btnKill";
            btnKill.Size = new Size(120, 30);
            btnKill.TabIndex = 0;
            btnKill.Text = "❌ 종료";
            btnKill.Click += btnKill_Click;
            // 
            // btnCheckRunning
            // 
            btnCheckRunning.Location = new Point(470, 30);
            btnCheckRunning.Name = "btnCheckRunning";
            btnCheckRunning.Size = new Size(130, 30);
            btnCheckRunning.TabIndex = 1;
            btnCheckRunning.Text = "🔍 실행 확인";
            btnCheckRunning.Click += btnCheckRunning_Click;
            // 
            // txtProcessName
            // 
            txtProcessName.Location = new Point(100, 32);
            txtProcessName.Name = "txtProcessName";
            txtProcessName.Size = new Size(350, 27);
            txtProcessName.TabIndex = 2;
            txtProcessName.Text = "notepad";
            // 
            // lblProcessName
            // 
            lblProcessName.Location = new Point(10, 35);
            lblProcessName.Name = "lblProcessName";
            lblProcessName.Size = new Size(80, 20);
            lblProcessName.TabIndex = 3;
            lblProcessName.Text = "프로세스:";
            // 
            // grpList
            // 
            grpList.Controls.Add(btnRefreshList);
            grpList.Controls.Add(listProcesses);
            grpList.Location = new Point(20, 240);
            grpList.Name = "grpList";
            grpList.Size = new Size(750, 220);
            grpList.TabIndex = 1;
            grpList.TabStop = false;
            grpList.Text = "실행 중인 프로세스";
            // 
            // btnRefreshList
            // 
            btnRefreshList.Location = new Point(10, 180);
            btnRefreshList.Name = "btnRefreshList";
            btnRefreshList.Size = new Size(120, 30);
            btnRefreshList.TabIndex = 0;
            btnRefreshList.Text = "🔄 새로고침";
            btnRefreshList.Click += btnRefreshList_Click;
            // 
            // listProcesses
            // 
            listProcesses.Columns.AddRange(new ColumnHeader[] { colName, colPID, colTitle, colMemory });
            listProcesses.FullRowSelect = true;
            listProcesses.Location = new Point(10, 30);
            listProcesses.Name = "listProcesses";
            listProcesses.Size = new Size(730, 140);
            listProcesses.TabIndex = 1;
            listProcesses.UseCompatibleStateImageBehavior = false;
            listProcesses.View = View.Details;
            listProcesses.DoubleClick += listProcesses_DoubleClick;
            // 
            // colName
            // 
            colName.Text = "프로세스 이름";
            colName.Width = 200;
            // 
            // colPID
            // 
            colPID.Text = "PID";
            colPID.Width = 80;
            // 
            // colTitle
            // 
            colTitle.Text = "윈도우 제목";
            colTitle.Width = 300;
            // 
            // colMemory
            // 
            colMemory.Text = "메모리";
            colMemory.Width = 100;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.Blue;
            lblStatus.Location = new Point(20, 470);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(56, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "대기 중...";
            // 
            // ProcessManagementControl
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(lblStatus);
            Controls.Add(grpList);
            Controls.Add(grpManage);
            Controls.Add(grpRun);
            Font = new Font("맑은 고딕", 11F);
            Name = "ProcessManagementControl";
            Size = new Size(1854, 810);
            Load += ProcessManagementControl_Load;
            grpRun.ResumeLayout(false);
            grpRun.PerformLayout();
            grpManage.ResumeLayout(false);
            grpManage.PerformLayout();
            grpList.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

