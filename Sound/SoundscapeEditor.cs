using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using source_modding_tool.Modding;
using SourceSDK;
using SourceSDK.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;

namespace source_modding_tool.Sound
{
    public partial class SoundscapeEditor : DevExpress.XtraEditors.XtraForm
    {
        #region Variables
        PackageManager packageManager;
        PackageManager soundscapePackageManager;

        Launcher launcher;

        List<Soundscape> soundscapes = new List<Soundscape>();

        private Soundscape selectedSoundscape = null;
        private SoundscapeRule selectedSoundscapeRule = null;

        List<MediaPlayer> soundPlayers = new List<MediaPlayer>();
        List<System.Timers.Timer> timers = new List<System.Timers.Timer>();
        #endregion

        #region Constructors
        public SoundscapeEditor(Launcher launcher)
        {
            InitializeComponent();

            this.launcher = launcher;

            packageManager = new PackageManager(launcher, "sound");
            soundscapePackageManager = new PackageManager(launcher, "scripts");
        }

        public SoundscapeEditor(Launcher launcher, PackageFile file) : this(launcher)
        {
            LoadSoundscape(file);
        }
        #endregion

        #region UpdateTrees
        private void updateSoundscapesTree()
        {
            soundscapesTree.BeginUnboundLoad();
            soundscapesTree.Nodes.Clear();

            foreach (Soundscape soundscape in soundscapes)
            {
                TreeListNode node = soundscapesTree.AppendNode(new object[] { soundscape.name }, null);
                node.Tag = soundscape;
            }
            soundscapesTree.EndUnboundLoad();

            if (soundscapesTree.Selection.Count > 0)
            {
                selectedSoundscape = soundscapesTree.Selection[0].Tag as Soundscape;
            }

            UpdateSelectedSoundscape();
        }

        private void updateSoundscapeRulesTree()
        {
            rulesTree.BeginUnboundLoad();
            rulesTree.Nodes.Clear();

            if (selectedSoundscape != null)
            {
                foreach (SoundscapeRule soundscapeRule in selectedSoundscape.rules)
                {
                    TreeListNode node = rulesTree.AppendNode(new object[] { ruleToString(soundscapeRule.rule) }, null);
                    node.Tag = soundscapeRule;
                }
            }

            rulesTree.EndUnboundLoad();

            if (rulesTree.Selection.Count > 0)
            {
                selectedSoundscapeRule = rulesTree.Selection[0].Tag as SoundscapeRule;
            }

            UpdateSelectedSoundscapeRule();
        }

        private void updateWavesTree()
        {
            wavesTree.BeginUnboundLoad();
            wavesTree.Nodes.Clear();

            if (selectedSoundscapeRule != null)
            {
                foreach (PackageFile packageFile in selectedSoundscapeRule.wave)
                {
                    TreeListNode node = wavesTree.AppendNode(new object[] { packageFile.FullPath }, null);
                    node.Tag = packageFile;
                }
            }

            wavesTree.EndUnboundLoad();
        }

        private void UpdateSelectedSoundscape()
        {
            if (soundscapesTree.Selection.Count > 0 && soundscapesTree.Selection[0].Tag != null)
            {
                selectedSoundscape = soundscapesTree.Selection[0].Tag as Soundscape;

                nameEdit.Enabled = true;
                nameEdit.EditValue = selectedSoundscape.name;

                dspEdit.Enabled = true;
                dspEdit.EditValue = dspEdit.Properties.Items[selectedSoundscape.dsp];

                dspSpatialEdit.Enabled = true;
                dspSpatialEdit.EditValue = selectedSoundscape.dsp_spatial;

                dspVolumeSpin.Enabled = true;
                dspVolumeSpin.Value = (decimal)selectedSoundscape.dsp_volume;
            }
            else
            {
                selectedSoundscape = null;

                nameEdit.Enabled = false;
                dspEdit.Enabled = false;
                dspSpatialEdit.Enabled = false;
                dspVolumeSpin.Enabled = false;
                nameEdit.EditValue = "";
                dspEdit.EditValue = "";
                dspSpatialEdit.EditValue = "";
                dspVolumeSpin.EditValue = "";

                addRuleButton.Enabled = false;
            }

            updateSoundscapeRulesTree();
        }

