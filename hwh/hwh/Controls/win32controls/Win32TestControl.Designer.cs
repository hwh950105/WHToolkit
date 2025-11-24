namespace hwh.Controls.Win32Controls
{
    partial class Win32TestControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabKeyboard;
        private System.Windows.Forms.TabPage tabMouse;
        private System.Windows.Forms.TabPage tabScreen;
        private System.Windows.Forms.TabPage tabWindow;
        private System.Windows.Forms.TabPage tabProcess;
        private System.Windows.Forms.TabPage tabHotkey;

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
            tabControl = new TabControl();
            tabKeyboard = new TabPage();
            tabMouse = new TabPage();
            tabScreen = new TabPage();
            tabWindow = new TabPage();
            tabProcess = new TabPage();
            tabHotkey = new TabPage();
            tabControl.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabKeyboard);
            tabControl.Controls.Add(tabMouse);
            tabControl.Controls.Add(tabScreen);
            tabControl.Controls.Add(tabWindow);
            tabControl.Controls.Add(tabProcess);
            tabControl.Controls.Add(tabHotkey);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("맑은 고딕", 12F);
            tabControl.ItemSize = new Size(150, 35);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.Padding = new Point(10, 5);
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1953, 875);
            tabControl.TabIndex = 0;
            // 
            // tabKeyboard
            // 
            tabKeyboard.Location = new Point(4, 39);
            tabKeyboard.Name = "tabKeyboard";
            tabKeyboard.Padding = new Padding(3);
            tabKeyboard.Size = new Size(1945, 832);
            tabKeyboard.TabIndex = 0;
            tabKeyboard.Text = "키보드 시뮬레이션";
            tabKeyboard.UseVisualStyleBackColor = true;
            // 
            // tabMouse
            // 
            tabMouse.Location = new Point(4, 39);
            tabMouse.Name = "tabMouse";
            tabMouse.Padding = new Padding(3);
            tabMouse.Size = new Size(1392, 857);
            tabMouse.TabIndex = 1;
            tabMouse.Text = "마우스 시뮬레이션";
            tabMouse.UseVisualStyleBackColor = true;
            // 
            // tabScreen
            // 
            tabScreen.Location = new Point(4, 39);
            tabScreen.Name = "tabScreen";
            tabScreen.Size = new Size(1392, 857);
            tabScreen.TabIndex = 2;
            tabScreen.Text = "화면 캡처";
            tabScreen.UseVisualStyleBackColor = true;
            // 
            // tabWindow
            // 
            tabWindow.Location = new Point(4, 39);
            tabWindow.Name = "tabWindow";
            tabWindow.Size = new Size(1392, 857);
            tabWindow.TabIndex = 3;
            tabWindow.Text = "윈도우 관리";
            tabWindow.UseVisualStyleBackColor = true;
            // 
            // tabProcess
            // 
            tabProcess.Location = new Point(4, 39);
            tabProcess.Name = "tabProcess";
            tabProcess.Size = new Size(1392, 857);
            tabProcess.TabIndex = 4;
            tabProcess.Text = "프로세스 관리";
            tabProcess.UseVisualStyleBackColor = true;
            // 
            // tabHotkey
            // 
            tabHotkey.Location = new Point(4, 39);
            tabHotkey.Name = "tabHotkey";
            tabHotkey.Size = new Size(1392, 857);
            tabHotkey.TabIndex = 5;
            tabHotkey.Text = "핫키 관리";
            tabHotkey.UseVisualStyleBackColor = true;
            // 
            // Win32TestControl
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl);
            Font = new Font("맑은 고딕", 12F);
            MinimumSize = new Size(1200, 800);
            Name = "Win32TestControl";
            Size = new Size(1953, 875);
            tabControl.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}

