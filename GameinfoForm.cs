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
using static windows_source1ide.SourceSDK;
using System.Diagnostics;

namespace windows_source1ide
{
    public partial class GameinfoForm : DevExpress.XtraEditors.XtraForm
    {
        string game;
        string mod;
        SourceSDK sourceSDK;
        KeyVal<string, string> gameinfo;

        public GameinfoForm(string game, string mod)
        {
            this.game = game;
            this.mod = mod;

            InitializeComponent();
        }

        private void GameinfoForm_Load(object sender, EventArgs e)
        {
            sourceSDK = new SourceSDK();
            string path = sourceSDK.GetMods(game)[mod] + "\\gameinfo.txt";

            gameinfo = SourceSDK.readChunkfile(path);

            textGame.EditValue = gameinfo.GetChild("game").Value;
            textTitle.EditValue = gameinfo.GetChild("title").Value;

            string type = gameinfo.GetChild("type").Value;
            if (type == "multiplayer_only")
            {
                textType.EditValue = "Multi-player";
            } else
            {
                textType.EditValue = "Single-player";
            }

            textDeveloper.EditValue = gameinfo.GetChild("developer").Value;
            textDeveloperURL.EditValue = gameinfo.GetChild("developer_url").Value;
            textManual.EditValue = gameinfo.GetChild("manual").Value;
            textIcon.EditValue = gameinfo.GetChild("icon").Value;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            gameinfo.SetChild("game", textGame.EditValue.ToString());
            gameinfo.SetChild("title",textTitle.EditValue.ToString());

            string type;
            if (textType.EditValue.ToString() == "Multi-player")
            {
                type = "multiplayer_only";
            } else
            {
                type = "singleplayer_only";
            }
            gameinfo.SetChild("type", type);

            gameinfo.SetChild("developer", textDeveloper.EditValue.ToString());
            gameinfo.SetChild("developer_url", textDeveloperURL.EditValue.ToString());
            gameinfo.SetChild("manual", textManual.EditValue.ToString());
            gameinfo.SetChild("icon", textIcon.EditValue.ToString());

            string path = sourceSDK.GetMods(game)[mod] + "\\gameinfo.txt";

            writeChunkFile(path, "GameInfo", gameinfo);

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}