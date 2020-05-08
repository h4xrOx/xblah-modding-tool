using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace source_modding_tool
{
    public partial class VMFtoMDL : DevExpress.XtraEditors.XtraForm
    {
        private Launcher launcher;

        public VMFtoMDL(Launcher launcher)
        {
            InitializeComponent();

            this.launcher = launcher;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Valve Map Files (*.vmf)|*.vmf";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                foreach(String fileName in dialog.FileNames)
                {
                    vmfListBox.Items.Add(fileName);
                }
            }
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            foreach(String fileName in vmfListBox.Items)
            {
                String propperPath = launcher.GetCurrentGame().installPath + "\\bin\\propper.exe";

                if(!File.Exists(propperPath))
                {
                    if(File.Exists(Application.StartupPath + "\\Tools\\Propper\\propper.exe"))
                    {
                        File.Copy(Application.StartupPath + "\\Tools\\Propper\\propper.exe", propperPath, true);
                        File.Copy(Application.StartupPath + "\\Tools\\Propper\\propper.fgd", propperPath, true);
                    } else
                    {
                        MessageBox.Show("Could not find propper.exe");
                        return;
                    }
                }

                String modPath = launcher.GetCurrentMod().installPath;

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = propperPath;
                startInfo.Arguments = "-game \"" + modPath + "\" \"" + fileName + "\"";
                Process.Start(startInfo);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        { vmfListBox.Items.Remove(vmfListBox.SelectedItem); }
    }
}