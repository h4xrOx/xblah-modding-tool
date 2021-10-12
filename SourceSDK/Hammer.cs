using SourceSDK.Packages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SourceSDK
{
    public class Hammer
    {
        public static void RunHammer(Launcher launcher, Instance instance, Control parent)
        {
            RunHammer(launcher, instance, parent, null);
        }

        public static void RunHammer(Launcher launcher, Instance instance, Control parent, PackageFile packageFile)
        {
            Mod mod = launcher.GetCurrentMod();
            switch (mod.Game.EngineID)
            {
                case Engine.SOURCE:
                    {
                        RunSourceHammer(packageFile, mod);
                    }
                    break;
                case Engine.SOURCE2:
                    {
                        instance = RunSource2Hammer(launcher, parent, mod);
                    }
                    break;
                case Engine.GOLDSRC:
                    {
                        RunGoldsrcHammer(mod);
                    }
                    break;
            }

        }

        #region Goldsrc

        private static void RunGoldsrcHammer(Mod mod)
        {
            // Create a maps folder if it's not existant
            Directory.CreateDirectory(mod.InstallPath + "\\maps\\");
            Directory.CreateDirectory(mod.InstallPath + "\\mapsrc\\");

            ConfigureGoldsrcHammer(mod);

            string hammerPath = AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\J.A.C.K.\\jack.exe";

            // Start process
            Process process = new Process();
            process.StartInfo.FileName = hammerPath;
            process.StartInfo.Arguments = string.Empty;
            process.Start();
        }

        private static void ConfigureGoldsrcHammer(Mod mod)
        {
            List<string> fgds = mod.GetFGDs();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("[Config0]");
            sb.AppendLine("Title = " + mod.Name);
            sb.AppendLine("Enabled = true");
            sb.AppendLine("TextureFormat = 1");
            sb.AppendLine("MapProfile = vpHalfLife");
            sb.AppendLine("DefaultPointClass = light");
            sb.AppendLine("DefaultSolidClass = func_wall");
            sb.AppendLine("GameExecutable = " + mod.Game.getExePath().Replace("\\", "/"));
            sb.AppendLine("GameCommandLine =");
            sb.AppendLine("GameBasedir = " + mod.Game.InstallPath.Replace("\\", "/") + "/valve");
            sb.AppendLine("GameModdir = " + mod.InstallPath.Replace("\\", "/"));
            sb.AppendLine("MapSourceFileDirectory = " + mod.InstallPath.Replace("\\", "/") + "/mapsrc");
            sb.AppendLine("ModelViewer =");
            sb.AppendLine("TextureBrightness = 50");
            sb.AppendLine("DefaultTextureScale = 1.000");
            sb.AppendLine("PaletteFile =");
            sb.AppendLine("MapTool0 = " + AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "Tools/J.A.C.K/tools/hlcsg.exe");
            sb.AppendLine("MapTool1 = " + AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "Tools/J.A.C.K/tools/hlbsp.exe");
            sb.AppendLine("MapTool2 = " + AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "Tools/J.A.C.K/tools/hlvis.exe");
            sb.AppendLine("MapTool3 = " + AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + "Tools/J.A.C.K/tools/hlrad.exe");

            for (int i = 0; i < fgds.Count; i++)
                sb.AppendLine("GameData" + i + " = " + fgds[i].Replace("\\", "/"));

            int wadCount = 0;
            foreach(string file in Directory.GetFiles(mod.Game.InstallPath + "\\valve\\", "*.wad"))
            {
                sb.AppendLine("Parcel" + wadCount + " = " + file.Replace("\\", "/"));
                wadCount++;
            }

            foreach (string file in Directory.GetFiles(mod.InstallPath, "*.wad"))
            {
                sb.AppendLine("Parcel" + wadCount + " = " + file.Replace("\\", "/"));
                wadCount++;
            }

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "Tools\\J.A.C.K.\\VDKGameCfg.ini", sb.ToString());
        }

        #endregion

        #region Source

        private static void RunSourceHammer(PackageFile packageFile, Mod mod)
        {
            // Create a maps folder if it's not existant
            Directory.CreateDirectory(mod.InstallPath + "\\maps\\");
            Directory.CreateDirectory(mod.InstallPath + "\\mapsrc\\");

            CopySourceHammer(mod.Game);
            ConfigureSourceHammer(mod);

            string hammerPath = mod.Game.InstallPath + "\\bin\\hammerplusplus.exe";
            if (!File.Exists(hammerPath))
                hammerPath = mod.Game.InstallPath + "\\bin\\hammer.exe";

            // Start process
            Process process = new Process();
            process.StartInfo.FileName = hammerPath;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(hammerPath);
            process.StartInfo.Arguments = string.Empty;

            // Mapbase specfic hammer launch.
            if (mod.Game.Name == "Mapbase")
                process.StartInfo.Arguments += "-game " + mod.InstallPath;

            // Open file in hammer.
            if (packageFile != null)
                process.StartInfo.Arguments += " \"" + packageFile.Directory.ParentArchive.ArchivePath + "\\" + packageFile.Path + "\\" + packageFile.Filename + "." + packageFile.Extension + "\"" + " ";

            process.Start();
        }

        public static void RunSourceHammerWithPropper(Mod mod)
        {
            RunSourceHammerWithPropper(mod, null);
        }

        public static void RunSourceHammerWithPropper(Mod mod, PackageFile packageFile)
        {
            CopySourceHammer(mod.Game);
            ConfigureSourceHammerWithPropper(mod);

            string hammerPath = mod.Game.InstallPath + "\\bin\\hammerplusplus.exe";

            Process process = new Process();
            process.StartInfo.FileName = hammerPath;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(hammerPath);
            process.StartInfo.Arguments = string.Empty;

            // Mapbase specfic hammer launch.
            if (mod.Game.Name == "Mapbase")
                process.StartInfo.Arguments += "-game " + mod.InstallPath;

            // Open file in hammer.
            if (packageFile != null)
                process.StartInfo.Arguments += " \"" + packageFile.Directory.ParentArchive.ArchivePath + "\\" + packageFile.Path + "\\" + packageFile.Filename + "." + packageFile.Extension + "\"" + " ";

            process.Start();
        }

        private static void ConfigureSourceHammer(Mod mod)
        {
            Game game = mod.Game;

            string gameinfoPath = mod.InstallPath + "\\gameinfo.txt";
            KeyValue gameinfo = SourceSDK.KeyValue.readChunkfile(gameinfoPath);
            string instancePath = gameinfo.getValue("instancepath");
            string modName = gameinfo.getValue("name");

            if (File.Exists(game.InstallPath + "\\bin\\propper.fgd"))
                File.Delete(game.InstallPath + "\\bin\\propper.fgd");

            if (File.Exists(game.InstallPath + "\\bin\\propper.exe"))
                File.Delete(game.InstallPath + "\\bin\\propper.exe");

            List<string> fgds = mod.GetFGDs();

            // Hammer plus plus
            fgds.Add(game.InstallPath + "\\bin\\hammerplusplus\\hammerplusplus_fgd.fgd");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\"Configs\"");
            sb.AppendLine("{");
            sb.AppendLine(" \"Games\"");
            sb.AppendLine(" {");
            sb.AppendLine("     \"" + mod.Name + "\"");
            sb.AppendLine("     {");
            sb.AppendLine("         \"GameDir\"		\"" + mod.InstallPath + "\"");
            sb.AppendLine("         \"Hammer\"");
            sb.AppendLine("         {");

            for (int i = 0; i < fgds.Count; i++)
                sb.AppendLine("             \"GameData" + i + "\"		\"" + fgds[i] + "\"");

            sb.AppendLine("             \"DefaultTextureScale\"		\"0.250000\"");
            sb.AppendLine("             \"DefaultLightmapScale\"		\"16\"");
            sb.AppendLine("             \"GameExe\"		\"" + game.InstallPath + "\\hl2.exe\"");
            sb.AppendLine("             \"DefaultSolidEntity\"		\"func_detail\"");
            sb.AppendLine("             \"DefaultPointEntity\"		\"info_player_start\"");
            sb.AppendLine("             \"BSP\"		\"" + game.InstallPath + "\\bin\\vbsp.exe\"");
            sb.AppendLine("             \"Vis\"		\"" + game.InstallPath + "\\bin\\vvis.exe\"");
            sb.AppendLine("             \"Light\"		\"" + game.InstallPath + "\\bin\\vrad.exe\"");
            sb.AppendLine("             \"GameExeDir\"		\"" + game.InstallPath + "\"");
            sb.AppendLine("             \"MapDir\"		\"" + instancePath + "\"");
            sb.AppendLine("             \"BSPDir\"		\"" + mod.InstallPath + "\\maps\"");
            sb.AppendLine("             \"CordonTexture\"		\"tools\\toolsskybox\"");
            sb.AppendLine("             \"MaterialExcludeCount\"		\"0\"");
            sb.AppendLine("         }");
            sb.AppendLine("     }");
            sb.AppendLine(" }");
            sb.AppendLine(" \"SDKVersion\"		\"5\"");
            sb.AppendLine("}");

            File.WriteAllText(game.InstallPath + "\\bin\\GameConfig.txt", sb.ToString());
            Directory.CreateDirectory(game.InstallPath + "\\bin\\hammerplusplus\\");
            File.WriteAllText(game.InstallPath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt", sb.ToString());
        }

        private static void ConfigureSourceHammerWithPropper(Mod mod)
        {
            Game game = mod.Game;

            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Propper\\propper.fgd",
                      game.InstallPath + "\\bin\\propper.fgd",
                      true);
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Propper\\propper.exe",
                      game.InstallPath + "\\bin\\propper.exe",
                      true);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\"Configs\"");
            sb.AppendLine("{");
            sb.AppendLine(" \"Games\"");
            sb.AppendLine(" {");
            sb.AppendLine("     \"My game\"");
            sb.AppendLine("     {");
            sb.AppendLine("         \"GameDir\"		\"" + mod.InstallPath + "\"");
            sb.AppendLine("         \"Hammer\"");
            sb.AppendLine("         {");
            sb.AppendLine("             \"GameData0\"		\"" + game.InstallPath + "\\bin\\propper.fgd\"");
            sb.AppendLine("             \"DefaultTextureScale\"		\"0.250000\"");
            sb.AppendLine("             \"DefaultLightmapScale\"		\"16\"");
            sb.AppendLine("             \"GameExe\"		\"" + game.InstallPath + "\\hl2.exe\"");
            sb.AppendLine("             \"DefaultSolidEntity\"		\"propper_model\"");
            sb.AppendLine("             \"DefaultPointEntity\"		\"propper_skins\"");
            sb.AppendLine("             \"BSP\"       \"" + game.InstallPath + "\\bin\\propper.exe\"");
            sb.AppendLine("             \"GameExeDir\"		\"" + game.InstallPath + "\"");
            sb.AppendLine("             \"MapDir\"		\"" + game.InstallPath + "\\sourcesdk_content\\ep2\\mapsrc\"");
            sb.AppendLine("             \"BSPDir\"		\"" + mod.InstallPath + "\\maps\"");
            sb.AppendLine("             \"CordonTexture\"		\"tools\\toolsskybox\"");
            sb.AppendLine("             \"MaterialExcludeCount\"		\"0\"");
            sb.AppendLine("         }");
            sb.AppendLine("     }");
            sb.AppendLine(" }");
            sb.AppendLine(" \"SDKVersion\"		\"5\""); // TODO check other games to see what happen when I still use 5.
            sb.AppendLine("}");

            File.WriteAllText(game.InstallPath + "\\bin\\GameConfig.txt", sb.ToString());
            Directory.CreateDirectory(game.InstallPath + "\\bin\\hammerplusplus\\");
            File.WriteAllText(game.InstallPath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt", sb.ToString());
        }

        private static void CopySourceHammer(Game game)
        {
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;

            string hammerPath = "";

            if (game.Name == "Source SDK Base 2013 Singleplayer" || game.Name == "Mapbase" || game.Name == "Half-Life 2" || game.Name == "Portal")
            {
                hammerPath = startupPath + "\\Tools\\HammerPlusPlus\\sp\\";
                // Mapbase specfic hammer launch.
                if (game.Name == "Mapbase")
                    hammerPath = startupPath + "\\Tools\\HammerPlusPlus\\mapbase\\";


            }
            else if (game.Name == "Source SDK Base 2013 Multiplayer" || game.Name == "Half-Life 2 Deathmatch")
            {
                hammerPath = startupPath + "\\Tools\\HammerPlusPlus\\mp\\";
            }
            else if (game.Name == "Team Fortress 2")
            {
                hammerPath = startupPath + "\\Tools\\HammerPlusPlus\\tf2\\";
            }
            else if (game.Name == "Counter-Strike Source")
            {
                hammerPath = startupPath + "\\Tools\\HammerPlusPlus\\css\\";
            }

            if (hammerPath != "")
                foreach (string file in Directory.GetFiles(hammerPath, "*.*", SearchOption.AllDirectories))
                {
                    try
                    {
                        Uri path1 = new Uri(hammerPath);
                        Uri path2 = new Uri(file);
                        Uri diff = path1.MakeRelativeUri(path2);
                        string relPath = diff.OriginalString;

                        Directory.CreateDirectory(Path.GetDirectoryName(game.InstallPath + "\\bin\\" + relPath));
                        File.Copy(file, game.InstallPath + "\\bin\\" + relPath, true);
                    }
                    catch (IOException e)
                    {

                    }
                }
        }
        #endregion

        #region Source 2

        private static Instance RunSource2Hammer(Launcher launcher, Control parent, Mod mod)
        {
            Instance instance;
            if (mod.Game.EngineID == Engine.SOURCE2)
                mod.Game.ApplyNonVRPatch();    // Until Valve reinserts the -game parameter

            RunPreset runPreset = new RunPreset(RunMode.WINDOWED);
            string command = "-addon -tools -hlvr_workshop -novr -steam -retail -console -vconsole";

            instance = new Instance(launcher, parent);
            instance.Start(runPreset, command);
            return instance;
        }

        #endregion
    }
}
