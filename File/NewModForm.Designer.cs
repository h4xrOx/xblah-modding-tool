namespace source_modding_tool
{
    partial class NewModForm
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
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup1 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItem galleryItem1 = new DevExpress.XtraBars.Ribbon.GalleryItem();
            DevExpress.XtraBars.Ribbon.GalleryItem galleryItem2 = new DevExpress.XtraBars.Ribbon.GalleryItem();
            DevExpress.XtraBars.Ribbon.GalleryItem galleryItem3 = new DevExpress.XtraBars.Ribbon.GalleryItem();
            DevExpress.XtraBars.Ribbon.GalleryItem galleryItem4 = new DevExpress.XtraBars.Ribbon.GalleryItem();
            DevExpress.XtraBars.Ribbon.GalleryItem galleryItem5 = new DevExpress.XtraBars.Ribbon.GalleryItem();
            DevExpress.XtraBars.Ribbon.GalleryItem galleryItem6 = new DevExpress.XtraBars.Ribbon.GalleryItem();
            DevExpress.XtraBars.Ribbon.GalleryItem galleryItem7 = new DevExpress.XtraBars.Ribbon.GalleryItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewModForm));
            this.textFolder = new DevExpress.XtraEditors.TextEdit();
            this.createButton = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.labelFolderInfo = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.textModsPath = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gameGallery = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textModsPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameGallery)).BeginInit();
            this.gameGallery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.SuspendLayout();
            // 
            // textFolder
            // 
            this.textFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.textFolder.Location = new System.Drawing.Point(0, 0);
            this.textFolder.Name = "textFolder";
            this.textFolder.Size = new System.Drawing.Size(350, 20);
            this.textFolder.TabIndex = 1;
            this.textFolder.EditValueChanged += new System.EventHandler(this.textFolder_EditValueChanged);
            // 
            // createButton
            // 
            this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.createButton.Enabled = false;
            this.createButton.Location = new System.Drawing.Point(902, 8);
            this.createButton.Margin = new System.Windows.Forms.Padding(8);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 23);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "Create";
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.labelFolderInfo);
            this.panelControl4.Controls.Add(this.textFolder);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(617, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(350, 35);
            this.panelControl4.TabIndex = 1;
            // 
            // labelFolderInfo
            // 
            this.labelFolderInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelFolderInfo.Location = new System.Drawing.Point(287, 20);
            this.labelFolderInfo.Name = "labelFolderInfo";
            this.labelFolderInfo.Size = new System.Drawing.Size(63, 13);
            this.labelFolderInfo.TabIndex = 7;
            this.labelFolderInfo.Text = "Invalid folder";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.createButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 286);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(985, 39);
            this.panelControl1.TabIndex = 9;
            // 
            // textModsPath
            // 
            this.textModsPath.Dock = System.Windows.Forms.DockStyle.Left;
            this.textModsPath.Location = new System.Drawing.Point(2, 2);
            this.textModsPath.Name = "textModsPath";
            this.textModsPath.Properties.ReadOnly = true;
            this.textModsPath.Size = new System.Drawing.Size(615, 20);
            this.textModsPath.TabIndex = 5;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gameGallery);
            this.panelControl2.Controls.Add(this.panelControl5);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Padding = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.panelControl2.Size = new System.Drawing.Size(985, 286);
            this.panelControl2.TabIndex = 10;
            // 
            // gameGallery
            // 
            this.gameGallery.Controls.Add(this.galleryControlClient1);
            this.gameGallery.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            galleryItemGroup1.Caption = "Games";
            galleryItem1.Caption = "Half-Life: Alyx";
            galleryItem1.Enabled = false;
            galleryItem1.ImageOptions.Image = global::source_modding_tool.Properties.Resources.hl_notinstalled;
            galleryItem1.Tag = "goldsrc/Half-Life/valve";
            galleryItem2.Caption = "Half-Life 2";
            galleryItem2.Enabled = false;
            galleryItem2.ImageOptions.Image = global::source_modding_tool.Properties.Resources.hl2_notinstalled;
            galleryItem2.Tag = "source/Source SDK Base 2013 Singleplayer/hl2";
            galleryItem3.Caption = "Half-Life 2: Deathmatch";
            galleryItem3.Enabled = false;
            galleryItem3.ImageOptions.Image = global::source_modding_tool.Properties.Resources.hl2mp_notinstalled;
            galleryItem3.Tag = "source/Source SDK Base 2013 Multiplayer/hl2mp";
            galleryItem4.Caption = "Half-Life 2: Episode One";
            galleryItem4.Enabled = false;
            galleryItem4.Hint = "hey";
            galleryItem4.ImageOptions.Image = global::source_modding_tool.Properties.Resources.episodic_notinstalled;
            galleryItem4.Tag = "source/Source SDK Base 2013 Singleplayer/episodic";
            galleryItem5.Caption = "Half-Life 2: Episode Two";
            galleryItem5.Enabled = false;
            galleryItem5.ImageOptions.Image = global::source_modding_tool.Properties.Resources.ep2_notinstalled;
            galleryItem5.Tag = "source/Source SDK Base 2013 Singleplayer/ep2";
            galleryItem6.Caption = "Portal";
            galleryItem6.Enabled = false;
            galleryItem6.ImageOptions.Image = global::source_modding_tool.Properties.Resources.portal_notinstalled;
            galleryItem6.Tag = "source/Portal/portal";
            galleryItem7.Caption = "Half-Life: Alyx";
            galleryItem7.Enabled = false;
            galleryItem7.ImageOptions.Image = global::source_modding_tool.Properties.Resources.hla_notinstalled;
            galleryItem7.Tag = "source2/Half-Life Alyx/hlvr";
            galleryItemGroup1.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            galleryItem1,
            galleryItem2,
            galleryItem3,
            galleryItem4,
            galleryItem5,
            galleryItem6,
            galleryItem7});
            this.gameGallery.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup1});
            this.gameGallery.Gallery.ImageSize = new System.Drawing.Size(111, 166);
            this.gameGallery.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleCheck;
            this.gameGallery.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            this.gameGallery.Gallery.ItemCheckedChanged += new DevExpress.XtraBars.Ribbon.GalleryItemEventHandler(this.gameGallery_ItemCheckedChanged);
            this.gameGallery.Location = new System.Drawing.Point(8, 8);
            this.gameGallery.Name = "gameGallery";
            this.gameGallery.Size = new System.Drawing.Size(969, 239);
            this.gameGallery.TabIndex = 1;
            this.gameGallery.Text = "galleryGames";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.gameGallery;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(948, 235);
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.panelControl4);
            this.panelControl5.Controls.Add(this.textModsPath);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl5.Location = new System.Drawing.Point(8, 247);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(969, 39);
            this.panelControl5.TabIndex = 2;
            // 
            // panelControl3
            // 
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 171);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(677, 100);
            this.panelControl3.TabIndex = 0;
            // 
            // NewModForm
            // 
            this.AcceptButton = this.createButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 325);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewModForm";
            this.Text = "New Mod";
            this.Load += new System.EventHandler(this.NewModForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textModsPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gameGallery)).EndInit();
            this.gameGallery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit textFolder;
        private DevExpress.XtraEditors.SimpleButton createButton;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelFolderInfo;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraBars.Ribbon.GalleryControl gameGallery;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraEditors.TextEdit textModsPath;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl5;
    }
}