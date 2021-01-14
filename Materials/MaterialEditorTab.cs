using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using static source_modding_tool.MaterialEditor;
using System.IO;
 
using DevExpress.XtraBars;
using SourceSDK.Materials;
using SourceSDK;
using SourceSDK.Packages;
using System.Diagnostics;
using SourceSDK.Packages.VPKPackage;

namespace source_modding_tool
{
    public partial class MaterialEditorTab : DevExpress.XtraEditors.XtraUserControl
    {
        public Dictionary<string, PictureEdit> pictureEdits;
        Dictionary<string, Texture> textures;
        string[] detail = null;

        public Launcher launcher;
        private PackageManager packageManager;
        object popupMenuActivator;

        public string relativePath = "";

        int textureWidth = 512;
        int textureHeight = 512;

        [Browsable(true)]
        public event EventHandler OnUpdated;

        public string VMT
        {
            get
            {
                return KeyValue.writeChunk(GetVMT(relativePath), true);
            }
        }

        public string shader
        {
            get
            {
                return shaderCombo.EditValue.ToString();
            }
            set
            {
                shaderCombo.EditValue = value;
            }
        }

        public MaterialEditorTab(Launcher launcher, PackageManager packageManager)
        {
            this.launcher = launcher;
            this.packageManager = packageManager;
            InitializeComponent();
            populatePictureEdits();
            ClearMaterial();

            UpdatePreview();
        }

        private void populatePictureEdits()
        {
            pictureEdits = new Dictionary<string, PictureEdit>();
            pictureEdits.Add("tooltexture", pictureToolTexture);
            pictureEdits.Add("basetexture", pictureBaseTexture);
            pictureEdits.Add("basetexture2", pictureBaseTexture2);
            pictureEdits.Add("bumpmap", pictureBumpMap);
            pictureEdits.Add("envmapmask", pictureEnvMapMask);
            pictureEdits.Add("blendmodulatetexture", pictureBlendModulateTexture);
        }

        public void ClearMaterial()
        {
            textures = new Dictionary<string, Texture>();

            foreach (KeyValuePair<string, PictureEdit> kv in pictureEdits)
            {
                textures.Add(kv.Key, new Texture());
                kv.Value.Image = null;
            }

            //textEdit1.EditValue = "concrete/new_material";

            comboSurfaceProp.EditValue = string.Empty;
            comboSurfaceProp2.EditValue = string.Empty;
            comboDetail.EditValue = string.Empty;

            shaderCombo.EditValue = "LightmappedGeneric";
        }

        private void ClearTexture(PictureEdit pictureEdit)
        {
            string tag = pictureEdit.Tag.ToString();
            pictureEdit.Image = null;
            textures[tag].bitmap = null;
            textures[tag].bytes = null;
            textures[tag].relativePath = string.Empty;

            CreateToolTexture();
        }

        public void comboDetail_EditValueChanged(object sender, EventArgs e)
        {
            switch (comboDetail.EditValue.ToString())
            {
                case "noise":
                    detail = new string[4] { "detail\\noise_detail_01", "7.74", "0.8", "0" };
                    break;
                case "metal":
                    detail = new string[4] { "detail\\metal_detail_01", "4.283", ".65", "0" };
                    break;
                case "rock":
                    detail = new string[4] { "detail\\rock_detail_01", "11", "1", "0" };
                    break;
                case "plaster":
                    detail = new string[4] { "detail\\plaster_detail_01", "6.783", ".8", "0" };
                    break;
                case "wood":
                    detail = new string[4] { "detail\\wood_detail_01", "2.583", ".8", "0" };
                    break;
                default:
                    detail = null;
                    break;
            }

            UpdatePreview();
        }

        private void contextClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            PictureEdit pictureEdit = (PictureEdit)popupMenuActivator;
            ClearTexture(pictureEdit);
        }

        private void contextLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { LoadTexture((PictureEdit)popupMenuActivator); }

