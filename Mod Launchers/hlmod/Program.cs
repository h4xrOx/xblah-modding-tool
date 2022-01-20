using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using xblah_modding_lib;

namespace hlmod
{
    static class Program
    {
        static Launcher launcher;
        static Dictionary<string, Game> games;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            launcher = new Launcher();
            launcher.libraries.Load();

            games = launcher.GetGamesList();
            if (!games.ContainsKey("Half-Life"))
            {
                // Half-Life is not installed.
                MessageBox.Show("Half-Life is not installed. Try again after it finishes downloading.");
                return;
            }

            Game game = games["Half-Life"];
            if (Path.GetDirectoryName(game.getExePath()) + "\\" + new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Name + "\\" != AppDomain.CurrentDomain.BaseDirectory)
            {
                // Wrong dir.
                MessageBox.Show("The mod folder must be inside the Half-Life game folder.");
                return;
            }

            RunMod();
        }

        private static void RunMod()
        {
            Game game = games["Half-Life"];

            string modFolderName = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Name;

            string exePath = game.getExePath();
            string arguments = "-game " + modFolderName + " -fullscreen" +
                                " -x 0" +
                                " -y 0" +
                                " -width " + Screen.PrimaryScreen.Bounds.Width +
                                " -height " + Screen.PrimaryScreen.Bounds.Height;

            Process modProcess = new Process();
            modProcess.EnableRaisingEvents = true;
            modProcess.StartInfo.FileName = exePath;
            modProcess.StartInfo.Arguments = arguments;
            modProcess.Start();
        }
    }
}
