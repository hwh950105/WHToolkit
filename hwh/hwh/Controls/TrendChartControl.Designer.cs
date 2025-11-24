namespace hwh.Controls
{
    partial class ScottPlotTrendChart
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            panelTop = new Panel();
            groupBox2 = new GroupBox();
            btnRange12mo = new Button();
            btnRange6mo = new Button();
            btnRange3mo = new Button();
            groupBox3 = new GroupBox();
            btnRange30d = new Button();
            btnRange7d = new Button();
            btnRange1d = new Button();
            groupBox4 = new GroupBox();
            btnRange12h = new Button();
            btnRange6h = new Button();
            btnRange3h = new Button();
            groupBox5 = new GroupBox();
            btnRealtime = new Button();
            btnRange60m = new Button();
            btnRange30m = new Button();
            btnRange10m = new Button();
            panelControl = new Panel();
            btnRefresh = new Button();
            chkAutoUpdate = new CheckBox();
            dtpEnd = new DateTimePicker();
            label2 = new Label();
            dtpStart = new DateTimePicker();
            label1 = new Label();
            panelBottom = new Panel();
            dgvTags = new DataGridView();
            colNo = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colAlias = new DataGridViewTextBoxColumn();
            colCurrentValue = new DataGridViewTextBoxColumn();
            colMinValue = new DataGridViewTextBoxColumn();
            colMaxValue = new DataGridViewTextBoxColumn();
            colVisible = new DataGridViewCheckBoxColumn();
            panelTop.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            panelControl.SuspendLayout();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTags).BeginInit();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1.25F;
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(0, 110);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1200, 490);
            formsPlot1.TabIndex = 0;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(groupBox2);
            panelTop.Controls.Add(groupBox3);
            panelTop.Controls.Add(groupBox4);
            panelTop.Controls.Add(groupBox5);
            panelTop.Controls.Add(panelControl);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(5);
            panelTop.Size = new Size(1200, 110);
            panelTop.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnRange12mo);
            groupBox2.Controls.Add(btnRange6mo);
            groupBox2.Controls.Add(btnRange3mo);
            groupBox2.Dock = DockStyle.Left;
            groupBox2.Font = new Font("맑은 고딕", 9F);
            groupBox2.Location = new Point(605, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(5);
            groupBox2.Size = new Size(180, 50);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "월 단위";
            // 
            // btnRange12mo
            // 
            btnRange12mo.Dock = DockStyle.Left;
            btnRange12mo.Location = new Point(125, 21);
            btnRange12mo.Name = "btnRange12mo";
            btnRange12mo.Size = new Size(50, 24);
            btnRange12mo.TabIndex = 2;
            btnRange12mo.Text = "12개월";
            btnRange12mo.UseVisualStyleBackColor = true;
            btnRange12mo.Click += BtnRange_Click;
            // 
            // btnRange6mo
            // 
            btnRange6mo.Dock = DockStyle.Left;
            btnRange6mo.Location = new Point(65, 21);
            btnRange6mo.Name = "btnRange6mo";
            btnRange6mo.Size = new Size(60, 24);
            btnRange6mo.TabIndex = 1;
            btnRange6mo.Text = "6개월";
            btnRange6mo.UseVisualStyleBackColor = true;
            btnRange6mo.Click += BtnRange_Click;
            // 
            // btnRange3mo
            // 
            btnRange3mo.Dock = DockStyle.Left;
            btnRange3mo.Location = new Point(5, 21);
            btnRange3mo.Name = "btnRange3mo";
            btnRange3mo.Size = new Size(60, 24);
            btnRange3mo.TabIndex = 0;
            btnRange3mo.Text = "3개월";
            btnRange3mo.UseVisualStyleBackColor = true;
            btnRange3mo.Click += BtnRange_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnRange30d);
            groupBox3.Controls.Add(btnRange7d);
            groupBox3.Controls.Add(btnRange1d);
            groupBox3.Dock = DockStyle.Left;
            groupBox3.Font = new Font("맑은 고딕", 9F);
            groupBox3.Location = new Point(425, 5);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(5);
            groupBox3.Size = new Size(180, 50);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "일 단위";
            // 
            // btnRange30d
            // 
            btnRange30d.Dock = DockStyle.Left;
            btnRange30d.Location = new Point(125, 21);
            btnRange30d.Name = "btnRange30d";
            btnRange30d.Size = new Size(50, 24);
            btnRange30d.TabIndex = 2;
            btnRange30d.Text = "30일";
            btnRange30d.UseVisualStyleBackColor = true;
            btnRange30d.Click += BtnRange_Click;
            // 
            // btnRange7d
            // 
            btnRange7d.Dock = DockStyle.Left;
            btnRange7d.Location = new Point(65, 21);
            btnRange7d.Name = "btnRange7d";
            btnRange7d.Size = new Size(60, 24);
            btnRange7d.TabIndex = 1;
            btnRange7d.Text = "7일";
            btnRange7d.UseVisualStyleBackColor = true;
            btnRange7d.Click += BtnRange_Click;
            // 
            // btnRange1d
            // 
            btnRange1d.Dock = DockStyle.Left;
            btnRange1d.Location = new Point(5, 21);
            btnRange1d.Name = "btnRange1d";
            btnRange1d.Size = new Size(60, 24);
            btnRange1d.TabIndex = 0;
            btnRange1d.Text = "1일";
            btnRange1d.UseVisualStyleBackColor = true;
            btnRange1d.Click += BtnRange_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btnRange12h);
            groupBox4.Controls.Add(btnRange6h);
            groupBox4.Controls.Add(btnRange3h);
            groupBox4.Dock = DockStyle.Left;
            groupBox4.Font = new Font("맑은 고딕", 9F);
            groupBox4.Location = new Point(245, 5);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(5);
            groupBox4.Size = new Size(180, 50);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "시간 단위";
            // 
            // btnRange12h
            // 
            btnRange12h.Dock = DockStyle.Left;
            btnRange12h.Location = new Point(125, 21);
            btnRange12h.Name = "btnRange12h";
            btnRange12h.Size = new Size(50, 24);
            btnRange12h.TabIndex = 2;
            btnRange12h.Text = "12시간";
            btnRange12h.UseVisualStyleBackColor = true;
            btnRange12h.Click += BtnRange_Click;
            // 
            // btnRange6h
            // 
            btnRange6h.Dock = DockStyle.Left;
            btnRange6h.Location = new Point(65, 21);
            btnRange6h.Name = "btnRange6h";
            btnRange6h.Size = new Size(60, 24);
            btnRange6h.TabIndex = 1;
            btnRange6h.Text = "6시간";
            btnRange6h.UseVisualStyleBackColor = true;
            btnRange6h.Click += BtnRange_Click;
            // 
            // btnRange3h
            // 
            btnRange3h.Dock = DockStyle.Left;
            btnRange3h.Location = new Point(5, 21);
            btnRange3h.Name = "btnRange3h";
            btnRange3h.Size = new Size(60, 24);
            btnRange3h.TabIndex = 0;
            btnRange3h.Text = "3시간";
            btnRange3h.UseVisualStyleBackColor = true;
            btnRange3h.Click += BtnRange_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(btnRealtime);
            groupBox5.Controls.Add(btnRange60m);
            groupBox5.Controls.Add(btnRange30m);
            groupBox5.Controls.Add(btnRange10m);
            groupBox5.Dock = DockStyle.Left;
            groupBox5.Font = new Font("맑은 고딕", 9F);
            groupBox5.Location = new Point(5, 5);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(5);
            groupBox5.Size = new Size(240, 50);
            groupBox5.TabIndex = 1;
            groupBox5.TabStop = false;
            groupBox5.Text = "실시간/분 단위";
            // 
            // btnRealtime
            // 
            btnRealtime.BackColor = Color.LightGreen;
            btnRealtime.Dock = DockStyle.Left;
            btnRealtime.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            btnRealtime.Location = new Point(185, 21);
            btnRealtime.Name = "btnRealtime";
            btnRealtime.Size = new Size(50, 24);
            btnRealtime.TabIndex = 3;
            btnRealtime.Text = "실시간";
            btnRealtime.UseVisualStyleBackColor = false;
            btnRealtime.Click += BtnRange_Click;
            // 
            // btnRange60m
            // 
            btnRange60m.Dock = DockStyle.Left;
            btnRange60m.Location = new Point(125, 21);
            btnRange60m.Name = "btnRange60m";
            btnRange60m.Size = new Size(60, 24);
            btnRange60m.TabIndex = 2;
            btnRange60m.Text = "60분";
            btnRange60m.UseVisualStyleBackColor = true;
            btnRange60m.Click += BtnRange_Click;
            // 
            // btnRange30m
            // 
            btnRange30m.Dock = DockStyle.Left;
            btnRange30m.Location = new Point(65, 21);
            btnRange30m.Name = "btnRange30m";
            btnRange30m.Size = new Size(60, 24);
            btnRange30m.TabIndex = 1;
            btnRange30m.Text = "30분";
            btnRange30m.UseVisualStyleBackColor = true;
            btnRange30m.Click += BtnRange_Click;
            // 
            // btnRange10m
            // 
            btnRange10m.Dock = DockStyle.Left;
            btnRange10m.Location = new Point(5, 21);
            btnRange10m.Name = "btnRange10m";
            btnRange10m.Size = new Size(60, 24);
            btnRange10m.TabIndex = 0;
            btnRange10m.Text = "10분";
            btnRange10m.UseVisualStyleBackColor = true;
            btnRange10m.Click += BtnRange_Click;
            // 
            // panelControl
            // 
            panelControl.Controls.Add(btnRefresh);
            panelControl.Controls.Add(chkAutoUpdate);
            panelControl.Controls.Add(dtpEnd);
            panelControl.Controls.Add(label2);
            panelControl.Controls.Add(dtpStart);
            panelControl.Controls.Add(label1);
            panelControl.Dock = DockStyle.Bottom;
            panelControl.Location = new Point(5, 55);
            panelControl.Name = "panelControl";
            panelControl.Padding = new Padding(0, 5, 0, 5);
            panelControl.Size = new Size(1190, 50);
            panelControl.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            btnRefresh.Location = new Point(712, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 30);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "새로고침";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // chkAutoUpdate
            // 
            chkAutoUpdate.AutoSize = true;
            chkAutoUpdate.Font = new Font("맑은 고딕", 10F);
            chkAutoUpdate.Location = new Point(589, 15);
            chkAutoUpdate.Name = "chkAutoUpdate";
            chkAutoUpdate.Size = new Size(117, 23);
            chkAutoUpdate.TabIndex = 4;
            chkAutoUpdate.Text = "자동 업데이트";
            chkAutoUpdate.UseVisualStyleBackColor = true;
            chkAutoUpdate.CheckedChanged += ChkAutoUpdate_CheckedChanged;
            // 
            // dtpEnd
            // 
            dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpEnd.Font = new Font("맑은 고딕", 10F);
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Location = new Point(366, 13);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(200, 25);
            dtpEnd.TabIndex = 3;
            dtpEnd.ValueChanged += DtpEnd_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 10F);
            label2.Location = new Point(290, 15);
            label2.Name = "label2";
            label2.Size = new Size(70, 19);
            label2.TabIndex = 2;
            label2.Text = "종료 시간";
            // 
            // dtpStart
            // 
            dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpStart.Font = new Font("맑은 고딕", 10F);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(84, 13);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(200, 25);
            dtpStart.TabIndex = 1;
            dtpStart.ValueChanged += DtpStart_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 10F);
            label1.Location = new Point(14, 16);
            label1.Name = "label1";
            label1.Size = new Size(70, 19);
            label1.TabIndex = 0;
            label1.Text = "시작 시간";
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(dgvTags);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 600);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(5);
            panelBottom.Size = new Size(1200, 150);
            panelBottom.TabIndex = 2;
            // 
            // dgvTags
            // 
            dgvTags.AllowUserToAddRows = false;
            dgvTags.AllowUserToDeleteRows = false;
            dgvTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTags.Columns.AddRange(new DataGridViewColumn[] { colNo, colName, colAlias, colCurrentValue, colMinValue, colMaxValue, colVisible });
            dgvTags.Dock = DockStyle.Fill;
            dgvTags.Location = new Point(5, 5);
            dgvTags.Name = "dgvTags";
            dgvTags.ReadOnly = true;
            dgvTags.Size = new Size(1190, 140);
            dgvTags.TabIndex = 0;
            // 
            // colNo
            // 
            colNo.DataPropertyName = "No";
            colNo.FillWeight = 50F;
            colNo.HeaderText = "No";
            colNo.Name = "colNo";
            colNo.ReadOnly = true;
            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.HeaderText = "Tag 이름";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colAlias
            // 
            colAlias.DataPropertyName = "Alias";
            colAlias.HeaderText = "Alias";
            colAlias.Name = "colAlias";
            colAlias.ReadOnly = true;
            // 
            // colCurrentValue
            // 
            colCurrentValue.DataPropertyName = "CurrentValue";
            colCurrentValue.HeaderText = "현재값";
            colCurrentValue.Name = "colCurrentValue";
            colCurrentValue.ReadOnly = true;
            // 
            // colMinValue
            // 
            colMinValue.DataPropertyName = "MinValue";
            colMinValue.HeaderText = "최소값";
            colMinValue.Name = "colMinValue";
            colMinValue.ReadOnly = true;
            // 
            // colMaxValue
            // 
            colMaxValue.DataPropertyName = "MaxValue";
            colMaxValue.HeaderText = "최대값";
            colMaxValue.Name = "colMaxValue";
            colMaxValue.ReadOnly = true;
            // 
            // colVisible
            // 
            colVisible.DataPropertyName = "Visible";
            colVisible.FillWeight = 70F;
            colVisible.HeaderText = "표시";
            colVisible.Name = "colVisible";
            colVisible.ReadOnly = true;
            // 
            // ScottPlotTrendChart
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(formsPlot1);
            Controls.Add(panelTop);
            Controls.Add(panelBottom);
            MinimumSize = new Size(800, 600);
            Name = "ScottPlotTrendChart";
            Size = new Size(1200, 750);
            panelTop.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            panelControl.ResumeLayout(false);
            panelControl.PerformLayout();
            panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTags).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private Panel panelTop;
        private Panel panelControl;
        private DateTimePicker dtpStart;
        private Label label1;
        private DateTimePicker dtpEnd;
        private Label label2;
        private CheckBox chkAutoUpdate;
        private Button btnRefresh;
        private GroupBox groupBox5;
        private Button btnRealtime;
        private Button btnRange10m;
        private Button btnRange30m;
        private Button btnRange60m;
        private GroupBox groupBox4;
        private Button btnRange12h;
        private Button btnRange6h;
        private Button btnRange3h;
        private GroupBox groupBox3;
        private Button btnRange30d;
        private Button btnRange7d;
        private Button btnRange1d;
        private GroupBox groupBox2;
        private Button btnRange12mo;
        private Button btnRange6mo;
        private Button btnRange3mo;
        private Panel panelBottom;
        private DataGridView dgvTags;
        private DataGridViewTextBoxColumn colNo;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colAlias;
        private DataGridViewTextBoxColumn colCurrentValue;
        private DataGridViewTextBoxColumn colMinValue;
        private DataGridViewTextBoxColumn colMaxValue;
        private DataGridViewCheckBoxColumn colVisible;
    }
}

