using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Linq;

namespace source_modding_tool
{
    public partial class GamemenuDialog : DevExpress.XtraEditors.XtraForm
    {
        public string caption;
        public string command;
        public bool ingame;
        public string item;
        public bool onlyMulti;
        public bool onlySingle;

        public GamemenuDialog() { InitializeComponent(); }

        public GamemenuDialog(GamemenuForm.MenuAction menuAction) : this()
        {
            string item = menuAction.command;
            string command = string.Empty;

            if (item.StartsWith("engine "))
            {
                command = item.Substring("engine ".Length);
                item = "engine <command>";
            }

            foreach(TreeListNode node in list.Nodes)
            {
                if(node.GetDisplayText("command") == item)
                {
                    list.SetFocusedNode(node);
                    break;
                }
            }
            textCaption.EditValue = menuAction.label;
            switchIngame.IsOn = menuAction.inGame;
            if(menuAction.onlySingle)
            {
                comboVisible.EditValue = "Single-player only";
            } else if(menuAction.onlyMulti)
            {
                comboVisible.EditValue = "Multi-player only";
            } else
            {
                comboVisible.EditValue = "Both";
            }
            textCommand.EditValue = command;
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            item = list.FocusedNode.GetDisplayText("command");
            caption = textCaption.EditValue.ToString();
            ingame = switchIngame.IsOn;
            onlyMulti = (comboVisible.EditValue != null
                ? (comboVisible.EditValue.ToString() == "Multi-player only")
                : false);
            onlySingle = (comboVisible.EditValue != null
                ? (comboVisible.EditValue.ToString() == "Single-player only")
                : false);
            command = textCommand.EditValue.ToString();

            if(item.StartsWith("engine "))
                item = "engine " + command + "";
        }

        private void list_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if(list.FocusedNode != null)
            {
                int index = list.GetNodeIndex(list.FocusedNode);

                string command = list.FocusedNode.GetDisplayText("command");
                labelDescription.Text = list.FocusedNode.Tag.ToString();

                if(command == "engine <command>")
                {
                    textCommand.Enabled = true;
                } else
                {
                    textCommand.Enabled = false;
                    textCommand.EditValue = string.Empty;
                }
            }
        }
    }
}