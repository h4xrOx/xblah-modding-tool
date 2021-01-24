using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using SourceSDK;
using SourceSDK.Packages;
using SourceSDK.Packages.UnpackedPackage;
using SourceSDK.Packages.VPKPackage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace source_modding_tool.Modding
{
    public partial class AssetsCopierForm : DevExpress.XtraEditors.XtraForm
    {
        List<PackageArchive> archives;
        PackageManager packageManager;
        Launcher launcher;

        public string RootPath;

        public List<string> filePaths;

        public AssetsCopierForm(Launcher launcher)
        {
            InitializeComponent();

            this.launcher = launcher;
            packageManager = new PackageManager(launcher, "");
        }

        private void AssetsCopierForm_Load(object sender, EventArgs e)
        {
            PopulateFilePathTree();
        }

        private void PopulateFilePathTree()
        {
            filesTreeList.BeginUnboundLoad();
            packagesTree.BeginUnboundLoad();

            filesTreeList.Nodes.Clear();
            packagesTree.Nodes.Clear();
            archives = new List<PackageArchive>();

            foreach(string filePath in filePaths)
            {
                PackageFile file = packageManager.GetFile(filePath);

                if (file == null)
                    continue;

                if (!archives.Contains(file.Directory.ParentArchive))
                {
                    TreeListNode packageNode = packagesTree.AppendNode(new object[] { file.Directory.ParentArchive.Name, (file.Directory.ParentArchive is VpkArchive ? "VPK" : "Directory") }, null);
                    packageNode.Tag = file.Directory.ParentArchive;
                    if (file.Directory.ParentArchive is UnpackedArchive)
                        packageNode.Checked = true;

                    archives.Add(file.Directory.ParentArchive);
                }
                TreeListNode node = filesTreeList.AppendNode(new object[] { filePath, (file != null ? file.Directory.ParentArchive.Name : "null") }, null);
                node.Tag = file;
                if (file.Directory.ParentArchive is UnpackedArchive)
                    node.Checked = true;
            }

            filesTreeList.Sort(null, packagesTree.Columns["filePath"], SortOrder.Ascending, false);
            packagesTree.Sort(null, packagesTree.Columns["type"], SortOrder.Ascending, false);

            filesTreeList.EndUnboundLoad();
            packagesTree.EndUnboundLoad();
        }

        private void packagesTree_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            TreeListNode node = e.Node;
            PackageArchive archive = node.Tag as PackageArchive;

            foreach (TreeListNode fileNode in filesTreeList.Nodes.Where(n => n.Tag != null && (n.Tag as PackageFile).Directory.ParentArchive == archive))
            {
                fileNode.Checked = node.Checked;
            }
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            List<PackageFile> files = filesTreeList.Nodes.Where(n => n.Tag != null && n.Tag is PackageFile).Select(m => m.Tag as PackageFile).ToList();

            RootPath = launcher.GetCurrentMod().installPath + "\\custom\\assets-copier-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "\\";

            foreach (PackageFile file in files)
            {
                string destinationPath = (RootPath + file.Path).Replace("/", "\\");

                Directory.CreateDirectory(destinationPath);

                file.CopyTo(destinationPath + "\\" + file.Filename + "." + file.Extension);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}