//-----------------------------------------------------------------------
// <copyright file="D:\Development\CS\windows-source-modding-tool\SourceSDK\FileType\VMT.cs" company="">
//     Author: Jean XBLAH Knapp
//     Copyright (c) 2019-2020. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace source_modding_tool.SourceSDK
{
    class VMT
    {
        /// <summary>
        /// Returns a list of the relative paths of the assets used by this asset, excluding the asset itself
        /// </summary>
        /// <param name="relativePath">Path of the asset relative to the mod folder</param>
        /// <param name="game">The base game name (i.e. Source SDK Base 2013 Singleplayer)</param>
        /// <param name="mod">The mod and folder name, in the following format: Mod Title (mod_folder)</param>
        /// <param name="launcher">An instance of the Source SDK lib</param>
        /// <returns></returns>
        public static List<string> GetAssets(string relativePath, Game game, Mod mod, Launcher launcher)
        {
            if (string.IsNullOrEmpty(relativePath) ||
                game == null ||
                mod == null ||
                launcher == null)
                return null;

            List<string> assets = new List<string>();
            List<string> searchPaths = mod.GetSearchPaths();

            foreach (string searchPath in searchPaths)
            {
                string materialPath = searchPath + "\\" + relativePath;

                if (!File.Exists(materialPath))
                    continue;

                SourceSDK.KeyValue material = SourceSDK.KeyValue.readChunkfile(materialPath);
                List<SourceSDK.KeyValue> textures = new List<SourceSDK.KeyValue>();

                textures.Add(material.getChildByKey("$basetexture"));
                textures.Add(material.getChildByKey("$detail"));
                textures.Add(material.getChildByKey("$blendmodulatetexture"));
                textures.Add(material.getChildByKey("$envmapmask"));
                textures.Add(material.getChildByKey("$bumpmap"));
                textures.Add(material.getChildByKey("$parallaxmap"));
                textures.Add(material.getChildByKey("$basetexture2"));
                textures.Add(material.getChildByKey("%tooltexture"));

                foreach (SourceSDK.KeyValue textureKv in textures)
                {
                    if (textureKv == null)
                        continue;

                    string textureValue = "materials/" + textureKv.getValue().ToLower() + ".vtf";

                    if (!assets.Contains(textureValue))
                        assets.Add(textureValue);
                }

                break;
            }

            return assets;
        }
    }
}
