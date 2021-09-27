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
using SourceSDK;

namespace source_modding_tool
{
    public partial class SearchPathsForm : DevExpress.XtraEditors.XtraForm
    {
        KeyValue gameinfo;

        List<String[]> searchPaths;
        Launcher launcher;
        public SearchPathsForm(Launcher launcher)
        {
            this.launcher = launcher;
            InitializeComponent();
        }

        private void SearchPathsForm_Load(object sender, EventArgs e)
        {
            string modPath = launcher.GetCurrentMod().InstallPath;

            string gameinfoPath = modPath + "\\gameinfo.txt";

            gameinfo = SourceSDK.KeyValue.readChunkfile(gameinfoPath);

            comboGames.Properties.Items.Clear();
            string appID = gameinfo.getChildByKey("filesystem").getValue("steamappid");
            foreach (KeyValuePair<string, Game> item in launcher.GetGamesList())
            {
                comboGames.Properties.Items.Add(item.Key);
                Game game = launcher.GetGamesList()[item.Key];
                string gameAppID = game.GetAppId().ToString();
                if (appID == gameAppID.ToString())
                {
                    comboGames.EditValue = item.Key;
                }
            }

            searchPaths = new List<string[]>();
            foreach (SourceSDK.KeyValue searchPath in gameinfo.getChildByKey("filesystem")
                .getChildByKey("searchpaths")
                .getChildren())
            {
                string key = searchPath.getKey();
                if (key == "platform")
                    continue;

                searchPaths.Add(new string[] { key, searchPath.getValue() });
            }

            UpdateList();
        }

        private void UpdateList()
        {
            searchList.BeginUnboundLoad();
            searchList.Nodes.Clear();
            foreach (string[] searchPath in searchPaths)
            {
                string key = searchPath[0];
                string value = searchPath[1];

                value = value.Replace("|all_source_engine_paths|", "|game_path|");
                value = value.Replace("|gameinfo_path|", "|mod_path|");

                searchList.AppendNode(new object[] { key, value }, null);
            }
            searchList.EndUnboundLoad();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string modPath = launcher.GetCurrentMod().InstallPath;

            Game game = launcher.GetGamesList()[comboGames.EditValue.ToString()];
            int appID = game.GetAppId();
            gameinfo.getChildByKey("filesystem").setValue("steamappid", appID.ToString());

            searchPaths.Add(new string[] { "platform", "|all_source_engine_paths|platform/platform_misc.vpk" });
            searchPaths.Add(new string[] { "platform", "|all_source_engine_paths|platform" });

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
            string gamePath = launcher.GetCurrentGame().installPath + "\\";
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
            }

            UpdateList();
        }

        private void buttonAddDirectory_Click(object sender, EventArgs e)
        {
            string gamePath = launcher.GetCurrentGame().installPath + "\\";
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
            }

            UpdateList();
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