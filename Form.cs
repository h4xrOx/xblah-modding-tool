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
using static windows_source1ide.SourceSDK;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace windows_source1ide
{
    public partial class Form : DevExpress.XtraEditors.XtraForm
    {
        SourceSDK sourceSDK;
        public Form()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName;
            sourceSDK = new SourceSDK();
            updateGamesCombo();
        }

        private void updateGamesCombo()
        {
            string currentGame = (gamesCombo.EditValue != null ? gamesCombo.EditValue.ToString() : "");
            repositoryGamesCombo.Items.Clear();
            foreach (KeyValuePair<string, string> item in sourceSDK.GetGames())
            {
                repositoryGamesCombo.Items.Add(item.Key);
            }

            if (repositoryGamesCombo.Items.Count > 0 && repositoryGamesCombo.Items.Contains(currentGame))
                gamesCombo.EditValue = currentGame;
            else if (repositoryGamesCombo.Items.Count > 0)
                gamesCombo.EditValue = repositoryGamesCombo.Items[0];
            else
                gamesCombo.EditValue = "";
        }

        private void updateModsCombo()
        {
            string currentMod = (modsCombo.EditValue != null ? modsCombo.EditValue.ToString() : "");
            repositoryModsCombo.Items.Clear();
            foreach (KeyValuePair<string, string> item in sourceSDK.GetMods(gamesCombo.EditValue.ToString()))
            {
                repositoryModsCombo.Items.Add(item.Key);
            }

            if (repositoryModsCombo.Items.Count > 0 && repositoryModsCombo.Items.Contains(currentMod))
                modsCombo.EditValue = currentMod;
            else if (repositoryModsCombo.Items.Count > 0)
                modsCombo.EditValue = repositoryModsCombo.Items[0];
            else
                modsCombo.EditValue = "";
        }

        private void gamesCombo_EditValueChanged(object sender, EventArgs e)
        {
            updateModsCombo();
        }

        private void buttonModStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            sourceSDK.runGame(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
        }

        private void buttonHammerStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            sourceSDK.runHammer(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GameinfoForm form = new GameinfoForm(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
            form.ShowDialog();
            updateGamesCombo();
            updateModsCombo();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            sourceSDK.openModFolder(modsCombo.EditValue.ToString());
        }
    }
}