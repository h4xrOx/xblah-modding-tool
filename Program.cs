using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace source_modding_tool
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

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, Int32 lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hwndNewParent);

        [DllImport("user32.dll")]
        internal static extern bool SetWindowPos(IntPtr hWnd,
                                                 IntPtr hWndInsertAfter,
                                                 int X,
                                                 int Y,
                                                 int cx,
                                                 int cy,
                                                 uint uFlags);

        [DllImport("User32")]
        internal static extern int ShowWindow(int hwnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
