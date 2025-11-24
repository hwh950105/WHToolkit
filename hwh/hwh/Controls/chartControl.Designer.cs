namespace hwh.Controls
{
    partial class chartControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 타이머 정리
                if (_updateTimer != null)
                {
                    _updateTimer.Stop();
                    _updateTimer.Dispose();
                }
                
                // 컴포넌트 정리
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            panel1 = new Panel();
            btnRandom = new Button();
            btnSin = new Button();
            btnLine = new Button();
            btnBar = new Button();
            btnScatter = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1.25F;
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(0, 70);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1854, 740);
            formsPlot1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnRandom);
            panel1.Controls.Add(btnSin);
            panel1.Controls.Add(btnLine);
            panel1.Controls.Add(btnBar);
            panel1.Controls.Add(btnScatter);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.MinimumSize = new Size(0, 70);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(5);
            panel1.Size = new Size(1854, 70);
            panel1.TabIndex = 1;
            // 
            // btnRandom
            // 
            btnRandom.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            btnRandom.Location = new Point(642, 12);
            btnRandom.Name = "btnRandom";
            btnRandom.Size = new Size(150, 45);
            btnRandom.TabIndex = 5;
            btnRandom.Text = "🎲 랜덤 데이터";
            btnRandom.UseVisualStyleBackColor = true;
            btnRandom.Click += btnRandom_Click;
            // 
            // btnSin
            // 
            btnSin.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            btnSin.Location = new Point(486, 12);
            btnSin.Name = "btnSin";
            btnSin.Size = new Size(150, 45);
            btnSin.TabIndex = 3;
            btnSin.Text = "🌊 Sin/Cos파";
            btnSin.UseVisualStyleBackColor = true;
            btnSin.Click += btnSin_Click;
            // 
            // btnLine
            // 
            btnLine.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            btnLine.Location = new Point(330, 12);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(150, 45);
            btnLine.TabIndex = 2;
            btnLine.Text = "📉 선 그래프";
            btnLine.UseVisualStyleBackColor = true;
            btnLine.Click += btnLine_Click;
            // 
            // btnBar
            // 
            btnBar.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            btnBar.Location = new Point(170, 12);
            btnBar.Name = "btnBar";
            btnBar.Size = new Size(150, 45);
            btnBar.TabIndex = 1;
            btnBar.Text = "📈 막대 그래프";
            btnBar.UseVisualStyleBackColor = true;
            btnBar.Click += btnBar_Click;
            // 
            // btnScatter
            // 
            btnScatter.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            btnScatter.Location = new Point(10, 12);
            btnScatter.Name = "btnScatter";
            btnScatter.Size = new Size(150, 45);
            btnScatter.TabIndex = 0;
            btnScatter.Text = "📊 산점도";
            btnScatter.UseVisualStyleBackColor = true;
            btnScatter.Click += btnScatter_Click;
            // 
            // chartControl
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(formsPlot1);
            Controls.Add(panel1);
            Font = new Font("맑은 고딕", 12F);
            MinimumSize = new Size(800, 600);
            Name = "chartControl";
            Size = new Size(1854, 810);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private Panel panel1;
        private Button btnScatter;
        private Button btnBar;
        private Button btnLine;
        private Button btnSin;
        private Button btnRandom;
    }
}
