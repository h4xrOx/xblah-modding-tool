using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace xblah_modding_tool
{
    static class Program
    {
        private const int hiding = 0;
        private const int restoring = 9;
        internal const UInt32 SWP_NOMOVE = 0x0002;
        internal const UInt32 SWP_NOSIZE = 0x0001;
        internal const UInt32 SWP_SHOWWINDOW = 0x0040;

        internal static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //XtraMessageBox.Show("This is an BETA version. Make sure to report any bugs you find, as well as suggestions you may have. \n\nhttps://github.com/jean-knapp/windows-source-modding-tool/issues", "BETA Testing");

            Application.Run(new MainForm());
        }



        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
