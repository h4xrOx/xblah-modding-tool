using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using xblah_modding_lib;
using DevExpress.XtraEditors;

namespace xblah_modding_tool
{
    public partial class LibrariesForm : DevExpress.XtraEditors.XtraForm
    {
        private Launcher launcher;

        private string selectedPath = "";
        private string selectedSource = "";

        public LibrariesForm(Launcher launcher)
        {
            this.launcher = launcher;
            InitializeComponent();
        }

        private void LibrariesForm_Load(object sender, EventArgs e)
        {
            UpdateItemsTree();
        }

        private void UpdateItemsTree()
        {
            if (launcher == null)
                return;

            itemsTree.ClearNodes();
            itemsTree.BeginUnboundLoad();

            foreach(string path in launcher.libraries.GetSteamLibraries())
            {
                itemsTree.AppendNode(new object[] { path, "Steam" }, null);
            }

            foreach (string path in launcher.libraries.GetUserLibraries())
            {
                itemsTree.AppendNode(new object[] { path, "User" }, null);
            }

            itemsTree.EndUnboundLoad();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (addDialog.ShowDialog() == DialogResult.OK)
            {
                if (!Directory.Exists(addDialog.SelectedPath)) {
                    XtraMessageBox.Show("The library directory does not exist.");
                    return;
                } 
                else if(launcher.libraries.GetList().Contains(addDialog.SelectedPath))
                {
                    XtraMessageBox.Show("The library directory is already added.");
                    return;
                } 
                else
                {
                    bool hasSteamappsFolder = false;
                    foreach(var dir in Directory.GetDirectories(addDialog.SelectedPath))
                    {
                        if (Path.GetFileName(dir).ToLower() == "steamapps")
                        {
                            hasSteamappsFolder = true;
                            break;
                        }
                    }

                    if (!hasSteamappsFolder)
                    {
                        XtraMessageBox.Show("The library directory must contain the SteamApps folder.");
                        return;
                    }

                    launcher.libraries.AddUserLibrary(addDialog.SelectedPath);

                    UpdateItemsTree();
                }
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            launcher.libraries.RemoveUserLibrary(selectedPath);
            UpdateItemsTree();
        }

        private void itemsTree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null)
            {
                selectedPath = e.Node["path"].ToString();
                selectedSource = e.Node["source"].ToString();
            } else
            {
                selectedPath = "";
                selectedSource = "";
            }

            removeButton.Enabled = (selectedSource == "User");
            browseButton.Enabled = Directory.Exists(selectedPath);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            Process.Start(selectedPath);
        }
    }
}