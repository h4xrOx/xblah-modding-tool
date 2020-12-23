using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
 
using SourceSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace source_modding_tool
{
    public partial class MaterialEditor : DevExpress.XtraEditors.XtraForm
    {
        Instance instance;
        bool isPreviewing = false;
        Launcher launcher;

        string modPath;

        private MaterialEditorTab activeTab = null;

        public MaterialEditor(string relativePath, Launcher launcher)
        {
            this.launcher = launcher;
            modPath = launcher.GetCurrentMod().installPath;


            InitializeComponent();

            activeTab = null;

            
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            activeTab.SaveMaterial("models/tools/material_preview", "VertexLitGeneric");
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

        private void updatePreview()
        {
            if (isPreviewing)
            {
                activeTab.SaveMaterial("models/tools/material_preview", "VertexLitGeneric");
                instance.Command("+mat_reloadallmaterials +map material_preview");
            }
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

        private void barButtonNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            newVMTDialog.InitialDirectory = modPath + "/materials";
            if (newVMTDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = newVMTDialog.FileName;

                MaterialEditorTab userControl = new MaterialEditorTab();
                userControl.launcher = launcher;
                XtraTabPage tab = new XtraTabPage();

                string fileName = new FileInfo(fullPath).Name;

                tab.Text = fileName;
                tab.Controls.Add(userControl);
                userControl.Dock = DockStyle.Fill;
                tab.Tag = userControl;

                tabControl.TabPages.Add(tab);

                userControl.LoadMaterial(fullPath);
                tabControl.SelectedTabPage = tab;
            }  
        }

        private void barButtonOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            openVMTFileDialog.InitialDirectory = modPath + "\\materials\\";
            if (openVMTFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openVMTFileDialog.FileName;

                MaterialEditorTab userControl = new MaterialEditorTab();
                userControl.launcher = launcher;
                XtraTabPage tab = new XtraTabPage();

                string fileName = new FileInfo(fullPath).Name;

                tab.Text = fileName;
                tab.Controls.Add(userControl);
                userControl.Dock = DockStyle.Fill;
                tab.Tag = userControl;

                tabControl.TabPages.Add(tab);

                userControl.LoadMaterial(fullPath);
                tabControl.SelectedTabPage = tab;
            }
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
            activeTab = (MaterialEditorTab) e.Page.Tag;
        }
    }
}