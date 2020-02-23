using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceModdingTool
{
    public partial class ModSelectionDialog : DevExpress.XtraEditors.XtraForm
    {
        Steam sourceSDK;

        public string game = string.Empty;
        public string mod = string.Empty;

        public ModSelectionDialog() { InitializeComponent(); }

        private void gamesCombo_TextChanged(object sender, EventArgs e)
        {
            sourceSDK.setCurrentGame(gamesCombo.EditValue.ToString());
            game = gamesCombo.EditValue.ToString();
            updateModsCombo();
        }

        private void modsCombo_TextChanged(object sender, EventArgs e) { mod = modsCombo.EditValue.ToString(); }

        private void ModSelectionDialog_Load(object sender, EventArgs e)
        {
            sourceSDK = new Steam();
            updateGamesCombo();
        }

        private void updateGamesCombo()
        {
            string currentGame = (gamesCombo.EditValue != null ? gamesCombo.EditValue.ToString() : string.Empty);
            gamesCombo.Properties.Items.Clear();
            foreach(KeyValuePair<string, string> item in sourceSDK.GetGamesList())
                gamesCombo.Properties.Items.Add(item.Key);

            if(gamesCombo.Properties.Items.Count > 0 && gamesCombo.Properties.Items.Contains(currentGame))
                gamesCombo.EditValue = currentGame;
            else if(gamesCombo.Properties.Items.Count > 0)
                gamesCombo.EditValue = gamesCombo.Properties.Items[0];
            else
            {
                gamesCombo.EditValue = string.Empty;
                XtraMessageBox.Show("No Source games were found. Check if everything is all set at Steam and restart this tool.");
                Close();
            }
        }

        private void updateModsCombo()
        {
            string currentMod = (modsCombo.EditValue != null ? modsCombo.EditValue.ToString() : string.Empty);
            modsCombo.Properties.Items.Clear();

            string currentGame = (gamesCombo.EditValue != null ? gamesCombo.EditValue.ToString() : string.Empty);
            if(currentGame == string.Empty)
                return;

            Dictionary<string, string> dictionary = sourceSDK.GetModsList(currentGame);
            foreach (KeyValuePair<string, string> item in dictionary)
                modsCombo.Properties.Items.Add(item.Key);

            if(modsCombo.Properties.Items.Count > 0 && modsCombo.Properties.Items.Contains(currentMod))
                modsCombo.EditValue = currentMod;
            else if(modsCombo.Properties.Items.Count > 0)
                modsCombo.EditValue = modsCombo.Properties.Items[0];
            else
            {
                modsCombo.EditValue = string.Empty;
            }
        }
    }
}