        private void CreateToolTexture()
        {
            Bitmap basetexture = textures["basetexture"].bitmap;
            Bitmap basetexture2 = textures["basetexture2"].bitmap;

            if (basetexture == null)
            {
                textures["tooltexture"].bitmap = null;
                textures["tooltexture"].bytes = null;
                textures["tooltexture"].relativePath = string.Empty;
            }

            if (basetexture != null && basetexture2 == null)
            {
                textures["tooltexture"].bitmap = basetexture;
                textures["tooltexture"].bytes = textures["basetexture"].bytes;
                textures["tooltexture"].relativePath = string.Empty;
            }
            else if (basetexture != null && basetexture2 != null)
            {
                Bitmap tooltexture = null;

                // Resize images
                int width = basetexture.Width;
                int height = basetexture.Height;

                // Merge images
                tooltexture = new Bitmap(width, height);

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Color baseColor = basetexture.GetPixel(i, j);
                        Color baseColor2 = basetexture2.GetPixel(i, j);

                        float baseMultiply = Math.Min(Math.Max(2.5f - (float)(i + j) / (width + height) * 4, 0), 1);
                        float baseMultiply2 = 1 - baseMultiply;

                        Color toolColor = Color.FromArgb((int)(baseColor.R * baseMultiply + baseColor2.R * baseMultiply2),
                                                         (int)(baseColor.G * baseMultiply + baseColor2.G * baseMultiply2),
                                                         (int)(baseColor.B * baseMultiply + baseColor2.B * baseMultiply2));
                        tooltexture.SetPixel(i, j, toolColor);
                    }
                }

