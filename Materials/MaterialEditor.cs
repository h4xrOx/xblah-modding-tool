using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using source_modding_tool.Modding;
using SourceSDK;
using SourceSDK.Packages;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace source_modding_tool
{
    public partial class MaterialEditor : DevExpress.XtraEditors.XtraForm
    {
        Instance instance;
        bool isPreviewing = false;

        Launcher launcher;
        PackageManager packageManager;

        string modPath;

        private MaterialEditorTab activeTab = null;

        public MaterialEditor(string relativePath, Launcher launcher)
        {
            this.launcher = launcher;
            modPath = launcher.GetCurrentMod().installPath;

            packageManager = new PackageManager(launcher, "materials");

            InitializeComponent();

            activeTab = null;
        }

        #region MainMenu
        private void menu_ItemClick(object sender, ItemClickEventArgs e)
        {
            // New material
            if (e.Item == menuFileNew)
            {
                MaterialEditorTab materialEditorTab = new MaterialEditorTab(launcher, packageManager);
                materialEditorTab.OnUpdated += MaterialEditorTab_OnUpdated;

                XtraTabPage tab = new XtraTabPage();

                string fileName = "";

                tab.Text = (fileName != "" ? fileName : "Untited");
                tab.Controls.Add(materialEditorTab);
                materialEditorTab.Dock = DockStyle.Fill;
                tab.Tag = materialEditorTab;

                tabControl.TabPages.Add(tab);

                tabControl.SelectedTabPage = tab;

                vmtEdit.Text = materialEditorTab.VMT;
            } 
            
            // Load material
            else if(e.Item == menuFileOpen)
            {
                FileExplorer fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.OPEN)
                {
                    packageManager = packageManager,
                    RootDirectory = "materials"
                };
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    if (fileExplorer.Selection != null)
                        foreach(PackageFile file in fileExplorer.Selection)
                        {
                            MaterialEditorTab materialEditorTab = new MaterialEditorTab(launcher ,packageManager);

                            XtraTabPage tab = new XtraTabPage();

                            tab.Text = file.Filename;
                            tab.Controls.Add(materialEditorTab);
                            materialEditorTab.Dock = DockStyle.Fill;
                            tab.Tag = materialEditorTab;

                            materialEditorTab.LoadMaterial(file);

                            tabControl.TabPages.Add(tab);
                            tabControl.SelectedTabPage = tab;
                        }
                }
            }

            // Save material
            else if(e.Item == menuFileSave)
            {

            }

            // Save material as
            else if (e.Item == menuFileSaveAs)
            {
                XtraSaveFileDialog dialog = new XtraSaveFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.FileName;
                    if (path.EndsWith(".vmt"))
                        path = path.Substring(0, path.Length - 4);

                    string relativePath = activeTab.GetRelativePath(path);

                    string vtfRelativePath = relativePath;
                    if (vtfRelativePath.Contains("/materials/"))
                        vtfRelativePath = vtfRelativePath.Substring(relativePath.LastIndexOf("/materials/"));

                    activeTab.SetRelativePath(vtfRelativePath);

                    activeTab.SaveMaterial(relativePath);
                    
                }
            }
            else if(e.Item == menuFileExit)
            {
                Close();
            }
        }

        private void MaterialEditorTab_OnUpdated(object sender, EventArgs e)
        {
            vmtEdit.Text = activeTab.VMT;

            if (isPreviewing)
            {
                activeTab.SaveMaterial("models/tools/material_preview", "VertexLitGeneric");
                instance.Command("+mat_reloadallmaterials +map material_preview");
                //instance.Command("+mat_reloadallmaterials");
            }
        }
        #endregion

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            activeTab.SaveMaterial("materials/models/tools/material_preview", "VertexLitGeneric");
            startPreview();
        }

        private void startPreview()
        {
            string sourcePath = AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\IngamePreviews";
 
            // Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, modPath));

            // Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, modPath), true);

            instance = new Instance(launcher, panelControl1);
            instance.Start(new RunPreset(RunMode.WINDOWED), "-nomouse +map material_preview +crosshair 0");
            this.ActiveControl = null;

            isPreviewing = true;
        }

        public static string GetRelativePath(Launcher launcher, string fullPath)
        {
            string relativePath = fullPath;

            string modPath = launcher.GetCurrentMod().installPath;
            string gamePath = launcher.GetCurrentGame().installPath;

            if (fullPath.Contains(modPath))
            {
                relativePath = fullPath.Replace(modPath, string.Empty).Replace(".vmt", string.Empty);
            }
            else if (fullPath.Contains("\\materials\\"))
            {
                relativePath = fullPath.Substring(fullPath.LastIndexOf("\\materials\\") + "\\materials\\".Length)
                    .Replace(".vmt", string.Empty);
            }
            else
            {
                Uri path1 = new Uri(gamePath + "\\");
                Uri path2 = new Uri(fullPath);
                Uri diff = path1.MakeRelativeUri(path2);

                relativePath = diff.OriginalString.Replace(".vmt", string.Empty);
            }

            return relativePath;
        }

        private void stopPreview()
        {
            if (instance != null && instance.modProcess != null)
            {
                instance.modProcess.Kill();
            }
        }

        private void MaterialEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopPreview();
        }

        public class Texture
        {
            public Bitmap bitmap = null;
            public byte[] bytes = null;
            public string relativePath = string.Empty;
        }

        private void barButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string path = activeTab.relativePath;
            string shader = activeTab.shader;
            activeTab.SaveMaterial(path, shader);
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            (arg.Page as XtraTabPage).PageVisible = false;
        }

        private void tabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.Page != null)
            {
                activeTab = (MaterialEditorTab)e.Page.Tag;
                MaterialEditorTab_OnUpdated(e.Page.Tag, new EventArgs());
            }
            else
                activeTab = null;
        }
    }
}