        private void UpdateSelectedSoundscapeRule()
        {
            if (rulesTree.Selection.Count > 0 && rulesTree.Selection[0].Tag != null)
            {
                selectedSoundscapeRule = rulesTree.Selection[0].Tag as SoundscapeRule;

                switch (selectedSoundscapeRule.rule)
                {
                    case Soundscape.Rule.LOOPING:
                        typeCombo.EditValue = "Looping";
                        break;
                    case Soundscape.Rule.RANDOM:
                        typeCombo.EditValue = "Random";
                        break;
                    case Soundscape.Rule.SOUNDSCAPE:
                        typeCombo.EditValue = "Soundscape";
                        break;
                }

                minVolumeSpin.Value = Math.Round((decimal)selectedSoundscapeRule.volume.Item1, 4);
                maxVolumeSpin.Value = Math.Round((decimal)selectedSoundscapeRule.volume.Item2, 4);

                //System.Diagnostics.Debugger.Break();

                minPitchSpin.Value = (decimal)(selectedSoundscapeRule.pitch.Item1 / 100f);
                maxPitchSpin.Value = (decimal)(selectedSoundscapeRule.pitch.Item2 / 100f);

                minTimeSpin.Value = (decimal)(selectedSoundscapeRule.time.Item1);
                maxTimeSpin.Value = (decimal)(selectedSoundscapeRule.time.Item2);

                if (selectedSoundscapeRule.positionRandom == true)
                {
                    positionEdit.EditValue = positionEdit.Properties.Items[9];
                }
                else
                {
                    positionEdit.EditValue = positionEdit.Properties.Items[selectedSoundscapeRule.position + 1];
                }

                attenuationSpin.Value = (decimal)selectedSoundscapeRule.attenuation;
                soundlevelCombo.EditValue = selectedSoundscapeRule.soundlevel;
                

                typeCombo.Enabled = true;
                addWaveButton.Enabled = true;
            }
            else
            {
                selectedSoundscapeRule = null;

                typeCombo.EditValue = "";
                //fileEdit.EditValue = "";
                minVolumeSpin.Value = 0;
                maxVolumeSpin.Value = 0;

                typeCombo.Enabled = false;
                addWaveButton.Enabled = false;

            }

            updateWavesTree();
        }
        #endregion

        #region Preview

        private void StartPreview()
        {
            StopPreview();
            int i = 0;
            if (selectedSoundscape != null)
            {
                foreach (SoundscapeRule rule in selectedSoundscape.rules)
                {
                    if (rule.rule == Soundscape.Rule.LOOPING)
                    {
                        if (rule.wave.Count > 0)
                        {
                            File.WriteAllBytes("D:\\Files\\Desktop\\" + i + ".wav", rule.wave[0].Data);

                            MediaPlayer mediaPlayer = new MediaPlayer();
                            mediaPlayer.Open(new System.Uri("D:\\Files\\Desktop\\" + i + ".wav"));
                            mediaPlayer.Volume = rule.volume.Item1;
                            mediaPlayer.Play();
                            mediaPlayer.MediaEnded += delegate (object sender, EventArgs e2)
                            {
                                mediaPlayer.Position = TimeSpan.Zero;
                                mediaPlayer.Play();
                            };

                            soundPlayers.Add(mediaPlayer);
                        }
                        i++;

                    }
                    else if (rule.rule == Soundscape.Rule.RANDOM)
                    {
                        int startId = i;
                        int endId = i + rule.wave.Count;
                        foreach (var packageFile in rule.wave)
                        {
                            File.WriteAllBytes("D:\\Files\\Desktop\\" + i + ".wav", packageFile.Data);
                            i++;
                        }

                        System.Timers.Timer timer = new System.Timers.Timer(2 * 1000);
                        timer.Elapsed += (s, e) =>
                        {
                            Random rand = new Random();

                            Invoke(new Action(() =>
                            {
                                MediaPlayer mediaPlayer = new MediaPlayer();
                                mediaPlayer.Open(new System.Uri("D:\\Files\\Desktop\\" + rand.Next(startId, endId) + ".wav"));
                                mediaPlayer.Volume = rule.volume.Item1;
                                soundPlayers.Add(mediaPlayer);

                                mediaPlayer.Play();
                                mediaPlayer.MediaEnded += delegate (object sender, EventArgs e2)
                                {
                                    mediaPlayer.Close();
                                    soundPlayers.Remove(mediaPlayer);
                                };
                            }));


                            timer.Interval = rand.Next(rule.time.Item1, rule.time.Item2) * 1000;
                        };
                        timers.Add(timer);
                        timer.Start();
                    }

                    
                }
            }
        }

