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
            if (modProcess != null)
            {
                //Command("-width " + parent.Width + " -height " + parent.Height);
                //File.WriteAllText(sourceSDK.GetModPath() + "\\cfg\\cmd.cfg", "mat_setvideomode " + parent.Width + " " + parent.Height + " 1");
                Program.MoveWindow(modProcess.MainWindowHandle, 0, 0, parent.Width, parent.Height, true);
            }
        }

        public void KillExistant()
        {
            foreach (var process in Process.GetProcessesByName("hl2.exe"))
            {
                process.Kill();
            }
        }

        public Process Start()
        {
            return Start("");
        }

        public Process Start(string command)
        {
            isLoading = true;
            KillExistant();

            Game game = launcher.GetCurrentGame();
            string modPath = launcher.GetCurrentMod().installPath;
            string modFolder = new DirectoryInfo(modPath).Name;

            string exePath = launcher.GetCurrentGame().getExePath();

            modProcess = new Process();
            modProcess.StartInfo.FileName = exePath;

            Point location = parent.PointToScreen(Point.Empty);

            switch (game.engine)
            {
                case Engine.SOURCE:
                    modProcess.StartInfo.Arguments = "-game \"" +
                    modPath +
                    "\" -windowed -noborder -novid 0" +
                    " -x " + location.X +
                    " -y " + location.Y +
                    " -width " + parent.Width +
                    " -height " + parent.Height +
                    " -multirun " +
                    command;
                    break;
                case Engine.SOURCE2:
                    modProcess.StartInfo.Arguments = " -game " + modFolder + " -windowed -noborder -vr_enable_fake_vr_test" +
                    " -x " + location.X +
                    " -y " + location.Y +
                    " -width " + parent.Width +
                    " -height " + parent.Height +
                    " " + command;
                    break;
                case Engine.GOLDSRC:
                    modProcess.StartInfo.Arguments = "-game " +
                    modFolder +
                    " -windowed -noborder" +
                    " -x " + location.X +
                    " -y " + location.Y +
                    " -width " + parent.Width +
                    " -height " + parent.Height +
                    command;
                    break;
            }
            modProcess.Start();

            AttachProcessTo(modProcess, parent);
            isLoading = false;

            return modProcess;
        }

        public Process StartFullScreen()
        {
            return StartFullScreen("");
        }

        public Process StartFullScreen(string command)
        {
            isLoading = true;
            Game game = launcher.GetCurrentGame();
            string modPath = launcher.GetCurrentMod().installPath;
            string modFolder = new DirectoryInfo(modPath).Name;

            Debug.Write(modPath);

            string exePath = launcher.GetCurrentGame().getExePath();

            modProcess = new Process();
            modProcess.StartInfo.FileName = exePath;

            switch (game.engine)
            {
                case Engine.SOURCE:
                    modProcess.StartInfo.Arguments = "-game \"" +
                    modPath +
                    "\" -fullscreen -width " +
                    Screen.PrimaryScreen.Bounds.Width +
                    " -height " +
                    Screen.PrimaryScreen.Bounds.Height +
                    " " + command;
                    break;
                case Engine.SOURCE2:
                    modProcess.StartInfo.Arguments = "-game " + modFolder + " -fullscreen -vr_enable_fake_vr_test -width " +
                    Screen.PrimaryScreen.Bounds.Width +
                    " -height " +
                    Screen.PrimaryScreen.Bounds.Height +
                    " " + command;
                    break;
                case Engine.GOLDSRC:
                    modProcess.StartInfo.Arguments = "-game " +
                    modFolder +
                    " -fullscreen -width " +
                    Screen.PrimaryScreen.Bounds.Width +
                    " -height " +
                    Screen.PrimaryScreen.Bounds.Height +
                    " " + command;
                    break;
            }
            
            modProcess.Start();
            modProcess.EnableRaisingEvents = true;
            modProcess.WaitForInputIdle();
            isLoading = false;

            return modProcess;
        }

        public Process StartVR()
        {
            return StartVR("");
        }

        public Process StartVR(string command)
        {
            isLoading = true;
            string modPath = launcher.GetCurrentMod().installPath;
            string modFolder = new DirectoryInfo(modPath).Name;

            Debug.Write(modPath);

            string exePath = launcher.GetCurrentGame().getExePath();

            modProcess = new Process();
            modProcess.StartInfo.FileName = exePath;
            modProcess.StartInfo.Arguments = "-vr -game " + modFolder + " " + command;
            modProcess.Start();
            modProcess.EnableRaisingEvents = true;
            modProcess.WaitForInputIdle();


            isLoading = false;
            return modProcess;
        }

        public Process StartExpert(RunPreset runPreset, string command)
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
                //RemoveBorders(modProcess.MainWindowHandle);
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
