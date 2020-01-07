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
using windows_source1ide.SourceSDK;

namespace windows_source1ide
{
    public partial class MaterialEditor : DevExpress.XtraEditors.XtraForm
    {
        Steam sourceSDK;

        Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

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

            textures.Add("basetexture", new Texture());
            textures.Add("basetexture2", new Texture());
            textures.Add("bumpmap", new Texture());
            textures.Add("parallaxmap", new Texture());
            textures.Add("blendmodulatetexture", new Texture());
            textures.Add("tooltexture", new Texture());
        }

        private void picture_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string type = new FileInfo(openFileDialog.FileName).Extension;

                string modPath = sourceSDK.GetModPath();

                Uri path1 = new Uri(modPath + "\\");
                Uri path2 = new Uri(openFileDialog.FileName);
                Uri diff = path1.MakeRelativeUri(path2);

                string tag = ((PictureEdit)sender).Tag.ToString();
                if (type == ".vtf")
                {
                    textures[tag].relativePath = diff.OriginalString;
                    textures[tag].bytes = File.ReadAllBytes(openFileDialog.FileName);
                    textures[tag].bitmap = VTF.toBitmap(textures[tag].bytes, sourceSDK);
                } else
                {
                    textures[tag].relativePath = "";
                    textures[tag].bitmap = (Bitmap) Bitmap.FromFile(openFileDialog.FileName);
                    textures[tag].bytes = VTF.fromBitmap(textures[tag].bitmap, sourceSDK);
                }

                if (textures[tag].bitmap != null)
                    ((PictureEdit)sender).Image = textures[tag].bitmap;
            }
        }

        private void buttonCreateToolTexture_Click(object sender, EventArgs e)
        {
            Bitmap basetexture = textures["basetexture"].bitmap;
            Bitmap basetexture2 = textures["basetexture2"].bitmap;

            // Resize images
            int width = basetexture.Width;
            int height = basetexture.Height;

            // Merge images
            Bitmap tooltexture = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color baseColor = basetexture.GetPixel(i, j);
                    Color baseColor2 = basetexture2.GetPixel(i, j);

                    //float xdiff = (1f - (Math.Max((float)i - (float)width / 2f, 0f) / ((float)width / 2f)));

                    float baseMultiply = Math.Min(Math.Max(2.5f - (float)(i + j) / (width + height) * 4, 0),1);
                    float baseMultiply2 = 1 - baseMultiply;

                    // * (0.5f + ((j - height / 2f) / height))

                    Color toolColor = Color.FromArgb(
                        (int)(baseColor.R * baseMultiply + baseColor2.R * baseMultiply2),
                        (int)(baseColor.G * baseMultiply + baseColor2.G * baseMultiply2),
                        (int)(baseColor.B * baseMultiply + baseColor2.B * baseMultiply2));
                    tooltexture.SetPixel(i, j, toolColor);
                }
            }

            pictureToolTexture.Image = tooltexture;
        }
    }
}