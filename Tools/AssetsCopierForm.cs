using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using static windows_source1ide.Steam;
using System.Diagnostics;
using System.IO;

namespace windows_source1ide.Tools
{
    public partial class AssetsCopierForm : DevExpress.XtraEditors.XtraForm
    {
        string game;
        string mod;
        Steam sourceSDK;

        string destination = "";

        List<string> vmfs = new List<string>();
        List<string> assets = new List<string>();
        public AssetsCopierForm(string game, string mod)
        {
            this.game = game;
            this.mod = mod;

            InitializeComponent();
        }

        public AssetsCopierForm(string game, string mod, string destination) : this(game, mod)
        {
            this.destination = destination;
        }

        private void AssetsCopierForm_Load(object sender, EventArgs e)
        {
            sourceSDK = new Steam();
            updateVMFList();
        }

        class Asset
        {
            public string path = "";
            public string type = "";
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (selectVMFDialog.ShowDialog() == DialogResult.OK)
            {
                if (!vmfs.Contains(selectVMFDialog.FileName))
                    vmfs.Add(selectVMFDialog.FileName);

                updateVMFList();
            }
        }

        private void updateVMFList()
        {
            vmfList.BeginUnboundLoad();
            vmfList.Nodes.Clear();
            foreach(string vmf in vmfs)
            {
                vmfList.AppendNode(new object[] { vmf }, null);
            }
            vmfList.EndUnboundLoad();
            if (vmfs.Count > 0)
            {
                setStatusMessage("Ready to copy.", COLOR_BLUE);
            } else
            {
                setStatusMessage("Choose at least one VMF to start.", COLOR_RED);
            }
        }

        private void readMapButton_Click(object sender, EventArgs e)
        {
            foreach(string vmf in vmfs)
                assets.AddRange(getAssetsFromMap(vmf));

            assets = assets.Distinct().ToList();
            string customPath = copyAssets();

            string modPath = sourceSDK.GetMods(game)[mod];
            setStatusMessage("Done.", COLOR_GREEN);
            Process.Start(customPath);
        }

        private List<string> getAssetsFromMap(string fullPath)
        {
            setStatusMessage("Reading VMF " + fullPath, COLOR_ORANGE);

            List<string> assets = new List<string>();

            SourceSDK.KeyValue map = SourceSDK.KeyValue.readChunkfile(fullPath);

            // Add maps assets
            String mapName = Path.GetFileNameWithoutExtension(fullPath).ToLower();
            assets.Add("maps/" + mapName + ".bsp");
            setStatusMessage("Map added: " + "maps/" + mapName + ".bsp", COLOR_ORANGE);

            // Add material assets
            List<SourceSDK.KeyValue> materials = map.findChildren("material");
            foreach (SourceSDK.KeyValue kv in materials)
            {
                string value = "materials/" + kv.getValue().ToLower() + ".vmt";
                if (!assets.Contains(value))
                {
                    assets.Add(value);
                    setStatusMessage("Material added: " + value, COLOR_ORANGE);
                    assets.AddRange(getAssetsFromMaterial(value));
                }
            }

            // Add model and sprite assets
            List<SourceSDK.KeyValue> models = map.findChildren("model");
            foreach (SourceSDK.KeyValue kv in models)
            {
                string value = kv.getValue().ToLower();
                if (!assets.Contains(value))
                {
                    assets.Add(kv.getValue().ToLower());
                    setStatusMessage("Model added: " + value, COLOR_ORANGE);
                    assets.AddRange(getAssetsFromModel(value));
                }
            }

            // Add sound assets
            List<SourceSDK.KeyValue> sounds = map.findChildren("message");
            foreach (SourceSDK.KeyValue kv in sounds)
            {
                string value = kv.getValue().ToLower();
                if ((value.EndsWith(".wav") || value.EndsWith(".mp3")) && !assets.Contains(value))
                {
                    setStatusMessage("Sound added: " + value, COLOR_ORANGE);
                    assets.Add(kv.getValue().ToLower());
                }
            }

            assets = assets.Distinct().ToList();

            return assets;
        }

        private List<string> getAssetsFromMaterial(string relativePath)
        {
            List<string> assets = new List<string>();
            List<string> searchPaths = getModSearchPaths(game, mod);

            foreach (string searchPath in searchPaths)
            {
                string materialPath = searchPath + "\\" + relativePath;

                if (!File.Exists(materialPath))
                    continue;

                SourceSDK.KeyValue material = SourceSDK.KeyValue.readChunkfile(materialPath);
                List<SourceSDK.KeyValue> textures = new List<SourceSDK.KeyValue>();

                textures.Add(material.getChild("$basetexture"));
                textures.Add(material.getChild("$detail"));
                textures.Add(material.getChild("$blendmodulatetexture"));
                textures.Add(material.getChild("$bumpmap"));
                textures.Add(material.getChild("$parallaxmap"));
                textures.Add(material.getChild("$basetexture2"));
                textures.Add(material.getChild("%tooltexture"));
                textures.Add(material.getChild("$blendmodulatetexture"));

                foreach (SourceSDK.KeyValue textureKv in textures)
                {
                    if (textureKv == null)
                        continue;

                    string textureValue = "materials/" + textureKv.getValue().ToLower() + ".vtf";

                    if (!assets.Contains(textureValue))
                    {
                        setStatusMessage("Texture added: " + textureValue, COLOR_ORANGE);
                        assets.Add(textureValue);
                    }
                }

                break;
            }

            return assets;
        }

