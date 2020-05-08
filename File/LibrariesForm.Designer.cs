namespace source_modding_tool
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
            this.list = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.addButton = new DevExpress.XtraEditors.SimpleButton();
            this.removeButton = new DevExpress.XtraEditors.SimpleButton();
            this.addDialog = new DevExpress.XtraEditors.XtraFolderBrowserDialog(this.components);
            this.browseButton = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.list)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.list.Cursor = System.Windows.Forms.Cursors.Default;
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.Location = new System.Drawing.Point(8, 8);
            this.list.Name = "list";
            this.list.OptionsBehavior.ReadOnly = true;
            this.list.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.list.Size = new System.Drawing.Size(585, 307);
            this.list.TabIndex = 0;
            this.list.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.list_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Path";
            this.treeListColumn1.FieldName = "path";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 432;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Source";
            this.treeListColumn2.FieldName = "source";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 204;
            // 
            // addButton
            // 
            this.addButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("addButton.ImageOptions.SvgImage")));
            this.addButton.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.addButton.Location = new System.Drawing.Point(6, 0);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(23, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("removeButton.ImageOptions.SvgImage")));
            this.removeButton.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.removeButton.Location = new System.Drawing.Point(6, 29);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(23, 23);
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "Remove";
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("browseButton.ImageOptions.SvgImage")));
            this.browseButton.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.browseButton.Location = new System.Drawing.Point(6, 58);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(23, 23);
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "Browse";
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.addButton);
            this.panelControl1.Controls.Add(this.browseButton);
            this.panelControl1.Controls.Add(this.removeButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(593, 8);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(29, 307);
            this.panelControl1.TabIndex = 4;
            // 
            // LibrariesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 323);
            this.Controls.Add(this.list);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LibrariesForm";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Libraries";
            this.Load += new System.EventHandler(this.LibrariesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.list)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList list;
        private DevExpress.XtraEditors.SimpleButton addButton;
        private DevExpress.XtraEditors.SimpleButton removeButton;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.XtraFolderBrowserDialog addDialog;
        private DevExpress.XtraEditors.SimpleButton browseButton;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}