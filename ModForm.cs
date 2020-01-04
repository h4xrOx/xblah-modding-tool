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
using windows_source1ide.SourceSDK;
using windows_source1ide.Particles;
using System.Threading;

namespace windows_source1ide
{
    public partial class ModForm : DevExpress.XtraEditors.XtraForm
    {
        Game game = null;
        Steam sourceSDK;

        public ModForm()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName;
            string currentGame = Properties.Settings.Default.currentGame;
            string currentMod = Properties.Settings.Default.currentMod;

            sourceSDK = new Steam();
            updateGamesCombo();

            gamesCombo.EditValue = currentGame;
            modsCombo.EditValue = currentMod;
        }

        private void updateGamesCombo()
        {
            string currentGame = (gamesCombo.EditValue != null ? gamesCombo.EditValue.ToString() : "");
            repositoryGamesCombo.Items.Clear();
            foreach (KeyValuePair<string, string> item in sourceSDK.GetGamesList())
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
            foreach (KeyValuePair<string, string> item in sourceSDK.GetModsList(gamesCombo.EditValue.ToString()))
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
            sourceSDK.setCurrentGame((gamesCombo.EditValue != null ? gamesCombo.EditValue.ToString() : ""));
            updateModsCombo();
            Properties.Settings.Default.currentGame = gamesCombo.EditValue.ToString();
            Properties.Settings.Default.Save();
        }

