using source_modding_tool.SourceSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace source_modding_tool.Tools
{
    public partial class AssetsCopierForm : DevExpress.XtraEditors.XtraForm
    {
        private const int COLOR_BLUE = 0;

        private const int COLOR_GREEN = 1;
        private const int COLOR_ORANGE = 2;
        private const int COLOR_RED = 3;

        List<string> assets = new List<string>();

        string destination = string.Empty;
        BaseGame game;
        Mod mod;
        Launcher launcher;

        List<string> vmfs = new List<string>();

        public AssetsCopierForm(BaseGame game, Mod mod)
        {
            this.game = game;
            this.mod = mod;

            InitializeComponent();
        }

        public AssetsCopierForm(BaseGame game, Mod mod, string destination) : this(game, mod)
        { this.destination = destination; }

        private void AssetsCopierForm_Load(object sender, EventArgs e)
        {
            launcher = new Launcher();
            launcher.SetCurrentGame(game);
            game.SetCurrentMod(mod);

            updateVMFList();
            xtraOpenFileDialog1.InitialDirectory = launcher.GetModPath(game, mod);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if(xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(!vmfs.Contains(xtraOpenFileDialog1.FileName))
                    vmfs.Add(xtraOpenFileDialog1.FileName);

                updateVMFList();
            }
        }

        private string copyAssets()
        {
            string gamePath = game.installPath;
            string modPath = launcher.GetModPath(game, mod);

            String mapName = Path.GetFileNameWithoutExtension(vmfs[0]).ToLower();

            List<string> searchPaths = mod.GetSearchPaths();

            string customPath = modPath + "\\custom\\" + mapName;
            if(this.destination != string.Empty)
                customPath = destination;

            Directory.CreateDirectory(customPath);

            foreach(string asset in assets)
            {
                foreach(string searchPath in searchPaths)
                {
                    if(File.Exists(searchPath + "\\" + asset))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(customPath + "\\" + asset));

                        File.Copy(searchPath + "\\" + asset, customPath + "\\" + asset, true);
                    }
                }
            }

            return customPath;
        }

        private List<string> getAssetsFromMap(string fullPath)
        {
            setStatusMessage("Reading VMF " + fullPath, COLOR_ORANGE);
            assets = VMF.GetAssets(fullPath, game, mod, launcher);


            return assets;
        }

        private void readMapButton_Click(object sender, EventArgs e)
        {
            foreach(string vmf in vmfs)
                assets.AddRange(getAssetsFromMap(vmf));

            assets = assets.Distinct().ToList();
            string customPath = copyAssets();

            string modPath = launcher.GetModPath(game, mod);
            setStatusMessage("Done.", COLOR_GREEN);
            Process.Start(customPath);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if(vmfList.FocusedNode == null)
                return;
            vmfs.RemoveAt(vmfList.FocusedNode.Id);
            updateVMFList();
        }

        private void setStatusMessage(string message, int color)
        {
            statusLabel.Caption = message;
            /*switch(color)
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
            }*/

            Application.DoEvents();
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
            if(vmfs.Count > 0)
            {
                setStatusMessage("Ready to copy.", COLOR_BLUE);
            } else
            {
                setStatusMessage("Choose at least one VMF to start.", COLOR_RED);
            }
        }

        private void vmfList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            removeButton.Enabled = (vmfList.FocusedNode != null);
            readMapButton.Enabled = (vmfList.FocusedNode != null);
        }
    }
}