using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xblah_modding_tool.Materials
{
    public partial class ShaderDialog : DevExpress.XtraEditors.XtraForm
    {
        public ShaderDialog()
        {
            InitializeComponent();
        }

        public string Shader
        {
            get
            {
                if (treeList.Selection.Count > 0)
                {
                    return treeList.Selection[0].GetDisplayText("shader");
                }

                return null;
            }
        }
    }
}