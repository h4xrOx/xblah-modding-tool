using SourceSDK.Materials;
using SourceSDK.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SourceSDK.Models
{
    public class MDL
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
                    materials.Add(word.ToLower());

                    chars.Clear();
                }
                else if (b > 0)
                    chars.Add(Convert.ToChar(b));

            if (!materials.Contains("body"))
                return new List<string>();

            materials.RemoveRange(0, materials.IndexOf("body") + 1);

            string materialPath = materials.Last();

            if (materialPath.EndsWith("\\") || materialPath.EndsWith("/"))
            {
                // Last string is the path, and all the previous ones are individual file names.
                materials.RemoveAt(materials.Count - 1);
            }
            else
            {
                // All strings are individual file names (apparently...)
                materialPath = "";
            }

            for (int i = 0; i < materials.Count; i++)
                materials[i] = "materials\\" + materialPath + materials[i] + ".vmt";

            return materials;
        }

        /// <summary>
        /// Returns a list of the relative paths of the assets used by this asset, excluding the model itself
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
                PackageDirectory directory = packageFile.Directory;
                string modelName = Path.GetFileNameWithoutExtension(relativePath).ToLower();

                // Search for files with the same name as the model, but with different extensions
                foreach(PackageFile file in directory.Entries)
                {
                    if (file.Filename == modelName)
                    {
                        assets.Add(file.Path + "/" + file.Filename + "." + file.Extension);
                    }
                }

                // Search for materials used by the model
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
