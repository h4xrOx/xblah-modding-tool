using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windows_source1ide.SourceSDK
{
    class Game
    {
        private Steam sourceSDK;
        private Control parent;
        public Process modProcess;

        public Game(Steam sourceSDK, Control parent)
        {
            this.sourceSDK = sourceSDK;
            this.parent = parent;
        }

        public Process Start()
        {
            string gamePath = sourceSDK.GetGamePath();
            string modPath = sourceSDK.GetModPath();

            string exePath = "";

            foreach (string file in Directory.GetFiles(gamePath))
            {
                if (new FileInfo(file).Extension == ".exe")
                {
                    exePath = file;
                    break;
                }
            }

            modProcess = new Process();
            modProcess.StartInfo.FileName = exePath;
            modProcess.StartInfo.Arguments = "-game \"" + modPath + "\" -windowed -noborder 0 -width " + parent.Width + " -height " + parent.Height;
            modProcess.Start();
            modProcess.EnableRaisingEvents = true;
            modProcess.WaitForInputIdle();

            Thread.Sleep(300);
            Program.SetParent(modProcess.MainWindowHandle, parent.Handle);
            Resize();

            return modProcess;
        }

        public void Resize()
        {
            if (modProcess != null)
            {
                Program.MoveWindow(modProcess.MainWindowHandle, 0,0, parent.Width, parent.Height, true);
                File.WriteAllText(sourceSDK.GetModPath() + "\\cfg\\smt_cmd.cfg", "mat_setvideomode " + parent.Width + " " + parent.Height + " 1");
            }
        }

        internal void Stop()
        {
            if (modProcess != null)
            {
                modProcess.Kill();
                modProcess = null;
            }
        }
    }
}
