using DevExpress.XtraEditors;
 
using source_modding_tool.Tools;
using SourceSDK;
using SourceSDK.Materials;
using SourceSDK.Packages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace source_modding_tool
{
    public partial class ChaptersForm : DevExpress.XtraEditors.XtraForm
    {
        List<Chapter> chapters = new List<Chapter>();
        string gamePath = string.Empty;

        KeyValue lang;
        string modPath = string.Empty;
        Launcher launcher;

        public ChaptersForm(Launcher launcher)
        {
            InitializeComponent();

            this.launcher = launcher;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            chapters.Add(new Chapter(launcher.GetCurrentGame().EngineID));
            updateChaptersList();
        }

        private void buttonBackground_Click(object sender, EventArgs e)
        {
            Game game = launcher.GetCurrentGame();
            LegacyFileExplorer form = new LegacyFileExplorer(launcher);
            form.RootDirectory = "maps/";
            form.Filter = "BSP Files (*.bsp)|*.bsp|VPK Files (*.vpk)|*.vpk";
            if (form.ShowDialog() == DialogResult.OK)
            {
                VPK.File file = form.selectedFiles[0];
                if (file.type == ".bsp" && game.EngineID == Engine.SOURCE)
                {
                    // It's a map
                    string mapName = Path.GetFileNameWithoutExtension(file.path);
                    textBackground.EditValue = mapName;

                }
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if(galleryControl1.Gallery.GetCheckedItem() != null)
            {
                int index = galleryControl1.Gallery.Groups[0].Items.IndexOf(galleryControl1.Gallery.GetCheckedItem());
                DevExpress.XtraBars.Ribbon.GalleryItem galleryItem = galleryControl1.Gallery.GetCheckedItem();

                galleryControl1.Gallery.Groups[0].Items.RemoveAt(index);
                galleryControl1.Gallery.Groups[0].Items.Insert(index - 1, galleryItem);
                galleryControl1.Gallery.SetItemCheck(galleryItem, true);

                buttonLeft.Enabled = (index - 1 > 0);
                buttonRight.Enabled = (index - 1 < chapters.Count - 1);
                buttonRemove.Enabled = true;

                Chapter item = chapters[index];
                chapters.RemoveAt(index);
                chapters.Insert(index - 1, item);
            }
        }

        private void buttonMap_Click(object sender, EventArgs e)
        {
            Game game = launcher.GetCurrentGame();
            LegacyFileExplorer form = new LegacyFileExplorer(launcher);
            form.RootDirectory = "maps/";
            form.Filter = "BSP Files (*.bsp)|*.bsp|VPK Files (*.vpk)|*.vpk";
            if (form.ShowDialog() == DialogResult.OK)
            {
                VPK.File file = form.selectedFiles[0];
                if (file.type == ".bsp" && game.EngineID == Engine.SOURCE)
                {
                    // It's a map
                    string mapName = Path.GetFileNameWithoutExtension(file.path);
                    textMap.EditValue = mapName;

                }
                else if (file.type == ".vpk" && game.EngineID == Engine.SOURCE2)
                {
                    // It's a map
                    string mapName = Path.GetFileNameWithoutExtension(file.path);
                    textMap.EditValue = mapName;

                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if(galleryControl1.Gallery.GetCheckedItem() != null)
            {
                int index = galleryControl1.Gallery.Groups[0].Items.IndexOf(galleryControl1.Gallery.GetCheckedItem());

                chapters.RemoveAt(index);
                updateChaptersList();
            }
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if(galleryControl1.Gallery.GetCheckedItem() != null)
            {
                int index = galleryControl1.Gallery.Groups[0].Items.IndexOf(galleryControl1.Gallery.GetCheckedItem());
                DevExpress.XtraBars.Ribbon.GalleryItem galleryItem = galleryControl1.Gallery.GetCheckedItem();

                galleryControl1.Gallery.Groups[0].Items.RemoveAt(index);
                galleryControl1.Gallery.Groups[0].Items.Insert(index + 1, galleryItem);
                galleryControl1.Gallery.SetItemCheck(galleryItem, true);

                buttonLeft.Enabled = (index + 1 > 0);
                buttonRight.Enabled = (index + 1 < chapters.Count - 1);
                buttonRemove.Enabled = true;

                Chapter item = chapters[index];
                chapters.RemoveAt(index);
                chapters.Insert(index + 1, item);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            writeChapters();
            writeChapterTitles();
            writeChapterBackgrounds();
            writeChapterThumbnails();
            writeBackgroundImages();
        }

        private void ChaptersForm_Load(object sender, EventArgs e)
        {
            gamePath = launcher.GetCurrentGame().InstallPath;
            modPath = launcher.GetCurrentMod().InstallPath;
            readChapters();
            readChapterTitles();
            readChapterBackgrounds();
            readChapterThumbnails();
            readBackgroundImages();
            updateChaptersList();

            Directory.CreateDirectory(modPath + "\\maps");
            selectBSPDialog.InitialDirectory = modPath + "\\maps";

            pictureBackgroundWide.ContextMenuStrip = new ContextMenuStrip();
        }

        private void createChapterTitles()
        {
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\resource\\" + modFolder + "_english.txt";

            KeyValue root = new KeyValue("lang");
            root.addChild(new KeyValue("language", "English"));
            KeyValue tokens = new KeyValue("tokens");
            root.addChild(tokens);

            SourceSDK.KeyValue.writeChunkFile(filePath, root, Encoding.Unicode);
        }

        private void galleryControl1_Gallery_ItemCheckedChanged(object sender,
                                                                DevExpress.XtraBars.Ribbon.GalleryItemEventArgs e)
        {
            if(galleryControl1.Gallery.GetCheckedItem() != null)
            {
                int index = galleryControl1.Gallery.Groups[0].Items.IndexOf(galleryControl1.Gallery.GetCheckedItem());
                textMap.EditValue = chapters[index].map;
                textName.EditValue = chapters[index].title;
                textBackground.EditValue = chapters[index].background;
                pictureThumbnail.Image = chapters[index].thumbnail;
                pictureBackground.Image = chapters[index].backgroundImage;
                pictureBackgroundWide.Image = chapters[index].backgroundImageWide;

                textName.Enabled = true;
                buttonMap.Enabled = true;
                buttonBackground.Enabled = true;
                pictureBackgroundWide.Enabled = true;
                pictureThumbnail.Enabled = true;
                pictureBackground.Enabled = true;
                //table.Visible = true;

                buttonLeft.Enabled = (index > 0);
                buttonRight.Enabled = (index < chapters.Count - 1);
                buttonRemove.Enabled = true;
            } else
            {
                //table.Visible = false;
                textName.Enabled = false;
                buttonMap.Enabled = false;
                buttonBackground.Enabled = false;
                pictureBackgroundWide.Enabled = false;
                pictureThumbnail.Enabled = false;
                pictureBackground.Enabled = false;

                buttonLeft.Enabled = false;
                buttonRight.Enabled = false;
                buttonRemove.Enabled = false;
            }
        }

        private void pictureBackground_Click(object sender, EventArgs e)
        {
            int index = galleryControl1.Gallery.Groups[0].Items.IndexOf(galleryControl1.Gallery.GetCheckedItem());

            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = dialog.FileName;


                Bitmap src = Image.FromFile(filePath) as Bitmap;

                if (XtraMessageBox.Show("Do you want to blur the selected image?", "Blur image", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    // Blur image
                    GaussianBlur blur = new GaussianBlur(src);
                    src = blur.Process(30);
                }

                // Crop image
                Bitmap targetWide = new Bitmap(1920, 1080);

                int idealHeight = src.Height;
                int idealWidth = src.Width;

                if(idealHeight / idealWidth > targetWide.Height / targetWide.Width)
                    idealHeight = idealWidth * targetWide.Height / targetWide.Width;
                else
                    idealWidth = idealHeight * targetWide.Width / targetWide.Height;

                using(Graphics gfx = Graphics.FromImage(targetWide))
                    gfx.DrawImage(src,
                                  new Rectangle(0, 0, targetWide.Width, targetWide.Height),
                                  new Rectangle((src.Width - idealWidth) / 2,
                                                (src.Height - idealHeight) / 2,
                                                idealWidth,
                                                idealHeight),
                                  GraphicsUnit.Pixel);

                chapters[index].backgroundImageWide = targetWide;
                pictureBackgroundWide.Image = targetWide;

                Bitmap target = new Bitmap(1024, 768);

                idealHeight = src.Height;
                idealWidth = src.Width;

                if(idealHeight / idealWidth > target.Height / target.Width)
                    idealHeight = idealWidth * target.Height / target.Width;
                else
                    idealWidth = idealHeight * target.Width / target.Height;

                using(Graphics gfx = Graphics.FromImage(target))
                    gfx.DrawImage(src,
                                  new Rectangle(0, 0, target.Width, target.Height),
                                  new Rectangle((src.Width - idealWidth) / 2,
                                                (src.Height - idealHeight) / 2,
                                                idealWidth,
                                                idealHeight),
                                  GraphicsUnit.Pixel);

                chapters[index].backgroundImage = target;
                pictureBackground.Image = target;

                if (launcher.GetCurrentGame().EngineID == Engine.SOURCE)
                {
                    writeBackgroundImage(index);
                    writeBackgroundImageWide(index);
                }
            }
        }

        private void pictureThumbnail_Click(object sender, EventArgs e)
        {
            int index = galleryControl1.Gallery.Groups[0].Items.IndexOf(galleryControl1.Gallery.GetCheckedItem());

            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = dialog.FileName;

                // Crop image
                Bitmap src = Image.FromFile(filePath) as Bitmap;
                Bitmap target = new Bitmap(152, 86);

                int idealHeight = src.Height;
                int idealWidth = src.Width;

                if(idealHeight / idealWidth > target.Height / target.Width)
                    idealHeight = idealWidth * target.Height / target.Width;
                else
                    idealWidth = idealHeight * target.Width / target.Height;

                using(Graphics gfx = Graphics.FromImage(target))
                    gfx.DrawImage(src,
                                  new Rectangle(0, 0, target.Width, target.Height),
                                  new Rectangle((src.Width - idealWidth) / 2,
                                                (src.Height - idealHeight) / 2,
                                                idealWidth,
                                                idealHeight),
                                  GraphicsUnit.Pixel);

                chapters[index].thumbnail = target;
                pictureThumbnail.Image = target;
                galleryControl1.Gallery.GetCheckedItem().ImageOptions.Image = target;

                if (launcher.GetCurrentGame().EngineID == Engine.SOURCE)
                    writeChapterThumbnail(index);
            }
        }

        private void readBackgroundImages()
        {
            string filePath = modPath + "\\materials\\console";

            string vtf2tgaPath = gamePath + "\\bin\\vtf2tga.exe";

            for(int i = 0; i < chapters.Count; i++)
            {
                switch(launcher.GetCurrentGame().EngineID)
                {
                    case Engine.SOURCE:
                        {
                            if (File.Exists(filePath + "\\" + chapters[i].background + ".vtf"))
                            {
                                chapters[i].backgroundImageFile = File.ReadAllBytes(filePath +
                                    "\\" +
                                    chapters[i].background +
                                    ".vtf");
                                Bitmap src = VTF.ToBitmap(chapters[i].backgroundImageFile, launcher);

                                // Crop image
                                if (src == null)
                                    continue;

                                Bitmap target = new Bitmap(1024, 768);
                                using (Graphics gfx = Graphics.FromImage(target))
                                    gfx.DrawImage(src,
                                                  new Rectangle(0, 0, target.Width, target.Height),
                                                  new Rectangle(0, 0, src.Width, src.Height),
                                                  GraphicsUnit.Pixel);

                                chapters[i].backgroundImage = target;
                            }

                            if (File.Exists(filePath + "\\" + chapters[i].background + "_widescreen.vtf"))
                            {
                                chapters[i].backgroundImageWideFile = File.ReadAllBytes(filePath +
                                    "\\" +
                                    chapters[i].background +
                                    "_widescreen.vtf");
                                Bitmap src = VTF.ToBitmap(chapters[i].backgroundImageWideFile, launcher);


                                // Crop image
                                if (src == null)
                                    continue;

                                Bitmap target = new Bitmap(1920, 1080);
                                using (Graphics gfx = Graphics.FromImage(target))
                                    gfx.DrawImage(src,
                                                  new Rectangle(0, 0, target.Width, target.Height),
                                                  new Rectangle(0, 0, src.Width, src.Height),
                                                  GraphicsUnit.Pixel);

                                chapters[i].backgroundImageWide = target;
                            }
                        }
                        break;
                    case Engine.SOURCE2:
                        {
                            if (File.Exists(filePath + "\\" + chapters[i].background + ".png"))
                            {
                                Bitmap src;

                                using (var fs = new System.IO.FileStream(filePath + "\\" + chapters[i].background + ".png", System.IO.FileMode.Open))
                                {
                                    var bmp = new Bitmap(fs);
                                    src = (Bitmap)bmp.Clone();
                                    bmp.Dispose();
                                }

                                // Crop image
                                if (src == null)
                                    continue;

                                Bitmap target = new Bitmap(1024, 768);
                                using (Graphics gfx = Graphics.FromImage(target))
                                    gfx.DrawImage(src,
                                                  new Rectangle(0, 0, target.Width, target.Height),
                                                  new Rectangle(0, 0, src.Width, src.Height),
                                                  GraphicsUnit.Pixel);

                                chapters[i].backgroundImage = target;
                            }

                            if (File.Exists(filePath + "\\" + chapters[i].background + "_widescreen.png"))
                            {
                                Bitmap src;

                                using (var fs = new System.IO.FileStream(filePath + "\\" + chapters[i].background + "_widescreen.png", System.IO.FileMode.Open))
                                {
                                    var bmp = new Bitmap(fs);
                                    src = (Bitmap)bmp.Clone();
                                    bmp.Dispose();
                                }

                                // Crop image
                                if (src == null)
                                    continue;

                                Bitmap target = new Bitmap(1920, 1080);
                                using (Graphics gfx = Graphics.FromImage(target))
                                    gfx.DrawImage(src,
                                                  new Rectangle(0, 0, target.Width, target.Height),
                                                  new Rectangle(0, 0, src.Width, src.Height),
                                                  GraphicsUnit.Pixel);

                                chapters[i].backgroundImageWide = target;
                            }
                        }
                        break;
                }
            }
        }

        private void readChapterBackgrounds()
        {
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\scripts\\chapterbackgrounds.txt";

            if(File.Exists(filePath))
            {
                SourceSDK.KeyValue chapters = SourceSDK.KeyValue.readChunkfile(filePath);
                for(int i = 0; i < this.chapters.Count; i++)
                {
                    string background = chapters.getValue((i + 1).ToString());
                    this.chapters[i].background = background;
                }
            }
        }

        private void readChapters()
        {
            string path = modPath + "\\cfg";

            Directory.CreateDirectory(path);

            List<int> available = new List<int>();

            foreach(string file in Directory.GetFiles(path))
            {
                if(new FileInfo(file).Name.StartsWith("chapter"))
                {
                    int id = int.Parse(new FileInfo(file).Name
                        .Replace("chapter", string.Empty)
                        .Replace(".cfg", string.Empty));
                    available.Add(id);
                }
            }

            available.Sort();

            foreach(int i in available)
            {
                string map = File.ReadAllText(path + "\\chapter" + i + ".cfg").Replace("map ", string.Empty);
                Chapter chapter = new Chapter(launcher.GetCurrentGame().EngineID) { map = map };
                chapters.Add(chapter);
            }
        }

        private void readChapterThumbnails()
        {
            string filePath = modPath + "\\materials\\vgui\\chapters";

            string vtf2tgaPath = gamePath + "\\bin\\vtf2tga.exe";

            for(int i = 0; i < chapters.Count; i++)
            {
                switch(launcher.GetCurrentGame().EngineID)
                {
                    case Engine.SOURCE:
                        {
                            if (File.Exists(filePath + "\\chapter" + (i + 1) + ".vtf"))
                            {
                                chapters[i].thumbnailFile = File.ReadAllBytes(filePath + "\\chapter" + (i + 1) + ".vtf");
                                Bitmap src = VTF.ToBitmap(chapters[i].thumbnailFile, launcher);

                                // Crop image
                                Bitmap target = new Bitmap(152, 86);

                                using (Graphics gfx = Graphics.FromImage(target))
                                    gfx.DrawImage(src,
                                                  new Rectangle(0, 0, target.Width, target.Height),
                                                  new Rectangle(0, 0, target.Width, target.Height),
                                                  GraphicsUnit.Pixel);

                                src.Dispose();

                                chapters[i].thumbnail = target;
                            }
                        }
                        break;
                    case Engine.SOURCE2:
                        {
                            string bitmapPath = filePath + "\\chapter" + (i + 1) + ".png";
                            if (File.Exists(bitmapPath))
                            {
                                Bitmap src;

                                using (var fs = new System.IO.FileStream(bitmapPath, System.IO.FileMode.Open))
                                {
                                    var bmp = new Bitmap(fs);
                                    src = (Bitmap)bmp.Clone();
                                    bmp.Dispose();
                                }

                                // Crop image
                                Bitmap target = new Bitmap(152, 86);

                                using (Graphics gfx = Graphics.FromImage(target))
                                    gfx.DrawImage(src,
                                                  new Rectangle(0, 0, target.Width, target.Height),
                                                  new Rectangle(0, 0, target.Width, target.Height),
                                                  GraphicsUnit.Pixel);

                                chapters[i].thumbnail = target;
                            }
                        }
                        break;
                }
                
            }
        }

        private void readChapterTitles()
        {
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\resource\\" + modFolder + "_english.txt";

            if(File.Exists(filePath))
            {
                lang = SourceSDK.KeyValue.readChunkfile(filePath);
                SourceSDK.KeyValue tokens = lang.getChildByKey("tokens");
                for(int i = 0; i < chapters.Count; i++)
                {
                    string title = tokens.getValue(modFolder + "_chapter" + (i + 1) + "_title");
                    chapters[i].title = title;
                }
            } else
            {
                createChapterTitles();
                readChapterTitles();
            }
        }

        private void textBackground_TextChanged(object sender, EventArgs e)
        {
            int index = galleryControl1.Gallery.Groups[0].Items.IndexOf(galleryControl1.Gallery.GetCheckedItem());
            if(((TextEdit)sender).EditValue != null)
                chapters[index].background = ((TextEdit)sender).EditValue.ToString();
        }

        private void textMap_EditValueChanged(object sender, EventArgs e)
        {
            int index = galleryControl1.Gallery.Groups[0].Items.IndexOf(galleryControl1.Gallery.GetCheckedItem());
            if(((TextEdit)sender).EditValue != null)
                chapters[index].map = ((TextEdit)sender).EditValue.ToString();
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            int index = galleryControl1.Gallery.Groups[0].Items.IndexOf(galleryControl1.Gallery.GetCheckedItem());
            if(((TextEdit)sender).EditValue != null)
            {
                chapters[index].title = ((TextEdit)sender).EditValue.ToString();
                galleryControl1.Gallery.GetCheckedItem().Description = ((TextEdit)sender).EditValue.ToString();
            }
        }

        private void updateChaptersList()
        {
            galleryControl1.Gallery.Groups[0].Items.Clear();
            foreach(Chapter chapter in chapters)
                galleryControl1.Gallery.Groups[0].Items
                    .Add(new DevExpress.XtraBars.Ribbon.GalleryItem(chapter.thumbnail,
                                                                    "Chapter " + (chapters.IndexOf(chapter) + 1),
                                                                    chapter.title));


            if(galleryControl1.Gallery.Groups[0].Items.Count > 0)
            {
                galleryControl1.Gallery.SetItemCheck(galleryControl1.Gallery.Groups[0].Items[0], true);
            } else
            {
                //table.Visible = false;
                textName.Enabled = false;
                buttonMap.Enabled = false;
                buttonBackground.Enabled = false;
                pictureBackgroundWide.Enabled = false;
                pictureBackground.Enabled = false;
                pictureThumbnail.Enabled = false;

                buttonLeft.Enabled = false;
                buttonRight.Enabled = false;
                buttonRemove.Enabled = false;
            }
        }

        private void writeBackgroundImage(int i)
        {
            // Crop image
            Bitmap src = chapters[i].backgroundImage;
            Bitmap target = new Bitmap(1024, 1024);

            using(Graphics gfx = Graphics.FromImage(target))
            {
                gfx.Clear(Color.Black);
                gfx.DrawImage(src,
                              new Rectangle(0, 0, target.Width, target.Height),
                              new Rectangle(0, 0, src.Width, src.Height),
                              GraphicsUnit.Pixel);
            }

            chapters[i].backgroundImageFile = VTF.FromBitmap(target, launcher);
        }

        private void writeBackgroundImages()
        {
            string filePath = modPath + "\\materials\\console";

            for(int i = 0; i < chapters.Count; i++)
            {
                switch(launcher.GetCurrentGame().EngineID)
                {
                    case Engine.SOURCE:
                        {
                            if (chapters[i].backgroundImageFile == null)
                                chapters[i].backgroundImageFile = VTF.FromBitmap(chapters[i].backgroundImage, launcher);

                            if (chapters[i].backgroundImageWideFile == null)
                                chapters[i].backgroundImageWideFile = VTF.FromBitmap(chapters[i].backgroundImageWide, launcher);

                            if (chapters[i].backgroundImageFile != null && chapters[i].backgroundImageWideFile != null)
                            {
                                Directory.CreateDirectory(filePath);

                                File.WriteAllBytes(filePath + "\\" + chapters[i].background + ".vtf",
                                                   chapters[i].backgroundImageFile);
                                File.WriteAllBytes(filePath + "\\" + chapters[i].background + "_widescreen.vtf",
                                                   chapters[i].backgroundImageWideFile);

                                KeyValue root = new KeyValue("UnlitGeneric");
                                root.addChild(new KeyValue("$basetexture", "console/" + chapters[i].background));
                                root.addChild(new KeyValue("$vertexcolor", "1"));
                                root.addChild(new KeyValue("$vertexalpha", "1"));
                                root.addChild(new KeyValue("$ignorez", "1"));
                                root.addChild(new KeyValue("$no_fullbright", "1"));
                                root.addChild(new KeyValue("$nolod", "1"));
                                SourceSDK.KeyValue
                                    .writeChunkFile(filePath + "\\" + chapters[i].background + ".vmt",
                                                    root,
                                                    false,
                                                    new UTF8Encoding(false));

                                root.getChildByKey("$basetexture").setValue("console/" + chapters[i].background + "_widescreen");
                                SourceSDK.KeyValue
                                    .writeChunkFile(filePath + "\\" + chapters[i].background + "_widescreen.vmt",
                                                    root,
                                                    false,
                                                    new UTF8Encoding(false));
                            }
                        }
                        break;
                    case Engine.SOURCE2:
                        {
                            Directory.CreateDirectory(filePath);
                            if (chapters[i].backgroundImage != null) {
                                chapters[i].backgroundImage.Save(filePath + "\\" + chapters[i].background + ".png", ImageFormat.Png);
                            }
                            
                            if (chapters[i].backgroundImageWide != null)
                            {
                                chapters[i].backgroundImageWide.Save(filePath + "\\" + chapters[i].background + "_widescreen.png", ImageFormat.Png);
                            }
                        }
                        break;
                }
                
            }
        }

        private void writeBackgroundImageWide(int i)
        {
            // Crop image
            Bitmap src = chapters[i].backgroundImageWide;
            Bitmap target = new Bitmap(1024, 1024);

            using(Graphics gfx = Graphics.FromImage(target))
            {
                gfx.Clear(Color.Black);
                gfx.DrawImage(src,
                              new Rectangle(0, 0, target.Width, target.Height),
                              new Rectangle(0, 0, src.Width, src.Height),
                              GraphicsUnit.Pixel);
            }

            chapters[i].backgroundImageWideFile = VTF.FromBitmap(target, launcher);
        }

        private void writeChapterBackgrounds()
        {
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\scripts\\chapterbackgrounds.txt";

            KeyValue root = new KeyValue("chapters");

            for(int i = 0; i < chapters.Count; i++)
            {
                if (chapters[i].background == string.Empty)
                {
                    switch(launcher.GetCurrentGame().EngineID)
                    {
                        case Engine.SOURCE:
                            {
                                chapters[i].background = "default";
                            }
                            break;
                        case Engine.SOURCE2:
                            {
                                chapters[i].background = "chapter" + (i + 1);
                            }
                            break;
                    }
                    
                }

                root.addChild(new KeyValue((i + 1).ToString(), chapters[i].background));
            }

            SourceSDK.KeyValue.writeChunkFile(filePath, root, false);
        }

        private void writeChapters()
        {
            string path = modPath + "\\cfg";

            foreach(string file in Directory.GetFiles(path))
            {
                if(new FileInfo(file).Name.StartsWith("chapter"))
                {
                    File.Delete(file);
                }
            }

            for(int i = 0; i < chapters.Count; i++)
            {
                File.WriteAllText(path + "\\chapter" + (i + 1) + ".cfg", "map " + chapters[i].map);
            }
        }

        private void writeChapterThumbnail(int i)
        {
            // Crop image
            Bitmap src = chapters[i].thumbnail;
            Bitmap target = new Bitmap(256, 128);

            using(Graphics gfx = Graphics.FromImage(target))
            {
                gfx.Clear(Color.Black);
                gfx.DrawImage(src,
                              new Rectangle(0, 0, src.Width, src.Height),
                              new Rectangle(0, 0, src.Width, src.Height),
                              GraphicsUnit.Pixel);
            }
            chapters[i].thumbnailFile = VTF.FromBitmap(target, launcher);
        }

        private void writeChapterThumbnails()
        {
            string filePath = modPath + "\\materials\\vgui\\chapters";

            for (int i = 0; i < chapters.Count; i++)
            {
                switch (launcher.GetCurrentGame().EngineID) { 
                    case Engine.SOURCE:
                    {
                        if (chapters[i].thumbnailFile == null)
                            chapters[i].thumbnailFile = VTF.FromBitmap(chapters[i].thumbnail, launcher);

                        if (chapters[i].thumbnailFile != null)
                        {
                            Directory.CreateDirectory(filePath);
                            File.WriteAllBytes(filePath + "\\chapter" + (i + 1) + ".vtf", chapters[i].thumbnailFile);

                            KeyValue root = new KeyValue("UnlitGeneric");
                            root.addChild(new KeyValue("$basetexture", "VGUI/chapters/chapter" + (i + 1)));
                            root.addChild(new KeyValue("$vertexalpha", "1"));

                            SourceSDK.KeyValue
                                .writeChunkFile(filePath + "\\chapter" + (i + 1) + ".vmt", root, false, new UTF8Encoding(false));
                        }
                    }
                    break;
                    case Engine.SOURCE2:
                    {
                        Directory.CreateDirectory(filePath);
                        if (chapters[i].thumbnail != null)
                            chapters[i].thumbnail.Save(filePath + "\\chapter" + (i + 1) + ".png", ImageFormat.Png);
                    }
                    break;
                }
            }
        }

        private void writeChapterTitles()
        {
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\resource\\" + modFolder + "_english.txt";

            for(int i = 0; i < chapters.Count; i++)
                lang.getChildByKey("tokens").setValue(modFolder + "_chapter" + (i + 1) + "_title", chapters[i].title);

            KeyValue.writeChunkFile(filePath, lang, Encoding.Unicode);
        }

        internal class Chapter
        {
            internal string background = string.Empty;
            //internal Bitmap backgroundImage = new Bitmap(4, 4);
            internal Bitmap backgroundImage = null;
            internal byte[] backgroundImageFile = null;
            //internal Bitmap backgroundImageWide = new Bitmap(4, 4);
            internal Bitmap backgroundImageWide = null;
            internal byte[] backgroundImageWideFile = null;
            internal string map = string.Empty;
            //internal Bitmap thumbnail = new Bitmap(256, 128);
            internal Bitmap thumbnail = null;
            internal byte[] thumbnailFile = null;
            internal string title = string.Empty;

            internal Chapter(int engine) {
                if (engine == Engine.SOURCE)
                {
                    backgroundImage = new Bitmap(4, 4);
                    backgroundImageWide = new Bitmap(4, 4);
                    thumbnail = new Bitmap(256, 128);
                }
            }
        }
    }
}