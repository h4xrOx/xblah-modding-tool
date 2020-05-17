using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace source_modding_tool.SourceSDK
{
    class Instance
    {
        private Control parent = null;
        private Launcher launcher = null;
        public Process modProcess = null;

        public bool isLoading = false;

        public Instance(Launcher launcher, Control parent)
        {
            this.launcher = launcher;
            this.parent = parent;
        }

        internal void Stop()
        {
            if (modProcess != null)
            {
                modProcess.Kill();
                modProcess = null;
            }
        }

        public void Command(string command)
        {
            if (modProcess == null)
                return;

            string gamePath = launcher.GetCurrentGame().installPath;
            string modPath = launcher.GetCurrentMod().installPath;

            Debug.Write(modPath);

            string exePath = string.Empty;

            foreach (string file in Directory.GetFiles(gamePath))
            {
                if (new FileInfo(file).Extension == ".exe")
                {
                    exePath = file;
                    break;
                }
            }

            Program.SetParent(modProcess.MainWindowHandle, IntPtr.Zero);
            Program.ShowWindow((int)modProcess.MainWindowHandle, 0);

            Process process = new Process();
            process.StartInfo.FileName = exePath;
            process.StartInfo.Arguments = "-hijack " + command;
            process.Start();
            process.EnableRaisingEvents = true;
            process.WaitForInputIdle();
            Program.ShowWindow((int)modProcess.MainWindowHandle, 9);
            Program.SetParent(modProcess.MainWindowHandle, parent.Handle);
        }

        public void Resize()
        {
            try
            {
                if (modProcess != null)
                {
                    //Command("-width " + parent.Width + " -height " + parent.Height);
                    //File.WriteAllText(sourceSDK.GetModPath() + "\\cfg\\cmd.cfg", "mat_setvideomode " + parent.Width + " " + parent.Height + " 1");
                    Program.MoveWindow(modProcess.MainWindowHandle, 0, 0, parent.Width, parent.Height, true);
                }
            }
            catch (Exception) { }
        }

        public void KillExistant()
        {
            foreach (var process in Process.GetProcessesByName("hl2.exe"))
            {
                process.Kill();
            }
        }

        public Process Start(RunPreset runPreset, string command)
        {
            isLoading = true;
            KillExistant();

            Game game = launcher.GetCurrentGame();
            string modPath = launcher.GetCurrentMod().installPath;
            string modFolder = new DirectoryInfo(modPath).Name;

            string exePath = launcher.GetCurrentGame().getExePath();
            if (runPreset.exePath != string.Empty)
                exePath = runPreset.exePath;

            if (!File.Exists(exePath))
            {
                XtraMessageBox.Show("Can't find executable file \"" + exePath + "\"");
                isLoading = false;
                return null;
            }

            modProcess = new Process();
            modProcess.StartInfo.FileName = exePath;
            modProcess.StartInfo.Arguments = runPreset.GetArguments(launcher, parent) + " " + command;
            modProcess.Start();

            if (runPreset.runMode == RunMode.WINDOWED)
                AttachProcessTo(modProcess, parent);

            isLoading = false;

            return modProcess;
        }

        public void AttachProcessTo(Process process, Control parent)
        {
            if (modProcess != null)
            {
                modProcess.EnableRaisingEvents = true;
                modProcess.WaitForInputIdle();
                while (modProcess.MainWindowHandle.ToString() == "0")
                {
                    // Just wait until the window is created. Bad, right?
                }
                RemoveBorders(modProcess.MainWindowHandle);
                Program.SetParent(modProcess.MainWindowHandle, parent.Handle);

                Resize();
            }
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        const int WS_BORDER = 0x00800000;
        const int WS_DLGFRAME = 0x00400000;
        const int WS_THICKFRAME = 0x00040000;
        const int WS_CAPTION = WS_BORDER | WS_DLGFRAME;
        const int WS_MINIMIZE = 0x20000000;
        const int WS_MAXIMIZE = 0x01000000;
        const int WS_SYSMENU = 0x00080000;
        const int WS_VISIBLE = 0x10000000;

        public IntPtr RemoveBorders(IntPtr WindowHandle)
        {
            int WindowStyle = GetWindowLong(WindowHandle, -16);

            //SetWindowLong(WindowHandle, -16, (WindowStyle & ~WS_SYSMENU));
            SetWindowLong(WindowHandle, -16, (WindowStyle & ~WS_BORDER & ~WS_DLGFRAME));
            return WindowHandle;
        }
    }
}
