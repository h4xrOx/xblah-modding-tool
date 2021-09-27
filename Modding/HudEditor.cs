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
using System.Diagnostics;
using DevExpress.XtraLayout;
using DevExpress.XtraTab;
using SourceSDK;

namespace source_modding_tool.Modding
{
    public partial class HudEditor : DevExpress.XtraEditors.XtraForm
    {
        Launcher launcher;
        Mod mod;

        Instance instance;

        KeyValue clientSchemeKV;
        KeyValue hudLayoutKV;

        bool requiresRestart = true;
        bool isUpdating = false;

        readonly Dictionary<string, string> align = new Dictionary<string, string>
        {
            { "", "Start" },
            { "r", "End" },
            { "c", "Middle" }
        };

        readonly Dictionary<string, string> paintBackgroundType = new Dictionary<string, string>
        {
            { "0", "Rectangle" },
            { "1", "Erased" },
            { "2", "Stadium" },
            { "3", "Gradient" }
        };

        public HudEditor(Launcher launcher, Mod mod)
        {
            InitializeComponent();

            this.launcher = launcher;
            this.mod = mod;
        }

        private void HudEditor2_Load(object sender, EventArgs e)
        {
            // Copy the preview map
            Directory.CreateDirectory(mod.InstallPath + "\\maps\\");
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\IngamePreviews\\maps\\hud_preview.bsp", mod.InstallPath + "\\maps\\hud_preview.bsp", true);

            startPreview();
            loadClientScheme();
            loadHudLayout();

            updatePages();
        }

        private void loadClientScheme()
        {
            string path = mod.InstallPath + "\\resource\\clientscheme.res";
            if (!File.Exists(path))
            {
                XtraMessageBox.Show("ClientScheme.res not found in \\resource\\");
                Close();
                return;
            }

            clientSchemeKV = KeyValue.readChunkfile(path, true);
        }

        private void loadHudLayout()
        {
            string path = mod.InstallPath + "\\scripts\\hudlayout.res";
            if (!File.Exists(path))
            {
                XtraMessageBox.Show("HudLayout.res not found in \\scripts\\");
                Close();
                return;
            }

            hudLayoutKV = KeyValue.readChunkfile(path, true);
        }

        private void updatePages()
        {
            isUpdating = true;
            foreach (XtraTabPage page in xtraTabControl1.TabPages)
            {
                foreach (LayoutControlGroup group in (page.Controls[0] as LayoutControl).Items.Where(i => i is LayoutControlGroup).Cast<LayoutControlGroup>().ToList())
                    updateGroup(group);
            }
            isUpdating = false;
        }

        private void updateGroup(LayoutControlGroup group)
        {
            foreach (dynamic subgroup in group.Items)
            {
                if (subgroup is LayoutControlGroup)
                    updateGroup(subgroup);
                else
                {
                    if (subgroup is EmptySpaceItem)
                        continue;

                    updateClientSchemes(subgroup.Control);
                    updateHudLayout(subgroup.Control);
                }
            }
        }

        private void updateClientSchemes(Control control)
        {
            if (control is ColorPickEdit)
            {
                ColorPickEdit colorPickEdit = control as ColorPickEdit;

                KeyValue colorKV = clientSchemeKV.findChildByKey(colorPickEdit.Tag.ToString());
                if (colorKV != null)
                {
                    string[] colorArray = colorKV.getValue().Split(' ');
                    colorPickEdit.Color = Color.FromArgb(int.Parse(colorArray[3]), int.Parse(colorArray[0]), int.Parse(colorArray[1]), int.Parse(colorArray[2]));
                }
            }
        }

        private void updateHudLayout(Control control)
        {
            string[] key = control.Tag.ToString().Split('.');

            KeyValue elementKV = hudLayoutKV.findChildByKey(key[0]);

            if (elementKV != null)
            {
                string valueStr = elementKV.getValue(key[1]);

                if (control is SpinEdit)
                {
                    SpinEdit spinEdit = control as SpinEdit;
                                    
                    int value = 0;

                    if (int.TryParse(valueStr, out value))
                        spinEdit.Value = value;
                    else if (int.TryParse(valueStr.Substring(1), out value))
                        spinEdit.Value = value;
                }
                else if(control is ComboBoxEdit)
                {
                    ComboBoxEdit comboBoxEdit = control as ComboBoxEdit;

                    switch(key[1])
                    {
                        case "xpos":
                        case "ypos":
                            string alignKey = "";
                            if (Char.IsLetter(valueStr[0]) && int.TryParse(valueStr.Substring(1), out _))
                            {
                                // It's a position value with a letter representing left, right, center, etc
                                alignKey = valueStr[0].ToString();
                            }

                            if (align.ContainsKey(alignKey))
                                comboBoxEdit.EditValue = align[alignKey];
                            else
                                comboBoxEdit.EditValue = "";
                            break;
                        case "paintbackgroundtype":
                            //if (valueStr != null)
                                comboBoxEdit.EditValue = paintBackgroundType[valueStr];
                            //else
                                //comboBoxEdit.EditValue = "";
                            break;
                        default:
                            comboBoxEdit.EditValue = valueStr;
                            break;
                    }

                }
            }
        }

