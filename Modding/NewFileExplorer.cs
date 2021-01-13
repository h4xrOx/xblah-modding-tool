using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SourceSDK;
using SourceSDK.Packages;
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
    public partial class NewFileExplorer : DevExpress.XtraEditors.XtraForm
    {
        public string CurrentDirectory = null;
        public string RootDirectory = string.Empty;
        public bool MultiSelect = false;
        public string Filter = "";

        string keywords = string.Empty;
        Stack<string> nextDirectories = new Stack<string>();
        Stack<string> previousDirectories = new Stack<string>();

        PackageManager packageManager;
        List<PackageDirectory> directories;

        Launcher launcher;

        public NewFileExplorer(Launcher launcher)
        {
            InitializeComponent();
            this.launcher = launcher;
        }

        private void NewFileExplorer_Load(object sender, EventArgs e)
        {
            if (CurrentDirectory == null)
                this.CurrentDirectory = RootDirectory;

            //RootDirectory = "materials";

            packageManager = new PackageManager(launcher, RootDirectory);

            UpdateDirectoryTree();
            UpdateFileTree(RootDirectory);
        }

        private void UpdateDirectoryTree()
        {
            directories = packageManager.Directories;

            directoryTree.BeginUnboundLoad();

            directoryTree.Nodes.Clear();

            Dictionary<string, TreeListNode> folders = new Dictionary<string, TreeListNode>();

            foreach(PackageDirectory directory in directories)
            {
                string[] pathArray = null;

                // Root
                if (directory.Path == " ")
                    directory.Path = "";

                if (!folders.ContainsKey(directory.Path)) {
                    AddDirectoryToTree(folders, directory.Path, RootDirectory);
                }
            }

            directoryTree.EndUnboundLoad();

            directoryTree.ExpandToLevel(0);
        }

        private void AddDirectoryToTree(Dictionary<string, TreeListNode> folders, string directoryPath, string rootPath)
        {
            if (!folders.ContainsKey(directoryPath))
            {
                // Root
                if (directoryPath == rootPath)
                {
                    TreeListNode node = directoryTree.AppendNode(new object[] { (rootPath != "" ? Path.GetFileName(rootPath) : "root") }, null);
                    node.Tag = directoryPath;
                    node.StateImageIndex = 0;
                    folders.Add(directoryPath, node);
                }

                else
                {
                    string parentPath = Path.GetDirectoryName(directoryPath).Replace("\\", "/");

                    if (!folders.ContainsKey(parentPath))
                    {
                        AddDirectoryToTree(folders, parentPath, rootPath);
                    }

                    TreeListNode node = directoryTree.AppendNode(new object[] { Path.GetFileName(directoryPath) }, folders[parentPath]);
                    node.StateImageIndex = 0;
                    node.Tag = directoryPath;
                    folders.Add(directoryPath, node);
                }

            }
        }

        private void UpdateFileTree(string directoryPath)
        {
            CurrentDirectory = directoryPath;
            textDirectory.EditValue = CurrentDirectory;

            List<PackageFile> files = new List<PackageFile>();

            foreach(PackageDirectory directory in directories.Where(d => d.Path == directoryPath))
                files.AddRange(directory.Entries);

            files = files.OrderBy(f => f.Filename).ToList();

            fileTree.BeginUnboundLoad();
            fileTree.Nodes.Clear();

            foreach (string directory in directories.Select(d => d.Path).Where(d => d.StartsWith(directoryPath.Length > 0 ? directoryPath + "/" : "") && d.Remove(0, (directoryPath.Length > 0 ? directoryPath + "/" : "").Length).Split('/').Length == 1).Distinct())
            {
                if (directory == directoryPath)
                    continue;

                string folderName = directory.Remove(0, (directoryPath.Length > 0 ? directoryPath + "/" : "").Length);
                TreeListNode node = fileTree.AppendNode(new object[] { folderName, "", "" }, null);
                node.Tag = directory;
                node.StateImageIndex = 0;
            }

            foreach(PackageFile file in files.GroupBy(f => f.Filename).Select(g => g.First()))
            {
                TreeListNode node = fileTree.AppendNode(new object[] { file.Filename, file.Extension, file.Directory.ParentArchive.Name}, null);
                node.Tag = file;
                node.StateImageIndex = 1;
            }

            fileTree.EndUnboundLoad();
        }

        private void directoryTree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            /*string directoryPath = "";
            if (e.Node.Tag != null)
                directoryPath = e.Node.Tag.ToString();

            UpdateFileTree(directoryPath);*/
        }

        private void fileTree_DoubleClick(object sender, EventArgs e)
        {
            TreeList tree = sender as TreeList;
            TreeListHitInfo hi = tree.CalcHitInfo(tree.PointToClient(Control.MousePosition));
            if (hi.Node != null)
            {
                if (hi.Node.Tag is string)
                {
                    // It's a folder
                    string tag = hi.Node.Tag.ToString();
                    if (tag != CurrentDirectory)
                    {
                        previousDirectories.Push(CurrentDirectory);
                        nextDirectories.Clear();
                    }

                    UpdateFileTree(tag);
                }
                else
                {
                    // It's a file
                    //FileAction(Action.OPEN);
                }
            }
        }

        private void directoryTree_Click(object sender, EventArgs e)
        {
            TreeList tree = sender as TreeList;
            TreeListHitInfo hi = tree.CalcHitInfo(tree.PointToClient(Control.MousePosition));
            if (hi.Node != null)
            {
                // It's a folder
                string tag = hi.Node.Tag.ToString();
                if (tag != CurrentDirectory)
                {
                    previousDirectories.Push(CurrentDirectory);
                    nextDirectories.Clear();
                }

                UpdateFileTree(tag);
            }
        }
    }
}
