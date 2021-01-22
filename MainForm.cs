using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using source_modding_tool.Modding;
 
using source_modding_tool.Tools;
using SourceSDK;
using SourceSDK.Packages;
using SourceSDK.Particles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace source_modding_tool
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        Instance instance = null;
        Launcher launcher;

        FormWindowState PreviousWindowState;

        public MainForm() {
            InitializeComponent();

            PreviousWindowState = WindowState;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName;
            string currentGame = Properties.Settings.Default.currentGame;
            string currentMod = Properties.Settings.Default.currentMod;

            launcher = new Launcher();
            updateToolsGames();

            if (repositoryGamesCombo.Items.Contains(currentGame))
                toolsGames.EditValue = currentGame;
            else
                toolsGames.EditValue = "";

            if (repositoryModsCombo.Items.Contains(currentMod))
                toolsMods.EditValue = currentMod;
            else
                toolsMods.EditValue = "";

            UpdateMenus();
            updateBackground();
        }

        private void menuChoreography_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Faceposer
            if (e.Item == menuChoreographyFaceposer)
            {
                string gamePath = launcher.GetGamesList()[(toolsGames.EditValue.ToString())].installPath;

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
                    string title = form.modFolder;
                    string gameName = form.gameName;
                    string gameBranch = form.gameBranch;

                    Game game = launcher.GetGamesList()[gameName];

                    string modName = title + " (" + folder + ")";

                    launcher.SetCurrentGame(game);
                    toolsGames.EditValue = game.name;

                    updateToolsMods();

                    Mod mod = launcher.GetCurrentGame().GetModsList(launcher)[modName];
                    launcher.GetCurrentGame().SetCurrentMod(mod);
                    toolsMods.EditValue = mod.name;
                }
            }

            // Exit
            else if (e.Item.Name == menuFileExit.Name)
            {
                Close();
            }

            // Libraries
            else if (e.Item.Name == menuFileLibraries.Name)
            {
                LibrariesForm form = new LibrariesForm(launcher);
                form.ShowDialog();

                updateToolsGames();
            }
        }

        private void menuLevelDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Run Map
            if (e.Item == menuLevelDesignRunMap)
            {
                Game game = launcher.GetCurrentGame();
                LegacyFileExplorer form = new LegacyFileExplorer(launcher);
                form.RootDirectory = "maps/";
                form.Filter = "BSP Files (*.bsp)|*.bsp|VPK Files (*.vpk)|*.vpk";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    VPK.File file = form.selectedFiles[0];
                    if ((file.type == ".bsp" && game.engine == Engine.GOLDSRC) || (file.type == ".bsp" && game.engine == Engine.SOURCE) || (file.type == ".vpk" && file.path.StartsWith("maps/") && game.engine == Engine.SOURCE2))
                    {
                        // It's a map
                        string mapName = Path.GetFileNameWithoutExtension(file.path);
                        if (instance != null)
                        {
                            instance.Command("+map " + mapName);
                        }
                        else
                        {
                            RunDialog runDialog = new RunDialog(launcher);
                            if (runDialog.ShowDialog() == DialogResult.OK)
                            {
                                Run(runDialog.runPreset, string.Join(" ", new string[] { runDialog.commands, "+map " + mapName }));
                            }
                        }
                    }
                }
            }
            // Hammer
            if (e.Item == menuLevelDesignHammer)
            {
                Hammer.RunHammer(launcher, instance, panel1);

                // TODO run source 2 hammer as a mod.
                if (launcher.GetCurrentGame().engine == Engine.SOURCE2)
                {
                    FormBorderStyle = FormBorderStyle.Fixed3D;
                    MaximizeBox = false;

                    modStarted();
                }
            }

            // Fog Previewer
            else if (e.Item == menuLevelDesignFogPreviewer)
            {
                FogForm form = new FogForm(launcher);
                form.ShowDialog();
            }

            // Prefabs
            else if (e.Item == menuLevelDesignPrefabs)
            {
                switch (launcher.GetCurrentGame().engine)
                {
                    case Engine.SOURCE:
                        string gamePath = launcher.GetCurrentGame().installPath;
                        Process.Start(gamePath + "\\bin\\Prefabs");
                        break;
                    case Engine.GOLDSRC:
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\HammerEditor\\prefabs");
                        break;
                }

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
                MaterialEditor form = new MaterialEditor(string.Empty, launcher);
                form.ShowDialog();
            }
        }

        private void menuModding_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Open folder
            if (e.Item == menuModdingOpenFolder)
            {
                launcher.GetCurrentMod().OpenInstallFolder();
            }

            // Clean
            else if (e.Item == menuModdingClean)
            {
                CleanDialog dialog = new CleanDialog(launcher);
                if (dialog.files.Count > 0)
                    dialog.ShowDialog();
            }

            // Import
            else if (e.Item == menuModdingImport2)
            {
                ModSelectionDialog dialog = new ModSelectionDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string gameName = dialog.game;
                    string modName = dialog.mod;

                    Game currentGame = launcher.GetCurrentGame();
                    Mod currentMod = launcher.GetCurrentMod();

                    Game sourceGame = launcher.GetGamesList()[gameName];
                    Mod sourceMod = launcher.GetModsList(sourceGame)[modName];

                    AssetsCopierForm assetsCopierForm = new AssetsCopierForm(launcher, sourceMod);
                    assetsCopierForm.OpenDestination = false;
                    assetsCopierForm.destination = currentMod.installPath;
                    if (assetsCopierForm.ShowDialog() == DialogResult.OK)
                    {
                        
                    }

                    launcher.SetCurrentGame(currentGame);
                    launcher.SetCurrentMod(currentMod);
                }
            }

            // File explorer (Legacy)
            else if (e.Item == menuModdingLegacyFileExplorer)
            {
                LegacyFileExplorer form = new LegacyFileExplorer(launcher);
                form.ShowDialog();
            }

            // File explorer
            else if (e.Item == menuModdingFileExplorer)
            {
                FileExplorer form = new FileExplorer(launcher);
                form.ShowDialog();
            }

            // Export
            else if (e.Item == menuModdingExport)
            {
                Game game = launcher.GetGamesList()[toolsGames.EditValue.ToString()];
                Mod mod = launcher.GetModsList(game)[toolsMods.EditValue.ToString()];

                AssetsCopierForm form = new AssetsCopierForm(launcher, mod);
                form.ShowDialog();
            }

            // Hud Editor
            else if (e.Item == menuModdingHudEditor)
            {
                HudEditor form = new HudEditor(launcher, launcher.GetCurrentMod());
                form.ShowDialog();
            }

            // Delete mod
            else if (e.Item == menuModdingDelete)
            {
                if (XtraMessageBox.Show("Are you sure you want to delete this mod?", "Delete mod", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    launcher.GetCurrentGame().DeleteMod();
                    updateToolsGames();
                    updateToolsMods();
                }
            }
        }

        public void Run(RunPreset runPreset, string command)
        {
            if (launcher.GetCurrentGame().engine == Engine.SOURCE2)
                launcher.GetCurrentGame().ApplyNonVRPatch();    // Until Valve reinserts the -game parameter

            instance = new Instance(launcher, panel1);
            updateBackground();
            Application.DoEvents();

            instance.Start(runPreset, command);

            if (runPreset.runMode == RunMode.WINDOWED && (launcher.GetCurrentGame().engine == Engine.SOURCE || launcher.GetCurrentGame().engine == Engine.SOURCE2))
            {
                FormBorderStyle = FormBorderStyle.Fixed3D;
                MaximizeBox = false;
            }
            modStarted();
        }

        public void Run(int runMode, string command)
        {
            RunPreset preset = new RunPreset(runMode);
            Run(preset, command);
        }

        private void menuModdingRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Run
            if (e.Item == toolsRun)
            {
                Run(RunMode.DEFAULT, "");
            }

            // Run fullscreen
            if (e.Item == menuModdingRunFullscreen || e.Item == toolsRunPopupRunFullscreen)
            {
                Run(RunMode.FULLSCREEN, "");
            }

            // Run Windowed
            else if (e.Item == menuModdingRunWindowed || e.Item == toolsRunPopupRunWindowed)
            {
                Run(RunMode.WINDOWED, "");
            }

            // Run VR
            if (e.Item == menuModdingRunVR || e.Item == toolsRunPopupRunVR)
            {
                Run(RunMode.VR, "");
            }

            // Expert mode
            else if (e.Item == menuModdingRunExpert || e.Item == toolsRunPopupExpert)
            {
                RunDialog dialog = new RunDialog(launcher);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string arguments = dialog.commands;
                    RunPreset runPreset = dialog.runPreset;
                    Run(runPreset, arguments);
                }
            }
        }

        private void menuModdingSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Game info
            if (e.Item == menuModdingSettingsGameInfo)
            {
                GameinfoForm form = new GameinfoForm(launcher);
                form.ShowDialog();
                updateToolsGames();
                updateToolsMods();
            }

            // Chapters
            else if (e.Item == menuModdingSettingsChapters)
            {
                ChaptersForm form = new ChaptersForm(launcher);
                form.ShowDialog();
            }

            // Starting maps
            else if (e.Item == menuModdingSettingsStartingMaps)
            {
                StartMap form = new StartMap(launcher);
                form.ShowDialog();
            }

            // Menu
            else if (e.Item == menuModdingSettingsMenu)
            {
                GamemenuForm form = new GamemenuForm(launcher);
                form.ShowDialog();
            }

            // Content Monut
            else if (e.Item == menuModdingSettingsContentMount)
            {
                SearchPathsForm form = new SearchPathsForm(launcher);
                form.ShowDialog();
            }
        }

        private void menuModeling_ItemClick(object sender, ItemClickEventArgs e)
        {
            // HLMV
            if (e.Item == menuModelingHLMV)
            {
                string gamePath = launcher.GetGamesList()[toolsGames.EditValue.ToString()].installPath;

                string toolPath = gamePath + "\\bin\\hlmv.exe";

                Process process = new Process();
                process.StartInfo.FileName = toolPath ;
                process.StartInfo.Arguments = string.Empty;

                if (launcher.GetCurrentGame().name == "Mapbase")
                    process.StartInfo.Arguments = "-game " + launcher.GetCurrentMod().installPath;

                process.Start();
            }

            // Propper
            else if (e.Item == menuModelingPropper)
            {
                Hammer.RunPropperHammer(launcher.GetCurrentMod());
            }

            // VMF to MDL
            else if (e.Item == menuModelingVMFtoMDL)
            {
                VMFtoMDL form = new VMFtoMDL(launcher);
                form.ShowDialog();
            }

            // Crowbar
            else if (e.Item == menuModelingCrowbar)
            {
                Process.Start("Tools\\Crowbar\\Crowbar.exe");
            }
        }

        private void menuParticles_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Manifest generator
            if (e.Item == menuParticlesManifestGenerator)
            {
                PCF.CreateManifest(launcher);
                XtraMessageBox.Show("Particle manifest generated.");
            }
        }

        private void modStarted()
        {
            if (instance == null || instance.modProcess == null)
                return;

            instance.modProcess.Exited += new EventHandler(modExited);

            modProcessUpdater.Enabled = true;
            toolsRun.Enabled = false;
            menuModdingRunFullscreen.Enabled = false;
            menuModdingRunWindowed.Enabled = false;
            menuModdingIngameTools.Enabled = false;
            menuModdingDelete.Enabled = false;
            toolsStop.Visibility = BarItemVisibility.Always;
        }

        private void modExited(object sender, EventArgs e) {
            if (instance != null)
                instance.modProcess = null;
        }

        private void ModForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(instance != null)
                instance.Stop();
        }

        private void ModForm_ResizeEnd(object sender, EventArgs e)
        {
            if (instance != null)
                instance.Resize();
            else
                updateBackground();
        }

        private void modProcessUpdater_Tick(object sender, EventArgs e)
        {
            if(instance == null || instance.modProcess == null)
            {
                instance = null;
                FormBorderStyle = FormBorderStyle.Sizable;
                modProcessUpdater.Enabled = false;
                MaximizeBox = true;

                toolsRun.Enabled = true;
                menuModdingRunFullscreen.Enabled = true;
                menuModdingRunWindowed.Enabled = true;
                menuModdingIngameTools.Enabled = true;
                menuModdingDelete.Enabled = true;
                toolsStop.Visibility = BarItemVisibility.Never;

                updateBackground();
            }
        }

        private void toolsGames_EditValueChanged(object sender, EventArgs e)
        {
            if(toolsGames.EditValue == null || toolsGames.EditValue.ToString() == string.Empty)
            {
                XtraMessageBox.Show("No Source game was selected. Please, try again.");
                return;
            }

            string gameName = toolsGames.EditValue.ToString();

            Dictionary<string, Game> gamesList = launcher.GetGamesList();

            if (!gamesList.ContainsKey(gameName))
            {
                XtraMessageBox.Show("Could not find game " + gameName);
                return;
            }

            Game game = gamesList[gameName];

            if (game == null)
            {
                XtraMessageBox.Show("Game " + gameName + " is null.");
                return;
            }

            if (launcher == null)
            {
                XtraMessageBox.Show("Launcher is null, and it really shouldn't be. Let XBLAH know.");
                return;
            }
                

            

            launcher.SetCurrentGame(game);
            updateToolsMods();
            Properties.Settings.Default.currentGame = game.name;
            Properties.Settings.Default.Save();
        }

        private void toolsMods_EditValueChanged(object sender, EventArgs e)
        {
            if (launcher == null)
                return;

            Game currentGame = launcher.GetCurrentGame();

            if (launcher.GetModsList(currentGame).ContainsKey(toolsMods.EditValue.ToString()))
            {
                Mod mod = launcher.GetModsList(currentGame)[toolsMods.EditValue.ToString()];

                launcher.GetCurrentGame().SetCurrentMod(mod);
                Properties.Settings.Default.currentMod = toolsMods.EditValue.ToString();
                Properties.Settings.Default.Save();

                UpdateMenus();
            }
        }

        private void UpdateMenus()
        {
            switch(launcher.GetCurrentGame().engine)
            {
                case Engine.SOURCE:
                    toolsRun.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        toolsRunPopupIngameTools.Enabled = true;
                        toolsRunPopupRunFullscreen.Enabled = true;
                        toolsRunPopupRunVR.Enabled = true;
                        toolsRunPopupRunWindowed.Enabled = true;
                    menuModding.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuModdingRunFullscreen.Enabled = true;
                        menuModdingRunWindowed.Enabled = true;
                        menuModdingRunVR.Enabled = true;
                        menuModdingIngameTools.Enabled = true;
                        menuModdingClean.Enabled = true;
                        menuModdingImport2.Enabled = true;
                        menuModdingSettings.Enabled = true;
                            menuModdingSettingsGameInfo.Enabled = true;
                            menuModdingSettingsChapters.Enabled = true;
                            menuModdingSettingsContentMount.Enabled = true;
                            menuModdingSettingsMenu.Enabled = true;
                            menuModdingSettingsStartingMaps.Enabled = false;
                            menuModdingHudEditor.Enabled = true;
                        menuModdingLegacyFileExplorer.Enabled = true;
                        menuModdingExport.Enabled = true;
                    menuLevelDesign.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuLevelDesignBatchCompiler.Enabled = true;
                        menuLevelDesignCrafty.Enabled = true;
                        menuLevelDesignFogPreviewer.Enabled = true;
                        menuLevelDesignHammer.Enabled = true;
                        menuLevelDesignMapsrc.Enabled = true;
                        menuLevelDesignPrefabs.Enabled = true;
                        menuLevelDesignTerrainGenerator.Enabled = true;
                        menuLevelDesignRunMap.Enabled = true;
                    menuModeling.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                    menuMaterials.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                    menuParticles.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                    menuChoreographyFaceposer.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);

                    break;
                case Engine.SOURCE2:
                    toolsRun.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        toolsRunPopupIngameTools.Enabled = false;
                        toolsRunPopupRunFullscreen.Enabled = true;
                        toolsRunPopupRunVR.Enabled = true;
                        toolsRunPopupRunWindowed.Enabled = true;
                    menuModding.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuModdingRunFullscreen.Enabled = true;
                        menuModdingRunWindowed.Enabled = true;
                        menuModdingRunVR.Enabled = true;
                        menuModdingIngameTools.Enabled = false;
                        menuModdingClean.Enabled = true;
                        menuModdingImport2.Enabled = false;
                        menuModdingSettings.Enabled = true;
                            menuModdingSettingsGameInfo.Enabled = true;
                            menuModdingSettingsChapters.Enabled = true;
                            menuModdingSettingsContentMount.Enabled = false;
                            menuModdingSettingsMenu.Enabled = false;
                            menuModdingSettingsStartingMaps.Enabled = false;
                            menuModdingHudEditor.Enabled = false;
                        menuModdingLegacyFileExplorer.Enabled = true;
                        menuModdingExport.Enabled = false;
                    menuLevelDesign.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuLevelDesignBatchCompiler.Enabled = false;
                        menuLevelDesignCrafty.Enabled = false;
                        menuLevelDesignFogPreviewer.Enabled = false;
                        menuLevelDesignHammer.Enabled = true;
                        menuLevelDesignMapsrc.Enabled = false;
                        menuLevelDesignPrefabs.Enabled = false;
                        menuLevelDesignTerrainGenerator.Enabled = false;
                        menuLevelDesignRunMap.Enabled = true;
                    menuModeling.Enabled = false;
                    menuMaterials.Enabled = false;
                    menuParticles.Enabled = false;
                    menuChoreographyFaceposer.Enabled = false;

                    break;
                case Engine.GOLDSRC:
                    toolsRun.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        toolsRunPopupIngameTools.Enabled = false;
                        toolsRunPopupRunFullscreen.Enabled = true;
                        toolsRunPopupRunVR.Enabled = false;
                        toolsRunPopupRunWindowed.Enabled = true;
                    menuModding.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuModdingRunFullscreen.Enabled = true;
                        menuModdingRunWindowed.Enabled = true;
                        menuModdingRunVR.Enabled = false;
                        menuModdingIngameTools.Enabled = false;
                        menuModdingClean.Enabled = false;
                        menuModdingImport2.Enabled = false;
                        menuModdingSettings.Enabled = true;
                            menuModdingSettingsGameInfo.Enabled = true;
                            menuModdingSettingsChapters.Enabled = false;
                            menuModdingSettingsContentMount.Enabled = false;
                            menuModdingSettingsMenu.Enabled = true;
                            menuModdingSettingsStartingMaps.Enabled = true;
                            menuModdingHudEditor.Enabled = false;
                        menuModdingLegacyFileExplorer.Enabled = true;
                        menuModdingExport.Enabled = false;
                    menuLevelDesign.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuLevelDesignBatchCompiler.Enabled = false;
                        menuLevelDesignCrafty.Enabled = false;
                        menuLevelDesignFogPreviewer.Enabled = false;
                        menuLevelDesignHammer.Enabled = true;
                        menuLevelDesignMapsrc.Enabled = false;
                        menuLevelDesignPrefabs.Enabled = true;
                        menuLevelDesignTerrainGenerator.Enabled = false;
                        menuLevelDesignRunMap.Enabled = true;
                    menuModeling.Enabled = false;
                    menuMaterials.Enabled = false;
                    menuParticles.Enabled = false;
                    menuChoreographyFaceposer.Enabled = false;

                    break;
            }

        }

        private void toolsStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (instance.modProcess != null)
            {
                instance.Stop();
                instance = null;
            }
        }

        private void updateToolsGames()
        {
            if (launcher == null)
                return;

            string currentGame = (toolsGames.EditValue != null ? toolsGames.EditValue.ToString() : string.Empty);
            repositoryGamesCombo.Items.Clear();
            Dictionary<string, Game> gamesList = launcher.GetGamesList();

            if (gamesList.Count > 0)
                foreach (KeyValuePair<string, Game> item in gamesList)
                    repositoryGamesCombo.Items.Add(item.Key);

            if(repositoryGamesCombo.Items.Count > 0 && repositoryGamesCombo.Items.Contains(currentGame))
                toolsGames.EditValue = currentGame;
            else if(repositoryGamesCombo.Items.Count > 0)
                toolsGames.EditValue = repositoryGamesCombo.Items[0];
            else
            {
                toolsGames.EditValue = string.Empty;
            }
        }

        private void updateToolsMods()
        {
            string currentMod = (toolsMods.EditValue != null ? toolsMods.EditValue.ToString() : string.Empty);
            repositoryModsCombo.Items.Clear();

            Game currentGame = (toolsGames.EditValue != null ? launcher.GetGamesList()[toolsGames.EditValue.ToString()] : null);
            if(currentGame == null)
                return;

            Dictionary<string, Mod> modsList = launcher.GetModsList(currentGame);
            if (modsList.Count > 0)
                foreach (KeyValuePair<string, Mod> item in modsList)
                    repositoryModsCombo.Items.Add(item.Key);

            if (repositoryModsCombo.Items.Count > 0 && repositoryModsCombo.Items.Contains(currentMod))
                toolsMods.EditValue = currentMod;
            else if(repositoryModsCombo.Items.Count > 0)
                toolsMods.EditValue = repositoryModsCombo.Items[0];
            else
            {
                toolsMods.EditValue = string.Empty;
            }
        }

        private void tools_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (e.Item.Tag)
            {
                case "reattach":
                    if (instance != null)
                        instance.AttachProcessTo(instance.modProcess, panel1);
                    break;
            }
        }

        private void updateBackground()
        {
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            using (Graphics gfx = Graphics.FromImage(bitmap))
            {
                Image src = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Assets\\Misc\\background.png");

                if (instance != null && instance.isLoading)
                    src = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Assets\\Misc\\background_loading.png");

                int squareSize = Math.Max(panel1.Width, panel1.Height);
                gfx.DrawImage(
                    src,
                    new Rectangle((panel1.Width - squareSize) / 2, (panel1.Height - squareSize) / 2, squareSize, squareSize),
                    new Rectangle(0, 0, src.Width, src.Height),
                    GraphicsUnit.Pixel);
            }
            if (panel1.BackgroundImage != null)
                panel1.BackgroundImage.Dispose();
            panel1.BackgroundImage = bitmap;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                ModForm_ResizeEnd(sender, e);
            }
            else if(PreviousWindowState == FormWindowState.Maximized && WindowState == FormWindowState.Normal)
            {
                ModForm_ResizeEnd(sender, e);
            }
            PreviousWindowState = WindowState;
        }
    }
}