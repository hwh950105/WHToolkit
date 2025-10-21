namespace sampleapp.UI.UserControls
{
    partial class DBControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitle = new ReaLTaiizor.Controls.BigLabel();
            panelContent = new Panel();
            gridResults = new DataGridView();
            panelButtons = new Panel();
            txtQuery = new TextBox();
            lblQuery = new Label();
            btnExecuteScalar = new ReaLTaiizor.Controls.HopeButton();
            btnExecuteNonQuery = new ReaLTaiizor.Controls.HopeButton();
            btnExecute = new ReaLTaiizor.Controls.HopeButton();
            btnDisconnect = new ReaLTaiizor.Controls.HopeButton();
            btnConnect = new ReaLTaiizor.Controls.HopeButton();
            panelConnection = new Panel();
            lblStatus = new Label();
            txtConnectionString = new TextBox();
            lblConnectionString = new Label();
            cmbDbType = new ReaLTaiizor.Controls.HopeComboBox();
            lblDbType = new Label();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridResults).BeginInit();
            panelButtons.SuspendLayout();
            panelConnection.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(20, 20, 20, 10);
            panelHeader.Size = new Size(1140, 80);
            panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(64, 158, 255);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(211, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "DB 연결 예시";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.Transparent;
            panelContent.Controls.Add(gridResults);
            panelContent.Controls.Add(panelButtons);
            panelContent.Controls.Add(panelConnection);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 80);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(20);
            panelContent.Size = new Size(1140, 460);
            panelContent.TabIndex = 1;
            // 
            // gridResults
            // 
            gridResults.AllowUserToAddRows = false;
            gridResults.AllowUserToDeleteRows = false;
            gridResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridResults.BackgroundColor = Color.White;
            gridResults.BorderStyle = BorderStyle.None;
            gridResults.ColumnHeadersHeight = 40;
            gridResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridResults.Dock = DockStyle.Fill;
            gridResults.Location = new Point(20, 360);
            gridResults.Name = "gridResults";
            gridResults.ReadOnly = true;
            gridResults.RowHeadersVisible = false;
            gridResults.RowTemplate.Height = 30;
            gridResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridResults.Size = new Size(1100, 80);
            gridResults.TabIndex = 2;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.White;
            panelButtons.Controls.Add(txtQuery);
            panelButtons.Controls.Add(lblQuery);
            panelButtons.Controls.Add(btnExecuteScalar);
            panelButtons.Controls.Add(btnExecuteNonQuery);
            panelButtons.Controls.Add(btnExecute);
            panelButtons.Controls.Add(btnDisconnect);
            panelButtons.Controls.Add(btnConnect);
            panelButtons.Dock = DockStyle.Top;
            panelButtons.Location = new Point(20, 150);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(20);
            panelButtons.Size = new Size(1100, 210);
            panelButtons.TabIndex = 1;
            // 
            // txtQuery
            // 
            txtQuery.Font = new Font("Consolas", 10F);
            txtQuery.Location = new Point(23, 93);
            txtQuery.Multiline = true;
            txtQuery.Name = "txtQuery";
            txtQuery.ScrollBars = ScrollBars.Vertical;
            txtQuery.Size = new Size(1050, 80);
            txtQuery.TabIndex = 6;
            txtQuery.Text = "SELECT 1 as num, 'test' as name";
            // 
            // lblQuery
            // 
            lblQuery.AutoSize = true;
            lblQuery.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblQuery.ForeColor = Color.FromArgb(80, 80, 80);
            lblQuery.Location = new Point(23, 68);
            lblQuery.Name = "lblQuery";
            lblQuery.Size = new Size(41, 19);
            lblQuery.TabIndex = 5;
            lblQuery.Text = "쿼리:";
            // 
            // btnExecuteScalar
            // 
            btnExecuteScalar.BorderColor = Color.FromArgb(220, 223, 230);
            btnExecuteScalar.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnExecuteScalar.Cursor = Cursors.Hand;
            btnExecuteScalar.DangerColor = Color.FromArgb(245, 108, 108);
            btnExecuteScalar.DefaultColor = Color.FromArgb(255, 255, 255);
            btnExecuteScalar.Enabled = false;
            btnExecuteScalar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExecuteScalar.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnExecuteScalar.InfoColor = Color.FromArgb(144, 147, 153);
            btnExecuteScalar.Location = new Point(733, 23);
            btnExecuteScalar.Name = "btnExecuteScalar";
            btnExecuteScalar.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnExecuteScalar.Size = new Size(170, 35);
            btnExecuteScalar.SuccessColor = Color.FromArgb(103, 194, 58);
            btnExecuteScalar.TabIndex = 4;
            btnExecuteScalar.Text = "ExecuteScalar";
            btnExecuteScalar.TextColor = Color.White;
            btnExecuteScalar.WarningColor = Color.FromArgb(230, 162, 60);
            btnExecuteScalar.Click += BtnExecuteScalar_Click;
            // 
            // btnExecuteNonQuery
            // 
            btnExecuteNonQuery.BorderColor = Color.FromArgb(220, 223, 230);
            btnExecuteNonQuery.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnExecuteNonQuery.Cursor = Cursors.Hand;
            btnExecuteNonQuery.DangerColor = Color.FromArgb(245, 108, 108);
            btnExecuteNonQuery.DefaultColor = Color.FromArgb(255, 255, 255);
            btnExecuteNonQuery.Enabled = false;
            btnExecuteNonQuery.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExecuteNonQuery.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnExecuteNonQuery.InfoColor = Color.FromArgb(144, 147, 153);
            btnExecuteNonQuery.Location = new Point(543, 23);
            btnExecuteNonQuery.Name = "btnExecuteNonQuery";
            btnExecuteNonQuery.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnExecuteNonQuery.Size = new Size(170, 35);
            btnExecuteNonQuery.SuccessColor = Color.FromArgb(103, 194, 58);
            btnExecuteNonQuery.TabIndex = 3;
            btnExecuteNonQuery.Text = "ExecuteNonQuery";
            btnExecuteNonQuery.TextColor = Color.White;
            btnExecuteNonQuery.WarningColor = Color.FromArgb(230, 162, 60);
            btnExecuteNonQuery.Click += BtnExecuteNonQuery_Click;
            // 
            // btnExecute
            // 
            btnExecute.BorderColor = Color.FromArgb(220, 223, 230);
            btnExecute.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnExecute.Cursor = Cursors.Hand;
            btnExecute.DangerColor = Color.FromArgb(245, 108, 108);
            btnExecute.DefaultColor = Color.FromArgb(255, 255, 255);
            btnExecute.Enabled = false;
            btnExecute.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExecute.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnExecute.InfoColor = Color.FromArgb(144, 147, 153);
            btnExecute.Location = new Point(353, 23);
            btnExecute.Name = "btnExecute";
            btnExecute.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnExecute.Size = new Size(170, 35);
            btnExecute.SuccessColor = Color.FromArgb(103, 194, 58);
            btnExecute.TabIndex = 2;
            btnExecute.Text = "Execute";
            btnExecute.TextColor = Color.White;
            btnExecute.WarningColor = Color.FromArgb(230, 162, 60);
            btnExecute.Click += BtnExecute_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.BorderColor = Color.FromArgb(220, 223, 230);
            btnDisconnect.ButtonType = ReaLTaiizor.Util.HopeButtonType.Danger;
            btnDisconnect.Cursor = Cursors.Hand;
            btnDisconnect.DangerColor = Color.FromArgb(245, 108, 108);
            btnDisconnect.DefaultColor = Color.FromArgb(255, 255, 255);
            btnDisconnect.Enabled = false;
            btnDisconnect.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDisconnect.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnDisconnect.InfoColor = Color.FromArgb(144, 147, 153);
            btnDisconnect.Location = new Point(183, 23);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnDisconnect.Size = new Size(150, 35);
            btnDisconnect.SuccessColor = Color.FromArgb(103, 194, 58);
            btnDisconnect.TabIndex = 1;
            btnDisconnect.Text = "연결 해제";
            btnDisconnect.TextColor = Color.White;
            btnDisconnect.WarningColor = Color.FromArgb(230, 162, 60);
            btnDisconnect.Click += BtnDisconnect_Click;
            // 
            // btnConnect
            // 
            btnConnect.BorderColor = Color.FromArgb(220, 223, 230);
            btnConnect.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success;
            btnConnect.Cursor = Cursors.Hand;
            btnConnect.DangerColor = Color.FromArgb(245, 108, 108);
            btnConnect.DefaultColor = Color.FromArgb(255, 255, 255);
            btnConnect.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConnect.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnConnect.InfoColor = Color.FromArgb(144, 147, 153);
            btnConnect.Location = new Point(23, 23);
            btnConnect.Name = "btnConnect";
            btnConnect.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnConnect.Size = new Size(150, 35);
            btnConnect.SuccessColor = Color.FromArgb(103, 194, 58);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "DB 연결";
            btnConnect.TextColor = Color.White;
            btnConnect.WarningColor = Color.FromArgb(230, 162, 60);
            btnConnect.Click += BtnConnect_Click;
            // 
            // panelConnection
            // 
            panelConnection.BackColor = Color.White;
            panelConnection.Controls.Add(lblStatus);
            panelConnection.Controls.Add(txtConnectionString);
            panelConnection.Controls.Add(lblConnectionString);
            panelConnection.Controls.Add(cmbDbType);
            panelConnection.Controls.Add(lblDbType);
            panelConnection.Dock = DockStyle.Top;
            panelConnection.Location = new Point(20, 20);
            panelConnection.Name = "panelConnection";
            panelConnection.Padding = new Padding(20);
            panelConnection.Size = new Size(1100, 130);
            panelConnection.TabIndex = 0;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(245, 108, 108);
            lblStatus.Location = new Point(23, 100);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(81, 19);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "연결 안됨 ●";
            // 
            // txtConnectionString
            // 
            txtConnectionString.Font = new Font("Segoe UI", 10F);
            txtConnectionString.Location = new Point(170, 60);
            txtConnectionString.Name = "txtConnectionString";
            txtConnectionString.Size = new Size(900, 25);
            txtConnectionString.TabIndex = 3;
            txtConnectionString.Text = "Host=localhost;Database=testdb;Username=postgres;Password=1234";
            // 
            // lblConnectionString
            // 
            lblConnectionString.AutoSize = true;
            lblConnectionString.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConnectionString.ForeColor = Color.FromArgb(80, 80, 80);
            lblConnectionString.Location = new Point(23, 63);
            lblConnectionString.Name = "lblConnectionString";
            lblConnectionString.Size = new Size(101, 19);
            lblConnectionString.TabIndex = 2;
            lblConnectionString.Text = "커넥션 스트링:";
            // 
            // cmbDbType
            // 
            cmbDbType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbDbType.FlatStyle = FlatStyle.Flat;
            cmbDbType.Font = new Font("Segoe UI", 10F);
            cmbDbType.ItemHeight = 24;
            cmbDbType.Items.AddRange(new object[] { "PostgreSQL", "MS SQL", "MySQL", "MariaDB", "Oracle" });
            cmbDbType.Location = new Point(170, 20);
            cmbDbType.Name = "cmbDbType";
            cmbDbType.Size = new Size(200, 30);
            cmbDbType.TabIndex = 1;
            cmbDbType.SelectedIndexChanged += CmbDbType_SelectedIndexChanged;
            // 
            // lblDbType
            // 
            lblDbType.AutoSize = true;
            lblDbType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDbType.ForeColor = Color.FromArgb(80, 80, 80);
            lblDbType.Location = new Point(23, 25);
            lblDbType.Name = "lblDbType";
            lblDbType.Size = new Size(64, 19);
            lblDbType.TabIndex = 0;
            lblDbType.Text = "DB 종류:";
            // 
            // DBControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Name = "DBControl";
            Size = new Size(1140, 540);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridResults).EndInit();
            panelButtons.ResumeLayout(false);
            panelButtons.PerformLayout();
            panelConnection.ResumeLayout(false);
            panelConnection.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private ReaLTaiizor.Controls.BigLabel lblTitle;
        private Panel panelContent;
        private Panel panelConnection;
        private Label lblDbType;
        private ReaLTaiizor.Controls.HopeComboBox cmbDbType;
        private Label lblConnectionString;
        private TextBox txtConnectionString;
        private Label lblStatus;
        private Panel panelButtons;
        private ReaLTaiizor.Controls.HopeButton btnConnect;
        private ReaLTaiizor.Controls.HopeButton btnDisconnect;
        private ReaLTaiizor.Controls.HopeButton btnExecute;
        private ReaLTaiizor.Controls.HopeButton btnExecuteNonQuery;
        private ReaLTaiizor.Controls.HopeButton btnExecuteScalar;
        private Label lblQuery;
        private TextBox txtQuery;
        private DataGridView gridResults;
    }
}
