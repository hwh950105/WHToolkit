using System;
using System.Windows.Forms;
using Win32.Wrapper;
using static Win32.Win32Enums;

namespace hwh.Controls.Win32Controls
{
    /// <summary>
    /// 핫키 관리 테스트 컨트롤
    /// </summary>
    public partial class HotkeyControl : UserControl
    {
        private int currentHotkeyId = 1;

        public HotkeyControl()
        {
            InitializeComponent();
        }

        private void HotkeyControl_Load(object sender, EventArgs e)
        {
            // 수정자 키 콤보박스 초기화
            comboModifiers.Items.Add("None");
            comboModifiers.Items.Add("Alt");
            comboModifiers.Items.Add("Control");
            comboModifiers.Items.Add("Shift");
            comboModifiers.Items.Add("Win");
            comboModifiers.Items.Add("Alt + Control");
            comboModifiers.Items.Add("Alt + Shift");
            comboModifiers.Items.Add("Control + Shift");
            comboModifiers.SelectedIndex = 0;

            // 키 콤보박스 초기화
            comboKey.Items.Add(Keys.F1.ToString());
            comboKey.Items.Add(Keys.F2.ToString());
            comboKey.Items.Add(Keys.F3.ToString());
            comboKey.Items.Add(Keys.F4.ToString());
            comboKey.Items.Add(Keys.F5.ToString());
            comboKey.Items.Add(Keys.F6.ToString());
            comboKey.Items.Add(Keys.F7.ToString());
            comboKey.Items.Add(Keys.F8.ToString());
            comboKey.Items.Add(Keys.F9.ToString());
            comboKey.Items.Add(Keys.F10.ToString());
            comboKey.Items.Add(Keys.F11.ToString());
            comboKey.Items.Add(Keys.F12.ToString());
            comboKey.Items.Add(Keys.A.ToString());
            comboKey.Items.Add(Keys.B.ToString());
            comboKey.Items.Add(Keys.C.ToString());
            comboKey.Items.Add(Keys.D.ToString());
            comboKey.Items.Add(Keys.E.ToString());
            comboKey.Items.Add(Keys.F.ToString());
            comboKey.Items.Add(Keys.G.ToString());
            comboKey.Items.Add(Keys.H.ToString());
            comboKey.SelectedIndex = 0;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var modifierText = comboModifiers.SelectedItem?.ToString();
                var keyText = comboKey.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(modifierText) ||
                    string.IsNullOrWhiteSpace(keyText))
                {
                    MessageBox.Show("수정자 키와 키를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                HotkeyModifiers modifiers = GetModifiers(modifierText);
                Keys key = (Keys)Enum.Parse(typeof(Keys), keyText);

                HotkeyManager.Register(currentHotkeyId, modifiers, key);
                
                string hotkeyText = $"ID: {currentHotkeyId}, {comboModifiers.SelectedItem} + {key}";
                listHotkeys.Items.Add(hotkeyText);
                
                lblStatus.Text = $"핫키 등록 성공: {comboModifiers.SelectedItem} + {key} (ID: {currentHotkeyId})";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                
                currentHotkeyId++;
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "핫키 등록 실패");
                lblStatus.Text = $"핫키 등록 실패: {ex.Message}";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnUnregister_Click(object sender, EventArgs e)
        {
            try
            {
                if (listHotkeys.SelectedIndex < 0)
                {
                    MessageBox.Show("해제할 핫키를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (listHotkeys.SelectedItem is not string selectedItem || string.IsNullOrWhiteSpace(selectedItem))
                {
                    MessageBox.Show("해제할 항목을 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = ExtractIdFromString(selectedItem);

                HotkeyManager.Unregister(id);
                listHotkeys.Items.RemoveAt(listHotkeys.SelectedIndex);

                lblStatus.Text = $"핫키 해제 성공 (ID: {id})";
                lblStatus.ForeColor = System.Drawing.Color.Blue;
            }
            catch (Exception ex)
            {
                Core.LogHelper.Error(ex, "핫키 해제 실패");
                lblStatus.Text = $"핫키 해제 실패: {ex.Message}";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private HotkeyModifiers GetModifiers(string modifier)
        {
            return modifier switch
            {
                "None" => 0,
                "Alt" => HotkeyModifiers.Alt,
                "Control" => HotkeyModifiers.Control,
                "Shift" => HotkeyModifiers.Shift,
                "Win" => HotkeyModifiers.Win,
                "Alt + Control" => HotkeyModifiers.Alt | HotkeyModifiers.Control,
                "Alt + Shift" => HotkeyModifiers.Alt | HotkeyModifiers.Shift,
                "Control + Shift" => HotkeyModifiers.Control | HotkeyModifiers.Shift,
                _ => 0
            };
        }

        private int ExtractIdFromString(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("핫키 정보가 올바르지 않습니다.", nameof(text));

            // "ID: 1, ..." 형식에서 ID 추출
            int startIndex = text.IndexOf("ID: ") + 4;
            int endIndex = text.IndexOf(",", startIndex);
            string idStr = text.Substring(startIndex, endIndex - startIndex);
            return int.Parse(idStr);
        }
    }
}

