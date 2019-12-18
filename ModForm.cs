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
using DevExpress.XtraBars;

namespace windows_source1ide
{
    public partial class ModForm : DevExpress.XtraEditors.XtraForm
    {

        Process modProcess = null;
        SourceSDK sourceSDK;

        public ModForm()
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
            modProcess = sourceSDK.runGame(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
            buttonModStart.Enabled = false;
            barButtonRun.Enabled = false;
            buttonModStop.Visibility = BarItemVisibility.Always;
            buttonModRestart.Visibility = BarItemVisibility.Always;
            modProcess.Exited += new EventHandler(modExited);
        }

        private void modExited(object sender, EventArgs e)
        {
            buttonModStart.Enabled = true;
            barButtonRun.Enabled = true;
            buttonModStop.Visibility = BarItemVisibility.Never;
            buttonModRestart.Visibility = BarItemVisibility.Never;
            modProcess = null;
        }

        private void barButtonHammer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            sourceSDK.runHammer(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonModOpenFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            sourceSDK.openModFolder(modsCombo.EditValue.ToString());
        }

        private void barButtonGameinfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GameinfoForm form = new GameinfoForm(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
            form.ShowDialog();
            updateGamesCombo();
            updateModsCombo();
        }

        private void barButtonChapters_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChaptersForm form = new ChaptersForm(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
            form.ShowDialog();
        }

        private void modsCombo_EditValueChanged(object sender, EventArgs e)
        {
            buttonModStart.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
            barMod.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
            barButtonHammer.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
        }

        private void buttonModStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (modProcess != null)
            {
                modProcess.Kill();
                modProcess = null;
            }
        }

        private void buttonModRestart_ItemClick(object sender, ItemClickEventArgs e)
        {
            buttonModStop_ItemClick(null, null);
            buttonModStart_ItemClick(null, null);
        }
    }
}