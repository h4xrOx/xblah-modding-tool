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
using static windows_source1ide.Steam;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using DevExpress.XtraBars;
using windows_source1ide.Tools;

namespace windows_source1ide
{
    public partial class ModForm : DevExpress.XtraEditors.XtraForm
    {

        Process modProcess = null;
        Steam sourceSDK;

        public ModForm()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName;
            sourceSDK = new Steam();
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
            modProcess = sourceSDK.RunMod(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
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
            sourceSDK.RunHammer(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonModOpenFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            sourceSDK.OpenModFolder(modsCombo.EditValue.ToString());
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

        private void assetsCopierButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            AssetsCopierForm form = new AssetsCopierForm(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
            form.ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            NewModForm form = new NewModForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                string folder = form.modFolder;
                string title = form.modTitle;
                string game = form.game;
                string gameBranch = form.gameBranch;
                string appId = sourceSDK.GetGameAppId(game).ToString();

                string mod = title + " (" + folder + ")";
                string gamePath = sourceSDK.GetGames()[game];
                string modPath = Steam.GetInstallPath() + "\\steamapps\\sourcemods\\" + folder;

                Directory.CreateDirectory(modPath + "\\bin");
                Directory.CreateDirectory(modPath + "\\resource");
                SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(gamePath + "\\" + gameBranch + "\\gameinfo.txt");

                // Edit basic gameinfo
                gameInfo.setValue("game", title);
                gameInfo.setValue("title", title);
                gameInfo.setValue("title2", "");
                gameInfo.findChild("steamappid").setValue(appId);

                gameInfo.getChild("filesystem").getChild("searchpaths").setValue("gamebin", "|gameinfo_path|bin");

                // Copy binaries
                File.Copy(Application.StartupPath + "\\template_sp\\bin\\client.dll", modPath + "\\bin\\client.dll", true);
                File.Copy(Application.StartupPath + "\\template_sp\\bin\\server.dll", modPath + "\\bin\\server.dll", true);

                File.Copy(Application.StartupPath + "\\template_sp\\resource\\english.txt", modPath + "\\resource\\" + folder + "_english.txt");
                File.Copy(Application.StartupPath + "\\template_sp\\resource\\HL2EP2.ttf", modPath + "\\resource\\HL2EP2.ttf");

                // Create gameinfo
                SourceSDK.KeyValue searchPaths = gameInfo.getChild("filesystem").getChild("searchpaths");
                searchPaths.clearChildren();

                searchPaths.addChild(new SourceSDK.KeyValue("game+mod+mod_write+default_write_path", "|gameinfo_path|."));

                searchPaths.addChild(new SourceSDK.KeyValue("gamebin", "|gameinfo_path|bin"));

                searchPaths.addChild(new SourceSDK.KeyValue("game_lv", "hl2/hl2_lv.vpk"));

                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|ep2/ep2_english.vpk"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|ep2/ep2_pak.vpk"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|episodic/ep1_english.vpk"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|episodic/ep1_pak.vpk"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|sourcetest/sourcetest_pak.vpk"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|hl2/hl2_english.vpk"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|hl2/hl2_pak.vpk"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|hl2/hl2_textures.vpk"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|hl2/hl2_sound_vo_english.vpk"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|hl2/hl2_sound_misc.vpk"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|hl2/hl2_misc.vpk"));

                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|ep2"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|episodic"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|sourcetest"));
                searchPaths.addChild(new SourceSDK.KeyValue("game", "|all_source_engine_paths|hl2"));

                searchPaths.addChild(new SourceSDK.KeyValue("platform", "|all_source_engine_paths|platform/platform_misc.vpk"));
                searchPaths.addChild(new SourceSDK.KeyValue("platform", "|all_source_engine_paths|platform"));

                SourceSDK.KeyValue.writeChunkFile(modPath + "\\gameinfo.txt", gameInfo, false, new UTF8Encoding(false));

                updateModsCombo();
                gamesCombo.EditValue = game;
                modsCombo.EditValue = title + " (" + folder + ")";
            }
        }

        private void importMapButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            ModSelectionDialog dialog = new ModSelectionDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string game = dialog.game;
                string mod = dialog.mod;

                AssetsCopierForm assetsCopierForm = new AssetsCopierForm(game, mod, sourceSDK.GetMods(gamesCombo.EditValue.ToString())[modsCombo.EditValue.ToString()]);
                if (assetsCopierForm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void menuButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            GamemenuForm form = new GamemenuForm(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
            form.ShowDialog();
        }

        private void buttonVPKExplorer_ItemClick(object sender, ItemClickEventArgs e)
        {
            VPKExplorer form = new VPKExplorer(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString());
            form.ShowDialog();
        }
    }
}