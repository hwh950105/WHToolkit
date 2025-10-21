namespace sampleapp.UI.UserControls
{
    partial class UserlistControl
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
            griddata = new DataGridView();
            panelFilters = new Panel();
            btnSearch = new ReaLTaiizor.Controls.HopeButton();
            dtpEnd = new DateTimePicker();
            dtpStart = new DateTimePicker();
            lblDateRange = new Label();
            cmbLine = new ReaLTaiizor.Controls.HopeComboBox();
            lblLine = new Label();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)griddata).BeginInit();
            panelFilters.SuspendLayout();
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
            lblTitle.Size = new Size(157, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "유저 목록";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.Transparent;
            panelContent.Controls.Add(griddata);
            panelContent.Controls.Add(panelFilters);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 80);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(20);
            panelContent.Size = new Size(1140, 460);
            panelContent.TabIndex = 1;
            // 
            // griddata
            // 
            griddata.AllowUserToAddRows = false;
            griddata.AllowUserToDeleteRows = false;
            griddata.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            griddata.BackgroundColor = Color.White;
            griddata.BorderStyle = BorderStyle.None;
            griddata.ColumnHeadersHeight = 40;
            griddata.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            griddata.Dock = DockStyle.Fill;
            griddata.Location = new Point(20, 120);
            griddata.Name = "griddata";
            griddata.ReadOnly = true;
            griddata.RowHeadersVisible = false;
            griddata.RowTemplate.Height = 35;
            griddata.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            griddata.Size = new Size(1100, 320);
            griddata.TabIndex = 1;
            // 
            // panelFilters
            // 
            panelFilters.BackColor = Color.White;
            panelFilters.Controls.Add(btnSearch);
            panelFilters.Controls.Add(dtpEnd);
            panelFilters.Controls.Add(dtpStart);
            panelFilters.Controls.Add(lblDateRange);
            panelFilters.Controls.Add(cmbLine);
            panelFilters.Controls.Add(lblLine);
            panelFilters.Dock = DockStyle.Top;
            panelFilters.Location = new Point(20, 20);
            panelFilters.Name = "panelFilters";
            panelFilters.Padding = new Padding(20);
            panelFilters.Size = new Size(1100, 100);
            panelFilters.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.BorderColor = Color.FromArgb(220, 223, 230);
            btnSearch.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.DangerColor = Color.FromArgb(245, 108, 108);
            btnSearch.DefaultColor = Color.FromArgb(255, 255, 255);
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.HoverTextColor = Color.FromArgb(48, 49, 51);
            btnSearch.InfoColor = Color.FromArgb(144, 147, 153);
            btnSearch.Location = new Point(560, 45);
            btnSearch.Name = "btnSearch";
            btnSearch.PrimaryColor = Color.FromArgb(64, 158, 255);
            btnSearch.Size = new Size(120, 35);
            btnSearch.SuccessColor = Color.FromArgb(103, 194, 58);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "🔍 조회";
            btnSearch.TextColor = Color.White;
            btnSearch.WarningColor = Color.FromArgb(230, 162, 60);
            // 
            // dtpEnd
            // 
            dtpEnd.CalendarFont = new Font("Segoe UI", 9F);
            dtpEnd.Font = new Font("Segoe UI", 10F);
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(390, 50);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(150, 25);
            dtpEnd.TabIndex = 4;
            // 
            // dtpStart
            // 
            dtpStart.CalendarFont = new Font("Segoe UI", 9F);
            dtpStart.Font = new Font("Segoe UI", 10F);
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(220, 50);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(150, 25);
            dtpStart.TabIndex = 3;
            // 
            // lblDateRange
            // 
            lblDateRange.AutoSize = true;
            lblDateRange.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDateRange.ForeColor = Color.FromArgb(80, 80, 80);
            lblDateRange.Location = new Point(220, 25);
            lblDateRange.Name = "lblDateRange";
            lblDateRange.Size = new Size(69, 19);
            lblDateRange.TabIndex = 2;
            lblDateRange.Text = "가입 기간";
            // 
            // cmbLine
            // 
            cmbLine.DrawMode = DrawMode.OwnerDrawFixed;
            cmbLine.FlatStyle = FlatStyle.Flat;
            cmbLine.Font = new Font("Segoe UI", 10F);
            cmbLine.ItemHeight = 24;
            cmbLine.Items.AddRange(new object[] { "전체", "라인 1", "라인 2", "라인 3", "라인 4" });
            cmbLine.Location = new Point(20, 50);
            cmbLine.Name = "cmbLine";
            cmbLine.Size = new Size(180, 30);
            cmbLine.TabIndex = 1;
            // 
            // lblLine
            // 
            lblLine.AutoSize = true;
            lblLine.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLine.ForeColor = Color.FromArgb(80, 80, 80);
            lblLine.Location = new Point(20, 25);
            lblLine.Name = "lblLine";
            lblLine.Size = new Size(37, 19);
            lblLine.TabIndex = 0;
            lblLine.Text = "이름";
            // 
            // UserlistControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Name = "UserlistControl";
            Size = new Size(1140, 540);
            Load += ProductionControl_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)griddata).EndInit();
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private ReaLTaiizor.Controls.BigLabel lblTitle;
        private Panel panelContent;
        private Panel panelFilters;
        private Label lblLine;
        private ReaLTaiizor.Controls.HopeComboBox cmbLine;
        private Label lblDateRange;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private ReaLTaiizor.Controls.HopeButton btnSearch;
        private DataGridView griddata;
    }
}
