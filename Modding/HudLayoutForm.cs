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
using SourceModdingTool.SourceSDK;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.Diagram.Core;
using DevExpress.XtraDiagram;
using System.IO;
using DevExpress.Utils;

namespace SourceModdingTool
{
    public partial class HudEditorForm : DevExpress.XtraEditors.XtraForm
    {
        string gamePath = string.Empty;
        string modPath = string.Empty;
        Steam sourceSDK;

        KeyValue root;
        List<HudItem> items;

        Game game;
        bool isPreviewing = false;

        public HudEditorForm(Steam sourceSDK)
        {
            InitializeComponent();

            this.sourceSDK = sourceSDK;
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
            gamePath = sourceSDK.GetGamePath();
            modPath = sourceSDK.GetModPath();

            string path = modPath + "\\scripts\\hudlayout.res";
            Directory.CreateDirectory(Path.GetDirectoryName(path));

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
            game = new Game(sourceSDK, panelControl1);
            game.Start("-nomouse +map hud_preview");
            this.ActiveControl = null;

            isPreviewing = true;
        }

        private void updatePreview()
        {
            if (isPreviewing)
            {
                Save();
                game.Command("+hud_reloadscheme +net_graph 0");
            }
        }

        private void ClientSchemeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (game != null && game.modProcess != null)
            {
                game.modProcess.Kill();
            }
        }

        private void propertyGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            ((HudItem)propertyGridControl1.SelectedObject).getKeyValue();
            updatePreview();
        }

        private void panelControl2_SizeChanged(object sender, EventArgs e)
        {
            panelControl1.Location = new Point(panelControl2.Size.Width / 2 - panelControl1.Size.Width / 2, panelControl2.Size.Height / 2 - panelControl1.Size.Height / 2);
        }
    }
}