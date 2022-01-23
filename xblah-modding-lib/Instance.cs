using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace xblah_modding_lib
{
    public class Instance
    {
        private Control parent = null;
        private Launcher launcher = null;
        public Process modProcess = null;

        public bool isLoading = false;

        [Browsable(true)]
        public event EventHandler OnStop;

        [Browsable(true)]
        public event EventHandler OnStart;

        public Instance(Launcher launcher, Control parent)
        {
            this.launcher = launcher;
            this.parent = parent;
        }

        public void Stop()
        {
            if (modProcess != null)
            {
                try
                {
                    modProcess.Kill();
                }
                catch (Exception) { }

                modProcess = null;
            }
        }

        private string hijackFilePath;
        private DateTime hijackWriteTime;
        private Timer hijackTimer = new Timer()
        {
            Interval = 30, // every 30 ms
            Enabled = false
        };

        public void Command(string command)
        {
            if (modProcess == null)
                return;

            string gamePath = launcher.GetCurrentGame().InstallPath;
            string modPath = launcher.GetCurrentMod().InstallPath;

            switch(launcher.GetCurrentGame().EngineID)
            {
                case Engine.SOURCE2:
                    {
                        // You can only hijack source2 (for now) if there is a map runnning with the hijack prefab.
                        // Add a logic_timer triggering a point_clientcommand with the output Command "exec hijack" and that's it.
                        // Use this with caution (basically like everything else).

                        hijackFilePath = launcher.GetCurrentMod().InstallPath + "\\cfg\\hijack.cfg";
                        File.WriteAllText(hijackFilePath, command);
                        hijackWriteTime = File.GetLastAccessTime(hijackFilePath);

                        hijackTimer.Tick += new EventHandler(hijacjTimer_Tick);
                        hijackTimer.Enabled = true;
                    }
                    break;
                case Engine.SOURCE:
                default:
                    {
                        string exePath = launcher.GetCurrentGame().getExePath();

                        Core.SetParent(modProcess.MainWindowHandle, IntPtr.Zero);
                        Core.ShowWindow((int)modProcess.MainWindowHandle, 0);

                        Process process = new Process();
                        process.StartInfo.FileName = exePath;
                        process.StartInfo.Arguments = "-hijack " + command;
                        process.Start();
                        process.EnableRaisingEvents = true;
                        process.WaitForInputIdle();
                        Core.ShowWindow((int)modProcess.MainWindowHandle, 9);
                        Core.SetParent(modProcess.MainWindowHandle, parent.Handle);
                    }
                    break;
            } 
        }

        void hijacjTimer_Tick(object sender, EventArgs e)
        {
            DateTime hijackAccessTime = File.GetLastAccessTime(hijackFilePath);
            if (hijackAccessTime != hijackWriteTime)
            {
                hijackWriteTime = hijackAccessTime;
                File.Delete(hijackFilePath);
                hijackTimer.Enabled = false;
            }
        }

        public void Resize()
        {
            try
            {
                if (modProcess != null)
                {
                    //Command("-width " + parent.Width + " -height " + parent.Height);
                    //File.WriteAllText(sourceSDK.GetModPath() + "\\cfg\\cmd.cfg", "mat_setvideomode " + parent.Width + " " + parent.Height + " 1");
                    Core.MoveWindow(modProcess.MainWindowHandle, 0, 0, parent.Width, parent.Height, true);
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
            string modPath = launcher.GetCurrentMod().InstallPath;
            string modFolder = new DirectoryInfo(modPath).Name;

            string exePath = launcher.GetCurrentGame().getExePath();
            if (runPreset.exePath != string.Empty)
                exePath = runPreset.exePath;

            if (!File.Exists(exePath))
            {
                MessageBox.Show("Can't find executable file \"" + exePath + "\"");
                isLoading = false;
                return null;
            }

            if (game.GetBranch() == "csgo")
            {
                exePath = "steam://rungameid/730//";
            }

            modProcess = new Process();
            modProcess.EnableRaisingEvents = true;
            modProcess.StartInfo.FileName = exePath;
            modProcess.StartInfo.Arguments = runPreset.GetArguments(launcher, parent) + " " + command;
            modProcess.Exited += (sender, e) =>
            {
                if (OnStop != null)
                    OnStop.Invoke(this, new EventArgs());
            };

            modProcess.Start();

            if (runPreset.runMode == RunMode.WINDOWED)
                AttachProcessTo(modProcess, parent);

            isLoading = false;

            if (OnStart != null)
                OnStart.Invoke(this, new EventArgs());

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
                Core.SetParent(modProcess.MainWindowHandle, parent.Handle);

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
            SetWindowLong(WindowHandle, -16, WindowStyle & ~WS_BORDER & ~WS_DLGFRAME);
            return WindowHandle;
        }
    }
}
