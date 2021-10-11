using CefSharp;
using CefSharp.WinForms;
using DevExpress.XtraEditors;
using source_modding_tool.Modding;
using SourceSDK;
using SourceSDK.Materials;
using SourceSDK.Packages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace source_modding_tool.Materials
{
    public partial class SkyboxEditor : DevExpress.XtraEditors.XtraForm
    {
        private Launcher launcher;
        private PackageManager packageManager;

        private Dictionary<string, PictureEdit> imageEdits;
        private Dictionary<string, PictureEdit> hdrImageEdits;

        ChromiumWebBrowser chromium;

        public SkyboxEditor(Launcher launcher)
        {
            this.launcher = launcher;
            this.packageManager = new PackageManager(launcher, "materials/skybox");

            InitializeComponent();

            Clear();

            SetImageEdits();
            UpdateSkyListCombo();

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
                    if (file.Path.EndsWith("materials/skybox") && file.Extension == "vmt")
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
        }

        private void Open(string skyname)
        {
            foreach (string face in new string[] { "up", "dn", "lf", "rt", "ft", "bk" })
            {
                PackageFile file = packageManager.GetFile("materials/skybox/" + skyname + face + ".vmt");

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

                            switch (face)
                            {
                                case "up":
                                    baseTextureImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                    break;
                                case "dn":
                                    baseTextureImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    break;
                            }

                            baseTextureImage.Save(AppDomain.CurrentDomain.BaseDirectory + "Tools/SkyboxPreviewer/" + face + ".png", ImageFormat.Png);
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
            }

            chromium.Reload();
        }

        private void skyListCombo_EditValueChanged(object sender, EventArgs e)
        {
            string skyname = skyListCombo.EditValue.ToString();

            Open(skyname);
        }

        private void clearButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach(PictureEdit imageEdit in imageEdits.Values)
            {
                imageEdit.Image = null;
            }
        }

        private void saveButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string skyboxesPath = launcher.GetCurrentMod().InstallPath + "\\materials\\skybox\\";
            string skyName = skyListCombo.EditValue.ToString();

            Directory.CreateDirectory(skyboxesPath);

            foreach (KeyValuePair<string, PictureEdit> keyValuePair in imageEdits)
            {
                string face = keyValuePair.Key;

                byte[] vtf = VTF.FromBitmap(keyValuePair.Value.Image as Bitmap, launcher, new string[] { "nonice 1", "nocompress 1" });
                string fileName = skyName + face;
                File.WriteAllBytes(skyboxesPath + fileName + ".vtf", vtf);

                KeyValue vmtRoot = new KeyValue("Sky");
                vmtRoot.addChild(new KeyValue("$hdrbasetexture", "skybox/" + fileName + "_hdr"));
                vmtRoot.addChild(new KeyValue("$basetexture", "skybox/" + fileName));
                vmtRoot.addChild(new KeyValue("$nofog", "1"));
                vmtRoot.addChild(new KeyValue("$ignorez", "1"));

                KeyValue.writeChunkFile(skyboxesPath + fileName + ".vmt", vmtRoot, true, new UTF8Encoding(false));
            }

            foreach (KeyValuePair<string, PictureEdit> keyValuePair in hdrImageEdits)
            {
                string face = keyValuePair.Key;

                byte[] vtf = VTF.FromBitmap(keyValuePair.Value.Image as Bitmap, launcher, new string[] { "nonice 1", "nocompress 1" });
                string fileName = skyName + face;
                File.WriteAllBytes(skyboxesPath + fileName + "_hdr.vtf", vtf);
            }
        }

        private void menu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Open material
            if (e.Item == menuFileOpen)
            {
                FileExplorer fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.OPEN)
                {
                    packageManager = packageManager,
                    RootDirectory = "materials/skybox",
                    Filter = "Skybox Files (*.vmt)|*.vmt",
                    MultiSelect = false
                };
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    string skyname = fileExplorer.Selection[0].Filename;
                    skyname = skyname.Substring(0, skyname.Length - 2);
                    if (skyname.EndsWith("_hdr"))
                        skyname = skyname.Substring(0, skyname.Length - 4);

                    Open(skyname);
                }
            }
        }
    }
}