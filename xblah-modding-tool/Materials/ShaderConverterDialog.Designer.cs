
namespace xblah_modding_tool.Materials
{
    partial class ShaderConverterDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShaderConverterDialog));
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.oldNameColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.newNameColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.buttonConvert = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.oldNameColumn,
            this.newNameColumn});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(8, 8);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(370, 263);
            this.treeList1.TabIndex = 0;
            // 
            // oldNameColumn
            // 
            this.oldNameColumn.Caption = "Old Name";
            this.oldNameColumn.FieldName = "oldName";
            this.oldNameColumn.Name = "oldNameColumn";
            this.oldNameColumn.Visible = true;
            this.oldNameColumn.VisibleIndex = 0;
            // 
            // newNameColumn
            // 
            this.newNameColumn.Caption = "New Name";
            this.newNameColumn.FieldName = "newName";
            this.newNameColumn.Name = "newNameColumn";
            this.newNameColumn.Visible = true;
            this.newNameColumn.VisibleIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.buttonCancel);
            this.panelControl1.Controls.Add(this.buttonConvert);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(8, 271);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(370, 31);
            this.panelControl1.TabIndex = 7;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(214, 8);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonConvert
            // 
            this.buttonConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConvert.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonConvert.Location = new System.Drawing.Point(295, 8);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(75, 23);
            this.buttonConvert.TabIndex = 3;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // ShaderConverterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 310);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("ShaderConverterDialog.IconOptions.Image")));
            this.Name = "ShaderConverterDialog";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Shader Converter";
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn oldNameColumn;
        private DevExpress.XtraTreeList.Columns.TreeListColumn newNameColumn;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.SimpleButton buttonConvert;
    }
}