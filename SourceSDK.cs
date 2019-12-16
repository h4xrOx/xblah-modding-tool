using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace windows_source1ide
{
    class SourceSDK
    {
        public List<string> libraries;
        Dictionary<string, string> games = new Dictionary<string, string>();
        Dictionary<string, string> mods = new Dictionary<string, string>();

        public SourceSDK()
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

        public class KeyVal<KType, VType>
        {
            public KType Value;
            public Dictionary<string, KeyVal<string, string>> Children;

            public KeyVal()
            {

            }

            public KeyVal(KType value)
            {
                this.Value = value;
            }

            public KeyVal<string, string> GetChild(string key)
            {
                if (Children.ContainsKey(key))
                    return Children[key];

                return new KeyVal<string, string>();
            }

            public void SetChild(string key, KeyVal<string, string> value)
            {
                if (Children.ContainsKey(key))
                    Children[key] = value;
                else
                    Children.Add(key, value);
            }

            public void SetChild(string key, string value)
            {
                KeyVal<string,string> keyVal = new KeyVal<string, string>(value);
                if (Children.ContainsKey(key))
                    Children[key] = keyVal;
                else
                    Children.Add(key, keyVal);
            }
        }

        public static KeyVal<string, string> readChunkfile(String path)
        {
            // Parse Valve chunkfile format
            KeyVal<string, string> root = null;
            Stack<KeyVal<string, string>> children = new Stack<KeyVal<string, string>>();

            if (File.Exists(path))
            {
                using (StreamReader r = new StreamReader(path))
                {

                    while (r.Peek() >= 0)
                    {
                        String line = r.ReadLine();
                        line = line.Trim();
                        line = Regex.Replace(line, @"\s+", " ");
                        if (line.StartsWith("//"))
                        {
                            continue;
                        }

                        string[] words = SourceSDK.splitByWords(line);

                        if (words.Length > 0 && words[0].Contains("{"))
                        {
                            // It opens a group
                            children.Peek().Children = new Dictionary<string, KeyVal<string, string>>();
                        }
                        else if (words.Length > 0 && words[0].Contains("}"))
                        {
                            // It closes a group
                            var child = children.Pop();
                            if (children.Count > 0)
                            {
                                String key = child.Value;
                                children.Peek().Children.Add(key, child);
                            }
                            else
                            {
                                root = child;
                            }
                        }
                        else if (words.Length == 1 || words.Length > 1 && words[1].StartsWith("//"))
                        {
                            // It's a group
                            line = line.Replace("\"", "");

                            children.Push(new KeyVal<string, string>
                            {
                                Value = line,
                                Children = new Dictionary<string, KeyVal<string, string>>()
                            });
                        }
                        else if (words.Length >= 2)
                        {
                            // It's a key value
                            var a = words;
                            var v = new KeyVal<string, string> { Value = a[1], Children = null };
                            string key = a[0].Replace("\"", "");
                            while (children.Peek().Children.Keys.Contains(key))
                            {
                                key = key + " ";
                            }
                            children.Peek().Children.Add(key, v);
                        } else
                        {

                        }


                    }
                }
            }
            if (children.Count == 1 && root == null)
            {
                root = children.Pop();
            }

            return root;
        }

        public static void writeChunkFile(string path, string key, KeyVal<string, string> root)
        {
            List<string> lines = writeChunkFileTraverse(key, root, 0);
            File.WriteAllLines(path, lines);
        }

        private static List<string> writeChunkFileTraverse(string key, KeyVal<string, string> node, int level)
        {
            List<string> lines = new List<string>();

            string tabs = "";
            for(int i = 0; i < level; i++)
            {
                tabs = tabs + "\t";
            }

            if (node.Children != null)
            {
                lines.Add(tabs + "\"" + key + "\"");
                lines.Add(tabs + "{");
                foreach(KeyValuePair<string, KeyVal<string, string>> entry in node.Children)
                {
                    lines.AddRange(writeChunkFileTraverse(entry.Key.Trim(), entry.Value, level + 1));
                }
                lines.Add(tabs + "}");
            }
            else if(node.Value != null)
            {
                lines.Add(tabs + "\"" + key + "\"\t\"" + node.Value + "\"");
            }
            


                return lines;
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

            KeyVal<string, string> root = readChunkfile(steamPath + "\\steamapps\\libraryfolders.vdf");
            
            foreach (KeyValuePair<string, KeyVal<string, string>> child in root.Children)
            {
                string dir = child.Value.Value;
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
                KeyVal<string, string> gameInfo = readChunkfile(path + "\\gameinfo.txt");

                string name = gameInfo.Children["game"].Value;
                string modAppId = gameInfo.Children["FileSystem"].Children["SteamAppId"].Value;

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
            string library = SourceSDK.GetSteamPath();
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

        public int GetGameAppId(string game)
        {
            string gamePath = games[game];
            string steam_appid = File.ReadAllText(gamePath + "\\steam_appid.txt");
            return int.Parse(steam_appid);
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
                    //if (subpart != "" && subpart != " ")
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

        public void runGame(string game, string mod)
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
    }
}
