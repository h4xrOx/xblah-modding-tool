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
using DevExpress.XtraTreeList.Nodes;
using xblah_modding_lib;

namespace xblah_modding_tool.Modding
{
    public partial class CleanDialog : DevExpress.XtraEditors.XtraForm
    {
        private Launcher launcher;

        public List<string> files;

        public CleanDialog()
        {
            
        }

        public CleanDialog(Launcher launcher)
        {
            this.launcher = launcher;
            ListFiles();

            if (files.Count == 0)
            {
                XtraMessageBox.Show("The Mod folder is already clean.");
                DialogResult = DialogResult.Cancel;
                Close();
            }

            InitializeComponent();

            PopulateTreeList();
        }

        private void CleanDialog_Load(object sender, EventArgs e)
        {
            
        }

        private void ListFiles()
        {
            string modPath = launcher.GetCurrentMod().InstallPath;
            files = new List<string>();
            files.Add("Gamestate.txt");
            files.Add("demoheader.tmp");
            files.Add("ep1_gamestats.dat");
            files.Add("modelsounds.cache");
            files.Add("stats.txt");
            files.Add("voice_ban.dt");
            files.Add("cfg\\config.cfg");
            files.Add("cfg\\server_blacklist.txt");
            files.Add("sound\\sound.cache");
            files.Add("voice_ban.dt");
            files.Add("materialsrc");
            files.Add("downloadlists");
            files.Add("mapsrc");
            files.Add("save");
            files.Add("screenshots");

            files = files.Where(f => (File.Exists(modPath + "\\" + f) || Directory.Exists(modPath + "\\" + f))).ToList();

            if (Directory.Exists(modPath + "\\cfg"))
                foreach (string file in Directory.GetFiles(modPath + "\\cfg"))
                {
                    if (new FileInfo(file).Name.StartsWith("user_") && new FileInfo(file).Name != "user_keys_default.vcfg")
                        files.Add(file.Replace(modPath + "\\", ""));
                }
        }

        private void PopulateTreeList()
        {
            treeList.BeginUnboundLoad();
            treeList.Nodes.Clear();

            foreach (string file in files)
                treeList.AppendNode(new object[] { file }, null);

            treeList.EndUnboundLoad();
            treeList.CheckAll();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string modPath = launcher.GetCurrentMod().InstallPath;

            foreach (TreeListNode node in treeList.GetAllCheckedNodes())
            {
                string f = node["filename"].ToString();
                if (File.Exists(modPath + "\\" + f))
                    File.Delete(modPath + "\\" + f);
                else if (Directory.Exists(modPath + "\\" + f))
                    Directory.Delete(modPath + "\\" + f, true);
                //MessageBox.Show(node["filename"].ToString());
            }

            Close();
        }
    }
}