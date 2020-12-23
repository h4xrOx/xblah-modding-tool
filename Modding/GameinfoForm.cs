using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
 
using SourceSDK;
using SourceSDK.Materials;
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
    public partial class GameinfoForm : DevExpress.XtraEditors.XtraForm
    {
        KeyValue gameinfo;

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
            GetFieldValues();

            string path = "";
            switch (launcher.GetCurrentGame().engine)
            {
                case Engine.SOURCE:
                    path = modPath + "\\gameinfo.txt";
                    break;
                case Engine.SOURCE2:
                    path = modPath + "\\gameinfo.gi";
                    break;
                case Engine.GOLDSRC:
                    path = modPath + "\\liblist.gam";
                    break;

            }

            if (File.Exists(path))
                SourceSDK.KeyValue.writeChunkFile(path, gameinfo, false, new UTF8Encoding(false));

            Close();
        }

        Dictionary<string, BaseLayoutItem> fields;

        private void ShowFields()
        {
            fields = new Dictionary<string, BaseLayoutItem>();

            List<BaseLayoutItem> controls = new List<BaseLayoutItem>();
            controls.AddRange(layoutControl1.Items);
            controls.AddRange(layoutControl2.Items);
            controls.AddRange(layoutControl3.Items);
            controls.AddRange(layoutControl4.Items);

            foreach (BaseLayoutItem item in controls)
                if (item.Tag != null)
                    fields.Add(item.Tag.ToString(), item);

            foreach (BaseLayoutItem item in fields.Values)
                item.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            foreach(string field in launcher.GetCurrentGame().getGameinfoFields())
            {
                if (fields.ContainsKey(field))
                    fields[field].Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void SetFieldValues()
        {
            string modPath = launcher.GetCurrentMod().installPath;

            foreach (string field in launcher.GetCurrentGame().getGameinfoFields())
            {
                SourceSDK.KeyValue childKV = gameinfo.findChildByKey(field);
                if (childKV == null)
                    continue;

                if (!fields.ContainsKey(field))
                    continue;

                BaseEdit control = (fields[field] as LayoutControlItem).Control as BaseEdit;

                switch (field)
                {
                    case "type":
                        {
                            string type = childKV.getValue();
                            if (type == "multiplayer_only")
                            {
                                control.EditValue = "Multi-player";
                            }
                            else if (type == "singleplayer_only")
                            {
                                control.EditValue = "Single-player";
                            } else
                            {
                                control.EditValue = "Both";
                            }
                        }
                        break;
                    case "nodifficulty":
                        control.EditValue = (childKV.getValue() == "1" ? false : true);
                        break;
                    case "hasportals":
                        control.EditValue = (childKV.getValue() == "1" ? true : false);
                        break;
                    case "nocrosshair":
                        control.EditValue = (childKV.getValue() == "1" ? false : true);
                        break;
                    case "advcrosshair":
                        control.EditValue = (childKV.getValue() == "1" ? true : false);
                        break;
                    case "nomodels":
                        control.EditValue = (childKV.getValue() == "1" ? false : true);
                        break;
                    case "nodegraph":
                        control.EditValue = (childKV.getValue() == "0" ? false : true);
                        break;
                    case "supportsvr":
                        control.EditValue = (childKV.getValue() == "1" ? true : false);
                        break;
                    case "icon":
                        {
                            string icon = childKV.getValue();

                            if (File.Exists(modPath + "\\" + icon + ".tga"))
                                pictureEdit2.Image = new TGA(modPath + "\\" + icon + ".tga").ToBitmap();

                            if (File.Exists(modPath + "\\" + icon + "_big.tga"))
                                pictureEdit1.Image = new TGA(modPath + "\\" + icon + "_big.tga").ToBitmap();

                            pictureEdit1.Properties.ContextMenuStrip = new ContextMenuStrip();
                        }
                        break;
                    case "gamedata":
                        textGamedata.EditValue = childKV.getValue();
                        break;
                    case "instancepath":
                        textInstance.EditValue = childKV.getValue();
                        break;
                    default:
                        if (fields.ContainsKey(field) && (fields[field] as LayoutControlItem).Control is BaseEdit)
                            ((fields[field] as LayoutControlItem).Control as BaseEdit).EditValue = childKV.getValue();
                        break;
                }
            }
        }

        private void GetFieldValues()
        {
            string modPath = launcher.GetCurrentMod().installPath;

            foreach (string field in launcher.GetCurrentGame().getGameinfoFields())
            {
                SourceSDK.KeyValue childKV = gameinfo.findChildByKey(field);

                if (!fields.ContainsKey(field))
                    continue;

                BaseEdit control = (fields[field] as LayoutControlItem).Control as BaseEdit;

                switch (field)
                {
                    case "type":
                        {
                            string type;
                            if (textType.EditValue.ToString() == "Multi-player")
                            {
                                type = "multiplayer_only";
                            }
                            else if (textType.EditValue.ToString() == "Single-player")
                            {
                                type = "singleplayer_only";
                            }
                            else
                            {
                                type = "both";
                            }
                            gameinfo.setValue("type", type);
                        }
                        break; 
                    case "nodifficulty":
                        gameinfo.setValue(field, ((control as ToggleSwitch).IsOn ? "0" : "1"));
                        break;
                    case "hasportals":
                        gameinfo.setValue(field, ((control as ToggleSwitch).IsOn ? "1" : "0"));
                        break;
                    case "nocrosshair":
                        gameinfo.setValue(field, ((control as ToggleSwitch).IsOn ? "0" : "1"));
                        break;
                    case "advcrosshair":
                        gameinfo.setValue(field, ((control as ToggleSwitch).IsOn ? "1" : "0"));
                        break;
                    case "nomodels":
                        gameinfo.setValue(field, ((control as ToggleSwitch).IsOn ? "0" : "1"));
                        break;
                    case "nodegraph":
                        gameinfo.setValue(field, ((control as ToggleSwitch).IsOn ? "1" : "0"));
                        break;
                    case "supportsvr":
                        gameinfo.setValue(field, ((control as ToggleSwitch).IsOn ? "1" : "0"));
                        break;
                    case "icon":
                        {
                            gameinfo.setValue("icon", "resource/icon");

                            if (pictureEdit2.Image != null)
                                new TGA((Bitmap)pictureEdit2.Image).Save(modPath + "\\resource\\icon.tga");
                            else if (File.Exists(modPath + "\\resource\\icon.tga"))
                                File.Delete(modPath + "\\resource\\icon.tga");

                            if (pictureEdit1.Image != null)
                                new TGA((Bitmap)pictureEdit1.Image).Save(modPath + "\\resource\\icon_big.tga");
                            else if (File.Exists(modPath + "\\resource\\icon_big.tga"))
                                File.Delete(modPath + "\\resource\\icon_big.tga");
                        }
                        break;
                    case "gamedata":
                        gameinfo.setValue(field, (textGamedata.EditValue != null ? textGamedata.EditValue.ToString() : ""));
                        break;
                    case "instancepath":
                        gameinfo.setValue(field, (textInstance.EditValue != null ? textInstance.EditValue.ToString() : ""));
                        break;
                    default:
                        if ((fields[field] as LayoutControlItem).Control is BaseEdit)
                        {
                            BaseEdit baseEdit = (fields[field] as LayoutControlItem).Control as BaseEdit;
                            gameinfo.setValue(field, (baseEdit.EditValue != null ? baseEdit.EditValue.ToString() : ""));
                        }

                        
                        break;
                }
            }
        }

        private void GameinfoForm_Load(object sender, EventArgs e)
        {
            ShowFields();
            gameinfo = new GameInfo(launcher).getRoot();
            SetFieldValues();
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            if(dialogIcon.ShowDialog() == DialogResult.OK)
            {
                Bitmap original = new TGA(dialogIcon.FileName).ToBitmap();


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
                Bitmap original = new TGA(dialogIcon.FileName).ToBitmap();


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