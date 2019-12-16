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
            repositoryGamesCombo.Items.Clear();
            foreach (KeyValuePair<string, string> item in sourceSDK.GetGames())
            {
                repositoryGamesCombo.Items.Add(item.Key);
            }

            if (repositoryGamesCombo.Items.Count > 0)
                gamesCombo.EditValue = repositoryGamesCombo.Items[0];
            else
                gamesCombo.EditValue = "";
        }

        private void updateModsCombo()
        {
            repositoryModsCombo.Items.Clear();
            foreach (KeyValuePair<string, string> item in sourceSDK.GetMods(gamesCombo.EditValue.ToString()))
            {
                repositoryModsCombo.Items.Add(item.Key);
            }

            if (repositoryModsCombo.Items.Count > 0)
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

        private void button1_Click(object sender, EventArgs e)
        {
            KeyVal<string, string> gameInfo = SourceSDK.readChunkfile("G:\\SteamLibrary\\steamapps\\sourcemods\\mapbase_episodic_template\\gameinfo.txt");
            SourceSDK.writeChunkFile("G:\\SteamLibrary\\steamapps\\sourcemods\\mapbase_episodic_template\\gameinfo.txt", "GameInfo", gameInfo);
        }
    }
}