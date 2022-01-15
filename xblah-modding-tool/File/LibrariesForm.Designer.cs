namespace xblah_modding_tool
{
    partial class LibrariesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LibrariesForm));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
            this.itemsTree = new DevExpress.XtraTreeList.TreeList();
            this.itemsTreePathColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.itemsTreeSourceColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.addButton = new DevExpress.XtraEditors.SimpleButton();
            this.removeButton = new DevExpress.XtraEditors.SimpleButton();
            this.addDialog = new DevExpress.XtraEditors.XtraFolderBrowserDialog(this.components);
            this.browseButton = new DevExpress.XtraEditors.SimpleButton();
            this.optionsPanel = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.itemsTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionsPanel)).BeginInit();
            this.optionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemsTree
            // 
            this.itemsTree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.itemsTreePathColumn,
            this.itemsTreeSourceColumn});
            this.itemsTree.Cursor = System.Windows.Forms.Cursors.Default;
            this.itemsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemsTree.Location = new System.Drawing.Point(8, 8);
            this.itemsTree.Name = "itemsTree";
            this.itemsTree.OptionsBehavior.ReadOnly = true;
            this.itemsTree.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.itemsTree.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFocus;
            this.itemsTree.Size = new System.Drawing.Size(585, 307);
            this.itemsTree.TabIndex = 0;
            this.itemsTree.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.itemsTree_FocusedNodeChanged);
            // 
            // itemsTreePathColumn
            // 
            this.itemsTreePathColumn.Caption = "Path";
            this.itemsTreePathColumn.FieldName = "path";
            this.itemsTreePathColumn.Name = "itemsTreePathColumn";
            this.itemsTreePathColumn.Visible = true;
            this.itemsTreePathColumn.VisibleIndex = 0;
            this.itemsTreePathColumn.Width = 432;
            // 
            // itemsTreeSourceColumn
            // 
            this.itemsTreeSourceColumn.Caption = "Source";
            this.itemsTreeSourceColumn.FieldName = "source";
            this.itemsTreeSourceColumn.Name = "itemsTreeSourceColumn";
            this.itemsTreeSourceColumn.Visible = true;
            this.itemsTreeSourceColumn.VisibleIndex = 1;
            this.itemsTreeSourceColumn.Width = 204;
            // 
            // addButton
            // 
            this.addButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("addButton.ImageOptions.SvgImage")));
            this.addButton.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.addButton.Location = new System.Drawing.Point(6, 0);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(23, 23);
            toolTipTitleItem1.Text = "Add Library";
            toolTipItem1.Text = "Add a library manually, by selecting a directory that contains a SteamApps folder" +
    ".";
            toolTipItem2.Text = resources.GetString("toolTipItem2.Text");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            superToolTip1.Items.Add(toolTipItem2);
            this.addButton.SuperTip = superToolTip1;
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add Library";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("removeButton.ImageOptions.SvgImage")));
            this.removeButton.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.removeButton.Location = new System.Drawing.Point(6, 29);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(23, 23);
            toolTipTitleItem2.Text = "Remove Library";
            toolTipItem3.Text = "Remove a User library.";
            toolTipItem4.Text = "It will not delete the files.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem3);
            superToolTip2.Items.Add(toolTipItem4);
            this.removeButton.SuperTip = superToolTip2;
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "Remove Library";
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("browseButton.ImageOptions.SvgImage")));
            this.browseButton.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.browseButton.Location = new System.Drawing.Point(6, 58);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(23, 23);
            toolTipTitleItem3.Text = "Browse Library";
            toolTipItem5.Text = "Open the library directory.";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem5);
            this.browseButton.SuperTip = superToolTip3;
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "Browse Library";
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // optionsPanel
            // 
            this.optionsPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.optionsPanel.Controls.Add(this.addButton);
            this.optionsPanel.Controls.Add(this.browseButton);
            this.optionsPanel.Controls.Add(this.removeButton);
            this.optionsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.optionsPanel.Location = new System.Drawing.Point(593, 8);
            this.optionsPanel.Name = "optionsPanel";
            this.optionsPanel.Size = new System.Drawing.Size(29, 307);
            this.optionsPanel.TabIndex = 4;
            // 
            // LibrariesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 323);
            this.Controls.Add(this.itemsTree);
            this.Controls.Add(this.optionsPanel);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("LibrariesForm.IconOptions.Icon")));
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("LibrariesForm.IconOptions.Image")));
            this.Name = "LibrariesForm";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Steam Libraries";
            this.Load += new System.EventHandler(this.LibrariesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.itemsTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionsPanel)).EndInit();
            this.optionsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList itemsTree;
        private DevExpress.XtraEditors.SimpleButton addButton;
        private DevExpress.XtraEditors.SimpleButton removeButton;
        private DevExpress.XtraTreeList.Columns.TreeListColumn itemsTreePathColumn;
        private DevExpress.XtraTreeList.Columns.TreeListColumn itemsTreeSourceColumn;
        private DevExpress.XtraEditors.XtraFolderBrowserDialog addDialog;
        private DevExpress.XtraEditors.SimpleButton browseButton;
        private DevExpress.XtraEditors.PanelControl optionsPanel;
    }
}