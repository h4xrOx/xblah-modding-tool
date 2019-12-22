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
using DevExpress.XtraEditors.Repository;

namespace windows_source1ide
{
    public partial class GamemenuForm : DevExpress.XtraEditors.XtraForm
    {
        string game;
        string mod;
        Steam sourceSDK;

        List<MenuAction> actions;
        class MenuAction
        {


            internal string label = "";
            internal string command = "";
            internal bool inGame = false;
            internal bool onlySingle = false;
            internal bool onlyMulti = false;

            public MenuAction(string label, string command)
            {
                this.label = label;
                this.command = command;
            }
        }

        public GamemenuForm(string game, string mod)
        {
            this.game = game;
            this.mod = mod;

            InitializeComponent();
        }

        private void GamemenuForm_Load(object sender, EventArgs e)
        {
            sourceSDK = new Steam();
            actions = new List<MenuAction>();
            readGameMenu();
            updateList();
        }

        private void createGameMenu()
        {
            actions.Clear();
            actions.Add(new MenuAction("Resume game", "ResumeGame") { inGame = true });
            actions.Add(new MenuAction("New game", "OpenNewGameDialog") { onlySingle = true });
            actions.Add(new MenuAction("Load game", "OpenLoadGameDialog") { onlySingle = true });
            actions.Add(new MenuAction("Save game", "OpenSaveGameDialog") { inGame = true, onlySingle = true });
            actions.Add(new MenuAction("Options", "OpenOptionsDialog") { });
            actions.Add(new MenuAction("Quit", "Quit") { });

            writeGameMenu();
        }

        private void readGameMenu()
        {
            string path = sourceSDK.GetMods(game)[mod] + "\\resource\\gamemenu.res";
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (!File.Exists(path))
            {
                MessageBox.Show("File does not exist. Creating new one");
                createGameMenu();
            }

            SourceSDK.KeyValue gameMenu = SourceSDK.KeyValue.readChunkfile(path);
            actions.Clear();
            foreach (SourceSDK.KeyValue actionKv in gameMenu.getChildrenList())
            {
                string label = actionKv.getValue("label");
                string command = actionKv.getValue("command");
                bool inGame = (actionKv.getValue("onlyingame") == "1");
                bool onlySingle = (actionKv.getValue("nomulti") == "1");
                bool onlyMulti = (actionKv.getValue("nosingle") == "1");

                MenuAction action = new MenuAction(label, command)
                {
                    inGame = inGame,
                    onlySingle = onlySingle,
                    onlyMulti = onlyMulti
                };
                actions.Add(action);
            }

        }

        private void writeGameMenu()
        {
            string path = sourceSDK.GetMods(game)[mod] + "\\resource\\gamemenu.res";
            SourceSDK.KeyValue gameMenu = new SourceSDK.KeyValue("gamemenu");

            for(int i = 0; i < actions.Count; i++)
            {
                MenuAction action = actions[i];
                SourceSDK.KeyValue actionKV = new SourceSDK.KeyValue(i.ToString());
                actionKV.addChild(new SourceSDK.KeyValue("label", action.label));
                actionKV.addChild(new SourceSDK.KeyValue("command", action.command));
                actionKV.addChild(new SourceSDK.KeyValue("ingameorder", i.ToString()));
                if (action.inGame)
                    actionKV.addChild(new SourceSDK.KeyValue("onlyingame", "1"));
                if (action.onlySingle)
                    actionKV.addChild(new SourceSDK.KeyValue("nomulti", "1"));
                if (action.onlyMulti)
                    actionKV.addChild(new SourceSDK.KeyValue("nosingle", "1"));

                gameMenu.addChild(actionKV);
            }

            SourceSDK.KeyValue.writeChunkFile(path, gameMenu, true, new UTF8Encoding(false));
        }

        private void updateList()
        {
            list.BeginUnboundLoad();
            list.Nodes.Clear();
            foreach(MenuAction action in actions)
            {
                list.AppendNode(new object[]
                {
                    action.label,
                    action.command,
                    action.inGame,
                    action.onlySingle,
                    action.onlyMulti
                }, null);
            }
            list.EndUnboundLoad();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            writeGameMenu();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            actions.Add(new MenuAction("",""));
            updateList();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (list.FocusedNode == null)
                return;
            actions.RemoveAt(list.FocusedNode.Id);
            updateList();
        }

        private void list_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            string column = e.Column.FieldName;
            int row = e.Node.Id;
            string value = e.Value.ToString();

            switch(column)
            {
                case "label":
                    actions[row].label = value;
                    break;
                case "command":
                    actions[row].command = value;
                    break;

            }
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            string column = list.FocusedColumn.FieldName ;
            int row = list.FocusedNode.Id;
            bool value = !(bool)list.FocusedNode.GetValue(column);

            switch (column)
            {
                case "onlyInGame":
                    actions[row].inGame = value;
                    break;
                case "onlySingle":
                    actions[row].onlySingle = value;
                    break;
                case "onlyMulti":
                    actions[row].onlyMulti = value;
                    break;

            }
        }
    }
}