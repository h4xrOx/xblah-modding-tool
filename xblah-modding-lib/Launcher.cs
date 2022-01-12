using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                            Game game = new Game(key, library + "\\steamapps\\common\\" + key, Engine.SOURCE);
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
                                        games.Add(key, new Game(key, library + "\\steamapps\\common\\" + key, Engine.SOURCE));

                                    }
                                    break;
                            }
                        }
                        else if (File.Exists(library + "\\steamapps\\common\\" + key + "\\game\\bin\\win64\\engine2.dll") && !games.ContainsKey(key))
                        {
                            // It's a Source 2 game
                            games.Add(key, new Game(key, library + "\\steamapps\\common\\" + key, Engine.SOURCE2));
                        }
                        else if (File.Exists(library + "\\steamapps\\common\\" + key + "\\valve\\dlls\\hl.dll") && !games.ContainsKey(key))
                        {
                            // It's a Goldsrc game
                            games.Add(key, new Game(key, library + "\\steamapps\\common\\" + key, Engine.GOLDSRC));
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

                    games.Add("Mapbase", new Game("Mapbase", games["Source SDK Base 2013 Singleplayer"].InstallPath, Engine.SOURCE));

                    break;
                }
            }

            games = games.OrderBy(m => m.Key).ToDictionary(m => m.Key, m => m.Value);

            return games;
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
