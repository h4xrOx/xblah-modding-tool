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
using System.IO;
using System.Diagnostics;

namespace SourceModdingTool
{
    public partial class LibrariesForm : DevExpress.XtraEditors.XtraForm
    {
        Steam sourceSDK;

        List<string> steamLibs;
        List<string> userLibs;

        string selectedPath = "";
        string selectedSource = "";

        public LibrariesForm(Steam sourceSDK)
        {
            this.sourceSDK = sourceSDK;
            InitializeComponent();
        }

        private void LibrariesForm_Load(object sender, EventArgs e)
        {
            UpdateLibrariesList();
        }

        private void UpdateLibrariesList()
        {
            steamLibs = sourceSDK.GetSteamLibraries();
            userLibs = sourceSDK.GetUserLibraries();

            list.ClearNodes();
            list.BeginUnboundLoad();

            foreach(string path in steamLibs)
            {
                list.AppendNode(new object[] { path, "Steam" }, null);
            }

            foreach (string path in userLibs)
            {
                list.AppendNode(new object[] { path, "User" }, null);
            }

            list.EndUnboundLoad();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (addDialog.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(addDialog.SelectedPath) && !userLibs.Contains(addDialog.SelectedPath) && !steamLibs.Contains(addDialog.SelectedPath))
                {
                    sourceSDK.AddUserLibrary(addDialog.SelectedPath);

                    UpdateLibrariesList();
                }
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            sourceSDK.RemoveUserLibrary(selectedPath);
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