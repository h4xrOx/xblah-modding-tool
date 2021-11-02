using DevExpress.XtraEditors;
using SourceSDK;
using SourceSDK.Packages;
using SourceSDK.Packages.UnpackedPackage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace source_modding_tool.Modding
{
    public partial class PackFilesDialog : DevExpress.XtraEditors.XtraForm
    {
        private Launcher launcher;

        List<PackageFile> files = new List<PackageFile>();

        public PackFilesDialog(Launcher launcher)
        {
            this.launcher = launcher;

            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            FileExplorer fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.OPEN)
            {
                MultiSelect = true
            };
            if (fileExplorer.ShowDialog() == DialogResult.OK)
            {
                PackageFile[] packageFiles = fileExplorer.Selection;

                foreach(PackageFile packageFile in packageFiles)
                {
                    //if (!(packageFile is UnpackedFile))
                        //continue;

                    files.Add(packageFile);
                }

                UpdateTreeList();
            }
        }

        private void UpdateTreeList()
        {
            treeList.BeginUnboundLoad();
            treeList.Nodes.Clear();
            foreach(PackageFile file in files)
            {
                var node = treeList.AppendNode(new object[] { file.FullPath }, null);
                node.Tag = file;
            }
            treeList.EndUnboundLoad();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            CreateVPK();
        }

        private void CreateVPK()
        {
            string exePath = launcher.GetCurrentGame().InstallPath + "\\bin\\vpk.exe";

            // Create the custom directory
            Directory.CreateDirectory(launcher.GetCurrentMod().InstallPath + "\\custom");

            // Create a temporary directory
            string rootPath = launcher.GetCurrentMod().InstallPath + "\\custom\\" + fileNameEdit.EditValue.ToString() + "\\";
            Directory.CreateDirectory(rootPath);

            bool deleteFiles = false;
            if (XtraMessageBox.Show("Do you want to delete the loose files?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                deleteFiles = true;
            }

            // Copy the files to the directory
            foreach (PackageFile file in files)
            {
                string targetPath = file.Directory.Path;
                if (targetPath.StartsWith("custom"))
                {
                    targetPath = targetPath.Substring(targetPath.IndexOf("/") + 1);
                    targetPath = targetPath.Substring(targetPath.IndexOf("/") + 1);
                }

                Directory.CreateDirectory(rootPath + targetPath);
                File.WriteAllBytes(rootPath + targetPath + "/" + file.Filename + "." + file.Extension, file.Data);

                // Delete the original files
                if (deleteFiles && file is UnpackedFile && File.Exists(launcher.GetCurrentMod().InstallPath + "/" + file.FullPath))
                {
                    File.Delete(launcher.GetCurrentMod().InstallPath + "/" + file.FullPath);

                    var directory = launcher.GetCurrentMod().InstallPath + "/" + file.Directory;

                    // Delete the directory if empty.
                    while (Directory.Exists(directory) && Directory.GetFiles(directory).Length == 0)
                    {
                        Directory.Delete(directory);
                        directory = Directory.GetParent(directory).FullName;
                        System.Diagnostics.Debugger.Break();
                    }
                }
            }

            // Create the VPK
            Process process = new Process();
            process.StartInfo.FileName = exePath;
            process.StartInfo.Arguments = rootPath;
            process.StartInfo.WorkingDirectory = launcher.GetCurrentMod().InstallPath;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();

            // Delete the temp directory
            Directory.Delete(rootPath, true);

            XtraMessageBox.Show("File '" + fileNameEdit.EditValue.ToString() + "' was created in 'custom/'");
        }
    }
}