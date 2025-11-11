using System.Linq;
using System.Windows.Forms;

namespace hwh.Core
{
    /// <summary>
    /// 커스텀 메시지박스를 쉽게 사용하기 위한 헬퍼 클래스
    /// </summary>
    public static class MessageBoxHelper
    {
        /// <summary>
        /// 메인 폼 가져오기
        /// </summary>
        private static Form? GetMainForm()
        {
            // Main 이름의 폼 찾기 (메인 폼)
            return Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Name == "Main") 
                   ?? Application.OpenForms.Cast<Form>().FirstOrDefault();
        }

        /// <summary>
        /// 정보 메시지 표시
        /// </summary>
        public static DialogResult ShowInfo(string message, string title = "알림", Form? owner = null)
        {
            using (var msgBox = new CustomMessageBox(title, message, MessageBoxType.Info, MessageBoxButtons.OK))
            {
                var parent = owner ?? GetMainForm();
                if (parent != null)
                {
                    msgBox.ShowDialog(parent);
                }
                else
                {
                    msgBox.ShowDialog();
                }
                return msgBox.Result;
            }
        }

        /// <summary>
        /// 성공 메시지 표시
        /// </summary>
        public static DialogResult ShowSuccess(string message, string title = "성공", Form? owner = null)
        {
            using (var msgBox = new CustomMessageBox(title, message, MessageBoxType.Success, MessageBoxButtons.OK))
            {
                var parent = owner ?? GetMainForm();
                if (parent != null)
                {
                    msgBox.ShowDialog(parent);
                }
                else
                {
                    msgBox.ShowDialog();
                }
                return msgBox.Result;
            }
        }

        /// <summary>
        /// 경고 메시지 표시
        /// </summary>
        public static DialogResult ShowWarning(string message, string title = "경고", Form? owner = null)
        {
            using (var msgBox = new CustomMessageBox(title, message, MessageBoxType.Warning, MessageBoxButtons.OK))
            {
                var parent = owner ?? GetMainForm();
                if (parent != null)
                {
                    msgBox.ShowDialog(parent);
                }
                else
                {
                    msgBox.ShowDialog();
                }
                return msgBox.Result;
            }
        }

        /// <summary>
        /// 에러 메시지 표시
        /// </summary>
        public static DialogResult ShowError(string message, string title = "오류", Form? owner = null)
        {
            using (var msgBox = new CustomMessageBox(title, message, MessageBoxType.Error, MessageBoxButtons.OK))
            {
                var parent = owner ?? GetMainForm();
                if (parent != null)
                {
                    msgBox.ShowDialog(parent);
                }
                else
                {
                    msgBox.ShowDialog();
                }
                return msgBox.Result;
            }
        }

        /// <summary>
        /// 확인 질문 (예/아니오)
        /// </summary>
        public static DialogResult ShowQuestion(string message, string title = "확인", Form? owner = null)
        {
            using (var msgBox = new CustomMessageBox(title, message, MessageBoxType.Question, MessageBoxButtons.YesNo))
            {
                var parent = owner ?? GetMainForm();
                if (parent != null)
                {
                    msgBox.ShowDialog(parent);
                }
                else
                {
                    msgBox.ShowDialog();
                }
                return msgBox.Result;
            }
        }

        /// <summary>
        /// 확인 질문 (확인/취소)
        /// </summary>
        public static DialogResult ShowConfirm(string message, string title = "확인", Form? owner = null)
        {
            using (var msgBox = new CustomMessageBox(title, message, MessageBoxType.Question, MessageBoxButtons.OKCancel))
            {
                var parent = owner ?? GetMainForm();
                if (parent != null)
                {
                    msgBox.ShowDialog(parent);
                }
                else
                {
                    msgBox.ShowDialog();
                }
                return msgBox.Result;
            }
        }

        /// <summary>
        /// 커스텀 메시지박스 표시
        /// </summary>
        public static DialogResult Show(string message, string title, MessageBoxType type, MessageBoxButtons buttons, Form? owner = null)
        {
            using (var msgBox = new CustomMessageBox(title, message, type, buttons))
            {
                var parent = owner ?? GetMainForm();
                if (parent != null)
                {
                    msgBox.ShowDialog(parent);
                }
                else
                {
                    msgBox.ShowDialog();
                }
                return msgBox.Result;
            }
        }
    }
}

