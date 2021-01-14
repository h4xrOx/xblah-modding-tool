namespace source_modding_tool
{
    partial class MaterialEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialEditor));
            this.openVMTFileDialog = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonSave = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.menuFile = new DevExpress.XtraBars.BarSubItem();
            this.menuFileNew = new DevExpress.XtraBars.BarButtonItem();
            this.menuFileOpen = new DevExpress.XtraBars.BarButtonItem();
            this.menuFileSave = new DevExpress.XtraBars.BarButtonItem();
            this.menuFileSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.menuFileExit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonPreview = new DevExpress.XtraEditors.SimpleButton();
            this.dockPanel3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.vmtEdit = new DevExpress.XtraEditors.MemoEdit();
            this.contextLoad = new DevExpress.XtraBars.BarButtonItem();
            this.contextClear = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.openBitmapFileDialog = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.newVMTDialog = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panelContainer1.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.dockPanel3.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vmtEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openVMTFileDialog
            // 
            this.openVMTFileDialog.Filter = "Valve Material Type (*.vmt)|*.vmt";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3,
            this.bar4});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonSave,
            this.contextLoad,
            this.contextClear,
            this.menuFile,
            this.menuFileNew,
            this.menuFileOpen,
            this.menuFileSave,
            this.menuFileExit,
            this.menuFileSaveAs});
            this.barManager1.MainMenu = this.bar4;
            this.barManager1.MaxItemId = 14;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonSave)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.Text = "Tools";
            // 
            // barButtonSave
            // 
            this.barButtonSave.Caption = "Save";
            this.barButtonSave.Id = 2;
            this.barButtonSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonSave.ImageOptions.SvgImage")));
            this.barButtonSave.Name = "barButtonSave";
            this.barButtonSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonSave_ItemClick);
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
            // bar4
            // 
            this.bar4.BarName = "Custom 5";
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFile)});
            this.bar4.OptionsBar.MultiLine = true;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.Text = "Custom 5";
            // 
            // menuFile
            // 
            this.menuFile.Caption = "File";
            this.menuFile.Id = 8;
            this.menuFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFileNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFileOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFileSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFileSaveAs),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFileExit)});
            this.menuFile.Name = "menuFile";
            // 
            // menuFileNew
            // 
            this.menuFileNew.Caption = "New";
            this.menuFileNew.Id = 9;
            this.menuFileNew.Name = "menuFileNew";
            this.menuFileNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menu_ItemClick);
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.Caption = "Open";
            this.menuFileOpen.Id = 10;
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menu_ItemClick);
            // 
            // menuFileSave
            // 
            this.menuFileSave.Caption = "Save";
            this.menuFileSave.Id = 11;
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menu_ItemClick);
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.Caption = "Save As";
            this.menuFileSaveAs.Id = 13;
            this.menuFileSaveAs.Name = "menuFileSaveAs";
            this.menuFileSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menu_ItemClick);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Caption = "Exit";
            this.menuFileExit.Id = 12;
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menu_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1056, 52);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 674);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1056, 18);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 52);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 622);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1056, 52);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 622);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl"});
            // 
            // panelContainer1
            // 
            this.panelContainer1.Controls.Add(this.dockPanel1);
            this.panelContainer1.Controls.Add(this.dockPanel3);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer1.ID = new System.Guid("c7e22a9e-a18d-4e67-89d9-0999f73d1cf1");
            this.panelContainer1.Location = new System.Drawing.Point(665, 52);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(391, 200);
            this.panelContainer1.Size = new System.Drawing.Size(391, 622);
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel1.ID = new System.Guid("67027259-8967-426a-9466-ee71a6c608fd");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowMaximizeButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(391, 200);
            this.dockPanel1.Size = new System.Drawing.Size(391, 419);
            this.dockPanel1.Text = "Preview";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.panelControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 30);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(384, 385);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.buttonPreview);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(8);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(384, 384);
            this.panelControl1.TabIndex = 27;
            // 
            // buttonPreview
            // 
            this.buttonPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPreview.Location = new System.Drawing.Point(2, 2);
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(380, 380);
            this.buttonPreview.TabIndex = 28;
            this.buttonPreview.Text = "Preview";
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // dockPanel3
            // 
            this.dockPanel3.Controls.Add(this.dockPanel3_Container);
            this.dockPanel3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel3.FloatVertical = true;
            this.dockPanel3.ID = new System.Guid("bc8573f6-f9ac-4e41-9fcf-41f9ad73aa75");
            this.dockPanel3.Location = new System.Drawing.Point(0, 419);
            this.dockPanel3.Name = "dockPanel3";
            this.dockPanel3.Options.ShowMaximizeButton = false;
            this.dockPanel3.OriginalSize = new System.Drawing.Size(384, 438);
            this.dockPanel3.Size = new System.Drawing.Size(391, 203);
            this.dockPanel3.Text = "VMT";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.vmtEdit);
            this.dockPanel3_Container.Location = new System.Drawing.Point(4, 30);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(384, 170);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // vmtEdit
            // 
            this.vmtEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmtEdit.Location = new System.Drawing.Point(0, 0);
            this.vmtEdit.MenuManager = this.barManager1;
            this.vmtEdit.Name = "vmtEdit";
            this.vmtEdit.Properties.ReadOnly = true;
            this.vmtEdit.Size = new System.Drawing.Size(384, 170);
            this.vmtEdit.TabIndex = 0;
            // 
            // contextLoad
            // 
            this.contextLoad.Caption = "Load";
            this.contextLoad.Id = 5;
            this.contextLoad.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("contextLoad.ImageOptions.SvgImage")));
            this.contextLoad.Name = "contextLoad";
            // 
            // contextClear
            // 
            this.contextClear.Caption = "Clear";
            this.contextClear.Id = 6;
            this.contextClear.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("contextClear.ImageOptions.SvgImage")));
            this.contextClear.Name = "contextClear";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.contextLoad),
            new DevExpress.XtraBars.LinkPersistInfo(this.contextClear)});
            this.popupMenu.Manager = this.barManager1;
            this.popupMenu.Name = "popupMenu";
            // 
            // tabControl
            // 
            this.tabControl.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 52);
            this.tabControl.Name = "tabControl";
            this.tabControl.Size = new System.Drawing.Size(665, 622);
            this.tabControl.TabIndex = 47;
            this.tabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabControl_SelectedPageChanged);
            this.tabControl.CloseButtonClick += new System.EventHandler(this.xtraTabControl1_CloseButtonClick);
            // 
            // newVMTDialog
            // 
            this.newVMTDialog.Filter = "Valve Material Type (*.vmt)|*.vmt";
            // 
            // MaterialEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 692);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("MaterialEditor.IconOptions.Icon")));
            this.Name = "MaterialEditor";
            this.Text = "Material Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MaterialEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panelContainer1.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.dockPanel3.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vmtEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.XtraOpenFileDialog openVMTFileDialog;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonSave;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem contextLoad;
        private DevExpress.XtraBars.BarButtonItem contextClear;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraEditors.XtraOpenFileDialog openBitmapFileDialog;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton buttonPreview;
        private DevExpress.XtraEditors.XtraSaveFileDialog newVMTDialog;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraBars.BarSubItem menuFile;
        private DevExpress.XtraBars.BarButtonItem menuFileNew;
        private DevExpress.XtraBars.BarButtonItem menuFileOpen;
        private DevExpress.XtraBars.BarButtonItem menuFileSave;
        private DevExpress.XtraBars.BarButtonItem menuFileExit;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel3;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraEditors.MemoEdit vmtEdit;
        private DevExpress.XtraBars.BarButtonItem menuFileSaveAs;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
    }
}