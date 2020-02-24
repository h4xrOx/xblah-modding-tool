//-----------------------------------------------------------------------
// <copyright file="windows-source-modding-tool\SourceSDK\FileType\VMF.cs" company="">
//     Author: Jean XBLAH Knapp
//     Copyright (c) 2019-2020. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SourceModdingTool.SourceSDK
{
    class VMF
    {
        /// <summary>
        /// Returns a list of the relative paths of the assets used by this asset, including the bsp
        /// </summary>
        /// <param name="fullPath">Full path to the asset</param>
        /// <param name="game">The base game name (i.e. Source SDK Base 2013 Singleplayer)</param>
        /// <param name="mod">The mod and folder name, in the following format: Mod Title (mod_folder)</param>
        /// <param name="sourceSDK">An instance of the Source SDK lib</param>
        /// <returns></returns>
        public static List<string> GetAssets(string fullPath, string game, string mod, Steam sourceSDK)
        {
            if (string.IsNullOrEmpty(fullPath) ||
                string.IsNullOrEmpty(game) ||
                string.IsNullOrEmpty(mod) ||
                sourceSDK == null)
                return null;

            List<string> assets = new List<string>();

            SourceSDK.KeyValue map = SourceSDK.KeyValue.readChunkfile(fullPath, false);

            // Add maps assets
            String mapName = Path.GetFileNameWithoutExtension(fullPath).ToLower();
            assets.Add("maps/" + mapName + ".bsp");

            // Add material assets
            List<SourceSDK.KeyValue> materials = map.findChildrenByKey("material");
            foreach (SourceSDK.KeyValue kv in materials)
            {
                string value = "materials/" + kv.getValue().ToLower() + ".vmt";
                if (!assets.Contains(value))
                {
                    assets.Add(value);
                    assets.AddRange(VMT.GetAssets(value, game, mod, sourceSDK));
                }
            }

            // Add model and sprite assets
            List<SourceSDK.KeyValue> models = map.findChildrenByKey("model");
            foreach (SourceSDK.KeyValue kv in models)
            {
                string value = kv.getValue().ToLower();
                if (!assets.Contains(value))
                {
                    assets.Add(kv.getValue().ToLower());
                    assets.AddRange(MDL.GetAssets(value, game, mod, sourceSDK));
                }
            }

            // Add sound assets
            List<SourceSDK.KeyValue> sounds = map.findChildrenByKey("message");
            foreach (SourceSDK.KeyValue kv in sounds)
            {
                string value = kv.getValue().ToLower();
                if ((value.EndsWith(".wav") || value.EndsWith(".mp3")) && !assets.Contains(value))
                {
                    string v = kv.getValue().ToLower();
                    if (v.StartsWith("#"))
                        v = v.Substring(1);
                    assets.Add("sound/" + v);
                }
            }

            // Add particle assets
            List<SourceSDK.KeyValue> effectsKVs = map.findChildrenByKey("effect_name");
            List<string> effects = new List<string>();
            List<string> pcfFiles = PCF.GetAllFiles(sourceSDK);
            foreach (SourceSDK.KeyValue kv in effectsKVs)
                effects.Add(kv.getValue());
            effects = effects.Distinct().ToList();
            foreach (string pcf in pcfFiles)
                if (PCF.ContainsEffect(effects, pcf))
                {
                    string asset = pcf.Substring(pcf.IndexOf("\\particles\\") + 1).Replace("\\", "/");
                    assets.Add(asset);
                    assets.AddRange(PCF.GetAssets(asset, game, mod, sourceSDK));
                }

            // Add skybox
            SourceSDK.KeyValue skybox = map.findChildByKey("skyname");
            if (skybox != null)
            {
                string value = skybox.getValue().ToLower();

                string[] parts = new string[] { "up", "dn", "lf", "rt", "ft", "bk" };
                foreach (string part in parts)
                {
                    assets.Add("materials/skybox/" + value + part + ".vmt");
                    VMT.GetAssets("materials/skybox/" + value + part + ".vmt", game, mod, sourceSDK);
                }
            }

            assets = assets.Distinct().ToList();

            Debugger.Break();
            return assets;
        }
    }
}
