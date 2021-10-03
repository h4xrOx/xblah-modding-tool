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
    public partial class UnlitGenericTab : DevExpress.XtraEditors.XtraUserControl, ShaderInterface
    {
        public Dictionary<string, PictureEdit> PictureEdits { get; set; }
        public Dictionary<string, Texture> Textures { get; set; }

        public string VMT
        {
            get
            {
                return KeyValue.writeChunk(GetVMT(), true);
            }
        }

        public string Shader => "UnlitGeneric";

        public string RelativePath { get; set; } = "";

        public Launcher Launcher { get; set; }

        public PackageManager PackageManager { get; set; }

        public event EventHandler OnUpdated;

        public UnlitGenericTab()
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

            // Basics
            string surfaceProp = vmt.getValue("$surfaceprop");
            if (surfaceProp != "")
                comboSurfaceProp.EditValue = surfaceProp;

            // Adjustment
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

            // Add the textures.
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

                    switch(texture.Key)
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

            /** Basics **/

            // Surface prop
            vmt.addChild(new KeyValue("$surfaceprop", comboSurfaceProp.EditValue.ToString()));

            // Check if its a model
            if (RelativePath.StartsWith("materials/models/"))
                vmt.addChild(new KeyValue("$model", "1"));

            /** Adjustment **/

            // Color
            var color = editColor.Color;
            if (color != Color.White)
            {
                vmt.addChild("$color", "{" + color.R + " " + color.G + " " + color.B + "}");
            }

            /** Effect **/

            // Fog
            if (!switchNoFog.IsOn)
                vmt.addChild("$nofog", "1");

            return vmt;
        }

        private void pictureBaseTexture_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MaterialEditor.LoadTexture(((PictureEdit)sender).Tag.ToString(), this);
                MaterialEditor.CreateToolTexture(this);
            }
        }
    }
}
