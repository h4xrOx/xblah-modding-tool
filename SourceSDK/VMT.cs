using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceModdingTool.SourceSDK
{
    class VMT
    {
        public static List<string> getAssets(string relativePath, string game, string mod, Steam sourceSDK)
        {
            List<string> assets = new List<string>();
            List<string> searchPaths = sourceSDK.getModSearchPaths(game, mod);

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
                    {
                        //setStatusMessage("Texture added: " + textureValue, COLOR_ORANGE);
                        assets.Add(textureValue);
                    }
                }

                break;
            }

            return assets;
        }
    }
}
