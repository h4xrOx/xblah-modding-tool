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
            //textTitle2.EditValue = gameinfo.GetChild("title2").Value;

            string type = gameinfo.GetChild("type").Value;
            if (type == "multiplayer_only")
            {
                textType.EditValue = "Multi-player";
            } else
            {
                textType.EditValue = "Single-player";
            }
            switchDifficulty.EditValue = (gameinfo.GetChild("nodifficulty").Value == "1" ? false : true);
            switchPortals.EditValue = (gameinfo.GetChild("hasportals").Value == "1" ? true : false);
            switchCrosshair.EditValue = (gameinfo.GetChild("nocrosshair").Value == "1" ? false : true);
            switchAdvCrosshair.EditValue = (gameinfo.GetChild("advcrosshair").Value == "1" ? true : false);
            switchModels.EditValue = (gameinfo.GetChild("nomodels").Value == "1" ? false : true);

            textDeveloper.EditValue = gameinfo.GetChild("developer").Value;
            textDeveloperURL.EditValue = gameinfo.GetChild("developer_url").Value;
            textManual.EditValue = gameinfo.GetChild("manual").Value;
            textIcon.EditValue = gameinfo.GetChild("icon").Value;

            switchNodegraph.EditValue = (gameinfo.GetChild("nodegraph").Value == "1" ? true : false);
            textGamedata.EditValue = gameinfo.GetChild("gamedata").Value;
            textInstance.EditValue = gameinfo.GetChild("instancepath").Value;
            switchVR.EditValue = (gameinfo.GetChild("supportsvr").Value == "1" ? true : false);

            comboGames.Properties.Items.Clear();
            string appID = gameinfo.GetChild("filesystem").GetChild("steamappid").Value;
            foreach (KeyValuePair<string, string> item in sourceSDK.GetGames())
            {
                comboGames.Properties.Items.Add(item.Key);
                string gameAppID = sourceSDK.GetGameAppId(item.Key).ToString();
                if (appID == gameAppID.ToString())
                {
                    comboGames.EditValue = item.Key;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            gameinfo.SetChild("game", (textGame.EditValue != null ? textGame.EditValue.ToString() : ""));
            gameinfo.SetChild("title", (textTitle.EditValue != null ? textTitle.EditValue.ToString() : ""));
            //gameinfo.SetChild("title2", (textTitle2.EditValue != null ? textTitle2.EditValue.ToString() : ""));

            string type;
            if (textType.EditValue.ToString() == "Multi-player")
            {
                type = "multiplayer_only";
            } else
            {
                type = "singleplayer_only";
            }
            gameinfo.SetChild("type", type);

            gameinfo.SetChild("nodifficulty", (switchDifficulty.IsOn ? "0" : "1"));
            gameinfo.SetChild("hasportals", (switchPortals.IsOn ? "1" : "0"));
            gameinfo.SetChild("nocrosshair", (switchCrosshair.IsOn ? "0" : "1"));
            gameinfo.SetChild("advcrosshair", (switchAdvCrosshair.IsOn ? "1" : "0"));
            gameinfo.SetChild("nomodels", (switchModels.IsOn ? "0" : "1"));

            gameinfo.SetChild("developer", (textDeveloper.EditValue != null ? textDeveloper.EditValue.ToString() : ""));
            gameinfo.SetChild("developer_url", (textDeveloperURL.EditValue != null ? textDeveloperURL.EditValue.ToString() : ""));
            gameinfo.SetChild("manual", (textManual.EditValue != null ? textManual.EditValue.ToString() : ""));
            gameinfo.SetChild("icon", (textIcon.EditValue != null ? textIcon.EditValue.ToString() : ""));

            gameinfo.SetChild("nodegraph", (switchNodegraph.IsOn ? "1" : "0"));
            gameinfo.SetChild("gamedata", (textGamedata.EditValue != null ? textGamedata.EditValue.ToString() : ""));
            gameinfo.SetChild("instancepath", (textInstance.EditValue != null ? textInstance.EditValue.ToString() : ""));
            gameinfo.SetChild("supportsvr", (switchVR.IsOn ? "1" : "0"));

            int appID = sourceSDK.GetGameAppId(comboGames.EditValue.ToString());
            gameinfo.GetChild("filesystem").SetChild("steamappid", appID.ToString());

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