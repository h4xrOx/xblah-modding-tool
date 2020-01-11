namespace SourceModdingTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialEditor));
            this.pictureToolTexture = new DevExpress.XtraEditors.PictureEdit();
            this.pictureBaseTexture = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBaseTexture2 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureBumpMap = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEnvMapMask = new DevExpress.XtraEditors.PictureEdit();
            this.pictureBlendModulateTexture = new DevExpress.XtraEditors.PictureEdit();
            this.openFileDialog = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.shaderCombo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.comboDetail = new DevExpress.XtraEditors.ComboBoxEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.contextLoad = new DevExpress.XtraBars.BarButtonItem();
            this.contextClear = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonNew = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonOpen = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonSave = new DevExpress.XtraBars.BarButtonItem();
            this.textPath = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.comboSurfaceProp2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboSurfaceProp = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureToolTexture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBaseTexture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBaseTexture2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBumpMap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEnvMapMask.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBlendModulateTexture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shaderCombo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboDetail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboSurfaceProp2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboSurfaceProp.Properties)).BeginInit();
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
            // 
            // pictureBaseTexture
            // 
            this.pictureBaseTexture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBaseTexture.Location = new System.Drawing.Point(142, 6);
            this.pictureBaseTexture.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBaseTexture.Name = "pictureBaseTexture";
            this.barManager1.SetPopupContextMenu(this.pictureBaseTexture, this.popupMenu);
            this.pictureBaseTexture.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureBaseTexture.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureBaseTexture.Properties.Tag = "basetexture";
            this.pictureBaseTexture.Size = new System.Drawing.Size(128, 128);
            this.pictureBaseTexture.TabIndex = 1;
            this.pictureBaseTexture.Tag = "basetexture";
            this.pictureBaseTexture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBaseTexture_MouseClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(142, 141);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Base texture";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(686, 140);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(112, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Blend modulate texture";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(414, 141);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(46, 13);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Bumpmap";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(550, 141);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(65, 13);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "Envmap Mask";
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
            // pictureBaseTexture2
            // 
            this.pictureBaseTexture2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBaseTexture2.Location = new System.Drawing.Point(278, 6);
            this.pictureBaseTexture2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBaseTexture2.Name = "pictureBaseTexture2";
            this.barManager1.SetPopupContextMenu(this.pictureBaseTexture2, this.popupMenu);
            this.pictureBaseTexture2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureBaseTexture2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureBaseTexture2.Properties.Tag = "basetexture2";
            this.pictureBaseTexture2.Size = new System.Drawing.Size(128, 128);
            this.pictureBaseTexture2.TabIndex = 10;
            this.pictureBaseTexture2.Tag = "basetexture2";
            this.pictureBaseTexture2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBaseTexture_MouseClick);
            // 
            // pictureBumpMap
            // 
            this.pictureBumpMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBumpMap.Location = new System.Drawing.Point(414, 6);
            this.pictureBumpMap.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBumpMap.Name = "pictureBumpMap";
            this.barManager1.SetPopupContextMenu(this.pictureBumpMap, this.popupMenu);
            this.pictureBumpMap.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureBumpMap.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureBumpMap.Properties.Tag = "bumpmap";
            this.pictureBumpMap.Size = new System.Drawing.Size(128, 128);
            this.pictureBumpMap.TabIndex = 11;
            this.pictureBumpMap.Tag = "bumpmap";
            this.pictureBumpMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBaseTexture_MouseClick);
            // 
            // pictureEnvMapMask
            // 
            this.pictureEnvMapMask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureEnvMapMask.Location = new System.Drawing.Point(550, 6);
            this.pictureEnvMapMask.Margin = new System.Windows.Forms.Padding(4);
            this.pictureEnvMapMask.Name = "pictureEnvMapMask";
            this.barManager1.SetPopupContextMenu(this.pictureEnvMapMask, this.popupMenu);
            this.pictureEnvMapMask.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEnvMapMask.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEnvMapMask.Properties.Tag = "parallaxmap";
            this.pictureEnvMapMask.Size = new System.Drawing.Size(128, 128);
            this.pictureEnvMapMask.TabIndex = 12;
            this.pictureEnvMapMask.Tag = "envmapmask";
            this.pictureEnvMapMask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBaseTexture_MouseClick);
            // 
            // pictureBlendModulateTexture
            // 
            this.pictureBlendModulateTexture.Location = new System.Drawing.Point(686, 6);
            this.pictureBlendModulateTexture.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBlendModulateTexture.Name = "pictureBlendModulateTexture";
            this.barManager1.SetPopupContextMenu(this.pictureBlendModulateTexture, this.popupMenu);
            this.pictureBlendModulateTexture.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureBlendModulateTexture.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureBlendModulateTexture.Properties.Tag = "blendmodulatetexture";
            this.pictureBlendModulateTexture.Size = new System.Drawing.Size(128, 128);
            this.pictureBlendModulateTexture.TabIndex = 13;
            this.pictureBlendModulateTexture.Tag = "blendmodulatetexture";
            this.pictureBlendModulateTexture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBaseTexture_MouseClick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Valve Material Type (*.vmt)|*.vmt";
            // 
            // shaderCombo
            // 
            this.shaderCombo.EditValue = "LightmappedGeneric";
            this.shaderCombo.Location = new System.Drawing.Point(52, 31);
            this.shaderCombo.Name = "shaderCombo";
            this.shaderCombo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.shaderCombo.Properties.Items.AddRange(new object[] {
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
            this.shaderCombo.Properties.Sorted = true;
            this.shaderCombo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.shaderCombo.Size = new System.Drawing.Size(229, 20);
            this.shaderCombo.TabIndex = 16;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 34);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(34, 13);
            this.labelControl6.TabIndex = 17;
            this.labelControl6.Text = "Shader";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.comboDetail);
            this.panelControl2.Controls.Add(this.comboSurfaceProp2);
            this.panelControl2.Controls.Add(this.comboSurfaceProp);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.pictureBumpMap);
            this.panelControl2.Controls.Add(this.pictureBlendModulateTexture);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.pictureEnvMapMask);
            this.panelControl2.Controls.Add(this.pictureToolTexture);
            this.panelControl2.Controls.Add(this.pictureBaseTexture);
            this.panelControl2.Controls.Add(this.labelControl8);
            this.panelControl2.Controls.Add(this.labelControl7);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.pictureBaseTexture2);
            this.panelControl2.Location = new System.Drawing.Point(11, 56);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(8);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(821, 197);
            this.panelControl2.TabIndex = 22;
            // 
            // comboDetail
            // 
            this.comboDetail.Location = new System.Drawing.Point(414, 160);
            this.comboDetail.MenuManager = this.barManager1;
            this.comboDetail.Name = "comboDetail";
            this.comboDetail.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboDetail.Properties.DropDownRows = 15;
            this.comboDetail.Properties.Items.AddRange(new object[] {
            "",
            "metal",
            "noise",
            "plaster",
            "rock",
            "wood"});
            this.comboDetail.Properties.Sorted = true;
            this.comboDetail.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboDetail.Size = new System.Drawing.Size(128, 20);
            this.comboDetail.TabIndex = 16;
            this.comboDetail.Tag = "detail";
            this.comboDetail.EditValueChanged += new System.EventHandler(this.comboDetail_EditValueChanged);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonNew,
            this.barButtonOpen,
            this.barButtonSave,
            this.textPath,
            this.contextLoad,
            this.contextClear});
            this.barManager1.MaxItemId = 7;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.barManager1.StatusBar = this.bar3;
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.contextLoad),
            new DevExpress.XtraBars.LinkPersistInfo(this.contextClear)});
            this.popupMenu.Manager = this.barManager1;
            this.popupMenu.Name = "popupMenu";
            this.popupMenu.Popup += new System.EventHandler(this.popupMenu_Popup);
            // 
            // contextLoad
            // 
            this.contextLoad.Caption = "Load";
            this.contextLoad.Id = 5;
            this.contextLoad.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("contextLoad.ImageOptions.SvgImage")));
            this.contextLoad.Name = "contextLoad";
            this.contextLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.contextLoad_ItemClick);
            // 
            // contextClear
            // 
            this.contextClear.Caption = "Clear";
            this.contextClear.Id = 6;
            this.contextClear.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("contextClear.ImageOptions.SvgImage")));
            this.contextClear.Name = "contextClear";
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.textPath)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.Text = "Tools";
            // 
            // barButtonNew
            // 
            this.barButtonNew.Caption = "New";
            this.barButtonNew.Id = 0;
            this.barButtonNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonNew.Name = "barButtonNew";
            // 
            // barButtonOpen
            // 
            this.barButtonOpen.Caption = "Open";
            this.barButtonOpen.Id = 1;
            this.barButtonOpen.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem2.ImageOptions.SvgImage")));
            this.barButtonOpen.Name = "barButtonOpen";
            this.barButtonOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonSave
            // 
            this.barButtonSave.Caption = "Save";
            this.barButtonSave.Id = 2;
            this.barButtonSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem3.ImageOptions.SvgImage")));
            this.barButtonSave.Name = "barButtonSave";
            this.barButtonSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // textPath
            // 
            this.textPath.AutoFillWidth = true;
            this.textPath.Caption = "textPath";
            this.textPath.Edit = this.repositoryItemTextEdit1;
            this.textPath.Id = 4;
            this.textPath.Name = "textPath";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(843, 28);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 264);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(843, 18);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 28);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 236);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(843, 28);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 236);
            // 
            // comboSurfaceProp2
            // 
            this.comboSurfaceProp2.Location = new System.Drawing.Point(278, 160);
            this.comboSurfaceProp2.MenuManager = this.barManager1;
            this.comboSurfaceProp2.Name = "comboSurfaceProp2";
            this.comboSurfaceProp2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboSurfaceProp2.Properties.DropDownRows = 15;
            this.comboSurfaceProp2.Properties.Items.AddRange(new object[] {
            "alienflesh",
            "antlion",
            "antlionsand",
            "armorflesh",
            "bloodyflesh",
            "brakingrubbertire",
            "brick",
            "canister",
            "cardboard",
            "carpet",
            "ceiling_tile",
            "chain",
            "chainlink",
            "combine_glass",
            "combine_metal",
            "computer",
            "concrete",
            "concrete_block",
            "crowbar",
            "default",
            "default_silent",
            "dirt",
            "flesh",
            "floating_metal_barrel",
            "floatingstandable",
            "foliage",
            "glass",
            "glassbottle",
            "grass",
            "gravel",
            "gravel",
            "grenade",
            "gunship",
            "ice",
            "item",
            "jeeptire",
            "ladder",
            "metal",
            "metal_barrel",
            "metal_bouncy",
            "Metal_Box",
            "metal_seafloorcar",
            "metalgrate",
            "metalpanel",
            "metalvehicle",
            "metalvent",
            "mud",
            "no_decal",
            "paintcan",
            "paper",
            "papercup",
            "plaster",
            "plastic",
            "plastic_barrel",
            "plastic_barrel_buoyant",
            "Plastic_Box",
            "player",
            "player_control_clip",
            "popcan",
            "pottery",
            "quicksand",
            "rock",
            "roller",
            "rubber",
            "rubbertire",
            "sand",
            "slidingrubbertire",
            "slidingrubbertire_front",
            "slidingrubbertire_rear",
            "slime",
            "slipperymetal",
            "slipperyslime",
            "snow",
            "solidmetal",
            "strider",
            "tile",
            "wade",
            "water",
            "watermelon",
            "weapon",
            "Wood",
            "Wood_Box",
            "Wood_Furniture",
            "Wood_Panel",
            "Wood_Plank",
            "Wood_Solid",
            "zombieflesh"});
            this.comboSurfaceProp2.Properties.Sorted = true;
            this.comboSurfaceProp2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboSurfaceProp2.Size = new System.Drawing.Size(128, 20);
            this.comboSurfaceProp2.TabIndex = 15;
            this.comboSurfaceProp2.Tag = "surfaceprop2";
            // 
            // comboSurfaceProp
            // 
            this.comboSurfaceProp.Location = new System.Drawing.Point(142, 160);
            this.comboSurfaceProp.MenuManager = this.barManager1;
            this.comboSurfaceProp.Name = "comboSurfaceProp";
            this.comboSurfaceProp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboSurfaceProp.Properties.DropDownRows = 15;
            this.comboSurfaceProp.Properties.Items.AddRange(new object[] {
            "alienflesh",
            "antlion",
            "antlionsand",
            "armorflesh",
            "bloodyflesh",
            "brakingrubbertire",
            "brick",
            "canister",
            "cardboard",
            "carpet",
            "ceiling_tile",
            "chain",
            "chainlink",
            "combine_glass",
            "combine_metal",
            "computer",
            "concrete",
            "concrete_block",
            "crowbar",
            "default",
            "default_silent",
            "dirt",
            "flesh",
            "floating_metal_barrel",
            "floatingstandable",
            "foliage",
            "glass",
            "glassbottle",
            "grass",
            "gravel",
            "gravel",
            "grenade",
            "gunship",
            "ice",
            "item",
            "jeeptire",
            "ladder",
            "metal",
            "metal_barrel",
            "metal_bouncy",
            "Metal_Box",
            "metal_seafloorcar",
            "metalgrate",
            "metalpanel",
            "metalvehicle",
            "metalvent",
            "mud",
            "no_decal",
            "paintcan",
            "paper",
            "papercup",
            "plaster",
            "plastic",
            "plastic_barrel",
            "plastic_barrel_buoyant",
            "Plastic_Box",
            "player",
            "player_control_clip",
            "popcan",
            "pottery",
            "quicksand",
            "rock",
            "roller",
            "rubber",
            "rubbertire",
            "sand",
            "slidingrubbertire",
            "slidingrubbertire_front",
            "slidingrubbertire_rear",
            "slime",
            "slipperymetal",
            "slipperyslime",
            "snow",
            "solidmetal",
            "strider",
            "tile",
            "wade",
            "water",
            "watermelon",
            "weapon",
            "Wood",
            "Wood_Box",
            "Wood_Furniture",
            "Wood_Panel",
            "Wood_Plank",
            "Wood_Solid",
            "zombieflesh"});
            this.comboSurfaceProp.Properties.Sorted = true;
            this.comboSurfaceProp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboSurfaceProp.Size = new System.Drawing.Size(128, 20);
            this.comboSurfaceProp.TabIndex = 14;
            this.comboSurfaceProp.Tag = "surfaceprop";
            // 
            // MaterialEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 282);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.shaderCombo);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MaterialEditor";
            this.Text = "Material Editor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureToolTexture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBaseTexture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBaseTexture2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBumpMap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEnvMapMask.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBlendModulateTexture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shaderCombo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboDetail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboSurfaceProp2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboSurfaceProp.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureToolTexture;
        private DevExpress.XtraEditors.PictureEdit pictureBaseTexture;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PictureEdit pictureBaseTexture2;
        private DevExpress.XtraEditors.PictureEdit pictureBumpMap;
        private DevExpress.XtraEditors.PictureEdit pictureEnvMapMask;
        private DevExpress.XtraEditors.PictureEdit pictureBlendModulateTexture;
        private DevExpress.XtraEditors.XtraOpenFileDialog openFileDialog;
        private DevExpress.XtraEditors.ComboBoxEdit shaderCombo;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonNew;
        private DevExpress.XtraBars.BarButtonItem barButtonOpen;
        private DevExpress.XtraBars.BarButtonItem barButtonSave;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem textPath;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem contextLoad;
        private DevExpress.XtraBars.BarButtonItem contextClear;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraEditors.ComboBoxEdit comboSurfaceProp;
        private DevExpress.XtraEditors.ComboBoxEdit comboDetail;
        private DevExpress.XtraEditors.ComboBoxEdit comboSurfaceProp2;
    }
}