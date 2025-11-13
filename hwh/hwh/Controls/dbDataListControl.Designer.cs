using System.Drawing;
using System.Windows.Forms;
using hwh.Core;

namespace hwh.Controls
{
    partial class dbDataListControl
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

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblCount = new Label();
            lblTitle = new Label();
            panelButtons = new Panel();
            btnDelete = new Button();
            btnRefresh = new Button();
            dataGridView1 = new DataGridView();
            panelHeader.SuspendLayout();
            panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(249, 250, 251);
            panelHeader.Controls.Add(lblCount);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(30, 20, 30, 10);
            panelHeader.Size = new Size(1000, 80);
            panelHeader.TabIndex = 2;
            // 
            // lblCount
            // 
            lblCount.Dock = DockStyle.Bottom;
            lblCount.Font = new Font("Segoe UI", 10F);
            lblCount.ForeColor = Color.FromArgb(107, 114, 128);
            lblCount.Location = new Point(30, 45);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(940, 25);
            lblCount.TabIndex = 0;
            lblCount.Text = "총 0명의 사용자";
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(940, 30);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "사용자 관리";
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.FromArgb(249, 250, 251);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(btnRefresh);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(0, 630);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(30, 15, 30, 15);
            panelButtons.Size = new Size(1000, 70);
            panelButtons.TabIndex = 1;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(239, 68, 68);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDelete.ForeColor = Color.FromArgb(255, 255, 255);
            btnDelete.Location = new Point(160, 15);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 40);
            btnDelete.TabIndex = 0;
            btnDelete.Text = "🗑️ 삭제";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(124, 58, 237);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.FromArgb(255, 255, 255);
            btnRefresh.Location = new Point(30, 15);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 40);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "🔄 새로고침";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 80);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1000, 550);
            dataGridView1.TabIndex = 0;
            // 
            // dbDataListControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 250, 251);
            Controls.Add(dataGridView1);
            Controls.Add(panelButtons);
            Controls.Add(panelHeader);
            Name = "dbDataListControl";
            Size = new Size(1000, 700);
            panelHeader.ResumeLayout(false);
            panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblCount;
        private Panel panelButtons;
        private Button btnRefresh;
        private Button btnDelete;
        private DataGridView dataGridView1;
    }
}
