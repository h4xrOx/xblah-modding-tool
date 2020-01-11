using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using SourceModdingTool.SourceSDK;
using System.Diagnostics;
using DevExpress.XtraBars;

namespace SourceModdingTool
{
    public partial class MaterialEditor : DevExpress.XtraEditors.XtraForm
    {
        Steam sourceSDK;

        Dictionary<string, Texture> textures;
        string[] detail = null;


        public class Texture
        {
            public string relativePath = "";
            public byte[] bytes = null;
            public Bitmap bitmap = null;
        }

        public MaterialEditor(string relativePath, Steam sourceSDK)
        {
            this.sourceSDK = sourceSDK;
            InitializeComponent();
            newMaterial();

            textPath.EditValue = relativePath;
        }

        private void newMaterial()
        {
            textures = new Dictionary<string, Texture>();

            textures.Add("basetexture", new Texture());
            textures.Add("basetexture2", new Texture());
            textures.Add("bumpmap", new Texture());
            textures.Add("envmapmask", new Texture());
            textures.Add("blendmodulatetexture", new Texture());
            textures.Add("tooltexture", new Texture());

            textPath.EditValue = "concrete/new_material";

            comboSurfaceProp.EditValue = "";
            comboSurfaceProp2.EditValue = "";
            comboDetail.EditValue = "";

            pictureToolTexture.Image = null;
            pictureBaseTexture.Image = null;
            pictureBaseTexture2.Image = null;
            pictureBumpMap.Image = null;
            pictureEnvMapMask.Image = null;
            pictureBlendModulateTexture.Image = null;

            shaderCombo.EditValue = "LightmappedGeneric";
        }

        private void createToolTexture()
        {
            Bitmap basetexture = textures["basetexture"].bitmap;
            Bitmap basetexture2 = textures["basetexture2"].bitmap;
            

            if (basetexture == null)
            {
                textures["tooltexture"].bitmap = null;
                textures["tooltexture"].bytes = null;
                textures["tooltexture"].relativePath = "";
            }

            if (basetexture != null && basetexture2 == null)
            {
                textures["tooltexture"].bitmap = basetexture;
                textures["tooltexture"].bytes = textures["basetexture"].bytes;
                textures["tooltexture"].relativePath = "";
            } else if(basetexture != null && basetexture2 != null)
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

                        Color toolColor = Color.FromArgb(
                            (int)(baseColor.R * baseMultiply + baseColor2.R * baseMultiply2),
                            (int)(baseColor.G * baseMultiply + baseColor2.G * baseMultiply2),
                            (int)(baseColor.B * baseMultiply + baseColor2.B * baseMultiply2));
                        tooltexture.SetPixel(i, j, toolColor);
                    }
                }

