using DevExpress.XtraEditors;
using SourceSDK;
using SourceSDK.Materials;
using SourceSDK.Packages;
using SourceSDK.Scripts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static source_modding_tool.MaterialEditor;

namespace source_modding_tool.Materials.ShaderTabs
{
    public partial class LightmappedGenericTab : DevExpress.XtraEditors.XtraUserControl, ShaderInterface
    {
        private Control popupCallerControl;

        public Dictionary<string, PictureEdit> PictureEdits { get; set; }
        public Dictionary<string, Texture> Textures { get; set; }

        public string VMT
        {
            get
            {
                return KeyValue.writeChunk(GetVMT(), true);
            }
        }

        public string Shader => "LightmappedGeneric";

        public string RelativePath { get; set; } = "";

        public Launcher Launcher { get; set; }

        public PackageManager PackageManager { get; set; }

        public event EventHandler OnUpdated;

        public LightmappedGenericTab()
        {
            InitializeComponent();

        }

        private void UnlitGenericTab_Load(object sender, EventArgs e)
        {

            PopulatePictureEdits();
            MaterialEditor.ClearMaterial(this);

            if (RelativePath != "")
                LoadMaterial(PackageManager.GetFile(RelativePath + ".vmt"));
        }

        public void PopulatePictureEdits()
        {
            PictureEdits = new Dictionary<string, PictureEdit>();
            PictureEdits.Add("basetexture", pictureBaseTexture);
            PictureEdits.Add("tooltexture", pictureToolTexture);
            PictureEdits.Add("bumpmap", pictureBumpMap);

            PackageManager packageManager = new PackageManager(Launcher, "scripts");
            string[] surfaceProps = SurfaceProperty.GetStringArray(packageManager);
            foreach(string surfaceProp in surfaceProps) {
                comboSurfaceProp.Properties.Items.Add(surfaceProp);
            }
        }

        public void LoadMaterial(PackageFile file)
        {
            SourceSDK.KeyValue vmt = SourceSDK.KeyValue.ReadChunk(System.Text.Encoding.UTF8.GetString(file.Data));
            this.RelativePath = file.Path + "/" + file.Filename;

            // Get the textures.
            foreach (KeyValuePair<string, PictureEdit> kv in PictureEdits)
            {
                if (vmt != null)
                {
                    string key = "$" + kv.Key;
                    if (kv.Key == "tooltexture")
                        key = "%" + kv.Key;

                    string value = vmt.getValue(key);
                    if (value != null && value != "")
                    {
                        
                        PackageFile textureFile = PackageManager.GetFile("materials/" + value + ".vtf");

                        if (textureFile != null)
                        {
                            Textures[kv.Key].relativePath = textureFile.Path + "/" + textureFile.Filename + ".vtf";
                            Textures[kv.Key].bytes = textureFile.Data;
                            Textures[kv.Key].bitmap = VTF.ToBitmap(Textures[kv.Key].bytes, Launcher);
                            kv.Value.Image = Textures[kv.Key].bitmap;
                        }
                        else
                        {
                            MaterialEditor.ClearTexture(kv.Key, this);
                        }
                    }
                }
                else
                {
                    MaterialEditor.ClearTexture(kv.Key, this);
                }
            }

            /** Basics **/

            // Surfaceprop
            string surfaceProp = vmt.getValue("$surfaceprop");
            if (surfaceProp != "")
                comboSurfaceProp.EditValue = surfaceProp;

            /** Adjustment **/

            // Color
            string color = vmt.getValue("$color");
            if (color != "")
            {
                if (color.StartsWith("{"))
                {
                    color = color.Substring(1, color.Length - 2);
                    string[] rgbs = color.Split(' ');
                    editColor.Color = Color.FromArgb(255, int.Parse(rgbs[0]), int.Parse(rgbs[1]), int.Parse(rgbs[2]));
                } else if(color.StartsWith("["))
                {
                    color = color.Substring(1, color.Length - 2);
                    string[] rgbs = color.Split(' ');
                    editColor.Color = Color.FromArgb(255, (int)(float.Parse(rgbs[0]) * 255), (int)(float.Parse(rgbs[1]) * 255), (int)(float.Parse(rgbs[2]) * 255));
                }
            }

            /** Transparency **/

            // Alphatest
            string alphatest = vmt.getValue("$alphatest");
            if (alphatest != "")
            {
                switchAlphaTest.IsOn = (alphatest == "1");
            }

            string nocull = vmt.getValue("$nocull");
            if (alphatest != "")
            {
                switchNoCull.IsOn = (nocull == "1");
            }

            /** Lighting **/

            // Self Illum
            string selfillum = vmt.getValue("$selfillum");
            if (selfillum != "")
            {
                switchSelfIllum.IsOn = (selfillum == "1");
            }

            // Effect
            string nofog = vmt.getValue("$nofog");
            if (nofog != "")
            {
                switchNoFog.IsOn = (nofog != "1");
            }
        }

