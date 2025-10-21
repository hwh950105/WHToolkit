namespace sampleapp.UI.UserControls
{
    partial class DashboardControl
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
            panelStats = new Panel();
            cardEfficiency = new ReaLTaiizor.Controls.HopeGroupBox();
            lblEfficiencyLabel = new Label();
            lblEfficiencyValue = new ReaLTaiizor.Controls.BigLabel();
            cardInventory = new ReaLTaiizor.Controls.HopeGroupBox();
            lblInventoryLabel = new Label();
            lblInventoryValue = new ReaLTaiizor.Controls.BigLabel();
            cardQuality = new ReaLTaiizor.Controls.HopeGroupBox();
            lblQualityLabel = new Label();
            lblQualityValue = new ReaLTaiizor.Controls.BigLabel();
            cardProduction = new ReaLTaiizor.Controls.HopeGroupBox();
            lblProductionLabel = new Label();
            lblProductionValue = new ReaLTaiizor.Controls.BigLabel();
            panelHeader.SuspendLayout();
            panelStats.SuspendLayout();
            cardEfficiency.SuspendLayout();
            cardInventory.SuspendLayout();
            cardQuality.SuspendLayout();
            cardProduction.SuspendLayout();
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
            lblTitle.Size = new Size(202, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🏠 대시보드";
            // 
            // panelStats
            // 
            panelStats.BackColor = Color.Transparent;
            panelStats.Controls.Add(cardEfficiency);
            panelStats.Controls.Add(cardInventory);
            panelStats.Controls.Add(cardQuality);
            panelStats.Controls.Add(cardProduction);
            panelStats.Dock = DockStyle.Fill;
            panelStats.Location = new Point(0, 80);
            panelStats.Name = "panelStats";
            panelStats.Padding = new Padding(20);
            panelStats.Size = new Size(1140, 460);
            panelStats.TabIndex = 1;
            // 
            // cardEfficiency
            // 
            cardEfficiency.BorderColor = Color.FromArgb(220, 223, 230);
            cardEfficiency.Controls.Add(lblEfficiencyLabel);
            cardEfficiency.Controls.Add(lblEfficiencyValue);
            cardEfficiency.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            cardEfficiency.ForeColor = Color.FromArgb(40, 40, 40);
            cardEfficiency.LineColor = Color.FromArgb(144, 147, 153);
            cardEfficiency.Location = new Point(840, 30);
            cardEfficiency.Name = "cardEfficiency";
            cardEfficiency.ShowText = false;
            cardEfficiency.Size = new Size(250, 180);
            cardEfficiency.TabIndex = 3;
            cardEfficiency.TabStop = false;
            cardEfficiency.Text = "효율";
            cardEfficiency.ThemeColor = Color.FromArgb(144, 147, 153);
            // 
            // lblEfficiencyLabel
            // 
            lblEfficiencyLabel.AutoSize = true;
            lblEfficiencyLabel.Font = new Font("Segoe UI", 11F);
            lblEfficiencyLabel.ForeColor = Color.FromArgb(100, 100, 100);
            lblEfficiencyLabel.Location = new Point(70, 120);
            lblEfficiencyLabel.Name = "lblEfficiencyLabel";
            lblEfficiencyLabel.Size = new Size(88, 20);
            lblEfficiencyLabel.TabIndex = 1;
            lblEfficiencyLabel.Text = "설비 가동률";
            // 
            // lblEfficiencyValue
            // 
            lblEfficiencyValue.AutoSize = true;
            lblEfficiencyValue.BackColor = Color.Transparent;
            lblEfficiencyValue.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblEfficiencyValue.ForeColor = Color.FromArgb(144, 147, 153);
            lblEfficiencyValue.Location = new Point(50, 50);
            lblEfficiencyValue.Name = "lblEfficiencyValue";
            lblEfficiencyValue.Size = new Size(149, 59);
            lblEfficiencyValue.TabIndex = 0;
            lblEfficiencyValue.Text = "87.2%";
            // 
            // cardInventory
            // 
            cardInventory.BorderColor = Color.FromArgb(220, 223, 230);
            cardInventory.Controls.Add(lblInventoryLabel);
            cardInventory.Controls.Add(lblInventoryValue);
            cardInventory.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            cardInventory.ForeColor = Color.FromArgb(40, 40, 40);
            cardInventory.LineColor = Color.FromArgb(230, 162, 60);
            cardInventory.Location = new Point(570, 30);
            cardInventory.Name = "cardInventory";
            cardInventory.ShowText = false;
            cardInventory.Size = new Size(250, 180);
            cardInventory.TabIndex = 2;
            cardInventory.TabStop = false;
            cardInventory.Text = "재고";
            cardInventory.ThemeColor = Color.FromArgb(230, 162, 60);
            // 
            // lblInventoryLabel
            // 
            lblInventoryLabel.AutoSize = true;
            lblInventoryLabel.Font = new Font("Segoe UI", 11F);
            lblInventoryLabel.ForeColor = Color.FromArgb(100, 100, 100);
            lblInventoryLabel.Location = new Point(60, 120);
            lblInventoryLabel.Name = "lblInventoryLabel";
            lblInventoryLabel.Size = new Size(102, 20);
            lblInventoryLabel.TabIndex = 1;
            lblInventoryLabel.Text = "현재 재고 (개)";
            // 
            // lblInventoryValue
            // 
            lblInventoryValue.AutoSize = true;
            lblInventoryValue.BackColor = Color.Transparent;
            lblInventoryValue.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblInventoryValue.ForeColor = Color.FromArgb(230, 162, 60);
            lblInventoryValue.Location = new Point(50, 50);
            lblInventoryValue.Name = "lblInventoryValue";
            lblInventoryValue.Size = new Size(137, 59);
            lblInventoryValue.TabIndex = 0;
            lblInventoryValue.Text = "3,420";
            // 
            // cardQuality
            // 
            cardQuality.BorderColor = Color.FromArgb(220, 223, 230);
            cardQuality.Controls.Add(lblQualityLabel);
            cardQuality.Controls.Add(lblQualityValue);
            cardQuality.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            cardQuality.ForeColor = Color.FromArgb(40, 40, 40);
            cardQuality.LineColor = Color.FromArgb(103, 194, 58);
            cardQuality.Location = new Point(300, 30);
            cardQuality.Name = "cardQuality";
            cardQuality.ShowText = false;
            cardQuality.Size = new Size(250, 180);
            cardQuality.TabIndex = 1;
            cardQuality.TabStop = false;
            cardQuality.Text = "품질";
            cardQuality.ThemeColor = Color.FromArgb(103, 194, 58);
            // 
            // lblQualityLabel
            // 
            lblQualityLabel.AutoSize = true;
            lblQualityLabel.Font = new Font("Segoe UI", 11F);
            lblQualityLabel.ForeColor = Color.FromArgb(100, 100, 100);
            lblQualityLabel.Location = new Point(70, 120);
            lblQualityLabel.Name = "lblQualityLabel";
            lblQualityLabel.Size = new Size(54, 20);
            lblQualityLabel.TabIndex = 1;
            lblQualityLabel.Text = "합격률";
            // 
            // lblQualityValue
            // 
            lblQualityValue.AutoSize = true;
            lblQualityValue.BackColor = Color.Transparent;
            lblQualityValue.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblQualityValue.ForeColor = Color.FromArgb(103, 194, 58);
            lblQualityValue.Location = new Point(50, 50);
            lblQualityValue.Name = "lblQualityValue";
            lblQualityValue.Size = new Size(149, 59);
            lblQualityValue.TabIndex = 0;
            lblQualityValue.Text = "98.5%";
            // 
            // cardProduction
            // 
            cardProduction.BorderColor = Color.FromArgb(220, 223, 230);
            cardProduction.Controls.Add(lblProductionLabel);
            cardProduction.Controls.Add(lblProductionValue);
            cardProduction.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            cardProduction.ForeColor = Color.FromArgb(40, 40, 40);
            cardProduction.LineColor = Color.FromArgb(64, 158, 255);
            cardProduction.Location = new Point(30, 30);
            cardProduction.Name = "cardProduction";
            cardProduction.ShowText = false;
            cardProduction.Size = new Size(250, 180);
            cardProduction.TabIndex = 0;
            cardProduction.TabStop = false;
            cardProduction.Text = "생산 현황";
            cardProduction.ThemeColor = Color.FromArgb(64, 158, 255);
            // 
            // lblProductionLabel
            // 
            lblProductionLabel.AutoSize = true;
            lblProductionLabel.Font = new Font("Segoe UI", 11F);
            lblProductionLabel.ForeColor = Color.FromArgb(100, 100, 100);
            lblProductionLabel.Location = new Point(50, 120);
            lblProductionLabel.Name = "lblProductionLabel";
            lblProductionLabel.Size = new Size(117, 20);
            lblProductionLabel.TabIndex = 1;
            lblProductionLabel.Text = "오늘 생산량 (개)";
            // 
            // lblProductionValue
            // 
            lblProductionValue.AutoSize = true;
            lblProductionValue.BackColor = Color.Transparent;
            lblProductionValue.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblProductionValue.ForeColor = Color.FromArgb(64, 158, 255);
            lblProductionValue.Location = new Point(40, 50);
            lblProductionValue.Name = "lblProductionValue";
            lblProductionValue.Size = new Size(137, 59);
            lblProductionValue.TabIndex = 0;
            lblProductionValue.Text = "1,250";
            // 
            // DashboardControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Controls.Add(panelStats);
            Controls.Add(panelHeader);
            Name = "DashboardControl";
            Size = new Size(1140, 540);
            Load += DashboardControl_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelStats.ResumeLayout(false);
            cardEfficiency.ResumeLayout(false);
            cardEfficiency.PerformLayout();
            cardInventory.ResumeLayout(false);
            cardInventory.PerformLayout();
            cardQuality.ResumeLayout(false);
            cardQuality.PerformLayout();
            cardProduction.ResumeLayout(false);
            cardProduction.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private ReaLTaiizor.Controls.BigLabel lblTitle;
        private Panel panelStats;
        private ReaLTaiizor.Controls.HopeGroupBox cardProduction;
        private ReaLTaiizor.Controls.BigLabel lblProductionValue;
        private Label lblProductionLabel;
        private ReaLTaiizor.Controls.HopeGroupBox cardQuality;
        private ReaLTaiizor.Controls.BigLabel lblQualityValue;
        private Label lblQualityLabel;
        private ReaLTaiizor.Controls.HopeGroupBox cardInventory;
        private ReaLTaiizor.Controls.BigLabel lblInventoryValue;
        private Label lblInventoryLabel;
        private ReaLTaiizor.Controls.HopeGroupBox cardEfficiency;
        private ReaLTaiizor.Controls.BigLabel lblEfficiencyValue;
        private Label lblEfficiencyLabel;
    }
}
