using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SourceSDK
{
    public class Game
    {
        public string Name { get; set; }

        public string InstallPath { get; set; }

        public int EngineID { get; set; }

        Mod CurrentMod { get; set; } = null;

        public Dictionary<string, Mod> Mods { get; set; }

        public Game(string name, string installPath, int engine)
        {
            Mods = new Dictionary<string, Mod>();

            this.Name = name;
            this.InstallPath = installPath;
            this.EngineID = engine;
        }

        public int GetAppId()
        {
            string gamePath = InstallPath;
            switch (EngineID)
            {
                case Engine.SOURCE:
                    switch (Name)
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
                    if (Name == "Half-Life Alyx")
                        return 546560;
                    break;
                case Engine.GOLDSRC:
                    string modName = "";
                    if (File.Exists(gamePath + "\\" + modName + "\\steam_appid.txt"))
                    {
                        string steam_appid_string = File.ReadAllText(gamePath + "\\steam_appid.txt");
                        int steam_appid = -1;
                        int.TryParse(steam_appid_string, out steam_appid);

                        return steam_appid;
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
            Mods = new Dictionary<string, Mod>();

            if (launcher == null)
                return Mods;

            int gameAppId = GetAppId();
            string gamePath = InstallPath;

            if (gameAppId == -1 && EngineID != Engine.GOLDSRC || gamePath == null)
                return Mods;

            List<string> paths = new List<string>();
            switch (EngineID)
            {
                case Engine.SOURCE:
                    paths.AddRange(GetAllModPaths(launcher));

                    foreach (string path in GetAllBaseGameinfoFolders())
                        paths.Add(gamePath + "\\" + path);
                    break;
                case Engine.SOURCE2:
                    paths.AddRange(GetAllModPaths(launcher));

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
                switch (EngineID)
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

                                // Special cases
                                if (modAppId == "220" && new DirectoryInfo(path).Name == "hl2") // HL2
                                {
                                    name = "Half-Life 2 (" + new DirectoryInfo(path).Name + ")";
                                }

                                if (modAppId == "380") // HL2:EP1
                                {
                                    modAppId = "220";
                                    name = "Half-Life 2: Episode One (" + new DirectoryInfo(path).Name + ")";
                                } 

                                if (modAppId == "420") // HL2:EP2
                                {
                                    modAppId = "220";
                                    name = "Half-Life 2: Episode Two (" + new DirectoryInfo(path).Name + ")";
                                }

                                if (modAppId == "240" && new DirectoryInfo(path).Name == "hl1") // HL:S
                                {
                                    modAppId = "220";
                                    name = "Half-Life: Source (" + new DirectoryInfo(path).Name + ")";
                                }

                                if (modAppId == "243730")
                                {
                                    // Check if is a Mapbase mod (Since mapbase doesn't have its own appid)
                                    SourceSDK.KeyValue searchPaths = gameInfo.findChildByKey("searchpaths");

                                    if (searchPaths != null && searchPaths.getChildren() != null)
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

                                if (int.Parse(modAppId) == gameAppId || path.Contains(gamePath) && !(Mods.Values.Where(p => p.InstallPath == path).ToList().Count == 0))
                                {

                                    bool containsMod = false;
                                    string newModPath = new FileInfo(path).Name;
                                    foreach (Mod mod in Mods.Values)
                                    {
                                        if (new FileInfo(mod.InstallPath).Name == newModPath)
                                        {
                                            containsMod = true;
                                            break;
                                        }
                                    }
                                    if (!containsMod)
                                    {
                                        while (Mods.Keys.Contains(name))
                                            name = name + "_";
                                        Mods.Add(name, new Mod(this, name, path));
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

                                string name = gameInfo.findChildByKey("title").getValue() + " (" + new DirectoryInfo(path).Name + ")";
                                //string modAppId = gameInfo.getChildByKey("filesystem").getChildByKey("steamappid").getValue();

                                if (!File.Exists(path + "\\gameinfo_branchspecific.gi"))
                                    continue;

                                KeyValue gameInfoBranch = KeyValue.readChunkfile(path + "\\gameinfo_branchspecific.gi");
                                if (gameInfoBranch == null)
                                    continue;

                                SourceSDK.KeyValue steamAppIdKV = gameInfoBranch.findChildByKey("steamappid");
                                string modAppId = "-1";
                                if (steamAppIdKV != null)
                                    modAppId = steamAppIdKV.getValue();

                                if (int.Parse(modAppId) == gameAppId || path.Contains(gamePath) && !(Mods.Values.Where(p => p.InstallPath == path).ToList().Count == 0))
                                {        
                                    // Create a symbolic link in Half-Life: Alyx, so the mod can run.
                                    if (!path.Contains(gamePath))
                                    {
                                        var psi = new ProcessStartInfo("cmd.exe", " /C mklink /d \"" + gamePath + "\\game\\" + Path.GetFileName(path) + "\" \"" + path + "\"");
                                        psi.CreateNoWindow = true;
                                        psi.UseShellExecute = false;
                                        Process.Start(psi).WaitForExit();
                                    }

                                    bool containsMod = false;
                                    string newModPath = new FileInfo(path).Name;
                                    foreach (Mod mod in Mods.Values)
                                    {
                                        if (new FileInfo(mod.InstallPath).Name == newModPath)
                                        {
                                            containsMod = true;
                                            break;
                                        }
                                    }
                                    if (!containsMod)
                                    {
                                        while (Mods.Keys.Contains(name))
                                            name = name + "_";
                                        Mods.Add(name, new Mod(this, name, path));
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
                                    foreach (Mod mod in Mods.Values)
                                    {
                                        if (new FileInfo(mod.InstallPath).Name == newModPath)
                                        {
                                            containsMod = true;
                                            break;
                                        }
                                    }
                                    if (!containsMod)
                                    {
                                        while (Mods.Keys.Contains(name))
                                            name = name + "_";
                                        Mods.Add(name, new Mod(this, name, path));
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

            return Mods;
        }

        /// <summary>
        /// Returns a list of all the folders with game infos of the specific game
        /// </summary>
        /// <param name="game">The base game name (i.e. Source SDK Base 2013 Singleplayer)</param>
        /// <returns></returns>
        public List<string> GetAllBaseGameinfoFolders()
        {
            List<string> mods = new List<string>();

            switch (EngineID)
            {
                case Engine.SOURCE:
                    foreach (string path in Directory.GetDirectories(InstallPath))
                    {
                        string gameBranch = new FileInfo(path).Name;

                        if (File.Exists(InstallPath + "\\" + gameBranch + "\\gameinfo.txt"))
                            mods.Add(gameBranch);
                    }
                    break;
                case Engine.SOURCE2:
                    if (Directory.Exists(InstallPath + "\\"))
                        foreach (string path in Directory.GetDirectories(InstallPath + "\\game\\"))
                        {
                            string gameBranch = new FileInfo(path).Name;

                            if (File.Exists(InstallPath + "\\game\\" + gameBranch + "\\gameinfo.gi"))
                                mods.Add(gameBranch);
                        }
                    break;
                case Engine.GOLDSRC:
                    foreach (string path in Directory.GetDirectories(InstallPath))
                    {
                        string gameBranch = new FileInfo(path).Name;

                        if (File.Exists(InstallPath + "\\" + gameBranch + "\\liblist.gam"))
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
            string modPath = GetCurrentMod().InstallPath;

            if (Directory.Exists(modPath))
                Directory.Delete(modPath, true);
        }

        public List<string> GetAllModPaths(Launcher launcher)
        {
            List<string> mods = new List<string>();
            foreach (string library in launcher.libraries.GetList())
            {
                switch(launcher.GetCurrentGame().EngineID)
                {
                    case Engine.SOURCE:
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
                        break;
                    case Engine.SOURCE2:
                        {
                            if (Directory.Exists(library + "\\steamapps\\source2mods\\"))
                            {
                                foreach (string path in Directory.GetDirectories(library + "\\steamapps\\source2mods\\"))
                                {
                                    string game = new FileInfo(path).Name;

                                    if (File.Exists(library + "\\steamapps\\source2mods\\" + game + "\\gameinfo.gi"))
                                    {
                                        mods.Add(library + "\\steamapps\\source2mods\\" + game);
                                    }
                                }
                            } else
                            {
                                Directory.CreateDirectory(Launcher.GetInstallPath() + "\\steamapps\\source2mods\\");
                            }
                        }
                        break;
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
            CurrentMod = mod;
        }

        public Mod GetCurrentMod()
        {
            return CurrentMod;
        }
        public string getExePath()
        {
            switch (EngineID)
            {
                case Engine.SOURCE:
                    foreach (string file in Directory.GetFiles(InstallPath))
                    {
                        if (new FileInfo(file).Extension == ".exe")
                        {
                            return file;
                        }
                    }
                    break;
                case Engine.SOURCE2:
                    return InstallPath + "\\game\\bin\\win64\\hlnonvr.exe";
                case Engine.GOLDSRC:
                    return InstallPath + "\\hl.exe";
            }
            return string.Empty;
        }

        public string[] getGameinfoFields()
        {
            switch (EngineID)
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
            string fullPath = InstallPath + "\\game\\bin\\win64\\hlvr.exe";
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
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "/Tools/HLnonVR/hlnonvr.dll", InstallPath + "\\game\\bin\\win64\\hlnonvr.dll");
            }
            catch (Exception) { }
        }
    }
}
