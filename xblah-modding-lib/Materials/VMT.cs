using xblah_modding_lib.Packages;
using System.Collections.Generic;
using System.IO;

namespace xblah_modding_lib.Materials
{
    public class VMT
    {
        public static KeyValue FromData(byte[] data)
        {
            return KeyValue.ReadChunk(System.Text.Encoding.UTF8.GetString(data));
        }

        /// <summary>
        /// Returns a list of the relative paths of the assets used by this asset, excluding the asset itself
        /// </summary>
        /// <param name="relativePath">Path of the asset relative to the mod folder</param>
        /// <param name="game">The base game name (i.e. Source SDK Base 2013 Singleplayer)</param>
        /// <param name="mod">The mod and folder name, in the following format: Mod Title (mod_folder)</param>
        /// <param name="launcher">An instance of the Source SDK lib</param>
        /// <returns></returns>
        public static List<string> GetAssets(string relativePath, PackageManager packageManager)
        {
            if (string.IsNullOrEmpty(relativePath) ||
                packageManager == null)
                return null;

            List<string> assets = new List<string>();

            PackageFile packageFile = packageManager.GetFile(relativePath);

            if (packageFile != null)
            { 
                KeyValue material = VMT.FromData(packageFile.Data);
                List<KeyValue> textures = new List<KeyValue>();

                KeyValue baseTexture = material.findChildByKey("$basetexture");

                textures.Add(material.findChildByKey("$basetexture"));
                textures.Add(material.findChildByKey("$detail"));
                textures.Add(material.findChildByKey("$blendmodulatetexture"));
                textures.Add(material.findChildByKey("$envmapmask"));
                textures.Add(material.findChildByKey("$bumpmap"));
                textures.Add(material.findChildByKey("$parallaxmap"));
                textures.Add(material.findChildByKey("$basetexture2"));
                textures.Add(material.findChildByKey("%tooltexture"));
                textures.Add(material.findChildByKey("$selfillummask"));

                // for PBR
                textures.Add(material.findChildByKey("$mraotexture"));
                textures.Add(material.findChildByKey("$emissiontexture"));
                textures.Add(material.findChildByKey("$normalmap"));
                textures.Add(material.findChildByKey("$normaltexture"));
                textures.Add(material.findChildByKey("$mrotexture"));
                textures.Add(material.findChildByKey("$speculartexture"));

                foreach (KeyValue textureKv in textures)
                {
                    if (textureKv == null)
                        continue;

                    string textureRelativePath = textureKv.getValue().ToLower();
                    if (textureRelativePath.EndsWith(".vtf"))
                        textureRelativePath = textureRelativePath.Substring(0, textureRelativePath.Length - 4);

                    string textureValue = "materials/" + textureRelativePath + ".vtf";

                    if (!assets.Contains(textureValue))
                        assets.Add(textureValue);
                }
            }

            return assets;
        }
    }
}
