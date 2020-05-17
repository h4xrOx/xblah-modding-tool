using source_modding_tool.SourceSDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace source_modding_tool
{
    public partial class GameinfoForm : DevExpress.XtraEditors.XtraForm
    {
        KeyValue gameinfo;

        //List<String[]> searchPaths;
        Launcher launcher;

        public GameinfoForm(Launcher launcher)
        {
            this.launcher = launcher;
            InitializeComponent();
        }

        private void buttonGamedata_Click(object sender, EventArgs e)
        {
            string hammerPath = launcher.GetCurrentGame().installPath + "\\bin\\";
            fgdDialog.InitialDirectory = hammerPath;
            if(fgdDialog.ShowDialog() == DialogResult.OK)
            {
                Uri path1 = new Uri(hammerPath);
                Uri path2 = new Uri(fgdDialog.FileName);
                Uri diff = path1.MakeRelativeUri(path2);
                textGamedata.EditValue = diff.OriginalString;
            }
        }

        private void buttonInstance_Click(object sender, EventArgs e)
        {
            string mapsrcPath = launcher.GetCurrentMod().installPath + "\\mapsrc\\";
            Directory.CreateDirectory(mapsrcPath);
            instanceDialog.SelectedPath = (textGamedata.EditValue.ToString() == string.Empty
                ? mapsrcPath
                : textGamedata.EditValue.ToString());
            if(instanceDialog.ShowDialog() == DialogResult.OK)
            {
                textInstance.EditValue = instanceDialog.SelectedPath;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string modPath = launcher.GetCurrentMod().installPath;

            gameinfo.setValue("game", textGame.EditValue != null ? textGame.EditValue.ToString() : string.Empty);
            gameinfo.setValue("title", textTitle.EditValue != null ? textTitle.EditValue.ToString() : string.Empty);
            gameinfo.setValue("title2", textTitle2.EditValue != null ? textTitle2.EditValue.ToString() : string.Empty);

            string type;
            if(textType.EditValue.ToString() == "Multi-player")
            {
                type = "multiplayer_only";
            } else if(textType.EditValue.ToString() == "Single-player")
            {
                type = "singleplayer_only";
            } else
            {
                type = "both";
            }
            gameinfo.setValue("type", type);

            gameinfo.setValue("nodifficulty", (switchDifficulty.IsOn ? "0" : "1"));
            gameinfo.setValue("hasportals", (switchPortals.IsOn ? "1" : "0"));
            gameinfo.setValue("nocrosshair", (switchCrosshair.IsOn ? "0" : "1"));
            gameinfo.setValue("advcrosshair", (switchAdvCrosshair.IsOn ? "1" : "0"));
            gameinfo.setValue("nomodels", (switchModels.IsOn ? "0" : "1"));

            gameinfo.setValue("developer",
                              textDeveloper.EditValue != null ? textDeveloper.EditValue.ToString() : string.Empty);
            gameinfo.setValue("developer_url",
                              textDeveloperURL.EditValue != null ? textDeveloperURL.EditValue.ToString() : string.Empty);
            gameinfo.setValue("manual", textManual.EditValue != null ? textManual.EditValue.ToString() : string.Empty);
            gameinfo.setValue("icon", "resource/icon");

            if(pictureEdit2.Image != null)
                new TGASharpLib.TGA((Bitmap)pictureEdit2.Image).Save(modPath + "\\resource\\icon.tga");
            else if(File.Exists(modPath + "\\resource\\icon.tga"))
                File.Delete(modPath + "\\resource\\icon.tga");

            if(pictureEdit1.Image != null)
                new TGASharpLib.TGA((Bitmap)pictureEdit1.Image).Save(modPath + "\\resource\\icon_big.tga");
            else if(File.Exists(modPath + "\\resource\\icon_big.tga"))
                File.Delete(modPath + "\\resource\\icon_big.tga");

            gameinfo.setValue("nodegraph", switchNodegraph.IsOn ? "1" : "0");
            gameinfo.setValue("gamedata",
                              textGamedata.EditValue != null ? textGamedata.EditValue.ToString() : string.Empty);
            gameinfo.setValue("instancepath",
                              textInstance.EditValue != null ? textInstance.EditValue.ToString() : string.Empty);
            gameinfo.setValue("supportsvr", switchVR.IsOn ? "1" : "0");

            //SourceSDK.KeyValue searchPathsKV = gameinfo.getChildByKey("filesystem").getChildByKey("searchpaths");
            //searchPathsKV.clearChildren();
            /*foreach(String[] searchPath in searchPaths)
            {
                searchPathsKV.addChild(new SourceSDK.KeyValue(searchPath[0], searchPath[1]));
            }*/

            string path = modPath + "\\gameinfo.txt";

            SourceSDK.KeyValue.writeChunkFile(path, gameinfo, false, new UTF8Encoding(false));

            Close();
        }

        private void GameinfoForm_Load(object sender, EventArgs e)
        {
            string modPath = launcher.GetCurrentMod().installPath;

            string gameinfoPath = modPath + "\\gameinfo.txt";

            gameinfo = SourceSDK.KeyValue.readChunkfile(gameinfoPath);

            textGame.EditValue = gameinfo.getValue("game");
            textTitle.EditValue = gameinfo.getValue("title");
            textTitle2.EditValue = gameinfo.getValue("title2");

            string type = gameinfo.getValue("type");
            if(type == "multiplayer_only")
            {
                textType.EditValue = "Multi-player";
            } else if(type == "singleplayer_only")
            {
                textType.EditValue = "Single-player";
            } else
            {
                textType.EditValue = "Both";
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

            if(File.Exists(modPath + "\\" + icon + ".tga"))
                pictureEdit2.Image = new TGASharpLib.TGA(modPath + "\\" + icon + ".tga").ToBitmap();

            if(File.Exists(modPath + "\\" + icon + "_big.tga"))
                pictureEdit1.Image = new TGASharpLib.TGA(modPath + "\\" + icon + "_big.tga").ToBitmap();

            switchNodegraph.EditValue = (gameinfo.getValue("nodegraph") == "0" ? false : true);
            textGamedata.EditValue = gameinfo.getValue("gamedata");
            textInstance.EditValue = gameinfo.getValue("instancepath");
            switchVR.EditValue = (gameinfo.getValue("supportsvr") == "1" ? true : false);

            pictureEdit1.Properties.ContextMenuStrip = new ContextMenuStrip();
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            if(dialogIcon.ShowDialog() == DialogResult.OK)
            {
                Bitmap original = new TGASharpLib.TGA(dialogIcon.FileName).ToBitmap();


                Bitmap large = new Bitmap(32, 32);
                Bitmap small = new Bitmap(16, 16);
                using(Graphics g = Graphics.FromImage(large))
                    g.DrawImage(original, 0, 0, 32, 32);

                using(Graphics g = Graphics.FromImage(small))
                    g.DrawImage(original, 0, 0, 16, 16);

                pictureEdit2.Image = small;
                pictureEdit1.Image = large;
            }
        }

        private void pictureIconLarge_Click(object sender, EventArgs e)
        {
            if(dialogIcon.ShowDialog() == DialogResult.OK)
            {
                Bitmap original = new TGASharpLib.TGA(dialogIcon.FileName).ToBitmap();


                Bitmap large = new Bitmap(32, 32);
                Bitmap small = new Bitmap(16, 16);
                using(Graphics g = Graphics.FromImage(large))
                    g.DrawImage(original, 0, 0, 32, 32);

                using(Graphics g = Graphics.FromImage(small))
                    g.DrawImage(original, 0, 0, 16, 16);

                pictureEdit2.Image = small;
                pictureEdit1.Image = large;
            }
        }
    }
}