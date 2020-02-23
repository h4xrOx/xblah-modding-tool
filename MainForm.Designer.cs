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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.gamesCombo = new DevExpress.XtraBars.BarEditItem();
            this.repositoryGamesCombo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.modsCombo = new DevExpress.XtraBars.BarEditItem();
            this.repositoryModsCombo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.buttonModStart = new DevExpress.XtraBars.BarButtonItem();
            this.buttonModStop = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barFile = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barMod = new DevExpress.XtraBars.BarSubItem();
            this.barButtonRun = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonRunFullscreen = new DevExpress.XtraBars.BarButtonItem();
            this.buttonIngameTools = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonModOpenFolder = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonClear = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.importMapButton = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonGameinfo = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonChapters = new DevExpress.XtraBars.BarButtonItem();
            this.menuButton = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonClientScheme = new DevExpress.XtraBars.BarButtonItem();
            this.buttonVPKExplorer = new DevExpress.XtraBars.BarButtonItem();
            this.assetsCopierButton = new DevExpress.XtraBars.BarButtonItem();
            this.barLevelDesign = new DevExpress.XtraBars.BarSubItem();
            this.barButtonHammer = new DevExpress.XtraBars.BarButtonItem();
            this.buttonOpenPrefabsFolder = new DevExpress.XtraBars.BarButtonItem();
            this.buttonOpenMapsrcFolder = new DevExpress.XtraBars.BarButtonItem();
            this.buttonCrafty = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.buttonBatchCompiler = new DevExpress.XtraBars.BarButtonItem();
            this.barModeling = new DevExpress.XtraBars.BarSubItem();
            this.buttonHLMV = new DevExpress.XtraBars.BarButtonItem();
            this.buttonHammerPropper = new DevExpress.XtraBars.BarButtonItem();
            this.buttonVMFtoMDL = new DevExpress.XtraBars.BarButtonItem();
            this.buttonCrowbar = new DevExpress.XtraBars.BarButtonItem();
            this.barMaterials = new DevExpress.XtraBars.BarSubItem();
            this.buttonMaterialEditor = new DevExpress.XtraBars.BarButtonItem();
            this.barParticles = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barChoreography = new DevExpress.XtraBars.BarSubItem();
            this.buttonFaceposer = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.modProcessUpdater = new System.Windows.Forms.Timer(this.components);
            this.imageCollection3 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryGamesCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryModsCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection3)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.gamesCombo,
            this.modsCombo,
            this.buttonModStart,
            this.barButtonItem4,
            this.barButtonHammer,
            this.barMod,
            this.barButtonRun,
            this.barFile,
            this.barButtonItem1,
            this.barButtonModOpenFolder,
            this.barButtonItem3,
            this.barSubItem3,
            this.barButtonGameinfo,
            this.barButtonChapters,
            this.buttonModStop,
            this.assetsCopierButton,
            this.barSubItem1,
            this.importMapButton,
            this.menuButton,
            this.buttonVPKExplorer,
            this.barButtonClear,
            this.buttonCrafty,
            this.buttonBatchCompiler,
            this.buttonCrowbar,
            this.buttonHLMV,
            this.buttonFaceposer,
            this.barButtonItem5,
            this.buttonIngameTools,
            this.buttonVMFtoMDL,
            this.barModeling,
            this.barChoreography,
            this.barLevelDesign,
            this.barMaterials,
            this.barParticles,
            this.barButtonItem2,
            this.buttonHammerPropper,
            this.buttonOpenPrefabsFolder,
            this.buttonOpenMapsrcFolder,
            this.barButtonRunFullscreen,
            this.buttonMaterialEditor,
            this.barButtonClientScheme,
            this.barButtonItem6});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 70;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryGamesCombo,
            this.repositoryModsCombo,
            this.repositoryItemTextEdit1,
            this.repositoryItemButtonEdit1});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.gamesCombo),
            new DevExpress.XtraBars.LinkPersistInfo(this.modsCombo),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonModStart),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonModStop)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // gamesCombo
            // 
            this.gamesCombo.Caption = "barEditItem1";
            this.gamesCombo.Edit = this.repositoryGamesCombo;
            this.gamesCombo.EditWidth = 128;
            this.gamesCombo.Id = 2;
            this.gamesCombo.Name = "gamesCombo";
            this.gamesCombo.EditValueChanged += new System.EventHandler(this.gamesCombo_EditValueChanged);
            // 
            // repositoryGamesCombo
            // 
            this.repositoryGamesCombo.AutoHeight = false;
            this.repositoryGamesCombo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryGamesCombo.Name = "repositoryGamesCombo";
            this.repositoryGamesCombo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // modsCombo
            // 
            this.modsCombo.Caption = "barEditItem2";
            this.modsCombo.Edit = this.repositoryModsCombo;
            this.modsCombo.EditWidth = 128;
            this.modsCombo.Id = 3;
            this.modsCombo.Name = "modsCombo";
            this.modsCombo.EditValueChanged += new System.EventHandler(this.modsCombo_EditValueChanged);
            // 
            // repositoryModsCombo
            // 
            this.repositoryModsCombo.AutoHeight = false;
            this.repositoryModsCombo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryModsCombo.Name = "repositoryModsCombo";
            this.repositoryModsCombo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // buttonModStart
            // 
            this.buttonModStart.Caption = "Start";
            this.buttonModStart.Id = 8;
            this.buttonModStart.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonModStart.ImageOptions.Image")));
            this.buttonModStart.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonModStart.ImageOptions.SvgImage")));
            this.buttonModStart.Name = "buttonModStart";
            this.buttonModStart.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.buttonModStart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonModStart_ItemClick);
            // 
            // buttonModStop
            // 
            this.buttonModStop.Caption = "Stop";
            this.buttonModStop.Id = 27;
            this.buttonModStop.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonModStop.ImageOptions.Image")));
            this.buttonModStop.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("buttonModStop.ImageOptions.LargeImage")));
            this.buttonModStop.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonModStop.ImageOptions.SvgImage")));
            this.buttonModStop.Name = "buttonModStop";
            this.buttonModStop.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.buttonModStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonModStop_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.barMod),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLevelDesign),
            new DevExpress.XtraBars.LinkPersistInfo(this.barModeling),
            new DevExpress.XtraBars.LinkPersistInfo(this.barMaterials),
            new DevExpress.XtraBars.LinkPersistInfo(this.barParticles),
            new DevExpress.XtraBars.LinkPersistInfo(this.barChoreography)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barFile
            // 
            this.barFile.Caption = "File";
            this.barFile.Id = 19;
            this.barFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1, true)});
            this.barFile.Name = "barFile";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "New";
            this.barButtonItem3.Id = 22;
            this.barButtonItem3.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem3.ImageOptions.SvgImage")));
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Exit";
            this.barButtonItem1.Id = 20;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barMod
            // 
            this.barMod.Caption = "Modding";
            this.barMod.Id = 17;
            this.barMod.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonRun),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonRunFullscreen, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonIngameTools),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonModOpenFolder),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonClear),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonVPKExplorer, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.assetsCopierButton)});
            this.barMod.Name = "barMod";
            // 
            // barButtonRun
            // 
            this.barButtonRun.Caption = "Run";
            this.barButtonRun.Id = 18;
            this.barButtonRun.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonRun.ImageOptions.SvgImage")));
            this.barButtonRun.Name = "barButtonRun";
            this.barButtonRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonModStart_ItemClick);
            // 
            // barButtonRunFullscreen
            // 
            this.barButtonRunFullscreen.Caption = "Run (Full screen)";
            this.barButtonRunFullscreen.Id = 56;
            this.barButtonRunFullscreen.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonRunFullscreen.ImageOptions.SvgImage")));
            this.barButtonRunFullscreen.Name = "barButtonRunFullscreen";
            this.barButtonRunFullscreen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonRunFullscreen_ItemClick);
            // 
            // buttonIngameTools
            // 
            this.buttonIngameTools.Caption = "Ingame Tools";
            this.buttonIngameTools.Id = 43;
            this.buttonIngameTools.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonIngameTools.ImageOptions.SvgImage")));
            this.buttonIngameTools.Name = "buttonIngameTools";
            this.buttonIngameTools.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonIngameTools_ItemClick);
            // 
            // barButtonModOpenFolder
            // 
            this.barButtonModOpenFolder.Caption = "Open folder";
            this.barButtonModOpenFolder.Id = 21;
            this.barButtonModOpenFolder.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonModOpenFolder.ImageOptions.SvgImage")));
            this.barButtonModOpenFolder.Name = "barButtonModOpenFolder";
            this.barButtonModOpenFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonModOpenFolder_ItemClick);
            // 
            // barButtonClear
            // 
            this.barButtonClear.Caption = "Clean";
            this.barButtonClear.Id = 34;
            this.barButtonClear.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonClear.ImageOptions.SvgImage")));
            this.barButtonClear.Name = "barButtonClear";
            this.barButtonClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonClean_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Import assets";
            this.barSubItem1.Id = 30;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.importMapButton)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // importMapButton
            // 
            this.importMapButton.Caption = "From another mod";
            this.importMapButton.Id = 31;
            this.importMapButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("importMapButton.ImageOptions.SvgImage")));
            this.importMapButton.Name = "importMapButton";
            this.importMapButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.importMapButton_ItemClick);
            // 
            // barSubItem3
            // 
            this.barSubItem3.Caption = "Settings";
            this.barSubItem3.Id = 24;
            this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonGameinfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonChapters),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonClientScheme)});
            this.barSubItem3.Name = "barSubItem3";
            // 
            // barButtonGameinfo
            // 
            this.barButtonGameinfo.Caption = "Game info";
            this.barButtonGameinfo.Id = 25;
            this.barButtonGameinfo.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonGameinfo.ImageOptions.SvgImage")));
            this.barButtonGameinfo.Name = "barButtonGameinfo";
            this.barButtonGameinfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonGameinfo_ItemClick);
            // 
            // barButtonChapters
            // 
            this.barButtonChapters.Caption = "Chapters";
            this.barButtonChapters.Id = 26;
            this.barButtonChapters.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonChapters.ImageOptions.SvgImage")));
            this.barButtonChapters.Name = "barButtonChapters";
            this.barButtonChapters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonChapters_ItemClick);
            // 
            // menuButton
            // 
            this.menuButton.Caption = "Menu";
            this.menuButton.Id = 32;
            this.menuButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("menuButton.ImageOptions.SvgImage")));
            this.menuButton.Name = "menuButton";
            this.menuButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuButton_ItemClick);
            // 
            // barButtonClientScheme
            // 
            this.barButtonClientScheme.Caption = "Client Scheme";
            this.barButtonClientScheme.Id = 59;
            this.barButtonClientScheme.Name = "barButtonClientScheme";
            this.barButtonClientScheme.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonClientScheme_ItemClick);
            // 
            // buttonVPKExplorer
            // 
            this.buttonVPKExplorer.Caption = "File Explorer";
            this.buttonVPKExplorer.Id = 33;
            this.buttonVPKExplorer.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonVPKExplorer.ImageOptions.SvgImage")));
            this.buttonVPKExplorer.Name = "buttonVPKExplorer";
            this.buttonVPKExplorer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonVPKExplorer_ItemClick);
            // 
            // assetsCopierButton
            // 
            this.assetsCopierButton.Caption = "Assets copier";
            this.assetsCopierButton.Id = 29;
            this.assetsCopierButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("assetsCopierButton.ImageOptions.SvgImage")));
            this.assetsCopierButton.Name = "assetsCopierButton";
            this.assetsCopierButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.assetsCopierButton_ItemClick);
            // 
            // barLevelDesign
            // 
            this.barLevelDesign.Caption = "Level Design";
            this.barLevelDesign.Id = 47;
            this.barLevelDesign.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonHammer),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonOpenPrefabsFolder, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, false, this.buttonOpenMapsrcFolder, false),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonCrafty, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonBatchCompiler)});
            this.barLevelDesign.Name = "barLevelDesign";
            // 
            // barButtonHammer
            // 
            this.barButtonHammer.Caption = "Hammer";
            this.barButtonHammer.Id = 16;
            this.barButtonHammer.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonHammer.ImageOptions.Image")));
            this.barButtonHammer.Name = "barButtonHammer";
            this.barButtonHammer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonHammer_ItemClick);
            // 
            // buttonOpenPrefabsFolder
            // 
            this.buttonOpenPrefabsFolder.Caption = "Open Prefabs folder";
            this.buttonOpenPrefabsFolder.Id = 52;
            this.buttonOpenPrefabsFolder.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonOpenPrefabsFolder.ImageOptions.SvgImage")));
            this.buttonOpenPrefabsFolder.Name = "buttonOpenPrefabsFolder";
            this.buttonOpenPrefabsFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonOpenPrefabsFolder_ItemClick);
            // 
            // buttonOpenMapsrcFolder
            // 
            this.buttonOpenMapsrcFolder.Caption = "Open Mapsrc folder";
            this.buttonOpenMapsrcFolder.Id = 53;
            this.buttonOpenMapsrcFolder.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonOpenMapsrcFolder.ImageOptions.SvgImage")));
            this.buttonOpenMapsrcFolder.Name = "buttonOpenMapsrcFolder";
            this.buttonOpenMapsrcFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonOpenMapsrcFolder_ItemClick);
            // 
            // buttonCrafty
            // 
            this.buttonCrafty.Caption = "Crafty (external)";
            this.buttonCrafty.Id = 36;
            this.buttonCrafty.Name = "buttonCrafty";
            this.buttonCrafty.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonCrafty_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Terrain Generator (external)";
            this.barButtonItem5.Id = 42;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // buttonBatchCompiler
            // 
            this.buttonBatchCompiler.Caption = "Batch Compiler (external)";
            this.buttonBatchCompiler.Id = 38;
            this.buttonBatchCompiler.Name = "buttonBatchCompiler";
            this.buttonBatchCompiler.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonBatchCompiler_ItemClick);
            // 
            // barModeling
            // 
            this.barModeling.Caption = "Modeling";
            this.barModeling.Id = 45;
            this.barModeling.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonHLMV),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonHammerPropper),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonVMFtoMDL, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonCrowbar, true)});
            this.barModeling.Name = "barModeling";
            // 
            // buttonHLMV
            // 
            this.buttonHLMV.Caption = "HLMV";
            this.buttonHLMV.Id = 40;
            this.buttonHLMV.Name = "buttonHLMV";
            this.buttonHLMV.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonHLMV_ItemClick);
            // 
            // buttonHammerPropper
            // 
            this.buttonHammerPropper.Caption = "Hammer (Propper)";
            this.buttonHammerPropper.Id = 51;
            this.buttonHammerPropper.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonHammerPropper.ImageOptions.Image")));
            this.buttonHammerPropper.Name = "buttonHammerPropper";
            this.buttonHammerPropper.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonHammerPropper_ItemClick);
            // 
            // buttonVMFtoMDL
            // 
            this.buttonVMFtoMDL.Caption = "VMF to MDL";
            this.buttonVMFtoMDL.Id = 44;
            this.buttonVMFtoMDL.Name = "buttonVMFtoMDL";
            this.buttonVMFtoMDL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonVMFtoMDL_ItemClick);
            // 
            // buttonCrowbar
            // 
            this.buttonCrowbar.Caption = "Crowbar (external)";
            this.buttonCrowbar.Id = 39;
            this.buttonCrowbar.Name = "buttonCrowbar";
            this.buttonCrowbar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonCrowbar_ItemClick);
            // 
            // barMaterials
            // 
            this.barMaterials.Caption = "Materials";
            this.barMaterials.Id = 48;
            this.barMaterials.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonMaterialEditor)});
            this.barMaterials.Name = "barMaterials";
            // 
            // buttonMaterialEditor
            // 
            this.buttonMaterialEditor.Caption = "Material Editor";
            this.buttonMaterialEditor.Id = 58;
            this.buttonMaterialEditor.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonMaterialEditor.ImageOptions.SvgImage")));
            this.buttonMaterialEditor.Name = "buttonMaterialEditor";
            this.buttonMaterialEditor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonMaterialEditor_ItemClick);
            // 
            // barParticles
            // 
            this.barParticles.Caption = "Particles";
            this.barParticles.Id = 49;
            this.barParticles.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.barParticles.Name = "barParticles";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Generate manifest";
            this.barButtonItem2.Id = 50;
            this.barButtonItem2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem2.ImageOptions.SvgImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barChoreography
            // 
            this.barChoreography.Caption = "Choreography";
            this.barChoreography.Id = 46;
            this.barChoreography.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonFaceposer)});
            this.barChoreography.Name = "barChoreography";
            this.barChoreography.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // buttonFaceposer
            // 
            this.buttonFaceposer.Caption = "Faceposer";
            this.buttonFaceposer.Id = 41;
            this.buttonFaceposer.Name = "buttonFaceposer";
            this.buttonFaceposer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonFaceposer_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(944, 54);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 573);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(944, 18);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 54);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 519);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(944, 54);
            this.barDockControlRight.Manager = this.barManager1;
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
            // imageCollection3
            // 
            this.imageCollection3.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection3.ImageStream")));
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
            this.Text = "Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModForm_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResizeEnd += new System.EventHandler(this.ModForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.ModForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryGamesCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryModsCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem gamesCombo;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryGamesCombo;
        private DevExpress.XtraBars.BarEditItem modsCombo;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryModsCombo;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem buttonModStart;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonHammer;
        private DevExpress.XtraBars.BarSubItem barMod;
        private DevExpress.XtraBars.BarButtonItem barButtonRun;
        private DevExpress.XtraBars.BarSubItem barFile;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonModOpenFolder;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarSubItem barSubItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonGameinfo;
        private DevExpress.XtraBars.BarButtonItem barButtonChapters;
        private DevExpress.XtraBars.BarButtonItem buttonModStop;
        private DevExpress.XtraBars.BarButtonItem assetsCopierButton;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem importMapButton;
        private DevExpress.XtraBars.BarButtonItem menuButton;
        private DevExpress.XtraBars.BarButtonItem buttonVPKExplorer;
        private DevExpress.XtraBars.BarButtonItem barButtonClear;
        private DevExpress.XtraBars.BarButtonItem buttonCrafty;
        private DevExpress.XtraBars.BarButtonItem buttonBatchCompiler;
        private DevExpress.XtraBars.BarButtonItem buttonCrowbar;
        private DevExpress.XtraBars.BarButtonItem buttonHLMV;
        private DevExpress.XtraBars.BarButtonItem buttonFaceposer;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem buttonIngameTools;
        private DevExpress.XtraBars.BarButtonItem buttonVMFtoMDL;
        private DevExpress.XtraBars.BarSubItem barModeling;
        private DevExpress.XtraBars.BarSubItem barChoreography;
        private DevExpress.XtraBars.BarSubItem barLevelDesign;
        private DevExpress.XtraBars.BarSubItem barMaterials;
        private DevExpress.XtraBars.BarSubItem barParticles;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem buttonHammerPropper;
        private DevExpress.XtraBars.BarButtonItem buttonOpenPrefabsFolder;
        private DevExpress.XtraBars.BarButtonItem buttonOpenMapsrcFolder;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraBars.BarButtonItem barButtonRunFullscreen;
        private System.Windows.Forms.Timer modProcessUpdater;
        private DevExpress.Utils.ImageCollection imageCollection3;
        private DevExpress.XtraBars.BarButtonItem buttonMaterialEditor;
        private DevExpress.XtraBars.BarButtonItem barButtonClientScheme;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
    }
}