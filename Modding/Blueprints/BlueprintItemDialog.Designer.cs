
namespace source_modding_tool.Modding.Blueprints
{
    partial class BlueprintItemDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlueprintItemDialog));
            this.nameLabel = new DevExpress.XtraEditors.LabelControl();
            this.galleryControl1 = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.installButton = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.statusLabel = new DevExpress.XtraEditors.LabelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.engineEdit = new DevExpress.XtraEditors.TextEdit();
            this.sectionEdit = new DevExpress.XtraEditors.TextEdit();
            this.categoryEdit = new DevExpress.XtraEditors.TextEdit();
            this.descriptionEdit = new DevExpress.XtraEditors.MemoEdit();
            this.authorsEdit = new DevExpress.XtraEditors.MemoEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.descriptionLayout = new DevExpress.XtraLayout.LayoutControlItem();
            this.authorsLayout = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).BeginInit();
            this.galleryControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.engineEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sectionEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.descriptionEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorsEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.descriptionLayout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorsLayout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Appearance.Options.UseFont = true;
            this.nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.nameLabel.Location = new System.Drawing.Point(18, 18);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(168, 35);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "labelControl1";
            // 
            // galleryControl1
            // 
            this.galleryControl1.Controls.Add(this.galleryControlClient1);
            this.galleryControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            // 
            // 
            // 
            galleryItemGroup1.Caption = "Screenshots";
            this.galleryControl1.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup1});
            this.galleryControl1.Gallery.ImageSize = new System.Drawing.Size(120, 67);
            this.galleryControl1.Gallery.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.galleryControl1.Gallery.ShowGroupCaption = false;
            this.galleryControl1.Location = new System.Drawing.Point(0, 464);
            this.galleryControl1.Name = "galleryControl1";
            this.galleryControl1.Size = new System.Drawing.Size(796, 120);
            this.galleryControl1.TabIndex = 3;
            this.galleryControl1.Text = "galleryControl1";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl1;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(792, 99);
            // 
            // installButton
            // 
            this.installButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.installButton.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installButton.Appearance.Options.UseFont = true;
            this.installButton.Location = new System.Drawing.Point(900, 18);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(120, 30);
            this.installButton.TabIndex = 4;
            this.installButton.Text = "Install";
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureEdit1.Location = new System.Drawing.Point(0, 16);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.ShowEditMenuItem = DevExpress.Utils.DefaultBoolean.False;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(796, 448);
            this.pictureEdit1.TabIndex = 5;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.statusLabel);
            this.panelControl1.Controls.Add(this.nameLabel);
            this.panelControl1.Controls.Add(this.installButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(8, 8);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(16);
            this.panelControl1.Size = new System.Drawing.Size(1041, 68);
            this.panelControl1.TabIndex = 6;
            // 
            // statusLabel
            // 
            this.statusLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Appearance.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.statusLabel.Appearance.Options.UseFont = true;
            this.statusLabel.Appearance.Options.UseForeColor = true;
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.statusLabel.Location = new System.Drawing.Point(186, 18);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.statusLabel.Size = new System.Drawing.Size(87, 23);
            this.statusLabel.TabIndex = 5;
            this.statusLabel.Text = "Installed";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.engineEdit);
            this.layoutControl1.Controls.Add(this.sectionEdit);
            this.layoutControl1.Controls.Add(this.categoryEdit);
            this.layoutControl1.Controls.Add(this.descriptionEdit);
            this.layoutControl1.Controls.Add(this.authorsEdit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.layoutControl1.Location = new System.Drawing.Point(804, 76);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(245, 584);
            this.layoutControl1.TabIndex = 7;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // engineEdit
            // 
            this.engineEdit.Location = new System.Drawing.Point(77, 12);
            this.engineEdit.Name = "engineEdit";
            this.engineEdit.Properties.AllowFocused = false;
            this.engineEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.engineEdit.Properties.ReadOnly = true;
            this.engineEdit.Size = new System.Drawing.Size(156, 18);
            this.engineEdit.StyleController = this.layoutControl1;
            this.engineEdit.TabIndex = 4;
            // 
            // sectionEdit
            // 
            this.sectionEdit.Location = new System.Drawing.Point(77, 34);
            this.sectionEdit.Name = "sectionEdit";
            this.sectionEdit.Properties.AllowFocused = false;
            this.sectionEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.sectionEdit.Properties.ReadOnly = true;
            this.sectionEdit.Size = new System.Drawing.Size(156, 18);
            this.sectionEdit.StyleController = this.layoutControl1;
            this.sectionEdit.TabIndex = 5;
            // 
            // categoryEdit
            // 
            this.categoryEdit.Location = new System.Drawing.Point(77, 56);
            this.categoryEdit.Name = "categoryEdit";
            this.categoryEdit.Properties.AllowFocused = false;
            this.categoryEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.categoryEdit.Properties.ReadOnly = true;
            this.categoryEdit.Size = new System.Drawing.Size(156, 18);
            this.categoryEdit.StyleController = this.layoutControl1;
            this.categoryEdit.TabIndex = 6;
            // 
            // descriptionEdit
            // 
            this.descriptionEdit.Location = new System.Drawing.Point(77, 78);
            this.descriptionEdit.Name = "descriptionEdit";
            this.descriptionEdit.Properties.AllowFocused = false;
            this.descriptionEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.descriptionEdit.Properties.ReadOnly = true;
            this.descriptionEdit.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.descriptionEdit.Size = new System.Drawing.Size(156, 245);
            this.descriptionEdit.StyleController = this.layoutControl1;
            this.descriptionEdit.TabIndex = 7;
            // 
            // authorsEdit
            // 
            this.authorsEdit.Location = new System.Drawing.Point(77, 327);
            this.authorsEdit.Name = "authorsEdit";
            this.authorsEdit.Properties.AllowFocused = false;
            this.authorsEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.authorsEdit.Properties.ReadOnly = true;
            this.authorsEdit.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.authorsEdit.Size = new System.Drawing.Size(156, 120);
            this.authorsEdit.StyleController = this.layoutControl1;
            this.authorsEdit.TabIndex = 8;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.descriptionLayout,
            this.authorsLayout});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(245, 584);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.engineEdit;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(225, 22);
            this.layoutControlItem1.Text = "Engine";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(53, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sectionEdit;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 22);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(225, 22);
            this.layoutControlItem2.Text = "Section";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(53, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.categoryEdit;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(225, 22);
            this.layoutControlItem3.Text = "Category";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(53, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 439);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(225, 125);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // descriptionLayout
            // 
            this.descriptionLayout.Control = this.descriptionEdit;
            this.descriptionLayout.Location = new System.Drawing.Point(0, 66);
            this.descriptionLayout.Name = "descriptionLayout";
            this.descriptionLayout.Size = new System.Drawing.Size(225, 249);
            this.descriptionLayout.Text = "Description";
            this.descriptionLayout.TextSize = new System.Drawing.Size(53, 13);
            // 
            // authorsLayout
            // 
            this.authorsLayout.Control = this.authorsEdit;
            this.authorsLayout.Location = new System.Drawing.Point(0, 315);
            this.authorsLayout.Name = "authorsLayout";
            this.authorsLayout.Size = new System.Drawing.Size(225, 124);
            this.authorsLayout.Text = "Authors";
            this.authorsLayout.TextSize = new System.Drawing.Size(53, 13);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.pictureEdit1);
            this.panelControl2.Controls.Add(this.galleryControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(8, 76);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.panelControl2.Size = new System.Drawing.Size(796, 584);
            this.panelControl2.TabIndex = 8;
            // 
            // BlueprintItemDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 668);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("BlueprintItemDialog.IconOptions.Image")));
            this.Name = "BlueprintItemDialog";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Blueprint";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BlueprintItemDialog_FormClosing);
            this.Load += new System.EventHandler(this.BlueprintItemDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).EndInit();
            this.galleryControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.engineEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sectionEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoryEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.descriptionEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorsEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.descriptionLayout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorsLayout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl nameLabel;
        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraEditors.SimpleButton installButton;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit engineEdit;
        private DevExpress.XtraEditors.TextEdit sectionEdit;
        private DevExpress.XtraEditors.TextEdit categoryEdit;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.MemoEdit descriptionEdit;
        private DevExpress.XtraLayout.LayoutControlItem descriptionLayout;
        private DevExpress.XtraEditors.MemoEdit authorsEdit;
        private DevExpress.XtraLayout.LayoutControlItem authorsLayout;
        private DevExpress.XtraEditors.LabelControl statusLabel;
    }
}