using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceModdingTool.SourceSDK
{
    class PCF
    {
        public static bool containsEffect(List<string> effects, string fullPath)
        {
            List<string> strings = ListStrings(fullPath);

            foreach(string effect in effects)
            {
                if (strings.Contains(effect))
                    return true;
            }
            return false;
        }

        private static List<string> ListStrings(string fullPath)
        {
            byte[] byteArray = File.ReadAllBytes(fullPath);
            List<string> list = new List<string>();

            List<char> chars = new List<char>();
            foreach (byte b in byteArray)
            {
                if (b == 0 && chars.Count > 0)
                {
                    string word = new String(chars.ToArray());
                    list.Add(word);
                    chars.Clear();
                }
                else if (b > 0)
                    chars.Add(Convert.ToChar(b));
            }

            return list;
        }

        public static List<string> getAllFiles(Steam sourceSDK)
        {
            List<string> searchPaths = sourceSDK.getModSearchPaths();

            List<string> files = new List<string>();

            

            foreach(string path in searchPaths)
                if (Directory.Exists(path + "\\particles"))
                    files.AddRange(Directory.GetFiles(path + "\\particles", "*.pcf", SearchOption.AllDirectories));

            return files;
        }


        public static void read(string relativePath, string game, string mod, Steam sourceSDK)
        {

            //List<string> particles = ListStrings(fullPath).Where(x => x.Contains("particle")).ToList();


            Debugger.Break();
        }

        public static List<string> getAssets(string relativePath, string game, string mod, Steam sourceSDK)
        {
            List<string> assets = new List<string>();
            List<string> searchPaths = sourceSDK.getModSearchPaths(game, mod);

            foreach (string searchPath in searchPaths)
            {
                string particlePath = searchPath + "\\" + relativePath;

                if (!File.Exists(particlePath))
                    continue;

                List<string> materials = getMaterials(particlePath, game, mod, sourceSDK);
                foreach (string material in materials)
                {
                    assets.Add(material.Replace("\\", "/"));
                    assets.AddRange(VMT.getAssets(material, game, mod, sourceSDK));
                }

                break;
            }

            return assets;
        }

        private static List<string> getMaterials(string fullPath, string game, string mod, Steam sourceSDK)
        {
            List<string> materials = new List<string>();

            if (!File.Exists(fullPath))
                return materials;

            byte[] byteArray = File.ReadAllBytes(fullPath);

            List<char> chars = new List<char>();
            foreach (byte b in byteArray)
            {
                if (b == 0 && chars.Count > 0)
                {
                    string word = new String(chars.ToArray());
                    materials.Add(word);
                    chars.Clear();
                }
                else if (b > 0)
                    chars.Add(Convert.ToChar(b));
            }

            materials = materials.Where(x => x.Contains(".vmt")).Select(x => x.Replace("\u0005", "materials\\")).Distinct().ToList();
            return materials;
        }
        public static void CreateManifest(Steam sourceSDK)
        {
            VPKManager vpkManager = new VPKManager(sourceSDK);
            vpkManager.extractFile("particles/particles_manifest.txt");

            string modPath = sourceSDK.GetModPath();

            KeyValue manifest = KeyValue.readChunkfile(sourceSDK.GetModPath() + "\\particles\\particles_manifest.txt");
            foreach (string file in Directory.GetFiles(sourceSDK.GetModPath() + "\\particles", "*.pcf", SearchOption.AllDirectories))
            {
                Uri path1 = new Uri(modPath + "\\");
                Uri path2 = new Uri(file);
                Uri diff = path1.MakeRelativeUri(path2);

                manifest.addChild(new KeyValue("file", diff.OriginalString));
            }
            KeyValue.writeChunkFile(sourceSDK.GetModPath() + "\\particles\\particles_manifest.txt", manifest);
        }
    }
}
