namespace hwh.Forms
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            hopeForm1 = new ReaLTaiizor.Forms.HopeForm();
            panelSidebar = new Panel();
            panelTabBar = new Panel();
            panelContent = new Panel();
            SuspendLayout();
            // 
            // hopeForm1
            // 
            hopeForm1.ControlBoxColorH = Color.FromArgb(249, 250, 251);
            hopeForm1.ControlBoxColorHC = Color.FromArgb(239, 68, 68);
            hopeForm1.ControlBoxColorN = Color.FromArgb(255, 255, 255);
            hopeForm1.Dock = DockStyle.Top;
            hopeForm1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            hopeForm1.ForeColor = Color.FromArgb(17, 24, 39);
            hopeForm1.Image = null;
            hopeForm1.Location = new Point(0, 0);
            hopeForm1.Name = "hopeForm1";
            hopeForm1.Size = new Size(1367, 40);
            hopeForm1.TabIndex = 0;
            hopeForm1.Text = " ";
            hopeForm1.ThemeColor = Color.FromArgb(124, 58, 237);
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(17, 24, 39);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 40);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(280, 859);
            panelSidebar.TabIndex = 1;
            panelSidebar.Paint += PanelSidebar_Paint;
            // 
            // panelTabBar
            // 
            panelTabBar.BackColor = Color.FromArgb(255, 255, 255);
            panelTabBar.Dock = DockStyle.Top;
            panelTabBar.Location = new Point(280, 40);
            panelTabBar.Name = "panelTabBar";
            panelTabBar.Size = new Size(1087, 56);
            panelTabBar.TabIndex = 2;
            panelTabBar.Paint += PanelTabBar_Paint;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(249, 250, 251);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(280, 96);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(24);
            panelContent.Size = new Size(1087, 803);
            panelContent.TabIndex = 3;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1367, 899);
            Controls.Add(panelContent);
            Controls.Add(panelTabBar);
            Controls.Add(panelSidebar);
            Controls.Add(hopeForm1);
            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = new Size(2560, 1392);
            MinimumSize = new Size(190, 40);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Forms.HopeForm hopeForm1;
        private Panel panelSidebar;
        private Panel panelTabBar;
        private Panel panelContent;
    }
}
