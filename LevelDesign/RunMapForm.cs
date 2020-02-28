using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using windows_source1ide.Properties;

namespace SourceModdingTool
{
    public partial class RunMapForm : DevExpress.XtraEditors.XtraForm
    {
        List<string> dirs = new List<string>();
        string modPath;

        MapFolder root;
        Steam sourceSDK;
        string bspPath;
        string vmfPath;

        public string fileName;

        public RunMapForm(Steam sourceSDK)
        {
            InitializeComponent();

            this.sourceSDK = sourceSDK;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            foreach(var file in new DirectoryInfo(sourceSDK.GetModPath() + "\\maps\\").EnumerateFiles(bspPath + ".*"))
                file.Delete();
            LoadMaps();
        }

        private void GalleryControl_Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            GalleryItem checkedItem = galleryControl.Gallery.GetCheckedItem();

            if (checkedItem != null && galleryControl.Gallery.GetCheckedItems().Count > 0)
                if (checkedItem.Tag is KeyValuePair<string, MapFolder>)
                {
                    KeyValuePair<string, MapFolder> mapFolder = (KeyValuePair<string, MapFolder>)checkedItem.Tag;

                    dirs.Add(mapFolder.Key);
                    string path = string.Join("/", dirs);
                    ShowMaps(path);
                }
                else
                {
                    Map map = (Map)checkedItem.Tag;
                    versionsCombo.Properties.Items.Clear();
                    versionsCombo.Tag = map;

                    foreach (KeyValuePair<string, Map.Version> version in map.versions
                        .OrderByDescending(f => f.Value.lastUpdateDate)
                        .ToList())
                        versionsCombo.Properties.Items.Add(version.Key);

                    versionsCombo.EditValue = versionsCombo.Properties.Items[0];
                }
            else
            {
                bspPath = null;
                vmfPath = null;
                runButton.Enabled = false;
                editButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        private void LoadMaps()
        {
            string modPath = sourceSDK.GetModPath();

            List<Map> maps = Map.LoadMaps(sourceSDK).OrderByDescending(i => i.GetLastUpdate()).ToList();
            root = Map.GetMapsByFolder(maps);

            ShowMaps(string.Empty);
        }

        private void RunMapForm_Load(object sender, EventArgs e)
        {
            modPath = sourceSDK.GetModPath();

            string gameinfoPath = modPath + "\\gameinfo.txt";
            string instancePath = SourceSDK.KeyValue.readChunkfile(gameinfoPath).getValue("instancepath");

            LoadMaps();
        }

        private void ShowMaps(string path)
        {
            path = path.Replace("\\", "/");

            string[] dirs = path.Split('/');
            MapFolder subdir = root;

            for(int i = 0; i < dirs.Length; i++)
            {
                string dir = dirs[i];

                if(dir == string.Empty)
                    break;

                if(!subdir.subdirs.ContainsKey(dir))   // Error. dir not found
                    Debugger.Break();
                subdir = subdir.subdirs[dir];
            }

            galleryControl.Gallery.Groups[0].Items.Clear();

            // Create gallery map folders
            foreach(KeyValuePair<string, MapFolder> map in subdir.subdirs)
            {
                string mapName = map.Key;

                Image bitmap = Resources.folder;

                string mapDescription = string.Empty;
                GalleryItem item = new GalleryItem(bitmap, mapName, mapDescription);
                item.Tag = map;
                galleryControl.Gallery.Groups[0].Items.Add(item);
            }

            // Create gallery map items
            foreach(Map map in subdir.maps)
            {
                string mapName = map.name;

                string screenshotFilter = Path.GetFileName(mapName) + "*.jpg";
                List<string> screenshots = new DirectoryInfo(modPath + "\\screenshots\\").GetFiles(screenshotFilter)
                    .OrderByDescending(f => f.LastWriteTime)
                    .Select(f => f.Name)
                    .ToList();

                Image bitmap = Resources.noScreenshots;
                foreach(string screenshot in screenshots)
                {
                    int i;
                    if(int.TryParse(screenshot.Replace(Path.GetFileName(mapName), string.Empty)
                        .Replace(".jpg", string.Empty),
                                    out i))
                    {
                        bitmap = Bitmap.FromFile(modPath + "\\screenshots\\" + screenshot);

                        break;
                    }
                }

                string mapDescription = map.versions.Last().Value.lastUpdateDate.ToString("dd/MM/yy HH:mm:ss");
                GalleryItem item = new GalleryItem(bitmap, mapName, mapDescription);
                item.Tag = map;
                galleryControl.Gallery.Groups[0].Items.Add(item);
            }

            // Show path 
            string instancePath = new GameInfo(sourceSDK).getValue("instancepath");
            pathEdit.EditValue = instancePath + "\\" + string.Join("\\", dirs);
        }

        private void ToolsUpButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(dirs.Count > 0)
            {
                dirs.Remove(dirs.Last());

                string path = string.Join("/", dirs);
                ShowMaps(path);
            }
        }

        private void VersionsCombo_EditValueChanged(object sender, EventArgs e)
        {
            if(versionsCombo.Tag == null)
            {
                bspPath = null;
                vmfPath = null;
                runButton.Enabled = false;
                editButton.Enabled = false;
                deleteButton.Enabled = false;
            } else
            {
                Map map = (Map)versionsCombo.Tag;
                string selectedVersion = versionsCombo.EditValue.ToString();
                Map.Version version = map.versions[selectedVersion];
                vmfPath = version.vmfPath;
                string bspPath = vmfPath.Substring(0, vmfPath.Length - 3) + "bsp";
                if(File.Exists(bspPath))
                    this.bspPath = bspPath;
                else
                    this.bspPath = string.Empty;

                if(!File.Exists(vmfPath))
                    vmfPath = string.Empty;

                runButton.Enabled = (this.bspPath != string.Empty);
                editButton.Enabled = (vmfPath != string.Empty);
                deleteButton.Enabled = true;
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            this.fileName = bspPath;
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.fileName = vmfPath;
        }
    }
}