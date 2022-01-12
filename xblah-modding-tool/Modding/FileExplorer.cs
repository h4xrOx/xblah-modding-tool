using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using xblah_modding_tool.Materials;
using xblah_modding_tool.Sound;
using xblah_modding_lib;
using xblah_modding_lib.Maps;
using xblah_modding_lib.Packages;
using xblah_modding_lib.Packages.UnpackedPackage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace xblah_modding_tool.Modding
{
    public partial class FileExplorer : DevExpress.XtraEditors.XtraForm
    {
        public string CurrentDirectory = null;
        public string RootDirectory = string.Empty;
        public bool MultiSelect = false;
        public string Filter = "";
        public string FileName { 
            get
            {
                switch(mode)
                {
                    case Mode.SAVE:
                        return CurrentDirectory + "/" + fileNameEdit.EditValue.ToString();
                    case Mode.OPEN:
                    default:
                        return Selection[0].FullPath;
                }
            }
            set
            {
                switch (mode)
                {
                    case Mode.SAVE:
                        CurrentDirectory = Path.GetDirectoryName(value);
                        fileNameEdit.EditValue = Path.GetFileName(value);

                        break;
                    case Mode.OPEN:
                    default:
                        CurrentDirectory = Path.GetDirectoryName(value);
                        fileNameEdit.EditValue = Path.GetFileName(value);
                        break;
                }
            }
        }

        string keywords = string.Empty;
        Stack<string> nextDirectories = new Stack<string>();
        Stack<string> previousDirectories = new Stack<string>();

        public PackageManager packageManager;
        List<PackageDirectory> directories;

        Launcher launcher;
        private Mode mode = Mode.BROWSE;

        public PackageFile[] Selection { get; internal set; }

        public enum Mode
        {
            BROWSE = 0,
            OPEN = 1,
            SAVE = 2
        };

        public FileExplorer(Launcher launcher)
        {
            InitializeComponent();
            this.launcher = launcher;
        }

        public FileExplorer(Launcher launcher, Mode mode) : this(launcher)
        {
            this.mode = mode;
            switch(mode)
            {
                case Mode.OPEN:
                    {
                        openFileDialogPanel.Visible = true;
                        statusBar.Visible = false;
                        okButton.Text = "Open";
                    }
                    break;
                case Mode.SAVE:
                    {
                        openFileDialogPanel.Visible = true;
                        openFileDialogBar.Visible = true;
                        statusBar.Visible = false;
                        okButton.Text = "Save";
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
        }

        private void NewFileExplorer_Load(object sender, EventArgs e)
        {
            if (CurrentDirectory == null)
                this.CurrentDirectory = RootDirectory;

            if (packageManager == null)
                packageManager = new PackageManager(launcher, RootDirectory);

            fileTree.OptionsSelection.MultiSelect = MultiSelect;

            UpdateDirectoryTree();
            UpdateFileTree(RootDirectory);
        }

        private void UpdateDirectoryTree()
        {
            directories = packageManager.Directories;

            directoryTree.BeginUnboundLoad();

            directoryTree.Nodes.Clear();

            Dictionary<string, TreeListNode> folders = new Dictionary<string, TreeListNode>();

            foreach (PackageDirectory directory in directories)
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

                else if(directoryPath != "")
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
            buttonUp.Enabled = (CurrentDirectory != string.Empty);
            buttonBack.Enabled = (previousDirectories.Count > 0);
            buttonForward.Enabled = (nextDirectories.Count > 0);

            textSearch.EditValue = "";

            List<PackageFile> files = new List<PackageFile>();

            foreach (PackageDirectory directory in directories.Where(d => d.Path == directoryPath))
                files.AddRange(directory.Entries);

            files = files.OrderBy(f => f.Filename).ToList();

            if (Filter != string.Empty)
            {
                List<string> types = Filter.Split('|').ToList();

                for (int i = 0; i < types.Count; i++)
                    types.RemoveAt(i);

                List<PackageFile> filteredFiles = new List<PackageFile>();
                foreach(PackageFile packageFile in files)
                {
                    foreach(string type in types)
                    {
                        string[] filters = type.Split('*');

                        string str = (packageFile.Filename + "." + packageFile.Extension);

                        if ((packageFile.Filename + "." + packageFile.Extension).StartsWith(filters.First()) && (packageFile.Filename + "." + packageFile.Extension).EndsWith(filters.Last())) {
                            filteredFiles.Add(packageFile);
                            break;
                        }
                    }
                }
                files = filteredFiles;
            }

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

            List<string> addedFiles = new List<string>();

            foreach (PackageFile file in files.GroupBy(f => new { f.Filename, f.Extension }).Select(g => g.First()))
            {
                string filename = file.Filename;
                string extension = file.Extension;
                object tag = file;

                int stateImageIndex = 0;

                if (file.Filename.StartsWith("soundscapes_") && file.Filename != "soundscapes_manifest" && file.Extension == "txt")
                {
                    stateImageIndex = 8;
                    extension = "soundscape";
                }
                else if(file.Path == "materials/skybox" || file.Path == "gfx/env")
                {
                    if (file.Extension == "vtf" || file.Extension == "bmp")
                        continue;

                    if (filename.EndsWith("up") || filename.EndsWith("dn") || filename.EndsWith("lf") || filename.EndsWith("rt") || filename.EndsWith("ft") || filename.EndsWith("bk"))
                        filename = filename.Substring(0, filename.Length - 2);

                    extension = "skybox";

                    stateImageIndex = 9;
                }
                else
                {
                    switch (file.Extension)
                    {
                        case "vmt":
                            stateImageIndex = 5;
                            break;
                        case "vtf":
                            stateImageIndex = 7;
                            break;
                        case "vmf":
                            if (file.Path.StartsWith("modelsrc"))
                            {
                                stateImageIndex = 3;
                            }
                            else
                            {
                                stateImageIndex = 6;
                            }

                            break;
                        case "vmx":
                            stateImageIndex = 6;
                            break;
                        case "bsp":
                            stateImageIndex = 2;
                            break;
                        case "wav":
                            stateImageIndex = 8;
                            break;
                        default:
                            stateImageIndex = 1;
                            break;
                    }
                }

                if (addedFiles.Contains(filename + "." + extension))
                    continue;

                TreeListNode node = fileTree.AppendNode(new object[] { filename, extension, file.Directory.ParentArchive.Name }, null);
                node.StateImageIndex = stateImageIndex;
                node.Tag = file;

                addedFiles.Add(filename + "." + extension);
            }

            fileTree.EndUnboundLoad();
        }

        private void SearchFileTree(string searchString)
        {
            textDirectory.EditValue = "Searching in " + CurrentDirectory;
            buttonUp.Enabled = (CurrentDirectory != string.Empty);
            buttonBack.Enabled = (previousDirectories.Count > 0);
            buttonForward.Enabled = (nextDirectories.Count > 0);

            searchString = searchString.ToLower();

            List<PackageFile> files = new List<PackageFile>();

            foreach (PackageDirectory directory in directories.Where(d => d.Path.ToLower() == CurrentDirectory.ToLower() || d.Path.ToLower().StartsWith((CurrentDirectory != "" ? CurrentDirectory + "/" : "").ToLower())))
                files.AddRange(directory.Entries);

            files = files.OrderBy(f => f.Filename).ToList();

            fileTree.BeginUnboundLoad();
            fileTree.Nodes.Clear();

            foreach (PackageFile file in files.GroupBy(f => f.Filename).Select(g => g.First()))
            {
                // Search pattern
                if (!(file.Directory.Path + "/" + file.Filename).ToLower().Contains(searchString))
                    continue;

                TreeListNode node = fileTree.AppendNode(new object[] { file.Directory.Path + "/" + file.Filename, file.Extension, file.Directory.ParentArchive.Name }, null);
                node.Tag = file;
                node.StateImageIndex = 1;
            }

            fileTree.EndUnboundLoad();
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
                else if (hi.Node.Tag is PackageFile && mode == Mode.BROWSE)
                {
                    // It's a file;
                    OpenFile(hi.Node.Tag as PackageFile);
                } else
                {
                    // Unknown listing type.
                }
            }
        }

        private void OpenFile(PackageFile packageFile)
        {
            if (packageFile.Path == "materials/skybox" || packageFile.Path == "gfx/env")
            {
                // It's a skybox.
                SkyboxEditor skyboxEditor = new SkyboxEditor(launcher)
                {
                    PackageFile = packageFile
                };
                skyboxEditor.ShowDialog();
            }
            else if (packageFile.Extension == "vmt")
            {
                // It's a material file.
                MaterialEditor materialEditor = new MaterialEditor(launcher, packageFile);
                materialEditor.ShowDialog();
            }
            else if(packageFile.Extension == "vmf" || packageFile.Extension == "vmx")
            {
                if (packageFile.Path.StartsWith("modelsrc"))
                {
                    // It's a map file.
                    Hammer.RunSourceHammerWithPropper(launcher.GetCurrentMod(), packageFile);
                } else
                {
                    // It's a map file.
                    Hammer.RunHammer(launcher, null, null, packageFile);
                }

            }
            else if (packageFile.Filename.StartsWith("soundscapes_") && packageFile.Filename != "soundscapes_manifest" && packageFile.Extension == "txt")
            {
                // It's a soundscape.
                SoundscapeEditor soundscapeEditor = new SoundscapeEditor(launcher)
                {
                    PackageFile = packageFile
                };
                soundscapeEditor.ShowDialog();
            }
            else if(new string[] { "txt", "gi" }.Contains(packageFile.Extension))
            {
                // TODO temp method. This will only work for unpacked files.
                if (packageFile is UnpackedFile)
                    Process.Start("NOTEPAD", packageFile.Directory.ParentArchive.ArchivePath + "\\" + packageFile.Path + "\\" + packageFile.Filename + "." + packageFile.Extension);
            }
            else if(packageFile.Extension == "wav")
            {
                using (Stream s = new MemoryStream(packageFile.Data))
                {
                    System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer(s);
                    myPlayer.Play();
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

        private void repositoryTextSearch_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void textSearch_EditValueChanged(object sender, EventArgs e)
        {
            string searchString = textSearch.EditValue.ToString();
            if (searchString != "")
                SearchFileTree(searchString);
            else
                UpdateFileTree(CurrentDirectory);
        }

        private void fileTree_SelectionChanged(object sender, EventArgs e)
        {
            List<PackageFile> result = new List<PackageFile>();
            foreach(TreeListNode node in fileTree.Selection)
            {
                if (node.Tag is PackageFile)
                {
                    result.Add(node.Tag as PackageFile);
                }
            }

            Selection = result.ToArray();
            okButton.Enabled = (Selection.Length > 0);

            if (result.Count == 1 && result[0].Extension == "wav")
            {
                previewButton.Enabled = true;
            } else
            {
                previewButton.Enabled = false;
            }

            if (Selection.Length > 0)
                fileNameEdit.EditValue = string.Join(", ", fileTree.Selection.Select(p => p["name"]).ToArray());

            //fileNameEdit.EditValue = string.Join(", ", Selection.Select(p => p.Filename).ToArray());
        }

        private void navigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item == buttonBack && previousDirectories.Count > 0)
            {
                nextDirectories.Push(CurrentDirectory);
                UpdateFileTree(previousDirectories.Pop());
            }
            if (e.Item == buttonForward && nextDirectories.Count > 0)
            {
                previousDirectories.Push(CurrentDirectory);
                UpdateFileTree(nextDirectories.Pop());
            }
            if (e.Item == buttonUp && CurrentDirectory != string.Empty)
            {
                previousDirectories.Push(CurrentDirectory);
                nextDirectories.Clear();

                if (CurrentDirectory.Contains("/"))
                    CurrentDirectory = CurrentDirectory.Substring(0, CurrentDirectory.LastIndexOf("/"));

                if (CurrentDirectory.Contains("/"))
                    CurrentDirectory = CurrentDirectory.Substring(0, CurrentDirectory.LastIndexOf("/") + 1);
                else
                    CurrentDirectory = string.Empty;

                UpdateFileTree(CurrentDirectory);
            }
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            PackageFile file = Selection[0];

            if (file.Extension == "wav")
            {
                using (Stream s = new MemoryStream(file.Data))
                {
                    System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer(s);
                    myPlayer.Play();
                }
            }
        }

        private void fileNameEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (mode == Mode.SAVE)
            {
                okButton.Enabled = (fileNameEdit.EditValue.ToString().Length > 0);
            }
        }

        // Context menu
        private void fileTree_MouseUp(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = fileTree.CalcHitInfo(e.Location);

            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None
                   && fileTree.State == TreeListState.Regular)
            {
                Point pt = fileTree.PointToClient(MousePosition);
                TreeListHitInfo info = fileTree.CalcHitInfo(pt);

                if (info.Node == null)
                {
                    fileTree.ClearSelection();
                }
                else
                {
                    fileTree.SetFocusedNode(info.Node);

                    if (!info.Node.IsSelected)
                    {
                        fileTree.ClearSelection();
                        fileTree.SelectNode(info.Node);
                    }
                }

                if (fileTree.Selection.Count > 0)
                {
                    bool isFile = false;
                    bool isFolder = false;
                    bool multiple = fileTree.Selection.Count > 1;

                    bool isFilePacked = true;
                    bool isFileUnpacked = true;

                    bool isFolderUnpacked = true;

                    contextFolderOpen.Visibility = (!multiple ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never);

                    foreach (TreeListNode node in fileTree.Selection)
                    {
                        if (node.Tag is PackageFile)
                        {
                            isFile = true;

                            PackageFile packageFile = node.Tag as PackageFile;

                            if (packageFile is UnpackedFile)
                            {
                                isFilePacked = false;
                            } else
                            {
                                isFileUnpacked = false;
                            }                            
                        } else
                        {
                            isFolder = true;

                            if (!Directory.Exists(launcher.GetCurrentMod().InstallPath + "\\" + node.Tag.ToString().Replace("/", "\\")))
                            {
                                isFolderUnpacked = false;
                            }
                        }
                    }

                    contextFileExtractFromVPK.Visibility = (isFilePacked ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never);
                    contextFileDelete.Visibility = (isFileUnpacked ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never);
                    contextFileShowInWindowsExplorer.Visibility = (isFileUnpacked ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never);

                    contextFolderOpenInWindows.Visibility = (!multiple && isFolderUnpacked ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never);

                    if (isFile && !isFolder)
                    {
                        fileMenu.ShowPopup(MousePosition);
                    }
                    if (isFolder && !isFile)
                    {
                        folderMenu.ShowPopup(MousePosition);
                    }
                }
            }
        }

        private void contextFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item == contextFileDelete)
            {
                foreach(TreeListNode node in fileTree.Selection)
                {
                    string path = launcher.GetCurrentMod().InstallPath + "\\" + (node.Tag as PackageFile).FullPath;
                    File.Delete(path);
                }

                packageManager.Refresh();
                UpdateDirectoryTree();
                UpdateFileTree(CurrentDirectory);
            }
            else if(e.Item == contextFileExtractFromVPK)
            {
                Directory.CreateDirectory(launcher.GetCurrentMod().InstallPath + "\\" + CurrentDirectory);
                foreach (TreeListNode node in fileTree.Selection)
                {
                    PackageFile packageFile = (node.Tag as PackageFile);
                    string path = launcher.GetCurrentMod().InstallPath + "\\" + packageFile.FullPath;
                    File.WriteAllBytes(path, packageFile.Data);
                }

                packageManager.Refresh();
                UpdateDirectoryTree();
                UpdateFileTree(CurrentDirectory);
            }
            else if (e.Item == contextFileShowInWindowsExplorer)
            {
                PackageFile packageFile = (fileTree.Selection[0].Tag as PackageFile);
                string path = Path.GetDirectoryName(launcher.GetCurrentMod().InstallPath + "\\" + packageFile.FullPath);
                Process.Start(path);
            }
            else if(e.Item == contextFileDecompile)
            {
                List<PackageFile> bspFiles = new List<PackageFile>();
                foreach (TreeListNode node in fileTree.Selection)
                {
                    PackageFile packageFile = (node.Tag as PackageFile);
                    if (packageFile.Extension == "bsp")
                    {
                        // Map files
                        bspFiles.Add(packageFile);
                    }
                }

                if (bspFiles.Count > 0)
                {
                    foreach(PackageFile packageFile in bspFiles)
                        BSP.Decompile(packageFile, launcher);

                    if ("mapsrc" != CurrentDirectory)
                    {
                        previousDirectories.Push(CurrentDirectory);
                        nextDirectories.Clear();
                    }

                    UpdateFileTree("mapsrc");
                }
            }
            else if (e.Item == contextFolderOpen)
            {
                string tag = fileTree.Selection[0].Tag.ToString();
                if (tag != CurrentDirectory)
                {
                    previousDirectories.Push(CurrentDirectory);
                    nextDirectories.Clear();
                }

                UpdateFileTree(tag);
            }
            else if(e.Item == contextFolderOpenInWindows)
            {
                string tag = fileTree.Selection[0].Tag.ToString();
                string path = launcher.GetCurrentMod().InstallPath + "\\" + tag.Replace("/", "\\");
                Process.Start(path);
            }
        }

        private void fileTree_Click(object sender, EventArgs e)
        {
            fileTree_SelectionChanged(sender, e);
        }

        private void newFolderButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var folderName = XtraInputBox.Show("Type the folder name.", "New folder", "New folder");

            var directories = this.directories.Where(d => d.Path == CurrentDirectory + "/" + folderName).ToList();
            if (directories.Count > 0)
            {
                XtraMessageBox.Show("Directory already exists.");
            } else
            {
                packageManager.CreateDirectory(CurrentDirectory + "/" + folderName);
                UpdateDirectoryTree();
                UpdateFileTree(RootDirectory);
            }

            var nodes = fileTree.Nodes.Where(n => !(n.Tag is PackageFile) && n.Tag.ToString() == CurrentDirectory + "/" + folderName).ToList();
            if (nodes.Count > 0)
            {
                fileTree.SelectNode(nodes[0]);
                fileTree.FocusedNode = nodes[0];
                fileTree.MakeNodeVisible(fileTree.FocusedNode);
            }
        }
    }
}
