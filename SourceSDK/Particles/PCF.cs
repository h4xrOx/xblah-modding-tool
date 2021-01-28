using SourceSDK.Materials;
using SourceSDK.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SourceSDK.Particles
{
    public class PCF
    {
        /// <summary>
        /// Returns a list of the relative paths of the materials used by this asset
        /// </summary>
        /// <param name="fullPath">Full path to the asset</param>
        /// <returns></returns>
        private static List<string> GetMaterials(PackageFile packageFile)
        {
            List<string> materials = new List<string>();

            byte[] byteArray = packageFile.Data;

            List<char> chars = new List<char>();
            foreach (byte b in byteArray)
                if (b == 0 && chars.Count > 0)
                {
                    string word = new string(chars.ToArray());
                    materials.Add(word);
                    chars.Clear();
                }
                else if (b > 0)
                    chars.Add(Convert.ToChar(b));

            materials = materials.Where(x => x.Contains(".vmt"))
                .Select(x => x.Replace("\u0005", "materials\\"))
                .Distinct()
                .ToList();
            return materials;
        }

        /// <summary>
        /// Returns true if this particle file contains an specific effect
        /// </summary>
        /// <param name="effects">The effect name to look up.</param>
        /// <param name="fullPath">Full path to the asset</param>
        /// <returns></returns>
        public static bool ContainsEffect(List<string> effects, PackageFile packageFile)
        {
            if (effects == null || effects.Count == 0)
                return false;

            byte[] byteArray = packageFile.Data;
            List<string> list = new List<string>();

            List<char> chars = new List<char>();
            foreach (byte b in byteArray)
                if (b == 0 && chars.Count > 0)
                {
                    string word = new string(chars.ToArray());
                    list.Add(word);
                    chars.Clear();
                }
                else if (b > 0)
                    chars.Add(Convert.ToChar(b));
            List<string> strings = list;

            foreach (string effect in effects)
                if (strings.Contains(effect))
                    return true;
            return false;
        }

        /// <summary>
        /// Creates a particles_manifest.txt with the particles included in the base game plus the ones found in the particles folder.
        /// </summary>
        /// <param name="launcher">An instance of the Source SDK lib</param>
        public static void CreateManifest(Launcher launcher)
        {
            if (launcher == null)
                return;

            VPKManager vpkManager = new VPKManager(launcher);
            vpkManager.extractFile("particles/particles_manifest.txt");

            string modPath = launcher.GetCurrentMod().installPath;

            KeyValue manifest = KeyValue.readChunkfile(launcher.GetCurrentMod().installPath + "\\particles\\particles_manifest.txt");
            foreach (string file in Directory.GetFiles(launcher.GetCurrentMod().installPath + "\\particles",
                                                      "*.pcf",
                                                      SearchOption.AllDirectories))
            {
                Uri path1 = new Uri(modPath + "\\");
                Uri path2 = new Uri(file);
                Uri diff = path1.MakeRelativeUri(path2);

                manifest.addChild(new KeyValue("file", diff.OriginalString));
            }
            KeyValue.writeChunkFile(launcher.GetCurrentMod().installPath + "\\particles\\particles_manifest.txt", manifest);
        }

        /// <summary>
        /// Returns a list of the full path of all the particles located in the particles folder
        /// </summary>
        /// <param name="launcher">An instance of the Source SDK lib</param>
        /// <returns></returns>
        public static List<PackageFile> GetAllPackageFiles(PackageManager packageManager)
        {
            if (packageManager == null)
                return null;

            List<PackageFile> files = new List<PackageFile>();
            foreach (PackageDirectory directory in packageManager.Directories.Where(d => d.Path.StartsWith("particles")))
            {
                files.AddRange(directory.Entries.Where(a => a.Extension == "pcf").ToList());
            }

            return files;
        }

        /// <summary>
        /// Returns a list of the relative paths of the assets used by this asset, excluding the particle itself
        /// </summary>
        /// <param name="relativePath">Path of the asset relative to the mod folder</param>
        /// <param name="game">The base game name (i.e. Source SDK Base 2013 Singleplayer)</param>
        /// <param name="mod">The mod and folder name, in the following format: Mod Title (mod_folder)</param>
        /// <param name="launcher">An instance of the Source SDK lib</param>
        /// <returns></returns>
        public static List<string> GetAssets(string relativePath, PackageManager packageManager)
        {
            if (string.IsNullOrEmpty(relativePath) || packageManager == null)
                return null;

            List<string> assets = new List<string>();
            PackageFile packageFile = packageManager.GetFile(relativePath);
            if (packageFile != null)
            {
                List<string> materials = GetMaterials(packageFile);
                foreach (string material in materials)
                {
                    assets.Add(material.Replace("\\", "/"));
                    assets.AddRange(VMT.GetAssets(material, packageManager));
                }
            }

            return assets;
        }
    }
}
