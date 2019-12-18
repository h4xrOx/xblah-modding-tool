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
using static windows_source1ide.SourceSDK;
using TGASharpLib;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace windows_source1ide
{
    public partial class ChaptersForm : DevExpress.XtraEditors.XtraForm
    {
        string game;
        string mod;
        SourceSDK sourceSDK;

        KeyVal<string, string> lang;

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
            sourceSDK = new SourceSDK();

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
                lang = readChunkfile(filePath);
                KeyVal<string, string> tokens = lang.GetChild("tokens");
                for(int i = 0; i < chapters.Count; i++)
                {
                    string title = tokens.GetChild(modFolder + "_chapter" + (i + 1) + "_title").Value;
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
            {
                lang.GetChild("tokens").SetChild(modFolder + "_chapter" + (i + 1) + "_title", chapters[i].title);
            }

            writeChunkFile(filePath, "lang", lang, Encoding.Unicode);
        }

        private void createChapterTitles()
        {
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\resource\\" + modFolder + "_english.txt";

            KeyVal<string, string> root = new KeyVal<string, string>();
            root.Children = new Dictionary<string, KeyVal<string, string>>();
            root.Children.Add("language", new KeyVal<string, string>("English"));
            KeyVal<string, string> tokens = new KeyVal<string, string>();
            tokens.Children = new Dictionary<string, KeyVal<string, string>>();
            root.Children.Add("tokens", tokens);

            writeChunkFile(filePath, "lang", root, Encoding.Unicode);
        }

        private void readChapterBackgrounds()
        {
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\scripts\\chapterbackgrounds.txt";

            if (File.Exists(filePath))
            {
                KeyVal<string, string> chapters = readChunkfile(filePath);
                for (int i = 0; i < this.chapters.Count; i++)
                {
                    string background = chapters.GetChild((i + 1).ToString()).Value;
                    this.chapters[i].background = background;
                }
            }
        }

        private void writeChapterBackgrounds()
        {
            string modPath = sourceSDK.GetMods(game)[mod];
            string modFolder = new DirectoryInfo(modPath).Name;
            string filePath = modPath + "\\scripts\\chapterbackgrounds.txt";

            KeyVal<string, string> root = new KeyVal<string, string>();
            root.Children = new Dictionary<string, KeyVal<string, string>>();

            for (int i = 0; i < chapters.Count; i++)
            {
                root.SetChild((i + 1).ToString(), chapters[i].background);
            }

            writeChunkFile(filePath, "chapters", root, false);
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

                    KeyVal<string, string> root = new KeyVal<string, string>();
                    root.Children = new Dictionary<string, KeyVal<string, string>>();
                    root.Children.Add("$basetexture", new KeyVal<string, string>("VGUI/chapters/chapter" + (i + 1)));
                    root.Children.Add("$vertexalpha", new KeyVal<string, string>("1"));

                    writeChunkFile(filePath + "\\chapter" + (i + 1) + ".vmt", "UnlitGeneric", root, false, new UTF8Encoding(false));
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

                    KeyVal<string, string> root = new KeyVal<string, string>();
                    root.Children = new Dictionary<string, KeyVal<string, string>>();
                    root.Children.Add("$basetexture", new KeyVal<string, string>("console/" + chapters[i].background));
                    root.Children.Add("$vertexcolor", new KeyVal<string, string>("1"));
                    root.Children.Add("$vertexalpha", new KeyVal<string, string>("1"));
                    root.Children.Add("$ignorez", new KeyVal<string, string>("1"));
                    root.Children.Add("$no_fullbright", new KeyVal<string, string>("1"));
                    root.Children.Add("$nolod", new KeyVal<string, string>("1"));

                    writeChunkFile(filePath + "\\" + chapters[i].background + ".vmt", "UnlitGeneric", root, false, new UTF8Encoding(false));

                    root.Children = new Dictionary<string, KeyVal<string, string>>();
                    root.SetChild("$basetexture", "console/" + chapters[i].background + "_widescreen");

                    writeChunkFile(filePath + "\\" + chapters[i].background + "_widescreen.vmt", "UnlitGeneric", root, false, new UTF8Encoding(false));
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
            chapters.RemoveAt(list.FocusedNode.Id);
            updateChaptersList();
        }
    }
}