        private List<string> getAssetsFromModel(string relativePath)
        {
            List<string> assets = new List<string>();
            List<string> searchPaths = getModSearchPaths(game, mod);

            foreach (string searchPath in searchPaths)
            {
                string modelPath = searchPath + "\\" + relativePath;

                if (!File.Exists(modelPath))
                    continue;

                string directory = Path.GetDirectoryName(modelPath);
                string modelName = Path.GetFileNameWithoutExtension(modelPath).ToLower();

                foreach(string file in Directory.GetFiles(directory))
                {
                    string fileName = Path.GetFileName(file.ToLower());
                    if (fileName.Substring(0, fileName.IndexOf(".")) == modelName)
                    {
                        string filePath = relativePath.Replace(modelName + ".mdl", "") + fileName;
                        setStatusMessage("Model file added: " + filePath, COLOR_ORANGE);
                        assets.Add(filePath);
                    }
                }

                List<string> materials = getModelMaterials(modelPath);
                foreach(string material in materials) {
                    assets.Add(material.Replace("\\", "/"));
                    setStatusMessage("Material added: " + material.Replace("\\", "/"), COLOR_ORANGE);
                    assets.AddRange(getAssetsFromMaterial(material));
                }

                break;
            }

            return assets;
        }

        /*private void updateList()
        {
            list.BeginUnboundLoad();

            foreach(string asset in assets)
            {
                string type = "Other";
                if (asset.EndsWith(".bsp"))
                    type = "Map";
                else if (asset.EndsWith(".vmt"))
                    type = "Material";
                else if (asset.EndsWith(".mdl"))
                    type = "Model";
                else if (asset.EndsWith(".spr"))
                    type = "Sprite";
                else if (asset.EndsWith(".wav"))
                    type = "Sound";
                else if (asset.EndsWith(".mp3"))
                    type = "Music";
                else if (asset.EndsWith(".vtf"))
                    type = "Texture";

                list.AppendNode(new object[]
                {
                    asset,
                    type
                }, null);
            }

            list.EndUnboundLoad();
        }*/

        public List<string> getModSearchPaths(string game, string mod)
        {
            List<string> result = new List<string>();

            string gamePath = sourceSDK.GetGames()[game];
            string modPath = sourceSDK.GetMods(game)[mod];

            SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(modPath + "\\gameinfo.txt");

            SourceSDK.KeyValue searchPaths = gameInfo.findChild("searchpaths");
            foreach(SourceSDK.KeyValue searchPath in searchPaths.getChildrenList())
            {
                string[] keys = searchPath.getKey().Split('+');

                if (!keys.Contains("game"))
                    continue;

                string value = searchPath.getValue();
                value = value.Replace("/", "\\");
                value = value.Replace("|all_source_engine_paths|", gamePath + "\\");
                value = value.Replace("|gameinfo_path|", modPath + "\\");
                value = value.Replace("\\\\", "\\");
                if (value.EndsWith("/"))
                    value = value.Substring(0, value.Length - 1);

                if (Directory.Exists(value) && !result.Contains(value))
                    result.Add(value);
            }

            return result;
        }

        private List<string> getModelMaterials(string fullPath)
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
                else if(b > 0)
                    chars.Add(Convert.ToChar(b));
            }

            if (!materials.Contains("Body"))
                return new List<string>();

            materials.RemoveRange(0, materials.IndexOf("Body") + 1);

            string materialPath = materials.Last();
            materials.RemoveAt(materials.Count - 1);

            for(int i = 0; i < materials.Count; i++)
                materials[i] = "materials\\" + materialPath + materials[i] + ".vmt";


            return materials;
        }

        private string copyAssets()
        {
            string gamePath = sourceSDK.GetGames()[game];
            string modPath = sourceSDK.GetMods(game)[mod];

            String mapName = Path.GetFileNameWithoutExtension(vmfs[0]).ToLower();

            List<string> searchPaths = getModSearchPaths(game, mod);

            string customPath = modPath + "\\custom\\" + mapName;
            if (this.destination != "")
                customPath = destination;

            Directory.CreateDirectory(customPath);

            foreach (string asset in assets)
            {
                foreach(string searchPath in searchPaths)
                {
                    if (File.Exists(searchPath + "\\" + asset))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(customPath + "\\" + asset));

                        File.Copy(searchPath + "\\" + asset, customPath + "\\" + asset, true);
                    }
                }
            }

            return customPath;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (vmfList.FocusedNode == null)
                return; 
            vmfs.RemoveAt(vmfList.FocusedNode.Id);
            updateVMFList();
        }

        private const int COLOR_GREEN = 1;
        private const int COLOR_BLUE = 0;
        private const int COLOR_ORANGE = 2;
        private const int COLOR_RED = 3;
        private void setStatusMessage(string message, int color)
        {
            statusLabel.Caption = message;
            switch(color)
            {
                case COLOR_ORANGE:
                    statusBar.Appearance.BackColor = Color.FromArgb(230,81,0);
                    break;
                case COLOR_GREEN:
                    statusBar.Appearance.BackColor = Color.FromArgb(27,94,32);
                    break;
                case COLOR_RED:
                    statusBar.Appearance.BackColor = Color.FromArgb(183,28,28);
                    break;
                case COLOR_BLUE:
                default:
                    statusBar.Appearance.BackColor = Color.FromArgb(13, 71, 161);
                    break;
            }
            

            Application.DoEvents();
        }
    }
}