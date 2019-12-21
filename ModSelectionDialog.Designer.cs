namespace windows_source1ide
{
    partial class ModSelectionDialog
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
            this.modsCombo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.gamesCombo = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.modsCombo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gamesCombo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // modsCombo
            // 
            this.modsCombo.Location = new System.Drawing.Point(118, 12);
            this.modsCombo.Name = "modsCombo";
            this.modsCombo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.modsCombo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.modsCombo.Size = new System.Drawing.Size(100, 20);
            this.modsCombo.TabIndex = 1;
            this.modsCombo.TextChanged += new System.EventHandler(this.modsCombo_TextChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton1.Location = new System.Drawing.Point(143, 38);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "OK";
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(62, 38);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "Cancel";
            // 
            // gamesCombo
            // 
            this.gamesCombo.Location = new System.Drawing.Point(12, 12);
            this.gamesCombo.Name = "gamesCombo";
            this.gamesCombo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gamesCombo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.gamesCombo.Size = new System.Drawing.Size(100, 20);
            this.gamesCombo.TabIndex = 4;
            this.gamesCombo.TextChanged += new System.EventHandler(this.gamesCombo_TextChanged);
            // 
            // ModSelectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 75);
            this.Controls.Add(this.gamesCombo);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.modsCombo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModSelectionDialog";
            this.Text = "Select a mod";
            this.Load += new System.EventHandler(this.ModSelectionDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.modsCombo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gamesCombo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.ComboBoxEdit modsCombo;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.ComboBoxEdit gamesCombo;
    }
}