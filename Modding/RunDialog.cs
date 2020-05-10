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

namespace source_modding_tool.Modding
{
    public partial class RunDialog : DevExpress.XtraEditors.XtraForm
    {
        public int RunMode;
        public string Commands;
        public RunDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            switch(presetCombo.EditValue.ToString())
            {
                case "Default":
                    this.RunMode = source_modding_tool.RunMode.DEFAULT;
                    break;
                case "Fullscreen":
                    this.RunMode = source_modding_tool.RunMode.DEFAULT;
                    break;
                case "Windowed":
                    this.RunMode = source_modding_tool.RunMode.DEFAULT;
                    break;
                case "VR":
                    this.RunMode = source_modding_tool.RunMode.DEFAULT;
                    break;
                case "Ingame Tools":
                    this.RunMode = source_modding_tool.RunMode.DEFAULT;
                    break;
            }
            this.Commands = (commandText.EditValue != null ? commandText.EditValue.ToString() : "");
        }
    }
}