namespace windows_source1ide
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
            this.pictureToolTexture = new DevExpress.XtraEditors.PictureEdit();
            this.pictureBaseTexture = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit3 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit4 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit5 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit6 = new DevExpress.XtraEditors.PictureEdit();
            this.memoEdit = new DevExpress.XtraEditors.MemoEdit();
            this.openFileDialog = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonCreateToolTexture = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureToolTexture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBaseTexture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureToolTexture
            // 
            this.pictureToolTexture.Location = new System.Drawing.Point(6, 6);
            this.pictureToolTexture.Margin = new System.Windows.Forms.Padding(4);
            this.pictureToolTexture.Name = "pictureToolTexture";
            this.pictureToolTexture.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureToolTexture.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureToolTexture.Properties.Tag = "tooltexture";
            this.pictureToolTexture.Size = new System.Drawing.Size(128, 128);
            this.pictureToolTexture.TabIndex = 0;
            this.pictureToolTexture.Tag = "tooltexture";
            this.pictureToolTexture.Click += new System.EventHandler(this.picture_Click);
            // 
            // pictureBaseTexture
            // 
            this.pictureBaseTexture.Location = new System.Drawing.Point(142, 6);
            this.pictureBaseTexture.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBaseTexture.Name = "pictureBaseTexture";
            this.pictureBaseTexture.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureBaseTexture.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureBaseTexture.Properties.Tag = "basetexture";
            this.pictureBaseTexture.Size = new System.Drawing.Size(128, 128);
            this.pictureBaseTexture.TabIndex = 1;
            this.pictureBaseTexture.Tag = "basetexture";
            this.pictureBaseTexture.Click += new System.EventHandler(this.picture_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(142, 141);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Base texture";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 341);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(27, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Detail";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(855, 159);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(112, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Blend modulate texture";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(587, 159);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(46, 13);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Bumpmap";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(721, 159);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(58, 13);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "Parallaxmap";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(278, 141);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(71, 13);
            this.labelControl7.TabIndex = 8;
            this.labelControl7.Text = "Base texture 2";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(6, 141);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(59, 13);
            this.labelControl8.TabIndex = 9;
            this.labelControl8.Text = "Tool texture";
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.Location = new System.Drawing.Point(278, 6);
            this.pictureEdit2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit2.Properties.Tag = "basetexture2";
            this.pictureEdit2.Size = new System.Drawing.Size(128, 128);
            this.pictureEdit2.TabIndex = 10;
            this.pictureEdit2.Tag = "basetexture2";
            this.pictureEdit2.Click += new System.EventHandler(this.picture_Click);
            // 
            // pictureEdit3
            // 
            this.pictureEdit3.Location = new System.Drawing.Point(579, 17);
            this.pictureEdit3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit3.Properties.Tag = "bumpmap";
            this.pictureEdit3.Size = new System.Drawing.Size(128, 128);
            this.pictureEdit3.TabIndex = 11;
            this.pictureEdit3.Tag = "bumpmap";
            this.pictureEdit3.Click += new System.EventHandler(this.picture_Click);
            // 
            // pictureEdit4
            // 
            this.pictureEdit4.Location = new System.Drawing.Point(713, 17);
            this.pictureEdit4.Margin = new System.Windows.Forms.Padding(4);
            this.pictureEdit4.Name = "pictureEdit4";
            this.pictureEdit4.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit4.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit4.Properties.Tag = "parallaxmap";
            this.pictureEdit4.Size = new System.Drawing.Size(128, 128);
            this.pictureEdit4.TabIndex = 12;
            this.pictureEdit4.Tag = "parallaxmap";
            this.pictureEdit4.Click += new System.EventHandler(this.picture_Click);
            // 
            // pictureEdit5
            // 
            this.pictureEdit5.Location = new System.Drawing.Point(847, 17);
            this.pictureEdit5.Margin = new System.Windows.Forms.Padding(4);
            this.pictureEdit5.Name = "pictureEdit5";
            this.pictureEdit5.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit5.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit5.Properties.Tag = "blendmodulatetexture";
            this.pictureEdit5.Size = new System.Drawing.Size(128, 128);
            this.pictureEdit5.TabIndex = 13;
            this.pictureEdit5.Tag = "blendmodulatetexture";
            this.pictureEdit5.Click += new System.EventHandler(this.picture_Click);
            // 
            // pictureEdit6
            // 
            this.pictureEdit6.Location = new System.Drawing.Point(17, 206);
            this.pictureEdit6.Margin = new System.Windows.Forms.Padding(4);
            this.pictureEdit6.Name = "pictureEdit6";
            this.pictureEdit6.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit6.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit6.Size = new System.Drawing.Size(128, 128);
            this.pictureEdit6.TabIndex = 14;
            this.pictureEdit6.Tag = "detail";
            // 
            // memoEdit
            // 
            this.memoEdit.Location = new System.Drawing.Point(160, 260);
            this.memoEdit.Name = "memoEdit";
            this.memoEdit.Size = new System.Drawing.Size(262, 74);
            this.memoEdit.TabIndex = 15;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.EditValue = "LightmappedGeneric";
            this.comboBoxEdit1.Location = new System.Drawing.Point(161, 220);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "Cable",
            "EyeRefract",
            "Eyes",
            "LightmappedGeneric",
            "Modulate",
            "MonitorScreen",
            "Predator",
            "Refract",
            "ShatteredGlass",
            "Sprite",
            "Teeth",
            "UnlitGeneric",
            "UnlitTwoTexture",
            "VertexLitGeneric",
            "VortWarp",
            "Water",
            "WindowImposter",
            "WorldTwoTextureBlend",
            "WorldVertexAlpha",
            "WorldVertexTransition",
            "WriteZ"});
            this.comboBoxEdit1.Properties.Sorted = true;
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(262, 20);
            this.comboBoxEdit1.TabIndex = 16;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(169, 209);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(34, 13);
            this.labelControl6.TabIndex = 17;
            this.labelControl6.Text = "Shader";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(1008, 10);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 18;
            this.simpleButton1.Text = "Save";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Location = new System.Drawing.Point(925, 10);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(8);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 19;
            this.simpleButton2.Text = "Cancel";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(8, 393);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1081, 31);
            this.panelControl1.TabIndex = 20;
            // 
            // buttonCreateToolTexture
            // 
            this.buttonCreateToolTexture.Location = new System.Drawing.Point(348, 341);
            this.buttonCreateToolTexture.Name = "buttonCreateToolTexture";
            this.buttonCreateToolTexture.Size = new System.Drawing.Size(75, 23);
            this.buttonCreateToolTexture.TabIndex = 21;
            this.buttonCreateToolTexture.Text = "Create";
            this.buttonCreateToolTexture.Click += new System.EventHandler(this.buttonCreateToolTexture_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.pictureBaseTexture);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.pictureEdit2);
            this.panelControl2.Controls.Add(this.labelControl7);
            this.panelControl2.Controls.Add(this.pictureToolTexture);
            this.panelControl2.Controls.Add(this.labelControl8);
            this.panelControl2.Location = new System.Drawing.Point(11, 11);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(411, 160);
            this.panelControl2.TabIndex = 22;
            // 
            // MaterialEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 432);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.buttonCreateToolTexture);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.memoEdit);
            this.Controls.Add(this.pictureEdit6);
            this.Controls.Add(this.pictureEdit5);
            this.Controls.Add(this.pictureEdit4);
            this.Controls.Add(this.pictureEdit3);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Name = "MaterialEditor";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Material Editor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureToolTexture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBaseTexture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureToolTexture;
        private DevExpress.XtraEditors.PictureEdit pictureBaseTexture;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit3;
        private DevExpress.XtraEditors.PictureEdit pictureEdit4;
        private DevExpress.XtraEditors.PictureEdit pictureEdit5;
        private DevExpress.XtraEditors.PictureEdit pictureEdit6;
        private DevExpress.XtraEditors.MemoEdit memoEdit;
        private DevExpress.XtraEditors.XtraOpenFileDialog openFileDialog;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton buttonCreateToolTexture;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}