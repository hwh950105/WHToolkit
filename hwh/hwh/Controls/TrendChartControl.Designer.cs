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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            panelTop = new Panel();
            panelRangeButtons = new Panel();
            btnRealtime = new ReaLTaiizor.Controls.MaterialButton();
            btnRange10m = new ReaLTaiizor.Controls.MaterialButton();
            btnRange30m = new ReaLTaiizor.Controls.MaterialButton();
            btnRange60m = new ReaLTaiizor.Controls.MaterialButton();
            btnRange3h = new ReaLTaiizor.Controls.MaterialButton();
            btnRange6h = new ReaLTaiizor.Controls.MaterialButton();
            btnRange12h = new ReaLTaiizor.Controls.MaterialButton();
            btnRange1d = new ReaLTaiizor.Controls.MaterialButton();
            btnRange7d = new ReaLTaiizor.Controls.MaterialButton();
            btnRange30d = new ReaLTaiizor.Controls.MaterialButton();
            btnRange3mo = new ReaLTaiizor.Controls.MaterialButton();
            btnRange6mo = new ReaLTaiizor.Controls.MaterialButton();
            btnRange12mo = new ReaLTaiizor.Controls.MaterialButton();
            panelControl = new Panel();
            btnRefresh = new ReaLTaiizor.Controls.MaterialButton();
            chkAutoUpdate = new ReaLTaiizor.Controls.MaterialSwitch();
            dtpEnd = new DateTimePicker();
            lblEndTime = new ReaLTaiizor.Controls.MaterialLabel();
            dtpStart = new DateTimePicker();
            lblStartTime = new ReaLTaiizor.Controls.MaterialLabel();
            panelBottom = new Panel();
            dgvTags = new DataGridView();
            colNo = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colAlias = new DataGridViewTextBoxColumn();
            colCurrentValue = new DataGridViewTextBoxColumn();
            colMinValue = new DataGridViewTextBoxColumn();
            colMaxValue = new DataGridViewTextBoxColumn();
            colVisible = new DataGridViewCheckBoxColumn();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            groupBox5 = new GroupBox();
            label1 = new Label();
            label2 = new Label();
            panelTop.SuspendLayout();
            panelRangeButtons.SuspendLayout();
            panelControl.SuspendLayout();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTags).BeginInit();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1.25F;
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(0, 120);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1200, 480);
            formsPlot1.TabIndex = 0;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(249, 250, 251);
            panelTop.Controls.Add(panelRangeButtons);
            panelTop.Controls.Add(panelControl);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(12);
            panelTop.Size = new Size(1200, 120);
            panelTop.TabIndex = 1;
            // 
            // panelRangeButtons
            // 
            panelRangeButtons.BackColor = Color.White;
            panelRangeButtons.Controls.Add(btnRealtime);
            panelRangeButtons.Controls.Add(btnRange10m);
            panelRangeButtons.Controls.Add(btnRange30m);
            panelRangeButtons.Controls.Add(btnRange60m);
            panelRangeButtons.Controls.Add(btnRange3h);
            panelRangeButtons.Controls.Add(btnRange6h);
            panelRangeButtons.Controls.Add(btnRange12h);
            panelRangeButtons.Controls.Add(btnRange1d);
            panelRangeButtons.Controls.Add(btnRange7d);
            panelRangeButtons.Controls.Add(btnRange30d);
            panelRangeButtons.Controls.Add(btnRange3mo);
            panelRangeButtons.Controls.Add(btnRange6mo);
            panelRangeButtons.Controls.Add(btnRange12mo);
            panelRangeButtons.Dock = DockStyle.Top;
            panelRangeButtons.Location = new Point(12, 12);
            panelRangeButtons.Name = "panelRangeButtons";
            panelRangeButtons.Padding = new Padding(8);
            panelRangeButtons.Size = new Size(1176, 50);
            panelRangeButtons.TabIndex = 0;
            // 
            // btnRealtime
            // 
            btnRealtime.AutoSize = false;
            btnRealtime.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRealtime.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRealtime.Cursor = Cursors.Hand;
            btnRealtime.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRealtime.Depth = 0;
            btnRealtime.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            btnRealtime.HighEmphasis = true;
            btnRealtime.Icon = null;
            btnRealtime.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRealtime.Location = new Point(8, 8);
            btnRealtime.Margin = new Padding(4, 6, 4, 6);
            btnRealtime.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRealtime.Name = "btnRealtime";
            btnRealtime.NoAccentTextColor = Color.Empty;
            btnRealtime.Size = new Size(70, 32);
            btnRealtime.TabIndex = 0;
            btnRealtime.Text = "▶ 실시간";
            btnRealtime.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            btnRealtime.UseAccentColor = false;
            btnRealtime.UseVisualStyleBackColor = true;
            btnRealtime.Click += BtnRange_Click;
            // 
            // btnRange10m
            // 
            btnRange10m.AutoSize = false;
            btnRange10m.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange10m.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange10m.Cursor = Cursors.Hand;
            btnRange10m.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange10m.Depth = 0;
            btnRange10m.Font = new Font("맑은 고딕", 9F);
            btnRange10m.HighEmphasis = false;
            btnRange10m.Icon = null;
            btnRange10m.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange10m.Location = new Point(86, 8);
            btnRange10m.Margin = new Padding(4, 6, 4, 6);
            btnRange10m.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange10m.Name = "btnRange10m";
            btnRange10m.NoAccentTextColor = Color.Empty;
            btnRange10m.Size = new Size(55, 32);
            btnRange10m.TabIndex = 1;
            btnRange10m.Text = "10분";
            btnRange10m.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange10m.UseAccentColor = false;
            btnRange10m.UseVisualStyleBackColor = true;
            btnRange10m.Click += BtnRange_Click;
            // 
            // btnRange30m
            // 
            btnRange30m.AutoSize = false;
            btnRange30m.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange30m.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange30m.Cursor = Cursors.Hand;
            btnRange30m.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange30m.Depth = 0;
            btnRange30m.Font = new Font("맑은 고딕", 9F);
            btnRange30m.HighEmphasis = false;
            btnRange30m.Icon = null;
            btnRange30m.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange30m.Location = new Point(149, 8);
            btnRange30m.Margin = new Padding(4, 6, 4, 6);
            btnRange30m.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange30m.Name = "btnRange30m";
            btnRange30m.NoAccentTextColor = Color.Empty;
            btnRange30m.Size = new Size(55, 32);
            btnRange30m.TabIndex = 2;
            btnRange30m.Text = "30분";
            btnRange30m.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange30m.UseAccentColor = false;
            btnRange30m.UseVisualStyleBackColor = true;
            btnRange30m.Click += BtnRange_Click;
            // 
            // btnRange60m
            // 
            btnRange60m.AutoSize = false;
            btnRange60m.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange60m.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange60m.Cursor = Cursors.Hand;
            btnRange60m.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange60m.Depth = 0;
            btnRange60m.Font = new Font("맑은 고딕", 9F);
            btnRange60m.HighEmphasis = false;
            btnRange60m.Icon = null;
            btnRange60m.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange60m.Location = new Point(212, 8);
            btnRange60m.Margin = new Padding(4, 6, 4, 6);
            btnRange60m.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange60m.Name = "btnRange60m";
            btnRange60m.NoAccentTextColor = Color.Empty;
            btnRange60m.Size = new Size(55, 32);
            btnRange60m.TabIndex = 3;
            btnRange60m.Text = "60분";
            btnRange60m.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange60m.UseAccentColor = false;
            btnRange60m.UseVisualStyleBackColor = true;
            btnRange60m.Click += BtnRange_Click;
            // 
            // btnRange3h
            // 
            btnRange3h.AutoSize = false;
            btnRange3h.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange3h.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange3h.Cursor = Cursors.Hand;
            btnRange3h.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange3h.Depth = 0;
            btnRange3h.Font = new Font("맑은 고딕", 9F);
            btnRange3h.HighEmphasis = false;
            btnRange3h.Icon = null;
            btnRange3h.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange3h.Location = new Point(280, 8);
            btnRange3h.Margin = new Padding(4, 6, 4, 6);
            btnRange3h.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange3h.Name = "btnRange3h";
            btnRange3h.NoAccentTextColor = Color.Empty;
            btnRange3h.Size = new Size(60, 32);
            btnRange3h.TabIndex = 4;
            btnRange3h.Text = "3시간";
            btnRange3h.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange3h.UseAccentColor = false;
            btnRange3h.UseVisualStyleBackColor = true;
            btnRange3h.Click += BtnRange_Click;
            // 
            // btnRange6h
            // 
            btnRange6h.AutoSize = false;
            btnRange6h.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange6h.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange6h.Cursor = Cursors.Hand;
            btnRange6h.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange6h.Depth = 0;
            btnRange6h.Font = new Font("맑은 고딕", 9F);
            btnRange6h.HighEmphasis = false;
            btnRange6h.Icon = null;
            btnRange6h.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange6h.Location = new Point(348, 8);
            btnRange6h.Margin = new Padding(4, 6, 4, 6);
            btnRange6h.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange6h.Name = "btnRange6h";
            btnRange6h.NoAccentTextColor = Color.Empty;
            btnRange6h.Size = new Size(60, 32);
            btnRange6h.TabIndex = 5;
            btnRange6h.Text = "6시간";
            btnRange6h.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange6h.UseAccentColor = false;
            btnRange6h.UseVisualStyleBackColor = true;
            btnRange6h.Click += BtnRange_Click;
            // 
            // btnRange12h
            // 
            btnRange12h.AutoSize = false;
            btnRange12h.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange12h.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange12h.Cursor = Cursors.Hand;
            btnRange12h.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange12h.Depth = 0;
            btnRange12h.Font = new Font("맑은 고딕", 9F);
            btnRange12h.HighEmphasis = false;
            btnRange12h.Icon = null;
            btnRange12h.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange12h.Location = new Point(416, 8);
            btnRange12h.Margin = new Padding(4, 6, 4, 6);
            btnRange12h.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange12h.Name = "btnRange12h";
            btnRange12h.NoAccentTextColor = Color.Empty;
            btnRange12h.Size = new Size(65, 32);
            btnRange12h.TabIndex = 6;
            btnRange12h.Text = "12시간";
            btnRange12h.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange12h.UseAccentColor = false;
            btnRange12h.UseVisualStyleBackColor = true;
            btnRange12h.Click += BtnRange_Click;
            // 
            // btnRange1d
            // 
            btnRange1d.AutoSize = false;
            btnRange1d.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange1d.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange1d.Cursor = Cursors.Hand;
            btnRange1d.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange1d.Depth = 0;
            btnRange1d.Font = new Font("맑은 고딕", 9F);
            btnRange1d.HighEmphasis = false;
            btnRange1d.Icon = null;
            btnRange1d.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange1d.Location = new Point(494, 8);
            btnRange1d.Margin = new Padding(4, 6, 4, 6);
            btnRange1d.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange1d.Name = "btnRange1d";
            btnRange1d.NoAccentTextColor = Color.Empty;
            btnRange1d.Size = new Size(50, 32);
            btnRange1d.TabIndex = 7;
            btnRange1d.Text = "1일";
            btnRange1d.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange1d.UseAccentColor = false;
            btnRange1d.UseVisualStyleBackColor = true;
            btnRange1d.Click += BtnRange_Click;
            // 
            // btnRange7d
            // 
            btnRange7d.AutoSize = false;
            btnRange7d.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange7d.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange7d.Cursor = Cursors.Hand;
            btnRange7d.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange7d.Depth = 0;
            btnRange7d.Font = new Font("맑은 고딕", 9F);
            btnRange7d.HighEmphasis = false;
            btnRange7d.Icon = null;
            btnRange7d.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange7d.Location = new Point(552, 8);
            btnRange7d.Margin = new Padding(4, 6, 4, 6);
            btnRange7d.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange7d.Name = "btnRange7d";
            btnRange7d.NoAccentTextColor = Color.Empty;
            btnRange7d.Size = new Size(50, 32);
            btnRange7d.TabIndex = 8;
            btnRange7d.Text = "7일";
            btnRange7d.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange7d.UseAccentColor = false;
            btnRange7d.UseVisualStyleBackColor = true;
            btnRange7d.Click += BtnRange_Click;
            // 
            // btnRange30d
            // 
            btnRange30d.AutoSize = false;
            btnRange30d.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange30d.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange30d.Cursor = Cursors.Hand;
            btnRange30d.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange30d.Depth = 0;
            btnRange30d.Font = new Font("맑은 고딕", 9F);
            btnRange30d.HighEmphasis = false;
            btnRange30d.Icon = null;
            btnRange30d.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange30d.Location = new Point(610, 8);
            btnRange30d.Margin = new Padding(4, 6, 4, 6);
            btnRange30d.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange30d.Name = "btnRange30d";
            btnRange30d.NoAccentTextColor = Color.Empty;
            btnRange30d.Size = new Size(55, 32);
            btnRange30d.TabIndex = 9;
            btnRange30d.Text = "30일";
            btnRange30d.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange30d.UseAccentColor = false;
            btnRange30d.UseVisualStyleBackColor = true;
            btnRange30d.Click += BtnRange_Click;
            // 
            // btnRange3mo
            // 
            btnRange3mo.AutoSize = false;
            btnRange3mo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange3mo.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange3mo.Cursor = Cursors.Hand;
            btnRange3mo.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange3mo.Depth = 0;
            btnRange3mo.Font = new Font("맑은 고딕", 9F);
            btnRange3mo.HighEmphasis = false;
            btnRange3mo.Icon = null;
            btnRange3mo.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange3mo.Location = new Point(678, 8);
            btnRange3mo.Margin = new Padding(4, 6, 4, 6);
            btnRange3mo.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange3mo.Name = "btnRange3mo";
            btnRange3mo.NoAccentTextColor = Color.Empty;
            btnRange3mo.Size = new Size(60, 32);
            btnRange3mo.TabIndex = 10;
            btnRange3mo.Text = "3개월";
            btnRange3mo.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange3mo.UseAccentColor = false;
            btnRange3mo.UseVisualStyleBackColor = true;
            btnRange3mo.Click += BtnRange_Click;
            // 
            // btnRange6mo
            // 
            btnRange6mo.AutoSize = false;
            btnRange6mo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange6mo.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange6mo.Cursor = Cursors.Hand;
            btnRange6mo.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange6mo.Depth = 0;
            btnRange6mo.Font = new Font("맑은 고딕", 9F);
            btnRange6mo.HighEmphasis = false;
            btnRange6mo.Icon = null;
            btnRange6mo.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange6mo.Location = new Point(746, 8);
            btnRange6mo.Margin = new Padding(4, 6, 4, 6);
            btnRange6mo.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange6mo.Name = "btnRange6mo";
            btnRange6mo.NoAccentTextColor = Color.Empty;
            btnRange6mo.Size = new Size(60, 32);
            btnRange6mo.TabIndex = 11;
            btnRange6mo.Text = "pm";
            btnRange6mo.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange6mo.UseAccentColor = false;
            btnRange6mo.UseVisualStyleBackColor = true;
            btnRange6mo.Click += BtnRange_Click;
            // 
            // btnRange12mo
            // 
            btnRange12mo.AutoSize = false;
            btnRange12mo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRange12mo.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRange12mo.Cursor = Cursors.Hand;
            btnRange12mo.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRange12mo.Depth = 0;
            btnRange12mo.Font = new Font("맑은 고딕", 9F);
            btnRange12mo.HighEmphasis = false;
            btnRange12mo.Icon = null;
            btnRange12mo.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRange12mo.Location = new Point(814, 8);
            btnRange12mo.Margin = new Padding(4, 6, 4, 6);
            btnRange12mo.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRange12mo.Name = "btnRange12mo";
            btnRange12mo.NoAccentTextColor = Color.Empty;
            btnRange12mo.Size = new Size(65, 32);
            btnRange12mo.TabIndex = 12;
            btnRange12mo.Text = "12개월";
            btnRange12mo.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnRange12mo.UseAccentColor = false;
            btnRange12mo.UseVisualStyleBackColor = true;
            btnRange12mo.Click += BtnRange_Click;
            // 
            // panelControl
            // 
            panelControl.BackColor = Color.White;
            panelControl.Controls.Add(btnRefresh);
            panelControl.Controls.Add(chkAutoUpdate);
            panelControl.Controls.Add(dtpEnd);
            panelControl.Controls.Add(lblEndTime);
            panelControl.Controls.Add(dtpStart);
            panelControl.Controls.Add(lblStartTime);
            panelControl.Dock = DockStyle.Bottom;
            panelControl.Location = new Point(12, 58);
            panelControl.Name = "panelControl";
            panelControl.Padding = new Padding(12, 8, 12, 8);
            panelControl.Size = new Size(1176, 50);
            panelControl.TabIndex = 1;
            // 
            // btnRefresh
            // 
            btnRefresh.AutoSize = false;
            btnRefresh.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRefresh.CharacterCasing = ReaLTaiizor.Controls.MaterialButton.CharacterCasingEnum.Normal;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRefresh.Depth = 0;
            btnRefresh.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            btnRefresh.HighEmphasis = true;
            btnRefresh.Icon = null;
            btnRefresh.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            btnRefresh.Location = new Point(780, 8);
            btnRefresh.Margin = new Padding(4, 6, 4, 6);
            btnRefresh.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            btnRefresh.Name = "btnRefresh";
            btnRefresh.NoAccentTextColor = Color.Empty;
            btnRefresh.Size = new Size(100, 32);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "🔄 새로고침";
            btnRefresh.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            btnRefresh.UseAccentColor = false;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // chkAutoUpdate
            // 
            chkAutoUpdate.AutoSize = true;
            chkAutoUpdate.Depth = 0;
            chkAutoUpdate.Font = new Font("맑은 고딕", 10F);
            chkAutoUpdate.Location = new Point(610, 8);
            chkAutoUpdate.Margin = new Padding(0);
            chkAutoUpdate.MouseLocation = new Point(-1, -1);
            chkAutoUpdate.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            chkAutoUpdate.Name = "chkAutoUpdate";
            chkAutoUpdate.Ripple = true;
            chkAutoUpdate.Size = new Size(134, 37);
            chkAutoUpdate.TabIndex = 4;
            chkAutoUpdate.Text = "자동 업데이트";
            chkAutoUpdate.UseAccentColor = false;
            chkAutoUpdate.UseVisualStyleBackColor = true;
            chkAutoUpdate.CheckedChanged += ChkAutoUpdate_CheckedChanged;
            // 
            // dtpEnd
            // 
            dtpEnd.CalendarFont = new Font("맑은 고딕", 10F);
            dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpEnd.Font = new Font("맑은 고딕", 10F);
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Location = new Point(388, 11);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(200, 25);
            dtpEnd.TabIndex = 3;
            dtpEnd.ValueChanged += DtpEnd_ValueChanged;
            // 
            // lblEndTime
            // 
            lblEndTime.AutoSize = true;
            lblEndTime.Depth = 0;
            lblEndTime.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblEndTime.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle2;
            lblEndTime.ForeColor = Color.FromArgb(55, 65, 81);
            lblEndTime.HighEmphasis = true;
            lblEndTime.Location = new Point(310, 15);
            lblEndTime.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblEndTime.Name = "lblEndTime";
            lblEndTime.Size = new Size(44, 17);
            lblEndTime.TabIndex = 2;
            lblEndTime.Text = "종료 시간";
            // 
            // dtpStart
            // 
            dtpStart.CalendarFont = new Font("맑은 고딕", 10F);
            dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpStart.Font = new Font("맑은 고딕", 10F);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(90, 11);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(200, 25);
            dtpStart.TabIndex = 1;
            dtpStart.ValueChanged += DtpStart_ValueChanged;
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Depth = 0;
            lblStartTime.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblStartTime.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle2;
            lblStartTime.ForeColor = Color.FromArgb(55, 65, 81);
            lblStartTime.HighEmphasis = true;
            lblStartTime.Location = new Point(12, 15);
            lblStartTime.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(44, 17);
            lblStartTime.TabIndex = 0;
            lblStartTime.Text = "시작 시간";
            // 
            // panelBottom
            // 
            panelBottom.BackColor = Color.FromArgb(249, 250, 251);
            panelBottom.Controls.Add(dgvTags);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 600);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(12);
            panelBottom.Size = new Size(1200, 150);
            panelBottom.TabIndex = 2;
            // 
            // dgvTags
            // 
            dgvTags.AllowUserToAddRows = false;
            dgvTags.AllowUserToDeleteRows = false;
            dgvTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTags.BackgroundColor = Color.White;
            dgvTags.BorderStyle = BorderStyle.None;
            dgvTags.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTags.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTags.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvTags.ColumnHeadersHeight = 35;
            dgvTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTags.Columns.AddRange(new DataGridViewColumn[] { colNo, colName, colAlias, colCurrentValue, colMinValue, colMaxValue, colVisible });
            dgvTags.Dock = DockStyle.Fill;
            dgvTags.EnableHeadersVisualStyles = false;
            dgvTags.GridColor = Color.FromArgb(229, 231, 235);
            dgvTags.Location = new Point(12, 12);
            dgvTags.Name = "dgvTags";
            dgvTags.ReadOnly = true;
            dgvTags.RowHeadersVisible = false;
            dgvTags.RowTemplate.Height = 30;
            dgvTags.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTags.Size = new Size(1176, 126);
            dgvTags.TabIndex = 0;
            // 
            // colNo
            // 
            colNo.DataPropertyName = "No";
            colNo.FillWeight = 40F;
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
            colVisible.FillWeight = 50F;
            colVisible.HeaderText = "표시";
            colVisible.Name = "colVisible";
            colVisible.ReadOnly = true;
            // 
            // groupBox2
            // 
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 100);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Visible = false;
            // 
            // groupBox3
            // 
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(200, 100);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Visible = false;
            // 
            // groupBox4
            // 
            groupBox4.Location = new Point(0, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(200, 100);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Visible = false;
            // 
            // groupBox5
            // 
            groupBox5.Location = new Point(0, 0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(200, 100);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Visible = false;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            label1.Visible = false;
            // 
            // label2
            // 
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 0;
            label2.Visible = false;
            // 
            // ScottPlotTrendChart
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(249, 250, 251);
            Controls.Add(formsPlot1);
            Controls.Add(panelTop);
            Controls.Add(panelBottom);
            MinimumSize = new Size(800, 600);
            Name = "ScottPlotTrendChart";
            Size = new Size(1200, 750);
            panelTop.ResumeLayout(false);
            panelRangeButtons.ResumeLayout(false);
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
        private Panel panelRangeButtons;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private ReaLTaiizor.Controls.MaterialLabel lblStartTime;
        private ReaLTaiizor.Controls.MaterialLabel lblEndTime;
        private ReaLTaiizor.Controls.MaterialSwitch chkAutoUpdate;
        private ReaLTaiizor.Controls.MaterialButton btnRefresh;
        private ReaLTaiizor.Controls.MaterialButton btnRealtime;
        private ReaLTaiizor.Controls.MaterialButton btnRange10m;
        private ReaLTaiizor.Controls.MaterialButton btnRange30m;
        private ReaLTaiizor.Controls.MaterialButton btnRange60m;
        private ReaLTaiizor.Controls.MaterialButton btnRange3h;
        private ReaLTaiizor.Controls.MaterialButton btnRange6h;
        private ReaLTaiizor.Controls.MaterialButton btnRange12h;
        private ReaLTaiizor.Controls.MaterialButton btnRange1d;
        private ReaLTaiizor.Controls.MaterialButton btnRange7d;
        private ReaLTaiizor.Controls.MaterialButton btnRange30d;
        private ReaLTaiizor.Controls.MaterialButton btnRange3mo;
        private ReaLTaiizor.Controls.MaterialButton btnRange6mo;
        private ReaLTaiizor.Controls.MaterialButton btnRange12mo;
        private Panel panelBottom;
        private DataGridView dgvTags;
        private DataGridViewTextBoxColumn colNo;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colAlias;
        private DataGridViewTextBoxColumn colCurrentValue;
        private DataGridViewTextBoxColumn colMinValue;
        private DataGridViewTextBoxColumn colMaxValue;
        private DataGridViewCheckBoxColumn colVisible;
        
        // 하위 호환성용
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private Label label1;
        private Label label2;
    }
}
