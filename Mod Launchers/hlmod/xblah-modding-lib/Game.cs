using System.Collections.Generic;
using System.IO;

namespace xblah_modding_lib
{
    public class Game
    {
        public string Name { get; set; }

        public string InstallPath { get; set; }

        public int EngineID { get; set; }

        public Game(string name, string installPath, int engine)
        {
            this.Name = name;
            this.InstallPath = installPath;
            this.EngineID = engine;
        }

        public int GetAppId()
        {
            string gamePath = InstallPath;
            switch (EngineID)
            {
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

        public string getExePath()
        {
            switch (EngineID)
            {
                case Engine.GOLDSRC:
                    return InstallPath + "\\hl.exe";
            }
            return string.Empty;
        }
    }
}
