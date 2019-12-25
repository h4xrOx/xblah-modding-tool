using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace windows_source1ide
{
    public class Steam
    {
        public List<string> libraries;
        Dictionary<string, string> games = new Dictionary<string, string>();
        Dictionary<string, string> mods = new Dictionary<string, string>();

        string currentGame = "";
        string currentMod = "";

        public Steam()
        {
            LoadLibraries();
        }

        public Dictionary<string, string> GetGamesList()
        {
            LoadLibraries();
            return LoadGames();
        }

        public string GetGamePath(string game)
        {
            if (games.ContainsKey(game))
                return games[game];
            else
            {
                LoadGames();
                if (games.ContainsKey(game))
                    return games[game];
                else
                    return null;
            }
        }

        public string GetGamePath()
        {
            return GetGamePath(currentGame);
        }

        public Dictionary<string, string> GetModsList(string game)
        {
            LoadGames();
            LoadMods(game);
            return mods;
        }

        public string GetModPath()
        {
            return GetModPath(currentGame, currentMod);
        }

        public string GetModPath(string game, string mod)
        {
            if (game != currentGame || !games.ContainsKey(game))
            {
                if (!games.ContainsKey(game))
                    LoadGames();

                if (games.ContainsKey(game))
                    LoadMods(game);
                else
                    return null;
            }

            if (mod != currentMod || !mods.ContainsKey(mod))
            {
                if (!mods.ContainsKey(mod))
                    LoadMods(game);

                if (!mods.ContainsKey(mod))
                    return null;
            }

            string path = mods[mod];

            if (game != currentGame)
                LoadMods(currentGame);

            return path;
        }

        public void setCurrentGame(string game)
        {
            currentGame = game;
            LoadMods(game);
        }

        public void setCurrentMod(string mod)
        {
            currentMod = mod;
        }

        public static string GetInstallPath()
        {
            return Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam", "InstallPath", null).ToString();
        }

        private void LoadLibraries()
        {
            String steamPath = GetInstallPath();

            libraries = new List<string>();
            libraries.Add(steamPath);

            SourceSDK.KeyValue root = SourceSDK.KeyValue.readChunkfile(steamPath + "\\steamapps\\libraryfolders.vdf");
            
            foreach (SourceSDK.KeyValue child in root.getChildrenList())
            {
                string dir = child.getValue().Replace("\\\\", "\\");
                if (Directory.Exists(dir))
                    libraries.Add(dir);
            }
        }

        private Dictionary<string, string> LoadGames()
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

        private Dictionary<string, string> LoadMods(String game)
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
            string library = Steam.GetInstallPath();
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
            string gamePath = GetGamesList()[game];
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

        public Process RunMod(string game, string mod)
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

            Process process = new Process();
            process.StartInfo.FileName = exePath;
            process.StartInfo.Arguments = "-game \"" + modPath + "\"";
            process.Start();
            process.EnableRaisingEvents = true;

            return process;
        }

        public Process RunIngameTools(string game, string mod)
        {
            string gamePath = games[game];
            string modPath = mods[mod];

            Debug.Write(modPath);

            string exePath = "";

            foreach (string file in Directory.GetFiles(gamePath))
            {
                if (new FileInfo(file).Extension == ".exe")
                {
                    exePath = file;
                    break;
                }
            }

            Process process = new Process();
            process.StartInfo.FileName = exePath;
            process.StartInfo.Arguments = "-game \"" + modPath + "\" -tools -nop4";
            process.Start();
            process.EnableRaisingEvents = true;

            return process;
        }

        public void RunHammer(string game, string mod)
        {
            string gamePath = games[game];
            string modPath = mods[mod];

            CreateGameConfig(game, mod);

            string hammerPath = gamePath + "\\bin\\hammer.exe";
            Debug.Write("Hammer: " + hammerPath);

            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = hammerPath;
            ffmpeg.StartInfo.Arguments = "";
            ffmpeg.Start();
        }

        private void CreateGameConfig(string game, string mod)
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

        public void OpenModFolder(string mod)
        {
            string modPath = mods[mod];
            Process.Start(modPath);
        }

        public List<string> getModMountedVPKs()
        {
            string modPath = GetModPath();
            string gamePath = GetGamePath();
            SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(modPath + "\\gameinfo.txt");

            SourceSDK.KeyValue searchPaths = gameInfo.findChild("searchpaths");
            List<string> result = new List<string>();
            foreach (SourceSDK.KeyValue searchPath in searchPaths.getChildrenList())
            {
                string value = searchPath.getValue();
                if (value.EndsWith(".vpk"))
                {
                    value = value.Replace("|all_source_engine_paths|", gamePath + "/");
                    value = value.Replace("|gameinfo_path|", modPath + "/");
                    result.Add(value);
                }
            }
            return result;
        }

        public List<string> getModSearchPaths(string game, string mod)
        {
            List<string> result = new List<string>();

            string gamePath = GetGamePath(game);
            string modPath = GetModPath(game,mod);

            SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(modPath + "\\gameinfo.txt");

            SourceSDK.KeyValue searchPaths = gameInfo.findChild("searchpaths");
            foreach (SourceSDK.KeyValue searchPath in searchPaths.getChildrenList())
            {
                string[] keys = searchPath.getKey().Split('+');

                if (!keys.Contains("game"))
                    continue;

                string value = searchPath.getValue();
                value = value.Replace("/", "\\");
                value = value.Replace("|all_source_engine_paths|", gamePath + "\\");
                value = value.Replace("|gameinfo_path|", modPath + "\\");
                value = value.Replace("\\\\", "\\");
                if (value.EndsWith("/"))
                    value = value.Substring(0, value.Length - 1);

                if (Directory.Exists(value) && !result.Contains(value))
                    result.Add(value);
            }

            return result;
        }

        public List<string> listFilesInVPK(string fullPath)
        {
            string gamePath = GetGamePath();
            string toolPath = gamePath + "\\bin\\vpk.exe";

            List<string> files = new List<string>();

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = toolPath,
                    Arguments = "l \"" + fullPath + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                files.Add(line);
            }

            return files;
        }

        public void extractFileFromVPK(string vpk, string filePath, string startupPath, Steam sourceSDK)
        {
            string modPath = sourceSDK.GetModPath();
            string toolPath = startupPath + "\\Tools\\HLExtract\\HLExtract.exe";

            string args = "-p \"" + vpk + "\" -d \"" + modPath + "\" -e \"" + filePath + "\" -s";
            Process process = new Process();
            process.StartInfo.FileName = toolPath;
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }

        public void extractFileFromVPKs(Dictionary<string, List<string>> vpks, string filePath, string startupPath, Steam sourceSDK)
        {
            foreach (KeyValuePair<string, List<string>> vpk in vpks)
            {
                if (vpk.Value.Contains(filePath))
                {
                    extractFileFromVPK(vpk.Key.Replace(".vpk", "_dir.vpk"), filePath, startupPath, sourceSDK);
                    return;
                }
            }
        }
    }
}
