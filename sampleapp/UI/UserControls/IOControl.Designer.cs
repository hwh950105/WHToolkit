namespace sampleapp.UI.UserControls
{
    partial class IOControl
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
            gridData = new DataGridView();
            panelButtons = new Panel();
            btnIniWrite = new ReaLTaiizor.Controls.HopeButton();
            btnIniRead = new ReaLTaiizor.Controls.HopeButton();
            btnJsonWrite = new ReaLTaiizor.Controls.HopeButton();
            btnJsonRead = new ReaLTaiizor.Controls.HopeButton();
            panelFilePath = new Panel();
            txtFilePath = new TextBox();
            lblFilePath = new Label();
            cmbFileType = new ReaLTaiizor.Controls.HopeComboBox();
            lblFileType = new Label();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridData).BeginInit();
            panelButtons.SuspendLayout();
            panelFilePath.SuspendLayout();
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
            lblTitle.Size = new Size(232, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "IO 사용법 예시";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.Transparent;
            panelContent.Controls.Add(gridData);
            panelContent.Controls.Add(panelButtons);
            panelContent.Controls.Add(panelFilePath);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 80);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(20);
            panelContent.Size = new Size(1140, 460);
            panelContent.TabIndex = 1;
            // 
            // gridData
            // 
            gridData.AllowUserToAddRows = false;
            gridData.AllowUserToDeleteRows = false;
            gridData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridData.BackgroundColor = Color.White;
            gridData.BorderStyle = BorderStyle.None;
            gridData.ColumnHeadersHeight = 40;
            gridData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridData.Dock = DockStyle.Fill;
            gridData.Location = new Point(20, 220);
            gridData.Name = "gridData";
            gridData.ReadOnly = true;
            gridData.RowHeadersVisible = false;
            gridData.RowTemplate.Height = 35;
            gridData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridData.Size = new Size(1100, 220);
            gridData.TabIndex = 2;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.White;
            panelButtons.Controls.Add(btnIniWrite);
            panelButtons.Controls.Add(btnIniRead);
            panelButtons.Controls.Add(btnJsonWrite);
            panelButtons.Controls.Add(btnJsonRead);
            panelButtons.Dock = DockStyle.Top;
            panelButtons.Location = new Point(20, 130);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(20);
            panelButtons.Size = new Size(1100, 90);
            panelButtons.TabIndex = 1;
            // 
            // btnIniWrite
            // 
            btnIniWrite.BorderColor = Color.FromArgb(220, 223, 230);
            btnIniWrite.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnIniWrite.Cursor = Cursors.Hand;
            btnIniWrite.DangerColor = Color.FromArgb(245, 108, 108);
            btnIniWrite.DefaultColor = Color.FromArgb(255, 255, 255);
            btnIniWrite.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnIniWrite.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnIniWrite.InfoColor = Color.FromArgb(144, 147, 153);
            btnIniWrite.Location = new Point(490, 23);
            btnIniWrite.Name = "btnIniWrite";
            btnIniWrite.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnIniWrite.Size = new Size(200, 40);
            btnIniWrite.SuccessColor = Color.FromArgb(103, 194, 58);
            btnIniWrite.TabIndex = 3;
            btnIniWrite.Text = "INI 쓰기 예제";
            btnIniWrite.TextColor = Color.White;
            btnIniWrite.WarningColor = Color.FromArgb(230, 162, 60);
            btnIniWrite.Click += BtnIniWrite_Click;
            // 
            // btnIniRead
            // 
            btnIniRead.BorderColor = Color.FromArgb(220, 223, 230);
            btnIniRead.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnIniRead.Cursor = Cursors.Hand;
            btnIniRead.DangerColor = Color.FromArgb(245, 108, 108);
            btnIniRead.DefaultColor = Color.FromArgb(255, 255, 255);
            btnIniRead.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnIniRead.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnIniRead.InfoColor = Color.FromArgb(144, 147, 153);
            btnIniRead.Location = new Point(260, 23);
            btnIniRead.Name = "btnIniRead";
            btnIniRead.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnIniRead.Size = new Size(200, 40);
            btnIniRead.SuccessColor = Color.FromArgb(103, 194, 58);
            btnIniRead.TabIndex = 2;
            btnIniRead.Text = "INI 읽기 예제";
            btnIniRead.TextColor = Color.White;
            btnIniRead.WarningColor = Color.FromArgb(230, 162, 60);
            btnIniRead.Click += BtnIniRead_Click;
            // 
            // btnJsonWrite
            // 
            btnJsonWrite.BorderColor = Color.FromArgb(220, 223, 230);
            btnJsonWrite.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success;
            btnJsonWrite.Cursor = Cursors.Hand;
            btnJsonWrite.DangerColor = Color.FromArgb(245, 108, 108);
            btnJsonWrite.DefaultColor = Color.FromArgb(255, 255, 255);
            btnJsonWrite.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnJsonWrite.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnJsonWrite.InfoColor = Color.FromArgb(144, 147, 153);
            btnJsonWrite.Location = new Point(720, 23);
            btnJsonWrite.Name = "btnJsonWrite";
            btnJsonWrite.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnJsonWrite.Size = new Size(200, 40);
            btnJsonWrite.SuccessColor = Color.FromArgb(103, 194, 58);
            btnJsonWrite.TabIndex = 1;
            btnJsonWrite.Text = "JSON 쓰기 예제";
            btnJsonWrite.TextColor = Color.White;
            btnJsonWrite.WarningColor = Color.FromArgb(230, 162, 60);
            btnJsonWrite.Click += BtnJsonWrite_Click;
            // 
            // btnJsonRead
            // 
            btnJsonRead.BorderColor = Color.FromArgb(220, 223, 230);
            btnJsonRead.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success;
            btnJsonRead.Cursor = Cursors.Hand;
            btnJsonRead.DangerColor = Color.FromArgb(245, 108, 108);
            btnJsonRead.DefaultColor = Color.FromArgb(255, 255, 255);
            btnJsonRead.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnJsonRead.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnJsonRead.InfoColor = Color.FromArgb(144, 147, 153);
            btnJsonRead.Location = new Point(23, 23);
            btnJsonRead.Name = "btnJsonRead";
            btnJsonRead.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnJsonRead.Size = new Size(200, 40);
            btnJsonRead.SuccessColor = Color.FromArgb(103, 194, 58);
            btnJsonRead.TabIndex = 0;
            btnJsonRead.Text = "JSON 읽기 예제";
            btnJsonRead.TextColor = Color.White;
            btnJsonRead.WarningColor = Color.FromArgb(230, 162, 60);
            btnJsonRead.Click += BtnJsonRead_Click;
            // 
            // panelFilePath
            // 
            panelFilePath.BackColor = Color.White;
            panelFilePath.Controls.Add(txtFilePath);
            panelFilePath.Controls.Add(lblFilePath);
            panelFilePath.Controls.Add(cmbFileType);
            panelFilePath.Controls.Add(lblFileType);
            panelFilePath.Dock = DockStyle.Top;
            panelFilePath.Location = new Point(20, 20);
            panelFilePath.Name = "panelFilePath";
            panelFilePath.Padding = new Padding(20);
            panelFilePath.Size = new Size(1100, 110);
            panelFilePath.TabIndex = 0;
            // 
            // txtFilePath
            // 
            txtFilePath.Font = new Font("Segoe UI", 10F);
            txtFilePath.Location = new Point(120, 60);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(800, 25);
            txtFilePath.TabIndex = 3;
            txtFilePath.Text = "sample_data.json";
            // 
            // lblFilePath
            // 
            lblFilePath.AutoSize = true;
            lblFilePath.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFilePath.ForeColor = Color.FromArgb(80, 80, 80);
            lblFilePath.Location = new Point(23, 63);
            lblFilePath.Name = "lblFilePath";
            lblFilePath.Size = new Size(73, 19);
            lblFilePath.TabIndex = 2;
            lblFilePath.Text = "파일 경로:";
            // 
            // cmbFileType
            // 
            cmbFileType.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFileType.FlatStyle = FlatStyle.Flat;
            cmbFileType.Font = new Font("Segoe UI", 10F);
            cmbFileType.ItemHeight = 24;
            cmbFileType.Items.AddRange(new object[] { "JSON", "INI" });
            cmbFileType.Location = new Point(120, 20);
            cmbFileType.Name = "cmbFileType";
            cmbFileType.Size = new Size(200, 30);
            cmbFileType.TabIndex = 1;
            cmbFileType.SelectedIndexChanged += CmbFileType_SelectedIndexChanged;
            // 
            // lblFileType
            // 
            lblFileType.AutoSize = true;
            lblFileType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFileType.ForeColor = Color.FromArgb(80, 80, 80);
            lblFileType.Location = new Point(23, 25);
            lblFileType.Name = "lblFileType";
            lblFileType.Size = new Size(73, 19);
            lblFileType.TabIndex = 0;
            lblFileType.Text = "파일 형식:";
            // 
            // IOControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Name = "IOControl";
            Size = new Size(1140, 540);
            Load += ProductionControl_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridData).EndInit();
            panelButtons.ResumeLayout(false);
            panelFilePath.ResumeLayout(false);
            panelFilePath.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private ReaLTaiizor.Controls.BigLabel lblTitle;
        private Panel panelContent;
        private Panel panelFilePath;
        private Label lblFileType;
        private ReaLTaiizor.Controls.HopeComboBox cmbFileType;
        private Label lblFilePath;
        private TextBox txtFilePath;
        private Panel panelButtons;
        private ReaLTaiizor.Controls.HopeButton btnJsonRead;
        private ReaLTaiizor.Controls.HopeButton btnJsonWrite;
        private ReaLTaiizor.Controls.HopeButton btnIniRead;
        private ReaLTaiizor.Controls.HopeButton btnIniWrite;
        private DataGridView gridData;
    }
}
