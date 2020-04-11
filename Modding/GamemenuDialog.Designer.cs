namespace SourceModdingTool
{
    partial class GamemenuDialog
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
            this.list = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelDescription = new DevExpress.XtraEditors.LabelControl();
            this.textCaption = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.buttonSave = new DevExpress.XtraEditors.SimpleButton();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.textCommand = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.switchIngame = new DevExpress.XtraEditors.ToggleSwitch();
            this.comboVisible = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.list)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCaption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCommand.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchIngame.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboVisible.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.list.Cursor = System.Windows.Forms.Cursors.Default;
            this.list.Dock = System.Windows.Forms.DockStyle.Left;
            this.list.Location = new System.Drawing.Point(8, 8);
            this.list.Name = "list";
            this.list.BeginUnboundLoad();
            this.list.AppendNode(new object[] {
            "OpenPlayerListDialog"}, -1, 0, 0, 1, "Players can mute other players");
            this.list.AppendNode(new object[] {
            "OpenNewGameDialog"}, -1, 0, 0, 2, "Shows the chapter selection window");
            this.list.AppendNode(new object[] {
            "OpenLoadGameDialog"}, -1, 0, 0, 4, "Shows the load game window");
            this.list.AppendNode(new object[] {
            "OpenSaveGameDialog"}, -1, 0, 0, 5, "Shows the save game window");
            this.list.AppendNode(new object[] {
            "OpenBonusMapsDialog"}, -1, 0, 0, 6, "Shows the \"Bonus Maps\" dialog");
            this.list.AppendNode(new object[] {
            "OpenOptionsDialog"}, -1, 0, 0, 11, "Shows the options dialog");
            this.list.AppendNode(new object[] {
            "OpenBenchmarkDialog"}, -1, 0, 0, 8, "Opens the Lost Coast benchmark dialog");
            this.list.AppendNode(new object[] {
            "OpenServerBrowser"}, -1, 0, 0, 9, "Shows a list of game servers");
            this.list.AppendNode(new object[] {
            "OpenCreateMultiplayerGameDialog"}, -1, 0, 0, 10, "Shows the listen server creation dialog");
            this.list.AppendNode(new object[] {
            "OpenLoadCommentaryDialog"}, -1, 0, 0, 3, "Shows a dialog with a list of commentaries");
            this.list.AppendNode(new object[] {
            "OpenLoadSingleplayerCommentaryDialog"}, -1, 0, 0, 3, "Shows the new game dialog, but this one starts the game in commentary mode\t");
            this.list.AppendNode(new object[] {
            "Quit"}, -1, 0, 0, 0, "Shows the quit confirmation dialog");
            this.list.AppendNode(new object[] {
            "QuitNoConfirm"}, -1, 0, 0, 0, "Closes the game without confirmation");
            this.list.AppendNode(new object[] {
            "ResumeGame"}, -1, 0, 0, 7, "Hides the menu when in-game");
            this.list.AppendNode(new object[] {
            "Disconnect"}, -1, 0, 0, 0, "Disconnects from the game");
            this.list.AppendNode(new object[] {
            "DisconnectNoConfirm"}, -1, 0, 0, 0, "\tDisconnects from the game without confirmation");
            this.list.AppendNode(new object[] {
            "engine <command>"}, -1, 0, 0, 12, "Executes console command <concommand>");
            this.list.AppendNode(new object[] {
            "OpenAchievementsDialog"}, -1, 0, 0, 13, "Shows the achievements dialog");
            this.list.EndUnboundLoad();
            this.list.OptionsBehavior.Editable = false;
            this.list.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.list.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFullFocus;
            this.list.RowHeight = 32;
            this.list.Size = new System.Drawing.Size(357, 351);
            this.list.StateImageList = this.svgImageCollection1;
            this.list.TabIndex = 0;
            this.list.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.list_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Commands";
            this.treeListColumn1.FieldName = "command";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.ImageSize = new System.Drawing.Size(32, 32);
            this.svgImageCollection1.Add("clearheaderandfooter", "image://svgimages/richedit/clearheaderandfooter.svg");
            this.svgImageCollection1.Add("bo_department", "image://svgimages/business objects/bo_department.svg");
            this.svgImageCollection1.Add("shopping_new", "image://svgimages/icon builder/shopping_new.svg");
            this.svgImageCollection1.Add("newcomment", "image://svgimages/richedit/newcomment.svg");
            this.svgImageCollection1.Add("open", "image://svgimages/actions/open.svg");
            this.svgImageCollection1.Add("save", "image://svgimages/save/save.svg");
            this.svgImageCollection1.Add("bold", "image://svgimages/outlook inspired/bold.svg");
            this.svgImageCollection1.Add("backward", "image://svgimages/navigation/backward.svg");
            this.svgImageCollection1.Add("charttype_area", "image://svgimages/chart/charttype_area.svg");
            this.svgImageCollection1.Add("listview", "image://svgimages/scheduling/listview.svg");
            this.svgImageCollection1.Add("newtablestyle", "image://svgimages/actions/newtablestyle.svg");
            this.svgImageCollection1.Add("properties", "image://svgimages/setup/properties.svg");
            this.svgImageCollection1.Add("initialstate", "image://svgimages/dashboards/initialstate.svg");
            this.svgImageCollection1.Add("itemtypechecked", "image://svgimages/dashboards/itemtypechecked.svg");
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Description:";
            // 
            // labelDescription
            // 
            this.labelDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDescription.Location = new System.Drawing.Point(80, 3);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(179, 13);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "...";
            // 
            // textCaption
            // 
            this.textCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textCaption.Location = new System.Drawing.Point(80, 22);
            this.textCaption.Name = "textCaption";
            this.textCaption.Size = new System.Drawing.Size(179, 20);
            this.textCaption.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 22);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(37, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Caption";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Location = new System.Drawing.Point(187, 8);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(104, 8);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            // 
            // textCommand
            // 
            this.textCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textCommand.Location = new System.Drawing.Point(80, 100);
            this.textCommand.Name = "textCommand";
            this.textCommand.Size = new System.Drawing.Size(179, 20);
            this.textCommand.TabIndex = 8;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(11, 100);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Command";
            // 
            // switchIngame
            // 
            this.switchIngame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.switchIngame.Location = new System.Drawing.Point(80, 48);
            this.switchIngame.Name = "switchIngame";
            this.switchIngame.Properties.OffText = "No";
            this.switchIngame.Properties.OnText = "Yes";
            this.switchIngame.Size = new System.Drawing.Size(179, 20);
            this.switchIngame.TabIndex = 11;
            // 
            // comboVisible
            // 
            this.comboVisible.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboVisible.Location = new System.Drawing.Point(80, 74);
            this.comboVisible.Name = "comboVisible";
            this.comboVisible.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboVisible.Properties.Items.AddRange(new object[] {
            "Single-player only",
            "Multi-player only",
            "Both"});
            this.comboVisible.Size = new System.Drawing.Size(179, 20);
            this.comboVisible.TabIndex = 12;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(11, 48);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(63, 13);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "In-game only";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(11, 74);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(44, 13);
            this.labelControl6.TabIndex = 14;
            this.labelControl6.Text = "Visible on";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.Controls.Add(this.textCommand, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelControl4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboVisible, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.switchIngame, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelControl6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelControl3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelControl5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelDescription, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textCaption, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(365, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(262, 351);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.buttonSave);
            this.panelControl1.Controls.Add(this.buttonCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(365, 328);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(262, 31);
            this.panelControl1.TabIndex = 16;
            // 
            // GamemenuDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 367);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.list);
            this.Name = "GamemenuDialog";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Menu item";
            ((System.ComponentModel.ISupportInitialize)(this.list)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCaption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCommand.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchIngame.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboVisible.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList list;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelDescription;
        private DevExpress.XtraEditors.TextEdit textCaption;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton buttonSave;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.TextEdit textCommand;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ToggleSwitch switchIngame;
        private DevExpress.XtraEditors.ComboBoxEdit comboVisible;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
    }
}