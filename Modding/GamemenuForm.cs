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

namespace SourceModdingTool
{
    public partial class GamemenuForm : DevExpress.XtraEditors.XtraForm
    {
        Steam sourceSDK;
        string gamePath = "";
        string modPath = "";

        List<MenuAction> actions;
        public class MenuAction
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

        public GamemenuForm(Steam sourceSDK)
        {
            InitializeComponent();

            this.sourceSDK = sourceSDK;
        }

        private void GamemenuForm_Load(object sender, EventArgs e)
        {
            gamePath = sourceSDK.GetGamePath();
            modPath = sourceSDK.GetModPath();
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
            string path = modPath + "\\resource\\gamemenu.res";
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (!File.Exists(path))
            {
                MessageBox.Show("File does not exist. Creating new one");
                createGameMenu();
            }

            SourceSDK.KeyValue gameMenu = SourceSDK.KeyValue.readChunkfile(path);
            actions.Clear();
            foreach (SourceSDK.KeyValue actionKv in gameMenu.getChildren())
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
            string path = modPath + "\\resource\\gamemenu.res";
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
            GamemenuDialog dialog = new GamemenuDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                MenuAction menuAction = new MenuAction(dialog.caption, dialog.item);
                menuAction.inGame = dialog.ingame;
                menuAction.onlyMulti = dialog.onlyMulti;
                menuAction.onlySingle = dialog.onlySingle;
                actions.Add(menuAction);
                updateList();
            }
            //actions.Add(new MenuAction("",""));
            //updateList();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (list.FocusedNode != null)
            {
                int index = list.GetNodeIndex(list.FocusedNode);
                actions.RemoveAt(index);
                list.Nodes.RemoveAt(index);
                    
                if (list.FocusedNode != null)
                {
                    index = list.GetNodeIndex(list.FocusedNode);

                    buttonUp.Enabled = (index > 0);
                    buttonDown.Enabled = (index < actions.Count - 1);
                    removeButton.Enabled = true;
                    buttonEdit.Enabled = true;
                }
                else
                {
                    buttonUp.Enabled = false;
                    buttonDown.Enabled = false;
                    removeButton.Enabled = false;
                    buttonEdit.Enabled = false;
                }
            }
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

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (list.FocusedNode != null)
            {
                int index = list.GetNodeIndex(list.FocusedNode);
                list.SetNodeIndex(list.FocusedNode, index - 1);

                buttonUp.Enabled = (index - 1 > 0);
                buttonDown.Enabled = (index - 1 < actions.Count - 1);
                removeButton.Enabled = true;
                buttonEdit.Enabled = true;

                MenuAction item = actions[index];
                actions.RemoveAt(index);
                actions.Insert(index - 1, item);
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (list.FocusedNode != null)
            {
                int index = list.GetNodeIndex(list.FocusedNode);
                list.SetNodeIndex(list.FocusedNode, index + 1);

                buttonUp.Enabled = (index + 1 > 0);
                buttonDown.Enabled = (index + 1 < actions.Count - 1);
                removeButton.Enabled = true;
                buttonEdit.Enabled = true;

                MenuAction item = actions[index];
                actions.RemoveAt(index);
                actions.Insert(index + 1, item);
            }
        }

        private void list_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (list.FocusedNode != null)
            {
                int index = list.GetNodeIndex(list.FocusedNode);

                buttonUp.Enabled = (index > 0);
                buttonDown.Enabled = (index < actions.Count - 1);
                removeButton.Enabled = true;
                buttonEdit.Enabled = true;
            }
            else
            {
                buttonUp.Enabled = false;
                buttonDown.Enabled = false;
                removeButton.Enabled = false;
                buttonEdit.Enabled = false;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (list.FocusedNode != null)
            {
                int index = list.GetNodeIndex(list.FocusedNode);
                GamemenuDialog dialog = new GamemenuDialog(actions[index]);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    actions[index].label = dialog.caption;
                    actions[index].command = dialog.item;
                    actions[index].inGame = dialog.ingame;
                    actions[index].onlyMulti = dialog.onlyMulti;
                    actions[index].onlySingle = dialog.onlySingle;
                    updateList();
                }
            }
        }
    }
}