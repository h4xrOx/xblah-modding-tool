using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xblah_modding_lib
{
    public class Launcher
    {
        /// <summary>
        /// Gets the user directory.
        /// </summary>
        public static string UserDirectory => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Application.ProductName + "\\";

        /// <summary>
        /// Gets the application directory.
        /// </summary>
        public static string ApplicationDirectory => AppDomain.CurrentDomain.BaseDirectory;


        Game currentGame = null;

        public Libraries libraries;
        Dictionary<string, Game> games;
        public List<string> ReleasedModsDirectories;

        public Launcher()
        {
            libraries = new Libraries();
            games = new Dictionary<string, Game>();
        }

        private Dictionary<string, Game> LoadGames()
        {
            games = new Dictionary<string, Game>();
            ReleasedModsDirectories = new List<string>();

            foreach (string library in libraries.GetList())
            {
                if (Directory.Exists(library + "\\steamapps\\common\\"))
                    foreach (string path in Directory.GetDirectories(library + "\\steamapps\\common\\"))
                    {
                        string key = new FileInfo(path).Name;

                        // Entropy zero special case
                        if (key == "Entropy Zero" && File.Exists(path + "\\Entropy Zero\\EntropyZero\\gameinfo.txt")) {
                            ReleasedModsDirectories.Add(path + "\\Entropy Zero");
                        }

                        if (File.Exists(library + "\\steamapps\\common\\" + key + "\\bin\\engine.dll") && !games.ContainsKey(key))
                        {
                            // It's a Source game

                            Game game = new Game(key, library + "\\steamapps\\common\\" + key, Engine.SOURCE, this);
                            if (game.GetAppId() == 243730 && key != "Source SDK Base 2013 Singleplayer")
                            {
                                // This is actually a wrapper for a SDK2013 mod, and should be treated as such.
                                ReleasedModsDirectories.Add(library + "\\steamapps\\common\\" + key);
                                continue;
                            }

                            if (games.ContainsKey(key))
                            {
                                MessageBox.Show("Error: There is already a game loaded as \"" + key + "\" in the directory " + games[key].InstallPath + ". It's not possible to read directory " + library + "\\steamapps\\common\\" + key + ". You have to solve this issue manually.");
                                continue;
                            }

                            switch (key)
                            {
                                case "half-life 2":
                                    {
                                        game.Name = "Half-Life 2";
                                        if (games.ContainsKey("Half-Life 2"))
                                        {
                                            MessageBox.Show("Error: There is already a game loaded as \"Half-Life 2\" in the directory " + games["Half-Life 2"].InstallPath + ". It's not possible to read directory " + library + "\\steamapps\\common\\" + key + ". You have to solve this issue manually.");
                                            continue;
                                        }
                                        games.Add("Half-Life 2", game);
                                    }
                                    break;
                                default:
                                    {
                                        games.Add(key, new Game(key, library + "\\steamapps\\common\\" + key, Engine.SOURCE, this));

                                    }
                                    break;
                            }
                        }
                        else if (File.Exists(library + "\\steamapps\\common\\" + key + "\\game\\bin\\win64\\engine2.dll") && !games.ContainsKey(key))
                        {
                            // It's a Source 2 game
                            games.Add(key, new Game(key, library + "\\steamapps\\common\\" + key, Engine.SOURCE2, this));
                        }
                        else if (File.Exists(library + "\\steamapps\\common\\" + key + "\\valve\\dlls\\hl.dll") && !games.ContainsKey(key))
                        {
                            // It's a Goldsrc game
                            games.Add(key, new Game(key, library + "\\steamapps\\common\\" + key, Engine.GOLDSRC, this));
                        }
                    }
            }

            // Mapbase, since they don't have their own gameid and I have to hardcode stuff
            foreach (string library in libraries.GetList())
            {
                if (games.ContainsKey("Source SDK Base 2013 Singleplayer") &&
                    Directory.Exists(library + "\\steamapps\\sourcemods\\mapbase_shared") &&
                    Directory.Exists(library + "\\steamapps\\sourcemods\\mapbase_hl2") &&
                    Directory.Exists(library + "\\steamapps\\sourcemods\\mapbase_episodic"))
                {

                    games.Add("Mapbase", new Game("Mapbase", games["Source SDK Base 2013 Singleplayer"].InstallPath, Engine.SOURCE, this));

                    break;
                }
            }

            foreach (string library in libraries.GetList())
            {
                if (games.ContainsKey("Source SDK Base 2013 Multiplayer") &&
                    Directory.Exists(library + "\\steamapps\\sourcemods\\mapbase_shared") &&
                    Directory.Exists(library + "\\steamapps\\sourcemods\\mapbase_hl2") &&
                    Directory.Exists(library + "\\steamapps\\sourcemods\\mapbase_episodic"))
                {

                    games.Add("Mapbase MP", new Game("Mapbase MP", games["Source SDK Base 2013 Multiplayer"].InstallPath, Engine.SOURCE, this));

                    break;
                }
            }

            games = games.OrderBy(m => m.Key).ToDictionary(m => m.Key, m => m.Value);

            foreach(KeyValuePair<string, Game> game in games)
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Assets/GameIcons/" + game.Key + ".ico"))
                    game.Value.Icon = (Bitmap)Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Assets/GameIcons/" + game.Key + ".ico");
                else if (File.Exists(game.Value.getExePath()))
                {
                    try
                    {
                        game.Value.Icon = Icon.ExtractAssociatedIcon(game.Value.getExePath()).ToBitmap();
                    }
                    catch (FileNotFoundException)
                    {

                    }
                }
            }

            return games;
        }

        /// <summary>
        /// Automatically sets the SDK 2013 to upcoming and restarts Steam so the download can proceed.
        /// </summary>
        public void SetSDK2013SPUpcoming()
        {
            foreach(string library in libraries.GetList())
            {
                if (File.Exists(library + "\\steamapps\\appmanifest_243730.acf"))
                {
                    // Get file contents and check if it contains the 'upcoming' keyword.
                    string fileContents = File.ReadAllText(library + "\\steamapps\\appmanifest_243730.acf");
                    if (!fileContents.Contains("upcoming"))
                    {
                        // Save a backup.
                        if (!File.Exists(library + "\\steamapps\\appmanifest_243730_backup.acf"))
                            File.Copy(library + "\\steamapps\\appmanifest_243730.acf", library + "\\steamapps\\appmanifest_243730_backup.acf");

                        // Replace the key values.
                        KeyValue root = KeyValue.readChunkfile(library + "\\steamapps\\appmanifest_243730.acf");
                        root.setValue("StateFlags", "4");
                        root.setValue("UpdateResult", "6");
                        root.setValue("BytesToDownload", "0");
                        root.setValue("BytesDownloaded", "0");
                        root.setValue("BytesToStage", "0");
                        root.setValue("BytesStaged", "0");
                        root.getChildByKey("UserConfig").setValue("betakey", "upcoming");
                        KeyValue.writeChunkFile(library + "\\steamapps\\appmanifest_243730.acf", root, new UTF8Encoding(false));

                        // Restart Steam so it starts updating to upcoming branch.
                        var process = Process.GetProcessesByName("steam");
                        if (process != null && process.Length > 0)
                        {
                            var path = process[0].MainModule.FileName;
                            process[0].Kill();
                            var p = Process.Start(path);
                        }
                        else
                        {
                            string steamPath = Launcher.GetInstallPath() + "\\steam.exe";
                            Process.Start(steamPath);
                        }
                    }
                }
            }
        }

        public bool IsSDK2013SpReady()
        {
            foreach (string library in libraries.GetList())
            {
                if (File.Exists(library + "\\steamapps\\appmanifest_243730.acf"))
                {
                    KeyValue root = KeyValue.readChunkfile(library + "\\steamapps\\appmanifest_243730.acf");
                    return (root.getValue("UpdateResult") == "0" && root.getChildByKey("UserConfig").getValue("betakey") == "upcoming");
                }
            }

            return false;
        }

        public string GetSourceModsDirectory()
        {
            List<string> mods = new List<string>();
            foreach (string library in libraries.GetList())
            {
                if (Directory.Exists(library + "\\steamapps\\sourcemods\\"))
                {
                    return library + "\\steamapps\\sourcemods\\";
                }

            }
            return "";
        }

        public Dictionary<string, Game> GetGamesList()
        {
            libraries.Load();
            Dictionary<string, Game> games = LoadGames();
            return games;
        }

        public static string GetInstallPath()
        {
            return Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam", "InstallPath", null).ToString();
        }

        public string GetModPath(Game game, Mod mod)
        {
            if (game != currentGame || !games.ContainsKey(game.Name))
            {
                if (!games.ContainsKey(game.Name))
                    LoadGames();

                if (games.ContainsKey(game.Name))
                    game.LoadMods(this);
                else
                    return null;
            }

            if (mod != game.GetCurrentMod() || !game.Mods.ContainsKey(mod.Name))
            {
                if (!game.Mods.ContainsKey(mod.Name))
                    game.LoadMods(this);

                if (!game.Mods.ContainsKey(mod.Name))
                    return null;
            }

            string path = mod.InstallPath;

            if (game != currentGame)
                currentGame.LoadMods(this);

            return path;
        }

        public Dictionary<string, Mod> GetModsList(Game game)
        {
            if (game != null)
                return game.LoadMods(this);

            return new Dictionary<string, Mod>();
        }

        public void SetCurrentGame(Game game)
        {
            currentGame = game;
            if (game == null)
            {
                MessageBox.Show("Current game is null. Can't load mods.");
                return;
            }
            game.LoadMods(this);
        }

        public void SetCurrentGame(string game)
        {
            SetCurrentGame(games[game]);
        }

        public Game GetCurrentGame()
        {
            return currentGame;
        }

        public void SetCurrentMod(Mod mod)
        {
            if (currentGame != null)
                currentGame.SetCurrentMod(mod);
        }

        public void SetCurrentMod(string mod)
        {
            SetCurrentMod(currentGame.Mods[mod]);
        }

        public Mod GetCurrentMod()
        {
            if (currentGame != null)
                return currentGame.GetCurrentMod();
            return null;
        }
    }
}
