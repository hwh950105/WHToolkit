using System.Drawing;
using System.Windows.Forms;
using hwh.Core;

namespace hwh.Controls
{
    partial class DataListControl
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
            this.panelHeader = new Panel();
            this.lblTitle = new Label();
            this.lblCount = new Label();
            this.panelButtons = new Panel();
            this.btnRefresh = new Button();
            this.btnDelete = new Button();
            this.dataGridView1 = new DataGridView();
            this.colUserId = new DataGridViewTextBoxColumn();
            this.colUsername = new DataGridViewTextBoxColumn();
            this.colName = new DataGridViewTextBoxColumn();
            this.colEmail = new DataGridViewTextBoxColumn();
            this.colPhone = new DataGridViewTextBoxColumn();
            this.colRole = new DataGridViewTextBoxColumn();
            this.colStatus = new DataGridViewTextBoxColumn();
            this.colCreatedAt = new DataGridViewTextBoxColumn();

            this.panelHeader.SuspendLayout();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
            this.SuspendLayout();

            // 
            // DataListControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Theme.ContentBg;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelHeader);
            this.Name = "DataListControl";
            this.Size = new Size(1000, 700);

            // 
            // panelHeader
            // 
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 80;
            this.panelHeader.BackColor = Theme.ContentBg;
            this.panelHeader.Padding = new Padding(30, 20, 30, 10);
            this.panelHeader.Controls.Add(this.lblCount);
            this.panelHeader.Controls.Add(this.lblTitle);

            // 
            // lblTitle
            // 
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Font = Theme.FontTitle;
            this.lblTitle.ForeColor = Theme.TextPrimary;
            this.lblTitle.Text = "사용자 관리";
            this.lblTitle.Height = 30;

            // 
            // lblCount
            // 
            this.lblCount.Dock = DockStyle.Bottom;
            this.lblCount.Font = Theme.FontBody;
            this.lblCount.ForeColor = Theme.TextSecondary;
            this.lblCount.Text = "총 0명의 사용자";
            this.lblCount.Height = 25;

            // 
            // panelButtons
            // 
            this.panelButtons.Dock = DockStyle.Bottom;
            this.panelButtons.Height = 70;
            this.panelButtons.BackColor = Theme.ContentBg;
            this.panelButtons.Padding = new Padding(30, 15, 30, 15);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnRefresh);

            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new Point(30, 15);
            this.btnRefresh.Size = new Size(120, 40);
            this.btnRefresh.Font = Theme.FontBodyBold;
            this.btnRefresh.BackColor = Theme.Primary;
            this.btnRefresh.ForeColor = Theme.TextWhite;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Cursor = Cursors.Hand;
            this.btnRefresh.Text = "🔄 새로고침";
            this.btnRefresh.Click += BtnRefresh_Click;

            // 
            // btnDelete
            // 
            this.btnDelete.Location = new Point(160, 15);
            this.btnDelete.Size = new Size(120, 40);
            this.btnDelete.Font = Theme.FontBodyBold;
            this.btnDelete.BackColor = Theme.Error;
            this.btnDelete.ForeColor = Theme.TextWhite;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Cursor = Cursors.Hand;
            this.btnDelete.Text = "🗑️ 삭제";
            this.btnDelete.Click += BtnDelete_Click;

            // 
            // dataGridView1
            // 
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] {
                this.colUserId,
                this.colUsername,
                this.colName,
                this.colEmail,
                this.colPhone,
                this.colRole,
                this.colStatus,
                this.colCreatedAt
            });
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 
            // colUserId
            // 
            this.colUserId.HeaderText = "ID";
            this.colUserId.Name = "colUserId";
            this.colUserId.FillWeight = 50;

            // 
            // colUsername
            // 
            this.colUsername.HeaderText = "아이디";
            this.colUsername.Name = "colUsername";
            this.colUsername.FillWeight = 100;

            // 
            // colName
            // 
            this.colName.HeaderText = "이름";
            this.colName.Name = "colName";
            this.colName.FillWeight = 80;

            // 
            // colEmail
            // 
            this.colEmail.HeaderText = "이메일";
            this.colEmail.Name = "colEmail";
            this.colEmail.FillWeight = 150;

            // 
            // colPhone
            // 
            this.colPhone.HeaderText = "연락처";
            this.colPhone.Name = "colPhone";
            this.colPhone.FillWeight = 100;

            // 
            // colRole
            // 
            this.colRole.HeaderText = "권한";
            this.colRole.Name = "colRole";
            this.colRole.FillWeight = 70;

            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "상태";
            this.colStatus.Name = "colStatus";
            this.colStatus.FillWeight = 70;

            // 
            // colCreatedAt
            // 
            this.colCreatedAt.HeaderText = "가입일";
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.FillWeight = 120;

            this.panelHeader.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
            this.ResumeLayout(false);
        }

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblCount;
        private Panel panelButtons;
        private Button btnRefresh;
        private Button btnDelete;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colUserId;
        private DataGridViewTextBoxColumn colUsername;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colPhone;
        private DataGridViewTextBoxColumn colRole;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colCreatedAt;
    }
}
