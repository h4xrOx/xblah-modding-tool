using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using source_modding_tool.Materials;
using source_modding_tool.Materials.ShaderTabs;
using source_modding_tool.Modding;
using SourceSDK;
using SourceSDK.Materials;
using SourceSDK.Packages;
using SourceSDK.Scripts;
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
        PackageManager packageManager;

        string modPath;

        /// <summary>
        /// When the Material Editor is loaded and a packageFile is set, it will open the package file.
        /// </summary>
        PackageFile packageFile = null;     

        private ShaderInterface activeTab = null;

        public MaterialEditor(Launcher launcher)
        {
            this.launcher = launcher;
            modPath = launcher.GetCurrentMod().InstallPath;

            packageManager = new PackageManager(launcher, "materials");

            InitializeComponent();

            activeTab = null;

            previewDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
        }

        public MaterialEditor(Launcher launcher, PackageFile file) : this(launcher)
        {
            this.packageFile = file;
        }

        #region MainMenu
        private void menu_ItemClick(object sender, ItemClickEventArgs e)
        {
            // New material
            if (e.Item == menuFileNew)
            {
                ShaderDialog dialog = new ShaderDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string shader = dialog.Shader;
                    XtraTabPage tabPage = new XtraTabPage();
                    string fileName = "";                    

                    //System.Diagnostics.Debugger.Break();

                    ShaderInterface tab;

                    switch (shader)
                    {
                        case "UnlitGeneric":
                            tab = new UnlitGenericTab()
                            {
                                Launcher = launcher,
                                PackageManager = packageManager
                            };

                            break;
                        case "VertexLitGeneric":
                            tab = new VertexLitGenericTab()
                            {
                                Launcher = launcher,
                                PackageManager = packageManager
                            };

                            break;
                        case "LightmappedGeneric":
                            tab = new LightmappedGenericTab()
                            {
                                Launcher = launcher,
                                PackageManager = packageManager
                            };

                            break;
                        default:
                            {
                                return;
                            }
                            break;
                    }

                    tab.OnUpdated += MaterialEditorTab_OnUpdated;
                    vmtEdit.Text = tab.VMT;

                    tabPage.Controls.Add(tab as Control);
                    (tab as Control).Dock = DockStyle.Fill;
                    tabPage.Tag = tab;

                    tabControl.TabPages.Add(tabPage);
                    tabControl.SelectedTabPage = tabPage;
                    tabPage.Text = (fileName != "" ? fileName : "Untitled");
                }

                
            } 
            
            // Open material
            else if(e.Item == menuFileOpen)
            {
                FileExplorer fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.OPEN)
                {
                    packageManager = packageManager,
                    RootDirectory = "materials",
                    Filter = "Valve Material Type Files (*.vmt)|*.vmt"
                };
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    if (fileExplorer.Selection != null)
                        foreach(PackageFile file in fileExplorer.Selection)
                        {
                            LoadMaterial(file);
                        }
                }
            }

            // Save material
            else if(e.Item == menuFileSave)
            {
                if (activeTab.RelativePath != "")
                    SaveMaterial(activeTab);

                else
                    menuFileSaveAs.PerformClick();
            }

            // Save material as
            else if (e.Item == menuFileSaveAs)
            {
                FileExplorer fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.SAVE)
                {
                    packageManager = packageManager,
                    RootDirectory = "materials",
                    Filter = "Valve Material Files (*.vmt)|*.vmt"
                };
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    string fileName = fileExplorer.FileName;
                    activeTab.RelativePath = fileName;
                    SaveMaterial(activeTab);
                }
            }
            else if(e.Item == menuFileExit)
            {
                Close();
            }
        }

        public static Bitmap GetAlphaMask(Bitmap bitmap)
        {
            Bitmap transparencymask = new Bitmap(bitmap.Width, bitmap.Height);
            for (int i = 0; i < transparencymask.Width; i++)
            {
                for (int j = 0; j < transparencymask.Height; j++)
                {
                    int a = bitmap.GetPixel(i, j).A;
                    transparencymask.SetPixel(i, j, Color.FromArgb(255, a, a, a));
                }
            }

            return transparencymask;
        }

        private void LoadMaterial(PackageFile file)
        {
            string data = System.Text.Encoding.UTF8.GetString(file.Data);

            string fullPath = "";

            SourceSDK.KeyValue vmt = SourceSDK.KeyValue.ReadChunk(data);

            ShaderInterface tab;

            switch (vmt.getKey())
            {
                case "sdk_unlitgeneric":
                case "unlitgeneric":
                    tab = new UnlitGenericTab()
                    {
                        Launcher = launcher,
                        PackageManager = packageManager,
                        RelativePath = file.Path + "/" + file.Filename
                    };
                    break;
                case "sdk_vertexlitgeneric":
                case "vertexlitgeneric":
                    tab = new VertexLitGenericTab()
                    {
                        Launcher = launcher,
                        PackageManager = packageManager,
                        RelativePath = file.Path + "/" + file.Filename
                    };
                    break;
                case "sdk_lightmappedgeneric":
                case "lightmappedgeneric":
                    tab = new LightmappedGenericTab()
                    {
                        Launcher = launcher,
                        PackageManager = packageManager,
                        RelativePath = file.Path + "/" + file.Filename
                    };
                    break;
                default:
                    XtraMessageBox.Show("Shader " + vmt.getKey() + " is not supported.");
                    return;
            }

            XtraTabPage tabPage = new XtraTabPage();

            tab.OnUpdated += MaterialEditorTab_OnUpdated;
            vmtEdit.Text = tab.VMT;

            tabPage.Text = file.Filename;
            tabPage.Controls.Add(tab as Control);
            (tab as Control).Dock = DockStyle.Fill;
            tabPage.Tag = tab;

            tabControl.TabPages.Add(tabPage);
            tabControl.SelectedTabPage = tabPage;
        }

        internal static void SaveMaterial(ShaderInterface tabInterface)
        {
            // Gets the vmt.
            string vmt = tabInterface.VMT;

            // Gets the full path.
            string fullPath = tabInterface.Launcher.GetCurrentMod().GetFullPath(tabInterface.RelativePath);

            // Saves the file.
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            File.WriteAllText(fullPath + ".vmt", vmt, new UTF8Encoding(false));

            // Adds the file to File Explorer.
            tabInterface.PackageManager.CreateFile(tabInterface.RelativePath + ".vmt");

            // Saves the VTFs.
            foreach (KeyValuePair<string, Texture> texture in tabInterface.Textures)
            {
                if (texture.Value.bitmap != null && texture.Value.relativePath == "")
                {
                    switch (texture.Key)
                    {
                        default:
                            {
                                // Saves the file
                                File.WriteAllBytes(fullPath + "_" + texture.Key + ".vtf", texture.Value.bytes);

                                // Adds the file to File Explorer.
                                tabInterface.PackageManager.CreateFile(tabInterface.RelativePath + "_" + texture.Key + ".vtf");
                                break;
                            }
                    }
                }
            }
        }

        public void MaterialEditorTab_OnUpdated(object sender, EventArgs e)
        {
            if (activeTab != null)
                vmtEdit.Text = activeTab.VMT;

            if (isPreviewing)
            {
                SaveMaterial(activeTab);
                instance.Command("+mat_reloadallmaterials +map preview_material_unlitgeneric");
                //instance.Command("+mat_reloadallmaterials");
            }
        }
        #endregion

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            if (activeTab != null)
            {
                SaveMaterial(activeTab);
            }
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
            instance.Start(new RunPreset(RunMode.WINDOWED), "-nomouse -novid +map preview_material_unlitgeneric +crosshair 0");
            this.ActiveControl = null;

            isPreviewing = true;
        }

        public static string GetRelativePath(Launcher launcher, string fullPath)
        {
            string relativePath = fullPath;

            string modPath = launcher.GetCurrentMod().InstallPath;
            string gamePath = launcher.GetCurrentGame().InstallPath;

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
            SaveMaterial(activeTab);
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
                activeTab = (ShaderInterface)e.Page.Tag;
                MaterialEditorTab_OnUpdated(e.Page.Tag, new EventArgs());
            }
            else
                activeTab = null;
        }

        private void dockPanel1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (e.Button.Properties.Tag.ToString() == "refresh")
            {
                MaterialEditorTab_OnUpdated(null, new EventArgs());
            }
        }

        /// <summary>
        /// Clears a pictureEdit and disposes its texture.
        /// </summary>
        /// <param name="pictureEdit"></param>
        /// <param name="texture"></param>
        public static void ClearTexture(string tag, ShaderInterface tabInterface)
        {
            tabInterface.PictureEdits[tag].Image = null;
            tabInterface.Textures[tag].bitmap = null;
            tabInterface.Textures[tag].bytes = null;
            tabInterface.Textures[tag].relativePath = string.Empty;
        }

        /// <summary>
        /// Clears the entire material.
        /// </summary>
        /// <param name="tabInterface"></param>
        public static void ClearMaterial(ShaderInterface tabInterface)
        {
            tabInterface.Textures = new Dictionary<string, Texture>();

            foreach (KeyValuePair<string, PictureEdit> kv in tabInterface.PictureEdits)
            {
                if (kv.Key == "transparencymask")
                    continue;

                tabInterface.Textures.Add(kv.Key, new Texture());
                kv.Value.Image = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pictureEdit"></param>
        public static void ImportTexture(string tag, ShaderInterface tabInterface)
        {
            int width;
            int height;

            XtraOpenFileDialog dialog = new XtraOpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string type = new FileInfo(dialog.FileName).Extension;

                string relativePath = tabInterface.Launcher.GetCurrentMod().GetRelativePath(dialog.FileName);

                if (type == ".vtf")
                {
                    tabInterface.Textures[tag].relativePath = relativePath;
                    tabInterface.Textures[tag].bytes = File.ReadAllBytes(dialog.FileName);
                    tabInterface.Textures[tag].bitmap = VTF.ToBitmap(tabInterface.Textures[tag].bytes, tabInterface.Launcher);
                }
                else
                {
                    tabInterface.Textures[tag].relativePath = string.Empty;
                    Image originalBitmap = Bitmap.FromFile(dialog.FileName);
                    width = (int)Math.Pow(2, Math.Floor(Math.Log(originalBitmap.Width, 2)));
                    height = (int)Math.Pow(2, Math.Floor(Math.Log(originalBitmap.Height, 2)));
                    tabInterface.Textures[tag].bitmap = new Bitmap(originalBitmap, width, height);
                    originalBitmap.Dispose();
                    tabInterface.Textures[tag].bytes = VTF.FromBitmap(tabInterface.Textures[tag].bitmap, tabInterface.Launcher);
                }

                if (tabInterface.Textures[tag].bitmap != null)
                    tabInterface.PictureEdits[tag].Image = tabInterface.Textures[tag].bitmap;
            }
        }

        internal static void ImportMask(string tag, ShaderInterface tabInterface)
        {
            int width;
            int height;

            XtraOpenFileDialog dialog = new XtraOpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string type = new FileInfo(dialog.FileName).Extension;

                Image originalBitmap = Bitmap.FromFile(dialog.FileName);
                width = (int)Math.Pow(2, Math.Floor(Math.Log(originalBitmap.Width, 2)));
                height = (int)Math.Pow(2, Math.Floor(Math.Log(originalBitmap.Height, 2)));

                Bitmap maskBitmap = new Bitmap(originalBitmap, width, height);
                originalBitmap.Dispose();

                if (tag == "transparencymask")
                {
                    // If no albedo is set yet.
                    if (tabInterface.Textures["basetexture"].bitmap == null)
                        tabInterface.Textures["basetexture"].bitmap = new Bitmap(width, height);

                    MaterialEditor.ApplyMask(maskBitmap, tabInterface.Textures["basetexture"].bitmap);

                    tabInterface.Textures["basetexture"].relativePath = string.Empty;
                    tabInterface.Textures["basetexture"].bytes = VTF.FromBitmap(tabInterface.Textures["basetexture"].bitmap, tabInterface.Launcher);
                }

                if (tabInterface.PictureEdits["transparencymask"].Image != null)
                    tabInterface.PictureEdits["transparencymask"].Image.Dispose();

                tabInterface.PictureEdits["transparencymask"].Image = maskBitmap;
            }
        }

        private static void ApplyMask(Bitmap transparencymask, Bitmap bitmap)
        {
            for (int i = 0; i < transparencymask.Width; i++)
            {
                for (int j = 0; j < transparencymask.Height; j++)
                {
                    int alpha = transparencymask.GetPixel(i, j).R;
                    Color color = bitmap.GetPixel(i, j);
                    bitmap.SetPixel(i, j, Color.FromArgb(alpha, color.R, color.G, color.B));
                }
            }
        }

        public static void OpenTexture(string tag, ShaderInterface tabInterface)
        {
            FileExplorer fileExplorer = new FileExplorer(tabInterface.Launcher, FileExplorer.Mode.OPEN)
            {
                RootDirectory = "materials",
                Filter = "Valve Texture Files (*.vtf)|*.vtf",
                MultiSelect = false
            };
            if (fileExplorer.ShowDialog() == DialogResult.OK)
            {
                PackageFile file = fileExplorer.Selection[0];
                //System.Diagnostics.Debugger.Break();

                tabInterface.Textures[tag].relativePath = file.FullPath;
                tabInterface.Textures[tag].bytes = file.Data;
                tabInterface.Textures[tag].bitmap = VTF.ToBitmap(file.Data, tabInterface.Launcher);

                if (tabInterface.PictureEdits[tag].Image != null)
                    tabInterface.PictureEdits[tag].Image.Dispose();

                if (tabInterface.Textures[tag].bitmap != null)
                    tabInterface.PictureEdits[tag].Image = tabInterface.Textures[tag].bitmap;
            }
        }

        public static void CreateToolTexture(ShaderInterface shaderInterface)
        {
            Bitmap basetexture = (shaderInterface.Textures.Keys.Contains("basetexture") ? shaderInterface.Textures["basetexture"].bitmap : null);
            Bitmap basetexture2 = (shaderInterface.Textures.Keys.Contains("basetexture2") ? shaderInterface.Textures["basetexture2"].bitmap : null);
            Bitmap shader = (Bitmap)Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "/Assets/Shaders/" + shaderInterface.Shader + ".png");

            Texture toolTexture = new Texture();

            if (basetexture == null)
            {
                toolTexture.bitmap = null;
                toolTexture.relativePath = string.Empty;
            }

            if (basetexture != null && basetexture2 == null)
            {
                toolTexture.bitmap = (Bitmap)basetexture.Clone();
                toolTexture.relativePath = string.Empty;
            }
            else if (basetexture != null && basetexture2 != null)
            {
                Bitmap tooltexture = null;

                // Resize images
                int width = basetexture.Width;
                int height = basetexture.Height;

                // Merge images
                tooltexture = new Bitmap(width, height);

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Color baseColor = basetexture.GetPixel(i, j);
                        Color baseColor2 = basetexture2.GetPixel(i, j);

                        float baseMultiply = Math.Min(Math.Max(2.5f - (float)(i + j) / (width + height) * 4, 0), 1);
                        float baseMultiply2 = 1 - baseMultiply;

                        Color toolColor = Color.FromArgb((int)(baseColor.R * baseMultiply + baseColor2.R * baseMultiply2),
                                                         (int)(baseColor.G * baseMultiply + baseColor2.G * baseMultiply2),
                                                         (int)(baseColor.B * baseMultiply + baseColor2.B * baseMultiply2));
                        tooltexture.SetPixel(i, j, toolColor);
                    }
                }

                toolTexture.bitmap = tooltexture;
                toolTexture.relativePath = string.Empty;
            }

            if (toolTexture.bitmap != null)
            {
                using (Graphics g = Graphics.FromImage(toolTexture.bitmap))
                    g.DrawImage(shader, new Rectangle(0, 0, basetexture.Width / 8, basetexture.Height / 8), new Rectangle(0, 0, shader.Width, shader.Height), GraphicsUnit.Point);

                toolTexture.bytes = VTF.FromBitmap(toolTexture.bitmap, shaderInterface.Launcher);

                shaderInterface.Textures["tooltexture"] = toolTexture;
                shaderInterface.PictureEdits["tooltexture"].Image = toolTexture.bitmap;
            }
        }

        private void MaterialEditor_Load(object sender, EventArgs e)
        {
            if (packageFile != null)
            {
                LoadMaterial(packageFile);
                packageFile = null;
            }
        }
    }
}