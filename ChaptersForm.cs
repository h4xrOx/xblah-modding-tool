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
using static windows_source1ide.Steam;
using TGASharpLib;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using windows_source1ide.SourceSDK;

namespace windows_source1ide
{
    public partial class ChaptersForm : DevExpress.XtraEditors.XtraForm
    {
        string game;
        string mod;
        Steam sourceSDK;

        SourceSDK.KeyValue lang;

        List<Chapter> chapters = new List<Chapter>();

        public ChaptersForm(string game, string mod)
        {
            this.game = game;
            this.mod = mod; 
            InitializeComponent();
        }

        class Chapter
        {
            internal string map = "";
            internal string title = "";
            internal string background = "";
            internal Bitmap thumbnail = new Bitmap(152,86);
            internal byte[] thumbnailFile = null;
            internal Bitmap backgroundImageWide = new Bitmap(1920, 1080);
            internal Bitmap backgroundImage = new Bitmap(1024,768);
            internal byte[] backgroundImageFile = null;
            internal byte[] backgroundImageWideFile = null;
        }

        private void ChaptersForm_Load(object sender, EventArgs e)
        {
            sourceSDK = new Steam();

            readChapters();
            readChapterTitles();
            readChapterBackgrounds();
            readChapterThumbnails();
            readBackgroundImages();
            updateChaptersList();
        }

        private void updateChaptersList()
        {
            list.BeginUnboundLoad();
            list.Nodes.Clear();
            foreach(Chapter chapter in chapters)
            {
                list.AppendNode(new object[] { "Chapter " + (chapters.IndexOf(chapter) + 1) + " - " + chapter.title }, null);
            }
            list.EndUnboundLoad();
        }

