namespace hwh.Controls
{
    partial class Maketest
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            airButton1 = new ReaLTaiizor.Controls.AirButton();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // airButton1
            // 
            airButton1.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            airButton1.Font = new Font("Segoe UI", 9F);
            airButton1.Image = null;
            airButton1.Location = new Point(117, 201);
            airButton1.Name = "airButton1";
            airButton1.NoRounding = false;
            airButton1.Size = new Size(100, 45);
            airButton1.TabIndex = 0;
            airButton1.Text = "airButton1";
            airButton1.Transparent = false;
            airButton1.Click += airButton1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(299, 104);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(438, 462);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // Maketest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(richTextBox1);
            Controls.Add(airButton1);
            Name = "Maketest";
            Size = new Size(931, 681);
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.AirButton airButton1;
        private RichTextBox richTextBox1;
    }
}
