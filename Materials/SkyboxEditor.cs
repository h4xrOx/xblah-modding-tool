using DevExpress.XtraEditors;
using SourceSDK;
using SourceSDK.Materials;
using SourceSDK.Packages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

        public SkyboxEditor(Launcher launcher)
        {
            this.launcher = launcher;
            this.packageManager = new PackageManager(launcher, "");

            InitializeComponent();

            SetImageEdits();
            UpdateSkyListCombo();
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

        private void skyListCombo_EditValueChanged(object sender, EventArgs e)
        {
            string skyname = skyListCombo.EditValue.ToString();

            foreach (string face in new string[]{ "up", "dn", "lf", "rt", "ft", "bk"}) {
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
    }
}