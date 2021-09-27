namespace source_modding_tool.Tools
{
    partial class LegacyFileExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegacyFileExplorer));
            this.tree = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.list = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
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
            this.filePopExtractButton = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.filePopEditButton = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.filePopDeleteButton = new DevExpress.XtraBars.BarButtonItem();
            this.filePopOpenFileLocationButton = new DevExpress.XtraBars.BarButtonItem();
            this.filePopOpenButton = new DevExpress.XtraBars.BarButtonItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.filePopMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.okButton = new DevExpress.XtraEditors.SimpleButton();
            this.cancelButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.list)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryTextSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filePopMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tree
            // 
            this.tree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.Location = new System.Drawing.Point(8, 8);
            this.tree.Name = "tree";
            this.tree.OptionsBehavior.Editable = false;
            this.tree.Size = new System.Drawing.Size(260, 560);
            this.tree.StateImageList = this.imageCollection1;
            this.tree.TabIndex = 0;
            this.tree.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tree_SelectionChanged);
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
            this.treeListColumn3,
            this.treeListColumn4});
            this.list.Cursor = System.Windows.Forms.Cursors.Default;
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.Location = new System.Drawing.Point(0, 8);
            this.list.MenuManager = this.barManager1;
            this.list.Name = "list";
            this.list.OptionsBehavior.Editable = false;
            this.list.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.list.OptionsSelection.MultiSelect = true;
            this.list.OptionsSelection.SelectNodesOnRightClick = true;
            this.list.Size = new System.Drawing.Size(692, 560);
            this.list.StateImageList = this.imageCollection1;
            this.list.TabIndex = 1;
            this.list.SelectionChanged += new System.EventHandler(this.list_SelectionChanged);
            this.list.DoubleClick += new System.EventHandler(this.list_DoubleClick);
            this.list.MouseClick += new System.Windows.Forms.MouseEventHandler(this.list_MouseClick);
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
            this.treeListColumn3.SortOrder = System.Windows.Forms.SortOrder.Descending;
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 1;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Source";
            this.treeListColumn4.FieldName = "source";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 2;
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
            this.textSearch,
            this.filePopExtractButton,
            this.barButtonItem2,
            this.filePopEditButton,
            this.barButtonItem4,
            this.filePopDeleteButton,
            this.filePopOpenFileLocationButton,
            this.filePopOpenButton});
            this.barManager1.MaxItemId = 15;
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
            this.buttonBack.Hint = "Back";
            this.buttonBack.Id = 0;
            this.buttonBack.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.ImageOptions.Image")));
            this.buttonBack.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("buttonBack.ImageOptions.LargeImage")));
            this.buttonBack.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonBack.ImageOptions.SvgImage")));
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.navigation_ItemClick);
            // 
            // buttonForward
            // 
            this.buttonForward.Caption = "Forward";
            this.buttonForward.Hint = "Forward";
            this.buttonForward.Id = 1;
            this.buttonForward.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonForward.ImageOptions.Image")));
            this.buttonForward.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("buttonForward.ImageOptions.LargeImage")));
            this.buttonForward.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonForward.ImageOptions.SvgImage")));
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.navigation_ItemClick);
            // 
            // buttonUp
            // 
            this.buttonUp.Caption = "Up";
            this.buttonUp.Hint = "Up";
            this.buttonUp.Id = 2;
            this.buttonUp.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("buttonUp.ImageOptions.Image")));
            this.buttonUp.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("buttonUp.ImageOptions.LargeImage")));
            this.buttonUp.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonUp.ImageOptions.SvgImage")));
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.navigation_ItemClick);
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
            this.bar3.Visible = false;
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
            // filePopExtractButton
            // 
            this.filePopExtractButton.Caption = "Extract";
            this.filePopExtractButton.Id = 8;
            this.filePopExtractButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("filePopExtractButton.ImageOptions.SvgImage")));
            this.filePopExtractButton.Name = "filePopExtractButton";
            this.filePopExtractButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Popup_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "World";
            this.barButtonItem2.Id = 9;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // filePopEditButton
            // 
            this.filePopEditButton.Caption = "Edit";
            this.filePopEditButton.Id = 10;
            this.filePopEditButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("filePopEditButton.ImageOptions.SvgImage")));
            this.filePopEditButton.Name = "filePopEditButton";
            this.filePopEditButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Popup_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "barButtonItem4";
            this.barButtonItem4.Id = 11;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // filePopDeleteButton
            // 
            this.filePopDeleteButton.Caption = "Delete";
            this.filePopDeleteButton.Id = 12;
            this.filePopDeleteButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("filePopDeleteButton.ImageOptions.SvgImage")));
            this.filePopDeleteButton.Name = "filePopDeleteButton";
            this.filePopDeleteButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Popup_ItemClick);
            // 
            // filePopOpenFileLocationButton
            // 
            this.filePopOpenFileLocationButton.Caption = "Open File Location";
            this.filePopOpenFileLocationButton.Id = 13;
            this.filePopOpenFileLocationButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("filePopOpenFileLocationButton.ImageOptions.SvgImage")));
            this.filePopOpenFileLocationButton.Name = "filePopOpenFileLocationButton";
            this.filePopOpenFileLocationButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Popup_ItemClick);
            // 
            // filePopOpenButton
            // 
            this.filePopOpenButton.Caption = "Open";
            this.filePopOpenButton.Id = 14;
            this.filePopOpenButton.Name = "filePopOpenButton";
            this.filePopOpenButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Popup_ItemClick);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 28);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.tree);
            this.splitContainerControl1.Panel1.Padding = new System.Windows.Forms.Padding(8, 8, 0, 8);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.list);
            this.splitContainerControl1.Panel2.Padding = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(978, 576);
            this.splitContainerControl1.SplitterPosition = 268;
            this.splitContainerControl1.TabIndex = 7;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // filePopMenu
            // 
            this.filePopMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.filePopEditButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.filePopOpenFileLocationButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.filePopExtractButton),
            new DevExpress.XtraBars.LinkPersistInfo(this.filePopDeleteButton)});
            this.filePopMenu.Manager = this.barManager1;
            this.filePopMenu.Name = "filePopMenu";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.okButton);
            this.panelControl1.Controls.Add(this.cancelButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 604);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(978, 32);
            this.panelControl1.TabIndex = 2;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(817, 5);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Open";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(898, 5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            // 
            // LegacyFileExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 654);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("LegacyFileExplorer.IconOptions.Icon")));
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("LegacyFileExplorer.IconOptions.Image")));
            this.Name = "LegacyFileExplorer";
            this.Text = "File Explorer";
            this.Load += new System.EventHandler(this.FileExplorer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.list)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryTextSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.filePopMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tree;
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
        private DevExpress.XtraBars.BarButtonItem filePopExtractButton;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraBars.PopupMenu filePopMenu;
        private DevExpress.XtraBars.BarButtonItem filePopEditButton;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem filePopDeleteButton;
        private DevExpress.XtraBars.BarButtonItem filePopOpenFileLocationButton;
        private DevExpress.XtraBars.BarButtonItem filePopOpenButton;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton okButton;
        private DevExpress.XtraEditors.SimpleButton cancelButton;
    }
}