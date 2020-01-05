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
using DevExpress.XtraTreeList.Nodes;

namespace windows_source1ide
{
    public partial class GamemenuDialog : DevExpress.XtraEditors.XtraForm
    {
        public string item;
        public string caption;
        public bool ingame;
        public bool onlyMulti;
        public bool onlySingle;
        public string command;

        public GamemenuDialog()
        {
            InitializeComponent();
        }

        public GamemenuDialog(GamemenuForm.MenuAction menuAction) : this()
        {
            string item = menuAction.command;
            string command = "";
            if (item.StartsWith("engine "))
            {
                command = item.Substring("engine ".Length);
                item = item.Remove(0, "engine ".Length).Replace("\"", "");
            }

            foreach (TreeListNode node in list.Nodes)
            {
                if (node.GetDisplayText("command") == item)
                {
                    list.SetFocusedNode(node);
                    break;
                }
            }
            textCaption.EditValue = menuAction.label;
            switchIngame.IsOn = menuAction.inGame;
            if (menuAction.onlySingle)
            {
                comboVisible.EditValue = "Single-player only";
            } else if (menuAction.onlyMulti)
            {
                comboVisible.EditValue = "Multi-player only";
            } else
            {
                comboVisible.EditValue = "Both";
            }
            textCommand.EditValue = command;
        }

        private void list_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (list.FocusedNode != null)
            {
                int index = list.GetNodeIndex(list.FocusedNode);

                string command = list.FocusedNode.GetDisplayText("command");
                labelDescription.Text = list.FocusedNode.Tag.ToString();

                if (command == "engine <command>")
                {
                    textCommand.Enabled = true;
                } else
                {
                    textCommand.Enabled = false;
                    textCommand.EditValue = "";
                }
            }
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            item = list.FocusedNode.GetDisplayText("command");
            caption = textCaption.EditValue.ToString();
            ingame = switchIngame.IsOn;
            onlyMulti = (comboVisible.EditValue != null ? (comboVisible.EditValue.ToString() == "Multi-player only") : false);
            onlySingle = (comboVisible.EditValue != null ? (comboVisible.EditValue.ToString() == "Single-player only") : false);
            command = textCommand.EditValue.ToString();

            if (command == "engine <command>")
                command = "engine \"" + command + "\"";
        }
    }
}