using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SourceSDK
{
    public class Game
    {
        public string name;
        public string installPath;
        public int engine;

        Mod currentMod = null;

        public Dictionary<string, Mod> mods;

        public Game(string name, string installPath, int engine)
        {
            mods = new Dictionary<string, Mod>();

            this.name = name;
            this.installPath = installPath;
            this.engine = engine;
        }

        public int GetAppId()
        {
            string gamePath = installPath;
            switch (engine)
            {
                case Engine.SOURCE:
                    switch (name)
                    {
                        case "Source SDK Base 2013 Singleplayer":
                            return 243730;
                        case "Black Mesa":
                            return 362890;
                        case "Counter-Strike Global Offensive":
                            return 730;
                        case "Half-Life 2":
                            return 220;
                        case "Half-Life 2: Episode One":
                            return 380;
                        case "Half-Life 2: Episode Two":
                            return 420;
                        case "Counter-Strike Source":
                            return 240;
                        case "Day of Defeat Source":
                            return 300;
                        case "Half-Life 2 Deathmatch":
                            return 320;
                        case "Portal":
                            return 400;
                        case "Source SDK Base 2013 Multiplayer":
                            return 243750;
                        case "Team Fortress 2":
                            return 440;
                        case "Half-Life 1 Source Deathmatch":
                            return 360;
                        case "Mapbase":
                            return 243731;  // Hard coded fake mapbase appid
                        default:
                            if (File.Exists(gamePath + "\\steam_appid.txt"))
                            {
                                string steam_appid = File.ReadAllText(gamePath + "\\steam_appid.txt");
                                return int.Parse(steam_appid);
                            }
                            else
                            {
                                return -1;
                            }
                    }

                    break;
                case Engine.SOURCE2:
                    if (name == "Half-Life Alyx")
                        return 546560;
                    break;
                case Engine.GOLDSRC:
                    string modName = "";
                    if (File.Exists(gamePath + "\\" + modName + "\\steam_appid.txt"))
                    {
                        string steam_appid = File.ReadAllText(gamePath + "\\steam_appid.txt");
                        return int.Parse(steam_appid);
                    }
                    break;
            }
            return -1;
        }

        /// <summary>
        /// Loads all the mods with the same game app id of the specified game
        /// </summary>
        /// <param name="game">The base game name (i.e. Source SDK Base 2013 Singleplayer)</param>
        /// <returns></returns>
        public Dictionary<string, Mod> LoadMods(Launcher launcher)
        {
            mods = new Dictionary<string, Mod>();

            if (launcher == null)
                return mods;

            int gameAppId = GetAppId();
            string gamePath = installPath;

            if (gameAppId == -1 && engine != Engine.GOLDSRC || gamePath == null)
                return mods;

            List<string> paths = new List<string>();
            switch (engine)
            {
                case Engine.SOURCE:
                    paths.AddRange(GetAllModPaths(launcher));

                    foreach (string path in GetAllBaseGameinfoFolders())
                        paths.Add(gamePath + "\\" + path);
                    break;
                case Engine.SOURCE2:
                    foreach (string path in GetAllBaseGameinfoFolders())
                        paths.Add(gamePath + "\\game\\" + path);
                    break;
                case Engine.GOLDSRC:
                    foreach (string path in GetAllBaseGameinfoFolders())
                        paths.Add(gamePath + "\\" + path);
                    break;
            }

            foreach (string path in paths)
            {
                switch (engine)
                {
                    case Engine.SOURCE:
                        {
                            SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(path + "\\gameinfo.txt");

                            if (gameInfo != null)
                            {

                                string name = gameInfo.getValue("game") + " (" + new DirectoryInfo(path).Name + ")";
                                SourceSDK.KeyValue steamAppIdKV = gameInfo.findChildByKey("steamappid");
                                string modAppId = "-1";
                                if (steamAppIdKV != null)
                                    modAppId = steamAppIdKV.getValue();

                                /*if (modAppId == 380 || // HL2:EP1 games
                                    modAppId == 420

                                    )*/

                                if (modAppId == "243730")
                                {
                                    // Check if is a Mapbase mod (Since mapbase doesn't have its own appid)
                                    SourceSDK.KeyValue searchPaths = gameInfo.findChildByKey("searchpaths");

                                    foreach (SourceSDK.KeyValue searchPath in searchPaths.getChildren())
                                    {

                                        // Simple yet effective way of detecting mapbase dependency
                                        string value = searchPath.getValue();

                                        if (value != null && value.Contains("mapbase_shared"))
                                        {
                                            modAppId = "243731";    // Hardcoded fake mapbase gameappid
                                            break;
                                        }
                                    }
                                }

                                if (int.Parse(modAppId) == gameAppId || path.Contains(gamePath) && !(mods.Values.Where(p => p.installPath == path).ToList().Count == 0))
                                {
                                    bool containsMod = false;
                                    string newModPath = new FileInfo(path).Name;
                                    foreach (Mod mod in mods.Values)
                                    {
                                        if (new FileInfo(mod.installPath).Name == newModPath)
                                        {
                                            containsMod = true;
                                            break;
                                        }
                                    }
                                    if (!containsMod)
                                    {
                                        while (mods.Keys.Contains(name))
                                            name = name + "_";
                                        mods.Add(name, new Mod(this, name, path));
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Could not load mod " + path + ". It's gameinfo.txt is broken.");
                            }
                        }
                        break;
                    case Engine.SOURCE2:
                        {
                            SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(path + "\\gameinfo.gi");

                            if (gameInfo != null)
                            {

                                string name = gameInfo.findChildByKey("game").getValue() + " (" + new DirectoryInfo(path).Name + ")";
                                //string modAppId = gameInfo.getChildByKey("filesystem").getChildByKey("steamappid").getValue();

                                if (path.Contains(gamePath) && mods.Values.Where(p => p.installPath == path).ToList().Count == 0)
                                {
                                    bool containsMod = false;
                                    string newModPath = new FileInfo(path).Name;
                                    foreach (Mod mod in mods.Values)
                                    {
                                        if (new FileInfo(mod.installPath).Name == newModPath)
                                        {
                                            containsMod = true;
                                            break;
                                        }
                                    }
                                    if (!containsMod)
                                    {
                                        while (mods.Keys.Contains(name))
                                            name = name + "_";
                                        mods.Add(name, new Mod(this, name, path));
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Could not load mod " + path + ". It's gameinfo.gi is broken.");
                            }
                        }
                        break;
                    case Engine.GOLDSRC:
                        {
                            SourceSDK.KeyValue gameInfo = Config.readChunkfile(path + "\\liblist.gam");

                            if (gameInfo != null)
                            {

                                string name = gameInfo.getValue("game") + " (" + new DirectoryInfo(path).Name + ")";
                                //string modAppId = gameInfo.getChildByKey("filesystem").getChildByKey("steamappid").getValue();

                                if (path.Contains(gamePath))
                                {
                                    bool containsMod = false;
                                    string newModPath = new FileInfo(path).Name;
                                    foreach (Mod mod in mods.Values)
                                    {
                                        if (new FileInfo(mod.installPath).Name == newModPath)
                                        {
                                            containsMod = true;
                                            break;
                                        }
                                    }
                                    if (!containsMod)
                                    {
                                        while (mods.Keys.Contains(name))
                                            name = name + "_";
                                        mods.Add(name, new Mod(this, name, path));
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Could not load mod " + path + ". It's liblist.gam is broken.");
                            }
                        }
                        break;
                }

            }

            return mods;
        }

        /// <summary>
        /// Returns a list of all the folders with game infos of the specific game
        /// </summary>
        /// <param name="game">The base game name (i.e. Source SDK Base 2013 Singleplayer)</param>
        /// <returns></returns>
        public List<string> GetAllBaseGameinfoFolders()
        {
            List<string> mods = new List<string>();

            switch (engine)
            {
                case Engine.SOURCE:
                    foreach (string path in Directory.GetDirectories(installPath))
                    {
                        string gameBranch = new FileInfo(path).Name;

                        if (File.Exists(installPath + "\\" + gameBranch + "\\gameinfo.txt"))
                            mods.Add(gameBranch);
                    }
                    break;
                case Engine.SOURCE2:
                    if (Directory.Exists(installPath + "\\"))
                        foreach (string path in Directory.GetDirectories(installPath + "\\game\\"))
                        {
                            string gameBranch = new FileInfo(path).Name;

                            if (File.Exists(installPath + "\\game\\" + gameBranch + "\\gameinfo.gi"))
                                mods.Add(gameBranch);
                        }
                    break;
                case Engine.GOLDSRC:
                    foreach (string path in Directory.GetDirectories(installPath))
                    {
                        string gameBranch = new FileInfo(path).Name;

                        if (File.Exists(installPath + "\\" + gameBranch + "\\liblist.gam"))
                        {
                            mods.Add(gameBranch);
                        }
                    }
                    break;
            }

            return mods;
        }

        public void DeleteMod()
        {
            string modPath = GetCurrentMod().installPath;

            if (Directory.Exists(modPath))
                Directory.Delete(modPath, true);
        }

        public List<string> GetAllModPaths(Launcher launcher)
        {
            List<string> mods = new List<string>();
            foreach (string library in launcher.libraries.GetList())
            {
                if (Directory.Exists(library + "\\steamapps\\sourcemods\\"))
                {
                    foreach (string path in Directory.GetDirectories(library + "\\steamapps\\sourcemods\\"))
                    {
                        string game = new FileInfo(path).Name;

                        if (File.Exists(library + "\\steamapps\\sourcemods\\" + game + "\\gameinfo.txt"))
                        {
                            mods.Add(library + "\\steamapps\\sourcemods\\" + game);
                        }
                    }
                }
            }
            return mods;
        }

        public Dictionary<string, Mod> GetModsList(Launcher launcher)
        {
            Dictionary<string, Mod> mods = LoadMods(launcher);
            return mods;
        }

        public void SetCurrentMod(Mod mod)
        {
            currentMod = mod;
        }

        public Mod GetCurrentMod()
        {
            return currentMod;
        }
        public string getExePath()
        {
            switch (engine)
            {
                case Engine.SOURCE:
                    foreach (string file in Directory.GetFiles(installPath))
                    {
                        if (new FileInfo(file).Extension == ".exe")
                        {
                            return file;
                        }
                    }
                    break;
                case Engine.SOURCE2:
                    return installPath + "\\game\\bin\\win64\\hlnonvr.exe";
                case Engine.GOLDSRC:
                    return installPath + "\\hl.exe";
            }
            return string.Empty;
        }

        public string[] getGameinfoFields()
        {
            switch (engine)
            {
                case Engine.SOURCE:
                    return new string[] {
                        "game",
                        "title",
                        "title2",
                        "type",
                        "nodifficulty",
                        "hasportals",
                        "nocrosshair",
                        "advcrosshair",
                        "nomodels",
                        "nohimodel",
                        "developer",
                        "developer_url",
                        "manual",
                        "icon",
                        "nodegraph",
                        "gamedata",
                        "instancepath",
                        "supportsdx8",
                        "supportsvr",
                        "supportsxbox360",
                        "steamappid",
                        "filesystem",
                        "searchpaths"
                    };
                case Engine.SOURCE2:
                    return new string[] {
                        "game",
                        "title",
                        "type",
                        "nomodels",
                        "nohimodel",
                        "developer",
                        "developer_url",
                        "manual",
                        "icon",
                        "nodegraph",
                        "gamedata",
                        "instancepath",
                        "filesystem",
                        "searchpaths"
                    };
                case Engine.GOLDSRC:
                    return new string[] {
                        "game",
                        "icon",
                        "developer",
                        "developer_url",
                        "manual",
                        "type",
                        "nomodels",
                        "nohimodel",
                        "secure",
                        "svonly",
                        "gamedll",
                        "gamedll_linux",
                        "gamedll_osx",
                        "cldll",
                        "startmap",
                        "trainmap",
                        "mpentity",
                        "mpfilter",
                        "fallback_dir",
                        "fallback_maps",
                        "detailed_textures"

                    };
            }

            return new string[] { };
        }

        public void ApplyNonVRPatch()
        {
            string fullPath = installPath + "\\game\\bin\\win64\\hlvr.exe";
            Dictionary<int, string> strings = new Dictionary<int, string>();

            byte[] byteArray = File.ReadAllBytes(fullPath);

            List<char> chars = new List<char>();
            for (int i = 0; i < byteArray.Length; i++)
            {
                byte b = byteArray[i];

                if (b == 0 && chars.Count > 0)
                {
                    string word = new string(chars.ToArray());
                    strings.Add(i - word.Length, word);
                    chars.Clear();
                }
                else if (b > 0)
                    chars.Add(Convert.ToChar(b));
            }

            strings = strings.Where(x => x.Value.Contains("engine2"))
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (KeyValuePair<int, string> kv in strings)
            {
                string value = kv.Value.Replace("engine2", "hlnonvr");
                char[] charArray = value.ToCharArray();
                for (int i = 0; i < charArray.Length; i++)
                {
                    byteArray[kv.Key + i] = Convert.ToByte(charArray[i]);
                }
            }

            try
            {
                File.WriteAllBytes(fullPath.Replace("hlvr.exe", "hlnonvr.exe"), byteArray);
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "/Tools/HLnonVR/hlnonvr.dll", installPath + "\\game\\bin\\win64\\hlnonvr.dll");
            }
            catch (Exception) { }
        }
    }
}
