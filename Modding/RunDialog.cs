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

namespace source_modding_tool.Modding
{
    public partial class RunDialog : DevExpress.XtraEditors.XtraForm
    {
        public RunPreset runPreset;
        public string commands;

        private Launcher launcher;

        private List<RunPreset> presets = new List<RunPreset>();
        private List<RunPreset> availablePresets = new List<RunPreset>();

        bool newPreset = false;

        string PRESET_PATH = AppDomain.CurrentDomain.BaseDirectory + "RunPresets.cfg";

        public RunDialog(Launcher launcher)
        {
            InitializeComponent();

            this.launcher = launcher;
            LoadPresets();
            splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
        }

        private void LoadPresets()
        {
            if (!File.Exists(PRESET_PATH) && File.Exists(PRESET_PATH.Replace(".cfg", "_default.cfg")))
                    File.Copy(PRESET_PATH.Replace(".cfg", "_default.cfg"), PRESET_PATH);

            SourceSDK.KeyValue root = SourceSDK.KeyValue.readChunkfile(PRESET_PATH);

            presets = new List<RunPreset>();
            availablePresets = new List<RunPreset>();
            presetCombo.Properties.Items.Clear();

            foreach (SourceSDK.KeyValue kv in root.getChildren())
            {
                RunPreset preset = new RunPreset();
                preset.name = (kv.getValue("name") != string.Empty ? kv.getValue("name") : kv.getKey());
                preset.engine = kv.getValue("engine");
                preset.game = kv.getValue("game");
                preset.mod = kv.getValue("mod");
                preset.exePath = kv.getValue("exe");
                preset.command = kv.getValue("command");
                preset.runMode = RunMode.FromString(kv.findChildByKey("runmode").getValue());

                presets.Add(preset);

                if (
                    (preset.mod == string.Empty || preset.mod == new DirectoryInfo(launcher.GetCurrentMod().installPath).Name) &&
                    (preset.game == string.Empty || preset.game == launcher.GetCurrentGame().name) &&
                    (preset.engine == string.Empty || preset.engine == Engine.ToString(launcher.GetCurrentGame().engine))
                )
                    availablePresets.Add(preset);
            }

            updatePresetCombo();
        }

        private void SavePresets()
        {
            SourceSDK.KeyValue root = new SourceSDK.KeyValue("Presets");
            foreach(RunPreset preset in presets)
            {
                SourceSDK.KeyValue presetKV = new SourceSDK.KeyValue(preset.name);
                presetKV.addChild("Name", preset.name);
                presetKV.addChild("Engine", preset.engine);
                presetKV.addChild("Game", preset.game);
                presetKV.addChild("Mod", preset.mod);
                presetKV.addChild("RunMode", RunMode.ToString(preset.runMode));
                presetKV.addChild("Exe", preset.exePath);
                presetKV.addChild("Command", preset.command);

                root.addChild(preset.name, presetKV);
            }

            SourceSDK.KeyValue.writeChunkFile(PRESET_PATH, root);
        }

        private RunPreset getSelectedPreset()
        {
            int index = presetCombo.Properties.Items.IndexOf(presetCombo.EditValue.ToString());
            if (index >= 0)
                return availablePresets[index];

            return null;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.runPreset = getSelectedPreset();
            this.commands = (commandText.EditValue != null ? commandText.EditValue.ToString() : "");
        }

        private void startEdit()
        {
            splitContainerControl1.PanelVisibility = SplitPanelVisibility.Both;
            okButton.Enabled = false;
            cancelButton.Enabled = false;
            editButton.Enabled = false;
            addButton.Enabled = false;
            deleteButton.Enabled = false;
            presetCombo.Enabled = false;
            commandText.Enabled = false;
        }

        private void endEdit()
        {
            splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
            okButton.Enabled = true;
            cancelButton.Enabled = true;
            editButton.Enabled = true;
            addButton.Enabled = true;
            deleteButton.Enabled = true;
            presetCombo.Enabled = true;
            commandText.Enabled = true;
        }

