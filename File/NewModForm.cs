using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using windows_source1ide.Properties;

namespace SourceModdingTool
{
    public partial class NewModForm : DevExpress.XtraEditors.XtraForm
    {
        Steam sourceSDK;
        public string game = string.Empty;
        public string gameBranch = string.Empty;
        public string modFolder = string.Empty;
        public string modTitle = string.Empty;

        public bool validFolder = false;

        string modsPath;

        public NewModForm() { InitializeComponent(); }

        private void checkModDetails()
        {
            createButton.Enabled = false;
            if (!validFolder || galleryControl1.Gallery.GetCheckedItems().Count == 0)
                return;

            createButton.Enabled = true;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string modsPath = Steam.GetInstallPath() + "\\steamapps\\sourcemods\\";
            modFolder = textFolder.EditValue.ToString();

            modTitle = modFolder;

            string appId = sourceSDK.GetGameAppId(game).ToString();

            string mod = modTitle + " (" + modFolder + ")";
            string gamePath = sourceSDK.GetGamePath(game);
            string modPath = Steam.GetInstallPath() + "\\steamapps\\sourcemods\\" + modFolder;

            Directory.CreateDirectory(modPath + "\\bin");
            Directory.CreateDirectory(modPath + "\\resource");

            // Copy binaries
            string templatePath = AppDomain.CurrentDomain.BaseDirectory +
                "Templates\\" +
                game +
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

            File.Move(modPath + "\\resource\\template_english.txt",
                      modPath + "\\resource\\" + modFolder + "_english.txt");

            SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(modPath + "\\gameinfo.txt");
            gameInfo.setValue("game", modTitle);
            gameInfo.setValue("title", modTitle);


            SourceSDK.KeyValue.writeChunkFile(modPath + "\\gameinfo.txt", gameInfo, false, new UTF8Encoding(false));

            DialogResult = DialogResult.OK;
            Close();
        }

        private void galleryControl1_Gallery_ItemCheckedChanged(object sender,
                                                                DevExpress.XtraBars.Ribbon.GalleryItemEventArgs e)
        {
            string branchAndGame = e.Item.Tag.ToString();
            game = branchAndGame.Split('/')[0];
            gameBranch = branchAndGame.Split('/')[1];

            checkModDetails();
        }

        private void NewModForm_Load(object sender, EventArgs e)
        {
            sourceSDK = new Steam();
            modsPath = Steam.GetInstallPath() + "\\steamapps\\sourcemods\\";

            List<string> gamesList = sourceSDK.GetGamesList().Keys.ToList();
            foreach(DevExpress.XtraBars.Ribbon.GalleryItem item in galleryControl1.Gallery.GetAllItems())
            {
                if (gamesList.Contains(item.Tag.ToString().Split('/')[0]))
                {
                    item.Enabled = true;
                    switch(item.Tag.ToString().Split('/')[1])
                    {
                        case "hl2":
                            item.ImageOptions.Image = Resources.hl2;
                            break;
                        case "episodic":
                            item.ImageOptions.Image = Resources.episodic;
                            break;
                        case "ep2":
                            item.ImageOptions.Image = Resources.ep2;
                            break;
                        case "portal":
                            item.ImageOptions.Image = Resources.portal;
                            break;
                        case "portal2":
                            item.ImageOptions.Image = Resources.portal2;
                            break;
                        case "hl2mp":
                            item.ImageOptions.Image = Resources.hl2mp;
                            break;
                        case "bms":
                            item.ImageOptions.Image = Resources.bms;
                            item.Enabled = false;
                            break;
                    }
                }
            }

            textModsPath.EditValue = Steam.GetInstallPath() + "\\steamapps\\sourcemods\\";
            Size size = TextRenderer.MeasureText(textModsPath.EditValue.ToString(), textModsPath.Font);
            textModsPath.Width = size.Width + 8;

            checkModDetails();

            string templateModName = "mymod";

            int counter = 1;

            if (Directory.Exists(modsPath + templateModName))
            {
                do
                {
                    counter++;
                    templateModName = "mymod" + counter;
                } while (Directory.Exists(modsPath + templateModName));
            }

            textFolder.EditValue = templateModName;
        }

        private void textFolder_EditValueChanged(object sender, EventArgs e)
        {
            createButton.Enabled = false;
            validFolder = false;

            if(textFolder.EditValue == null)
            {
                labelFolderInfo.Text = "Invalid mod folder.";
                return;
            }

            string modFolder = textFolder.EditValue.ToString();
            string pattern = @"^[a-zA-Z0-9_]+$";
            Regex regex = new Regex(pattern);

            // Compare a string against the regular expression

            if(modFolder.Length == 0 || !regex.IsMatch(modFolder))
            {
                labelFolderInfo.Text = ("Invalid mod folder.");
                return;
            }

            if(Directory.Exists(modsPath + textFolder.EditValue.ToString()))
            {
                labelFolderInfo.Text = ("The mod folder already exists.");
                return;
            }

            labelFolderInfo.Text = string.Empty;
            validFolder = true;
            checkModDetails();
        }
    }
}