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
using source_modding_tool.SourceSDK;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraLayout;

namespace source_modding_tool.Modding
{
    public partial class HudEditor2 : DevExpress.XtraEditors.XtraForm
    {
        Launcher launcher;
        Mod mod;

        Instance instance;

        KeyValue clientSchemeKV;

        public HudEditor2(Launcher launcher, Mod mod)
        {
            InitializeComponent();

            this.launcher = launcher;
            this.mod = mod;
        }

        private void HudEditor2_Load(object sender, EventArgs e)
        {
            startPreview();
            loadClientScheme();
        }

        private void loadClientScheme()
        {
            if (!File.Exists(mod.installPath + "\\resource\\clientscheme.res"))
            {
                XtraMessageBox.Show("ClientScheme.res not found in /resource/");
                Close();
            }

            clientSchemeKV = KeyValue.readChunkfile(mod.installPath + "\\resource\\clientscheme.res", true);

            foreach(dynamic group in baseSettingsLayout.Items)
            {
                if (group is LayoutControlGroup) {
                    foreach (dynamic item in (group as LayoutControlGroup).Items)
                    {

                        if (item is LayoutControlGroup)
                        {
                            LayoutControlGroup group2 = item as LayoutControlGroup;

                            foreach (dynamic item2 in group2.Items)
                            {

                                if (item2.Control is ColorPickEdit)
                                {
                                    ColorPickEdit colorPickEdit = item2.Control as ColorPickEdit;

                                    KeyValue colorKV = clientSchemeKV.findChildByKey(colorPickEdit.Tag.ToString());

                                    string[] colorArray = colorKV.getValue().Split(' ');

                                    colorPickEdit.Color = Color.FromArgb(int.Parse(colorArray[3]), int.Parse(colorArray[0]), int.Parse(colorArray[1]), int.Parse(colorArray[2]));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HudEditor2_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance.Stop();
        }

        private void colorPickEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is ColorPickEdit)
            {
                ColorPickEdit colorPickEdit = sender as ColorPickEdit;
                string tag = colorPickEdit.Tag.ToString();
                string color = colorPickEdit.Color.R + " " + colorPickEdit.Color.G + " " + colorPickEdit.Color.B + " " + colorPickEdit.Color.A;

                KeyValue colorKV = clientSchemeKV.findChildByKey(tag);
                if (colorKV != null)
                    colorKV.setValue(color);
            }
            
        }

        private void startPreview()
        {
            if (instance != null)
                instance.Stop();

            playerHealthSpin.Value = 100;

            RunPreset runPreset = new RunPreset(RunMode.WINDOWED);
            instance = new Instance(launcher, panelControl1);

            instance.Start(runPreset, "-nomouse -novid +map hud_preview +sv_cheats 1 +ent_fire \"!player sethealth " + (int)playerHealthSpin.Value + "\"");
        }

        private void playerHealthSpin_EditValueChanged(object sender, EventArgs e)
        {
            instance.Command("+ent_fire \"!player sethealth " + (int)playerHealthSpin.Value + "\"");
        }

        private void reloadButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            instance.Command("+hud_reloadscheme");
        }

        private void restartButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            startPreview();
        }

        private void saveChangesButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KeyValue.writeChunkFile(mod.installPath + "\\resource\\clientscheme.res", clientSchemeKV, false, Encoding.UTF8);
        }

        private void HudEditor2_ResizeEnd(object sender, EventArgs e)
        {
            if (instance != null)
                instance.Resize();

            //instance.Command("-w " + panelControl1.Width + " -h " + panelControl1.Height + " +mat_setvideomode \"" + panelControl1.Width + " " + panelControl1.Height + " 1\"");
        }
    }
}