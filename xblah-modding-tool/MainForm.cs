using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Microsoft.Win32;
using xblah_modding_tool.Materials;
using xblah_modding_tool.Modding;
using xblah_modding_tool.Modding.Blueprints;
using xblah_modding_tool.Sound;
using xblah_modding_tool.Tools;
using xblah_modding_lib;
using xblah_modding_lib.Maps;
using xblah_modding_lib.Packages;
using xblah_modding_lib.Particles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace xblah_modding_tool
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
            Text = Application.ProductName + " - v" + Application.ProductVersion;
            string currentGame = Properties.Settings.Default.currentGame;
            string currentMod = Properties.Settings.Default.currentMod;

            launcher = new Launcher();
            launcher.libraries.Load();

            checkMapbaseInstallation();
            setSDK2013Upcoming();
            updateToolsGames();

            if (launcher.GetGamesList().ContainsKey(currentGame))
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

        private void setSDK2013Upcoming()
        {
            launcher.SetSDK2013SPUpcoming();
        }

        private void checkMapbaseInstallation()
        {
            string sourceModsDir = launcher.GetSourceModsDirectory();
            if (!Directory.Exists(sourceModsDir + "mapbase_shared\\") || !Directory.Exists(sourceModsDir + "mapbase_hl2\\") || !Directory.Exists(sourceModsDir + "\\mapbase_episodic\\"))
            {
                foreach (string file in Directory.GetFiles(Launcher.ApplicationDirectory + "\\Tools\\Mapbase\\", "*", SearchOption.AllDirectories))
                {
                    string destinationPath = sourceModsDir + file.Replace(Launcher.ApplicationDirectory + "\\Tools\\Mapbase\\", string.Empty);
                    string destinationDirectory = new FileInfo(destinationPath).Directory.FullName;
                    Directory.CreateDirectory(destinationDirectory);
                    File.Copy(file, destinationPath);
                }
            }
        }

        private void menuChoreography_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Faceposer
            if (e.Item == menuChoreographyFaceposer)
            {
                string gamePath = launcher.GetCurrentGame().InstallPath;

                // Copy files
                string path = AppDomain.CurrentDomain.BaseDirectory + "Tools\\Faceposer\\";
                foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                {
                    try
                    {
                        Uri path1 = new Uri(path);
                        Uri path2 = new Uri(file);
                        Uri diff = path1.MakeRelativeUri(path2);
                        string relPath = diff.OriginalString;

                        Directory.CreateDirectory(Path.GetDirectoryName(gamePath + "\\bin\\" + relPath));
                        File.Copy(file, gamePath + "\\bin\\" + relPath, true);
                    }
                    catch (IOException e2)
                    {

                    }
                }

                // Run faceposer
                Process process = new Process();
                process.StartInfo.FileName = gamePath + "\\bin\\hlfaceposer.exe";
                process.StartInfo.Arguments = "-game " + launcher.GetCurrentMod().InstallPath;

                process.Start();
            }
        }

        private void menuFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            // New
            if (e.Item == menuFileNew)
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
                    toolsGames.EditValue = game.Name;

                    updateToolsMods();

                    Mod mod = launcher.GetCurrentGame().GetModsList(launcher)[modName];
                    launcher.GetCurrentGame().SetCurrentMod(mod);
                    toolsMods.EditValue = mod.Name;
                }
            }

            // Exit
            else if (e.Item == menuFileExit)
            {
                Close();
            }

            // Libraries
            else if (e.Item == menuFileLibraries)
            {
                LibrariesForm form = new LibrariesForm(launcher);
                form.ShowDialog();

                updateToolsGames();
            }

            // Game folder
            else if(e.Item == menuFileOpenGameFolder)
            {
                launcher.GetCurrentGame().OpenInstallFolder();
            }
        }

        private void menuLevelDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Run Map
            if (e.Item == menuLevelDesignRunMap)
            {
                Game game = launcher.GetCurrentGame();
                FileExplorer form = new FileExplorer(launcher, FileExplorer.Mode.OPEN)
                {
                    RootDirectory = "maps",
                    Filter = "BSP Files (*.bsp)|*.bsp|VPK Files (*.vpk)|*.vpk",
                    MultiSelect = false
                };
                if (form.ShowDialog() == DialogResult.OK)
                {
                    PackageFile file = form.Selection[0];
                    if ((file.Extension == "bsp" && game.EngineID == Engine.GOLDSRC) || (file.Extension == "bsp" && game.EngineID == Engine.SOURCE) || (file.Extension == "vpk" && file.FullPath.StartsWith("maps/") && game.EngineID == Engine.SOURCE2))
                    {
                        // It's a map
                        string mapName = file.Filename;
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
                if (launcher.GetCurrentGame().EngineID == Engine.SOURCE2)
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
                switch (launcher.GetCurrentGame().EngineID)
                {
                    case Engine.SOURCE:
                        string gamePath = launcher.GetCurrentGame().InstallPath;
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

            else if(e.Item == menuLevelDesignResetHammerConfigsButton)
            {
                Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Valve\\Hammer", false);
            }

            // Compile a map
            else if (e.Item == menuLevelDesignCompile)
            {

            }

            // Decompile a map
            else if(e.Item == menuLevelDesignDecompile)
            {
                FileExplorer fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.OPEN)
                {
                    RootDirectory = "maps",
                    Filter = "BSP Files (*.bsp)|*.bsp"
                };
                if (fileExplorer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PackageFile[] files = fileExplorer.Selection;

                    Directory.CreateDirectory(launcher.GetCurrentMod().InstallPath + "\\mapsrc");

                    foreach (PackageFile file in files)
                    {
                        BSP.Decompile(file, launcher);
                    }

                    Process.Start("explorer.exe", launcher.GetCurrentMod().InstallPath + "\\mapsrc\\");
                }
            }
        }

        private void menuMaterials_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Material editor
            if (e.Item == menuMaterialsEditor)
            {
                MaterialEditor form = new MaterialEditor(launcher);
                form.ShowDialog();
            }
            else if(e.Item == menuMaterialsSkyboxEditor)
            {
                SkyboxEditor form = new SkyboxEditor(launcher);
                form.ShowDialog();
            }
            else if(e.Item == menuMaterialsReload)
            {
                instance?.Command("+mat_reloadallmaterials");
            }

            // Blueprints
            else if (e.Item == menuMaterialsBlueprints)
            {
                BlueprintCategoriesDialog form = new BlueprintCategoriesDialog(launcher, "textures");
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
            else if (e.Item == menuModdingImport)
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

                    AssetsRequiredForm assetsCopierForm = new AssetsRequiredForm(launcher, sourceMod);
                    assetsCopierForm.OpenDestination = false;
                    assetsCopierForm.destination = currentMod.InstallPath;
                    if (assetsCopierForm.ShowDialog() == DialogResult.OK)
                    {

                    }

                    launcher.SetCurrentGame(currentGame);
                    launcher.SetCurrentMod(currentMod);
                }
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

                AssetsRequiredForm form = new AssetsRequiredForm(launcher, mod);
                form.ShowDialog();
            }

            // Hud Editor
            else if (e.Item == menuScriptsHudEditor)
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

            // Create VPK
            else if (e.Item == menuModdingAssetsPack)
            {
                PackFilesDialog form = new PackFilesDialog(launcher);
                form.ShowDialog();
            }

            // Extract VPK
            else if (e.Item == menuModdingAssetsUnpack)
            {
                UnpackFilesDialog form = new UnpackFilesDialog(launcher);
                form.ShowDialog();
            }

            // Pack Custom Folder
            else if(e.Item == menuModdingAssetsPackCustomFolder)
            {
                PackCustomFolderDialog form = new PackCustomFolderDialog(launcher);
                form.ShowDialog();
            }

            // Remove Duplicates
            else if(e.Item == menuModdingAssetsRemoveDuplicates)
            {
                RemoveDuplicatesForm form = new RemoveDuplicatesForm(launcher);
                form.ShowDialog();
            }
        }

        public void Run(RunPreset runPreset, string command)
        {
            if (launcher.GetCurrentGame().EngineID == Engine.SOURCE2)
                launcher.GetCurrentGame().ApplyNonVRPatch();    // Until Valve reinserts the -game parameter

            instance = new Instance(launcher, panel1);
            updateBackground();
            Application.DoEvents();

            instance.Start(runPreset, command);

            if (runPreset.runMode == RunMode.WINDOWED && (launcher.GetCurrentGame().EngineID == Engine.SOURCE || launcher.GetCurrentGame().EngineID == Engine.SOURCE2))
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
            if (e.Item.Tag is RunPreset)
            {
                Run(e.Item.Tag as RunPreset, "");
                return;
            }

            if (e.Item == toolsRun)
            {
                List<RunPreset> runPresets = RunDialog.GetPresets(launcher);
                Run(runPresets[0], "");
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
            else if (e.Item == menuModdingContentMountAdvanced)
            {
                ExpertContentMountForm form = new ExpertContentMountForm(launcher);
                form.ShowDialog();
            }
        }

        private void menuModeling_ItemClick(object sender, ItemClickEventArgs e)
        {
            // HLMV
            if (e.Item == menuModelingHLMV)
            {
                if (launcher.GetCurrentGame().EngineID == Engine.SOURCE)
                {
                    string gamePath = launcher.GetGamesList()[toolsGames.EditValue.ToString()].InstallPath;

                    string toolPath = gamePath + "\\bin\\hlmv.exe";

                    Process process = new Process();
                    process.StartInfo.FileName = toolPath;
                    process.StartInfo.Arguments = string.Empty;

                    if (launcher.GetCurrentGame().Name == "Mapbase")
                        process.StartInfo.Arguments = "-game " + launcher.GetCurrentMod().InstallPath;

                    process.Start();
                } else if(launcher.GetCurrentGame().EngineID == Engine.GOLDSRC)
                {
                    string gamePath = launcher.GetGamesList()[toolsGames.EditValue.ToString()].InstallPath;

                    string toolPath = AppDomain.CurrentDomain.BaseDirectory + "Tools\\Jed's Half-Life Model Viewer\\hlmv.exe";

                    Process process = new Process();
                    process.StartInfo.FileName = toolPath;
                    process.StartInfo.Arguments = string.Empty;

                    process.Start();
                }
            }

            // Propper
            else if (e.Item == menuModelingPropper)
            {
                Hammer.RunSourceHammerWithPropper(launcher.GetCurrentMod());
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

            // Reload
            else if(e.Item == menuModelingReload)
            {
                instance?.Command("+r_flushlod");
            }

            // Blueprints
            else if(e.Item == menuModelingBlueprints)
            {
                BlueprintCategoriesDialog form = new BlueprintCategoriesDialog(launcher, "models");
                form.ShowDialog();
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
            menuModdingDelete.Enabled = false;
            toolsStop.Visibility = BarItemVisibility.Always;
            toolsReload.Visibility = BarItemVisibility.Always;
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
                menuModdingDelete.Enabled = true;
                toolsStop.Visibility = BarItemVisibility.Never;
                toolsReload.Visibility = BarItemVisibility.Never;

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
            Properties.Settings.Default.currentGame = game.Name;
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
            switch(launcher.GetCurrentGame().EngineID)
            {
                case Engine.SOURCE:
                    toolsRun.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                    menuModding.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuModdingRunExpert.Enabled = true;
                        menuModdingClean.Enabled = true;
                        menuModdingAssets.Enabled = true;
                        menuModdingImport.Enabled = true;
                            menuModdingSettingsGameInfo.Enabled = true;
                            menuModdingSettingsChapters.Enabled = true;
                            menuModdingContentMountAdvanced.Enabled = true;
                            menuModdingSettingsMenu.Enabled = true;
                            menuModdingSettingsStartingMaps.Enabled = false;
                            menuScriptsHudEditor.Enabled = true;
                        menuModdingExport.Enabled = true;
                    menuLevelDesign.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuLevelDesignReload.Enabled = true;
                        menuLevelDesignBatchCompiler.Enabled = true;
                        menuLevelDesignCrafty.Enabled = true;
                        menuLevelDesignFogPreviewer.Enabled = true;
                        menuLevelDesignHammer.Enabled = true;
                        menuLevelDesignMapsrc.Enabled = true;
                        menuLevelDesignPrefabs.Enabled = true;
                        menuLevelDesignTerrainGenerator.Enabled = true;
                        menuLevelDesignRunMap.Enabled = true;
                        menuLevelDesignDecompile.Enabled = true;
                    menuModeling.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuModelingReload.Enabled = true;
                        menuModelingHLMV.Enabled = true;
                        menuModelingEditor2.Enabled = true;
                            menuModelingPropper.Enabled = true;
                        menuModelingCompile.Enabled = true;
                        menuModelingDecompile.Enabled = true;
                    menuMaterials.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuMaterialsReload.Enabled = true;
                        menuMaterialsEditor.Enabled = true;
                        menuMaterialsSkyboxEditor.Enabled = true;
                    menuParticles.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                    menuChoreography.Enabled = true;
                        menuChoreographyFaceposer.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                    menuSound.Enabled = true;
                    menuScripts.Enabled = false;

                    if (launcher.GetCurrentGame().GetAppId() == 730) // CSGO
                    {
                        menuLevelDesignHammer.Enabled = false;
                        menuLevelDesignDecompile.Enabled = false;
                        menuModelingPropper.Enabled = false;
                        toolsRun.Enabled = false;
                        menuModdingRunExpert.Enabled = false;
                        menuChoreography.Enabled = false;
                        menuMaterials.Enabled = false;
                        menuModeling.Enabled = false;
                        menuSound.Enabled = false;
                        //System.Diagnostics.Debugger.Break();
                    }

                    break;
                case Engine.SOURCE2:
                    toolsRun.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                    menuModding.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuModdingRunExpert.Enabled = true;
                        menuModdingClean.Enabled = true;
                        menuModdingAssets.Enabled = false;
                        menuModdingImport.Enabled = false;
                            menuModdingSettingsGameInfo.Enabled = true;
                            menuModdingSettingsChapters.Enabled = true;
                            menuModdingContentMountAdvanced.Enabled = false;
                            menuModdingSettingsMenu.Enabled = false;
                            menuModdingSettingsStartingMaps.Enabled = false;
                            menuScriptsHudEditor.Enabled = false;
                        menuModdingExport.Enabled = false;
                    menuLevelDesign.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuLevelDesignReload.Enabled = false;
                        menuLevelDesignBatchCompiler.Enabled = false;
                        menuLevelDesignCrafty.Enabled = false;
                        menuLevelDesignFogPreviewer.Enabled = false;
                        menuLevelDesignHammer.Enabled = false;
                        menuLevelDesignMapsrc.Enabled = false;
                        menuLevelDesignPrefabs.Enabled = false;
                        menuLevelDesignTerrainGenerator.Enabled = false;
                        menuLevelDesignRunMap.Enabled = true;
                        menuLevelDesignDecompile.Enabled = false;
                    menuModeling.Enabled = false;
                        menuModelingReload.Enabled = false;
                        menuModelingHLMV.Enabled = false;
                        menuModelingEditor2.Enabled = false;
                        menuModelingCompile.Enabled = false;
                        menuModelingDecompile.Enabled = false;
                    menuMaterials.Enabled = false;
                        menuMaterialsReload.Enabled = false;
                        menuMaterialsEditor.Enabled = false;
                        menuMaterialsSkyboxEditor.Enabled = false;
                    menuParticles.Enabled = false;
                    menuChoreography.Enabled = false;
                        menuChoreographyFaceposer.Enabled = false;
                    menuSound.Enabled = false;
                    menuScripts.Enabled = false;

                    break;
                case Engine.GOLDSRC:
                    toolsRun.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                    menuModding.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuModdingRunExpert.Enabled = true;
                        menuModdingClean.Enabled = false;
                        menuModdingAssets.Enabled = false;
                        menuModdingImport.Enabled = false;
                            menuModdingSettingsGameInfo.Enabled = true;
                            menuModdingSettingsChapters.Enabled = false;
                            menuModdingContentMountAdvanced.Enabled = false;
                            menuModdingSettingsMenu.Enabled = true;
                            menuModdingSettingsStartingMaps.Enabled = true;
                            menuScriptsHudEditor.Enabled = false;
                        menuModdingExport.Enabled = false;
                    menuLevelDesign.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuLevelDesignReload.Enabled = false;
                        menuLevelDesignBatchCompiler.Enabled = false;
                        menuLevelDesignCrafty.Enabled = true;
                        menuLevelDesignFogPreviewer.Enabled = false;
                        menuLevelDesignHammer.Enabled = true;
                        menuLevelDesignMapsrc.Enabled = false;
                        menuLevelDesignPrefabs.Enabled = true;
                        menuLevelDesignTerrainGenerator.Enabled = false;
                        menuLevelDesignRunMap.Enabled = true;
                        menuLevelDesignDecompile.Enabled = true;
                    menuModeling.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuModelingReload.Enabled = false;
                        menuModelingHLMV.Enabled = true;
                        menuModelingEditor2.Enabled = false;
                        menuModelingCompile.Enabled = false;
                        menuModelingDecompile.Enabled = false;
                    menuMaterials.Enabled = (toolsMods.EditValue != null && toolsMods.EditValue.ToString() != string.Empty);
                        menuMaterialsReload.Enabled = false;
                        menuMaterialsEditor.Enabled = false;
                        menuMaterialsSkyboxEditor.Enabled = true;
                    menuParticles.Enabled = false;
                    menuChoreography.Enabled = false;
                        menuChoreographyFaceposer.Enabled = false;
                    menuSound.Enabled = false;
                    menuScripts.Enabled = false;
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

        private void toolsReload_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (instance.modProcess != null)
            {
                instance.Command("+reload");
            }
        }

        private void updateToolsGames()
        {
            if (launcher == null)
                return;

            string currentGame = (toolsGames.EditValue != null ? toolsGames.EditValue.ToString() : string.Empty);
            Dictionary<string, Game> gamesList = launcher.GetGamesList();

            // Image list
            repositoryGamesCombo.Items.Clear();
            imageCollection1.Clear();
            if (gamesList.Count > 0)
                foreach (KeyValuePair<string, Game> item in gamesList)
                {
                    Bitmap icon = null;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Assets/GameIcons/" + item.Key + ".ico"))
                        icon = (Bitmap)Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Assets/GameIcons/" + item.Key + ".ico");
                    else if (File.Exists(item.Value.getExePath()))
                    {
                        try
                        {
                            icon = Icon.ExtractAssociatedIcon(item.Value.getExePath()).ToBitmap();
                        } catch (FileNotFoundException)
                        {

                        }
                    }

                    if (icon != null)
                        imageCollection1.Images.Add(icon);

                    repositoryGamesCombo.Items.Add(new ImageComboBoxItem(item.Key, item.Key, imageCollection1.Images.Count - 1));
                }

            if (gamesList.Count > 0 && gamesList.ContainsKey(currentGame))
                toolsGames.EditValue = currentGame;
            else if (gamesList.Count > 0)
                toolsGames.EditValue = repositoryGamesCombo.Items[0];
            else
                toolsGames.EditValue = string.Empty;

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

        private void menuSound_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item == menuSoundSoundscapeEditor)
            {
                SoundscapeEditor editor = new SoundscapeEditor(launcher);
                editor.ShowDialog();
            }
            else if(e.Item == menuSoundCreateManifest)
            {
                Soundscape.CreateManifest(launcher);
            }
        }

        private void menuLevelDesignPrefabsWorkshopButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            BlueprintCategoriesDialog form = new BlueprintCategoriesDialog(launcher, "prefabs");
            form.ShowDialog();
        }

        private void toolsRunPopup_BeforePopup(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Run presets
            List<RunPreset> runPresets = RunDialog.GetPresets(launcher);

            toolsRunPopup.ItemLinks.Clear();

            foreach (RunPreset runPreset in runPresets)
            {
                BarButtonItem item = new BarButtonItem(barManager, runPreset.name);
                item.ItemClick += menuModdingRun_ItemClick;
                item.Tag = runPreset;
                toolsRunPopup.AddItem(item);
            }

            BarItemLink link = toolsRunPopup.AddItem(toolsRunPopupExpert);
            link.BeginGroup = true;
        }

        private void becomePatronButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("https://www.patreon.com/moddingassets");
        }
    }
}