using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceModdingTool.SourceSDK
{
    class VMF
    {
        public static List<string> getAssets(string fullPath, string game, string mod, Steam sourceSDK)
        {
            List<string> assets = new List<string>();

            SourceSDK.KeyValue map = SourceSDK.KeyValue.readChunkfile(fullPath);

            // Add maps assets
            String mapName = Path.GetFileNameWithoutExtension(fullPath).ToLower();
            assets.Add("maps/" + mapName + ".bsp");
            //setStatusMessage("Map added: " + "maps/" + mapName + ".bsp", COLOR_ORANGE);

            // Add material assets
            List<SourceSDK.KeyValue> materials = map.findChildrenByKey("material");
            foreach (SourceSDK.KeyValue kv in materials)
            {
                string value = "materials/" + kv.getValue().ToLower() + ".vmt";
                if (!assets.Contains(value))
                {
                    assets.Add(value);
                    //setStatusMessage("Material added: " + value, COLOR_ORANGE);
                    assets.AddRange(VMT.getAssets(value, game, mod, sourceSDK));
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
                    //setStatusMessage("Model added: " + value, COLOR_ORANGE);
                    assets.AddRange(MDL.getAssets(value, game, mod, sourceSDK));
                }
            }

            // Add sound assets
            List<SourceSDK.KeyValue> sounds = map.findChildrenByKey("message");
            foreach (SourceSDK.KeyValue kv in sounds)
            {
                string value = kv.getValue().ToLower();
                if ((value.EndsWith(".wav") || value.EndsWith(".mp3")) && !assets.Contains(value))
                {
                    //setStatusMessage("Sound added: " + value, COLOR_ORANGE);
                    string v = kv.getValue().ToLower();
                    if (v.StartsWith("#"))
                        v = v.Substring(1);
                    assets.Add("sound/" + v);
                }
            }

            // Add particle assets
            List<SourceSDK.KeyValue> effectsKVs = map.findChildrenByKey("effect_name");
            List<string> effects = new List<string>();
            List<string> pcfFiles = PCF.getAllFiles(sourceSDK);
            foreach (SourceSDK.KeyValue kv in effectsKVs)
            {
                effects.Add(kv.getValue());
            }
            effects = effects.Distinct().ToList();
            foreach(string pcf in pcfFiles)
            {
                if (PCF.containsEffect(effects, pcf))
                {
                    string asset = pcf.Substring(pcf.IndexOf("\\particles\\") + 1).Replace("\\", "/");
                    assets.Add(asset);
                    assets.AddRange(PCF.getAssets(asset, game, mod, sourceSDK));
                }
            }

            // Add skybox
            SourceSDK.KeyValue skybox = map.findChildByKey("skyname");
            if (skybox != null)
            {
                string value = skybox.getValue().ToLower();

                //setStatusMessage("Skybox added: " + value, COLOR_ORANGE);
                string[] parts = new string[] { "up", "dn", "lf", "rt", "ft", "bk" };
                foreach(string part in parts)
                {
                    assets.Add("materials/skybox/" + value + part + ".vmt");
                    VMT.getAssets("materials/skybox/" + value + part + ".vmt", game, mod, sourceSDK);
                }
            }

            assets = assets.Distinct().ToList();
            return assets;
        }
    }
}
