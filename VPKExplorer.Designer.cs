namespace windows_source1ide.Tools
{
    partial class VPKExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VPKExplorer));
            this.dirs = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.list = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.buttonBack = new DevExpress.XtraBars.BarButtonItem();
            this.buttonForward = new DevExpress.XtraBars.BarButtonItem();
            this.buttonUp = new DevExpress.XtraBars.BarButtonItem();
            this.textDirectory = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.textSearch = new DevExpress.XtraBars.BarEditItem();
            this.repositoryTextSearch = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.dirs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.list)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryTextSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dirs
            // 
            this.dirs.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.dirs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dirs.Location = new System.Drawing.Point(8, 8);
            this.dirs.Name = "dirs";
            this.dirs.OptionsBehavior.Editable = false;
            this.dirs.Size = new System.Drawing.Size(260, 592);
            this.dirs.StateImageList = this.imageCollection1;
            this.dirs.TabIndex = 0;
            this.dirs.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.dirs_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Directories";
            this.treeListColumn1.FieldName = "directories";
            this.treeListColumn1.MinWidth = 34;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertGalleryImage("open_16x16.png", "office2013/actions/open_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("office2013/actions/open_16x16.png"), 0);
            this.imageCollection1.Images.SetKeyName(0, "open_16x16.png");
            this.imageCollection1.InsertGalleryImage("new_16x16.png", "office2013/actions/new_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("office2013/actions/new_16x16.png"), 1);
            this.imageCollection1.Images.SetKeyName(1, "new_16x16.png");
            // 
            // list
            // 
            this.list.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2,
            this.treeListColumn3});
            this.list.Cursor = System.Windows.Forms.Cursors.Default;
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.Location = new System.Drawing.Point(0, 8);
            this.list.Name = "list";
            this.list.OptionsBehavior.Editable = false;
            this.list.OptionsSelection.MultiSelect = true;
            this.list.Size = new System.Drawing.Size(690, 592);
            this.list.StateImageList = this.imageCollection1;
            this.list.TabIndex = 1;
            this.list.DoubleClick += new System.EventHandler(this.list_DoubleClick);
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Name";
            this.treeListColumn2.FieldName = "name";
            this.treeListColumn2.MinWidth = 34;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Type";
            this.treeListColumn3.FieldName = "type";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 1;
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
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.buttonBack,
            this.buttonForward,
            this.buttonUp,
            this.textDirectory,
            this.textSearch});
            this.barManager1.MaxItemId = 6;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryTextSearch});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonBack),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonForward),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonUp),
            new DevExpress.XtraBars.LinkPersistInfo(this.textDirectory),
            new DevExpress.XtraBars.LinkPersistInfo(this.textSearch)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.Text = "Tools";
            // 
            // buttonBack
            // 
            this.buttonBack.Caption = "Back";
            this.buttonBack.Id = 0;
            this.buttonBack.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.ImageOptions.Image")));
            this.buttonBack.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("buttonBack.ImageOptions.LargeImage")));
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonBack_ItemClick);
            // 
            // buttonForward
            // 
            this.buttonForward.Caption = "Forward";
            this.buttonForward.Id = 1;
            this.buttonForward.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonForward.ImageOptions.Image")));
            this.buttonForward.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("buttonForward.ImageOptions.LargeImage")));
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonForward_ItemClick);
            // 
            // buttonUp
            // 
            this.buttonUp.Caption = "Up";
            this.buttonUp.Id = 2;
            this.buttonUp.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonUp.ImageOptions.Image")));
            this.buttonUp.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("buttonUp.ImageOptions.LargeImage")));
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonUp_ItemClick);
            // 
            // textDirectory
            // 
            this.textDirectory.AutoFillWidth = true;
            this.textDirectory.Caption = "Directory";
            this.textDirectory.Edit = this.repositoryItemTextEdit1;
            this.textDirectory.Id = 3;
            this.textDirectory.Name = "textDirectory";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.ReadOnly = true;
            // 
            // textSearch
            // 
            this.textSearch.Caption = "Search";
            this.textSearch.Edit = this.repositoryTextSearch;
            this.textSearch.EditWidth = 128;
            this.textSearch.Id = 4;
            this.textSearch.Name = "textSearch";
            // 
            // repositoryTextSearch
            // 
            this.repositoryTextSearch.AutoHeight = false;
            this.repositoryTextSearch.Name = "repositoryTextSearch";
            this.repositoryTextSearch.NullValuePrompt = "Search";
            this.repositoryTextSearch.NullValuePromptShowForEmptyValue = true;
            this.repositoryTextSearch.EditValueChanged += new System.EventHandler(this.repositoryTextSearch_EditValueChanged);
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
            this.barDockControlTop.Size = new System.Drawing.Size(978, 28);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 636);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(978, 18);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 28);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 608);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(978, 28);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 608);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 28);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.dirs);
            this.splitContainerControl1.Panel1.Padding = new System.Windows.Forms.Padding(8, 8, 0, 8);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.list);
            this.splitContainerControl1.Panel2.Padding = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(978, 608);
            this.splitContainerControl1.SplitterPosition = 268;
            this.splitContainerControl1.TabIndex = 7;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // VPKExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 654);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "VPKExplorer";
            this.Text = "VPK Explorer";
            this.Load += new System.EventHandler(this.VPKExplorer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dirs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.list)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryTextSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList dirs;
        private DevExpress.XtraTreeList.TreeList list;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem buttonBack;
        private DevExpress.XtraBars.BarButtonItem buttonForward;
        private DevExpress.XtraBars.BarButtonItem buttonUp;
        private DevExpress.XtraBars.BarEditItem textDirectory;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.BarEditItem textSearch;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryTextSearch;
    }
}