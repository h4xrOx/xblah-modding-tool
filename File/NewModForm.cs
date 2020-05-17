using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using source_modding_tool.Properties;

namespace source_modding_tool
{
    public partial class NewModForm : DevExpress.XtraEditors.XtraForm
    {
        Launcher launcher;
        public int engine = 0;
        public string gameName = string.Empty;
        public string gameBranch = string.Empty;
        public string modFolder = string.Empty;

        public bool validFolder = false;

        string baseModPath;

        public NewModForm() {
            InitializeComponent();
        }

        private void NewModForm_Load(object sender, EventArgs e)
        {
            launcher = new Launcher();
            SetBaseModPath(Launcher.GetInstallPath() + "\\steamapps\\sourcemods\\");

            List<string> gamesList = launcher.GetGamesList().Keys.ToList();
            foreach (DevExpress.XtraBars.Ribbon.GalleryItem item in gameGallery.Gallery.GetAllItems())
            {
                string branchName = item.Tag.ToString().Split('/')[2];

                if (gamesList.Contains(item.Tag.ToString().Split('/')[1]))
                {
                    item.Enabled = true;
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Assets\\Posters\\" + branchName + ".png"))
                        item.ImageOptions.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Assets\\Posters\\" + branchName + ".png");
                } else
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Assets\\Posters\\" + branchName + "_notinstalled.png"))
                        item.ImageOptions.Image = Bitmap.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Assets\\Posters\\" + branchName + "_notinstalled.png");
                }
            }

            checkModDetails();

            string templateModName = "mymod";

            int counter = 1;
            if (Directory.Exists(baseModPath + templateModName))
            {
                do
                {
                    counter++;
                    templateModName = "mymod" + counter;
                } while (Directory.Exists(baseModPath + templateModName));
            }

            textFolder.EditValue = templateModName;
        }

        private void SetBaseModPath(string baseModPath)
        {
            this.baseModPath = baseModPath;

            textModsPath.EditValue = baseModPath;
            Size size = TextRenderer.MeasureText(textModsPath.EditValue.ToString(), textModsPath.Font);
            textModsPath.Width = size.Width + 8;
        }

        private void gameGallery_ItemCheckedChanged(object sender,
                                                        DevExpress.XtraBars.Ribbon.GalleryItemEventArgs e)
        {
            string branchAndGame = e.Item.Tag.ToString();
            string engineName = branchAndGame.Split('/')[0];
            gameName = branchAndGame.Split('/')[1];
            gameBranch = branchAndGame.Split('/')[2];

            switch (engineName)
            {
                case "source":
                    engine = Engine.SOURCE;
                    SetBaseModPath(Launcher.GetInstallPath() + "\\steamapps\\sourcemods\\");
                    break;
                case "source2":
                    engine = Engine.SOURCE2;
                    SetBaseModPath(launcher.GetGamesList()[gameName].installPath + "\\game\\");
                    break;
                case "goldsrc":
                    engine = Engine.GOLDSRC;
                    SetBaseModPath(launcher.GetGamesList()[gameName].installPath + "\\");
                    break;
            }

            ValidateModFolder();

            checkModDetails();
        }

        private void ValidateModFolder()
        {
            createButton.Enabled = false;
            validFolder = false;

            if (textFolder.EditValue == null)
            {
                labelFolderInfo.Text = "Invalid mod folder.";
                return;
            }

            string modFolder = textFolder.EditValue.ToString();
            string pattern = @"^[a-zA-Z0-9_]+$";
            Regex regex = new Regex(pattern);

            // Compare a string against the regular expression

            if (modFolder.Length == 0 || !regex.IsMatch(modFolder))
            {
                labelFolderInfo.Text = ("Invalid mod folder.");
                return;
            }

            if (Directory.Exists(baseModPath + textFolder.EditValue.ToString()))
            {
                labelFolderInfo.Text = ("The mod folder already exists.");
                return;
            }

            labelFolderInfo.Text = string.Empty;
            validFolder = true;
        }

        private void textFolder_EditValueChanged(object sender, EventArgs e)
        {
            ValidateModFolder();
            checkModDetails();
        }

        private void checkModDetails()
        {
            createButton.Enabled = false;
            if (!validFolder || gameGallery.Gallery.GetCheckedItems().Count == 0)
                return;

            createButton.Enabled = true;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            modFolder = textFolder.EditValue.ToString();

            Game game = launcher.GetGamesList()[gameName];

            string appId = game.GetAppId().ToString();

            string mod = modFolder + " (" + modFolder + ")";
            string gamePath = game.installPath;
            string modPath = baseModPath + modFolder;

            // Copy the mod template
            string templatePath = AppDomain.CurrentDomain.BaseDirectory +
                "Templates\\" +
                game.name +
                "\\" +
                gameBranch +
                "\\";
            foreach(string file in Directory.GetFiles(templatePath, "*", SearchOption.AllDirectories))
            {
                string destinationPath = modPath + "\\" + file.Replace(templatePath, string.Empty);
                string destinationDirectory = new FileInfo(destinationPath).Directory.FullName;
                Directory.CreateDirectory(destinationDirectory);
                File.Copy(file, destinationPath);
            }
            switch(engine)
            {
                case Engine.SOURCE:
                    {
                        File.Move(modPath + "\\resource\\template_english.txt", modPath + "\\resource\\" + modFolder + "_english.txt");

                        SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(modPath + "\\gameinfo.txt");
                        gameInfo.setValue("game", modFolder);
                        gameInfo.setValue("title", modFolder);

                        SourceSDK.KeyValue.writeChunkFile(modPath + "\\gameinfo.txt", gameInfo, false, new UTF8Encoding(false));
                    }
                    break;
                case Engine.SOURCE2:
                    {
                        SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(modPath + "\\gameinfo.gi");
                        gameInfo.setValue("game", modFolder);
                        gameInfo.setValue("title", modFolder);

                        SourceSDK.KeyValue searchPaths = gameInfo.getChildByKey("filesystem").getChildByKey("searchpaths");
                        searchPaths.clearChildren();
                        searchPaths.addChild("game", modFolder);
                        searchPaths.addChild("game", "hlvr");
                        searchPaths.addChild("game", "core");
                        searchPaths.addChild("mod", modFolder);
                        searchPaths.addChild("write", modFolder);

                        SourceSDK.KeyValue.writeChunkFile(modPath + "\\gameinfo.gi", gameInfo, false, new UTF8Encoding(false));
                    }
                    break;
                case Engine.GOLDSRC:
                    {
                        SourceSDK.KeyValue gameInfo = SourceSDK.Config.readChunkfile(modPath + "\\liblist.gam");
                        gameInfo.setValue("game", modFolder);

                        SourceSDK.Config.writeChunkFile(modPath + "\\liblist.gam", gameInfo, false, new UTF8Encoding(false));
                    }
                    break;
            }


            DialogResult = DialogResult.OK;
            Close();
        }
    }
}