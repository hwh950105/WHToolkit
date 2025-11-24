using System;
using System.Windows.Forms;

namespace hwh.Controls.Win32Controls
{
    /// <summary>
    /// Win32 API 기능 테스트 메인 컨트롤
    /// </summary>
    public partial class Win32TestControl : UserControl
    {
        public Win32TestControl()
        {
            InitializeComponent();
            InitializeTabs();
        }

        private void InitializeTabs()
        {
            // 각 탭에 서브 컨트롤 추가
            tabKeyboard.Controls.Add(new KeyboardSimulatorControl { Dock = DockStyle.Fill });
            tabMouse.Controls.Add(new MouseSimulatorControl { Dock = DockStyle.Fill });
            tabScreen.Controls.Add(new ScreenCaptureControl { Dock = DockStyle.Fill });
            tabWindow.Controls.Add(new WindowManagementControl { Dock = DockStyle.Fill });
            tabProcess.Controls.Add(new ProcessManagementControl { Dock = DockStyle.Fill });
            tabHotkey.Controls.Add(new HotkeyControl { Dock = DockStyle.Fill });
        }
    }
}

