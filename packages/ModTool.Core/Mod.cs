using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSDK
{
    public class Mod
    {
        public Game game;
        public string name;
        public string installPath;

        public Mod(Game game, string name, string installPath)
        {
            this.game = game;
            this.name = name;
            this.installPath = installPath;
        }

        public void OpenInstallFolder()
        {
            Process.Start(installPath);
        }

        public List<string> GetSearchPaths()
        {
            List<string> mountedPaths = GetMountedPaths();

            List<string> existingPaths = mountedPaths.Where(x => Directory.Exists(x)).ToList();

            return existingPaths;
        }

        public List<string> GetMountedPaths()
        {
            List<string> result = new List<string>();

            string gamePath = game.installPath;
            string modPath = installPath;

            KeyValue gameInfo = null;
            KeyValue searchPaths = null;
            switch (game.engine)
            {
                case Engine.SOURCE:
                    gameInfo = KeyValue.readChunkfile(modPath + "\\gameinfo.txt");
                    searchPaths = gameInfo.findChildByKey("searchpaths");
                    break;
                case Engine.SOURCE2:
                    gameInfo = KeyValue.readChunkfile(modPath + "\\gameinfo.gi");
                    searchPaths = gameInfo.findChildByKey("searchpaths");
                    break;
                case Engine.GOLDSRC:
                    searchPaths = new KeyValue("searchpaths");
                    searchPaths.addChild(new KeyValue("game", new DirectoryInfo(installPath).Name));
                    searchPaths.addChild(new KeyValue("game", "valve"));
                    break;
            }

            foreach (KeyValue searchPath in searchPaths.getChildren())
            {
                string[] keys = searchPath.getKey().Split('+');

                if (!keys.Contains("game"))
                    continue;

                string value = searchPath.getValue();

                switch (game.engine)
                {
                    case Engine.SOURCE:
                        value = value.Replace("/", "\\");
                        if (value.Contains("|all_source_engine_paths|"))
                            value = value.Replace("|all_source_engine_paths|", gamePath + "\\");
                        else if (value.Contains("|gameinfo_path|"))
                            value = value.Replace("|gameinfo_path|", modPath + "\\");
                        else if (!Directory.Exists(value))
                            value = gamePath + "\\" + value;
                        value = value.Replace("\\\\", "\\");
                        if (value.EndsWith("/"))
                            value = value.Substring(0, value.Length - 1);

                        break;
                    case Engine.SOURCE2:
                        value = gamePath + "\\game\\" + value;
                        // We can't mount the vpks because we have no vpk.exe
                        /*foreach(string file in Directory.GetFiles(value, "*.vpk", SearchOption.AllDirectories))
                        {
                            string fileName = Path.GetFileNameWithoutExtension(file);
                            if (int.TryParse(fileName.Substring(fileName.Length - 3), out _))
                                continue;

                                result.Add(file.Replace("_dir.vpk", ".vpk"));
                        }*/
                        break;
                    case Engine.GOLDSRC:
                        value = gamePath + "\\" + value;
                        break;
                }

                if (value.EndsWith("*"))
                {
                    // Ends with wildcard. Add all subdirectories.
                    value = value.Substring(0, value.Length - 1);

                    if (Directory.Exists(value))
                        foreach (string subdir in Directory.GetDirectories(value))
                        {
                            result.Add(subdir);
                        }
                }
                else
                {
                    // Add directory.
                    result.Add(value);
                }


            }

            return result.Distinct().ToList();
        }

        public List<string> GetMountedVPKs()
        {
            return GetMountedPaths().Where(x => x.EndsWith(".vpk")).ToList();
        }

        public List<string> GetFGDs()
        {
            List<string> result = new List<string>();

            if (game.name == "Mapbase")
            {
                // Mapbase specific fgd directory
                result.Add(installPath + "\\..\\mapbase_shared\\shared_misc\\bin\\halflife2.fgd");
            }
            else
            {
                result.AddRange(Directory.GetFiles(game.installPath, "*.fgd", SearchOption.AllDirectories));
            }

            result.AddRange(Directory.GetFiles(installPath, "*.fgd", SearchOption.AllDirectories));

            List<string> alreadyIncluded = new List<string>();

            foreach (string fgd in result)
            {
                string line;
                StreamReader file = new StreamReader(fgd);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Trim().StartsWith("@include"))
                    {
                        line = line.Replace("@include", string.Empty).Replace("\"", string.Empty).Trim();
                        alreadyIncluded.Add(new FileInfo(fgd).Directory.FullName + "\\" + line);
                        alreadyIncluded.Add(game.installPath + "\\bin\\" + line);
                    }
                }
            }

            foreach (string fgd in alreadyIncluded.Distinct().ToList())
            {
                result.Remove(fgd);
            }

            return result;
        }
    }
}
