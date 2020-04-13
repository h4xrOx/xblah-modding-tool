using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using SourceModdingTool.SourceSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SourceModdingTool
{
    public partial class MaterialEditor : DevExpress.XtraEditors.XtraForm
    {
        Game game;
        bool isPreviewing = false;
        Steam sourceSDK;

        string modPath;

        private MaterialEditorTab activeTab = null;

        public MaterialEditor(string relativePath, Steam sourceSDK)
        {
            this.sourceSDK = sourceSDK;
            modPath = sourceSDK.GetModPath();


            InitializeComponent();

            activeTab = materialEditorTab1;
            activeTab.sourceSDK = sourceSDK;
            activeTab.ClearMaterial();

            if (relativePath != string.Empty)
            {
                activeTab.LoadMaterial(relativePath);
            }

            
        }

        private void barButtonOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            openVMTFileDialog.InitialDirectory = modPath + "\\materials\\";
            if(openVMTFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openVMTFileDialog.FileName;
                activeTab.LoadMaterial(fullPath);
            }
        }

        private void barButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string path = activeTab.relativePath;
            string shader = activeTab.shader;
            activeTab.SaveMaterial(path, shader);
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

            game = new Game(sourceSDK, panelControl1);
            game.Start("-nomouse +map material_preview +crosshair 0");
            this.ActiveControl = null;

            isPreviewing = true;
        }

        private void updatePreview()
        {
            if (isPreviewing)
            {
                activeTab.SaveMaterial("models/tools/material_preview", "VertexLitGeneric");
                game.Command("+mat_reloadallmaterials +map material_preview");
            }
        }

        public static string GetRelativePath(Steam sourceSDK, string fullPath)
        {
            string relativePath = fullPath;

            string modPath = sourceSDK.GetModPath();
            string gamePath = sourceSDK.GetGamePath();

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
            if (game != null && game.modProcess != null)
            {
                game.modProcess.Kill();
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
                activeTab.LoadMaterial(fullPath);
            }
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            (arg.Page as XtraTabPage).PageVisible = false;
        }
    }
}