        private void list_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null)
            {
                textMap.EditValue = chapters[e.Node.Id].map;
                textName.EditValue = chapters[e.Node.Id].title;
                textBackground.EditValue = chapters[e.Node.Id].background;
                pictureThumbnail.Image = chapters[e.Node.Id].thumbnail;
                pictureBackground.Image = chapters[e.Node.Id].backgroundImage;
                pictureBackgroundWide.Image = chapters[e.Node.Id].backgroundImageWide;
            } else
            {
                textMap.EditValue = "";
                textName.EditValue = "";
                textBackground.EditValue = "";
                pictureThumbnail.Image  = null;
                pictureBackground.Image = null;
                pictureBackgroundWide.Image = null;
            }

        }

        private void readChapters()
        {
            string path = sourceSDK.GetMods(game)[mod] + "\\cfg";

            Directory.CreateDirectory(path);
            foreach (string file in Directory.GetFiles(path))
            {
                if (new FileInfo(file).Name.StartsWith("chapter"))
                {
                    string map = File.ReadAllText(file).Replace("map ", "");
                    Chapter chapter = new Chapter()
                    {
                        map = map
                    };
                    chapters.Add(chapter);
                }
            }
        }

        private void writeChapters()
        {
            string path = sourceSDK.GetMods(game)[mod] + "\\cfg";

            foreach (string file in Directory.GetFiles(path))
            {
                if (new FileInfo(file).Name.StartsWith("chapter"))
                {
                    File.Delete(file);
                }
            }

            for(int i = 0; i < chapters.Count; i++)
            {
                File.WriteAllText(path + "\\chapter" + (i + 1) + ".cfg", "map " + chapters[i].map);
            }
        }

        private void readChapterTitles()
        {
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\resource\\" + modFolder + "_english.txt";

            if (File.Exists(filePath))
            {
                lang = SourceSDK.KeyValue.readChunkfile(filePath);
                SourceSDK.KeyValue tokens = lang.getChild("tokens");
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

        private void writeChapterTitles()
        {
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\resource\\" + modFolder + "_english.txt";

            for (int i = 0; i < chapters.Count; i++)
                lang.getChild("tokens").setValue(modFolder + "_chapter" + (i + 1) + "_title", chapters[i].title);

            SourceSDK.KeyValue.writeChunkFile(filePath, lang, Encoding.Unicode);
        }

        private void createChapterTitles()
        {
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\resource\\" + modFolder + "_english.txt";

            KeyValue root = new KeyValue("lang");
            root.addChild(new KeyValue("language", "English"));
            KeyValue tokens = new KeyValue("tokens");
            root.addChild(tokens);

            SourceSDK.KeyValue.writeChunkFile(filePath, root, Encoding.Unicode);
        }

        private void readChapterBackgrounds()
        {
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\scripts\\chapterbackgrounds.txt";

            if (File.Exists(filePath))
            {
                SourceSDK.KeyValue chapters = SourceSDK.KeyValue.readChunkfile(filePath);
                for (int i = 0; i < this.chapters.Count; i++)
                {
                    string background = chapters.getChild((i + 1).ToString()).getValue();
                    this.chapters[i].background = background;
                }
            }
        }

        private void writeChapterBackgrounds()
        {
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\scripts\\chapterbackgrounds.txt";

            KeyValue root = new KeyValue("chapters");

            for (int i = 0; i < chapters.Count; i++)
            {
                root.addChild(new KeyValue((i + 1).ToString(), chapters[i].background));
            }

            SourceSDK.KeyValue.writeChunkFile(filePath, root, false);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            writeChapters();
            writeChapterTitles();
            writeChapterBackgrounds();
            writeChapterThumbnails();
            writeBackgroundImages();
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            if (list.FocusedNode != null && ((TextEdit)sender).EditValue != null)
                chapters[list.FocusedNode.Id].title = ((TextEdit)sender).EditValue.ToString();
        }

        private void textMap_EditValueChanged(object sender, EventArgs e)
        {
            if (list.FocusedNode != null && ((TextEdit)sender).EditValue != null)
                chapters[list.FocusedNode.Id].map = ((TextEdit)sender).EditValue.ToString();
        }

        private void textBackground_TextChanged(object sender, EventArgs e)
        {
            if (list.FocusedNode != null && ((TextEdit)sender).EditValue != null)
                chapters[list.FocusedNode.Id].background = ((TextEdit)sender).EditValue.ToString();
        }

        private void pictureThumbnail_Click(object sender, EventArgs e)
        {
            if (list.FocusedNode == null)
                return;

            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = dialog.FileName;

                // Crop image
                Bitmap src = Image.FromFile(filePath) as Bitmap;
                Bitmap target = new Bitmap(152, 86);

                int idealHeight = src.Height;
                int idealWidth = src.Width;

                if (idealHeight / idealWidth > target.Height / target.Width)
                {
                    idealHeight = idealWidth * target.Height / target.Width;
                } else
                {
                    idealWidth = idealHeight * target.Width / target.Height;
                }

                using (Graphics gfx = Graphics.FromImage(target))
                {
                    gfx.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), new Rectangle((src.Width - idealWidth) / 2, (src.Height - idealHeight) / 2, idealWidth, idealHeight), GraphicsUnit.Pixel);
                }

                chapters[list.FocusedNode.Id].thumbnail = target;
                pictureThumbnail.Image = target;

                writeChapterThumbnail(list.FocusedNode.Id);
            }
        }

        private void writeChapterThumbnail(int i)
        {
            string gamePath = sourceSDK.GetGames()[game];
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\materialsrc\\vgui\\chapters";

            string vtexPath = gamePath + "\\bin\\vtex.exe";

            // Crop image
            Bitmap src = chapters[i].thumbnail;
            Bitmap target = new Bitmap(256, 128);

            using (Graphics gfx = Graphics.FromImage(target))
            {
                gfx.Clear(Color.Black);
                gfx.DrawImage(src, new Rectangle(0, 0, src.Width, src.Height), new Rectangle(0, 0, src.Width, src.Height), GraphicsUnit.Pixel);
            }

            var tga = new TGA(target);
            Directory.CreateDirectory(filePath);
            tga.Save(filePath + "\\temp.tga");
            File.WriteAllText(filePath + "\\temp.txt", "nolod 1\r\nnomip 1");

            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = vtexPath;
            ffmpeg.StartInfo.Arguments = "-mkdir -quiet -nopause -shader UnlitGeneric temp.tga";
            ffmpeg.StartInfo.WorkingDirectory = filePath;
            ffmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            ffmpeg.Start();
            ffmpeg.WaitForExit();

            chapters[i].thumbnailFile = File.ReadAllBytes(modPath + "\\materials\\vgui\\chapters\\temp.vtf");

            File.Delete(filePath + "\\temp.tga");
            File.Delete(filePath + "\\temp.txt");
            File.Delete(modPath + "\\materials\\vgui\\chapters\\temp.vtf");
            File.Delete(modPath + "\\materials\\vgui\\chapters\\temp.vmt");
        }

        private void writeChapterThumbnails()
        {
            string gamePath = sourceSDK.GetGames()[game];
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\materials\\vgui\\chapters";

            for (int i = 0; i < chapters.Count; i++)
            {
                if (chapters[i].thumbnailFile != null)
                {
                    File.WriteAllBytes(filePath + "\\chapter" + (i + 1) + ".vtf", chapters[i].thumbnailFile);

                    KeyValue root = new KeyValue("UnlitGeneric");
                    root.addChild(new KeyValue("$basetexture", "VGUI/chapters/chapter" + (i + 1)));
                    root.addChild(new KeyValue("$vertexalpha", "1"));

                    SourceSDK.KeyValue.writeChunkFile(filePath + "\\chapter" + (i + 1) + ".vmt", root, false, new UTF8Encoding(false));
                }
            }
        }

        private void readChapterThumbnails()
        {
            string gamePath = sourceSDK.GetGames()[game];
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\materials\\vgui\\chapters";

            string vtf2tgaPath = gamePath + "\\bin\\vtf2tga.exe";

            for (int i = 0; i < chapters.Count; i++)
            {
                if (File.Exists(filePath + "\\chapter" + (i + 1) + ".vtf"))
                {
                    chapters[i].thumbnailFile = File.ReadAllBytes(filePath + "\\chapter" + (i + 1) + ".vtf");
                    Process ffmpeg = new Process();
                    ffmpeg.StartInfo.FileName = vtf2tgaPath;
                    ffmpeg.StartInfo.Arguments = "-i chapter" + (i + 1) + " -o chapter" + (i + 1) + "";
                    ffmpeg.StartInfo.WorkingDirectory = filePath;
                    ffmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    ffmpeg.Start();
                    ffmpeg.WaitForExit();

                    // Crop image
                    Bitmap src = TGA.FromFile(filePath + "\\chapter" + (i + 1) + ".tga").ToBitmap();
                    Bitmap target = new Bitmap(152, 86);

                    using (Graphics gfx = Graphics.FromImage(target))
                    {
                        gfx.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0, 0, target.Width, target.Height), GraphicsUnit.Pixel);
                    }

                    chapters[i].thumbnail = target;
   
                    File.Delete(filePath + "\\chapter" + (i + 1) + ".tga");
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            chapters.Add(new Chapter());
            updateChaptersList();
        }

        private void pictureBackground_Click(object sender, EventArgs e)
        {
            if (list.FocusedNode == null)
                return;

            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = dialog.FileName;

                // Crop image
                Bitmap src = Image.FromFile(filePath) as Bitmap;
                Bitmap targetWide = new Bitmap(1920, 1080);

                int idealHeight = src.Height;
                int idealWidth = src.Width;

                if (idealHeight / idealWidth > targetWide.Height / targetWide.Width)
                {
                    idealHeight = idealWidth * targetWide.Height / targetWide.Width;
                }
                else
                {
                    idealWidth = idealHeight * targetWide.Width / targetWide.Height;
                }

                using (Graphics gfx = Graphics.FromImage(targetWide))
                {
                    gfx.DrawImage(src, new Rectangle(0, 0, targetWide.Width, targetWide.Height), new Rectangle((src.Width - idealWidth) / 2, (src.Height - idealHeight) / 2, idealWidth, idealHeight), GraphicsUnit.Pixel);
                }

                chapters[list.FocusedNode.Id].backgroundImageWide = targetWide;
                pictureBackgroundWide.Image = targetWide;


                Bitmap target = new Bitmap(1024, 768);

                idealHeight = src.Height;
                idealWidth = src.Width;

                if (idealHeight / idealWidth > target.Height / target.Width)
                {
                    idealHeight = idealWidth * target.Height / target.Width;
                }
                else
                {
                    idealWidth = idealHeight * target.Width / target.Height;
                }

                using (Graphics gfx = Graphics.FromImage(target))
                {
                    gfx.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), new Rectangle((src.Width - idealWidth) / 2, (src.Height - idealHeight) / 2, idealWidth, idealHeight), GraphicsUnit.Pixel);
                }

                chapters[list.FocusedNode.Id].backgroundImage = target;
                pictureBackground.Image = target;

                writeBackgroundImage(list.FocusedNode.Id);
                writeBackgroundImageWide(list.FocusedNode.Id);
            }
        }

        private void writeBackgroundImage(int i)
        {
            string gamePath = sourceSDK.GetGames()[game];
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\materialsrc\\console";

            string vtexPath = gamePath + "\\bin\\vtex.exe";

            // Crop image
            Bitmap src = chapters[i].backgroundImage;
            Bitmap target = new Bitmap(1024, 1024);

            using (Graphics gfx = Graphics.FromImage(target))
            {
                gfx.Clear(Color.Black);
                gfx.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0, 0, src.Width, src.Height), GraphicsUnit.Pixel);
            }

            var tga = new TGA(target);
            Directory.CreateDirectory(filePath);
            tga.Save(filePath + "\\temp.tga");
            File.WriteAllText(filePath + "\\temp.txt", "nolod 1\r\nnomip 1");

            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = vtexPath;
            ffmpeg.StartInfo.Arguments = "-mkdir -quiet -nopause -shader UnlitGeneric temp.tga";
            ffmpeg.StartInfo.WorkingDirectory = filePath;
            ffmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            ffmpeg.Start();
            ffmpeg.WaitForExit();

            chapters[i].backgroundImageFile = File.ReadAllBytes(modPath + "\\materials\\console\\temp.vtf");

            File.Delete(filePath + "\\temp.tga");
            File.Delete(filePath + "\\temp.txt");
            File.Delete(modPath + "\\materials\\console\\temp.vtf");
            File.Delete(modPath + "\\materials\\console\\temp.vmt");
        }

        private void writeBackgroundImageWide(int i)
        {
            string gamePath = sourceSDK.GetGames()[game];
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\materialsrc\\console";

            string vtexPath = gamePath + "\\bin\\vtex.exe";

            // Crop image
            Bitmap src = chapters[i].backgroundImageWide;
            Bitmap target = new Bitmap(1024, 1024);

            using (Graphics gfx = Graphics.FromImage(target))
            {
                gfx.Clear(Color.Black);
                gfx.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0, 0, src.Width, src.Height), GraphicsUnit.Pixel);
            }

            var tga = new TGA(target);
            Directory.CreateDirectory(filePath);
            tga.Save(filePath + "\\temp.tga");
            File.WriteAllText(filePath + "\\temp.txt", "nolod 1\r\nnomip 1");

            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = vtexPath;
            ffmpeg.StartInfo.Arguments = "-mkdir -quiet -nopause -shader UnlitGeneric temp.tga";
            ffmpeg.StartInfo.WorkingDirectory = filePath;
            ffmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            ffmpeg.Start();
            ffmpeg.WaitForExit();

            chapters[i].backgroundImageWideFile = File.ReadAllBytes(modPath + "\\materials\\console\\temp.vtf");

            File.Delete(filePath + "\\temp.tga");
            File.Delete(filePath + "\\temp.txt");
            File.Delete(modPath + "\\materials\\console\\temp.vtf");
            File.Delete(modPath + "\\materials\\console\\temp.vmt");
        }

        private void writeBackgroundImages()
        {
            string gamePath = sourceSDK.GetGames()[game];
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\materials\\console";

            for (int i = 0; i < chapters.Count; i++)
            {
                if (chapters[i].backgroundImageFile != null && chapters[i].backgroundImageWideFile != null)
                {
                    File.WriteAllBytes(filePath + "\\" + chapters[i].background + ".vtf", chapters[i].backgroundImageFile);
                    File.WriteAllBytes(filePath + "\\" + chapters[i].background + "_widescreen.vtf", chapters[i].backgroundImageWideFile);

                    KeyValue root = new KeyValue("UnlitGeneric");
                    root.addChild(new KeyValue("$basetexture", "console/" + chapters[i].background));
                    root.addChild(new KeyValue("$vertexcolor", "1"));
                    root.addChild(new KeyValue("$vertexalpha", "1"));
                    root.addChild(new KeyValue("$ignorez", "1"));
                    root.addChild(new KeyValue("$no_fullbright", "1"));
                    root.addChild(new KeyValue("$nolod", "1"));

                    SourceSDK.KeyValue.writeChunkFile(filePath + "\\" + chapters[i].background + ".vmt", root, false, new UTF8Encoding(false));

                    root.getChild("$basetexture").setValue("console/" + chapters[i].background + "_widescreen");

                    SourceSDK.KeyValue.writeChunkFile(filePath + "\\" + chapters[i].background + "_widescreen.vmt", root, false, new UTF8Encoding(false));
                }
            }
        }

        private void readBackgroundImages()
        {
            string gamePath = sourceSDK.GetGames()[game];
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\materials\\console";

            string vtf2tgaPath = gamePath + "\\bin\\vtf2tga.exe";

            for (int i = 0; i < chapters.Count; i++)
            {
                if (File.Exists(filePath + "\\" + chapters[i].background + ".vtf"))
                {
                    chapters[i].backgroundImageFile = File.ReadAllBytes(filePath + "\\" + chapters[i].background + ".vtf");
                    Process ffmpeg = new Process();
                    ffmpeg.StartInfo.FileName = vtf2tgaPath;
                    ffmpeg.StartInfo.Arguments = "-i " + chapters[i].background + " -o " + chapters[i].background + "";
                    ffmpeg.StartInfo.WorkingDirectory = filePath;
                    ffmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    ffmpeg.Start();
                    ffmpeg.WaitForExit();

                    // Crop image
                    if (!File.Exists(filePath + "\\" + chapters[i].background + ".tga"))
                        continue;

                    Bitmap src = TGA.FromFile(filePath + "\\" + chapters[i].background + ".tga").ToBitmap();
                    Bitmap target = new Bitmap(1024, 768);

                    using (Graphics gfx = Graphics.FromImage(target))
                    {
                        gfx.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0, 0, src.Width, src.Height), GraphicsUnit.Pixel);
                    }

                    chapters[i].backgroundImage = target;

                    File.Delete(filePath + "\\" + chapters[i].background + ".tga");
                }

                if (File.Exists(filePath + "\\" + chapters[i].background + "_widescreen.vtf"))
                {
                    chapters[i].backgroundImageWideFile = File.ReadAllBytes(filePath + "\\" + chapters[i].background + "_widescreen.vtf");
                    Process ffmpeg = new Process();
                    ffmpeg.StartInfo.FileName = vtf2tgaPath;
                    ffmpeg.StartInfo.Arguments = "-i " + chapters[i].background + "_widescreen -o " + chapters[i].background + "_widescreen";
                    ffmpeg.StartInfo.WorkingDirectory = filePath;
                    ffmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    ffmpeg.Start();
                    ffmpeg.WaitForExit();

                    // Crop image
                    if (!File.Exists(filePath + "\\" + chapters[i].background + "_widescreen.tga"))
                        continue;

                    Bitmap src = TGA.FromFile(filePath + "\\" + chapters[i].background + "_widescreen.tga").ToBitmap();
                    Bitmap target = new Bitmap(1920, 1080);

                    using (Graphics gfx = Graphics.FromImage(target))
                    {
                        gfx.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), new Rectangle(0, 0, src.Width, src.Height), GraphicsUnit.Pixel);
                    }

                    chapters[i].backgroundImageWide = target;

                    File.Delete(filePath + "\\" + chapters[i].background + "_widescreen.tga");
                }
            }
        }

        private void buttonMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textMap.EditValue = Path.GetFileNameWithoutExtension(dialog.FileName);
            }
        }

        private void buttonBackground_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBackground.EditValue = Path.GetFileNameWithoutExtension(dialog.FileName);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (list.FocusedNode == null)
                return; 
            chapters.RemoveAt(list.FocusedNode.Id);
            updateChaptersList();
        }
    }
}