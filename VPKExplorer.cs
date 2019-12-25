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
using System.Diagnostics;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace windows_source1ide.Tools
{
    public partial class VPKExplorer : DevExpress.XtraEditors.XtraForm
    {
        string game;
        string mod;
        Steam sourceSDK;

        string currentDirectory = "";
        Stack<string> previousDirectories = new Stack<string>();
        Stack<string> nextDirectories = new Stack<string>();

        string filter = "";

        Dictionary<string, List<string>> vpks = new Dictionary<string, List<string>>();

        public VPKExplorer(string game, string mod)
        {
            this.game = game;
            this.mod = mod;

            InitializeComponent();
        }

        private void VPKExplorer_Load(object sender, EventArgs e)
        {
            sourceSDK = new Steam();

            vpks.Clear();
            foreach(string vpk in sourceSDK.getModMountedVPKs(game, mod))
                vpks.Add(vpk, sourceSDK.listFilesInVPK(game, vpk));

            traverseFileTree();
            traverseDirectory("");
        }

        class Folder {
            List<string> files = new List<string>();
            List<Folder> folders = new List<Folder>();
        }

        private void traverseFileTree()
        {
            List<string> files = getAllFiles();

            dirs.BeginUnboundLoad();
            dirs.Nodes.Clear();

            Stack<TreeListNode> stack = new Stack<TreeListNode>();
            Stack<string> stackString = new Stack<string>();

            stack.Push(dirs.AppendNode(new object[] { "root" },null));
            stack.Peek().Tag = "";
            stack.Peek().StateImageIndex = 0;

            for (int f = 0; f < files.Count; f++)
            {
                string file = files[f];

                string[] fileSplit = file.Split('/');

                while (stackString.Count >= fileSplit.Length)
                {
                    stackString.Pop();
                    stack.Pop();
                }

                for (int i = stackString.Count - 1; i >= 0; i--)
                {
                    if (stackString.Peek() != fileSplit[i])
                    {
                        stackString.Pop();
                        stack.Pop();
                    }
                    else
                        break;
                }

                for (int i = stack.Count - 1; i < fileSplit.Length - 1; i++)
                {
                    string tag = fileSplit[i] + "/";
                    if (stackString.Count > 0)
                        tag = stack.Peek().Tag.ToString() + tag;

                    stack.Push(dirs.AppendNode(new object[] { fileSplit[i] }, stack.Peek()));
                    stack.Peek().Tag = tag;
                    stack.Peek().StateImageIndex = 0;
                    stackString.Push(fileSplit[i]);
                }
                
                //dirs.AppendNode(new object[] { fileSplit.Last() }, stack.Peek());
            }

            dirs.ExpandToLevel(0);

            dirs.EndUnboundLoad();
        }

        private void traverseDirectory(string directory)
        {
            currentDirectory = directory;
            buttonUp.Enabled = (currentDirectory != "");
            buttonBack.Enabled = (previousDirectories.Count > 0);
            buttonForward.Enabled = (nextDirectories.Count > 0);

            filter = "";
            textSearch.EditValue = "";

            if (directory.Contains("/"))
                repositoryTextSearch.NullValuePrompt = "Search in " + directory.Substring(0, directory.Length - 1).Split('/').Last();
            else
                repositoryTextSearch.NullValuePrompt = "Search";

            textDirectory.EditValue = directory;

            list.BeginUnboundLoad();
            list.Nodes.Clear();

            List<string> files = getAllFiles();

            List<string> usedFiles = new List<string>();

            for (int f = 0; f < files.Count; f++)
            {
                string file = files[f];

                if (!file.StartsWith(directory))
                    continue;

                file = file.Substring(directory.Length);

                string[] fileSplit = file.Split('/');

                if (fileSplit.Length > 1)
                {
                    // It's a directory
                    if (usedFiles.Contains(fileSplit[0]))
                        continue;

                    TreeListNode node = list.AppendNode(new object[] { fileSplit[0], "Folder" }, null);
                    node.Tag = directory + fileSplit[0] + "/";
                    node.StateImageIndex = 0;
                    usedFiles.Add(fileSplit[0]);
                } else
                {
                    // It's a file
                    TreeListNode node = list.AppendNode(new object[] { fileSplit[0], "File" }, null);
                    node.Tag = directory + file;
                    node.StateImageIndex = 1;
                    usedFiles.Add(file);
                } 
            }

            list.EndUnboundLoad();
        }

        private void traverseDirectoryFiltered(string directory)
        {
            buttonUp.Enabled = (currentDirectory != "");
            buttonBack.Enabled = (previousDirectories.Count > 0);
            buttonForward.Enabled = (nextDirectories.Count > 0);

            textDirectory.EditValue = "Search results for " + filter;

            list.BeginUnboundLoad();
            list.Nodes.Clear();

            List<string> files = getAllFiles();

            List<string> usedFiles = new List<string>();

            for (int f = 0; f < files.Count; f++)
            {
                string file = files[f];

                if (!file.StartsWith(directory))
                    continue;

                string[] fileSplit = file.Split('/');

                string dir = "";
                for (int j = 0; j < fileSplit.Length; j++)
                {
                    dir = dir + fileSplit[j] + "/";

                    if (!fileSplit[j].Contains(filter))
                        continue;

                    if (j < fileSplit.Length - 1)
                    {
                        // It's a directory
                        if (usedFiles.Contains(dir))
                            continue;

                        TreeListNode node = list.AppendNode(new object[] { file, "Folder" }, null);
                        node.Tag = dir;
                        node.StateImageIndex = 0;
                        usedFiles.Add(dir);
                    } else
                    {
                        // It's a file
                        TreeListNode node = list.AppendNode(new object[] { file, "File" }, null);
                        node.Tag = file;
                        node.StateImageIndex = 1;
                        usedFiles.Add(file);
                    }
                }
            }

            list.EndUnboundLoad();
        }

        private List<string> getAllFiles()
        {
            List<string> files = new List<string>();
            foreach (List<string> vpk in vpks.Values)
                files.AddRange(vpk);
            files = files.Distinct().ToList();

            files = files.Where(x => x.Contains(filter)).ToList();
            files.Sort();

            return files;
        }

        private void dirs_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (dirs.FocusedNode == null || dirs.FocusedNode.Tag == null)
                return;

            string directory = dirs.FocusedNode.Tag.ToString();

            if (directory != currentDirectory)
            {
                previousDirectories.Push(currentDirectory);
                nextDirectories.Clear();
            }

            traverseDirectory(directory);
        }

        private void list_DoubleClick(object sender, EventArgs e)
        {
            TreeList tree = sender as TreeList;
            TreeListHitInfo hi = tree.CalcHitInfo(tree.PointToClient(Control.MousePosition));
            if (hi.Node != null)
            {
                string tag = hi.Node.Tag.ToString();
                if (tag.EndsWith("/"))
                {
                    // It's a folder
                    if (tag != currentDirectory)
                    {
                        previousDirectories.Push(currentDirectory);
                        nextDirectories.Clear();
                    }

                    traverseDirectory(tag);
                } else
                {
                    // It's a file
                    extractSelected();
                }
            }
        }

        private void buttonUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (currentDirectory == "")
                return;

            previousDirectories.Push(currentDirectory);
            nextDirectories.Clear();

            if (currentDirectory.Contains("/"))
                currentDirectory = currentDirectory.Substring(0, currentDirectory.LastIndexOf("/"));

            if (currentDirectory.Contains("/"))
                currentDirectory = currentDirectory.Substring(0, currentDirectory.LastIndexOf("/") + 1);
            else
                currentDirectory = "";



            traverseDirectory(currentDirectory);
        }

        private void buttonBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (previousDirectories.Count > 0)
            {
                nextDirectories.Push(currentDirectory);
                traverseDirectory(previousDirectories.Pop());
            }
        }

        private void buttonForward_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (nextDirectories.Count > 0)
            {
                previousDirectories.Push(currentDirectory);
                traverseDirectory(nextDirectories.Pop());
            }
        }

        private void repositoryTextSearch_EditValueChanged(object sender, EventArgs e)
        {
            filter = ((TextEdit) sender).EditValue.ToString();
            if (filter != "")
                traverseDirectoryFiltered(currentDirectory);
            else
                traverseDirectory(currentDirectory);
        }

        private void extractSelected()
        {
            var nodes = list.Selection;
            List<string> values = new List<string>();
            foreach (TreeListNode node in nodes)
            {
                values.Add(node.Tag.ToString());
            }

            foreach(string filePath in values)
            {
                sourceSDK.extractFileFromVPKs(game, mod, vpks, filePath, Application.StartupPath);
            }

            string modPath = sourceSDK.GetMods(game)[mod];
            Process.Start(modPath);
        }

        private void buttonDownload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            extractSelected();
        }

        private void list_SelectionChanged(object sender, EventArgs e)
        {
            buttonDownload.Enabled = (list.Selection.Count > 0);
            buttonEdit.Enabled = (list.Selection.Count == 1);
        }
    }
}