
namespace xblah_modding_tool.Modding.Blueprints
{
    partial class BlueprintCategoriesDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlueprintCategoriesDialog));
            this.galleryControl1 = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.categoriesControl = new DevExpress.XtraBars.Navigation.AccordionControl();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).BeginInit();
            this.galleryControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoriesControl)).BeginInit();
            this.SuspendLayout();
            // 
            // galleryControl1
            // 
            this.galleryControl1.Controls.Add(this.galleryControlClient1);
            this.galleryControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.galleryControl1.Gallery.Appearance.ItemDescriptionAppearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(80)))));
            this.galleryControl1.Gallery.Appearance.ItemDescriptionAppearance.Normal.Options.UseForeColor = true;
            galleryItemGroup1.Caption = "Items";
            this.galleryControl1.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup1});
            this.galleryControl1.Gallery.ImageSize = new System.Drawing.Size(320, 180);
            this.galleryControl1.Gallery.ShowItemText = true;
            this.galleryControl1.Location = new System.Drawing.Point(260, 0);
            this.galleryControl1.Name = "galleryControl1";
            this.galleryControl1.Size = new System.Drawing.Size(1123, 646);
            this.galleryControl1.TabIndex = 0;
            this.galleryControl1.Text = "galleryControl1";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl1;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(1102, 642);
            // 
            // categoriesControl
            // 
            this.categoriesControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.categoriesControl.Location = new System.Drawing.Point(0, 0);
            this.categoriesControl.Name = "categoriesControl";
            this.categoriesControl.Size = new System.Drawing.Size(260, 646);
            this.categoriesControl.TabIndex = 1;
            // 
            // BlueprintCategoriesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 646);
            this.Controls.Add(this.galleryControl1);
            this.Controls.Add(this.categoriesControl);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("BlueprintCategoriesDialog.IconOptions.Image")));
            this.Name = "BlueprintCategoriesDialog";
            this.Text = "Blueprints";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrefabsWorkshop_FormClosing);
            this.Load += new System.EventHandler(this.PrefabsWorkshop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).EndInit();
            this.galleryControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.categoriesControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraBars.Navigation.AccordionControl categoriesControl;
    }
}