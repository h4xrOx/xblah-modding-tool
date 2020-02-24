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

        Game game;
        bool isPreviewing = false;
        Dictionary<string, PictureEdit> pictureEdits;

        object popupMenuActivator;
        Steam sourceSDK;

        string modPath;

        Dictionary<string, Texture> textures;

        public MaterialEditor(string relativePath, Steam sourceSDK)
        {
            this.sourceSDK = sourceSDK;
            modPath = sourceSDK.GetModPath();


            InitializeComponent();
            populatePictureEdits();
            ClearMaterial();
            if(relativePath != string.Empty)
            {
                LoadMaterial(relativePath);
            }
        }

        private void barButtonOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            openVMTFileDialog.InitialDirectory = modPath + "\\materials\\";
            if(openVMTFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openVMTFileDialog.FileName;
                LoadMaterial(fullPath);
            }
        }

        private void barButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { SaveMaterial(textPath.EditValue.ToString(), shaderCombo.EditValue.ToString()); }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            //SaveMaterial("models/tools/material_preview", "VertexLitGeneric");
            startPreview();
        }

        private void startPreview()
        {
            string sourcePath = AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\IngamePreviews\\";
 
            // Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, modPath));

            // Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, modPath), true);

            game = new Game(sourceSDK, panelControl1);
            game.Start("-nomouse +map material_preview +crosshair 0");
            this.ActiveControl = null;

            isPreviewing = true;
        }

        private void ClearMaterial()
        {
            textures = new Dictionary<string, Texture>();

            foreach(KeyValuePair<string, PictureEdit> kv in pictureEdits)
            {
                textures.Add(kv.Key, new Texture());
                kv.Value.Image = null;
            }

            textPath.EditValue = "concrete/new_material";

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
                textures["tooltexture"].bytes = VTF.FromBitmap(tooltexture, sourceSDK);
                textures["tooltexture"].relativePath = string.Empty;
            }

            pictureToolTexture.Image = textures["tooltexture"].bitmap;

            if(isPreviewing)
            {
                SaveMaterial("models/tools/material_preview", "VertexLitGeneric");
                game.Command("+mat_reloadallmaterials");
            }
        }

        private void LoadMaterial(string fullPath)
        {
            SourceSDK.KeyValue vmt = SourceSDK.KeyValue.readChunkfile(fullPath);

            string relativePath = fullPath;

            string modPath = sourceSDK.GetModPath();
            string gamePath = sourceSDK.GetGamePath();

            if (fullPath.Contains(modPath))
            {
                relativePath = fullPath.Replace(modPath, string.Empty).Replace(".vmt", string.Empty);
            }
            else if (fullPath.Contains("\\materials\\"))
            {
                relativePath = fullPath.Substring(fullPath.LastIndexOf("\\materials\\") + "\\materials\\".Length)
                    .Replace(".vmt", string.Empty);
            }
            else
            {
                Uri path1 = new Uri(gamePath + "\\");
                Uri path2 = new Uri(fullPath);
                Uri diff = path1.MakeRelativeUri(path2);

                relativePath = diff.OriginalString.Replace(".vmt", string.Empty);
            }

            textPath.EditValue = relativePath.Substring("\\materials\\".Length);

            VPKManager vpkManager = new VPKManager(sourceSDK);
            //Debugger.Break();

            foreach (KeyValuePair<string, PictureEdit> kv in pictureEdits)
            {
                string value = vmt.getValue("$" + kv.Key);
                if (value != null)
                {
                    string texturePath = vpkManager.getExtractedPath("materials/" + value + ".vtf");
                    if (texturePath != "" && File.Exists(texturePath))
                    {
                        textures[kv.Key].relativePath = value;
                        textures[kv.Key].bytes = File.ReadAllBytes(texturePath);
                        textures[kv.Key].bitmap = VTF.ToBitmap(textures[kv.Key].bytes, sourceSDK);
                        kv.Value.Image = textures[kv.Key].bitmap;
                    } else
                    {
                        ClearTexture(kv.Value);
                    }
                    
                }
            }

            if (vmt.getValue("$normalmapalphaenvmapmask") == "1" && textures["bumpmap"].bitmap != null)
            {
                textures["envmapmask"].bitmap = new Bitmap(textures["bumpmap"].bitmap.Width, textures["bumpmap"].bitmap.Height);
                for(int i = 0; i < textures["bumpmap"].bitmap.Width; i++)
                {
                    for (int j = 0; j < textures["bumpmap"].bitmap.Height; j++)
                    {
                        int alpha = textures["bumpmap"].bitmap.GetPixel(i, j).A;
                        textures["envmapmask"].bitmap.SetPixel(i, j, Color.FromArgb(alpha, alpha, alpha));
                    }
                }
                textures["envmapmask"].bytes = VTF.FromBitmap(textures["envmapmask"].bitmap, sourceSDK);
                textures["envmapmask"].relativePath = "";
                pictureEnvMapMask.Image = textures["envmapmask"].bitmap;
            }
        }

        private void LoadTexture(PictureEdit pictureEdit)
        {
            string tag = pictureEdit.Tag.ToString();

            int width = int.Parse(textWidth.EditValue.ToString());
            int height = int.Parse(textHeight.EditValue.ToString());

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
                    textures[tag].bitmap = VTF.ToBitmap(textures[tag].bytes, sourceSDK);
                } else
                {
                    textures[tag].relativePath = string.Empty;
                    textures[tag].bitmap = new Bitmap(Bitmap.FromFile(openBitmapFileDialog.FileName), width, height);
                    textures[tag].bytes = VTF.FromBitmap(textures[tag].bitmap, sourceSDK);
                }

                if(textures[tag].bitmap != null)
                    pictureEdit.Image = textures[tag].bitmap;
            }

            CreateToolTexture();
        }

        private void stopPreview()
        {
            if (game != null && game.modProcess != null)
            {
                game.modProcess.Kill();
            }
        }

        private void MaterialEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopPreview();
        }

        private void pictureBaseTexture_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                LoadTexture((PictureEdit)sender);
            }
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

        private void popupMenu_Popup(object sender, EventArgs e) { popupMenuActivator = popupMenu.Activator; }

        private void SaveMaterial(string path, string shader)
        {
            SourceSDK.KeyValue vmt = new SourceSDK.KeyValue(shader);

            string relativePath = path;
            string fullPath = (sourceSDK.GetModPath() + "\\materials\\" + relativePath).Replace("/", "\\");

            Directory.CreateDirectory(fullPath.Substring(0, fullPath.LastIndexOf("\\")));

            bool hasNormalMap = false;
            bool hasSpecularMap = false;

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
                            hasSpecularMap = true;
                            break;
                        case "bumpmap":
                            hasNormalMap = true;
                            break;
                        default:
                            vmt.addChild(new KeyValue("$" + texture.Key, relativePath + "_" + texture.Key));
                            File.WriteAllBytes(fullPath + "_" + texture.Key + ".vtf", texture.Value.bytes);
                            break;
                    }
                }
            }

            if(hasNormalMap && hasSpecularMap)
            {
                Bitmap normalBitmap = textures["bumpmap"].bitmap;
                Bitmap specularBitmap = new Bitmap(textures["envmapmask"].bitmap,
                                                   normalBitmap.Width,
                                                   normalBitmap.Height);

                for(int i = 0; i < normalBitmap.Width; i++)
                {
                    for(int j = 0; j < normalBitmap.Height; j++)
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
                textures["bumpmap"].bytes = VTF.FromBitmap(textures["bumpmap"].bitmap, sourceSDK);
                vmt.addChild(new KeyValue("$bumpmap", relativePath + "_bumpmap"));
                vmt.addChild(new KeyValue("$normalmapalphaenvmapmask", "1"));
                vmt.addChild(new KeyValue("$envmap", "env_cubemap"));
                File.WriteAllBytes(fullPath + "_bumpmap.vtf", textures["bumpmap"].bytes);
            } else if(hasNormalMap)
            {
                vmt.addChild(new KeyValue("$bumpmap", relativePath + "_bumpmap"));
                File.WriteAllBytes(fullPath + "_bumpmap.vtf", textures["bumpmap"].bytes);
            } else if(hasSpecularMap)
            {
                vmt.addChild(new KeyValue("$envmap", "env_cubemap"));
                vmt.addChild(new KeyValue("$envmapmask", relativePath + "_envmapmask"));
                File.WriteAllBytes(fullPath + "_envmapmask.vtf", textures["envmapmask"].bytes);
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


        public class Texture
        {
            public Bitmap bitmap = null;
            public byte[] bytes = null;
            public string relativePath = string.Empty;
        }
    }
}