        private void StopPreview()
        {
            foreach (var timer in timers)
            {
                if (timer != null)
                    timer.Stop();
            }

            foreach (var mediaPlayer in soundPlayers)
            {

                if (mediaPlayer != null)
                    mediaPlayer.Close();
            }

            timers.Clear();
            soundPlayers.Clear();
        }
        #endregion

        #region Inputs

        private void soundscapeButton_Click(object sender, EventArgs e)
        {
            if (sender == addRuleButton)
            {
                if (selectedSoundscape != null)
                {
                    selectedSoundscape.rules.Add(new SoundscapeRule());
                    updateSoundscapeRulesTree();
                }
            }
            else if (sender == removeRuleButton)
            {
                if (rulesTree.Selection.Count > 0)
                {
                    rulesTree.BeginUnboundLoad();
                    for (int i = rulesTree.Selection.Count - 1; i >= 0; i--)
                    {
                        TreeListNode node = rulesTree.Selection[i];
                        selectedSoundscape.rules.Remove(node.Tag as SoundscapeRule);
                        node.Remove();
                    }
                    rulesTree.EndUnboundLoad();
                }
            }
            else if (sender == addSoundscapeButton)
            {
                Soundscape soundscape = new Soundscape();
                soundscapes.Add(soundscape);
                soundscapesTree.BeginUnboundLoad();
                TreeListNode node = soundscapesTree.AppendNode(new object[] { "" }, null);
                node.Tag = soundscape;
                soundscapesTree.EndUnboundLoad();
                soundscapesTree.SetFocusedNode(node);

                UpdateSelectedSoundscape();
            }
            else if (sender == removeSoundscapeButton)
            {
                if (soundscapesTree.Selection.Count > 0)
                {
                    soundscapesTree.BeginUnboundLoad();
                    for (int i = soundscapesTree.Selection.Count - 1; i >= 0; i--)
                    {
                        TreeListNode node = soundscapesTree.Selection[i];
                        soundscapes.Remove(node.Tag as Soundscape);
                        node.Remove();
                    }
                    soundscapesTree.EndUnboundLoad();
                }

                UpdateSelectedSoundscape();
            }
            else if (sender == addWaveButton)
            {
                FileExplorer fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.OPEN);
                fileExplorer.packageManager = packageManager;
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    selectedSoundscapeRule.wave.Add(fileExplorer.Selection[0]);
                    updateWavesTree();
                }
            }
            else if (sender == stopButton)
            {
                StopPreview();
            }
            else if (sender == playButton)
            {
                StartPreview();
            }
        }

        private void nameEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (selectedSoundscape != null)
            {
                selectedSoundscape.name = nameEdit.EditValue.ToString();
                soundscapesTree.Selection[0].SetValue("name", nameEdit.EditValue.ToString());
            }
        }

        private void dspEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (selectedSoundscape != null)
            {
                selectedSoundscape.dsp = dspEdit.Properties.Items.IndexOf(dspEdit.EditValue.ToString());
            }
        }

        private void typeCombo_EditValueChanged(object sender, EventArgs e)
        {
            if (selectedSoundscapeRule != null)
            {
                switch (typeCombo.EditValue)
                {
                    case "Looping":
                        {
                            selectedSoundscapeRule.rule = Soundscape.Rule.LOOPING;
                            break;
                        }
                    case "Random":
                        {
                            selectedSoundscapeRule.rule = Soundscape.Rule.RANDOM;
                            break;
                        }
                }

                rulesTree.Selection[0].SetValue("type", typeCombo.EditValue.ToString());
            }
        }

        private void minVolumeSpin_ValueChanged(object sender, EventArgs e)
        {
            if (selectedSoundscapeRule != null)
            {
                if (sender == minVolumeSpin)
                {
                    selectedSoundscapeRule.volume.Item1 = (double)(sender as SpinEdit).Value;
                }
                else if (sender == maxVolumeSpin)
                {
                    selectedSoundscapeRule.volume.Item2 = (double)(sender as SpinEdit).Value;
                }
            }
        }
        #endregion

        #region File

        private void menu_ItemClick(object sender, ItemClickEventArgs e)
        {
            // New soundscape
            if (e.Item == menuFileNew)
            {
                // New file
            }

            // Load material
            else if (e.Item == menuFileOpen)
            {
                FileExplorer fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.OPEN)
                {
                    packageManager = soundscapePackageManager,
                    RootDirectory = "scripts",
                    Filter = "Valve Soundscape Files (soundscapes_*.txt)|soundscapes_*.txt"
                };
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    if (fileExplorer.Selection != null)
                        foreach (PackageFile file in fileExplorer.Selection)
                        {
                            // Load file
                            LoadSoundscape(file);
                        }
                }
            }

            // Save material
            else if (e.Item == menuFileSave)
            {
                //if (activeTab.relativePath != "")
                // Save file

                //else
                //menuFileSaveAs.PerformClick();
            }

            // Save material as
            else if (e.Item == menuFileSaveAs)
            {
                XtraSaveFileDialog dialog = new XtraSaveFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Save file

                }
            }
            else if (e.Item == menuFileExit)
            {
                Close();
            }
        }

        private void LoadSoundscape(PackageFile file)
        {
            soundscapes.Clear();

            string data = "\"soundscapes\"\n{\n" + System.Text.Encoding.UTF8.GetString(file.Data) + "\n}";

            string fullPath = "";

            SourceSDK.KeyValue soundscapeFile = SourceSDK.KeyValue.ReadChunk(data);

            foreach (KeyValue soundscapeKV in soundscapeFile.getChildren())
            {
                if (soundscapeKV.getKey() == "")
                    continue;

                Soundscape soundscape = new Soundscape()
                {
                    name = soundscapeKV.getKey()
                };

                KeyValue dspKV = soundscapeKV.getChildByKey("dsp");
                if (dspKV != null)
                    soundscape.dsp = int.Parse(dspKV.getValue());
                else
                    soundscape.dsp = 0;

                KeyValue dspSpatialKV = soundscapeKV.getChildByKey("dsp_spatial");
                if (dspSpatialKV != null)
                    soundscape.dsp_spatial = int.Parse(dspSpatialKV.getValue());
                else
                    soundscape.dsp_spatial = 0;

                KeyValue dspVolumeKV = soundscapeKV.getChildByKey("dspVolume");
                if (dspVolumeKV != null)
                    soundscape.dsp_volume = float.Parse(dspVolumeKV.getValue());
                else
                    soundscape.dsp_volume = 1;

                soundscapes.Add(soundscape);

                foreach (KeyValue soundscapeRuleKV in soundscapeKV.getChildren())
                {
                    if (soundscapeRuleKV.getKey() == "playrandom" || soundscapeRuleKV.getKey() == "playlooping") {
                        SoundscapeRule soundscapeRule = new SoundscapeRule();
                        switch (soundscapeRuleKV.getKey())
                        {
                            case "playrandom":
                                soundscapeRule.rule = Soundscape.Rule.RANDOM;
                                break;
                            case "playlooping":
                                soundscapeRule.rule = Soundscape.Rule.LOOPING;
                                break;
                            case "playsoundscape":
                                soundscapeRule.rule = Soundscape.Rule.SOUNDSCAPE;
                                break;
                        }

                        KeyValue timeKV = soundscapeRuleKV.getChildByKey("time");
                        if (timeKV != null)
                            soundscapeRule.time = ((int)float.Parse(timeKV.getValue().Split(',').First()), (int)float.Parse(timeKV.getValue().Split(',').Last()));
                        else
                            soundscapeRule.time = (1, 1);

                        KeyValue volumeKV = soundscapeRuleKV.getChildByKey("volume");
                        if (volumeKV != null)
                            soundscapeRule.volume = (float.Parse(volumeKV.getValue().Split(',').First()), float.Parse(volumeKV.getValue().Split(',').Last()));
                        else
                            soundscapeRule.volume = (1, 1);

                        KeyValue pitchKV = soundscapeRuleKV.getChildByKey("pitch");
                        if (pitchKV != null)
                            soundscapeRule.pitch = ((int)float.Parse(pitchKV.getValue().Split(',').First()), (int)float.Parse(pitchKV.getValue().Split(',').Last()));
                        else
                            soundscapeRule.pitch = (100, 100);

                        KeyValue positionKV = soundscapeRuleKV.getChildByKey("position");
                        if (positionKV != null)
                        {
                            soundscapeRule.positionRandom = (positionKV.getValue() == "random");
                            soundscapeRule.position = (positionKV.getValue() != "random" ? int.Parse(positionKV.getValue()) : -1);
                        }
                        else
                        {
                            soundscapeRule.position = -1;
                            soundscapeRule.positionRandom = false;
                        }

                        KeyValue attenuationKV = soundscapeRuleKV.getChildByKey("attenuation");
                        if (attenuationKV != null)
                            soundscapeRule.attenuation = float.Parse(attenuationKV.getValue());
                        else
                            soundscapeRule.attenuation = 1;

                        KeyValue soundlevelKV = soundscapeRuleKV.getChildByKey("soundlevel");
                        if (soundlevelKV != null)
                            soundscapeRule.soundlevel = soundlevelKV.getValue();
                        else
                            soundscapeRule.soundlevel = "SNDLVL_NONE";

                        KeyValue waveKV = soundscapeRuleKV.getChildByKey("wave");
                        if (waveKV != null)
                        {
                            string fileName = waveKV.getValue();
                            if (fileName.StartsWith("*"))
                                fileName = fileName.Substring(1);

                            PackageFile packageFile = packageManager.GetFile("sound\\" + fileName);
                            soundscapeRule.wave.Add(packageFile);
                        }

                        KeyValue rndWaveKV = soundscapeRuleKV.getChildByKey("rndwave");
                        if (rndWaveKV != null)
                        {
                            foreach (KeyValue childWaveKV in rndWaveKV.getChildren())
                            {
                                if (childWaveKV.getKey() == "")
                                    continue;

                                string fileName = childWaveKV.getValue();
                                if (fileName.StartsWith("*"))
                                    fileName = fileName.Substring(1);

                                PackageFile packageFile = packageManager.GetFile("sound\\" + fileName);
                                soundscapeRule.wave.Add(packageFile);
                            }
                        }

                        //System.Diagnostics.Debugger.Break();

                        soundscape.rules.Add(soundscapeRule);
                    }
                }
            }

            updateSoundscapesTree();

        }
        #endregion

        #region Utils
        private string ruleToString(Soundscape.Rule rule)
        {
            switch (rule)
            {
                case Soundscape.Rule.LOOPING:
                    return "Looping";
                case Soundscape.Rule.RANDOM:
                    return "Random";
                case Soundscape.Rule.SOUNDSCAPE:
                    return "Soundscape";
            }

            return "";
        }
        #endregion

        #region Misc
        private void soundscapesTree_Click(object sender, EventArgs e)
        {
            UpdateSelectedSoundscape();
        }

        private void soundscapesTree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            UpdateSelectedSoundscape();
        }

        private void soundscapeRulesTree_Click(object sender, EventArgs e)
        {
            UpdateSelectedSoundscapeRule();
        }

        private void soundscapeRulesTree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            UpdateSelectedSoundscapeRule();
        }

        #endregion
    }
}