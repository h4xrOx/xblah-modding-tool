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

namespace windows_source1ide
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

            comboGames.Properties.Items.Clear();
            foreach (KeyValuePair<string, string> item in sourceSDK.GetGamesList())
                comboGames.Properties.Items.Add(item.Key);

            comboGames.EditValue = comboGames.Properties.Items[0];
            comboGames.EditValue = "Source SDK Base 2013 Singleplayer";
            checkModDetails();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string modsPath = Steam.GetInstallPath() + "\\steamapps\\sourcemods\\";
            modFolder = textFolder.EditValue.ToString();

            modTitle = modFolder;
            if (textTitle.EditValue != null && textTitle.EditValue.ToString().Length > 0 && !(textTitle.EditValue.ToString().IndexOfAny("{}\"".ToCharArray()) != -1))
            {
                modTitle = textTitle.EditValue.ToString();
            }

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

        private void comboGames_TextChanged(object sender, EventArgs e)
        {
            game = comboGames.EditValue.ToString();

            comboBranches.Properties.Items.Clear();
            foreach (string branch in sourceSDK.GetAllGameBranches(game))
                comboBranches.Properties.Items.Add(branch);

            comboBranches.EditValue = comboBranches.Properties.Items[0];
            checkModDetails();
        }

        private void comboBranches_TextChanged(object sender, EventArgs e)
        {
            gameBranch = comboBranches.EditValue.ToString();
            checkModDetails();
        }

        private void checkModDetails()
        {
            createButton.Enabled = false;
            if (!validFolder)
                return;

            createButton.Enabled = true;
        }
    }
}