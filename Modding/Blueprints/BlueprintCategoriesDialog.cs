

using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using Newtonsoft.Json;
using SourceSDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;

namespace source_modding_tool.Modding.Blueprints
{
    public partial class BlueprintCategoriesDialog : DevExpress.XtraEditors.XtraForm
    {
        string Section { get; set; } = "";
        string EngineString { get; set; } = "";

        Launcher launcher;

        List<Thread> downloadThreads = new List<Thread>();
        public BlueprintCategoriesDialog(Launcher launcher, string section)
        {
            this.launcher = launcher;

            switch (launcher.GetCurrentGame().EngineID)
            {
                case Engine.GOLDSRC:
                    EngineString = "goldsrc";
                    break;
                case Engine.SOURCE:
                    EngineString = "source";
                    break;
                case Engine.SOURCE2:
                    EngineString = "source2";
                    break;
            }

            Section = section;

            InitializeComponent();

            /*var client = new MegaApiClient();
            client.LoginAnonymous();

            Uri fileLink = new Uri("https://mega.nz/file/avg0USyb#82-grQuLqUdqk1UHtd0VNUg7q0e4DPuIHgsDqpncBf8");
            INodeInfo node = client.GetNodeFromLink(fileLink);

            Console.WriteLine($"Downloading {node.Name}");
            client.DownloadFile(fileLink, node.Name);

            client.Logout();*/
        }

        private void PrefabsWorkshop_Load(object sender, EventArgs e)
        {
                dynamic json = GetJson("https://modding-assets.net/categories/json/" + EngineString + "/" + Section);

                LoadCategories(json);
                LoadItems(json);         
        }

        private dynamic GetJson(string url)
        {
            using (WebClient wc2 = new WebClient())
            {
                //System.Diagnostics.Debugger.Break();
                var html = wc2.DownloadString(url);
                return JsonConvert.DeserializeObject(html);
            }
        }

        private void LoadCategories(dynamic json)
        {
            categoriesControl.Clear();
            foreach (var category in json.categories)
            {
                string name = category.name;
                AccordionControlElement categoryNode = AddGroup(categoriesControl, name);

                AccordionControlElement subcategoryAllNode = AddItem(categoryNode, "All");
                subcategoryAllNode.Click += SubcategoryNode_Click;
                subcategoryAllNode.Tag = "https://modding-assets.net/" + category.path;

                foreach (var subcategory in category.subcategories)
                {
                    string subname = subcategory.name;
                    AccordionControlElement subcategoryNode = AddItem(categoryNode, subname);
                    subcategoryNode.Tag = "https://modding-assets.net/" + subcategory.path;
                    subcategoryNode.Click += SubcategoryNode_Click;
                }
            }
        }

        private void SubcategoryNode_Click(object sender, EventArgs e)
        {
            dynamic json = GetJson((sender as AccordionControlElement).Tag.ToString());
            LoadItems(json);
        }

        private void LoadItems(dynamic json)
        {
            galleryControl1.Gallery.Groups[0].Items.Clear();
            foreach (var item in json.items)
            {
                string local_path = Launcher.UserDirectory + "Resources\\Workshop\\Thumbnails\\" + item.id + ".jpg";
                string remote_path = item.image_url;

                GalleryItem galleryItem = new GalleryItem();
                galleryItem.Caption = item.name;
                galleryControl1.Gallery.Groups[0].Items.Add(galleryItem);
                galleryItem.Tag = "https://modding-assets.net/" + item.path;
                //galleryItem.Description = "This is a blueprint description.";
                galleryItem.ItemClick += GalleryItem_ItemClick;

                if (File.Exists(local_path))
                {
                    Image img = Bitmap.FromFile(local_path);
                    galleryItem.ImageOptions.Image = img;
                }
                else
                {
                    Thread thread = new Thread(() =>
                    {
                        WebClient wc = new WebClient();
                        byte[] bytes = wc.DownloadData(remote_path);
                        MemoryStream ms = new MemoryStream(bytes);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                        img = ResizeImage(img, 320, 180);

                        Invoke(new Action(() =>
                        {
                            //img.Save(local_path);
                            galleryItem.ImageOptions.Image = img;
                        }));

                    });
                    downloadThreads.Add(thread);
                    thread.Start();
                }
            }
        }

        private void GalleryItem_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            BlueprintItemDialog dialog = new BlueprintItemDialog(launcher, (sender as GalleryItem).Tag.ToString());
            dialog.ShowDialog();
        }

        AccordionControlElement AddGroup(AccordionControl accrodionCtrl, string txt)
        {
            AccordionControlElement group = new AccordionControlElement() { Text = txt };
            accrodionCtrl.Elements.Add(group);
            return group;
        }

        AccordionControlElement AddItem(AccordionControlElement group, string txt)
        {
            AccordionControlElement item = new AccordionControlElement() { Text = txt, Style = ElementStyle.Item };
            group.Elements.Add(item);
            return item;
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

        private void PrefabsWorkshop_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            foreach(Thread thread in downloadThreads)
            {
                if (thread != null && thread.IsAlive)
                    thread.Abort();
            }
        }
    }
}