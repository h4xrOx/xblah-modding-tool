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
            }
            return -1;
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
            }
            return string.Empty;
        }
    }
}
