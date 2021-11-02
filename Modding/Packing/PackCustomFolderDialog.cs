using DevExpress.XtraEditors;
using SourceSDK;
using SourceSDK.Packages;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace source_modding_tool.Modding
{
    public partial class PackCustomFolderDialog : DevExpress.XtraEditors.XtraForm
    {
        Launcher launcher;

        PackageManager packageManager;

        public PackCustomFolderDialog(Launcher launcher)
        {
            this.launcher = launcher;
            InitializeComponent();

            foreach (string folder in Directory.GetDirectories(launcher.GetCurrentMod().InstallPath + "\\custom"))
            {
                listBoxControl1.Items.Add(Path.GetFileName(folder));
            }
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            packButton.Enabled = (listBoxControl1.SelectedItems.Count > 0);
        }

        private void packButton_Click(object sender, EventArgs e)
        {
            foreach (string folder in Directory.GetDirectories(launcher.GetCurrentMod().InstallPath + "\\custom"))
            {
                if (Path.GetFileName(folder) == listBoxControl1.SelectedItem.ToString())
                {
                    string exePath = launcher.GetCurrentGame().InstallPath + "\\bin\\vpk.exe";
                   
                    string rootPath = launcher.GetCurrentMod().InstallPath + "\\custom\\" + Path.GetFileName(folder) + "\\";

                    // Create the VPK
                    Process process = new Process();
                    process.StartInfo.FileName = exePath;
                    process.StartInfo.Arguments = rootPath;
                    process.StartInfo.WorkingDirectory = launcher.GetCurrentMod().InstallPath;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.Start();
                    process.WaitForExit();

                    // Delete the temp directory
                    if (XtraMessageBox.Show("Do you want to delete the loose files?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Directory.Delete(rootPath, true);
                    }                    

                    XtraMessageBox.Show("File '" + Path.GetFileName(folder) + ".vpk' was created in 'custom/'");

                    break;
                }
            }
        }
    }
}