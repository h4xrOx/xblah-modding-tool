using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace xblah_modding_lib
{
    public class Launcher
    {
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

                        if (File.Exists(library + "\\steamapps\\common\\" + key + "\\valve\\dlls\\hl.dll") && !games.ContainsKey(key))
                        {
                            // It's a Goldsrc game
                            games.Add(key, new Game(key, library + "\\steamapps\\common\\" + key, Engine.GOLDSRC));
                        }
                    }
            }

            games = games.OrderBy(m => m.Key).ToDictionary(m => m.Key, m => m.Value);

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
    }
}
