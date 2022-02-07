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
using xblah_modding_lib;
using xblah_modding_lib.Packages;

namespace xblah_modding_tool.Scripting
{
    public partial class TextEditor : DevExpress.XtraEditors.XtraForm
    {
        Launcher launcher;
        PackageManager packageManager;

        bool hasCarriageReturn = true;

        /// <summary>
        /// When the Text Editor is loaded and a packageFile is set, it will open the package file.
        /// </summary>
        PackageFile packageFile = null;

        public TextEditor(Launcher launcher)
        {
            InitializeComponent();
        }

        public TextEditor(Launcher launcher, PackageFile file) : this(launcher)
        {
            this.packageFile = file;
        }

        private void TextEditor_Load(object sender, EventArgs e)
        {
            if (packageFile != null)
            {
                LoadFile(packageFile);
                packageFile = null;
            }
        }

        private void LoadFile(PackageFile file)
        {
            string data = System.Text.Encoding.UTF8.GetString(file.Data);
            if (data.Contains("\n") && !data.Contains("\r"))
            {
                hasCarriageReturn = false;
                data = data.Replace("\n", "\r\n");
            }

            memoEdit1.Text = data;
        }

        private void menu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item == menuFileClose)
            {
                Close();
            }
        }
    }
}