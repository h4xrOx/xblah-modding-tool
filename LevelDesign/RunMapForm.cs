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
using DevExpress.XtraBars.Ribbon;
using windows_source1ide.Properties;

namespace SourceModdingTool
{
    public partial class RunMapForm : DevExpress.XtraEditors.XtraForm
    {
        Steam sourceSDK;
        string modPath;
        public string fileName;

        public RunMapForm(Steam sourceSDK)
        {
            InitializeComponent();

            this.sourceSDK = sourceSDK;
        }

        private void loadMaps()
        {
            string modPath = sourceSDK.GetModPath();

            galleryControl.Gallery.Groups[0].Items.Clear();
            foreach (string file in new DirectoryInfo(modPath + "\\maps\\").GetFiles("*.bsp")
                        .OrderByDescending(f => f.LastWriteTime)
                        .Select(f => f.Name)
                        .ToList())
            {
                string mapName = Path.GetFileNameWithoutExtension(file);

                List<string> screenshots = new DirectoryInfo(modPath + "\\screenshots\\").GetFiles(mapName + "*.jpg")
                        .OrderByDescending(f => f.LastWriteTime)
                        .Select(f => f.Name)
                        .ToList();

                Image bitmap = Resources.noScreenshots;
                foreach (string screenshot in screenshots)
                {
                    int i;
                    if (int.TryParse(screenshot.Replace(mapName, "").Replace(".jpg", ""), out i))
                    {
                        bitmap = Bitmap.FromFile(modPath + "\\screenshots\\" + screenshot);
                        break;
                    }
                }

                string mapDescription = File.GetLastWriteTime(modPath + "\\maps\\" + file).ToString("dd/MM/yy HH:mm:ss");
                galleryControl.Gallery.Groups[0].Items.Add(new GalleryItem(bitmap, mapName, mapDescription));
            }
        }

        private void RunMapForm_Load(object sender, EventArgs e)
        {
            modPath = sourceSDK.GetModPath();

            string gameinfoPath = modPath + "\\gameinfo.txt";
            string instancePath = SourceSDK.KeyValue.readChunkfile(gameinfoPath).getValue("instancepath");
            XtraMessageBox.Show(instancePath);

            loadMaps();
        }

        private void galleryControl1_Gallery_ItemCheckedChanged(object sender, GalleryItemEventArgs e)
        {
            if (e.Item != null && galleryControl.Gallery.GetCheckedItems().Count > 0)
            {
                fileName = e.Item.Caption;
                runButton.Enabled = true;
                deleteButton.Enabled = true;
            } else
            {
                fileName = null;
                runButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var dir = new DirectoryInfo(sourceSDK.GetModPath() + "\\maps\\");

            foreach (var file in dir.EnumerateFiles(fileName + ".*"))
            {
                file.Delete();
            }
            loadMaps();
        }
    }
}