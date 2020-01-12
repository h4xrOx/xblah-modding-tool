using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SourceModdingTool.SourceSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SourceModdingTool
{
    public partial class MaterialEditor : DevExpress.XtraEditors.XtraForm
    {
        string[] detail = null;

        object popupMenuActivator;
        Steam sourceSDK;

        Dictionary<string, Texture> textures;

        public MaterialEditor(string relativePath, Steam sourceSDK)
        {
            this.sourceSDK = sourceSDK;
            InitializeComponent();
            newMaterial();

            textPath.EditValue = relativePath;
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(openVMTFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openVMTFileDialog.FileName;

                string relativePath = fullPath;

                string modPath = sourceSDK.GetModPath();
                string gamePath = sourceSDK.GetGamePath();

                Debugger.Break();
                if(fullPath.Contains(modPath))
                {
                    relativePath = fullPath.Replace(modPath, string.Empty).Replace(".vmt", string.Empty);
                } else if(fullPath.Contains("\\materials\\"))
                {
                    relativePath = fullPath.Substring(fullPath.LastIndexOf("\\materials\\") + "\\materials\\".Length)
                        .Replace(".vmt", string.Empty);
                } else
                {
                    Uri path1 = new Uri(gamePath + "\\");
                    Uri path2 = new Uri(fullPath);
                    Uri diff = path1.MakeRelativeUri(path2);

                    relativePath = diff.OriginalString.Replace(".vmt", string.Empty);
                }

                SourceSDK.KeyValue vmt = SourceSDK.KeyValue.readChunkfile(fullPath);

                textPath.EditValue = relativePath;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SourceSDK.KeyValue vmt = new SourceSDK.KeyValue(shaderCombo.EditValue.ToString());

            string relativePath = textPath.EditValue.ToString();
            string fullPath = (sourceSDK.GetModPath() + "\\materials\\" + relativePath).Replace("/", "\\");

            Directory.CreateDirectory(fullPath.Substring(0, fullPath.LastIndexOf("\\")));
            foreach(KeyValuePair<string, Texture> texture in textures)
            {
                if(texture.Value.bitmap != null)
                {
                    switch(texture.Key)
                    {
                        case "tooltexture":
                            if(texture.Value.bytes == textures["basetexture"].bytes)
                            {
                                vmt.addChild(new KeyValue("$" + texture.Key, relativePath + "_basetexture"));
                            } else
                            {
                                vmt.addChild(new KeyValue("$" + texture.Key, relativePath + "_" + texture.Key));
                                File.WriteAllBytes(fullPath + "_" + texture.Key + ".vtf", texture.Value.bytes);
                            }
                            break;
                        case "envmapmask":
                            vmt.addChild(new KeyValue("$envmap", "env_cubemap"));
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

            if(detail != null)
            {
                vmt.addChild(new KeyValue("$detail", detail[0]));
                vmt.addChild(new KeyValue("$detailscale", detail[0]));
                vmt.addChild(new KeyValue("$detailblendfactor", detail[0]));
                vmt.addChild(new KeyValue("$detailblendmode", detail[0]));
            }

            vmt.addChild(new KeyValue("$surfaceprop", comboSurfaceProp.EditValue.ToString()));
            vmt.addChild(new KeyValue("$surfaceprop2", comboSurfaceProp2.EditValue.ToString()));

            SourceSDK.KeyValue.writeChunkFile(fullPath + ".vmt", vmt, false, new UTF8Encoding(false));
        }

        private void clearTexture(PictureEdit pictureEdit)
        {
            string tag = pictureEdit.Tag.ToString();

            textures[tag].bitmap = null;
            textures[tag].bytes = null;
            textures[tag].relativePath = string.Empty;

            createToolTexture();
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

        private void contextLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { loadTexture((PictureEdit)popupMenuActivator); }

        private void createToolTexture()
        {
            Bitmap basetexture = textures["basetexture"].bitmap;
            Bitmap basetexture2 = textures["basetexture2"].bitmap;


            if(basetexture == null)
            {
                textures["tooltexture"].bitmap = null;
                textures["tooltexture"].bytes = null;
                textures["tooltexture"].relativePath = string.Empty;
            }

            if(basetexture != null && basetexture2 == null)
            {
                textures["tooltexture"].bitmap = basetexture;
                textures["tooltexture"].bytes = textures["basetexture"].bytes;
                textures["tooltexture"].relativePath = string.Empty;
            } else if(basetexture != null && basetexture2 != null)
            {
                Bitmap tooltexture = null;

                // Resize images
                int width = basetexture.Width;
                int height = basetexture.Height;

                // Merge images
                tooltexture = new Bitmap(width, height);

                for(int i = 0; i < width; i++)
                {
                    for(int j = 0; j < height; j++)
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
                textures["tooltexture"].bytes = VTF.fromBitmap(tooltexture, sourceSDK);
                textures["tooltexture"].relativePath = string.Empty;
            }

            pictureToolTexture.Image = textures["tooltexture"].bitmap;
        }

        private void loadTexture(PictureEdit pictureEdit)
        {
            string tag = pictureEdit.Tag.ToString();

            if(openBitmapFileDialog.ShowDialog() == DialogResult.OK)
            {
                string type = new FileInfo(openBitmapFileDialog.FileName).Extension;

                string modPath = sourceSDK.GetModPath();

                Uri path1 = new Uri(modPath + "\\");
                Uri path2 = new Uri(openBitmapFileDialog.FileName);
                Uri diff = path1.MakeRelativeUri(path2);


                if(type == ".vtf")
                {
                    textures[tag].relativePath = diff.OriginalString;
                    textures[tag].bytes = File.ReadAllBytes(openBitmapFileDialog.FileName);
                    textures[tag].bitmap = VTF.toBitmap(textures[tag].bytes, sourceSDK);
                } else
                {
                    textures[tag].relativePath = string.Empty;
                    textures[tag].bitmap = (Bitmap)Bitmap.FromFile(openBitmapFileDialog.FileName);
                    textures[tag].bytes = VTF.fromBitmap(textures[tag].bitmap, sourceSDK);
                }

                if(textures[tag].bitmap != null)
                    pictureEdit.Image = textures[tag].bitmap;
            }

            createToolTexture();
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

            comboSurfaceProp.EditValue = string.Empty;
            comboSurfaceProp2.EditValue = string.Empty;
            comboDetail.EditValue = string.Empty;

            pictureToolTexture.Image = null;
            pictureBaseTexture.Image = null;
            pictureBaseTexture2.Image = null;
            pictureBumpMap.Image = null;
            pictureEnvMapMask.Image = null;
            pictureBlendModulateTexture.Image = null;

            shaderCombo.EditValue = "LightmappedGeneric";
        }

        private void pictureBaseTexture_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                loadTexture((PictureEdit)sender);
            }
        }

        private void popupMenu_Popup(object sender, EventArgs e) { popupMenuActivator = popupMenu.Activator; }


        public class Texture
        {
            public Bitmap bitmap = null;
            public byte[] bytes = null;
            public string relativePath = string.Empty;
        }

        Game game;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string modPath = sourceSDK.GetModPath();
            File.Copy(modPath + "\\gameinfo.txt", modPath + "\\gameinfo.txt.temp");
            File.Copy(modPath + "\\resource\\gamemenu.res", modPath + "\\resource\\gamemenu.res.temp");

            KeyValue gameinfo = KeyValue.readChunkfile(modPath + "\\gameinfo.txt");
            gameinfo.setValue("title", "");
            gameinfo.setValue("title2", "");
            KeyValue.writeChunkFile(modPath + "\\gameinfo.txt", gameinfo, false, new UTF8Encoding(false));

            KeyValue gamemenu = new KeyValue("GameMenu");
            KeyValue.writeChunkFile(modPath + "\\resource\\gamemenu.res", gamemenu, true, new UTF8Encoding(false));

            game = new Game(sourceSDK, panelControl1);
            game.Start("+map_background material_preview +crosshair 0");
            this.ActiveControl = null;
        }

        private void MaterialEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            string modPath = sourceSDK.GetModPath();

            if (game != null && game.modProcess != null)
            {
                game.modProcess.Kill();
                File.Copy(modPath + "\\gameinfo.txt.temp", modPath + "\\gameinfo.txt", true);
                File.Copy(modPath + "\\resource\\gamemenu.res.temp", modPath + "\\resource\\gamemenu.res", true);
                File.Delete(modPath + "\\gameinfo.txt.temp");
                File.Delete(modPath + "\\resource\\gamemenu.res.temp");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            game.Command("+mat_reloadallmaterials");
        }
    }
}