        private void HudEditor2_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance.Stop();
        }

        private void clientSchemeColorPickEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (isUpdating)
                return;

            if (sender is ColorPickEdit)
            {
                ColorPickEdit colorPickEdit = sender as ColorPickEdit;
                string tag = colorPickEdit.Tag.ToString();
                string color = colorPickEdit.Color.R + " " + colorPickEdit.Color.G + " " + colorPickEdit.Color.B + " " + colorPickEdit.Color.A;

                KeyValue colorKV = clientSchemeKV.findChildByKey(tag);
                if (colorKV != null)
                    colorKV.setValue(color);

                requiresRestart = true;

                updatePages();
            }
        }

        private void hudLayoutSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (isUpdating)
                return;

            if (sender is SpinEdit)
            {
                SpinEdit spinEdit = sender as SpinEdit;
                string tag = spinEdit.Tag.ToString();

                string[] key = tag.Split('.');

                KeyValue elementKV = hudLayoutKV.findChildByKey(key[0]);
                if (elementKV != null)
                {
                    string oldValue = elementKV.getValue(key[1]);
                    string newValue = ((int)spinEdit.Value).ToString();

                    if (int.TryParse(oldValue, out _))
                    {
                        elementKV.setValue(key[1], newValue);
                    }
                    else if (int.TryParse(oldValue.Substring(1), out _))
                    {
                        elementKV.setValue(key[1], oldValue[0] + newValue);
                    }
                }

                updatePages();
            }
        }

        private void hudLayoutComboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdating)
                return;

            if (sender is ComboBoxEdit)
            {
                ComboBoxEdit comboBoxEdit = sender as ComboBoxEdit;
                string tag = comboBoxEdit.Tag.ToString();

                string[] key = tag.Split('.');

                string valueStr = comboBoxEdit.EditValue.ToString();

                KeyValue elementKV = hudLayoutKV.findChildByKey(key[0]);
                if (elementKV != null)
                {
                    switch (key[1])
                    {
                        case "xpos":
                        case "ypos":
                            {
                                string oldValue = elementKV.getValue(key[1]);
                                string newValue = valueStr;

                                int value = 0;
                                if (!int.TryParse(oldValue, out value))
                                    int.TryParse(oldValue.Substring(1), out value);

                                string dictionaryKey = align.FirstOrDefault(x => x.Value == newValue).Key;
                                elementKV.setValue(key[1], dictionaryKey + value);
                            }
                            break;
                        case "paintbackgroundtype":
                            {
                                string dictionaryKey = paintBackgroundType.FirstOrDefault(x => x.Value == valueStr).Key;
                                elementKV.setValue(key[1], dictionaryKey);
                            }
                            break;
                        default:
                            elementKV.setValue(key[1], valueStr);
                            break;
                    }

                }

                updatePages();
            }
        }

        private void startPreview()
        {
            if (instance != null)
                instance.Stop();

            playerHealthSpin.Value = 100;

            RunPreset runPreset = new RunPreset(RunMode.WINDOWED);
            instance = new Instance(launcher, panelControl1);

            requiresRestart = false;

            instance.Start(runPreset, "-nomouse -novid +map hud_preview");
        }

        private void playerHealthSpin_EditValueChanged(object sender, EventArgs e)
        {
            instance.Command("+ent_fire \"!player sethealth " + (int)playerHealthSpin.Value + "\"");
        }

        private void reloadHUD()
        {
            instance.Command("+hud_reloadscheme");
        }

        private void saveChangesButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KeyValue.writeChunkFile(mod.InstallPath + "\\resource\\clientscheme.res", clientSchemeKV, false, Encoding.UTF8);
            KeyValue.writeChunkFile(mod.InstallPath + "\\scripts\\hudlayout.res", hudLayoutKV, false, Encoding.UTF8);

            if (requiresRestart)
                startPreview();
            else
                reloadHUD();

        }

        private void HudEditor2_ResizeEnd(object sender, EventArgs e)
        {
            if (instance != null)
                instance.Resize();
        }
    }
}