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
using System.Text.RegularExpressions;

namespace SourceModdingTool
{
    public partial class NewModForm : DevExpress.XtraEditors.XtraForm
    {
        Steam sourceSDK;

        public bool validFolder = false;
        public string modFolder = "";
        public string modTitle = "";
        public string game = "";
        public string gameBranch = "";

        public NewModForm()
        {
            InitializeComponent();
        }

        private void NewModForm_Load(object sender, EventArgs e)
        {
            sourceSDK = new Steam();

            List<string> gamesList = sourceSDK.GetGamesList().Keys.ToList();
            foreach (DevExpress.XtraBars.Ribbon.GalleryItem item in galleryControl1.Gallery.GetAllItems())
            {
                if (gamesList.Contains(item.Tag.ToString().Split('/')[0]))
                    item.Enabled = true;
            }

            checkModDetails();
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
            string templatePath = AppDomain.CurrentDomain.BaseDirectory + "Templates\\" + game + "\\" + gameBranch + "\\";
            foreach (string file in Directory.GetFiles(templatePath, "*", SearchOption.AllDirectories))
            {
                string destinationPath = modPath + "\\" + file.Replace(templatePath, "");
                string destinationDirectory = new FileInfo(destinationPath).Directory.FullName;
                Directory.CreateDirectory(destinationDirectory);
                File.Copy(file, destinationPath);
            }

            File.Move(modPath + "\\resource\\template_english.txt", modPath + "\\resource\\" + modFolder + "_english.txt");

            SourceSDK.KeyValue gameInfo = SourceSDK.KeyValue.readChunkfile(modPath + "\\gameinfo.txt");
            gameInfo.setValue("game", modTitle);
            gameInfo.setValue("title", modTitle);


            SourceSDK.KeyValue.writeChunkFile(modPath + "\\gameinfo.txt", gameInfo, false, new UTF8Encoding(false));

            DialogResult = DialogResult.OK;
            Close();
        }

        private void textFolder_EditValueChanged(object sender, EventArgs e)
        {
            createButton.Enabled = false;
            validFolder = false;
            string modsPath = Steam.GetInstallPath() + "\\steamapps\\sourcemods\\";

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

            if (Directory.Exists(modsPath + textFolder.EditValue.ToString()))
            {
                labelFolderInfo.Text = ("The mod folder already exists.");
                return;
            }

            labelFolderInfo.Text = "";
            validFolder = true;
            checkModDetails();
        }

        private void checkModDetails()
        {
            createButton.Enabled = false;
            if (!validFolder)
                return;

            createButton.Enabled = true;
        }

        private void galleryControl1_Gallery_ItemCheckedChanged(object sender, DevExpress.XtraBars.Ribbon.GalleryItemEventArgs e)
        {
            string branchAndGame = e.Item.Tag.ToString();
            game = branchAndGame.Split('/')[0];
            gameBranch = branchAndGame.Split('/')[1];
            
            checkModDetails();
        }
    }
}