
namespace source_modding_tool.Sound
{
    partial class SoundscapeEditor
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
            this.browseSoundButton = new DevExpress.XtraEditors.SimpleButton();
            this.soundscapesTree = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.addButton = new DevExpress.XtraEditors.SimpleButton();
            this.soundscapeNameEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.soundscapeRulesTree = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.addRuleButton = new DevExpress.XtraEditors.SimpleButton();
            this.ruleCombo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.fileEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.playButton = new DevExpress.XtraEditors.SimpleButton();
            this.stopButton = new DevExpress.XtraEditors.SimpleButton();
            this.minVolumeSpin = new DevExpress.XtraEditors.SpinEdit();
            this.maxVolumeSpin = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.soundscapesTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soundscapeNameEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soundscapeRulesTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ruleCombo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minVolumeSpin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxVolumeSpin.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // browseSoundButton
            // 
            this.browseSoundButton.Enabled = false;
            this.browseSoundButton.Location = new System.Drawing.Point(601, 78);
            this.browseSoundButton.Name = "browseSoundButton";
            this.browseSoundButton.Size = new System.Drawing.Size(75, 23);
            this.browseSoundButton.TabIndex = 3;
            this.browseSoundButton.Text = "Browse";
            this.browseSoundButton.Click += new System.EventHandler(this.browseSoundButton_Click);
            // 
            // soundscapesTree
            // 
            this.soundscapesTree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.soundscapesTree.Location = new System.Drawing.Point(12, 12);
            this.soundscapesTree.Name = "soundscapesTree";
            this.soundscapesTree.OptionsBehavior.Editable = false;
            this.soundscapesTree.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.soundscapesTree.Size = new System.Drawing.Size(206, 292);
            this.soundscapesTree.TabIndex = 4;
            this.soundscapesTree.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.soundscapesTree_FocusedNodeChanged);
            this.soundscapesTree.Click += new System.EventHandler(this.soundscapesTree_Click);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Name";
            this.treeListColumn1.FieldName = "name";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(143, 310);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "Add";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // soundscapeNameEdit
            // 
            this.soundscapeNameEdit.Enabled = false;
            this.soundscapeNameEdit.Location = new System.Drawing.Point(257, 14);
            this.soundscapeNameEdit.Name = "soundscapeNameEdit";
            this.soundscapeNameEdit.Size = new System.Drawing.Size(313, 20);
            this.soundscapeNameEdit.TabIndex = 6;
            this.soundscapeNameEdit.EditValueChanged += new System.EventHandler(this.soundscapeNameEdit_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(224, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Name";
            // 
            // soundscapeRulesTree
            // 
            this.soundscapeRulesTree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.soundscapeRulesTree.Location = new System.Drawing.Point(224, 40);
            this.soundscapeRulesTree.Name = "soundscapeRulesTree";
            this.soundscapeRulesTree.OptionsBehavior.Editable = false;
            this.soundscapeRulesTree.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.soundscapeRulesTree.Size = new System.Drawing.Size(170, 264);
            this.soundscapeRulesTree.TabIndex = 8;
            this.soundscapeRulesTree.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.soundscapeRulesTree_FocusedNodeChanged);
            this.soundscapeRulesTree.Click += new System.EventHandler(this.soundscapeRulesTree_Click);
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Type";
            this.treeListColumn2.FieldName = "type";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // addRuleButton
            // 
            this.addRuleButton.Location = new System.Drawing.Point(319, 310);
            this.addRuleButton.Name = "addRuleButton";
            this.addRuleButton.Size = new System.Drawing.Size(75, 23);
            this.addRuleButton.TabIndex = 9;
            this.addRuleButton.Text = "Add";
            this.addRuleButton.Click += new System.EventHandler(this.addRuleButton_Click);
            // 
            // ruleCombo
            // 
            this.ruleCombo.EditValue = "";
            this.ruleCombo.Enabled = false;
            this.ruleCombo.Location = new System.Drawing.Point(427, 54);
            this.ruleCombo.Name = "ruleCombo";
            this.ruleCombo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ruleCombo.Properties.Items.AddRange(new object[] {
            "Looping",
            "Random"});
            this.ruleCombo.Size = new System.Drawing.Size(168, 20);
            this.ruleCombo.TabIndex = 10;
            this.ruleCombo.EditValueChanged += new System.EventHandler(this.ruleCombo_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(400, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(21, 13);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "Rule";
            // 
            // fileEdit
            // 
            this.fileEdit.Location = new System.Drawing.Point(427, 80);
            this.fileEdit.Name = "fileEdit";
            this.fileEdit.Properties.ReadOnly = true;
            this.fileEdit.Size = new System.Drawing.Size(168, 20);
            this.fileEdit.TabIndex = 12;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(400, 83);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(16, 13);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "File";
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(613, 349);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 14;
            this.playButton.Text = "Play";
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(532, 349);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 15;
            this.stopButton.Text = "Stop";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // minVolumeSpin
            // 
            this.minVolumeSpin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.minVolumeSpin.Location = new System.Drawing.Point(444, 125);
            this.minVolumeSpin.Name = "minVolumeSpin";
            this.minVolumeSpin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.minVolumeSpin.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.minVolumeSpin.Properties.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minVolumeSpin.Size = new System.Drawing.Size(100, 20);
            this.minVolumeSpin.TabIndex = 16;
            this.minVolumeSpin.ValueChanged += new System.EventHandler(this.minVolumeSpin_ValueChanged);
            // 
            // maxVolumeSpin
            // 
            this.maxVolumeSpin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.maxVolumeSpin.Location = new System.Drawing.Point(550, 125);
            this.maxVolumeSpin.Name = "maxVolumeSpin";
            this.maxVolumeSpin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.maxVolumeSpin.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.maxVolumeSpin.Properties.MaxValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxVolumeSpin.Size = new System.Drawing.Size(100, 20);
            this.maxVolumeSpin.TabIndex = 17;
            this.maxVolumeSpin.ValueChanged += new System.EventHandler(this.minVolumeSpin_ValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(400, 128);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(34, 13);
            this.labelControl4.TabIndex = 18;
            this.labelControl4.Text = "Volume";
            // 
            // SoundscapeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 384);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.maxVolumeSpin);
            this.Controls.Add(this.minVolumeSpin);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.fileEdit);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.ruleCombo);
            this.Controls.Add(this.addRuleButton);
            this.Controls.Add(this.soundscapeRulesTree);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.soundscapeNameEdit);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.soundscapesTree);
            this.Controls.Add(this.browseSoundButton);
            this.Name = "SoundscapeEditor";
            this.Text = "Sound Browser";
            ((System.ComponentModel.ISupportInitialize)(this.soundscapesTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soundscapeNameEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soundscapeRulesTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ruleCombo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minVolumeSpin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxVolumeSpin.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton browseSoundButton;
        private DevExpress.XtraTreeList.TreeList soundscapesTree;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.SimpleButton addButton;
        private DevExpress.XtraEditors.TextEdit soundscapeNameEdit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraTreeList.TreeList soundscapeRulesTree;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.SimpleButton addRuleButton;
        private DevExpress.XtraEditors.ComboBoxEdit ruleCombo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit fileEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton playButton;
        private DevExpress.XtraEditors.SimpleButton stopButton;
        private DevExpress.XtraEditors.SpinEdit minVolumeSpin;
        private DevExpress.XtraEditors.SpinEdit maxVolumeSpin;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}