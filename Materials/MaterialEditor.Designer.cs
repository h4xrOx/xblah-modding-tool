namespace SourceModdingTool
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
            this.barButtonNew = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonOpen = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonSave = new DevExpress.XtraBars.BarButtonItem();
            this.textPath = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonPreview = new DevExpress.XtraEditors.SimpleButton();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.contextLoad = new DevExpress.XtraBars.BarButtonItem();
            this.contextClear = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.materialEditorTab1 = new SourceModdingTool.MaterialEditorTab();
            this.openBitmapFileDialog = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.newVMTDialog = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
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
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonNew,
            this.barButtonOpen,
            this.barButtonSave,
            this.textPath,
            this.contextLoad,
            this.contextClear});
            this.barManager1.MaxItemId = 7;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.textPath)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.Text = "Tools";
            // 
            // barButtonNew
            // 
            this.barButtonNew.Caption = "New";
            this.barButtonNew.Id = 0;
            this.barButtonNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonNew.ImageOptions.SvgImage")));
            this.barButtonNew.Name = "barButtonNew";
            this.barButtonNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonNew_ItemClick);
            // 
            // barButtonOpen
            // 
            this.barButtonOpen.Caption = "Open";
            this.barButtonOpen.Id = 1;
            this.barButtonOpen.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonOpen.ImageOptions.SvgImage")));
            this.barButtonOpen.Name = "barButtonOpen";
            this.barButtonOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonOpen_ItemClick);
            // 
            // barButtonSave
            // 
            this.barButtonSave.Caption = "Save";
            this.barButtonSave.Id = 2;
            this.barButtonSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonSave.ImageOptions.SvgImage")));
            this.barButtonSave.Name = "barButtonSave";
            this.barButtonSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonSave_ItemClick);
            // 
            // textPath
            // 
            this.textPath.AutoFillWidth = true;
            this.textPath.CanOpenEdit = false;
            this.textPath.Caption = "textPath";
            this.textPath.Edit = this.repositoryItemTextEdit1;
            this.textPath.Id = 4;
            this.textPath.Name = "textPath";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
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
            this.barDockControlTop.Size = new System.Drawing.Size(1132, 28);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 552);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1132, 18);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 28);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 524);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1132, 28);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 524);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1,
            this.dockPanel2});
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
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel1.ID = new System.Guid("67027259-8967-426a-9466-ee71a6c608fd");
            this.dockPanel1.Location = new System.Drawing.Point(741, 28);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(391, 200);
            this.dockPanel1.Size = new System.Drawing.Size(391, 524);
            this.dockPanel1.Text = "Preview";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.panelControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 30);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(384, 491);
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
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel2.ID = new System.Guid("241aea45-5747-4a62-b26c-40a692dd627c");
            this.dockPanel2.Location = new System.Drawing.Point(0, 28);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel2.Size = new System.Drawing.Size(200, 524);
            this.dockPanel2.Text = "Files";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.treeList1);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 30);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(193, 491);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.treeList1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(193, 491);
            this.treeList1.TabIndex = 0;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Name";
            this.treeListColumn1.FieldName = "name";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Path";
            this.treeListColumn2.FieldName = "path";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
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
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.contextLoad),
            new DevExpress.XtraBars.LinkPersistInfo(this.contextClear)});
            this.popupMenu.Manager = this.barManager1;
            this.popupMenu.Name = "popupMenu";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(200, 28);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(541, 524);
            this.xtraTabControl1.TabIndex = 47;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            this.xtraTabControl1.CloseButtonClick += new System.EventHandler(this.xtraTabControl1_CloseButtonClick);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.materialEditorTab1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(539, 495);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // materialEditorTab1
            // 
            this.materialEditorTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialEditorTab1.Location = new System.Drawing.Point(0, 0);
            this.materialEditorTab1.Name = "materialEditorTab1";
            this.materialEditorTab1.shader = "LightmappedGeneric";
            this.materialEditorTab1.Size = new System.Drawing.Size(539, 495);
            this.materialEditorTab1.TabIndex = 0;
            // 
            // newVMTDialog
            // 
            this.newVMTDialog.Filter = "Valve Material Type (*.vmt)|*.vmt";
            // 
            // MaterialEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 570);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.dockPanel2);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MaterialEditor";
            this.Text = "Material Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MaterialEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.XtraOpenFileDialog openVMTFileDialog;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonNew;
        private DevExpress.XtraBars.BarButtonItem barButtonOpen;
        private DevExpress.XtraBars.BarButtonItem barButtonSave;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem textPath;
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
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private SourceModdingTool.MaterialEditorTab materialEditorTab1;
    }
}