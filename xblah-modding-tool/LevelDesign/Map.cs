using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace xblah_modding_tool
{
    class Map
    {
        public string name = string.Empty;
        public Dictionary<string, Version> versions = new Dictionary<string, Version>();

        public Map(string name) { this.name = name; }

        public static string GetMapNameWithoutVersion(string versionName)
        {
            if (string.IsNullOrEmpty(versionName))
                return string.Empty;

            string mapName = versionName;
            int i;
            while (mapName.Length > 0 && int.TryParse(mapName.ToCharArray().Last().ToString(), out i))
                mapName = mapName.Substring(0, mapName.Length - 1);

            return mapName;
        }

        public DateTime GetLastUpdate() => versions.OrderByDescending(i => i.Value.lastUpdateDate).ToList().First().Value.lastUpdateDate;

        public static MapFolder GetMapsByFolder(List<Map> maps)
        {
            MapFolder root = new MapFolder();

            foreach (Map map in maps)
            {
                string fullName = map.name.Replace("\\", "/");

                string[] dirs = fullName.Split('/');
                MapFolder subdir = root;

                for (int i = 0; i < dirs.Length - 1; i++)
                {
                    string dir = dirs[i];

                    if (!subdir.subdirs.ContainsKey(dir))
                        subdir.subdirs.Add(dir, new MapFolder());

                    subdir = subdir.subdirs[dir];
                }

                subdir.maps.Add(map);
            }

            return root;
        }

        public static List<Map> LoadMaps(string instancePath)
        {
            /*if (launcher == null)
                return null;

            string instancePath = new GameInfo(launcher).getValue("instancepath");

            if (instancePath != string.Empty && !instancePath.EndsWith("/") && !instancePath.EndsWith("\\"))
                instancePath = instancePath + "/";*/

            Dictionary<string, Map> maps = new Dictionary<string, Map>();

            if (instancePath != string.Empty && Directory.Exists(instancePath))
                foreach (string file in Directory.GetFiles(instancePath,"*", SearchOption.AllDirectories).Where(f => new string[] { ".bsp", ".vpk" }.Contains(new FileInfo(f).Extension.ToLower())).ToArray())
                //foreach (string file in Directory.GetFiles(instancePath, "*.vmf|*.bsp", SearchOption.AllDirectories))
                {
                    Uri path1 = new Uri(instancePath);
                    Uri path2 = new Uri(file);
                    Uri diff = path1.MakeRelativeUri(path2);
                    string relativePath = Uri.UnescapeDataString(diff.OriginalString);

                    string fileName = relativePath.Substring(0, relativePath.Length - 4); // Remove VMF
                    string versionName = fileName;

                    string mapName = GetMapNameWithoutVersion(versionName);

                    if (!maps.ContainsKey(mapName))
                        maps.Add(mapName, new Map(mapName));

                    if (!maps[mapName].versions.ContainsKey(versionName))
                        maps[mapName].versions.Add(versionName, new Version(file));
                    else
                        maps[mapName].versions[versionName].setFilePath(file);
                }

            return maps.Values.ToList();
        }

        public class Version
        {
            public DateTime lastUpdateDate;
            public string vmfPath = string.Empty;

            public Version(string path) { setFilePath(path); }

            public void setFilePath(string path)
            {
                vmfPath = path;
                lastUpdateDate = new FileInfo(path).LastWriteTime;
            }
        }
    }
}
