namespace windows_source1ide
{
    partial class ModForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModForm));
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.gamesCombo = new DevExpress.XtraBars.BarEditItem();
            this.repositoryGamesCombo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.modsCombo = new DevExpress.XtraBars.BarEditItem();
            this.repositoryModsCombo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.buttonModStart = new DevExpress.XtraBars.BarButtonItem();
            this.buttonModStop = new DevExpress.XtraBars.BarButtonItem();
            this.buttonModRestart = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barFile = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barMod = new DevExpress.XtraBars.BarSubItem();
            this.barButtonRun = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonModOpenFolder = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonClear = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.importMapButton = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonGameinfo = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonChapters = new DevExpress.XtraBars.BarButtonItem();
            this.menuButton = new DevExpress.XtraBars.BarButtonItem();
            this.barTools = new DevExpress.XtraBars.BarSubItem();
            this.barButtonHammer = new DevExpress.XtraBars.BarButtonItem();
            this.assetsCopierButton = new DevExpress.XtraBars.BarButtonItem();
            this.buttonVPKExplorer = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.buttonGCFScape = new DevExpress.XtraBars.BarButtonItem();
            this.buttonCrafty = new DevExpress.XtraBars.BarButtonItem();
            this.buttonVTFEdit = new DevExpress.XtraBars.BarButtonItem();
            this.buttonBatchCompiler = new DevExpress.XtraBars.BarButtonItem();
            this.buttonCrowbar = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryGamesCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryModsCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2016 Black";
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
            this.barTools,
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
            this.buttonModRestart,
            this.assetsCopierButton,
            this.barSubItem1,
            this.importMapButton,
            this.menuButton,
            this.buttonVPKExplorer,
            this.barButtonClear,
            this.buttonGCFScape,
            this.buttonCrafty,
            this.buttonVTFEdit,
            this.buttonBatchCompiler,
            this.buttonCrowbar});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 40;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryGamesCombo,
            this.repositoryModsCombo,
            this.repositoryItemTextEdit1});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonModStop),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonModRestart)});
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
            this.buttonModStop.Name = "buttonModStop";
            this.buttonModStop.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.buttonModStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonModStop_ItemClick);
            // 
            // buttonModRestart
            // 
            this.buttonModRestart.Caption = "Restart";
            this.buttonModRestart.Id = 28;
            this.buttonModRestart.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonModRestart.ImageOptions.Image")));
            this.buttonModRestart.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("buttonModRestart.ImageOptions.LargeImage")));
            this.buttonModRestart.Name = "buttonModRestart";
            this.buttonModRestart.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.buttonModRestart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonModRestart_ItemClick);
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barTools)});
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
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Exit";
            this.barButtonItem1.Id = 20;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barMod
            // 
            this.barMod.Caption = "Mod";
            this.barMod.Id = 17;
            this.barMod.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonRun),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonModOpenFolder),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonClear),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem3)});
            this.barMod.Name = "barMod";
            // 
            // barButtonRun
            // 
            this.barButtonRun.Caption = "Run";
            this.barButtonRun.Id = 18;
            this.barButtonRun.Name = "barButtonRun";
            this.barButtonRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonModStart_ItemClick);
            // 
            // barButtonModOpenFolder
            // 
            this.barButtonModOpenFolder.Caption = "Open folder";
            this.barButtonModOpenFolder.Id = 21;
            this.barButtonModOpenFolder.Name = "barButtonModOpenFolder";
            this.barButtonModOpenFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonModOpenFolder_ItemClick);
            // 
            // barButtonClear
            // 
            this.barButtonClear.Caption = "Clean";
            this.barButtonClear.Id = 34;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.menuButton)});
            this.barSubItem3.Name = "barSubItem3";
            // 
            // barButtonGameinfo
            // 
            this.barButtonGameinfo.Caption = "Game info";
            this.barButtonGameinfo.Id = 25;
            this.barButtonGameinfo.Name = "barButtonGameinfo";
            this.barButtonGameinfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonGameinfo_ItemClick);
            // 
            // barButtonChapters
            // 
            this.barButtonChapters.Caption = "Chapters";
            this.barButtonChapters.Id = 26;
            this.barButtonChapters.Name = "barButtonChapters";
            this.barButtonChapters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonChapters_ItemClick);
            // 
            // menuButton
            // 
            this.menuButton.Caption = "Menu";
            this.menuButton.Id = 32;
            this.menuButton.Name = "menuButton";
            this.menuButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuButton_ItemClick);
            // 
            // barTools
            // 
            this.barTools.Caption = "Tools";
            this.barTools.Id = 15;
            this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonHammer),
            new DevExpress.XtraBars.LinkPersistInfo(this.assetsCopierButton, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonVPKExplorer),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonGCFScape, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonCrafty),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonVTFEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonBatchCompiler),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonCrowbar)});
            this.barTools.Name = "barTools";
            // 
            // barButtonHammer
            // 
            this.barButtonHammer.Caption = "Hammer";
            this.barButtonHammer.Id = 16;
            this.barButtonHammer.Name = "barButtonHammer";
            this.barButtonHammer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonHammer_ItemClick);
            // 
            // assetsCopierButton
            // 
            this.assetsCopierButton.Caption = "Assets copier";
            this.assetsCopierButton.Id = 29;
            this.assetsCopierButton.Name = "assetsCopierButton";
            this.assetsCopierButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.assetsCopierButton_ItemClick);
            // 
            // buttonVPKExplorer
            // 
            this.buttonVPKExplorer.Caption = "VPK Explorer";
            this.buttonVPKExplorer.Id = 33;
            this.buttonVPKExplorer.Name = "buttonVPKExplorer";
            this.buttonVPKExplorer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonVPKExplorer_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(587, 58);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 452);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(587, 18);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 58);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 394);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(587, 58);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 394);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Test";
            this.barButtonItem4.Id = 14;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // buttonGCFScape
            // 
            this.buttonGCFScape.Caption = "GCFScape";
            this.buttonGCFScape.Id = 35;
            this.buttonGCFScape.Name = "buttonGCFScape";
            this.buttonGCFScape.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonGCFScape_ItemClick);
            // 
            // buttonCrafty
            // 
            this.buttonCrafty.Caption = "Crafty";
            this.buttonCrafty.Id = 36;
            this.buttonCrafty.Name = "buttonCrafty";
            this.buttonCrafty.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonCrafty_ItemClick);
            // 
            // buttonVTFEdit
            // 
            this.buttonVTFEdit.Caption = "VTFEdit";
            this.buttonVTFEdit.Id = 37;
            this.buttonVTFEdit.Name = "buttonVTFEdit";
            this.buttonVTFEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonVTFEdit_ItemClick);
            // 
            // buttonBatchCompiler
            // 
            this.buttonBatchCompiler.Caption = "Batch Compiler";
            this.buttonBatchCompiler.Id = 38;
            this.buttonBatchCompiler.Name = "buttonBatchCompiler";
            this.buttonBatchCompiler.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonBatchCompiler_ItemClick);
            // 
            // buttonCrowbar
            // 
            this.buttonCrowbar.Caption = "Crowbar";
            this.buttonCrowbar.Id = 39;
            this.buttonCrowbar.Name = "buttonCrowbar";
            this.buttonCrowbar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonCrowbar_ItemClick);
            // 
            // ModForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 470);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModForm";
            this.Text = "Form";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryGamesCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryModsCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
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
        private DevExpress.XtraBars.BarSubItem barTools;
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
        private DevExpress.XtraBars.BarButtonItem buttonModRestart;
        private DevExpress.XtraBars.BarButtonItem assetsCopierButton;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem importMapButton;
        private DevExpress.XtraBars.BarButtonItem menuButton;
        private DevExpress.XtraBars.BarButtonItem buttonVPKExplorer;
        private DevExpress.XtraBars.BarButtonItem barButtonClear;
        private DevExpress.XtraBars.BarButtonItem buttonGCFScape;
        private DevExpress.XtraBars.BarButtonItem buttonCrafty;
        private DevExpress.XtraBars.BarButtonItem buttonVTFEdit;
        private DevExpress.XtraBars.BarButtonItem buttonBatchCompiler;
        private DevExpress.XtraBars.BarButtonItem buttonCrowbar;
    }
}