using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace windows_source1ide
{
    class Steam
    {
        public List<string> libraries;
        Dictionary<string, string> games = new Dictionary<string, string>();
        Dictionary<string, string> mods = new Dictionary<string, string>();

        public Steam()
        {
            loadLibraries();
        }

        public Dictionary<string, string> GetGames()
        {
            loadLibraries();
            return loadGames();
        }

        public Dictionary<string, string> GetMods(string game)
        {
            loadGames();
            loadMods(game);
            return mods;
        }

        public static string GetSteamPath()
        {
            return Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam", "InstallPath", null).ToString();
        }

        private void loadLibraries()
        {
            String steamPath = GetSteamPath();

            libraries = new List<string>();
            libraries.Add(steamPath);

            SourceSDK.KeyValue root = SourceSDK.KeyValue.readChunkfile(steamPath + "\\steamapps\\libraryfolders.vdf");
            
            foreach (SourceSDK.KeyValue child in root.getChildrenList())
            {
                string dir = child.getValue();
                if (Directory.Exists(dir))
                    libraries.Add(dir);
            }
        }

        private Dictionary<string, string> loadGames()
        {
            games = new Dictionary<string, string>();
            if (libraries.Count > 0)
            {
                foreach (string library in libraries)
                {
                    foreach (String path in Directory.GetDirectories(library + "\\steamapps\\common\\"))
                    {
                        String game = new FileInfo(path).Name;

                        if (File.Exists(library + "\\steamapps\\common\\" + game + "\\bin\\hammer.exe"))
                            games.Add(game, library + "\\steamapps\\common\\" + game);
                    }
                }
            }
            return games;
        }

        private Dictionary<string, string> loadMods(String game)
        {
            mods = new Dictionary<string, string>();
            int gameAppId = GetGameAppId(game);

            foreach (string path in GetAllModPaths())
            {
                SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(path + "\\gameinfo.txt");

                string name = gameInfo.getChild("game").getValue() + " (" + new DirectoryInfo(path).Name + ")";
                string modAppId = gameInfo.getChild("filesystem").getChild("steamappid").getValue();

                if (int.Parse(modAppId) == gameAppId)
                {
                    while (mods.Keys.Contains(name))
                    {
                        name = name + "_";
                    }
                    mods.Add(name, path);
                }
            }
            return mods;
        }

        public List<string> GetAllModPaths()
        {
            List<string> mods = new List<string>();
            string library = Steam.GetSteamPath();
            foreach (String path in Directory.GetDirectories(library + "\\steamapps\\sourcemods\\"))
            {
                String game = new FileInfo(path).Name;

                if (File.Exists(library + "\\steamapps\\sourcemods\\" + game + "\\gameinfo.txt"))
                {
                    mods.Add(library + "\\steamapps\\sourcemods\\" + game);
                }
            }
            return mods;
        }

        public List<string> GetAllGameBranches(string game)
        {
            List<string> mods = new List<string>();
            string gamePath = GetGames()[game];
            foreach (String path in Directory.GetDirectories(gamePath))
            {
                String gameBranch = new FileInfo(path).Name;

                if (File.Exists(gamePath + "\\" + gameBranch + "\\gameinfo.txt"))
                {
                    mods.Add(gameBranch);
                }
            }
            return mods;
        }

        public int GetGameAppId(string game)
        {
            if (game == "")
                return -1;

            string gamePath = games[game];
            if (File.Exists(gamePath + "\\steam_appid.txt"))
            {
                string steam_appid = File.ReadAllText(gamePath + "\\steam_appid.txt");
                return int.Parse(steam_appid);
            }
            return -1;
        }

        public static string[] splitByWords(string fullString)
        {
            List<string> words = new List<string>();

            string[] parts = fullString.Split('\"');
            for (int i = 0; i < parts.Length; i++)
            {
                if (i % 2 == 1)
                {
                    // between quotes
                    string subpart = parts[i].Replace("\"", "");
                    words.Add(subpart);
                } else
                {
                    string[] subparts = parts[i].Split(null);
                    // outside quotes
                    foreach (string subpart in subparts)
                    {
                        if (subpart != "" && subpart != " ")
                            words.Add(subpart);
                    }  
                }
            }
            return words.ToArray();
        }

        public Process runGame(string game, string mod)
        {
            string gamePath = games[game];
            string modPath = mods[mod];

            Debug.Write(modPath);

            string exePath = "";

            foreach(string file in Directory.GetFiles(gamePath))
            {
                if (new FileInfo(file).Extension == ".exe")
                {
                    exePath = file;
                    break;
                }
            }

            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = exePath;
            ffmpeg.StartInfo.Arguments = "-game \"" + modPath + "\"";
            ffmpeg.Start();
            ffmpeg.EnableRaisingEvents = true;

            return ffmpeg;
        }

        public void runHammer(string game, string mod)
        {
            string gamePath = games[game];
            string modPath = mods[mod];

            createGameConfig(game, mod);

            string hammerPath = gamePath + "\\bin\\hammer.exe";
            Debug.Write("Hammer: " + hammerPath);

            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = hammerPath;
            ffmpeg.StartInfo.Arguments = "";
            ffmpeg.Start();
        }

        private void createGameConfig(string game, string mod)
        {
            string gamePath = games[game];
            string modPath = mods[mod];

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\"Configs\"");
            sb.AppendLine("{");
            sb.AppendLine(" \"Games\"");
            sb.AppendLine(" {");
            sb.AppendLine("     \"My game\"");
            sb.AppendLine("     {");
            sb.AppendLine("         \"GameDir\"		\"" + modPath + "\"");
            sb.AppendLine("         \"Hammer\"");
            sb.AppendLine("         {");
            sb.AppendLine("             \"GameData0\"		\"" + gamePath + "\\bin\\halflife2.fgd\"");
            sb.AppendLine("             \"DefaultTextureScale\"		\"0.250000\"");
            sb.AppendLine("             \"DefaultLightmapScale\"		\"16\"");
            sb.AppendLine("             \"GameExe\"		\"" + gamePath + "\\hl2.exe\"");
            sb.AppendLine("             \"DefaultSolidEntity\"		\"func_detail\"");
            sb.AppendLine("             \"DefaultPointEntity\"		\"info_player_start\"");
            sb.AppendLine("             \"BSP\"		\"" + gamePath + "\\bin\\vbsp.exe\"");
            sb.AppendLine("             \"Vis\"		\"" + gamePath + "\\bin\\vvis.exe\"");
            sb.AppendLine("             \"Light\"		\"" + gamePath + "\\bin\\vrad.exe\"");
            sb.AppendLine("             \"GameExeDir\"		\"" + gamePath + "\"");
            sb.AppendLine("             \"MapDir\"		\"" + gamePath + "\\sourcesdk_content\\ep2\\mapsrc\"");
            sb.AppendLine("             \"BSPDir\"		\"" + modPath + "\\maps\"");
            sb.AppendLine("             \"CordonTexture\"		\"tools\\toolsskybox\"");
            sb.AppendLine("             \"MaterialExcludeCount\"		\"0\"");
            sb.AppendLine("         }");
            sb.AppendLine("     }");
            sb.AppendLine(" }");
            sb.AppendLine(" \"SDKVersion\"		\"5\"");
            sb.AppendLine("}");

            File.WriteAllText(gamePath + "\\bin\\GameConfig.txt", sb.ToString());
        }

        public void openModFolder(string mod)
        {
            string modPath = mods[mod];
            Process.Start(modPath);
        }
    }
}