        public KeyValue GetVMT()
        {
            // Create the root with the shader as key.
            SourceSDK.KeyValue vmt = new SourceSDK.KeyValue(Shader);

            // Remove 'materials/' from the beginning of the path.
            string materialRelativePath = RelativePath;
            if (RelativePath.StartsWith("materials/"))
                materialRelativePath = RelativePath.Substring("materials/".Length);

            // Check if its a model
            bool isModel = RelativePath.StartsWith("materials/models/");

            // Add the textures.
            if (Textures != null)
            {
                foreach (KeyValuePair<string, Texture> texture in Textures)
                {
                    if (texture.Value.bitmap != null)
                    {
                        string relativePath = materialRelativePath + "_" + texture.Key;
                        if (Textures[texture.Key].relativePath != "")
                        {
                            relativePath = Textures[texture.Key].relativePath.Substring("materials/".Length);
                            relativePath = relativePath.Substring(0, relativePath.IndexOf(".vtf"));
                        }

                        switch (texture.Key)
                        {
                            case "tooltexture":
                                vmt.addChild(new KeyValue("%" + texture.Key, relativePath));
                                break;
                            default:
                                vmt.addChild(new KeyValue("$" + texture.Key, relativePath));
                                break;
                        }

                    }
                }
            }

            /** Basics **/

            // Surface prop
            if (comboSurfaceProp.EditValue.ToString() != "default")
                vmt.addChild(new KeyValue("$surfaceprop", comboSurfaceProp.EditValue.ToString()));

            // Model
            if (isModel)
                vmt.addChild(new KeyValue("$model", "1"));

            /** Adjustment **/

            // Color
            var color = editColor.Color;
            if (color.R != 255 || color.G != 255 || color.B != 255 || color.A != 255)
            {
                vmt.addChild("$color", "{" + color.R + " " + color.G + " " + color.B + "}");
            }

            /** Transparency **/

            // Alphatest
            if (switchAlphaTest.IsOn)
            {
                vmt.addChild("$alphatest", "1");
                vmt.addChild("$allowAlphaToCoverage", "1"); // Antialising for transparency.
            }

            // NoCull
            if (switchNoCull.IsOn)
            {
                vmt.addChild("$nocull", "1");
            }

            /** Lighting **/

            // SelfIllum
            if (switchSelfIllum.IsOn)
            {
                vmt.addChild("$selfillum", "1");
            }

            /** Effect **/

            // Fog
            if (!switchNoFog.IsOn)
                vmt.addChild("$nofog", "1");

            return vmt;
        }

        private void pictureEditPopup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            object control = popupCallerControl;

            if (e.Item == pictureEditMenuImport)
            {
                MaterialEditor.ImportTexture(((PictureEdit)control).Tag.ToString(), this);
                MaterialEditor.CreateToolTexture(this);
                OnUpdated.Invoke(this, EventArgs.Empty);
            }
            else if (e.Item == pictureEditMenuOpen)
            {
                MaterialEditor.OpenTexture(((PictureEdit)control).Tag.ToString(), this);
                MaterialEditor.CreateToolTexture(this);
                OnUpdated.Invoke(this, EventArgs.Empty);
            }
            else if(e.Item == pictureEditMenuClear)
            {
                MaterialEditor.ClearTexture(((PictureEdit)control).Tag.ToString(), this);
                MaterialEditor.CreateToolTexture(this);
                OnUpdated.Invoke(this, EventArgs.Empty);
            }
            else if(e.Item == pictureEditMenuExport)
            {
                XtraSaveFileDialog dialog = new XtraSaveFileDialog()
                {
                    Filter = "Portable Network Graphics (*.png)|*.png"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ((PictureEdit)control).Image.Save(dialog.FileName);
                }
            }
        }

        private void barManager_QueryShowPopupMenu(object sender, DevExpress.XtraBars.QueryShowPopupMenuEventArgs e)
        {
            popupCallerControl = e.Control;
        }

        private void editor_EditValueChanged(object sender, EventArgs e)
        {
            if (sender == switchAlphaTest && switchAlphaTest.IsOn)
            {
                switchSelfIllum.IsOn = false;
            }
            else if(sender == switchSelfIllum && switchSelfIllum.IsOn)
            {
                switchAlphaTest.IsOn = false;
            }
            OnUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
