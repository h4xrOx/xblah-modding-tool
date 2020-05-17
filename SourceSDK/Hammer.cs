using source_modding_tool.SourceSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace source_modding_tool
{
    public class Hammer
    {
        public static void RunPropperHammer(Mod mod)
        {
            CopySlartibartysHammer(mod.game);
            CreatePropperConfig(mod);

            string hammerPath = mod.game.installPath + "\\bin\\hammer.exe";

            Process process = new Process();
            process.StartInfo.FileName = hammerPath;
            process.StartInfo.Arguments = string.Empty;
            process.Start();
        }

        public static void RunHammer(Mod mod)
        {
            switch(mod.game.engine)
            {
                case Engine.SOURCE:
                    {
                        CopySlartibartysHammer(mod.game);
                        CreateGameConfig(mod);

                        string hammerPath = mod.game.installPath + "\\bin\\hammer.exe";

                        Process process = new Process();
                        process.StartInfo.FileName = hammerPath;
                        process.StartInfo.Arguments = string.Empty;
                        process.Start();
                    }
                    break;
                case Engine.SOURCE2:
                    {

                    }
                    break;
                case Engine.GOLDSRC:
                    {
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
        }

        private static void CopySlartibartysHammer(Game game)
        {
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;

            if (game.name == "Source SDK Base 2013 Singleplayer")
            {
                foreach (string file in Directory.GetFiles(startupPath + "\\Tools\\SlartibartysHammer\\sp\\"))
                {
                    try
                    {
                        File.Copy(file, game.installPath + "\\bin\\" + new FileInfo(file).Name);
                    }
                    catch (IOException)
                    {
                    }
                }
            }
        }
    }
}
