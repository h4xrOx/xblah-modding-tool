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

namespace windows_source1ide
{
    public partial class VMFtoMDL : DevExpress.XtraEditors.XtraForm
    {
        private Steam sourceSDK;

        public VMFtoMDL(Steam sourceSDK)
        {
            InitializeComponent();

            this.sourceSDK = sourceSDK;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            vmfListBox.Items.Remove(vmfListBox.SelectedItem);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Valve Map Files (*.vmf)|*.vmf";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (String fileName in dialog.FileNames)
                {
                    vmfListBox.Items.Add(fileName);
                }
            }
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            foreach (String fileName in vmfListBox.Items)
            {
                String propperPath = sourceSDK.GetGamePath() + "\\bin\\propper.exe";

                if (!File.Exists(propperPath))
                {
                    if (File.Exists(Application.StartupPath + "\\Tools\\Propper\\propper.exe"))
                    {
                        File.Copy(Application.StartupPath + "\\Tools\\Propper\\propper.exe", propperPath, true);
                        File.Copy(Application.StartupPath + "\\Tools\\Propper\\propper.fgd", propperPath, true);
                    }
                    else
                    {
                        MessageBox.Show("Could not find propper.exe");
                        return;
                    }
                }

                String modPath = sourceSDK.GetModPath();

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = propperPath;
                startInfo.Arguments = "-game \"" + modPath + "\" \"" + fileName + "\"";
                Process.Start(startInfo);
            }
        }
    }
}