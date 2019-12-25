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
using windows_source1ide.SourceSDK;

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
            selectVMFDialog.InitialDirectory = sourceSDK.GetModPath(game, mod);
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

            string modPath = sourceSDK.GetModPath(game,mod);
            setStatusMessage("Done.", COLOR_GREEN);
            Process.Start(customPath);
        }

        private List<string> getAssetsFromMap(string fullPath)
        {
            setStatusMessage("Reading VMF " + fullPath, COLOR_ORANGE);
            assets = VMF.getAssets(fullPath, game, mod, sourceSDK);
            

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



        

        private string copyAssets()
        {
            string gamePath = sourceSDK.GetGamePath(game);
            string modPath = sourceSDK.GetModPath(game, mod);

            String mapName = Path.GetFileNameWithoutExtension(vmfs[0]).ToLower();

            List<string> searchPaths = sourceSDK.getModSearchPaths(game, mod);

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