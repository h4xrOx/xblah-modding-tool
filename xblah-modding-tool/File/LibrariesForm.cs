using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using xblah_modding_lib;

namespace xblah_modding_tool
{
    public partial class LibrariesForm : DevExpress.XtraEditors.XtraForm
    {
        Launcher launcher;

        string selectedPath = "";
        string selectedSource = "";

        public LibrariesForm(Launcher launcher)
        {
            this.launcher = launcher;
            InitializeComponent();
        }

        private void LibrariesForm_Load(object sender, EventArgs e)
        {
            UpdateLibrariesList();
        }

        private void UpdateLibrariesList()
        {
            if (launcher == null)
                return;

            list.ClearNodes();
            list.BeginUnboundLoad();

            foreach(string path in launcher.libraries.GetSteamLibraries())
            {
                list.AppendNode(new object[] { path, "Steam" }, null);
            }

            foreach (string path in launcher.libraries.GetUserLibraries())
            {
                list.AppendNode(new object[] { path, "User" }, null);
            }

            list.EndUnboundLoad();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (addDialog.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(addDialog.SelectedPath) && !launcher.libraries.GetList().Contains(addDialog.SelectedPath))
                {
                    launcher.libraries.AddUserLibrary(addDialog.SelectedPath);

                    UpdateLibrariesList();
                }
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            launcher.libraries.RemoveUserLibrary(selectedPath);
            UpdateLibrariesList();
        }

        private void list_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
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