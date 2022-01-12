﻿using xblah_modding_tool.Modding;
using xblah_modding_lib;
using xblah_modding_lib.Maps;
using xblah_modding_lib.Packages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace xblah_modding_tool.Tools
{
    public partial class AssetsRequiredForm : DevExpress.XtraEditors.XtraForm
    {
        public bool OpenDestination = true;

        private const int COLOR_BLUE = 0;

        private const int COLOR_GREEN = 1;
        private const int COLOR_ORANGE = 2;
        private const int COLOR_RED = 3;

        List<string> assets = new List<string>();

        public string destination = string.Empty;
        Game game;
        Mod mod;
        Launcher launcher;
        PackageManager packageManager;

        List<string> vmfs = new List<string>();

        public AssetsRequiredForm(Launcher launcher, Mod mod)
        {
            this.launcher = launcher;
            this.game = mod.Game;
            this.mod = mod;
            this.packageManager = new PackageManager(launcher, "");

            InitializeComponent();
        }

        public AssetsRequiredForm(Launcher launcher, Mod mod, string destination) : this(launcher, mod)
        { this.destination = destination; }

        private void AssetsCopierForm_Load(object sender, EventArgs e)
        {
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

        private List<string> getAssetsFromMap(string fullPath)
        {
            setStatusMessage("Reading VMF " + fullPath, COLOR_ORANGE);
            assets = VMF.GetAssets(fullPath, packageManager);


            return assets;
        }

        private void readMapButton_Click(object sender, EventArgs e)
        {
            foreach(string vmf in vmfs)
                assets.AddRange(getAssetsFromMap(vmf));

            assets = assets.Distinct().ToList();

            AssetsCopierForm form = new AssetsCopierForm(launcher, packageManager);
            form.filePaths = assets;
            if (form.ShowDialog() == DialogResult.OK)
            {
                string modPath = launcher.GetModPath(game, mod);
                setStatusMessage("Done.", COLOR_GREEN);

                if (OpenDestination)
                    Process.Start(form.RootPath);
                else
                    DialogResult = DialogResult.OK;

                Close();
            }
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