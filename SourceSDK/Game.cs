using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SourceModdingTool.SourceSDK
{
    class Game
    {
        private Control parent = null;
        private Steam sourceSDK = null;
        public Process modProcess = null;

        public Game(Steam sourceSDK, Control parent)
        {
            this.sourceSDK = sourceSDK;
            this.parent = parent;
        }

        internal void Stop()
        {
            if(modProcess != null)
            {
                modProcess.Kill();
                modProcess = null;
            }
        }

        public void Command(string command)
        {
            if(modProcess == null)
                return;

            string gamePath = sourceSDK.GetGamePath();
            string modPath = sourceSDK.GetModPath();

            Debug.Write(modPath);

            string exePath = string.Empty;

            foreach(string file in Directory.GetFiles(gamePath))
            {
                if(new FileInfo(file).Extension == ".exe")
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
            if(modProcess != null)
            {
                //Command("-width " + parent.Width + " -height " + parent.Height);
                //File.WriteAllText(sourceSDK.GetModPath() + "\\cfg\\cmd.cfg", "mat_setvideomode " + parent.Width + " " + parent.Height + " 1");
                Program.MoveWindow(modProcess.MainWindowHandle, 0, 0, parent.Width, parent.Height, true);
            }
        }

        public Process Start()
        {
            string gamePath = sourceSDK.GetGamePath();
            string modPath = sourceSDK.GetModPath();

            string exePath = string.Empty;

            foreach(string file in Directory.GetFiles(gamePath))
            {
                if(new FileInfo(file).Extension == ".exe")
                {
                    exePath = file;
                    break;
                }
            }

            modProcess = new Process();
            modProcess.StartInfo.FileName = exePath;
            modProcess.StartInfo.Arguments = "-game \"" +
                modPath +
                "\" -windowed -noborder -novid 0 -width " +
                parent.Width +
                " -height " +
                parent.Height;
            modProcess.Start();
            modProcess.EnableRaisingEvents = true;
            modProcess.WaitForInputIdle();

            Thread.Sleep(300);
            Program.SetParent(modProcess.MainWindowHandle, parent.Handle);
            Resize();

            return modProcess;
        }

        public Process StartFullScreen()
        {
            string gamePath = sourceSDK.GetGamePath();
            string modPath = sourceSDK.GetModPath();

            Debug.Write(modPath);

            string exePath = string.Empty;

            foreach(string file in Directory.GetFiles(gamePath))
            {
                if(new FileInfo(file).Extension == ".exe")
                {
                    exePath = file;
                    break;
                }
            }

            modProcess = new Process();
            modProcess.StartInfo.FileName = exePath;
            modProcess.StartInfo.Arguments = "-game \"" +
                modPath +
                "\" -fullscreen -width " +
                Screen.PrimaryScreen.Bounds.Width +
                " -height " +
                Screen.PrimaryScreen.Bounds.Height;
            modProcess.Start();
            modProcess.EnableRaisingEvents = true;
            modProcess.WaitForInputIdle();

            return modProcess;
        }

        public Process StartTools()
        {
            string gamePath = sourceSDK.GetGamePath();
            string modPath = sourceSDK.GetModPath();

            string exePath = string.Empty;

            foreach(string file in Directory.GetFiles(gamePath))
            {
                if(new FileInfo(file).Extension == ".exe")
                {
                    exePath = file;
                    break;
                }
            }

            modProcess = new Process();
            modProcess.StartInfo.FileName = exePath;
            modProcess.StartInfo.Arguments = "-game \"" +
                modPath +
                "\" -tools -nop4 -windowed -novid -noborder 0 -width " +
                parent.Width +
                " -height " +
                parent.Height;
            modProcess.Start();
            modProcess.EnableRaisingEvents = true;
            modProcess.WaitForInputIdle();

            Thread.Sleep(300);
            Program.SetParent(modProcess.MainWindowHandle, parent.Handle);
            Resize();

            return modProcess;
        }
    }
}
