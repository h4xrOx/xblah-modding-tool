using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SourceSDK
{
    public class Launcher
    {
        Game currentGame = null;

        public Libraries libraries;
        Dictionary<string, Game> games;

        public Launcher()
        {
            libraries = new Libraries();
            games = new Dictionary<string, Game>();
        }

        private Dictionary<string, Game> LoadGames()
        {
            games = new Dictionary<string, Game>();
            foreach (string library in libraries.GetList())
            {
                if (Directory.Exists(library + "\\steamapps\\common\\"))
                    foreach (string path in Directory.GetDirectories(library + "\\steamapps\\common\\"))
                    {
                        string game = new FileInfo(path).Name;

                        if (File.Exists(library + "\\steamapps\\common\\" + game + "\\bin\\engine.dll") && !games.ContainsKey(game))
                        {
                            // It's a Source game
                            switch (game)
                            {
                                case "half-life 2":
                                    {
                                        games.Add("Half-Life 2", new Game("Half-Life 2", library + "\\steamapps\\common\\" + game, Engine.SOURCE));
                                        games.Add("Half-Life 2: Episode One", new Game("Half-Life 2: Episode One", library + "\\steamapps\\common\\" + game, Engine.SOURCE));
                                        games.Add("Half-Life 2: Episode Two", new Game("Half-Life 2: Episode Two", library + "\\steamapps\\common\\" + game, Engine.SOURCE));
                                    }
                                    break;
                                default:
                                    {
                                        games.Add(game, new Game(game, library + "\\steamapps\\common\\" + game, Engine.SOURCE));

                                    }
                                    break;
                            }
                        }
                        else if (File.Exists(library + "\\steamapps\\common\\" + game + "\\game\\bin\\win64\\engine2.dll") && !games.ContainsKey(game))
                        {
                            // It's a Source 2 game
                            games.Add(game, new Game(game, library + "\\steamapps\\common\\" + game, Engine.SOURCE2));
                        }
                        else if (File.Exists(library + "\\steamapps\\common\\" + game + "\\valve\\dlls\\hl.dll") && !games.ContainsKey(game))
                        {
                            // It's a Goldsrc game
                            games.Add(game, new Game(game, library + "\\steamapps\\common\\" + game, Engine.GOLDSRC));
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

            return games;
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