        private void barButtonHammer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            sourceSDK.RunHammer(Application.StartupPath);
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
            GameinfoForm form = new GameinfoForm(sourceSDK);
            form.ShowDialog();
            updateGamesCombo();
            updateModsCombo();
        }

        private void barButtonChapters_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChaptersForm form = new ChaptersForm(sourceSDK);
            form.ShowDialog();
        }

        private void modsCombo_EditValueChanged(object sender, EventArgs e)
        {
            sourceSDK.setCurrentMod(modsCombo.EditValue.ToString());
            Properties.Settings.Default.currentMod = modsCombo.EditValue.ToString();
            Properties.Settings.Default.Save();

            buttonModStart.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
            barMod.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
            barButtonHammer.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
            buttonHLMV.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
            buttonFaceposer.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
            assetsCopierButton.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
            buttonVPKExplorer.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
            barParticles.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
            buttonHLMV.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
            buttonHammerPropper.Enabled = (modsCombo.EditValue != null && modsCombo.EditValue.ToString() != "");
        }

        private void buttonModStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (game.modProcess != null)
                game.Stop();
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
                string gamePath = sourceSDK.GetGamePath(game);
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

                File.Copy(Application.StartupPath + "\\template_sp\\resource\\template_sp_english.txt", modPath + "\\resource\\" + folder + "_english.txt");
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

                AssetsCopierForm assetsCopierForm = new AssetsCopierForm(game, mod, sourceSDK.GetModPath(gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString()));
                if (assetsCopierForm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void menuButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            GamemenuForm form = new GamemenuForm(sourceSDK);
            form.ShowDialog();
        }

        private void buttonVPKExplorer_ItemClick(object sender, ItemClickEventArgs e)
        {
            VPKExplorer form = new VPKExplorer(sourceSDK);
            form.ShowDialog();
        }

        private void barButtonClean_ItemClick(object sender, ItemClickEventArgs e)
        {
            string modPath = sourceSDK.GetModPath(gamesCombo.EditValue.ToString(),modsCombo.EditValue.ToString());

            if (File.Exists(modPath + "\\Gamestate.txt"))
                File.Delete(modPath + "\\Gamestate.txt");
            if (File.Exists(modPath + "\\demoheader.tmp"))
                File.Delete(modPath + "\\demoheader.tmp");
            if (File.Exists(modPath + "\\ep1_gamestats.dat"))
                File.Delete(modPath + "\\ep1_gamestats.dat");
            if (File.Exists(modPath + "\\modelsounds.cache"))
                File.Delete(modPath + "\\modelsounds.cache");
            if (File.Exists(modPath + "\\stats.txt"))
                File.Delete(modPath + "\\stats.txt");
            if (File.Exists(modPath + "\\voice_ban.dt"))
                File.Delete(modPath + "\\voice_ban.dt");
            if (File.Exists(modPath + "\\cfg\\config.cfg"))
                File.Delete(modPath + "\\cfg\\config.cfg");
            if (File.Exists(modPath + "\\cfg\\server_blacklist.txt"))
                File.Delete(modPath + "\\cfg\\server_blacklist.txt");
            if (File.Exists(modPath + "\\sound\\sound.cache"))
                File.Delete(modPath + "\\sound\\sound.cache");
            if (File.Exists(modPath + "\\voice_ban.dt"))
                File.Delete(modPath + "\\voice_ban.dt");
            if (Directory.Exists(modPath + "\\materialsrc"))
                Directory.Delete(modPath + "\\materialsrc", true);
            if (Directory.Exists(modPath + "\\downloadlists"))
                Directory.Delete(modPath + "\\downloadlists", true);
            if (Directory.Exists(modPath + "\\mapsrc"))
                Directory.Delete(modPath + "\\mapsrc", true);
            if (Directory.Exists(modPath + "\\save"))
                Directory.Delete(modPath + "\\save", true);
            if (Directory.Exists(modPath + "\\screenshots"))
                Directory.Delete(modPath + "\\screenshots",true);
        }

        private void buttonCrafty_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("Tools\\Crafty\\Crafty.exe");
        }

        private void buttonVTFEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("Tools\\VTFEdit\\VTFEdit.exe");
        }

        private void buttonBatchCompiler_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("Tools\\BatchCompiler\\Batch Compiler.exe");
        }

        private void buttonCrowbar_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("Tools\\Crowbar\\Crowbar.exe");
        }

        private void buttonHLMV_ItemClick(object sender, ItemClickEventArgs e)
        {
            string gamePath = sourceSDK.GetGamePath(gamesCombo.EditValue.ToString());

            string toolPath = gamePath + "\\bin\\hlmv.exe";
            Process.Start(toolPath);
        }

        private void barButtonFaceposer_ItemClick(object sender, ItemClickEventArgs e)
        {
            string gamePath = sourceSDK.GetGamePath(gamesCombo.EditValue.ToString());

            string toolPath = gamePath + "\\bin\\hlfaceposer.exe";
            Process.Start(toolPath);
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("Tools\\TerrainGenerator\\TerrainGenerator.exe");
        }

        private void buttonIngameTools_ItemClick(object sender, ItemClickEventArgs e)
        {
            game = new Game(sourceSDK, panel1);
            game.StartTools();

            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            modProcessUpdater.Enabled = true;

            buttonModStart.Enabled = false;
            barButtonRun.Enabled = false;
            buttonModStop.Visibility = BarItemVisibility.Always;
            game.modProcess.Exited += new EventHandler(modExited);
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            //PCF.read("particles/aux_fx.pcf", gamesCombo.EditValue.ToString(), modsCombo.EditValue.ToString(), sourceSDK);

            string gamePath = sourceSDK.GetGamePath();
            string modPath = sourceSDK.GetModPath();


            string hammerPath = gamePath + "\\bin\\hammer.exe";
            Debug.Write("Hammer: " + hammerPath);

            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = hammerPath;
            ffmpeg.StartInfo.Arguments = "";
            ffmpeg.Start();

            ffmpeg.WaitForInputIdle();
            Thread.Sleep(100);

            // Set the panel control as the application's parent
            Program.SetParent(ffmpeg.MainWindowHandle, this.panel1.Handle);
            Program.SendMessage(ffmpeg.MainWindowHandle, 274, 61488, 0);
        }

        private void buttonVMFtoMDL_ItemClick(object sender, ItemClickEventArgs e)
        {
            VMFtoMDL form = new VMFtoMDL(sourceSDK);
            form.ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            ParticleManifestForm form = new ParticleManifestForm(sourceSDK);
            form.ShowDialog();
        }

        private void buttonHammerPropper_ItemClick(object sender, ItemClickEventArgs e)
        {
            sourceSDK.RunPropperHammer();
        }

        private void buttonOpenPrefabsFolder_ItemClick(object sender, ItemClickEventArgs e)
        {
            string gamePath = sourceSDK.GetGamePath();
            Process.Start(gamePath + "\\bin\\Prefabs");
        }

        private void buttonModStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            game = new Game(sourceSDK, panel1);
            game.Start();
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            modProcessUpdater.Enabled = true;

            buttonModStart.Enabled = false;
            barButtonRun.Enabled = false;
            buttonModStop.Visibility = BarItemVisibility.Always;
            game.modProcess.Exited += new EventHandler(modExited);
        }

        private void modExited(object sender, EventArgs e)
        {
            buttonModStart.Enabled = true;
            barButtonRun.Enabled = true;
            buttonModStop.Visibility = BarItemVisibility.Never;
            game.modProcess = null;
            //FormBorderStyle = FormBorderStyle.Sizable;
        }


        private void ModForm_ResizeEnd(object sender, EventArgs e)
        {
            if (game != null)
                game.Resize();
        }

        private void ModForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (game != null)
                game.Stop();
        }

        private void barButtonRunFullscreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            game = new Game(sourceSDK, panel1);
            game.StartFullScreen();

            buttonModStart.Enabled = false;
            barButtonRun.Enabled = false;
            buttonModStop.Visibility = BarItemVisibility.Always;
            game.modProcess.Exited += new EventHandler(modExited);
        }

        private void modProcessUpdater_Tick(object sender, EventArgs e)
        {
            if (game.modProcess == null)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                modProcessUpdater.Enabled = false;
                MaximizeBox = true;
            }
        }
    }
}