                textures["tooltexture"].bitmap = tooltexture;
                textures["tooltexture"].bytes = VTF.fromBitmap(tooltexture, sourceSDK);
                textures["tooltexture"].relativePath = "";
            }

            pictureToolTexture.Image = textures["tooltexture"].bitmap;
        }

        private void loadTexture(PictureEdit pictureEdit)
        {
            string tag = pictureEdit.Tag.ToString();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string type = new FileInfo(openFileDialog.FileName).Extension;

                string modPath = sourceSDK.GetModPath();

                Uri path1 = new Uri(modPath + "\\");
                Uri path2 = new Uri(openFileDialog.FileName);
                Uri diff = path1.MakeRelativeUri(path2);


                if (type == ".vtf")
                {
                    textures[tag].relativePath = diff.OriginalString;
                    textures[tag].bytes = File.ReadAllBytes(openFileDialog.FileName);
                    textures[tag].bitmap = VTF.toBitmap(textures[tag].bytes, sourceSDK);
                }
                else
                {
                    textures[tag].relativePath = "";
                    textures[tag].bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName);
                    textures[tag].bytes = VTF.fromBitmap(textures[tag].bitmap, sourceSDK);
                }

                if (textures[tag].bitmap != null)
                    pictureEdit.Image = textures[tag].bitmap;
            }

            createToolTexture();
        }

        private void clearTexture(PictureEdit pictureEdit)
        {
            string tag = pictureEdit.Tag.ToString();

            textures[tag].bitmap = null;
            textures[tag].bytes = null;
            textures[tag].relativePath = "";

            createToolTexture();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SourceSDK.KeyValue vmt = new SourceSDK.KeyValue(shaderCombo.EditValue.ToString());

            string relativePath = textPath.EditValue.ToString();
            string fullPath = (sourceSDK.GetModPath() + "\\materials\\" + relativePath).Replace("/", "\\");

            Directory.CreateDirectory(fullPath.Substring(0, fullPath.LastIndexOf("\\")));
            foreach (KeyValuePair<string, Texture> texture in textures)
            {
                if (texture.Value.bitmap != null)
                {
                    switch(texture.Key)
                    {
                        case "tooltexture":
                            if (texture.Value.bytes == textures["basetexture"].bytes)
                            {
                                vmt.addChild(new KeyValue("$" + texture.Key, relativePath + "_basetexture"));
                                
                            } else
                            {
                                vmt.addChild(new KeyValue("$" + texture.Key, relativePath + "_" + texture.Key));
                                File.WriteAllBytes(fullPath + "_" + texture.Key + ".vtf", texture.Value.bytes);
                            }
                            break;
                        case "envmapmask":
                            vmt.addChild(new KeyValue("envmap", "env_cubemap"));
                            vmt.addChild(new KeyValue("$" + texture.Key, relativePath + "_" + texture.Key));
                            File.WriteAllBytes(fullPath + "_" + texture.Key + ".vtf", texture.Value.bytes);
                            break;
                        default:
                            vmt.addChild(new KeyValue("$" + texture.Key, relativePath + "_" + texture.Key));
                            File.WriteAllBytes(fullPath + "_" + texture.Key + ".vtf", texture.Value.bytes);
                            break;
                    }
                }
            }

            if (detail != null)
            {
                vmt.addChild(new KeyValue("detail", detail[0]));
                vmt.addChild(new KeyValue("detailscale", detail[0]));
                vmt.addChild(new KeyValue("detailblendfactor", detail[0]));
                vmt.addChild(new KeyValue("detailblendmode", detail[0]));
            }

            vmt.addChild(new KeyValue("surfaceprop", comboSurfaceProp.EditValue.ToString()));
            vmt.addChild(new KeyValue("surfaceprop2", comboSurfaceProp2.EditValue.ToString()));

            SourceSDK.KeyValue.writeChunkFile(fullPath + ".vmt", vmt, false);
        }

        private void pictureBaseTexture_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                loadTexture((PictureEdit)sender);
            }
        }

        private void contextLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadTexture((PictureEdit)popupMenuActivator);
        }

        object popupMenuActivator;
        private void popupMenu_Popup(object sender, EventArgs e)
        {
            popupMenuActivator = popupMenu.Activator;
        }

        private void comboDetail_EditValueChanged(object sender, EventArgs e)
        {
            switch(comboDetail.EditValue.ToString())
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
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openFileDialog.FileName;

                string relativePath = fullPath;

                string modPath = sourceSDK.GetModPath();
                string gamePath = sourceSDK.GetGamePath();

                Debugger.Break();
                if (fullPath.Contains(modPath))
                {
                    relativePath = fullPath.Replace(modPath, "").Replace(".vmt", "");
                }
                else if (fullPath.Contains("\\materials\\"))
                {
                    relativePath = fullPath.Substring(fullPath.LastIndexOf("\\materials\\") + "\\materials\\".Length).Replace(".vmt", "");
                }
                else
                {
                    Uri path1 = new Uri(gamePath + "\\");
                    Uri path2 = new Uri(fullPath);
                    Uri diff = path1.MakeRelativeUri(path2);

                    relativePath = diff.OriginalString.Replace(".vmt", "");
                }

                SourceSDK.KeyValue vmt = SourceSDK.KeyValue.readChunkfile(fullPath);

                textPath.EditValue = relativePath;
            }
        }
    }
}