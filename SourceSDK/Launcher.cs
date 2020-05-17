using DevExpress.XtraEditors;
using Microsoft.Win32;
using source_modding_tool.SourceSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace source_modding_tool
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
            foreach(string library in this.libraries.GetList())
            {
                if(Directory.Exists(library + "\\steamapps\\common\\"))
                    foreach(String path in Directory.GetDirectories(library + "\\steamapps\\common\\"))
                    {
                        String game = new FileInfo(path).Name;

                        if (File.Exists(library + "\\steamapps\\common\\" + game + "\\bin\\engine.dll") && !games.ContainsKey(game))
                        {
                            // It's a Source game
                            games.Add(game, new Game(game, library + "\\steamapps\\common\\" + game, Engine.SOURCE));
                        }
                        else if (File.Exists(library + "\\steamapps\\common\\" + game + "\\game\\bin\\win64\\engine2.dll") && !games.ContainsKey(game)) {
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
            if(game != currentGame || !games.ContainsKey(game.name))
            {
                if(!games.ContainsKey(game.name))
                    LoadGames();

                if(games.ContainsKey(game.name))
                    game.LoadMods(this);
                else
                    return null;
            }

            if(mod != game.GetCurrentMod() || !game.mods.ContainsKey(mod.name))
            {
                if(!game.mods.ContainsKey(mod.name))
                    game.LoadMods(this);

                if(!game.mods.ContainsKey(mod.name))
                    return null;
            }

            string path = mod.installPath;

            if(game != currentGame)
                currentGame.LoadMods(this);

            return path;
        }

        public Dictionary<string, Mod> GetModsList(Game game)
        {
            return game.LoadMods(this);
        }        

        public void SetCurrentGame(Game game)
        {
            currentGame = game;
            game.LoadMods(this);
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

        public Mod GetCurrentMod()
        {
            if (currentGame != null)
                return currentGame.GetCurrentMod();
            return null;
        }
    }
}