        private void presetActions_Click(object sender, EventArgs e)
        {
            if (sender == addButton)
            {
                newPreset = true;
                presetNameText.EditValue = "";
                presetRunModeCombo.EditValue = "Windowed";
                presetCoverageCombo.EditValue = "Global";
                presetExecutableText.EditValue = "";
                presetCommandText.EditValue = "";
                startEdit();
            }
            if (sender == editButton)
            {
                newPreset = false;

                RunPreset currentPreset = getSelectedPreset();
                presetNameText.EditValue = currentPreset.name;
                if (currentPreset.mod == new DirectoryInfo(launcher.GetCurrentMod().installPath).Name)
                {
                    presetCoverageCombo.EditValue = "Current mod only";
                }
                else if (currentPreset.game == launcher.GetCurrentGame().name)
                {
                    presetCoverageCombo.EditValue = "Current game only";
                }
                else if (currentPreset.engine == Engine.ToString(launcher.GetCurrentGame().engine))
                {
                    presetCoverageCombo.EditValue = "Current engine only";
                } else
                {
                    presetCoverageCombo.EditValue = "Global";
                }
                presetCommandText.EditValue = currentPreset.command;
                presetRunModeCombo.EditValue = RunMode.ToString(currentPreset.runMode);
                presetExecutableText.EditValue = (currentPreset.exePath != string.Empty ? currentPreset.exePath : "");

                startEdit();
            }
            if (sender == deleteButton)
            {
                RunPreset preset = getSelectedPreset();
                if (preset != null)
                {
                    presets.Remove(preset);
                    availablePresets.Remove(preset);

                    updatePresetCombo();
                    SavePresets();
                }
            }
        }

        private void presetOptions_Click(object sender, EventArgs e)
        {
            if (sender == presetSaveButton)
            {
                RunPreset preset;
                if (newPreset)
                {
                    preset = new RunPreset();
                    presets.Add(preset);
                    availablePresets.Add(preset);
                } else
                {
                    preset = getSelectedPreset();
                }
                
                preset.name = (presetNameText.EditValue != null ? presetNameText.EditValue.ToString() : "");
                switch(presetRunModeCombo.EditValue.ToString())
                {
                    case "Global":
                        preset.engine = "";
                        preset.game = "";
                        preset.mod = "";
                        break;
                    case "Current engine only":
                        preset.engine = Engine.ToString(launcher.GetCurrentGame().engine);
                        preset.game = "";
                        preset.mod = "";
                        break;
                    case "Current game only":
                        preset.engine = Engine.ToString(launcher.GetCurrentGame().engine);
                        preset.game = launcher.GetCurrentGame().name;
                        preset.mod = "";
                        break;
                    case "Current mod only":
                        preset.engine = Engine.ToString(launcher.GetCurrentGame().engine);
                        preset.game = launcher.GetCurrentGame().name;
                        preset.mod = new DirectoryInfo(launcher.GetCurrentMod().installPath).Name;
                        break;
                }
                preset.runMode = RunMode.FromString(presetRunModeCombo.EditValue.ToString());
                preset.exePath = (presetExecutableText.EditValue != null && presetExecutableText.EditValue.ToString() != "" ? presetExecutableText.EditValue.ToString() : "");
                preset.command = (presetCommandText.EditValue.ToString() != null ? presetCommandText.EditValue.ToString() : "");

                updatePresetCombo();
                presetCombo.EditValue = preset.name;

                SavePresets();
            }
            if (sender == presetCancelButton)
            {

            }
            endEdit();
        }

        private void updatePresetCombo()
        {
            presetCombo.Properties.Items.Clear();
            foreach (RunPreset runPreset in availablePresets)
            {
                presetCombo.Properties.Items.Add(runPreset.name);
            }
            if (availablePresets.Count > 0)
                presetCombo.EditValue = presets[0].name;
            else
                presetCombo.EditValue = "";

            editButton.Enabled = (availablePresets.Count > 0);
            deleteButton.Enabled = (availablePresets.Count > 0);
        }

        private void presetNameText_TextChanged(object sender, EventArgs e)
        {
            presetSaveButton.Enabled = presetNameText.EditValue != null && presetNameText.EditValue.ToString().Length > 0;
        }

        private void presetExecutableClearButton_Click(object sender, EventArgs e)
        {
            presetExecutableText.EditValue = "";
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            File.Delete(PRESET_PATH);
            LoadPresets();
        }

        private void presetExecutableBrowseButton_Click(object sender, EventArgs e)
        {
            XtraOpenFileDialog dialog = new XtraOpenFileDialog();
            dialog.Filter = "Executable Files (*.exe)|*.exe";
            dialog.InitialDirectory = (presetExecutableText.EditValue != null && presetExecutableText.EditValue.ToString() != "" ? presetExecutableText.EditValue.ToString() : launcher.GetCurrentGame().installPath);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                presetExecutableText.EditValue = dialog.FileName;
            }
        }
    }
}