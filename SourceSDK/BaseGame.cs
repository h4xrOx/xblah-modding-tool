using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace source_modding_tool
{
    public class BaseGame
    {
        public string name;
        public string installPath;
        public int engine;

        Mod currentMod = null;

        public Dictionary<string, Mod> mods;

        public BaseGame(string name, string installPath, int engine)
        {
            mods = new Dictionary<string, Mod>();

            this.name = name;
            this.installPath = installPath;
            this.engine = engine;
        }

        public int GetAppId()
        {
            string gamePath = this.installPath;
            switch(engine)
            {
                case Engine.SOURCE:
                    if (File.Exists(gamePath + "\\steam_appid.txt"))
                    {
                        string steam_appid = File.ReadAllText(gamePath + "\\steam_appid.txt");
                        return int.Parse(steam_appid);
                    }
                    break;
                case Engine.SOURCE2:
                    if (name == "Half-Life Alyx")
                        return 546560;
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

            int gameAppId = GetAppId();
            string gamePath = installPath;

            if (gameAppId == -1 || gamePath == null)
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

                                string name = gameInfo.getChildByKey("game").getValue() + " (" + new DirectoryInfo(path).Name + ")";
                                string modAppId = gameInfo.getChildByKey("filesystem").getChildByKey("steamappid").getValue();

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
                                XtraMessageBox.Show("Could not load mod " + path + ". It's gameinfo.txt is broken.");
                            }
                        }
                        break;
                    case Engine.SOURCE2:
                        {
                            SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(path + "\\gameinfo.gi");

                            if (gameInfo != null)
                            {

                                string name = gameInfo.getChildByKey("game").getValue() + " (" + new DirectoryInfo(path).Name + ")";
                                //string modAppId = gameInfo.getChildByKey("filesystem").getChildByKey("steamappid").getValue();

                                if (path.Contains(gamePath) && (mods.Values.Where(p => p.installPath == path).ToList().Count == 0))
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
                                XtraMessageBox.Show("Could not load mod " + path + ". It's gameinfo.gi is broken.");
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

            switch(engine)
            {
                case Engine.SOURCE:
                    foreach (String path in Directory.GetDirectories(installPath))
                    {
                        String gameBranch = new FileInfo(path).Name;

                        if (File.Exists(installPath + "\\" + gameBranch + "\\gameinfo.txt"))
                            mods.Add(gameBranch);
                    }
                    break;
                case Engine.SOURCE2:
                    if (Directory.Exists(installPath + "\\"))
                        foreach (String path in Directory.GetDirectories(installPath + "\\game\\"))
                        {
                            String gameBranch = new FileInfo(path).Name;

                            if (File.Exists(installPath + "\\game\\" + gameBranch + "\\gameinfo.gi"))
                                mods.Add(gameBranch);
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
                    foreach (String path in Directory.GetDirectories(library + "\\steamapps\\sourcemods\\"))
                    {
                        String game = new FileInfo(path).Name;

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
    }
}
