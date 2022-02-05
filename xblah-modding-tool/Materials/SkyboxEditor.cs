using CefSharp;
using CefSharp.WinForms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using xblah_modding_tool.Modding;
using xblah_modding_lib;
using xblah_modding_lib.Materials;
using xblah_modding_lib.Packages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xblah_modding_tool.Materials
{
    public partial class SkyboxEditor : DevExpress.XtraEditors.XtraForm
    {
        private Launcher launcher;
        private PackageManager packageManager;

        private Dictionary<string, PictureEdit> imageEdits;
        private Dictionary<string, PictureEdit> hdrImageEdits;

        public PackageFile PackageFile { get; set; } = null;

        string Skyname { get; set; } = "";

        ChromiumWebBrowser chromium;

        public SkyboxEditor(Launcher launcher)
        {
            this.launcher = launcher;
            if (launcher.GetCurrentGame().EngineID == Engine.SOURCE)
            {
                this.packageManager = new PackageManager(launcher, "materials/skybox");
            } else if(launcher.GetCurrentGame().EngineID == Engine.GOLDSRC)
            {
                this.packageManager = new PackageManager(launcher, "gfx/env");
            } else
            {
                return;
            }

            InitializeComponent();

            SetImageEdits();
            Clear();

            UpdateSkyListCombo();

            hdrDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;

            CefSharpSettings.ShutdownOnExit = true;

            CefSettings settings = new CefSettings
            {
                CachePath = AppDomain.CurrentDomain.BaseDirectory + "/Assets/cache/"
            };
            if (!Cef.IsInitialized)
                Cef.Initialize(settings);

            // Init chromium
            chromium = new ChromiumWebBrowser(AppDomain.CurrentDomain.BaseDirectory + "Tools/SkyboxPreviewer/index.html");
            chromium.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            chromium.BrowserSettings.FileAccessFromFileUrls = CefState.Enabled;
            chromium.BrowserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chromium.BrowserSettings.BackgroundColor = Cef.ColorSetARGB(255, 0, 0, 0);

            // Add the control
            this.Controls.Add(chromium);
            chromium.Dock = DockStyle.Fill;
        }

        private void SetImageEdits()
        {
            imageEdits = new Dictionary<string, PictureEdit>
            {
                {"up", upEdit },
                {"dn", dnEdit },
                {"lf", lfEdit },
                {"rt", rtEdit },
                {"ft", ftEdit },
                {"bk", bkEdit }
            };

            hdrImageEdits = new Dictionary<string, PictureEdit>
            {
                {"up", upHdrEdit },
                {"dn", dnHdrEdit },
                {"lf", lfHdrEdit },
                {"rt", rtHdrEdit },
                {"ft", ftHdrEdit },
                {"bk", bkHdrEdit }
            };
        }

        private void UpdateSkyListCombo()
        {
            List<string> skyboxList = new List<string>();
            foreach(PackageDirectory directory in packageManager.Directories)
            {
                foreach(PackageFile file in directory.Entries)
                {
                    if ((file.Path.EndsWith("materials/skybox") && file.Extension == "vmt") || (file.Path.EndsWith("gfx/env") && file.Extension == "tga"))
                    {
                        string skyName = file.Filename;

                        if (skyName.EndsWith("up") || skyName.EndsWith("dn") || skyName.EndsWith("lf") || skyName.EndsWith("rt") || skyName.EndsWith("ft") || skyName.EndsWith("bk"))
                            skyName = skyName.Substring(0, skyName.Length - 2);

                        skyboxList.Add(skyName);
                    }
                }
            }

            skyboxList = skyboxList.Distinct().ToList();
            skyboxList.Sort();

            skyListRepository.Items.Clear();

            foreach(string skybox in skyboxList)
            {
                skyListRepository.Items.Add(skybox);
            }
        }

        private void Clear()
        {
            foreach (string face in new string[] { "up", "dn", "lf", "rt", "ft", "bk" })
            {
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "Tools/SkyboxPreviewer/blank.png", AppDomain.CurrentDomain.BaseDirectory + "Tools/SkyboxPreviewer/" + face + ".png",true);
            }

            if (imageEdits != null)
            {
                foreach (PictureEdit imageEdit in imageEdits.Values)
                {
                    imageEdit.Image = null;
                }
            }
            Skyname = "";
            UpdatePreview();
        }

        private void Open(string skyname)
        {
            foreach (string face in new string[] { "up", "dn", "lf", "rt", "ft", "bk" })
            {
                PackageFile file;

                // Source skyboxes
                if (launcher.GetCurrentGame().EngineID == Engine.SOURCE)
                {
                    file = packageManager.GetFile("materials/skybox/" + skyname + face + ".vmt");

                    if (file != null)
                    {
                        KeyValue fileData = VMT.FromData(file.Data);

                        KeyValue baseTexture = fileData.findChildByKey("$basetexture");
                        KeyValue hdrbaseTexture = fileData.findChildByKey("$hdrbasetexture");
                        KeyValue hdrcompressedTexture = fileData.findChildByKey("$hdrcompressedtexture");

                        if (baseTexture != null)
                        {
                            string baseTexturePath = baseTexture.getValue();
                            PackageFile baseTextureFile = packageManager.GetFile("materials/" + baseTexturePath + ".vtf");

                            if (baseTextureFile != null)
                            {
                                Bitmap baseTextureImage = VTF.ToBitmap(baseTextureFile.Data, launcher);

                                imageEdits[face].Image = baseTextureImage;

                                SavePreview(baseTextureImage, face);
                            }
                        }

                        if (hdrbaseTexture != null)
                        {
                            string baseTexturePath = hdrbaseTexture.getValue();
                            PackageFile baseTextureFile = packageManager.GetFile("materials/" + baseTexturePath + ".vtf");

                            if (baseTextureFile != null)
                            {
                                Bitmap baseTextureImage = VTF.ToBitmap(baseTextureFile.Data, launcher);

                                hdrImageEdits[face].Image = baseTextureImage;
                            }
                        }
                        else if (hdrcompressedTexture != null)
                        {
                            string baseTexturePath = hdrcompressedTexture.getValue();
                            PackageFile baseTextureFile = packageManager.GetFile("materials/" + baseTexturePath + ".vtf");

                            if (baseTextureFile != null)
                            {
                                Bitmap baseTextureImage = VTF.ToBitmap(baseTextureFile.Data, launcher);

                                hdrImageEdits[face].Image = baseTextureImage;
                            }
                        }
                    }

                    // Goldsrc skyboxes
                }
                else if (launcher.GetCurrentGame().EngineID == Engine.GOLDSRC)
                {
                    file = packageManager.GetFile("gfx/env/" + skyname + face + ".tga");

                    if (file != null)
                    {
                        Bitmap baseTextureImage = TGA.FromBytes(file.Data).ToBitmap();

                        imageEdits[face].Image = baseTextureImage;

                        SavePreview(baseTextureImage, face);
                    }
                }
            }

            UpdatePreview();
        }

        private void Save(string skyname)
        {
            if (launcher.GetCurrentGame().EngineID == Engine.SOURCE)
            {
                string skyboxesPath = launcher.GetCurrentMod().InstallPath + "\\materials\\skybox\\";

                Directory.CreateDirectory(skyboxesPath);

                bool hasHDRTextures = false;

                foreach (KeyValuePair<string, PictureEdit> keyValuePair in imageEdits)
                {
                    string face = keyValuePair.Key;

                    byte[] vtf = VTF.FromBitmap(keyValuePair.Value.Image as Bitmap, launcher, new string[] { "nonice 1", "nocompress 1", "nolod 1", "clamps 1", "clampt 1", "nomip 1" });

                    File.WriteAllBytes(skyboxesPath + skyname + face + ".vtf", vtf);

                    KeyValue vmtRoot;
                    if (hasHDRTextures)
                    {
                        vmtRoot = new KeyValue("Sky");
                        vmtRoot.addChild(new KeyValue("$hdrbasetexture", "skybox/" + skyname + "_hdr" + face));
                    }
                    else
                    {
                        vmtRoot = new KeyValue("UnlitGeneric");
                    }

                    vmtRoot.addChild(new KeyValue("$basetexture", "skybox/" + skyname + face));
                    vmtRoot.addChild(new KeyValue("$nofog", "1"));
                    vmtRoot.addChild(new KeyValue("$ignorez", "1"));

                    if (keyValuePair.Value.Image.Width == 2 * keyValuePair.Value.Image.Height)
                    {
                        vmtRoot.addChild(new KeyValue("$basetexturetransform", "center 0 0 scale 1 2 rotate 0 translate 0 0"));
                    }

                    KeyValue.writeChunkFile(skyboxesPath + skyname + face + ".vmt", vmtRoot, true, new UTF8Encoding(false));
                }

                if (hasHDRTextures)
                {
                    foreach (KeyValuePair<string, PictureEdit> keyValuePair in hdrImageEdits)
                    {
                        string face = keyValuePair.Key;

                        byte[] vtf = VTF.FromBitmap(keyValuePair.Value.Image as Bitmap, launcher, new string[] { "nonice 1", "nocompress 1" });
                        string fileName = skyname + face;
                        File.WriteAllBytes(skyboxesPath + skyname + "_hdr" + face + ".vtf", vtf);
                    }
                }
            } else if(launcher.GetCurrentGame().EngineID == Engine.GOLDSRC)
            {
                string skyboxesPath = launcher.GetCurrentMod().InstallPath + "\\gfx\\env\\";

                Directory.CreateDirectory(skyboxesPath);

                foreach (KeyValuePair<string, PictureEdit> keyValuePair in imageEdits)
                {
                    string face = keyValuePair.Key;

                    var destImage = new Bitmap(256, 256);
                    destImage.SetResolution(keyValuePair.Value.Image.HorizontalResolution, keyValuePair.Value.Image.VerticalResolution);

                    using (var graphics = Graphics.FromImage(destImage))
                    {
                        graphics.CompositingMode = CompositingMode.SourceCopy;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        using (var wrapMode = new ImageAttributes())
                        {
                            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                            graphics.DrawImage(keyValuePair.Value.Image, new Rectangle(0, 0, destImage.Width, destImage.Height), 0, 0, keyValuePair.Value.Image.Width, keyValuePair.Value.Image.Height, GraphicsUnit.Pixel, wrapMode);

                        }
                    }

                    TGA tga = TGA.FromBitmap(destImage);
                    tga.Save(skyboxesPath + skyname + face + ".tga");
                    destImage.Dispose();
                }
            }
        }

        private void menu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // New material
            if (e.Item == menuFileNew)
            {
                Clear();
            }
            // Open material
            else if (e.Item == menuFileOpen)
            {
                FileExplorer fileExplorer = null;
                if (launcher.GetCurrentGame().EngineID == Engine.SOURCE) {
                    fileExplorer  = new FileExplorer(launcher, FileExplorer.Mode.OPEN)
                    {
                        packageManager = packageManager,
                        RootDirectory = "materials/skybox",
                        Filter = "Skybox Files (*.skybox)|*.vmt",
                        MultiSelect = false
                    };
                } else if(launcher.GetCurrentGame().EngineID == Engine.GOLDSRC)
                {
                    fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.OPEN)
                    {
                        packageManager = packageManager,
                        RootDirectory = "gfx/env",
                        Filter = "Skybox Files (*.skybox)|*.tga",
                        MultiSelect = false
                    };
                }

                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    Skyname = fileExplorer.Selection[0].Filename;

                    if (Skyname.EndsWith("up") || Skyname.EndsWith("dn") || Skyname.EndsWith("lf") || Skyname.EndsWith("rt") || Skyname.EndsWith("ft") || Skyname.EndsWith("bk"))
                        Skyname = Skyname.Substring(0, Skyname.Length - 2);

                    Open(Skyname);
                }
            }
            // Save material
            else if (e.Item == menuFileSave)
            {
                if (Skyname != "")
                {
                    Save(Skyname);
                } else
                {
                    menuFileSaveAs.PerformClick();
                }
            }
            // Save material as
            else if (e.Item == menuFileSaveAs)
            {
                FileExplorer fileExplorer = null;
                if (launcher.GetCurrentGame().EngineID == Engine.SOURCE)
                {
                    fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.SAVE)
                    {
                        packageManager = packageManager,
                        RootDirectory = "materials/skybox",
                        Filter = "Skybox Files (*.skybox)|*.vmt",
                        MultiSelect = false
                    };
                }
                else if (launcher.GetCurrentGame().EngineID == Engine.GOLDSRC)
                {
                    fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.SAVE)
                    {
                        packageManager = packageManager,
                        RootDirectory = "gfx/env",
                        Filter = "Skybox Files (*.skybox)|*.tga",
                        MultiSelect = false
                    };
                }
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    Skyname = Path.GetFileName(fileExplorer.FileName);
                    Save(Skyname);
                }
            }
        }

        private void SkyboxEditor_Load(object sender, EventArgs e)
        {
            if (PackageFile != null)
            {
                Skyname = PackageFile.Filename;

                if (Skyname.EndsWith("up") || Skyname.EndsWith("dn") || Skyname.EndsWith("lf") || Skyname.EndsWith("rt") || Skyname.EndsWith("ft") || Skyname.EndsWith("bk"))
                    Skyname = Skyname.Substring(0, Skyname.Length - 2);

                Open(Skyname);
            }
        }

        private void cubemapEdit_ImageLoading(object sender, DevExpress.XtraEditors.Repository.SaveLoadImageEventArgs e)
        {
            RepositoryItem pictureEdit = (RepositoryItem) sender;
            PictureEdit ownerEdit = (PictureEdit) pictureEdit.OwnerEdit;

            foreach (KeyValuePair<string, PictureEdit> imageEdit in imageEdits)
            {
                if (imageEdit.Value == ownerEdit)
                {
                    SavePreview(Bitmap.FromFile(e.FileName), imageEdit.Key);
                    UpdatePreview();
                    break;
                }
            }
        }

        private void SavePreview(Image source, string face)
        {
            Bitmap bitmap = source.Clone() as Bitmap;
            switch (face)
            {
                case "up":
                    bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case "dn":
                    bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }

            var destImage = new Bitmap(1024, 1024);
            destImage.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    if (bitmap.Width == bitmap.Height)
                    {
                        graphics.DrawImage(bitmap, new Rectangle(0, 0, destImage.Width, destImage.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, wrapMode);
                    } else if (bitmap.Width == 2 * bitmap.Height)
                    {
                        graphics.DrawImage(bitmap, new Rectangle(0, 0, destImage.Width, destImage.Height / 2), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, wrapMode);
                        graphics.DrawImage(bitmap, new Rectangle(0, destImage.Height / 2, destImage.Width, destImage.Height / 2), 0, bitmap.Height - 1, bitmap.Width, 1, GraphicsUnit.Pixel, wrapMode);
                    }
                }
            }

            destImage.Save(AppDomain.CurrentDomain.BaseDirectory + "Tools/SkyboxPreviewer/" + face + ".png", ImageFormat.Png);
            //System.Diagnostics.Debugger.Break();
            bitmap.Dispose();
        }

        private void settingsCopyButton_Click(object sender, EventArgs e)
        {
            if (sender == settingsBrightnessCopyButton)
            {
                Clipboard.SetText(settingsBrightnessColorEdit.Color.R + " " + settingsBrightnessColorEdit.Color.G + " " + settingsBrightnessColorEdit.Color.B + " " + settingsBrightnessIntensityEdit.Value);
            } else if(sender == settingsAmbientCopyButton)
            {
                Clipboard.SetText(settingsAmbientColorEdit.Color.R + " " + settingsAmbientColorEdit.Color.G + " " + settingsAmbientColorEdit.Color.B + " " + settingsAmbientIntensityEdit.Value);
            }
            else if(sender == settingsFogCopyButton)
            {
                Clipboard.SetText(settingsFogColorEdit.Color.R + " " + settingsFogColorEdit.Color.G + " " + settingsFogColorEdit.Color.B);
            }
            else if(sender == settingsPitchYawRollCopyButton)
            {
                Clipboard.SetText(settingsPitchYawRollEdit.Text);
            }
            else if(sender == settingsPitchCopyButton)
            {
                Clipboard.SetText(settingsPitchEdit.Text);
            }
        }

        private void UpdateSettings()
        {
            int step = 4;

            int sectorSize = 4;

            int rSum = 0;
            int gSum = 0;
            int bSum = 0;
            int pixelCount = 0;

            int rSumHorizon = 0;
            int gSumHorizon = 0;
            int bSumHorizon = 0;
            int pixelCountHorizon = 0;

            Color brightnessColor = Color.Black;
            int brightnessIntensity = 0;

            Color ambientColor = Color.Black;
            int ambientIntensity = 0;

            Color fogColor = Color.Black;

            Dictionary<string, (int[,], int[,], int[,], int[,])> d = new Dictionary<string, (int[,], int[,], int[,], int[,])>();

            // Traverse through each face, minus dn
            foreach (string face in new string[] { "up", "lf", "rt", "ft", "bk" })
            {
                // Get the face bitmap
                Bitmap bitmap = imageEdits[face].Image as Bitmap;

                if (bitmap != null)
                {
                    // If it's a side face, only look at the above half.
                    int height = (face == "up" ? bitmap.Height : (bitmap.Width / 2));

                    if (!d.ContainsKey(face))
                    {
                        d[face] = (new int[bitmap.Width / sectorSize, height / sectorSize],
                           new int[bitmap.Width / sectorSize, height / sectorSize],
                           new int[bitmap.Width / sectorSize, height / sectorSize],
                           new int[bitmap.Width / sectorSize, height / sectorSize]);
                    }

                    // Traverse through the bitmap horizontally, from left to right.
                    for (int i = 0; i < bitmap.Width; i += step)
                    {
                        // Traverse through the bitmap vertically, from top to bottom.
                        for (int j = 0; j < height; j += step)
                        {
                            // Get the sector index.
                            int sectorI = (int)Math.Floor((double)i / sectorSize);
                            int sectorJ = (int)Math.Floor((double)j / sectorSize);

                            // Get the pixel color
                            Color pixelColor = bitmap.GetPixel(i, j);

                            // Add the pixel color to the sum
                            rSum += pixelColor.R;
                            gSum += pixelColor.G;
                            bSum += pixelColor.B;
                            pixelCount++;

                            // Add the horizon pixel color to the sum
                            if (j >= height - step * 4 && face != "up")
                            {
                                rSumHorizon += pixelColor.R;
                                gSumHorizon += pixelColor.G;
                                bSumHorizon += pixelColor.B;
                                pixelCountHorizon++;
                            }

                            // Add the pixel color to the sector sum
                            d[face].Item1[sectorI, sectorJ] += pixelColor.R;
                            d[face].Item2[sectorI, sectorJ] += pixelColor.G;
                            d[face].Item3[sectorI, sectorJ] += pixelColor.R;
                            d[face].Item4[sectorI, sectorJ] ++;

                            // Get the brightness
                            float brightness = pixelColor.GetBrightness();

                            // Check if pixel is brighter
                            if (brightness > brightnessColor.GetBrightness())
                                brightnessColor = pixelColor;
                        }
                    }
                }
            }

            float brightestSectorBrightness = 0;
            Color brightestSectorColor = Color.Black;
            string brightestFace = "";
            (int, int) brightestSector = (0, 0);
            (int, int) sectorsPerFace = (1, 1);

            foreach(KeyValuePair<string, (int[,], int[,], int[,], int[,])> f in d)
            {
                string face = f.Key;

                var k = f.Value;
                for (int i = 0; i < f.Value.Item1.GetLength(0); i++)
                {
                    for (int j = 0; j < f.Value.Item1.GetLength(1); j++)
                    {
                        int r = f.Value.Item1[i, j];
                        int g = f.Value.Item2[i, j];
                        int b = f.Value.Item3[i, j];
                        int count = f.Value.Item4[i, j];

                        Color c = Color.FromArgb(r / count, g / count, b / count);
                        float brt = c.GetBrightness();
                        if (brt > brightestSectorBrightness)
                        {
                            brightestSectorBrightness = brt;
                            brightestSectorColor = c;
                            brightestFace = f.Key;
                            brightestSector = (i, j);
                            sectorsPerFace = (f.Value.Item1.GetLength(0), f.Value.Item1.GetLength(1));
                        }
                    }
                }
                //
            }
            double cx = 0;
            double cy = 0;

            if (brightestFace != "up")
            {
                // Side faces
                int bx = sectorsPerFace.Item1 / 2;
                int ax = brightestSector.Item1 - bx - sectorSize / 2;
                cx = Math.Atan2(ax, bx) * 180 / Math.PI;

                int by = sectorsPerFace.Item1 / 2;
                int ay = brightestSector.Item2 - by - sectorSize / 2;
                cy = Math.Atan2(ay, by) * 180 / Math.PI;

                // Ft points to 180 degrees
                // Lf points to 270 degrees
                // Bk points to 0 degrees
                // RT points to 90 degrees
                switch (brightestFace)
                {
                    // center of LF comes from 0;
                    case "lf":
                        cx = -cx + 0;
                        break;
                    // center of FT comes from 90;
                    case "ft":
                        cx = -cx + 90;
                        break;
                    // center of RT comes from 180;
                    case "rt":
                        cx = -cx + 180;
                        break;
                    // center of BK comes from 90;
                    case "bk":
                        cx = -cx + 270;
                        break;
                }
            } else
            {
                // Up face
                int bx = sectorsPerFace.Item1 / 2;
                int ax = brightestSector.Item1 - bx - sectorSize / 2;

                int by = sectorsPerFace.Item2 / 2;
                int ay = brightestSector.Item2 - by - sectorSize / 2;

                cx = Math.Atan2(ax, -ay) * 180 / Math.PI;

                double hiponetuse = Math.Sqrt(Math.Pow(ax, 2) + Math.Pow(ay, 2));

                cy = -90 + Math.Atan2(hiponetuse, bx) * 180 / Math.PI;
            }

            settingsPitchYawRollEdit.Text = "0 " + (int)cx + " 0";
            settingsPitchEdit.Text = ((int)cy).ToString();

            brightnessIntensity = (int)(brightnessColor.GetBrightness() * 200);

            if (pixelCount > 0)
                ambientColor = Color.FromArgb(rSum / pixelCount, gSum / pixelCount, bSum / pixelCount);
            ambientIntensity = (int)(ambientColor.GetBrightness() * 200);

            if (pixelCountHorizon > 0)
                fogColor = Color.FromArgb(rSumHorizon / pixelCountHorizon, gSumHorizon / pixelCountHorizon, bSumHorizon / pixelCountHorizon);

            // Update the editors
            settingsBrightnessColorEdit.Color = brightnessColor;
            settingsBrightnessIntensityEdit.Value = brightnessIntensity;

            settingsAmbientColorEdit.Color = ambientColor;
            settingsAmbientIntensityEdit.Value = ambientIntensity;

            settingsFogColorEdit.Color = fogColor;
        }

        private void UpdatePreview()
        {
            if (chromium != null && chromium.IsBrowserInitialized)
                chromium.Reload();

            UpdateSettings();

            Text = "Skybox Editor - " + (Skyname == "" ? "Untitled" : Skyname);
        }

        private void menuEditRefreshSettingsButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateSettings();
        }
    }
}