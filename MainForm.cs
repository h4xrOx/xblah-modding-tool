using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SourceModdingTool.SourceSDK;
using SourceModdingTool.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace SourceModdingTool
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        Game game = null;
        Steam sourceSDK;

        public MainForm() { InitializeComponent(); }

        private void Form_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName;
            string currentGame = Properties.Settings.Default.currentGame;
            string currentMod = Properties.Settings.Default.currentMod;

            sourceSDK = new Steam();
            updateToolsGames();

            toolsGames.EditValue = currentGame;
            toolsMods.EditValue = currentMod;
        }

        private void menuChoreography_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Faceposer
            if (e.Item == menuChoreographyFaceposer)
            {
                string gamePath = sourceSDK.GetGamePath(toolsGames.EditValue.ToString());

                string toolPath = gamePath + "\\bin\\hlfaceposer.exe";
                Process.Start(toolPath);
            }
        }

        private void menuFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            // New
            if (e.Item.Name == menuFileNew.Name)
            {
                NewModForm form = new NewModForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string folder = form.modFolder;
                    string title = form.modTitle;
                    string game = form.game;
                    string gameBranch = form.gameBranch;

                    string mod = title + " (" + folder + ")";

                    sourceSDK.setCurrentGame(game);
                    sourceSDK.setCurrentMod(mod);

                    updateToolsMods();

                    toolsGames.EditValue = game;
                    toolsMods.EditValue = title + " (" + folder + ")";
                }
            }

            // Exit
            else if (e.Item.Name == menuFileExit.Name)
            {
                Close();
            }
        }

        private void menuLevelDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Hammer
            if (e.Item == menuLevelDesignHammer)
            {
                sourceSDK.RunHammer(Application.StartupPath);
            }

            // Prefabs
            else if (e.Item == menuLevelDesignPrefabs)
            {
                string gamePath = sourceSDK.GetGamePath();
                Process.Start(gamePath + "\\bin\\Prefabs");
            }

            // Mapsrc
            else if (e.Item == menuLevelDesignMapsrc)
            {
                // TODO implement this
            }

            // Crafty
            else if (e.Item == menuLevelDesignCrafty)
            {
                Process.Start("Tools\\Crafty\\Crafty.exe");
            }

            // Terrain generator
            else if (e.Item == menuLevelDesignTerrainGenerator)
            {
                Process.Start("Tools\\TerrainGenerator\\TerrainGenerator.exe");
            }

            // Batch compiler
            else if (e.Item == menuLevelDesignBatchCompiler)
            {
                Process.Start("Tools\\BatchCompiler\\Batch Compiler.exe");
            }
        }

        private void menuMaterials_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Material editor
            if (e.Item == menuMaterialsEditor)
            {
                MaterialEditor form = new MaterialEditor(string.Empty, sourceSDK);
                form.ShowDialog();
            }
        }

        private void menuModding_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Open folder
            if (e.Item == menuModdingOpenFolder)
            {
                sourceSDK.OpenModFolder(toolsMods.EditValue.ToString());
            }

            // Clean
            else if (e.Item == menuModdingClean)
            {
                sourceSDK.CleanModFolder();
            }

            // Import
            else if (e.Item == menuModdingImport2)
            {
                ModSelectionDialog dialog = new ModSelectionDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string game = dialog.game;
                    string mod = dialog.mod;

                    AssetsCopierForm assetsCopierForm = new AssetsCopierForm(game,
                                                                             mod,
                                                                             sourceSDK.GetModPath(toolsGames.EditValue
                        .ToString(),
                                                                                                  toolsMods.EditValue
                        .ToString()));
                    if (assetsCopierForm.ShowDialog() == DialogResult.OK)
                    {
                    }
                }
            }

            // File explorer
            else if (e.Item == menuModdingFileExplorer)
            {
                FileExplorer form = new FileExplorer(sourceSDK);
                form.ShowDialog();
            }

            // Export
            else if (e.Item == menuModdingExport)
            {
                AssetsCopierForm form = new AssetsCopierForm(toolsGames.EditValue.ToString(),
                                                             toolsMods.EditValue.ToString());
                form.ShowDialog();
            }

            // Hud Editor
            else if (e.Item == menuModdingHudEditor)
            {
                HudEditorForm form = new HudEditorForm(sourceSDK);
                form.ShowDialog();
            }
        }

        private void menuModdingRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Run
            if (e.Item == menuModdingRun || e.Item == toolsRun || e.Item == toolsRunPopupRun)
            {
                game = new Game(sourceSDK, panel1);
                game.Start();

                FormBorderStyle = FormBorderStyle.Fixed3D;
                MaximizeBox = false;
                modStarted();
            }

            // Run Fullscreen
            else if (e.Item == menuModdingRunFullscreen || e.Item == toolsRunPopupRunFullscreen)
            {
                game = new Game(sourceSDK, panel1);
                game.StartFullScreen();

                modStarted();
            }

            // Ingame tools
            else if (e.Item == menuModdingIngameTools || e.Item == toolsRunPopupIngameTools)
            {
                game = new Game(sourceSDK, panel1);
                game.StartTools();
                game.modProcess.Exited += new EventHandler(modExited);

                FormBorderStyle = FormBorderStyle.Fixed3D;
                MaximizeBox = false;
                modStarted();
            }
        }

        private void menuModdingSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Game info
            if(e.Item == menuModdingSettingsGameInfo)
            {
                GameinfoForm form = new GameinfoForm(sourceSDK);
                form.ShowDialog();
                updateToolsGames();
                updateToolsMods();
            }

            // Chapters
            else if(e.Item == menuModdingSettingsChapters)
            {
                ChaptersForm form = new ChaptersForm(sourceSDK);
                form.ShowDialog();
            }

            // Menu
            else if(e.Item == menuModdingSettingsMenu)
            {
                GamemenuForm form = new GamemenuForm(sourceSDK);
                form.ShowDialog();
            }
        }

        private void menuModeling_ItemClick(object sender, ItemClickEventArgs e)
        {
            // HLMV
            if(e.Item == menuModelingHLMV)
            {
                string gamePath = sourceSDK.GetGamePath(toolsGames.EditValue.ToString());

                string toolPath = gamePath + "\\bin\\hlmv.exe";
                Process.Start(toolPath);
            }

            // Propper
            else if(e.Item == menuModelingPropper)
            {
                sourceSDK.RunPropperHammer();
            }

            // VMF to MDL
            else if(e.Item == menuModelingVMFtoMDL)
            {
                VMFtoMDL form = new VMFtoMDL(sourceSDK);
                form.ShowDialog();
            }

            // Crowbar
            else if(e.Item == menuModelingCrowbar)
            {
                Process.Start("Tools\\Crowbar\\Crowbar.exe");
            }
        }

        private void menuParticles_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Manifest generator
            if(e.Item == menuParticlesManifestGenerator)
            {
                PCF.CreateManifest(sourceSDK);
                XtraMessageBox.Show("Particle manifest generated.");
            }
        }

        private void modStarted()
        {
            game.modProcess.Exited += new EventHandler(modExited);

            modProcessUpdater.Enabled = true;
            toolsRun.Enabled = false;
            menuModdingRun.Enabled = false;
            menuModdingRunFullscreen.Enabled = false;
            menuModdingIngameTools.Enabled = false;
            toolsStop.Visibility = BarItemVisibility.Always;
        }

        private void modExited(object sender, EventArgs e) {
            game.modProcess = null;
        }

        private void ModForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(game != null)
                game.Stop();
        }

        private void ModForm_ResizeEnd(object sender, EventArgs e)
        {
            if(game != null)
                game.Resize();
        }

        private void modProcessUpdater_Tick(object sender, EventArgs e)
        {
            if(game == null || game.modProcess == null)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                modProcessUpdater.Enabled = false;
                MaximizeBox = true;

                toolsRun.Enabled = true;
                menuModdingRun.Enabled = true;
                menuModdingRunFullscreen.Enabled = true;
                menuModdingIngameTools.Enabled = true;
                toolsStop.Visibility = BarItemVisibility.Never;
            }
        }

        private void toolsGames_EditValueChanged(object sender, EventArgs e)
        {
            if(toolsGames.EditValue == null || toolsGames.EditValue.ToString() == string.Empty)
            {
                XtraMessageBox.Show("No Source game was selected. Please, try again.");
                return;
            }

            sourceSDK.setCurrentGame(toolsGames.EditValue.ToString());
            updateToolsMods();
            Properties.Settings.Default.currentGame = toolsGames.EditValue.ToString();
            Properties.Settings.Default.Save();
        }

        private void toolsMods_EditValueChanged(object sender, EventArgs e)
        {
            sourceSDK.setCurrentMod(toolsMods.EditValue.ToString());
            Properties.Settings.Default.currentMod = toolsMods.EditValue.ToString();
            Properties.Settings.Default.Save();

            toolsRun.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
            menuModding.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
            menuLevelDesign.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
            menuParticles.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);

            menuModelingHLMV.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
            menuChoreographyFaceposer.Enabled = (toolsMods.EditValue != null &&
                toolsMods.EditValue.ToString() != string.Empty);

            menuModelingPropper.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
        }

        private void toolsStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(game.modProcess != null)
                game.Stop();
        }

        private void updateToolsGames()
        {
            string currentGame = (toolsGames.EditValue != null ? toolsGames.EditValue.ToString() : string.Empty);
            repositoryGamesCombo.Items.Clear();
            foreach(KeyValuePair<string, string> item in sourceSDK.GetGamesList())
            {
                repositoryGamesCombo.Items.Add(item.Key);
            }

            if(repositoryGamesCombo.Items.Count > 0 && repositoryGamesCombo.Items.Contains(currentGame))
                toolsGames.EditValue = currentGame;
            else if(repositoryGamesCombo.Items.Count > 0)
                toolsGames.EditValue = repositoryGamesCombo.Items[0];
            else
            {
                toolsGames.EditValue = string.Empty;
                XtraMessageBox.Show("No Source games were found. Check if everything is all set at Steam and restart this tool.");
                Close();
            }
        }

        private void updateToolsMods()
        {
            string currentGame = (toolsGames.EditValue != null ? toolsGames.EditValue.ToString() : string.Empty);
            if(currentGame == string.Empty)
                return;

            string currentMod = (toolsMods.EditValue != null ? toolsMods.EditValue.ToString() : string.Empty);
            repositoryModsCombo.Items.Clear();
            foreach(KeyValuePair<string, string> item in sourceSDK.GetModsList(currentGame))
            {
                repositoryModsCombo.Items.Add(item.Key);
            }

            if(repositoryModsCombo.Items.Count > 0 && repositoryModsCombo.Items.Contains(currentMod))
                toolsMods.EditValue = currentMod;
            else if(repositoryModsCombo.Items.Count > 0)
                toolsMods.EditValue = repositoryModsCombo.Items[0];
            else
            {
                XtraMessageBox.Show("No mods were found for this Source game.");
                toolsMods.EditValue = string.Empty;
            }
        }
    }
}