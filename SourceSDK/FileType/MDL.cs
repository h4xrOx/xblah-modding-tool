//-----------------------------------------------------------------------
// <copyright file="windows-source-modding-tool\SourceSDK\FileType\MDL.cs" company="">
//     Author: Jean XBLAH Knapp
//     Copyright (c) 2019-2020. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace source_modding_tool.SourceSDK
{
    class MDL
    {
        /// <summary>
        /// Returns a list of the relative paths of the materials used by this asset
        /// </summary>
        /// <param name="fullPath">Full path to the asset</param>
        /// <returns></returns>
        private static List<string> GetMaterials(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
                return null;

            List<string> materials = new List<string>();

            if(!File.Exists(fullPath))
                return materials;

            byte[] byteArray = File.ReadAllBytes(fullPath);

            List<char> chars = new List<char>();
            foreach(byte b in byteArray)
                if(b == 0 && chars.Count > 0)
                {
                    string word = new String(chars.ToArray());
                    materials.Add(word.ToLower());

                    chars.Clear();
                } else if(b > 0)
                    chars.Add(Convert.ToChar(b));

            if (!materials.Contains("body"))
                return new List<string>();

            materials.RemoveRange(0, materials.IndexOf("body") + 1);

            string materialPath = materials.Last();

            if (materialPath.EndsWith("\\"))
            {
                // Last string is the path, and all the previous ones are individual file names.
                materials.RemoveAt(materials.Count - 1);
            } else
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
        public static List<string> GetAssets(string relativePath, Game game, Mod mod, Launcher launcher)
        {
            if (string.IsNullOrEmpty(relativePath) || game == null || mod == null || launcher == null)
                return null;

            List<string> assets = new List<string>();
            List<string> searchPaths = mod.GetSearchPaths();

            foreach(string searchPath in searchPaths)
            {
                string modelPath = searchPath + "\\" + relativePath;

                // Check if the model exists in the current search path.
                if(!File.Exists(modelPath))
                    continue;

                string directory = Path.GetDirectoryName(modelPath);
                string modelName = Path.GetFileNameWithoutExtension(modelPath).ToLower();

                // Search for files with the same name as the model, but with different extensions
                foreach(string file in Directory.GetFiles(directory))
                {
                    string fileName = Path.GetFileName(file.ToLower());
                    if(fileName.Substring(0, fileName.IndexOf(".")) == modelName)
                    {
                        string filePath = relativePath.Replace(modelName + ".mdl", string.Empty) + fileName;
                        assets.Add(filePath);
                    }
                }

                // Search for materials used by the model
                List<string> materials = GetMaterials(modelPath);
                foreach(string material in materials)
                {
                    assets.Add(material.Replace("\\", "/"));
                    assets.AddRange(VMT.GetAssets(material, game, mod, launcher));
                }

                break;
            }

            return assets;
        }
    }
}
