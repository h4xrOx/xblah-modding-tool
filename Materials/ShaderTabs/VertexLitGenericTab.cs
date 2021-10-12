using DevExpress.XtraEditors;
using SourceSDK;
using SourceSDK.Packages;
using SourceSDK.Materials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static source_modding_tool.MaterialEditor;

namespace source_modding_tool.Materials.ShaderTabs
{
    public partial class VertexLitGenericTab : DevExpress.XtraEditors.XtraUserControl, ShaderInterface
    { 
        public Launcher Launcher { get; set; }
        public PackageManager PackageManager { get; set; }
        public Dictionary<string, PictureEdit> PictureEdits { get; set; }
        public Dictionary<string, MaterialEditor.Texture> Textures { get; set; }

        public string VMT
        {
            get
            {
                return KeyValue.writeChunk(GetVMT(), true);
            }
        }
        
        public string Shader => "VertexLitGeneric";

        public string RelativePath { get; set; } = "";

        public event EventHandler OnUpdated;

        public VertexLitGenericTab()
        {
            InitializeComponent();

            PopulatePictureEdits();
            MaterialEditor.ClearMaterial(this);
        }

        public void PopulatePictureEdits()
        {
            PictureEdits = new Dictionary<string, PictureEdit>();
            PictureEdits.Add("basetexture", pictureBaseTexture);
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

                    vmt.addChild(new KeyValue("$" + texture.Key, relativePath));
                }
            }

            return vmt;
        }

        private void pictureBaseTexture_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MaterialEditor.ImportTexture(((PictureEdit)sender).Tag.ToString(), this);
            }
        }
    }
}
