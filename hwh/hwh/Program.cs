using hwh.Forms;

namespace hwh
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FmLogin());
        }
    }
}
