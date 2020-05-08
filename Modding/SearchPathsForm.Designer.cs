namespace source_modding_tool
{
    partial class SearchPathsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchPathsForm));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.searchList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSave = new DevExpress.XtraEditors.SimpleButton();
            this.buttonAddVPK = new DevExpress.XtraEditors.SimpleButton();
            this.buttonAddDirectory = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.buttonUp = new DevExpress.XtraEditors.SimpleButton();
            this.buttonDown = new DevExpress.XtraEditors.SimpleButton();
            this.buttonRemove = new DevExpress.XtraEditors.SimpleButton();
            this.vpkDialog = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.searchDirDialog = new DevExpress.XtraEditors.XtraFolderBrowserDialog(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.comboGames = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.searchList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboGames.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // searchList
            // 
            this.searchList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.searchList.Cursor = System.Windows.Forms.Cursors.Default;
            this.searchList.CustomizationFormBounds = new System.Drawing.Rectangle(-1632, -9, 250, 280);
            this.searchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchList.Location = new System.Drawing.Point(8, 36);
            this.searchList.Name = "searchList";
            this.searchList.OptionsBehavior.Editable = false;
            this.searchList.OptionsCustomization.AllowBandMoving = false;
            this.searchList.OptionsCustomization.AllowBandResizing = false;
            this.searchList.OptionsCustomization.AllowColumnMoving = false;
            this.searchList.OptionsCustomization.AllowFilter = false;
            this.searchList.OptionsCustomization.AllowQuickHideColumns = false;
            this.searchList.OptionsCustomization.AllowSort = false;
            this.searchList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
            this.searchList.Size = new System.Drawing.Size(736, 335);
            this.searchList.TabIndex = 8;
            this.searchList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.searchList_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Key";
            this.treeListColumn1.FieldName = "key";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 113;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Path";
            this.treeListColumn2.FieldName = "path";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 432;
            // 
            // panelControl6
            // 
            this.panelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl6.Controls.Add(this.buttonCancel);
            this.panelControl6.Controls.Add(this.buttonSave);
            this.panelControl6.Controls.Add(this.buttonAddVPK);
            this.panelControl6.Controls.Add(this.buttonAddDirectory);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl6.Location = new System.Drawing.Point(8, 371);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(736, 31);
            this.panelControl6.TabIndex = 10;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(580, 8);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Location = new System.Drawing.Point(661, 8);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonAddVPK
            // 
            this.buttonAddVPK.Location = new System.Drawing.Point(0, 8);
            this.buttonAddVPK.Name = "buttonAddVPK";
            this.buttonAddVPK.Size = new System.Drawing.Size(75, 23);
            this.buttonAddVPK.TabIndex = 4;
            this.buttonAddVPK.Text = "Add VPK";
            this.buttonAddVPK.Click += new System.EventHandler(this.buttonAddVPK_Click);
            // 
            // buttonAddDirectory
            // 
            this.buttonAddDirectory.Location = new System.Drawing.Point(81, 8);
            this.buttonAddDirectory.Name = "buttonAddDirectory";
            this.buttonAddDirectory.Size = new System.Drawing.Size(75, 23);
            this.buttonAddDirectory.TabIndex = 5;
            this.buttonAddDirectory.Text = "Add Directory";
            this.buttonAddDirectory.Click += new System.EventHandler(this.buttonAddDirectory_Click);
            // 
            // panelControl5
            // 
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Controls.Add(this.buttonUp);
            this.panelControl5.Controls.Add(this.buttonDown);
            this.panelControl5.Controls.Add(this.buttonRemove);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl5.Location = new System.Drawing.Point(744, 36);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(29, 366);
            this.panelControl5.TabIndex = 9;
            // 
            // buttonUp
            // 
            this.buttonUp.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.buttonUp.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonUp.ImageOptions.SvgImage")));
            this.buttonUp.Location = new System.Drawing.Point(6, 0);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(23, 23);
            this.buttonUp.TabIndex = 1;
            this.buttonUp.Text = "Up";
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.buttonDown.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonDown.ImageOptions.SvgImage")));
            this.buttonDown.Location = new System.Drawing.Point(6, 29);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(23, 23);
            this.buttonDown.TabIndex = 2;
            this.buttonDown.Text = "Down";
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.buttonRemove.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonRemove.ImageOptions.SvgImage")));
            this.buttonRemove.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.buttonRemove.Location = new System.Drawing.Point(6, 58);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(23, 23);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // vpkDialog
            // 
            this.vpkDialog.Filter = "Valve Pack (*.vpk)|*.vpk";
            this.vpkDialog.Title = "Select a VPK";
            // 
            // searchDirDialog
            // 
            this.searchDirDialog.Title = "Select a search directory";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.comboGames);
            this.panelControl1.Controls.Add(this.labelControl18);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(8, 8);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(765, 28);
            this.panelControl1.TabIndex = 11;
            // 
            // comboGames
            // 
            this.comboGames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboGames.Location = new System.Drawing.Point(64, 0);
            this.comboGames.Name = "comboGames";
            this.comboGames.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboGames.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboGames.Size = new System.Drawing.Size(701, 20);
            toolTipTitleItem1.Text = "SteamAppID <integer>";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = resources.GetString("toolTipItem1.Text");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.comboGames.SuperTip = superToolTip1;
            this.comboGames.TabIndex = 2;
            // 
            // labelControl18
            // 
            this.labelControl18.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl18.Location = new System.Drawing.Point(0, 0);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Padding = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.labelControl18.Size = new System.Drawing.Size(64, 16);
            this.labelControl18.TabIndex = 3;
            this.labelControl18.Text = "Base game:";
            // 
            // SearchPathsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 410);
            this.Controls.Add(this.searchList);
            this.Controls.Add(this.panelControl6);
            this.Controls.Add(this.panelControl5);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchPathsForm";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Content Mount";
            this.Load += new System.EventHandler(this.SearchPathsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.searchList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboGames.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList searchList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.SimpleButton buttonAddVPK;
        private DevExpress.XtraEditors.SimpleButton buttonAddDirectory;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.SimpleButton buttonUp;
        private DevExpress.XtraEditors.SimpleButton buttonDown;
        private DevExpress.XtraEditors.SimpleButton buttonRemove;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.SimpleButton buttonSave;
        private DevExpress.XtraEditors.XtraOpenFileDialog vpkDialog;
        private DevExpress.XtraEditors.XtraFolderBrowserDialog searchDirDialog;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboGames;
        private DevExpress.XtraEditors.LabelControl labelControl18;
    }
}