using DevExpress.XtraEditors;
using SourceSDK;
using SourceSDK.Packages;
using SourceSDK.Packages.VPKPackage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace source_modding_tool.Modding
{
    public partial class UnpackForm : DevExpress.XtraEditors.XtraForm
    {
        Launcher launcher;

        PackageManager packageManager;

        public UnpackForm(Launcher launcher)
        {
            this.launcher = launcher;
            InitializeComponent();

            packageManager = new PackageManager(launcher, "");

            foreach(var archive in packageManager.Archives)
            {
                if (archive is VpkArchive)
                {
                    listBoxControl1.Items.Add(archive.Name);
                }
            }
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            unpackButton.Enabled = (listBoxControl1.SelectedItems.Count > 0);
        }

        private void unpackButton_Click(object sender, EventArgs e)
        {
            
            foreach (var archive in packageManager.Archives)
            {
                if (archive.Name == listBoxControl1.SelectedItem.ToString())
                {
                    string rootPath = launcher.GetCurrentMod().InstallPath + "\\custom\\" + Path.GetFileNameWithoutExtension(archive.ArchivePath) + "\\";
                    
                    foreach(var directory in archive.Directories)
                    {
                        Directory.CreateDirectory(rootPath + directory.Path);
                        foreach (var entry in directory.Entries)
                        {
                            File.WriteAllBytes(rootPath + entry.FullPath, entry.Data);
                        }
                    }
                    break;
                }
            }
        }
    }
}