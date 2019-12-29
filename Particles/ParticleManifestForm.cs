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
using windows_source1ide.SourceSDK;

namespace windows_source1ide.Particles
{
    public partial class ParticleManifestForm : DevExpress.XtraEditors.XtraForm
    {
        string gamePath;
        string modPath;
        Steam sourceSDK;

        string destination = "";

        List<string> vmfs = new List<string>();

        public ParticleManifestForm(Steam sourceSDK)
        {
            InitializeComponent();

            this.sourceSDK = sourceSDK;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (selectVMFDialog.ShowDialog() == DialogResult.OK)
            {
                if (!vmfs.Contains(selectVMFDialog.FileName))
                    vmfs.Add(selectVMFDialog.FileName);

                updateVMFList();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (vmfList.FocusedNode == null)
                return;
            vmfs.RemoveAt(vmfList.FocusedNode.Id);
            updateVMFList();
        }

        private void updateVMFList()
        {
            vmfList.BeginUnboundLoad();
            vmfList.Nodes.Clear();
            foreach (string vmf in vmfs)
            {
                vmfList.AppendNode(new object[] { vmf }, null);
            }
            vmfList.EndUnboundLoad();
            if (vmfs.Count > 0)
            {
                setStatusMessage("Ready to copy.", COLOR_BLUE);
            }
            else
            {
                setStatusMessage("Choose at least one VMF to start.", COLOR_RED);
            }
        }

        private void ParticleManifestForm_Load(object sender, EventArgs e)
        {
            gamePath = sourceSDK.GetGamePath();
            modPath = sourceSDK.GetModPath();
        }

        private const int COLOR_GREEN = 1;
        private const int COLOR_BLUE = 0;
        private const int COLOR_ORANGE = 2;
        private const int COLOR_RED = 3;
        private void setStatusMessage(string message, int color)
        {
            statusLabel.Caption = message;
            switch (color)
            {
                case COLOR_ORANGE:
                    statusBar.Appearance.BackColor = Color.FromArgb(230, 81, 0);
                    break;
                case COLOR_GREEN:
                    statusBar.Appearance.BackColor = Color.FromArgb(27, 94, 32);
                    break;
                case COLOR_RED:
                    statusBar.Appearance.BackColor = Color.FromArgb(183, 28, 28);
                    break;
                case COLOR_BLUE:
                default:
                    statusBar.Appearance.BackColor = Color.FromArgb(13, 71, 161);
                    break;
            }


            Application.DoEvents();
        }

        private void readMapButton_Click(object sender, EventArgs e)
        {
            foreach(string vmf in vmfs)
            {
                List<string> assets = new List<string>();

                SourceSDK.KeyValue map = SourceSDK.KeyValue.readChunkfile(vmf);

                List<string> effects = new List<string>();
                foreach(KeyValue kv in map.findChildren("effect_name"))
                    effects.Add(kv.getValue());

                effects = effects.Distinct().ToList();

                List<string> files = PCF.getAllFiles(sourceSDK);

                List<string> requiredFiles = new List<string>();

                foreach(string file in files)
                {
                    if (PCF.containsEffect(effects, file))
                        requiredFiles.Add(file);
                }

                Debugger.Break();
            }
        }
    }
}