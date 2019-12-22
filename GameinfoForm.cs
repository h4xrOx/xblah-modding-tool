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
using static windows_source1ide.Steam;
using System.Diagnostics;
using System.IO;

namespace windows_source1ide
{
    public partial class GameinfoForm : DevExpress.XtraEditors.XtraForm
    {
        string game;
        string mod;
        Steam sourceSDK;
        SourceSDK.KeyValue gameinfo;

        public GameinfoForm(string game, string mod)
        {
            this.game = game;
            this.mod = mod;

            InitializeComponent();
        }

        private void GameinfoForm_Load(object sender, EventArgs e)
        {
            sourceSDK = new Steam();
            string path = sourceSDK.GetMods(game)[mod] + "\\gameinfo.txt";

            gameinfo = SourceSDK.KeyValue.readChunkfile(path);

            textGame.EditValue = gameinfo.getValue("game");
            textTitle.EditValue = gameinfo.getValue("title");
            textTitle2.EditValue = gameinfo.getValue("title2");

            string type = gameinfo.getValue("type");
            if (type == "multiplayer_only")
            {
                textType.EditValue = "Multi-player";
            } else
            {
                textType.EditValue = "Single-player";
            }
            switchDifficulty.EditValue = (gameinfo.getValue("nodifficulty") == "1" ? false : true);
            switchPortals.EditValue = (gameinfo.getValue("hasportals") == "1" ? true : false);
            switchCrosshair.EditValue = (gameinfo.getValue("nocrosshair") == "1" ? false : true);
            switchAdvCrosshair.EditValue = (gameinfo.getValue("advcrosshair") == "1" ? true : false);
            switchModels.EditValue = (gameinfo.getValue("nomodels") == "1" ? false : true);

            textDeveloper.EditValue = gameinfo.getValue("developer");
            textDeveloperURL.EditValue = gameinfo.getValue("developer_url");
            textManual.EditValue = gameinfo.getValue("manual");
            string icon = gameinfo.getValue("icon");

            if (File.Exists(sourceSDK.GetMods(game)[mod] + "\\" + icon + ".tga"))
                pictureIconSmall.Image = new TGASharpLib.TGA(sourceSDK.GetMods(game)[mod] + "\\" + icon + ".tga").ToBitmap();

            if (File.Exists(sourceSDK.GetMods(game)[mod] + "\\" + icon + "_big.tga"))
                pictureIconLarge.Image = new TGASharpLib.TGA(sourceSDK.GetMods(game)[mod] + "\\" + icon + "_big.tga").ToBitmap();

            switchNodegraph.EditValue = (gameinfo.getValue("nodegraph") == "1" ? true : false);
            textGamedata.EditValue = gameinfo.getValue("gamedata");
            textInstance.EditValue = gameinfo.getValue("instancepath");
            switchVR.EditValue = (gameinfo.getValue("supportsvr") == "1" ? true : false);

            comboGames.Properties.Items.Clear();
            string appID = gameinfo.getChild("filesystem").getValue("steamappid");
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
            gameinfo.setValue("game", textGame.EditValue != null ? textGame.EditValue.ToString() : "");
            gameinfo.setValue("title", textTitle.EditValue != null ? textTitle.EditValue.ToString() : "");
            gameinfo.setValue("title2", textTitle2.EditValue != null ? textTitle2.EditValue.ToString() : "");

            string type;
            if (textType.EditValue.ToString() == "Multi-player")
            {
                type = "multiplayer_only";
            } else
            {
                type = "singleplayer_only";
            }
            gameinfo.setValue("type", type);

            gameinfo.setValue("nodifficulty", (switchDifficulty.IsOn ? "0" : "1"));
            gameinfo.setValue("hasportals", (switchPortals.IsOn ? "1" : "0"));
            gameinfo.setValue("nocrosshair", (switchCrosshair.IsOn ? "0" : "1"));
            gameinfo.setValue("advcrosshair", (switchAdvCrosshair.IsOn ? "1" : "0"));
            gameinfo.setValue("nomodels", (switchModels.IsOn ? "0" : "1"));

            gameinfo.setValue("developer", textDeveloper.EditValue != null ? textDeveloper.EditValue.ToString() : "");
            gameinfo.setValue("developer_url", textDeveloperURL.EditValue != null ? textDeveloperURL.EditValue.ToString() : "");
            gameinfo.setValue("manual", textManual.EditValue != null ? textManual.EditValue.ToString() : "");
            gameinfo.setValue("icon", "resource/icon");

            new TGASharpLib.TGA((Bitmap) pictureIconSmall.Image).Save(sourceSDK.GetMods(game)[mod] + "\\resource\\icon.tga");
            new TGASharpLib.TGA((Bitmap) pictureIconLarge.Image).Save(sourceSDK.GetMods(game)[mod] + "\\resource\\icon_big.tga");

            gameinfo.setValue("nodegraph", switchNodegraph.IsOn ? "1" : "0");
            gameinfo.setValue("gamedata", textGamedata.EditValue != null ? textGamedata.EditValue.ToString() : "");
            gameinfo.setValue("instancepath", textInstance.EditValue != null ? textInstance.EditValue.ToString() : "");
            gameinfo.setValue("supportsvr", switchVR.IsOn ? "1" : "0");

            int appID = sourceSDK.GetGameAppId(comboGames.EditValue.ToString());
            gameinfo.getChild("filesystem").setValue("steamappid", appID.ToString());

            string path = sourceSDK.GetMods(game)[mod] + "\\gameinfo.txt";

            SourceSDK.KeyValue.writeChunkFile(path, gameinfo, false, new UTF8Encoding(false));

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonIcon_Click(object sender, EventArgs e)
        {
 
        }

        private void pictureIconLarge_Click(object sender, EventArgs e)
        {
            if (dialogIcon.ShowDialog() == DialogResult.OK)
            {
                Bitmap original = new TGASharpLib.TGA(dialogIcon.FileName).ToBitmap();


                Bitmap large = new Bitmap(32, 32);
                Bitmap small = new Bitmap(16, 16);
                using (Graphics g = Graphics.FromImage(large))
                    g.DrawImage(original, 0, 0, 32, 32);

                using (Graphics g = Graphics.FromImage(small))
                    g.DrawImage(original, 0, 0, 16, 16);

                pictureIconSmall.Image = small;
                pictureIconLarge.Image = large;
            }
        }
    }
}