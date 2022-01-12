using CG.Web.MegaApiClient;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraWaitForm;
using Newtonsoft.Json;
using xblah_modding_lib;
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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xblah_modding_tool.Modding.Blueprints
{
    public partial class BlueprintItemDialog : DevExpress.XtraEditors.XtraForm
    {
        BlueprintWaitForm waitForm;

        List<Thread> downloadThreads = new List<Thread>();

        Launcher launcher;

        string Path { get; set; } = "";

        string FileName { get; set; } = "";

        string LocalPath { get; set; } = "";

        string RemotePath { get; set; } = "";

        bool Installed { get; set; } = false;
        public BlueprintItemDialog(Launcher launcher, string path)
        {
            this.Path = path;
            this.launcher = launcher;
 
            InitializeComponent();

            otherDescriptionHeight = descriptionLayout.Height - descriptionEdit.Height;
            otherAuthorsHeight = authorsLayout.Height - authorsEdit.Height;
            descriptionEdit.EditValueChanging += DescriptionEdit_EditValueChanging;
            authorsEdit.EditValueChanging += AuthorsEdit_EditValueChanging;
        }

        private void AuthorsEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            authorsLayout.Height = authorsEdit.CalcAutoHeight() + otherAuthorsHeight;
        }

        private void DescriptionEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            descriptionLayout.Height = descriptionEdit.CalcAutoHeight() + otherDescriptionHeight;
        }

        int otherDescriptionHeight;
        int otherAuthorsHeight;

        private void BlueprintItemDialog_Load(object sender, EventArgs e)
        {
            dynamic json = GetJson(Path);
            //System.Diagnostics.Debugger.Break();

            nameLabel.Text = json.name;
            descriptionEdit.EditValue = json.description;
            engineEdit.EditValue = json.engine;
            sectionEdit.EditValue = json.section;
            categoryEdit.EditValue = json.category;

            List<string> authors = new List<string>();
            foreach(dynamic author in json.authors)
            {
                string authorName = author;
                authors.Add(authorName);
            }
            authorsEdit.EditValue = string.Join("\r\n", authors);

            foreach (dynamic screenshot in json.screenshots)
            {
                Thread thread = new Thread(() =>
                {
                    string remote_path = screenshot;
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] bytes = wc.DownloadData(remote_path);
                    MemoryStream ms = new MemoryStream(bytes);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    Bitmap thumbnail = ResizeImage(img, 120, 67);

                    Invoke(new Action(() =>
                    {
                        //img.Save(local_path);
                        GalleryItem galleryItem = new GalleryItem();
                        galleryItem.Tag = img;
                        galleryControl1.Gallery.Groups[0].Items.Add(galleryItem);
                        galleryItem.ImageOptions.Image = thumbnail;
                        galleryItem.ItemClick += GalleryItem_ItemClick;

                        if (pictureEdit1.Image == null)
                            pictureEdit1.Image = img;
                    }));

                });
                downloadThreads.Add(thread);
                thread.Start();
            }

            foreach (dynamic mirror in json.mirrors)
            {
                string url = mirror;
                if (url.StartsWith("https://mega.nz/"))
                {
                    RemotePath = url;
                    break;
                }
            }

            string filename = json.filename;

            //System.Diagnostics.Debugger.Break();
            LocalPath = launcher.GetCurrentMod().InstallPath + "\\custom\\" + filename + ".vpk";
            FileName = filename;
            //System.Diagnostics.Debugger.Break();
            UpdateInstallStatus(File.Exists(LocalPath));
        }

        private void GalleryItem_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            pictureEdit1.Image = (sender as GalleryItem).Tag as Bitmap;
        }

        private dynamic GetJson(string url)
        {
            using (System.Net.WebClient wc2 = new System.Net.WebClient())
            {
                //System.Diagnostics.Debugger.Break();
                var html = wc2.DownloadString(url);
                return JsonConvert.DeserializeObject(html);
            }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

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
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void BlueprintItemDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Thread thread in downloadThreads)
            {
                if (thread != null && thread.IsAlive)
                    thread.Abort();
            }
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            if (Installed)
            {
                File.Delete(LocalPath);
                UpdateInstallStatus(File.Exists(LocalPath));
            } else
            {
                waitForm = new BlueprintWaitForm();

                Thread thread = new Thread(() =>
                {
                    DeleteDownloadedFile();
                    DownloadFile();
                    ExtractFile();
                    DeleteDownloadedFile();
                    Invoke(new Action(() =>
                    {
                        waitForm.Close();
                        UpdateInstallStatus(File.Exists(LocalPath));
                    }));
                });
                downloadThreads.Add(thread);
                thread.Start();

                waitForm.ShowDialog();
            }
        }

        private void DownloadFile()
        {
            var client = new MegaApiClient();
            client.LoginAnonymous();

            Uri fileLink = new Uri(RemotePath);
            INodeInfo node = client.GetNodeFromLink(fileLink);

            Console.WriteLine($"Downloading {node.Name}");
            client.DownloadFile(fileLink, node.Name);

            client.Logout();
        }

        private  void DeleteDownloadedFile()
        {
            if (File.Exists(Launcher.ApplicationDirectory + FileName + ".7z"))
            {
                File.Delete(Launcher.ApplicationDirectory + FileName + ".7z");
            }
        }

        private void ExtractFile()
        {
            if (File.Exists(Launcher.ApplicationDirectory + FileName + ".7z"))
            {
                string source = Launcher.ApplicationDirectory + FileName + ".7z";
                string dest = new FileInfo(LocalPath).DirectoryName;

                ProcessStartInfo pro = new ProcessStartInfo();
                pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = Launcher.ApplicationDirectory + "Tools\\7z\\7za.exe";
                pro.Arguments = string.Format("x \"{0}\" -y -o\"{1}\"", source, dest);
                Process x = Process.Start(pro);
                x.WaitForExit();
            } else
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        private void UpdateInstallStatus(bool status)
        {
            if (status == true)
            {
                Installed = true;
                statusLabel.Text = "Installed";
                statusLabel.ForeColor = Color.FromArgb(255, 0, 176, 80);
                installButton.Text = "Uninstall";
            }
            else
            {
                Installed = false;
                statusLabel.Text = "Not installed";
                statusLabel.ForeColor = Color.FromArgb(255, 127, 127, 127);
                installButton.Text = "Install";
            }
        }
    }
}