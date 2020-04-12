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
using System.IO;

namespace SourceModdingTool
{
    public partial class SearchPathsForm : DevExpress.XtraEditors.XtraForm
    {

        KeyValue gameinfo;

        List<String[]> searchPaths;
        Steam sourceSDK;
        public SearchPathsForm(Steam sourceSDK)
        {
            this.sourceSDK = sourceSDK;
            InitializeComponent();
        }

        private void SearchPathsForm_Load(object sender, EventArgs e)
        {
            string modPath = sourceSDK.GetModPath();

            string gameinfoPath = modPath + "\\gameinfo.txt";

            gameinfo = SourceSDK.KeyValue.readChunkfile(gameinfoPath);

            comboGames.Properties.Items.Clear();
            string appID = gameinfo.getChildByKey("filesystem").getValue("steamappid");
            foreach (KeyValuePair<string, string> item in sourceSDK.GetGamesList())
            {
                comboGames.Properties.Items.Add(item.Key);
                string gameAppID = sourceSDK.GetGameAppId(item.Key).ToString();
                if (appID == gameAppID.ToString())
                {
                    comboGames.EditValue = item.Key;
                }
            }

            searchList.BeginUnboundLoad();
            searchList.Nodes.Clear();
            searchPaths = new List<string[]>();
            foreach (SourceSDK.KeyValue searchPath in gameinfo.getChildByKey("filesystem")
                .getChildByKey("searchpaths")
                .getChildren())
            {
                searchPaths.Add(new string[] { searchPath.getKey(), searchPath.getValue() });
                searchList.AppendNode(new object[] { searchPath.getKey(), searchPath.getValue() }, null);
            }
            searchList.EndUnboundLoad();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string modPath = sourceSDK.GetModPath();

            int appID = sourceSDK.GetGameAppId(comboGames.EditValue.ToString());
            gameinfo.getChildByKey("filesystem").setValue("steamappid", appID.ToString());

            SourceSDK.KeyValue searchPathsKV = gameinfo.getChildByKey("filesystem").getChildByKey("searchpaths");
            searchPathsKV.clearChildren();
            foreach (String[] searchPath in searchPaths)
            {
                searchPathsKV.addChild(new SourceSDK.KeyValue(searchPath[0], searchPath[1]));
            }

            string path = modPath + "\\gameinfo.txt";

            SourceSDK.KeyValue.writeChunkFile(path, gameinfo, false, new UTF8Encoding(false));

            Close();
        }

        private void buttonAddVPK_Click(object sender, EventArgs e)
        {
            string gamePath = sourceSDK.GetGamePath() + "\\";
            vpkDialog.InitialDirectory = gamePath;
            if (vpkDialog.ShowDialog() == DialogResult.OK)
            {
                Uri path1 = new Uri(gamePath);
                Uri path2 = new Uri(vpkDialog.FileName);
                Uri diff = path1.MakeRelativeUri(path2);

                string path = diff.OriginalString;
                path.Replace("\\", "/");
                path = "|all_source_engine_paths|" + path;
                path = path.Replace("_dir.vpk", ".vpk");
                path = path.Replace("%20", " ");
                searchPaths.Add(new string[] { "game", path });
                searchList.BeginUnboundLoad();
                searchList.AppendNode(new object[] { "game", path }, null);
                searchList.EndUnboundLoad();
            }
        }

        private void buttonAddDirectory_Click(object sender, EventArgs e)
        {
            string gamePath = sourceSDK.GetGamePath() + "\\";
            Directory.CreateDirectory(gamePath);
            searchDirDialog.SelectedPath = gamePath;
            if (searchDirDialog.ShowDialog() == DialogResult.OK)
            {
                Uri path1 = new Uri(gamePath);
                Uri path2 = new Uri(searchDirDialog.SelectedPath);
                Uri diff = path1.MakeRelativeUri(path2);

                string path = diff.OriginalString;
                path.Replace("\\", "/");
                path = "|all_source_engine_paths|" + path;
                path = path.Replace("%20", " ");
                searchPaths.Add(new string[] { "game", path });
                searchList.BeginUnboundLoad();
                searchList.AppendNode(new object[] { "game", path }, null);
                searchList.EndUnboundLoad();
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (searchList.FocusedNode != null)
            {
                int index = searchList.GetNodeIndex(searchList.FocusedNode);
                searchList.SetNodeIndex(searchList.FocusedNode, index - 1);

                buttonUp.Enabled = (index - 1 > 0);
                buttonDown.Enabled = (index - 1 < searchPaths.Count - 1);
                buttonRemove.Enabled = true;

                String[] item = searchPaths[index];
                searchPaths.RemoveAt(index);
                searchPaths.Insert(index - 1, item);
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (searchList.FocusedNode != null)
            {
                int index = searchList.GetNodeIndex(searchList.FocusedNode);
                searchList.SetNodeIndex(searchList.FocusedNode, index + 1);

                buttonUp.Enabled = (index + 1 > 0);
                buttonDown.Enabled = (index + 1 < searchPaths.Count - 1);
                buttonRemove.Enabled = true;

                String[] item = searchPaths[index];
                searchPaths.RemoveAt(index);
                searchPaths.Insert(index + 1, item);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (searchList.FocusedNode != null)
            {
                int index = searchList.GetNodeIndex(searchList.FocusedNode);
                searchPaths.RemoveAt(index);
                searchList.Nodes.RemoveAt(index);

                if (searchList.FocusedNode != null)
                {
                    index = searchList.GetNodeIndex(searchList.FocusedNode);

                    buttonUp.Enabled = (index > 0);
                    buttonDown.Enabled = (index < searchPaths.Count - 1);
                    buttonRemove.Enabled = true;
                }
                else
                {
                    buttonUp.Enabled = false;
                    buttonDown.Enabled = false;
                    buttonRemove.Enabled = false;
                }
            }
        }

        private void searchList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (searchList.FocusedNode != null)
            {
                int index = searchList.GetNodeIndex(searchList.FocusedNode);

                buttonUp.Enabled = (index > 0);
                buttonDown.Enabled = (index < searchPaths.Count - 1);
                buttonRemove.Enabled = true;
            }
            else
            {
                buttonUp.Enabled = false;
                buttonDown.Enabled = false;
                buttonRemove.Enabled = false;
            }
        }
    }
}