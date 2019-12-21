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

namespace windows_source1ide
{
    public partial class ModSelectionDialog : DevExpress.XtraEditors.XtraForm
    {
        Steam sourceSDK;

        public string game = "";
        public string mod = "";

        public ModSelectionDialog()
        {
            InitializeComponent();
        }

        private void ModSelectionDialog_Load(object sender, EventArgs e)
        {
            sourceSDK = new Steam();
            updateGamesCombo();
        }

        private void updateGamesCombo()
        {
            string currentGame = (gamesCombo.EditValue != null ? gamesCombo.EditValue.ToString() : "");
            gamesCombo.Properties.Items.Clear();
            foreach (KeyValuePair<string, string> item in sourceSDK.GetGames())
            {
                gamesCombo.Properties.Items.Add(item.Key);
            }

            if (gamesCombo.Properties.Items.Count > 0 && gamesCombo.Properties.Items.Contains(currentGame))
                gamesCombo.EditValue = currentGame;
            else if (gamesCombo.Properties.Items.Count > 0)
                gamesCombo.EditValue = gamesCombo.Properties.Items[0];
            else
                gamesCombo.EditValue = "";
        }

        private void updateModsCombo()
        {
            string currentMod = (modsCombo.EditValue != null ? modsCombo.EditValue.ToString() : "");
            modsCombo.Properties.Items.Clear();
            foreach (KeyValuePair<string, string> item in sourceSDK.GetMods(gamesCombo.EditValue.ToString()))
            {
                modsCombo.Properties.Items.Add(item.Key);
            }

            if (modsCombo.Properties.Items.Count > 0 && modsCombo.Properties.Items.Contains(currentMod))
                modsCombo.EditValue = currentMod;
            else if (modsCombo.Properties.Items.Count > 0)
                modsCombo.EditValue = modsCombo.Properties.Items[0];
            else
                modsCombo.EditValue = "";
        }

        private void gamesCombo_TextChanged(object sender, EventArgs e)
        {
            game = gamesCombo.EditValue.ToString();
            updateModsCombo();
        }

        private void modsCombo_TextChanged(object sender, EventArgs e)
        {
            mod = modsCombo.EditValue.ToString();
        }
    }
}