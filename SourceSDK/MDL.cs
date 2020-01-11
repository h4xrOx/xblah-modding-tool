using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SourceModdingTool.SourceSDK
{
    class MDL
    {
        private static List<string> getModelMaterials(string fullPath, string game, string mod, Steam sourceSDK)
        {
            List<string> materials = new List<string>();

            if(!File.Exists(fullPath))
                return materials;

            byte[] byteArray = File.ReadAllBytes(fullPath);

            List<char> chars = new List<char>();
            foreach(byte b in byteArray)
            {
                if(b == 0 && chars.Count > 0)
                {
                    string word = new String(chars.ToArray());
                    materials.Add(word);
                    chars.Clear();
                } else if(b > 0)
                    chars.Add(Convert.ToChar(b));
            }

            if(!materials.Contains("Body"))
                return new List<string>();

            materials.RemoveRange(0, materials.IndexOf("Body") + 1);

            string materialPath = materials.Last();
            materials.RemoveAt(materials.Count - 1);

            for(int i = 0; i < materials.Count; i++)
                materials[i] = "materials\\" + materialPath + materials[i] + ".vmt";


            return materials;
        }

        public static List<string> getAssets(string relativePath, string game, string mod, Steam sourceSDK)
        {
            List<string> assets = new List<string>();
            List<string> searchPaths = sourceSDK.getModSearchPaths(game, mod);

            foreach(string searchPath in searchPaths)
            {
                string modelPath = searchPath + "\\" + relativePath;

                if(!File.Exists(modelPath))
                    continue;

                string directory = Path.GetDirectoryName(modelPath);
                string modelName = Path.GetFileNameWithoutExtension(modelPath).ToLower();

                foreach(string file in Directory.GetFiles(directory))
                {
                    string fileName = Path.GetFileName(file.ToLower());
                    if(fileName.Substring(0, fileName.IndexOf(".")) == modelName)
                    {
                        string filePath = relativePath.Replace(modelName + ".mdl", string.Empty) + fileName;
                        //setStatusMessage("Model file added: " + filePath, COLOR_ORANGE);
                        assets.Add(filePath);
                    }
                }

                List<string> materials = getModelMaterials(modelPath, game, mod, sourceSDK);
                foreach(string material in materials)
                {
                    assets.Add(material.Replace("\\", "/"));
                    //setStatusMessage("Material added: " + material.Replace("\\", "/"), COLOR_ORANGE);
                    assets.AddRange(VMT.getAssets(material, game, mod, sourceSDK));
                }

                break;
            }

            return assets;
        }
    }
}
