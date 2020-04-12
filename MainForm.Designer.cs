namespace SourceModdingTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.tools = new DevExpress.XtraBars.Bar();
            this.toolsGames = new DevExpress.XtraBars.BarEditItem();
            this.repositoryGamesCombo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.toolsMods = new DevExpress.XtraBars.BarEditItem();
            this.repositoryModsCombo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.toolsRun = new DevExpress.XtraBars.BarButtonItem();
            this.toolsRunPopup = new DevExpress.XtraBars.PopupMenu(this.components);
            this.toolsRunPopupRun = new DevExpress.XtraBars.BarButtonItem();
            this.toolsRunPopupRunFullscreen = new DevExpress.XtraBars.BarButtonItem();
            this.toolsRunPopupIngameTools = new DevExpress.XtraBars.BarButtonItem();
            this.toolsStop = new DevExpress.XtraBars.BarButtonItem();
            this.menu = new DevExpress.XtraBars.Bar();
            this.menuFile = new DevExpress.XtraBars.BarSubItem();
            this.menuFileNew = new DevExpress.XtraBars.BarButtonItem();
            this.menuFileLibraries = new DevExpress.XtraBars.BarButtonItem();
            this.menuFileExit = new DevExpress.XtraBars.BarButtonItem();
            this.menuModding = new DevExpress.XtraBars.BarSubItem();
            this.menuModdingRun = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingRunFullscreen = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingIngameTools = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingOpenFolder = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingClean = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingImport2 = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingSettings = new DevExpress.XtraBars.BarSubItem();
            this.menuModdingSettingsGameInfo = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingSettingsChapters = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingSettingsMenu = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingHudEditor = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingFileExplorer = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingExport = new DevExpress.XtraBars.BarButtonItem();
            this.menuModdingDelete = new DevExpress.XtraBars.BarButtonItem();
            this.menuLevelDesign = new DevExpress.XtraBars.BarSubItem();
            this.menuLevelDesignRunMap = new DevExpress.XtraBars.BarButtonItem();
            this.menuLevelDesignHammer = new DevExpress.XtraBars.BarButtonItem();
            this.menuLevelDesignFogPreviewer = new DevExpress.XtraBars.BarButtonItem();
            this.menuLevelDesignPrefabs = new DevExpress.XtraBars.BarButtonItem();
            this.menuLevelDesignMapsrc = new DevExpress.XtraBars.BarButtonItem();
            this.menuLevelDesignCrafty = new DevExpress.XtraBars.BarButtonItem();
            this.menuLevelDesignTerrainGenerator = new DevExpress.XtraBars.BarButtonItem();
            this.menuLevelDesignBatchCompiler = new DevExpress.XtraBars.BarButtonItem();
            this.menuModeling = new DevExpress.XtraBars.BarSubItem();
            this.menuModelingHLMV = new DevExpress.XtraBars.BarButtonItem();
            this.menuModelingPropper = new DevExpress.XtraBars.BarButtonItem();
            this.menuModelingVMFtoMDL = new DevExpress.XtraBars.BarButtonItem();
            this.menuModelingCrowbar = new DevExpress.XtraBars.BarButtonItem();
            this.menuMaterials = new DevExpress.XtraBars.BarSubItem();
            this.menuMaterialsEditor = new DevExpress.XtraBars.BarButtonItem();
            this.menuParticles = new DevExpress.XtraBars.BarSubItem();
            this.menuParticlesManifestGenerator = new DevExpress.XtraBars.BarButtonItem();
            this.menuChoreography = new DevExpress.XtraBars.BarSubItem();
            this.menuChoreographyFaceposer = new DevExpress.XtraBars.BarButtonItem();
            this.status = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.modProcessUpdater = new System.Windows.Forms.Timer(this.components);
            this.menuModdingSettingsContentMount = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryGamesCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryModsCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolsRunPopup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.tools,
            this.menu,
            this.status});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.toolsGames,
            this.toolsMods,
            this.toolsRun,
            this.barButtonItem4,
            this.menuLevelDesignHammer,
            this.menuModding,
            this.menuModdingRun,
            this.menuFile,
            this.menuFileExit,
            this.menuModdingOpenFolder,
            this.menuFileNew,
            this.menuModdingSettings,
            this.menuModdingSettingsGameInfo,
            this.menuModdingSettingsChapters,
            this.toolsStop,
            this.menuModdingExport,
            this.menuModdingSettingsMenu,
            this.menuModdingFileExplorer,
            this.menuModdingClean,
            this.menuLevelDesignCrafty,
            this.menuLevelDesignBatchCompiler,
            this.menuModelingCrowbar,
            this.menuModelingHLMV,
            this.menuChoreographyFaceposer,
            this.menuLevelDesignTerrainGenerator,
            this.menuModdingIngameTools,
            this.menuModelingVMFtoMDL,
            this.menuModeling,
            this.menuChoreography,
            this.menuLevelDesign,
            this.menuMaterials,
            this.menuParticles,
            this.menuParticlesManifestGenerator,
            this.menuModelingPropper,
            this.menuLevelDesignPrefabs,
            this.menuLevelDesignMapsrc,
            this.menuModdingRunFullscreen,
            this.menuMaterialsEditor,
            this.menuModdingHudEditor,
            this.barButtonItem6,
            this.barButtonItem7,
            this.barButtonItem8,
            this.toolsRunPopupRun,
            this.toolsRunPopupRunFullscreen,
            this.toolsRunPopupIngameTools,
            this.menuModdingImport2,
            this.menuModdingDelete,
            this.menuLevelDesignFogPreviewer,
            this.menuLevelDesignRunMap,
            this.menuFileLibraries,
            this.menuModdingSettingsContentMount});
            this.barManager.MainMenu = this.menu;
            this.barManager.MaxItemId = 85;
            this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryGamesCombo,
            this.repositoryModsCombo,
            this.repositoryItemTextEdit1,
            this.repositoryItemButtonEdit1});
            this.barManager.StatusBar = this.status;
            // 
            // tools
            // 
            this.tools.BarName = "Tools";
            this.tools.DockCol = 0;
            this.tools.DockRow = 1;
            this.tools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.tools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.toolsGames),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolsMods),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolsRun),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolsStop)});
            this.tools.OptionsBar.AllowQuickCustomization = false;
            this.tools.OptionsBar.DrawDragBorder = false;
            this.tools.OptionsBar.UseWholeRow = true;
            this.tools.Text = "Tools";
            // 
            // toolsGames
            // 
            this.toolsGames.Caption = "barEditItem1";
            this.toolsGames.Edit = this.repositoryGamesCombo;
            this.toolsGames.EditWidth = 128;
            this.toolsGames.Id = 2;
            this.toolsGames.Name = "toolsGames";
            this.toolsGames.EditValueChanged += new System.EventHandler(this.toolsGames_EditValueChanged);
            // 
            // repositoryGamesCombo
            // 
            this.repositoryGamesCombo.AutoHeight = false;
            this.repositoryGamesCombo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryGamesCombo.Name = "repositoryGamesCombo";
            this.repositoryGamesCombo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // toolsMods
            // 
            this.toolsMods.Caption = "barEditItem2";
            this.toolsMods.Edit = this.repositoryModsCombo;
            this.toolsMods.EditWidth = 128;
            this.toolsMods.Id = 3;
            this.toolsMods.Name = "toolsMods";
            this.toolsMods.EditValueChanged += new System.EventHandler(this.toolsMods_EditValueChanged);
            // 
            // repositoryModsCombo
            // 
            this.repositoryModsCombo.AutoHeight = false;
            this.repositoryModsCombo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryModsCombo.Name = "repositoryModsCombo";
            this.repositoryModsCombo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // toolsRun
            // 
            this.toolsRun.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.toolsRun.Caption = "Run";
            this.toolsRun.DropDownControl = this.toolsRunPopup;
            this.toolsRun.Id = 8;
            this.toolsRun.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolsRun.ImageOptions.Image")));
            this.toolsRun.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("toolsRun.ImageOptions.SvgImage")));
            this.toolsRun.Name = "toolsRun";
            this.toolsRun.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolsRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModdingRun_ItemClick);
            // 
            // toolsRunPopup
            // 
            this.toolsRunPopup.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.toolsRunPopupRun),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolsRunPopupRunFullscreen),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolsRunPopupIngameTools)});
            this.toolsRunPopup.Manager = this.barManager;
            this.toolsRunPopup.Name = "toolsRunPopup";
            // 
            // toolsRunPopupRun
            // 
            this.toolsRunPopupRun.Caption = "Run";
            this.toolsRunPopupRun.Id = 73;
            this.toolsRunPopupRun.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("toolsRunPopupRun.ImageOptions.SvgImage")));
            this.toolsRunPopupRun.Name = "toolsRunPopupRun";
            this.toolsRunPopupRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModdingRun_ItemClick);
            // 
            // toolsRunPopupRunFullscreen
            // 
            this.toolsRunPopupRunFullscreen.Caption = "Run (Full Screen)";
            this.toolsRunPopupRunFullscreen.Id = 74;
            this.toolsRunPopupRunFullscreen.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("toolsRunPopupRunFullscreen.ImageOptions.SvgImage")));
            this.toolsRunPopupRunFullscreen.Name = "toolsRunPopupRunFullscreen";
            this.toolsRunPopupRunFullscreen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModdingRun_ItemClick);
            // 
            // toolsRunPopupIngameTools
            // 
            this.toolsRunPopupIngameTools.Caption = "Ingame Tools";
            this.toolsRunPopupIngameTools.Id = 75;
            this.toolsRunPopupIngameTools.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("toolsRunPopupIngameTools.ImageOptions.SvgImage")));
            this.toolsRunPopupIngameTools.Name = "toolsRunPopupIngameTools";
            this.toolsRunPopupIngameTools.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModdingRun_ItemClick);
            // 
            // toolsStop
            // 
            this.toolsStop.Caption = "Stop";
            this.toolsStop.Id = 27;
            this.toolsStop.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolsStop.ImageOptions.Image")));
            this.toolsStop.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolsStop.ImageOptions.LargeImage")));
            this.toolsStop.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("toolsStop.ImageOptions.SvgImage")));
            this.toolsStop.Name = "toolsStop";
            this.toolsStop.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.toolsStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolsStop_ItemClick);
            // 
            // menu
            // 
            this.menu.BarName = "Main menu";
            this.menu.DockCol = 0;
            this.menu.DockRow = 0;
            this.menu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.menu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModding),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuLevelDesign),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModeling),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuMaterials),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuParticles),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuChoreography)});
            this.menu.OptionsBar.AllowQuickCustomization = false;
            this.menu.OptionsBar.DrawDragBorder = false;
            this.menu.OptionsBar.MultiLine = true;
            this.menu.OptionsBar.UseWholeRow = true;
            this.menu.Text = "Main menu";
            // 
            // menuFile
            // 
            this.menuFile.Caption = "File";
            this.menuFile.Id = 19;
            this.menuFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFileNew),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.menuFileLibraries, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFileExit, true)});
            this.menuFile.Name = "menuFile";
            this.menuFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuFile_ItemClick);
            // 
            // menuFileNew
            // 
            this.menuFileNew.Caption = "New";
            this.menuFileNew.Id = 22;
            this.menuFileNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuFileNew.ImageOptions.SvgImage")));
            this.menuFileNew.Name = "menuFileNew";
            this.menuFileNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuFile_ItemClick);
            // 
            // menuFileLibraries
            // 
            this.menuFileLibraries.Caption = "Steam Libraries";
            this.menuFileLibraries.Id = 83;
            this.menuFileLibraries.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuFileLibraries.ImageOptions.SvgImage")));
            this.menuFileLibraries.Name = "menuFileLibraries";
            this.menuFileLibraries.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuFile_ItemClick);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Caption = "Exit";
            this.menuFileExit.Id = 20;
            this.menuFileExit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuFileExit.ImageOptions.SvgImage")));
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuFile_ItemClick);
            // 
            // menuModding
            // 
            this.menuModding.Caption = "Modding";
            this.menuModding.Id = 17;
            this.menuModding.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingRun),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.menuModdingRunFullscreen, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingIngameTools),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingOpenFolder),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingClean),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingImport2, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingSettings),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, false, this.menuModdingHudEditor, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingFileExplorer),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingDelete, true)});
            this.menuModding.Name = "menuModding";
            // 
            // menuModdingRun
            // 
            this.menuModdingRun.Caption = "Run";
            this.menuModdingRun.Id = 18;
            this.menuModdingRun.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingRun.ImageOptions.SvgImage")));
            this.menuModdingRun.Name = "menuModdingRun";
            this.menuModdingRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModdingRun_ItemClick);
            // 
            // menuModdingRunFullscreen
            // 
            this.menuModdingRunFullscreen.Caption = "Run (Full screen)";
            this.menuModdingRunFullscreen.Id = 56;
            this.menuModdingRunFullscreen.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingRunFullscreen.ImageOptions.SvgImage")));
            this.menuModdingRunFullscreen.Name = "menuModdingRunFullscreen";
            this.menuModdingRunFullscreen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModdingRun_ItemClick);
            // 
            // menuModdingIngameTools
            // 
            this.menuModdingIngameTools.Caption = "Ingame Tools";
            this.menuModdingIngameTools.Id = 43;
            this.menuModdingIngameTools.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingIngameTools.ImageOptions.SvgImage")));
            this.menuModdingIngameTools.Name = "menuModdingIngameTools";
            this.menuModdingIngameTools.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModdingRun_ItemClick);
            // 
            // menuModdingOpenFolder
            // 
            this.menuModdingOpenFolder.Caption = "Open folder";
            this.menuModdingOpenFolder.Id = 21;
            this.menuModdingOpenFolder.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingOpenFolder.ImageOptions.SvgImage")));
            this.menuModdingOpenFolder.Name = "menuModdingOpenFolder";
            this.menuModdingOpenFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModding_ItemClick);
            // 
            // menuModdingClean
            // 
            this.menuModdingClean.Caption = "Clean";
            this.menuModdingClean.Id = 34;
            this.menuModdingClean.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingClean.ImageOptions.SvgImage")));
            this.menuModdingClean.Name = "menuModdingClean";
            this.menuModdingClean.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModding_ItemClick);
            // 
            // menuModdingImport2
            // 
            this.menuModdingImport2.Caption = "Import assets";
            this.menuModdingImport2.Id = 77;
            this.menuModdingImport2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingImport2.ImageOptions.SvgImage")));
            this.menuModdingImport2.Name = "menuModdingImport2";
            this.menuModdingImport2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModding_ItemClick);
            // 
            // menuModdingSettings
            // 
            this.menuModdingSettings.Caption = "Settings";
            this.menuModdingSettings.Id = 24;
            this.menuModdingSettings.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingSettingsGameInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingSettingsChapters),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingSettingsMenu),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModdingSettingsContentMount)});
            this.menuModdingSettings.Name = "menuModdingSettings";
            // 
            // menuModdingSettingsGameInfo
            // 
            this.menuModdingSettingsGameInfo.Caption = "Game info";
            this.menuModdingSettingsGameInfo.Id = 25;
            this.menuModdingSettingsGameInfo.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingSettingsGameInfo.ImageOptions.SvgImage")));
            this.menuModdingSettingsGameInfo.Name = "menuModdingSettingsGameInfo";
            this.menuModdingSettingsGameInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModdingSettings_ItemClick);
            // 
            // menuModdingSettingsChapters
            // 
            this.menuModdingSettingsChapters.Caption = "Chapters";
            this.menuModdingSettingsChapters.Id = 26;
            this.menuModdingSettingsChapters.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingSettingsChapters.ImageOptions.SvgImage")));
            this.menuModdingSettingsChapters.Name = "menuModdingSettingsChapters";
            this.menuModdingSettingsChapters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModdingSettings_ItemClick);
            // 
            // menuModdingSettingsMenu
            // 
            this.menuModdingSettingsMenu.Caption = "Menu";
            this.menuModdingSettingsMenu.Id = 32;
            this.menuModdingSettingsMenu.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingSettingsMenu.ImageOptions.SvgImage")));
            this.menuModdingSettingsMenu.Name = "menuModdingSettingsMenu";
            this.menuModdingSettingsMenu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModdingSettings_ItemClick);
            // 
            // menuModdingHudEditor
            // 
            this.menuModdingHudEditor.Caption = "Hud Editor";
            this.menuModdingHudEditor.Id = 59;
            this.menuModdingHudEditor.Name = "menuModdingHudEditor";
            this.menuModdingHudEditor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModding_ItemClick);
            // 
            // menuModdingFileExplorer
            // 
            this.menuModdingFileExplorer.Caption = "File Explorer";
            this.menuModdingFileExplorer.Id = 33;
            this.menuModdingFileExplorer.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingFileExplorer.ImageOptions.SvgImage")));
            this.menuModdingFileExplorer.Name = "menuModdingFileExplorer";
            this.menuModdingFileExplorer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModding_ItemClick);
            // 
            // menuModdingExport
            // 
            this.menuModdingExport.Caption = "Assets copier";
            this.menuModdingExport.Id = 29;
            this.menuModdingExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingExport.ImageOptions.SvgImage")));
            this.menuModdingExport.Name = "menuModdingExport";
            this.menuModdingExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModding_ItemClick);
            // 
            // menuModdingDelete
            // 
            this.menuModdingDelete.Caption = "Delete Mod";
            this.menuModdingDelete.Id = 78;
            this.menuModdingDelete.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.menuModdingDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuModdingDelete.ImageOptions.SvgImage")));
            this.menuModdingDelete.ItemInMenuAppearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.menuModdingDelete.ItemInMenuAppearance.Normal.Options.UseForeColor = true;
            this.menuModdingDelete.Name = "menuModdingDelete";
            this.menuModdingDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModding_ItemClick);
            // 
            // menuLevelDesign
            // 
            this.menuLevelDesign.Caption = "Level Design";
            this.menuLevelDesign.Id = 47;
            this.menuLevelDesign.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuLevelDesignRunMap),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuLevelDesignHammer, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuLevelDesignFogPreviewer),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuLevelDesignPrefabs, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, false, this.menuLevelDesignMapsrc, false),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuLevelDesignCrafty, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuLevelDesignTerrainGenerator),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuLevelDesignBatchCompiler)});
            this.menuLevelDesign.Name = "menuLevelDesign";
            // 
            // menuLevelDesignRunMap
            // 
            this.menuLevelDesignRunMap.Caption = "Run Map";
            this.menuLevelDesignRunMap.Id = 81;
            this.menuLevelDesignRunMap.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuLevelDesignRunMap.ImageOptions.SvgImage")));
            this.menuLevelDesignRunMap.Name = "menuLevelDesignRunMap";
            this.menuLevelDesignRunMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuLevelDesign_ItemClick);
            // 
            // menuLevelDesignHammer
            // 
            this.menuLevelDesignHammer.Caption = "Hammer";
            this.menuLevelDesignHammer.Id = 16;
            this.menuLevelDesignHammer.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("menuLevelDesignHammer.ImageOptions.Image")));
            this.menuLevelDesignHammer.Name = "menuLevelDesignHammer";
            this.menuLevelDesignHammer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuLevelDesign_ItemClick);
            // 
            // menuLevelDesignFogPreviewer
            // 
            this.menuLevelDesignFogPreviewer.Caption = "Fog Previewer";
            this.menuLevelDesignFogPreviewer.Id = 79;
            this.menuLevelDesignFogPreviewer.Name = "menuLevelDesignFogPreviewer";
            this.menuLevelDesignFogPreviewer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuLevelDesign_ItemClick);
            // 
            // menuLevelDesignPrefabs
            // 
            this.menuLevelDesignPrefabs.Caption = "Open Prefabs folder";
            this.menuLevelDesignPrefabs.Id = 52;
            this.menuLevelDesignPrefabs.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuLevelDesignPrefabs.ImageOptions.SvgImage")));
            this.menuLevelDesignPrefabs.Name = "menuLevelDesignPrefabs";
            this.menuLevelDesignPrefabs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuLevelDesign_ItemClick);
            // 
            // menuLevelDesignMapsrc
            // 
            this.menuLevelDesignMapsrc.Caption = "Open Mapsrc folder";
            this.menuLevelDesignMapsrc.Id = 53;
            this.menuLevelDesignMapsrc.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuLevelDesignMapsrc.ImageOptions.SvgImage")));
            this.menuLevelDesignMapsrc.Name = "menuLevelDesignMapsrc";
            this.menuLevelDesignMapsrc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuLevelDesign_ItemClick);
            // 
            // menuLevelDesignCrafty
            // 
            this.menuLevelDesignCrafty.Caption = "Crafty (external)";
            this.menuLevelDesignCrafty.Id = 36;
            this.menuLevelDesignCrafty.Name = "menuLevelDesignCrafty";
            this.menuLevelDesignCrafty.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuLevelDesign_ItemClick);
            // 
            // menuLevelDesignTerrainGenerator
            // 
            this.menuLevelDesignTerrainGenerator.Caption = "Terrain Generator (external)";
            this.menuLevelDesignTerrainGenerator.Id = 42;
            this.menuLevelDesignTerrainGenerator.Name = "menuLevelDesignTerrainGenerator";
            this.menuLevelDesignTerrainGenerator.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuLevelDesign_ItemClick);
            // 
            // menuLevelDesignBatchCompiler
            // 
            this.menuLevelDesignBatchCompiler.Caption = "Batch Compiler (external)";
            this.menuLevelDesignBatchCompiler.Id = 38;
            this.menuLevelDesignBatchCompiler.Name = "menuLevelDesignBatchCompiler";
            this.menuLevelDesignBatchCompiler.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuLevelDesign_ItemClick);
            // 
            // menuModeling
            // 
            this.menuModeling.Caption = "Modeling";
            this.menuModeling.Id = 45;
            this.menuModeling.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModelingHLMV),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModelingPropper),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModelingVMFtoMDL, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuModelingCrowbar, true)});
            this.menuModeling.Name = "menuModeling";
            // 
            // menuModelingHLMV
            // 
            this.menuModelingHLMV.Caption = "HLMV";
            this.menuModelingHLMV.Id = 40;
            this.menuModelingHLMV.Name = "menuModelingHLMV";
            this.menuModelingHLMV.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModeling_ItemClick);
            // 
            // menuModelingPropper
            // 
            this.menuModelingPropper.Caption = "Hammer (Propper)";
            this.menuModelingPropper.Id = 51;
            this.menuModelingPropper.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("menuModelingPropper.ImageOptions.Image")));
            this.menuModelingPropper.Name = "menuModelingPropper";
            this.menuModelingPropper.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModeling_ItemClick);
            // 
            // menuModelingVMFtoMDL
            // 
            this.menuModelingVMFtoMDL.Caption = "VMF to MDL";
            this.menuModelingVMFtoMDL.Id = 44;
            this.menuModelingVMFtoMDL.Name = "menuModelingVMFtoMDL";
            this.menuModelingVMFtoMDL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModeling_ItemClick);
            // 
            // menuModelingCrowbar
            // 
            this.menuModelingCrowbar.Caption = "Crowbar (external)";
            this.menuModelingCrowbar.Id = 39;
            this.menuModelingCrowbar.Name = "menuModelingCrowbar";
            this.menuModelingCrowbar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModeling_ItemClick);
            // 
            // menuMaterials
            // 
            this.menuMaterials.Caption = "Materials";
            this.menuMaterials.Id = 48;
            this.menuMaterials.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuMaterialsEditor)});
            this.menuMaterials.Name = "menuMaterials";
            // 
            // menuMaterialsEditor
            // 
            this.menuMaterialsEditor.Caption = "Material Editor";
            this.menuMaterialsEditor.Id = 58;
            this.menuMaterialsEditor.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuMaterialsEditor.ImageOptions.SvgImage")));
            this.menuMaterialsEditor.Name = "menuMaterialsEditor";
            this.menuMaterialsEditor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuMaterials_ItemClick);
            // 
            // menuParticles
            // 
            this.menuParticles.Caption = "Particles";
            this.menuParticles.Id = 49;
            this.menuParticles.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuParticlesManifestGenerator)});
            this.menuParticles.Name = "menuParticles";
            // 
            // menuParticlesManifestGenerator
            // 
            this.menuParticlesManifestGenerator.Caption = "Generate manifest";
            this.menuParticlesManifestGenerator.Id = 50;
            this.menuParticlesManifestGenerator.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuParticlesManifestGenerator.ImageOptions.SvgImage")));
            this.menuParticlesManifestGenerator.Name = "menuParticlesManifestGenerator";
            this.menuParticlesManifestGenerator.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuParticles_ItemClick);
            // 
            // menuChoreography
            // 
            this.menuChoreography.Caption = "Choreography";
            this.menuChoreography.Id = 46;
            this.menuChoreography.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuChoreographyFaceposer)});
            this.menuChoreography.Name = "menuChoreography";
            this.menuChoreography.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // menuChoreographyFaceposer
            // 
            this.menuChoreographyFaceposer.Caption = "Faceposer";
            this.menuChoreographyFaceposer.Id = 41;
            this.menuChoreographyFaceposer.Name = "menuChoreographyFaceposer";
            this.menuChoreographyFaceposer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuChoreography_ItemClick);
            // 
            // status
            // 
            this.status.BarName = "Status bar";
            this.status.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.status.DockCol = 0;
            this.status.DockRow = 0;
            this.status.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.status.OptionsBar.AllowQuickCustomization = false;
            this.status.OptionsBar.DrawDragBorder = false;
            this.status.OptionsBar.UseWholeRow = true;
            this.status.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Size = new System.Drawing.Size(944, 54);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 573);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(944, 18);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 54);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 519);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(944, 54);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 519);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Test";
            this.barButtonItem4.Id = 14;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "Reload Map";
            this.barButtonItem6.Id = 63;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "barButtonItem7";
            this.barButtonItem7.Id = 71;
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "barButtonItem8";
            this.barButtonItem8.Id = 72;
            this.barButtonItem8.Name = "barButtonItem8";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 519);
            this.panel1.TabIndex = 9;
            // 
            // modProcessUpdater
            // 
            this.modProcessUpdater.Interval = 1000;
            this.modProcessUpdater.Tick += new System.EventHandler(this.modProcessUpdater_Tick);
            // 
            // menuModdingSettingsContentMount
            // 
            this.menuModdingSettingsContentMount.Caption = "Content Mount";
            this.menuModdingSettingsContentMount.Id = 84;
            this.menuModdingSettingsContentMount.Name = "menuModdingSettingsContentMount";
            this.menuModdingSettingsContentMount.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuModdingSettings_ItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 591);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "XBLAH\'s Source Modding Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModForm_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResizeEnd += new System.EventHandler(this.ModForm_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryGamesCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryModsCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolsRunPopup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar tools;
        private DevExpress.XtraBars.Bar menu;
        private DevExpress.XtraBars.Bar status;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem toolsGames;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox toolsGamesRepository;
        private DevExpress.XtraBars.BarEditItem toolsMods;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox toolsModsRepository;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem toolsRun;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem menuLevelDesignHammer;
        private DevExpress.XtraBars.BarSubItem menuModding;
        private DevExpress.XtraBars.BarButtonItem menuModdingRun;
        private DevExpress.XtraBars.BarSubItem menuFile;
        private DevExpress.XtraBars.BarButtonItem menuFileExit;
        private DevExpress.XtraBars.BarButtonItem menuModdingOpenFolder;
        private DevExpress.XtraBars.BarButtonItem menuFileNew;
        private DevExpress.XtraBars.BarSubItem menuModdingSettings;
        private DevExpress.XtraBars.BarButtonItem menuModdingSettingsGameInfo;
        private DevExpress.XtraBars.BarButtonItem menuModdingSettingsChapters;
        private DevExpress.XtraBars.BarButtonItem toolsStop;
        private DevExpress.XtraBars.BarButtonItem menuModdingExport;
        private DevExpress.XtraBars.BarButtonItem menuModdingSettingsMenu;
        private DevExpress.XtraBars.BarButtonItem menuModdingFileExplorer;
        private DevExpress.XtraBars.BarButtonItem menuModdingClean;
        private DevExpress.XtraBars.BarButtonItem menuLevelDesignCrafty;
        private DevExpress.XtraBars.BarButtonItem menuLevelDesignBatchCompiler;
        private DevExpress.XtraBars.BarButtonItem menuModelingCrowbar;
        private DevExpress.XtraBars.BarButtonItem menuModelingHLMV;
        private DevExpress.XtraBars.BarButtonItem menuChoreographyFaceposer;
        private DevExpress.XtraBars.BarButtonItem menuLevelDesignTerrainGenerator;
        private DevExpress.XtraBars.BarButtonItem menuModdingIngameTools;
        private DevExpress.XtraBars.BarButtonItem menuModelingVMFtoMDL;
        private DevExpress.XtraBars.BarSubItem menuModeling;
        private DevExpress.XtraBars.BarSubItem menuChoreography;
        private DevExpress.XtraBars.BarSubItem menuLevelDesign;
        private DevExpress.XtraBars.BarSubItem menuMaterials;
        private DevExpress.XtraBars.BarSubItem menuParticles;
        private DevExpress.XtraBars.BarButtonItem menuParticlesManifestGenerator;
        private DevExpress.XtraBars.BarButtonItem menuModelingPropper;
        private DevExpress.XtraBars.BarButtonItem menuLevelDesignPrefabs;
        private DevExpress.XtraBars.BarButtonItem menuLevelDesignMapsrc;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraBars.BarButtonItem menuModdingRunFullscreen;
        private System.Windows.Forms.Timer modProcessUpdater;
        private DevExpress.XtraBars.BarButtonItem menuMaterialsEditor;
        private DevExpress.XtraBars.BarButtonItem menuModdingHudEditor;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.PopupMenu toolsRunPopup;
        private DevExpress.XtraBars.BarButtonItem toolsRunPopupRun;
        private DevExpress.XtraBars.BarButtonItem toolsRunPopupRunFullscreen;
        private DevExpress.XtraBars.BarButtonItem toolsRunPopupIngameTools;
        private DevExpress.XtraBars.BarButtonItem menuModdingImport2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryGamesCombo;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryModsCombo;
        private DevExpress.XtraBars.BarButtonItem menuModdingDelete;
        private DevExpress.XtraBars.BarButtonItem menuLevelDesignFogPreviewer;
        private DevExpress.XtraBars.BarButtonItem menuLevelDesignRunMap;
        private DevExpress.XtraBars.BarButtonItem menuFileLibraries;
        private DevExpress.XtraBars.BarButtonItem menuModdingSettingsContentMount;
    }
}