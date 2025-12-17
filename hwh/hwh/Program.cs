using hwh.Core;
using hwh.Forms;

namespace hwh
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // NLog 초기화
            LogHelper.Initialize();

            // 전역 예외 처리
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            try
            {
                ApplicationConfiguration.Initialize();
                Application.Run(new FmLogin());
            }
            catch (Exception ex)
            {
                LogHelper.Fatal(ex, "애플리케이션 치명적 오류 발생");
                MessageBoxHelper.ShowError($"치명적인 오류가 발생했습니다.\n{ex.Message}", "오류");
            }
            finally
            {
                LogHelper.Shutdown();
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LogHelper.Error(e.Exception, "UI 스레드에서 처리되지 않은 예외 발생");
            MessageBoxHelper.ShowError($"오류가 발생했습니다.\n{e.Exception.Message}", "오류");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                LogHelper.Fatal(ex, $"처리되지 않은 예외 발생 (IsTerminating: {e.IsTerminating})");
            }
        }
    }
}
