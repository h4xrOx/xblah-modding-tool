
namespace source_modding_tool.Materials.ShaderTabs
{
    partial class UnlitGenericTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.switchNoFog = new DevExpress.XtraEditors.ToggleSwitch();
            this.editColor = new DevExpress.XtraEditors.ColorPickEdit();
            this.pictureBaseTexture = new DevExpress.XtraEditors.PictureEdit();
            this.comboSurfaceProp = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.basicsGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutBaseTexture = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutSurfaceProp = new DevExpress.XtraLayout.LayoutControlItem();
            this.adjustmentGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutColor = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutNoFog = new DevExpress.XtraLayout.LayoutControlItem();
            this.pictureToolTexture = new DevExpress.XtraEditors.PictureEdit();
            this.layoutToolTexture = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.switchNoFog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBaseTexture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboSurfaceProp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.basicsGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBaseTexture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSurfaceProp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustmentGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutNoFog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureToolTexture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutToolTexture)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.pictureToolTexture);
            this.layoutControl.Controls.Add(this.switchNoFog);
            this.layoutControl.Controls.Add(this.editColor);
            this.layoutControl.Controls.Add(this.pictureBaseTexture);
            this.layoutControl.Controls.Add(this.comboSurfaceProp);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(276, 588);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // switchNoFog
            // 
            this.switchNoFog.EditValue = true;
            this.switchNoFog.Location = new System.Drawing.Point(118, 327);
            this.switchNoFog.Name = "switchNoFog";
            this.switchNoFog.Properties.OffText = "Off";
            this.switchNoFog.Properties.OnText = "On";
            this.switchNoFog.Size = new System.Drawing.Size(134, 20);
            this.switchNoFog.StyleController = this.layoutControl;
            this.switchNoFog.TabIndex = 7;
            this.switchNoFog.Tag = "nofog";
            // 
            // editColor
            // 
            this.editColor.EditValue = System.Drawing.Color.White;
            this.editColor.Location = new System.Drawing.Point(118, 254);
            this.editColor.Name = "editColor";
            this.editColor.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.editColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editColor.Size = new System.Drawing.Size(134, 20);
            this.editColor.StyleController = this.layoutControl;
            this.editColor.TabIndex = 6;
            this.editColor.Tag = "color";
            // 
            // pictureBaseTexture
            // 
            this.pictureBaseTexture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBaseTexture.Location = new System.Drawing.Point(118, 49);
            this.pictureBaseTexture.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBaseTexture.MaximumSize = new System.Drawing.Size(128, 128);
            this.pictureBaseTexture.MinimumSize = new System.Drawing.Size(128, 128);
            this.pictureBaseTexture.Name = "pictureBaseTexture";
            this.pictureBaseTexture.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureBaseTexture.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureBaseTexture.Properties.Tag = "basetexture";
            this.pictureBaseTexture.Size = new System.Drawing.Size(128, 128);
            this.pictureBaseTexture.StyleController = this.layoutControl;
            toolTipTitleItem2.Text = "Base Texture";
            toolTipItem2.Text = " It defines an albedo texture.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.pictureBaseTexture.SuperTip = superToolTip2;
            this.pictureBaseTexture.TabIndex = 4;
            this.pictureBaseTexture.Tag = "basetexture";
            this.pictureBaseTexture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBaseTexture_MouseClick);
            // 
            // comboSurfaceProp
            // 
            this.comboSurfaceProp.EditValue = "default";
            this.comboSurfaceProp.Location = new System.Drawing.Point(118, 181);
            this.comboSurfaceProp.Name = "comboSurfaceProp";
            this.comboSurfaceProp.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.comboSurfaceProp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboSurfaceProp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboSurfaceProp.Size = new System.Drawing.Size(134, 20);
            this.comboSurfaceProp.StyleController = this.layoutControl;
            this.comboSurfaceProp.TabIndex = 5;
            this.comboSurfaceProp.Tag = "surfaceprop";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.basicsGroup,
            this.adjustmentGroup,
            this.layoutControlGroup1,
            this.layoutToolTexture});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(276, 588);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 351);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(256, 85);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // basicsGroup
            // 
            this.basicsGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutBaseTexture,
            this.layoutSurfaceProp});
            this.basicsGroup.Location = new System.Drawing.Point(0, 0);
            this.basicsGroup.Name = "basicsGroup";
            this.basicsGroup.Size = new System.Drawing.Size(256, 205);
            this.basicsGroup.Text = "Basics";
            // 
            // layoutBaseTexture
            // 
            this.layoutBaseTexture.Control = this.pictureBaseTexture;
            this.layoutBaseTexture.Location = new System.Drawing.Point(0, 0);
            this.layoutBaseTexture.Name = "layoutBaseTexture";
            this.layoutBaseTexture.Size = new System.Drawing.Size(232, 132);
            this.layoutBaseTexture.Text = "Albedo";
            this.layoutBaseTexture.TextSize = new System.Drawing.Size(82, 13);
            // 
            // layoutSurfaceProp
            // 
            this.layoutSurfaceProp.Control = this.comboSurfaceProp;
            this.layoutSurfaceProp.Location = new System.Drawing.Point(0, 132);
            this.layoutSurfaceProp.Name = "layoutSurfaceProp";
            this.layoutSurfaceProp.Size = new System.Drawing.Size(232, 24);
            this.layoutSurfaceProp.Text = "Surface property";
            this.layoutSurfaceProp.TextSize = new System.Drawing.Size(82, 13);
            // 
            // adjustmentGroup
            // 
            this.adjustmentGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutColor});
            this.adjustmentGroup.Location = new System.Drawing.Point(0, 205);
            this.adjustmentGroup.Name = "adjustmentGroup";
            this.adjustmentGroup.Size = new System.Drawing.Size(256, 73);
            this.adjustmentGroup.Text = "Adjustment";
            // 
            // layoutColor
            // 
            this.layoutColor.Control = this.editColor;
            this.layoutColor.Location = new System.Drawing.Point(0, 0);
            this.layoutColor.Name = "layoutColor";
            this.layoutColor.Size = new System.Drawing.Size(232, 24);
            this.layoutColor.Text = "Color";
            this.layoutColor.TextSize = new System.Drawing.Size(82, 13);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutNoFog});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 278);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(256, 73);
            this.layoutControlGroup1.Text = "Effect";
            // 
            // layoutNoFog
            // 
            this.layoutNoFog.Control = this.switchNoFog;
            this.layoutNoFog.Location = new System.Drawing.Point(0, 0);
            this.layoutNoFog.Name = "layoutNoFog";
            this.layoutNoFog.Size = new System.Drawing.Size(232, 24);
            this.layoutNoFog.Text = "Affected by fog";
            this.layoutNoFog.TextSize = new System.Drawing.Size(82, 13);
            // 
            // pictureToolTexture
            // 
            this.pictureToolTexture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureToolTexture.Location = new System.Drawing.Point(106, 448);
            this.pictureToolTexture.Margin = new System.Windows.Forms.Padding(4);
            this.pictureToolTexture.MaximumSize = new System.Drawing.Size(128, 128);
            this.pictureToolTexture.MinimumSize = new System.Drawing.Size(128, 128);
            this.pictureToolTexture.Name = "pictureToolTexture";
            this.pictureToolTexture.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureToolTexture.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureToolTexture.Properties.Tag = "basetexture";
            this.pictureToolTexture.Size = new System.Drawing.Size(128, 128);
            this.pictureToolTexture.StyleController = this.layoutControl;
            toolTipTitleItem1.Text = "Base Texture";
            toolTipItem1.Text = " It defines an albedo texture.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.pictureToolTexture.SuperTip = superToolTip1;
            this.pictureToolTexture.TabIndex = 8;
            this.pictureToolTexture.Tag = "tooltexture";
            // 
            // layoutToolTexture
            // 
            this.layoutToolTexture.Control = this.pictureToolTexture;
            this.layoutToolTexture.Location = new System.Drawing.Point(0, 436);
            this.layoutToolTexture.Name = "layoutToolTexture";
            this.layoutToolTexture.Size = new System.Drawing.Size(256, 132);
            this.layoutToolTexture.Text = "Tool texture";
            this.layoutToolTexture.TextSize = new System.Drawing.Size(82, 13);
            // 
            // UnlitGenericTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl);
            this.Name = "UnlitGenericTab";
            this.Size = new System.Drawing.Size(276, 588);
            this.Load += new System.EventHandler(this.UnlitGenericTab_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.switchNoFog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBaseTexture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboSurfaceProp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.basicsGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBaseTexture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSurfaceProp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adjustmentGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutNoFog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureToolTexture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutToolTexture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.PictureEdit pictureBaseTexture;
        private DevExpress.XtraLayout.LayoutControlItem layoutBaseTexture;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup basicsGroup;
        private DevExpress.XtraEditors.ComboBoxEdit comboSurfaceProp;
        private DevExpress.XtraLayout.LayoutControlItem layoutSurfaceProp;
        private DevExpress.XtraEditors.ColorPickEdit editColor;
        private DevExpress.XtraLayout.LayoutControlGroup adjustmentGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutColor;
        private DevExpress.XtraEditors.ToggleSwitch switchNoFog;
        private DevExpress.XtraLayout.LayoutControlItem layoutNoFog;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PictureEdit pictureToolTexture;
        private DevExpress.XtraLayout.LayoutControlItem layoutToolTexture;
    }
}
