using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using source_modding_tool.SourceSDK;
using source_modding_tool.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace source_modding_tool
{
    public partial class HudEditorForm : DevExpress.XtraEditors.XtraForm
    {
        string gamePath = string.Empty;
        string modPath = string.Empty;
        Launcher launcher;

        KeyValue root;
        List<HudItem> items;

        Instance instance;
        bool isPreviewing = false;

        public HudEditorForm(Launcher launcher)
        {
            InitializeComponent();

            this.launcher = launcher;
        }

        private void loadFile(string fileName)
        {
            root = KeyValue.readChunkfile(fileName, true);
            items = new List<HudItem>();

            foreach(KeyValue kv in root.getChildren())
            {
                HudItem hudItem = new HudItem(kv);
                items.Add(hudItem);
            }

            populateItemTree();
            

        }

        private void populateItemTree()
        {
            itemTree.BeginUnboundLoad();
            itemTree.Nodes.Clear();

            foreach(HudItem kv in items)
            {
                TreeListNode node = itemTree.AppendNode(new object[] { kv.keyValue.getKey() }, null);
                node.Checked = true;
                node.Tag = kv;
            }

            itemTree.EndUnboundLoad();

            propertyGridControl1.SelectedObject = itemTree.FocusedNode.Tag;
        }

        private void populatePropertyGrid()
        {
            //propertyGridControl1.SelectedObject = items[0];
        }

        private void itemTree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node.Tag == null)
                return;
            propertyGridControl1.SelectedObject = e.Node.Tag;
        }

        private void ClientSchemeForm_Load(object sender, EventArgs e)
        {
            gamePath = launcher.GetCurrentGame().installPath;
            modPath = launcher.GetCurrentMod().installPath;

            string path = modPath + "\\scripts\\hudlayout.res";
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (!File.Exists(path))
            {
                XtraMessageBox.Show("hudlayout.res not found. Creating a new one.");
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "Templates/Source SDK Base 2013 Singleplayer/hl2/scripts/hudlayout.res", path);
            }

            if (!File.Exists(path))
            { 
                MessageBox.Show("File does not exist.");
                Close();
            }

            loadFile(path);
            Application.DoEvents();
            startPreview();
        }

        void Save()
        {
            KeyValue.writeChunkFile(modPath + "\\scripts\\hudlayout.res", root, false, new UTF8Encoding(false));
        }

        private void startPreview()
        {
            instance = new Instance(launcher, panelControl1);
            instance.Start(new RunPreset(RunMode.WINDOWED), "-nomouse +map hud_preview");
            this.ActiveControl = null;

            isPreviewing = true;
        }

        private void updatePreview()
        {
            if (isPreviewing)
            {
                Save();
                instance.Command("+hud_reloadscheme +net_graph 0");
            }
        }

        private void ClientSchemeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (instance != null && instance.modProcess != null && !instance.modProcess.HasExited)
            {
                instance.modProcess.Kill();
            }
        }

        private void propertyGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            ((HudItem)propertyGridControl1.SelectedObject).getKeyValue();
            updatePreview();
        }

        private void panelControl2_SizeChanged(object sender, EventArgs e)
        {
            //panelControl1.Location = new Point(panelControl2.Size.Width / 2 - panelControl1.Size.Width / 2, panelControl2.Size.Height / 2 - panelControl1.Size.Height / 2);
        }
    }
}