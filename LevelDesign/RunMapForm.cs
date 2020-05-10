using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using source_modding_tool.Properties;

namespace source_modding_tool
{
    public partial class RunMapForm : DevExpress.XtraEditors.XtraForm
    {
        List<string> dirs = new List<string>();
        string modPath;

        MapFolder root;
        Launcher launcher;

        string compiledPath;
        string sourcePath;

        public string fileName;
        public int runMode;

        public RunMapForm(Launcher launcher)
        {
            InitializeComponent();

            this.launcher = launcher;
        }

        private void RunMapForm_Load(object sender, EventArgs e)
        {
            modPath = launcher.GetCurrentMod().installPath;
            LoadMaps();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            foreach(var file in new DirectoryInfo(launcher.GetCurrentMod().installPath + "\\maps\\").EnumerateFiles(compiledPath + ".*"))
                file.Delete();
            LoadMaps();
        }

        private void GalleryControl_Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {

            GalleryItem checkedItem = galleryControl.Gallery.GetCheckedItem();

            if (checkedItem != null && galleryControl.Gallery.GetCheckedItems().Count > 0)
                if (checkedItem.Tag is KeyValuePair<string, MapFolder>)
                {
                    // Folder

                }
                else
                {
                    // File
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
                // Nothing
                compiledPath = null;
                sourcePath = null;
                runButton.Enabled = false;
                editButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        private void galleryControl_Gallery_ItemDoubleClick(object sender, GalleryItemClickEventArgs e)
        {
            GalleryItem checkedItem = galleryControl.Gallery.GetCheckedItem();

            if (checkedItem != null && galleryControl.Gallery.GetCheckedItems().Count > 0)
                if (checkedItem.Tag is KeyValuePair<string, MapFolder>)
                {
                    // Folder
                    KeyValuePair<string, MapFolder> mapFolder = (KeyValuePair<string, MapFolder>)checkedItem.Tag;

                    dirs.Add(mapFolder.Key);
                    string path = string.Join("/", dirs);
                    ShowMaps(path);
                }
                else
                {
                    // File
                   
                }
            else
            {
                // Nothing
                compiledPath = null;
                sourcePath = null;
                runButton.Enabled = false;
                editButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        private void LoadMaps()
        {
            string modPath = launcher.GetCurrentMod().installPath;

            string mapsPath = modPath + "\\maps\\";

            List<Map> maps = Map.LoadMaps(mapsPath).OrderByDescending(i => i.GetLastUpdate()).ToList();
            root = Map.GetMapsByFolder(maps);

            ShowMaps(string.Empty);
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
                List<string> screenshots = new List<string>();

                if (Directory.Exists(modPath + "\\screenshots\\"))
                    screenshots = new DirectoryInfo(modPath + "\\screenshots\\").GetFiles(screenshotFilter)
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
            string instancePath = new GameInfo(launcher).getValue("instancepath");
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
                compiledPath = null;
                sourcePath = null;
                runButton.Enabled = false;
                editButton.Enabled = false;
                deleteButton.Enabled = false;
            } else
            {
                Map map = (Map)versionsCombo.Tag;
                string selectedVersion = versionsCombo.EditValue.ToString();
                Map.Version version = map.versions[selectedVersion];
                sourcePath = version.vmfPath;
                string bspPath = sourcePath.Substring(0, sourcePath.Length - 3) + "bsp";
                if(File.Exists(bspPath))
                    this.compiledPath = bspPath;
                else
                    this.compiledPath = string.Empty;

                if(!File.Exists(sourcePath))
                    sourcePath = string.Empty;

                runButton.Enabled = (this.compiledPath != string.Empty);
                editButton.Enabled = (sourcePath != string.Empty);
                deleteButton.Enabled = true;

                Debugger.Break();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.fileName = sourcePath;
        }

        private void runPopup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.fileName = compiledPath;

            if (e.Item == runPopupRunFullscreen)
            {
                // Fullscreen
                this.runMode = RunMode.FULLSCREEN;
            }
            if (e.Item == runPopupRunWindowed)
            {
                // Windowed
                this.runMode = RunMode.WINDOWED;
            }
            if (e.Item == runPopupRunVR)
            {
                // VR
                this.runMode = RunMode.VR;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            this.fileName = compiledPath;
            this.runMode = RunMode.DEFAULT;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}