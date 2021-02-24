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
        public static void RunPropperHammer(Mod mod)
        {
            RunPropperHammer(mod, null);
        }
        public static void RunPropperHammer(Mod mod, PackageFile packageFile)
        {
            CopyHammerPlusPlus(mod.game);
            CreatePropperConfig(mod);

            string hammerPath = mod.game.installPath + "\\bin\\hammerplusplus.exe";

            Process process = new Process();
            process.StartInfo.FileName = hammerPath;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(hammerPath);
            process.StartInfo.Arguments = string.Empty;

            // Mapbase specfic hammer launch.
            if (mod.game.name == "Mapbase")
                process.StartInfo.Arguments += "-game " + mod.installPath;

            // Open file in hammer.
            if (packageFile != null)
                process.StartInfo.Arguments += " \"" + packageFile.Directory.ParentArchive.ArchivePath + "\\" + packageFile.Path + "\\" + packageFile.Filename + "." + packageFile.Extension + "\"" + " ";

            process.Start();
        }

        public static void RunHammer(Launcher launcher, Instance instance, Control parent)
        {
            RunHammer(launcher, instance, parent, null);
        }

        public static void RunHammer(Launcher launcher, Instance instance, Control parent, PackageFile packageFile)
        {
            Mod mod = launcher.GetCurrentMod();
            switch (mod.game.engine)
            {
                case Engine.SOURCE:
                    {
                        // Create a maps folder if it's not existant
                        string mapsDir = mod.installPath + "\\maps\\";
                        Directory.CreateDirectory(mapsDir);

                        CopyHammerPlusPlus(mod.game);
                        CreateGameConfig(mod);

                        string hammerPath = mod.game.installPath + "\\bin\\hammerplusplus.exe";

                        Process process = new Process();
                        process.StartInfo.FileName = hammerPath;
                        process.StartInfo.WorkingDirectory = Path.GetDirectoryName(hammerPath);
                        process.StartInfo.Arguments = string.Empty;

                        // Mapbase specfic hammer launch.
                        if (mod.game.name == "Mapbase")
                            process.StartInfo.Arguments += "-game " + mod.installPath;

                        // Open file in hammer.
                        if (packageFile != null)
                            process.StartInfo.Arguments += " \"" + packageFile.Directory.ParentArchive.ArchivePath + "\\" + packageFile.Path + "\\" + packageFile.Filename + "." + packageFile.Extension + "\"" + " ";

                        process.Start();
                    }
                    break;
                case Engine.SOURCE2:
                    {
                        if (mod.game.engine == Engine.SOURCE2)
                            mod.game.ApplyNonVRPatch();    // Until Valve reinserts the -game parameter

                        RunPreset runPreset = new RunPreset(RunMode.WINDOWED);
                        string command = "-addon -tools -hlvr_workshop -novr -steam -retail -console -vconsole";

                        instance = new Instance(launcher, parent);
                        instance.Start(runPreset, command);
                    }
                    break;
                case Engine.GOLDSRC:
                    {
                        // Create a maps folder if it's not existant
                        string mapsDir = mod.installPath + "\\maps\\";
                        Directory.CreateDirectory(mapsDir);

                        string hammerPath = AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\HammerEditor\\hammer.exe";

                        Process process = new Process();
                        process.StartInfo.FileName = hammerPath;
                        process.StartInfo.Arguments = string.Empty;
                        process.Start();
                    }
                    break;
            }

        }

        private static void CreatePropperConfig(Mod mod)
        {
            Game game = mod.game;

            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Propper\\propper.fgd",
                      game.installPath + "\\bin\\propper.fgd",
                      true);
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\Propper\\propper.exe",
                      game.installPath + "\\bin\\propper.exe",
                      true);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\"Configs\"");
            sb.AppendLine("{");
            sb.AppendLine(" \"Games\"");
            sb.AppendLine(" {");
            sb.AppendLine("     \"My game\"");
            sb.AppendLine("     {");
            sb.AppendLine("         \"GameDir\"		\"" + mod.installPath + "\"");
            sb.AppendLine("         \"Hammer\"");
            sb.AppendLine("         {");
            sb.AppendLine("             \"GameData0\"		\"" + game.installPath + "\\bin\\propper.fgd\"");
            sb.AppendLine("             \"DefaultTextureScale\"		\"0.250000\"");
            sb.AppendLine("             \"DefaultLightmapScale\"		\"16\"");
            sb.AppendLine("             \"GameExe\"		\"" + game.installPath + "\\hl2.exe\"");
            sb.AppendLine("             \"DefaultSolidEntity\"		\"propper_model\"");
            sb.AppendLine("             \"DefaultPointEntity\"		\"propper_skins\"");
            sb.AppendLine("             \"BSP\"       \"" + game.installPath + "\\bin\\propper.exe\"");
            sb.AppendLine("             \"GameExeDir\"		\"" + game.installPath + "\"");
            sb.AppendLine("             \"MapDir\"		\"" + game.installPath + "\\sourcesdk_content\\ep2\\mapsrc\"");
            sb.AppendLine("             \"BSPDir\"		\"" + mod.installPath + "\\maps\"");
            sb.AppendLine("             \"CordonTexture\"		\"tools\\toolsskybox\"");
            sb.AppendLine("             \"MaterialExcludeCount\"		\"0\"");
            sb.AppendLine("         }");
            sb.AppendLine("     }");
            sb.AppendLine(" }");
            sb.AppendLine(" \"SDKVersion\"		\"5\"");
            sb.AppendLine("}");

            File.WriteAllText(game.installPath + "\\bin\\GameConfig.txt", sb.ToString());
            Directory.CreateDirectory(game.installPath + "\\bin\\hammerplusplus\\");
            File.WriteAllText(game.installPath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt", sb.ToString());
        }

        private static void CreateGameConfig(Mod mod)
        {
            Game game = mod.game;

            string gameinfoPath = mod.installPath + "\\gameinfo.txt";
            KeyValue gameinfo = SourceSDK.KeyValue.readChunkfile(gameinfoPath);
            string instancePath = gameinfo.getValue("instancepath");
            string modName = gameinfo.getValue("name");

            if (File.Exists(game.installPath + "\\bin\\propper.fgd"))
                File.Delete(game.installPath + "\\bin\\propper.fgd");

            if (File.Exists(game.installPath + "\\bin\\propper.exe"))
                File.Delete(game.installPath + "\\bin\\propper.exe");

            List<string> fgds = mod.GetFGDs();

            // Hammer plus plus
            fgds.Add(game.installPath + "\\bin\\hammerplusplus\\hammerplusplus_fgd.fgd");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\"Configs\"");
            sb.AppendLine("{");
            sb.AppendLine(" \"Games\"");
            sb.AppendLine(" {");
            sb.AppendLine("     \"My game\"");
            sb.AppendLine("     {");
            sb.AppendLine("         \"GameDir\"		\"" + mod.installPath + "\"");
            sb.AppendLine("         \"Hammer\"");
            sb.AppendLine("         {");
            for (int i = 0; i < fgds.Count; i++)
            {
                sb.AppendLine("             \"GameData" + i + "\"		\"" + fgds[i] + "\"");
            }

            sb.AppendLine("             \"DefaultTextureScale\"		\"0.250000\"");
            sb.AppendLine("             \"DefaultLightmapScale\"		\"16\"");
            sb.AppendLine("             \"GameExe\"		\"" + game.installPath + "\\hl2.exe\"");
            sb.AppendLine("             \"DefaultSolidEntity\"		\"func_detail\"");
            sb.AppendLine("             \"DefaultPointEntity\"		\"info_player_start\"");
            sb.AppendLine("             \"BSP\"		\"" + game.installPath + "\\bin\\vbsp.exe\"");
            sb.AppendLine("             \"Vis\"		\"" + game.installPath + "\\bin\\vvis.exe\"");
            sb.AppendLine("             \"Light\"		\"" + game.installPath + "\\bin\\vrad.exe\"");
            sb.AppendLine("             \"GameExeDir\"		\"" + game.installPath + "\"");
            sb.AppendLine("             \"MapDir\"		\"" + instancePath + "\"");
            sb.AppendLine("             \"BSPDir\"		\"" + mod.installPath + "\\maps\"");
            sb.AppendLine("             \"CordonTexture\"		\"tools\\toolsskybox\"");
            sb.AppendLine("             \"MaterialExcludeCount\"		\"0\"");
            sb.AppendLine("         }");
            sb.AppendLine("     }");
            sb.AppendLine(" }");
            sb.AppendLine(" \"SDKVersion\"		\"5\"");
            sb.AppendLine("}");

            File.WriteAllText(game.installPath + "\\bin\\GameConfig.txt", sb.ToString());
            Directory.CreateDirectory(game.installPath + "\\bin\\hammerplusplus\\");
            File.WriteAllText(game.installPath + "\\bin\\hammerplusplus\\hammerplusplus_gameconfig.txt", sb.ToString());
        }

        private static void CopyHammerPlusPlus(Game game)
        {
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;

            string hammerPath = startupPath + "\\Tools\\HammerPlusPlus\\sp\\";
            // Mapbase specfic hammer launch.
            if (game.name == "Mapbase")
                hammerPath = startupPath + "\\Tools\\HammerPlusPlus\\mapbase\\";

            if (game.name == "Source SDK Base 2013 Singleplayer" || game.name == "Mapbase")
            {
                foreach (string file in Directory.GetFiles(hammerPath, "*.*", SearchOption.AllDirectories))
                {
                    try
                    {
                        Uri path1 = new Uri(hammerPath);
                        Uri path2 = new Uri(file);
                        Uri diff = path1.MakeRelativeUri(path2);
                        string relPath = diff.OriginalString;

                        Directory.CreateDirectory(Path.GetDirectoryName(game.installPath + "\\bin\\" + relPath));
                        File.Copy(file, game.installPath + "\\bin\\" + relPath, true);
                    }
                    catch (IOException e)
                    {

                    }
                }
            }
        }
    }
}
