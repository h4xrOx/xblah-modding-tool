using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SourceSDK
{
    public class Mod
    {
        /// <summary>
        /// The game associated to this mod.
        /// </summary>
        public Game Game { get; private set; }

        /// <summary>
        /// The mod name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The mod full install path.
        /// </summary>
        public string InstallPath { get; private set; }

        public string folderName {
            get {
                return Path.GetFileName(InstallPath);
            }
        }

        public Mod(Game game, string name, string installPath)
        {
            this.Game = game;
            this.Name = name;
            this.InstallPath = installPath;
        }

        /// <summary>
        /// Opens the mod install folder in the Windows file explorer.
        /// </summary>
        public void OpenInstallFolder()
        {
            Process.Start("explorer.exe", InstallPath);
        }

        /// <summary>
        /// Returns the full path of all the extracted mounted paths. This will not include the vpk files.
        /// </summary>
        /// <returns>List of the full path of the mounted paths, without the vpks.</returns>
        public List<string> GetSearchPaths()
        {
            List<string> mountedPaths = GetMountedPaths();

            List<string> existingPaths = mountedPaths.Where(x => Directory.Exists(x)).ToList();

            return existingPaths;
        }

        /// <summary>
        /// Returns the full path of all the mounted paths. This will include the vpk files.
        /// </summary>
        /// <returns>List of the full path of the mounted paths, with the vpks.</returns>
        public List<string> GetMountedPaths()
        {
            List<string> result = new List<string>();

            string gamePath = Game.InstallPath;
            string modPath = InstallPath;

            KeyValue gameInfo = null;
            KeyValue searchPaths = null;
            switch (Game.EngineID)
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
                    searchPaths.addChild(new KeyValue("game", new DirectoryInfo(InstallPath).Name));
                    searchPaths.addChild(new KeyValue("game", "valve"));
                    break;
            }

            foreach (KeyValue searchPath in searchPaths.getChildren())
            {
                string[] keys = searchPath.getKey().Split('+');

                if (!keys.Contains("game"))
                    continue;

                string value = searchPath.getValue();

                switch (Game.EngineID)
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
                    // Ends with wildcard. Add all subdirectories and vpks.
                    value = value.Substring(0, value.Length - 1);

                    if (Directory.Exists(value))
                    {
                        foreach (string subdir in Directory.GetDirectories(value))
                        {
                            result.Add(subdir);
                        }

                        foreach(string file in Directory.GetFiles(value, "*.vpk"))
                        {
                            string filename = Path.GetFileNameWithoutExtension(file);
                            if ((!int.TryParse(filename.Substring(filename.Length - 3), out int r) || filename[filename.Length - 4] != '_') || r == -1)
                            {
                                result.Add(file);
                            }
                        }
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

        /// <summary>
        /// Returns the full path of all the mounted vpks.
        /// </summary>
        /// <returns>List of the full path of the mounted vpks.</returns>
        public List<string> GetMountedVPKs()
        {
            return GetMountedPaths().Where(x => x.EndsWith(".vpk")).ToList();
        }

        /// <summary>
        /// Search for fgd files inside the game install path, and then searches for fgs inside the mod install path, in this order.
        /// </summary>
        /// <returns>List of the full path of the fgds.</returns>
        public List<string> GetFGDs()
        {
            List<string> result = new List<string>();

            if (Game.Name == "Mapbase")
            {
                // Mapbase specific fgd directory
                result.Add(InstallPath + "\\..\\mapbase_shared\\shared_misc\\bin\\halflife2.fgd");
            }
            else if(Game.Name == "Half-Life")
            {
                result.Add(AppDomain.CurrentDomain.BaseDirectory + "Tools\\J.A.C.K\\fgd\\valve.fgd");

                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Tools\\J.A.C.K\\fgd\\" + folderName + ".fgd"))
                    result.Add(AppDomain.CurrentDomain.BaseDirectory + "Tools\\J.A.C.K\\fgd\\" + folderName + ".fgd");
            }
            else
            {
                result.AddRange(Directory.GetFiles(Game.InstallPath, "*.fgd", SearchOption.AllDirectories));
            }

            result.AddRange(Directory.GetFiles(InstallPath, "*.fgd", SearchOption.AllDirectories));

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
                        alreadyIncluded.Add(Game.InstallPath + "\\bin\\" + line);
                    }
                }
            }

            foreach (string fgd in alreadyIncluded.Distinct().ToList())
            {
                result.Remove(fgd);
            }

            return result;
        }

        /// <summary>
        /// Returns the path relative to the mod root.
        /// </summary>
        /// <param name="fullPath">The full path of the file. (ex: D:\Steam\steamapps\sourcemods\mod_template\scripts\file_name.txt)</param>
        /// <returns>The relative path of the file. (ex: scripts/file_name.txt)</returns>
        public string GetRelativePath(string fullPath)
        {
            Uri path1 = new Uri(InstallPath + "\\");
            Uri path2 = new Uri(fullPath);
            Uri diff = path1.MakeRelativeUri(path2);
            return diff.OriginalString.Replace("\\", "/");
        }

        /// <summary>
        /// Returns the full path relative to the drive root.
        /// </summary>
        /// <param name="fullPath">The relative path to the mod. (ex: scripts/file_name.txt)</param>
        /// <returns>The relative path of the file. (ex: D:\Steam\steamapps\sourcemods\mod_template\scripts\file_name.txt)</returns>
        public string GetFullPath(string relativePath)
        {
            return InstallPath + "\\" + relativePath.Replace("/", "\\");
        }
    }
}