                textures["tooltexture"].bitmap = tooltexture;
                textures["tooltexture"].bytes = VTF.FromBitmap(tooltexture, launcher);
                textures["tooltexture"].relativePath = string.Empty;
            }

            pictureToolTexture.Image = textures["tooltexture"].bitmap;
        }

        public void LoadMaterial(PackageFile file)
        {
            string data = System.Text.Encoding.UTF8.GetString(file.Data);

            string fullPath = "";

            SourceSDK.KeyValue vmt = SourceSDK.KeyValue.ReadChunk(data);

            string relativePath = file.Path + "/" + file.Filename;

            this.relativePath = relativePath.Substring("materials/".Length);

            foreach (KeyValuePair<string, PictureEdit> kv in pictureEdits)
            {
                if (vmt != null)
                {
                    string value = vmt.getChildren()[0].getValue("$" + kv.Key);

                    if (value != null && value != "")
                    {
                        PackageFile textureFile = packageManager.GetFile("materials/" + value + ".vtf");

                        if (textureFile != null)
                        {


                            textures[kv.Key].relativePath = textureFile.Path + "/" + textureFile.Filename + ".vtf";
                            textures[kv.Key].bytes = textureFile.Data;
                            textures[kv.Key].bitmap = VTF.ToBitmap(textures[kv.Key].bytes, launcher);
                            kv.Value.Image = textures[kv.Key].bitmap;
                        } else
                        {
                            ClearTexture(kv.Value);
                        }

                    }
                }
                else
                {
                    ClearTexture(kv.Value);
                }
            }

            if (vmt != null && vmt.getValue("$normalmapalphaenvmapmask") == "1" && textures["bumpmap"].bitmap != null)
            {
                textures["envmapmask"].bitmap = new Bitmap(textures["bumpmap"].bitmap.Width, textures["bumpmap"].bitmap.Height);
                for (int i = 0; i < textures["bumpmap"].bitmap.Width; i++)
                {
                    for (int j = 0; j < textures["bumpmap"].bitmap.Height; j++)
                    {
                        int alpha = textures["bumpmap"].bitmap.GetPixel(i, j).A;
                        textures["envmapmask"].bitmap.SetPixel(i, j, Color.FromArgb(alpha, alpha, alpha));
                    }
                }
                textures["envmapmask"].bytes = VTF.FromBitmap(textures["envmapmask"].bitmap, launcher);
                textures["envmapmask"].relativePath = "";
                pictureEnvMapMask.Image = textures["envmapmask"].bitmap;
            }
        }

        private void LoadTexture(PictureEdit pictureEdit)
        {
            string tag = pictureEdit.Tag.ToString();

            int width = textureWidth;
            int height = textureHeight;

            if (openBitmapFileDialog.ShowDialog() == DialogResult.OK)
            {
                string type = new FileInfo(openBitmapFileDialog.FileName).Extension;

                string modPath = launcher.GetCurrentMod().installPath;

                Uri path1 = new Uri(modPath + "\\");
                Uri path2 = new Uri(openBitmapFileDialog.FileName);
                Uri diff = path1.MakeRelativeUri(path2);

                if (type == ".vtf")
                {
                    textures[tag].relativePath = diff.OriginalString;
                    textures[tag].bytes = File.ReadAllBytes(openBitmapFileDialog.FileName);
                    textures[tag].bitmap = VTF.ToBitmap(textures[tag].bytes, launcher);
                }
                else
                {
                    textures[tag].relativePath = string.Empty;
                    Image originalBitmap = Bitmap.FromFile(openBitmapFileDialog.FileName);
                    width = (int)Math.Pow(2, Math.Floor(Math.Log(originalBitmap.Width, 2)));
                    height = (int)Math.Pow(2, Math.Floor(Math.Log(originalBitmap.Height, 2)));
                    textures[tag].bitmap = new Bitmap(originalBitmap, width, height);
                    originalBitmap.Dispose();
                    textures[tag].bytes = VTF.FromBitmap(textures[tag].bitmap, launcher);
                }

                if (textures[tag].bitmap != null)
                    pictureEdit.Image = textures[tag].bitmap;
            }

            CreateToolTexture();
        }

        public void pictureBaseTexture_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LoadTexture((PictureEdit)sender);
            }

            UpdatePreview();
        }

        private void popupMenu_Popup(object sender, EventArgs e) { popupMenuActivator = popupMenu.Activator; }

        public void SaveMaterial(string path)
        {
            SaveMaterial(path, this.shader);
        }

        public string GetRelativePath(string fullPath)
        {
            Uri path1 = new Uri(launcher.GetCurrentMod().installPath + "\\");
            Uri path2 = new Uri(fullPath);
            Uri diff = path1.MakeRelativeUri(path2);
            return diff.OriginalString;
        }

        public void SetRelativePath(string path)
        {
            this.relativePath = path;

            UpdatePreview();
        }

        public void SaveMaterial(string relativePath, string shader)
        {
            SourceSDK.KeyValue vmt = new SourceSDK.KeyValue(shader);

            string fullPath = (launcher.GetCurrentMod().installPath + "\\" + relativePath).Replace(" / ", "\\");

            Directory.CreateDirectory(fullPath.Substring(0, fullPath.LastIndexOf("\\")));

            bool hasNormalMap = false;
            bool hasSpecularMap = false;

            foreach (KeyValuePair<string, Texture> texture in textures)
            {
                if (texture.Value.bitmap != null)
                {
                    switch (texture.Key)
                    {
                        case "tooltexture":
                            if (texture.Value.bytes != textures["basetexture"].bytes)
                                File.WriteAllBytes(fullPath + "_" + texture.Key + ".vtf", texture.Value.bytes);

                            break;
                        case "envmapmask":
                            hasSpecularMap = true;
                            break;
                        case "bumpmap":
                            hasNormalMap = true;
                            break;
                        default:
                            File.WriteAllBytes(fullPath + "_" + texture.Key + ".vtf", texture.Value.bytes);
                            break;
                    }
                }
            }

            if (hasNormalMap && hasSpecularMap)
            {
                Bitmap normalBitmap = textures["bumpmap"].bitmap;
                Bitmap specularBitmap = new Bitmap(textures["envmapmask"].bitmap,
                                                   normalBitmap.Width,
                                                   normalBitmap.Height);

                for (int i = 0; i < normalBitmap.Width; i++)
                {
                    for (int j = 0; j < normalBitmap.Height; j++)
                    {
                        Color normalColor = normalBitmap.GetPixel(i, j);
                        Color specularColor = specularBitmap.GetPixel(i, j);
                        normalBitmap.SetPixel(i,
                                              j,
                                              Color.FromArgb(specularColor.R,
                                                             normalColor.R,
                                                             normalColor.G,
                                                             normalColor.B));
                    }
                }
                textures["bumpmap"].bytes = VTF.FromBitmap(textures["bumpmap"].bitmap, launcher);
                File.WriteAllBytes(fullPath + "_bumpmap.vtf", textures["bumpmap"].bytes);
            }
            else if (hasNormalMap)
                File.WriteAllBytes(fullPath + "_bumpmap.vtf", textures["bumpmap"].bytes);
            
            else if (hasSpecularMap)
                File.WriteAllBytes(fullPath + "_envmapmask.vtf", textures["envmapmask"].bytes);

            File.WriteAllText(fullPath + ".vmt", VMT, new UTF8Encoding(false));
        }

        private KeyValue GetVMT(string relativePath)
        {
            SourceSDK.KeyValue vmt = new SourceSDK.KeyValue(shader);

            bool hasNormalMap = false;
            bool hasSpecularMap = false;

            string materialRelativePath = relativePath;
            if (relativePath.StartsWith("/materials/"))
                materialRelativePath = relativePath.Substring("/materials/".Length);

            foreach (KeyValuePair<string, Texture> texture in textures)
            {
                if (texture.Value.bitmap != null)
                {
                    switch (texture.Key)
                    {
                        case "tooltexture":
                            if (texture.Value.bytes == textures["basetexture"].bytes)
                            {
                                vmt.addChild(new KeyValue("$" + texture.Key, materialRelativePath + "_basetexture"));
                            }
                            else
                            {
                                vmt.addChild(new KeyValue("$" + texture.Key, materialRelativePath + "_" + texture.Key));
                            }
                            break;
                        case "envmapmask":
                            hasSpecularMap = true;
                            break;
                        case "bumpmap":
                            hasNormalMap = true;
                            break;
                        default:
                            vmt.addChild(new KeyValue("$" + texture.Key, materialRelativePath + "_" + texture.Key));
                            break;
                    }
                }
            }

            if (hasNormalMap && hasSpecularMap)
            {
                vmt.addChild(new KeyValue("$bumpmap", materialRelativePath + "_bumpmap"));
                vmt.addChild(new KeyValue("$normalmapalphaenvmapmask", "1"));
                vmt.addChild(new KeyValue("$envmap", "env_cubemap"));
            }
            else if (hasNormalMap)
            {
                vmt.addChild(new KeyValue("$bumpmap", materialRelativePath + "_bumpmap"));
            }
            else if (hasSpecularMap)
            {
                vmt.addChild(new KeyValue("$envmap", "env_cubemap"));
                vmt.addChild(new KeyValue("$envmapmask", materialRelativePath + "_envmapmask"));
            }

            if (detail != null)
            {
                vmt.addChild(new KeyValue("$detail", detail[0]));
                vmt.addChild(new KeyValue("$detailscale", detail[1]));
                vmt.addChild(new KeyValue("$detailblendfactor", detail[2]));
                vmt.addChild(new KeyValue("$detailblendmode", detail[3]));
            }

            vmt.addChild(new KeyValue("$surfaceprop", comboSurfaceProp.EditValue.ToString()));
            vmt.addChild(new KeyValue("$surfaceprop2", comboSurfaceProp2.EditValue.ToString()));

            return vmt;
        }

        private void UpdatePreview()
        {
            if (OnUpdated != null) {
                OnUpdated.Invoke(this, new EventArgs());
            };
        }

        private void pictureBaseTexture_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void EditValueChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }
    }
}
