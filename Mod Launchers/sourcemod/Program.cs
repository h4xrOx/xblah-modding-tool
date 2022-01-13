using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using xblah_modding_lib;

namespace sourcemod
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
            if (!games.ContainsKey("Source SDK Base 2013 Singleplayer"))
            {
                // Source SDK Base 2013 Singleplayer is not installed.
                MessageBox.Show("Source SDK Base 2013 Singleplayer is not installed. Try again after it finishes downloading.");
                return;
            }

            SetSDK2013SPUpcoming();
        }

        private static void SetSDK2013SPUpcoming()
        {
            launcher.SetSDK2013SPUpcoming();
            if (!launcher.IsSDK2013SpReady())
            {
                MessageBox.Show("Source SDK Base 2013 Singleplayer needs updating. Try again after it finishes downloading.");
                return;
            }

            CheckIfMapbaseIsInstalled();
        }

        private static void CheckIfMapbaseIsInstalled()
        {
            string gameinfoDir = AppDomain.CurrentDomain.BaseDirectory + "\\gameinfo.txt";
            xblah_modding_lib.KeyValue gameInfo = xblah_modding_lib.KeyValue.readChunkfile(gameinfoDir);
            if (gameInfo != null)
            {
                // Check if is a Mapbase mod (Since mapbase doesn't have its own appid)
                KeyValue searchPaths = gameInfo.findChildByKey("searchpaths");

                if (searchPaths != null && searchPaths.getChildren() != null)
                    foreach (KeyValue searchPath in searchPaths.getChildren())
                    {

                        // Simple yet effective way of detecting mapbase dependency
                        string value = searchPath.getValue();

                        if (value != null && value.Contains("mapbase_shared"))
                        {
                            // It's a mapbase mod

                            // Check if Mapbase is installed.
                            string sourceModsDir = AppDomain.CurrentDomain.BaseDirectory + "\\..\\";
                            if (!Directory.Exists(sourceModsDir + "mapbase_shared\\") || !Directory.Exists(sourceModsDir + "mapbase_hl2\\") || !Directory.Exists(sourceModsDir + "\\mapbase_episodic\\"))
                            {
                                MessageBox.Show("You need to have Mapbase installed to run this mod.");
                                return;
                            }
                        }
                    }
            }
            else
            {
                MessageBox.Show("Error loading mod. Gameinfo is broken.");
                return;
            }

            RunMod();
        }

        private static void RunMod()
        {
            Game game = games["Source SDK Base 2013 Singleplayer"];

            string exePath = game.getExePath();
            string arguments = "-game \"" + AppDomain.CurrentDomain.BaseDirectory + "\" -fullscreen" +
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
