using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using xblah_modding_tool.Modding;
using xblah_modding_lib;
using xblah_modding_lib.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;

namespace xblah_modding_tool.Sound
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
        private bool isPreviewing = false;

        private bool isUpdatingRulesTree = false;
        #endregion

        #region Constructors
        public SoundscapeEditor(Launcher launcher)
        {
            InitializeComponent();

            this.launcher = launcher;

            packageManager = new PackageManager(launcher, "sound");
            soundscapePackageManager = new PackageManager(launcher, "scripts");
        }

        #endregion

        #region Properties

        private string relativePath = "";
        public string RelativePath
        {
            get
            {
                return relativePath;
            }
            set
            {
                relativePath = value;
                menuFileSave.Enabled = (relativePath != "");
            }
        }

        public PackageFile PackageFile { get; set; } = null;

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
                removeSoundscapeButton.Enabled = true;
            } else
            {
                removeSoundscapeButton.Enabled = false;
            }

            UpdateSelectedSoundscape();
        }

        private void updateSoundscapeRulesTree()
        {
            if (isUpdatingRulesTree)
                return;

            isUpdatingRulesTree = true; 

            rulesTree.BeginUnboundLoad();
            rulesTree.Nodes.Clear();

            if (selectedSoundscape != null)
            {
                foreach (SoundscapeRule soundscapeRule in selectedSoundscape.rules)
                {
                    string rule = typeCombo.Properties.Items[(int)soundscapeRule.rule].ToString();
                    string position;
                    if (soundscapeRule.positionRandom == true)
                    {
                        position = positionCombo.Properties.Items[9].ToString();
                    }
                    else
                    {
                        position = positionCombo.Properties.Items[soundscapeRule.position + 1].ToString();
                    }

                    List<string> waves = new List<string>();
                    foreach(PackageFile packageFile in soundscapeRule.wave)
                    {
                        if (packageFile != null)
                        {
                            waves.Add(packageFile.Filename);
                        }
                    }
                    TreeListNode node = rulesTree.AppendNode(new object[] { rule, position , string.Join(", ", waves) }, null);
                    node.Tag = soundscapeRule;
                }
            }

            rulesTree.EndUnboundLoad();

            if (rulesTree.Selection.Count > 0)
            {
                selectedSoundscapeRule = rulesTree.Selection[0].Tag as SoundscapeRule;
            }

            UpdateSelectedSoundscapeRule();

            isUpdatingRulesTree = false;
        }

        private void updateWavesTree()
        {
            wavesTree.BeginUnboundLoad();
            wavesTree.Nodes.Clear();

            if (selectedSoundscapeRule != null)
            {
                foreach (PackageFile packageFile in selectedSoundscapeRule.wave)
                {
                    TreeListNode node = wavesTree.AppendNode(new object[] { packageFile?.FullPath ?? "file not found" }, null);
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

                addRuleButton.Visibility = BarItemVisibility.Always;
                playButton.Visibility = BarItemVisibility.Always;

                soundscapeInfoDock.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                rulesDock.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;

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

                addRuleButton.Visibility = BarItemVisibility.Never;
                playButton.Visibility = BarItemVisibility.Never;

                soundscapeInfoDock.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                rulesDock.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                ruleInfoDock.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                wavesDock.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
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

                minPitchSpin.Value = (decimal)(selectedSoundscapeRule.pitch.Item1);
                maxPitchSpin.Value = (decimal)(selectedSoundscapeRule.pitch.Item2);

                minTimeSpin.Value = (decimal)(selectedSoundscapeRule.time.Item1);
                maxTimeSpin.Value = (decimal)(selectedSoundscapeRule.time.Item2);

                if (selectedSoundscapeRule.positionRandom == true)
                {
                    positionCombo.EditValue = positionCombo.Properties.Items[9];
                }
                else
                {
                    positionCombo.EditValue = positionCombo.Properties.Items[selectedSoundscapeRule.position + 1];
                }

                if (selectedSoundscapeRule.soundlevel != "")
                {
                    if (int.TryParse(selectedSoundscapeRule.soundlevel, out int soundlevelInt))
                    {
                        soundlevelCombo.SelectedIndex = 0;
                        attenuationSpin.Value = (decimal)soundlevelInt;
                        attenuationSpin.Enabled = true;

                        if (soundlevelInt != 0)
                        {
                            System.Diagnostics.Debugger.Break();
                        }

                        selectedSoundscapeRule.soundlevel = "";
                        selectedSoundscapeRule.attenuation = soundlevelInt;
                    }
                    else
                    {
                        for (int i = 0; i < soundlevelCombo.Properties.Items.Count; i++)
                        {
                            if (soundlevelCombo.Properties.Items[i].ToString().ToUpper().StartsWith(selectedSoundscapeRule.soundlevel.ToUpper()))
                            {
                                soundlevelCombo.SelectedIndex = i;
                                break;
                            }

                            if (i == soundlevelCombo.Properties.Items.Count - 1)
                            {
                                System.Diagnostics.Debugger.Break();
                            }
                        }

                        attenuationSpin.Value = 1;
                        attenuationSpin.Enabled = false;
                    }
                   
                } else
                {
                    soundlevelCombo.SelectedIndex = 0;
                    attenuationSpin.Value = (decimal)selectedSoundscapeRule.attenuation;
                    attenuationSpin.Enabled = true;
                }

                ruleNameEdit.EditValue = selectedSoundscapeRule.name;
                

                typeCombo.Enabled = true;
                addWaveButton.Enabled = true;

                wavesDock.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                ruleInfoDock.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
            else
            {
                selectedSoundscapeRule = null;

                typeCombo.EditValue = "";
                minVolumeSpin.Value = 0;
                maxVolumeSpin.Value = 0;

                typeCombo.Enabled = false;
                addWaveButton.Enabled = false;

                ruleInfoDock.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                wavesDock.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            }

            updateWavesTree();
        }

        private void UpdateSelectedWave()
        {
            if (wavesTree.Selection.Count > 0)
            {
                removeWaveButton.Enabled = true;
            }
            else
            {
                removeWaveButton.Enabled = false;
            }
        }
        #endregion

        #region Preview

        private void PreviewSoundscape(Soundscape soundscape, ref int i, double volumeMultiplier)
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\temp\\");
            foreach (SoundscapeRule rule in soundscape.rules)
            {
                if (rule.rule == Soundscape.Rule.LOOPING)
                {
                    if (rule.wave.Count > 0)
                    {
                        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\temp\\" + i + ".wav", rule.wave[0].Data);

                        MediaPlayer mediaPlayer = new MediaPlayer();
                        mediaPlayer.Open(new System.Uri(AppDomain.CurrentDomain.BaseDirectory + "\\temp\\" + i + ".wav"));
                        mediaPlayer.Volume = rule.volume.Item1 * volumeMultiplier;
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
                        File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "\\temp\\" + i + ".wav", packageFile.Data);
                        i++;
                    }

                    System.Timers.Timer timer = new System.Timers.Timer(2 * 1000);
                    timer.Elapsed += (s, e) =>
                    {
                        Random rand = new Random();

                        Invoke(new Action(() =>
                        {
                            MediaPlayer mediaPlayer = new MediaPlayer();
                            mediaPlayer.Open(new System.Uri(AppDomain.CurrentDomain.BaseDirectory + "\\temp\\" + rand.Next(startId, endId) + ".wav"));
                            mediaPlayer.Volume = rule.volume.Item1 * volumeMultiplier;
                            soundPlayers.Add(mediaPlayer);

                            mediaPlayer.Play();
                            mediaPlayer.MediaEnded += delegate (object sender, EventArgs e2)
                            {
                                mediaPlayer.Close();
                                soundPlayers.Remove(mediaPlayer);
                            };
                        }));


                        timer.Interval = rand.Next(Math.Max(rule.time.Item1, 1), Math.Max(rule.time.Item2, 1)) * 1000;
                    };
                    timers.Add(timer);
                    timer.Start();
                } else if (rule.rule == Soundscape.Rule.SOUNDSCAPE)
                {
                    foreach(Soundscape subscape in soundscapes)
                    {
                        if (subscape.name == rule.name)
                        {
                            PreviewSoundscape(subscape, ref i, rule.volume.Item1);
                        }
                    }
                }
            }
        }

        private void StartPreview()
        {
            StopPreview();
            int i = 0;
            if (selectedSoundscape != null)
            {
                PreviewSoundscape(selectedSoundscape, ref i, 1);

                isPreviewing = true;
                stopButton.Enabled = true;
                playButton.Enabled = false;
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
            isPreviewing = false;
            stopButton.Enabled = false;

            if (selectedSoundscape != null)
                playButton.Enabled = true;

            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\temp\\"))
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\temp\\", true);
        }
        #endregion

        #region Inputs

        private void soundscapeButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item == addSoundscapeButton)
            {
                Soundscape soundscape = new Soundscape();
                soundscapes.Add(soundscape);
                soundscapesTree.BeginUnboundLoad();
                TreeListNode node = soundscapesTree.AppendNode(new object[] { "" }, null);
                node.Tag = soundscape;
                soundscapesTree.EndUnboundLoad();
                soundscapesTree.SetFocusedNode(node);
                removeSoundscapeButton.Enabled = true;

                UpdateSelectedSoundscape();
            }
            else if (e.Item == removeSoundscapeButton)
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

                    if (soundscapes.Count == 0)
                        removeSoundscapeButton.Enabled = false;
                }

                UpdateSelectedSoundscape();
            }
            else if (e.Item == stopButton)
            {
                StopPreview();
            }
            else if (e.Item == playButton)
            {
                StartPreview();
            }
            else if (e.Item == addWaveButton)
            {
                FileExplorer fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.OPEN);
                fileExplorer.packageManager = packageManager;
                fileExplorer.RootDirectory = "sound";
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    selectedSoundscapeRule.wave.Add(fileExplorer.Selection[0]);

                    wavesTree.BeginUnboundLoad();
                    TreeListNode node = wavesTree.AppendNode(new object[] { fileExplorer.Selection[0]?.FullPath ?? "file not found" }, null);
                    node.Tag = fileExplorer.Selection[0];
                    wavesTree.EndUnboundLoad();
                    removeWaveButton.Enabled = true;
                }
            }
            else if (e.Item == removeWaveButton)
            {
                if (selectedSoundscapeRule != null && wavesTree.Selection.Count > 0)
                {
                    wavesTree.BeginUnboundLoad();
                    for (int i = wavesTree.Selection.Count - 1; i >= 0; i--)
                    {
                        TreeListNode node = wavesTree.Selection[i];
                        selectedSoundscapeRule.wave.Remove(node.Tag as PackageFile);
                        node.Remove();
                    }
                    wavesTree.EndUnboundLoad();

                    if (selectedSoundscapeRule.wave.Count == 0)
                        removeWaveButton.Enabled = false;
                }
            }
            else if (e.Item == addRuleButton)
            {
                if (selectedSoundscape != null)
                {
                    selectedSoundscape.rules.Add(new SoundscapeRule());
                    updateSoundscapeRulesTree();
                }
            }
            else if (e.Item == removeRuleButton)
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
                    case "Soundscape":
                        {
                            selectedSoundscapeRule.rule = Soundscape.Rule.SOUNDSCAPE;
                            break;
                        }
                }

                rulesTree.Selection[0].SetValue("type", typeCombo.EditValue.ToString());
            }
        }

        private void ruleInfoSpin_ValueChanged(object sender, EventArgs e)
        {
            if (selectedSoundscapeRule != null)
            {
                if (sender == minVolumeSpin)
                {
                    if ((double)(sender as SpinEdit).Value <= selectedSoundscapeRule.volume.Item2)
                        selectedSoundscapeRule.volume.Item1 = (double)(sender as SpinEdit).Value;
                    else
                        (sender as SpinEdit).Value = (decimal)selectedSoundscapeRule.volume.Item2;

                }
                else if (sender == maxVolumeSpin)
                {
                    if ((double)(sender as SpinEdit).Value >= selectedSoundscapeRule.volume.Item1)
                        selectedSoundscapeRule.volume.Item2 = (double)(sender as SpinEdit).Value;
                    else
                        (sender as SpinEdit).Value = (decimal)selectedSoundscapeRule.volume.Item1;
                }
                else if (sender == minPitchSpin)
                {
                    if ((double)(sender as SpinEdit).Value <= selectedSoundscapeRule.pitch.Item2)
                        selectedSoundscapeRule.pitch.Item1 = (double)(sender as SpinEdit).Value;
                    else
                        (sender as SpinEdit).Value = (decimal)selectedSoundscapeRule.pitch.Item2;
                }
                else if (sender == maxPitchSpin)
                {
                    if ((double)(sender as SpinEdit).Value >= selectedSoundscapeRule.pitch.Item1)
                        selectedSoundscapeRule.pitch.Item2 = (double)(sender as SpinEdit).Value;
                    else
                        (sender as SpinEdit).Value = (decimal)selectedSoundscapeRule.pitch.Item1;
                }
                else if (sender == minTimeSpin)
                {
                    if ((double)(sender as SpinEdit).Value <= selectedSoundscapeRule.time.Item2)
                        selectedSoundscapeRule.time.Item1 = (int)(sender as SpinEdit).Value;
                    else
                        (sender as SpinEdit).Value = (decimal)selectedSoundscapeRule.time.Item2;
                }
                else if (sender == maxTimeSpin)
                {
                    if ((double)(sender as SpinEdit).Value >= selectedSoundscapeRule.time.Item1)
                        selectedSoundscapeRule.time.Item2 = (int)(sender as SpinEdit).Value;
                    else
                        (sender as SpinEdit).Value = (decimal)selectedSoundscapeRule.time.Item1;
                }
                else if(sender == attenuationSpin)
                {
                    selectedSoundscapeRule.attenuation = (float)(sender as SpinEdit).Value;
                }
            }
        }

        private void ruleInfoEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (selectedSoundscapeRule != null)
            {
                if (sender == positionCombo)
                {
                    if (positionCombo.SelectedIndex == 9)
                    {
                        selectedSoundscapeRule.positionRandom = true;
                    }
                    else
                    {
                        selectedSoundscapeRule.position = positionCombo.SelectedIndex - 1;
                    }
                    rulesTree.Selection[0].SetValue("position", positionCombo.EditValue.ToString());
                }
                else if(sender == soundlevelCombo)
                {
                    string soundlevelString = soundlevelCombo.EditValue.ToString();
                    if (soundlevelCombo.SelectedIndex != 0)
                    {
                        if (soundlevelString.Contains(" "))
                        {
                            soundlevelString = soundlevelString.Substring(0, soundlevelString.IndexOf(" "));
                        }
                        selectedSoundscapeRule.soundlevel = soundlevelString;
                        attenuationSpin.Value = 1;
                        attenuationSpin.Enabled = false;
                    } else
                    {
                        attenuationSpin.Enabled = true;
                        selectedSoundscapeRule.soundlevel = "";
                    }
                }
                else if(sender == ruleNameEdit)
                {
                    selectedSoundscapeRule.name = ruleNameEdit.EditValue.ToString();
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
                RelativePath = "";
                PackageFile = null;
                LoadSoundscape(null);
            }

            // Load soundscape
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

            // Save soundscape
            else if (e.Item == menuFileSave)
            {
                if (RelativePath != "")
                {
                    SaveSoundscape();
                }
            }

            // Save soundscape as
            else if (e.Item == menuFileSaveAs)
            {
                FileExplorer fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.SAVE)
                {
                    packageManager = soundscapePackageManager,
                    RootDirectory = "scripts",
                    Filter = "Valve Soundscape Files (soundscapes_*.txt)|soundscapes_*.txt",
                    FileName = "scripts/soundscapes_"
                };
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    string fileName = fileExplorer.FileName;
                    if (!Path.GetFileName(fileName).StartsWith("soundscapes_"))
                    {
                        XtraMessageBox.Show("Soundscape files should start with 'soundscapes_'.");
                        return;
                    }

                    if (!fileName.StartsWith("scripts/"))
                    {
                        XtraMessageBox.Show("Soundscape files should be saved at 'scripts/'.");
                    }

                    if (!fileName.EndsWith(".txt"))
                    {
                        fileName = fileName + ".txt";
                    }

                    RelativePath = fileName;
                    SaveSoundscape();
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

            if (file != null)
            {
                RelativePath = file.FullPath;
                string data = "\"soundscapes\"\n{\n" + System.Text.Encoding.UTF8.GetString(file.Data) + "\n}";

                xblah_modding_lib.KeyValue soundscapeFile = xblah_modding_lib.KeyValue.ReadChunk(data);

                // Traverse through all the soundscapes.
                foreach (KeyValue soundscapeKV in soundscapeFile.getChildren())
                {
                    // If the soundscape has no key, ignore it.
                    if (soundscapeKV.getKey() == "")
                        continue;

                    // Create a new soundscape with the key.
                    Soundscape soundscape = new Soundscape()
                    {
                        name = soundscapeKV.getKey()
                    };

                    // Add the soundscape to the list of soundscapes.
                    soundscapes.Add(soundscape);

                    // Check if the DSP is set.
                    KeyValue dspKV = soundscapeKV.getChildByKey("dsp");
                    if (dspKV != null)
                        soundscape.dsp = int.Parse(dspKV.getValue());
                    else
                        soundscape.dsp = 0;

                    // Check if the DSP Spatial is set.
                    KeyValue dspSpatialKV = soundscapeKV.getChildByKey("dsp_spatial");
                    if (dspSpatialKV != null)
                        soundscape.dsp_spatial = int.Parse(dspSpatialKV.getValue());
                    else
                        soundscape.dsp_spatial = 0;

                    // Check if the DSP Volume is set.
                    KeyValue dspVolumeKV = soundscapeKV.getChildByKey("dspVolume");
                    if (dspVolumeKV != null)
                        soundscape.dsp_volume = float.Parse(dspVolumeKV.getValue());
                    else
                        soundscape.dsp_volume = 1;

                    // Traverse through the soundscape rules.
                    if (soundscapeKV.getChildren() != null)
                        foreach (KeyValue soundscapeRuleKV in soundscapeKV.getChildren())
                        {
                            // Get the rule key.
                            if (soundscapeRuleKV.getKey() == "playrandom" || soundscapeRuleKV.getKey() == "playlooping")
                            {
                                // If the soundscape rule has no key, ignore it.
                                if (soundscapeRuleKV.getKey() == "")
                                    continue;

                                SoundscapeRule soundscapeRule = new SoundscapeRule();
                                soundscape.rules.Add(soundscapeRule);

                                switch (soundscapeRuleKV.getKey())
                                {
                                    case "playrandom":
                                        soundscapeRule.rule = Soundscape.Rule.RANDOM;
                                        break;
                                    case "playlooping":
                                        soundscapeRule.rule = Soundscape.Rule.LOOPING;
                                        break;
                                }

                                // Get the rule time.
                                KeyValue timeKV = soundscapeRuleKV.getChildByKey("time");
                                if (timeKV != null)
                                    soundscapeRule.time = ((int)float.Parse(timeKV.getValue().Split(',').First()), (int)float.Parse(timeKV.getValue().Split(',').Last()));
                                else
                                    soundscapeRule.time = (1, 1);

                                // Get the rule volume.
                                KeyValue volumeKV = soundscapeRuleKV.getChildByKey("volume");
                                if (volumeKV != null)
                                    soundscapeRule.volume = (float.Parse(volumeKV.getValue().Split(',').First()), float.Parse(volumeKV.getValue().Split(',').Last()));
                                else
                                    soundscapeRule.volume = (1, 1);

                                // Get the rule pitch.
                                KeyValue pitchKV = soundscapeRuleKV.getChildByKey("pitch");
                                if (pitchKV != null)
                                    soundscapeRule.pitch = ((int)float.Parse(pitchKV.getValue().Split(',').First()), (int)float.Parse(pitchKV.getValue().Split(',').Last()));
                                else
                                    soundscapeRule.pitch = (100, 100);

                                // Get the rule position.
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

                                // Get the rule attenuation.
                                KeyValue attenuationKV = soundscapeRuleKV.getChildByKey("attenuation");
                                if (attenuationKV != null)
                                    float.TryParse(attenuationKV.getValue(), out soundscapeRule.attenuation);
                                else
                                    soundscapeRule.attenuation = 1;

                                // Get the rule sound level.
                                KeyValue soundlevelKV = soundscapeRuleKV.getChildByKey("soundlevel");
                                if (soundlevelKV != null)
                                    soundscapeRule.soundlevel = soundlevelKV.getValue();
                                else
                                    soundscapeRule.soundlevel = "";

                                // Get the rule wave.
                                KeyValue waveKV = soundscapeRuleKV.getChildByKey("wave");
                                if (waveKV != null)
                                {
                                    string fileName = waveKV.getValue();
                                    if (fileName.StartsWith("*"))
                                        fileName = fileName.Substring(1);

                                    PackageFile packageFile = packageManager.GetFile("sound\\" + fileName);
                                    soundscapeRule.wave.Add(packageFile);
                                }

                                // Traverse through the rule random waves.
                                KeyValue rndWaveKV = soundscapeRuleKV.getChildByKey("rndwave");
                                if (rndWaveKV != null)
                                {
                                    foreach (KeyValue childWaveKV in rndWaveKV.getChildren())
                                    {
                                        // If the wave has no key, ignore it.
                                        if (childWaveKV.getKey() == "")
                                            continue;

                                        string fileName = childWaveKV.getValue();
                                        if (fileName.StartsWith("*"))
                                            fileName = fileName.Substring(1);

                                        // Get the packageFile from the relative path.
                                        PackageFile packageFile = packageManager.GetFile("sound\\" + fileName);

                                        soundscapeRule.wave.Add(packageFile);
                                    }
                                }
                            } else if(soundscapeRuleKV.getKey() == "playsoundscape")
                            {
                                // If the rule has no soundscape name, ignore it.
                                KeyValue nameKv = soundscapeRuleKV.getChildByKey("name");
                                if (nameKv == null)
                                    continue;

                                SoundscapeRule soundscapeRule = new SoundscapeRule()
                                {
                                    rule = Soundscape.Rule.SOUNDSCAPE
                                };
                                soundscape.rules.Add(soundscapeRule);

                                // Get the rule volume.
                                KeyValue volumeKV = soundscapeRuleKV.getChildByKey("volume");
                                if (volumeKV != null)
                                    soundscapeRule.volume = (float.Parse(volumeKV.getValue()), float.Parse(volumeKV.getValue()));
                                else
                                    soundscapeRule.volume = (1, 1);

                                // Get the sub-scape name.
                                soundscapeRule.name = nameKv.getValue();
                            }
                        }
                }
            }

            if (dockManager.IsInitialized)
                updateSoundscapesTree();

        }

        private void SaveSoundscape()
        {
            // Create the soundscape file (UTF 8 format).
            KeyValue soundscapeFile = new KeyValue("soundscapes");

            // Traverse through all the soundscapes.
            foreach(Soundscape soundscape in soundscapes)
            {
                // Create a new soundscape with the key.
                KeyValue soundscapeKV = new KeyValue(soundscape.name);
                soundscapeFile.addChild(soundscapeKV);
                soundscapeKV.setValue("dsp", soundscape.dsp.ToString());
                soundscapeKV.setValue("dsp_spatial", soundscape.dsp_spatial.ToString());
                soundscapeKV.setValue("dsp_volume", soundscape.dsp_volume.ToString());

                // Traverse through the soundscape rules.
                foreach (SoundscapeRule soundscapeRule in soundscape.rules)
                {
                    KeyValue soundscapeRuleKV = null;

                    // Get the rule key.
                    if (soundscapeRule.rule == Soundscape.Rule.RANDOM || soundscapeRule.rule == Soundscape.Rule.LOOPING)
                    {
                        switch (soundscapeRule.rule)
                        {
                            case Soundscape.Rule.RANDOM:
                                soundscapeRuleKV = new KeyValue("playrandom");
                                break;
                            case Soundscape.Rule.LOOPING:
                                soundscapeRuleKV = new KeyValue("playlooping");
                                break;
                        }

                        // Get the rule time.
                        if (soundscapeRule.time.Item1 == soundscapeRule.time.Item2)
                        {
                            soundscapeRuleKV.setValue("time", soundscapeRule.time.Item1.ToString());
                        } else
                        {
                            soundscapeRuleKV.setValue("time", soundscapeRule.time.Item1 + "," + soundscapeRule.time.Item2);
                        }

                        // Get the rule volume.
                        if (soundscapeRule.volume.Item1 == soundscapeRule.volume.Item2)
                        {
                            soundscapeRuleKV.setValue("volume", soundscapeRule.volume.Item1.ToString());
                        }
                        else
                        {
                            soundscapeRuleKV.setValue("volume", soundscapeRule.volume.Item1 + "," + soundscapeRule.volume.Item2);
                        }

                        // Get the rule pitch.
                        if (soundscapeRule.pitch.Item1 == soundscapeRule.pitch.Item2)
                        {
                            soundscapeRuleKV.setValue("pitch", (soundscapeRule.pitch.Item1).ToString());
                        }
                        else
                        {
                            soundscapeRuleKV.setValue("pitch", (soundscapeRule.pitch.Item1) + "," + (soundscapeRule.pitch.Item2));
                        }

                        // Get the rule position.
                        if (soundscapeRule.positionRandom == true)
                        {
                            soundscapeRuleKV.setValue("position", "random");
                        } else if(soundscapeRule.position >= 0)
                        {
                            soundscapeRuleKV.setValue("position", soundscapeRule.position.ToString());
                        }

                        // Get the rule attenuation.
                        if (soundscapeRule.attenuation != 1)
                        {
                            soundscapeRuleKV.setValue("attenuation", soundscapeRule.attenuation.ToString());
                        }

                        // Get the rule sound level.
                        if (soundscapeRule.soundlevel != "")
                        {
                            soundscapeRuleKV.setValue("soundlevel", soundscapeRule.soundlevel);
                        }

                        // Get the rule wave.
                        if (soundscapeRule.rule == Soundscape.Rule.LOOPING && soundscapeRule.wave.Count == 1)
                        {
                           
                            string path = soundscapeRule.wave[0].FullPath.Replace("/", "\\");
                            if (path.StartsWith("sound/") || path.StartsWith("sound\\"))
                            {
                                path = path.Substring(6);
                            }
                            soundscapeRuleKV.setValue("wave", path);
                        }

                        // Traverse through the rule random waves.
                        if (soundscapeRule.rule == Soundscape.Rule.RANDOM)
                        {
                            if (soundscapeRule.wave.Count > 0)
                            {
                                KeyValue rndWaveKV = new KeyValue("rndwave");

                                foreach (PackageFile packageFile in soundscapeRule.wave)
                                {
                                    string path = packageFile.FullPath.Replace("/", "\\");
                                    if (path.StartsWith("sound/") || path.StartsWith("sound\\"))
                                    {
                                        path = path.Substring(6);
                                    }

                                    KeyValue waveKV = new KeyValue("wave", path);

                                    rndWaveKV.addChild(waveKV);
                                }

                                soundscapeRuleKV.addChild(rndWaveKV);
                            }
                        }
                    }
                    else if (soundscapeRule.rule == Soundscape.Rule.SOUNDSCAPE)
                    {
                        soundscapeRuleKV = new KeyValue("playsoundscape");

                        soundscapeRuleKV.setValue("name", soundscapeRule.name);
                        soundscapeRuleKV.setValue("volume", soundscapeRule.volume.Item1.ToString());
                    }

                    soundscapeKV.addChild(soundscapeRuleKV);
                }
            }

            //.Diagnostics.Debugger.Break();

            // Write the file.
            string file = "";
            foreach(KeyValue soundscapeKV in soundscapeFile.getChildren())
            {
                file = file + KeyValue.writeChunk(soundscapeKV, true) + "\n";
            }

            File.WriteAllText(launcher.GetCurrentMod().InstallPath + "/" + RelativePath, file);

            //System.Diagnostics.Debugger.Break();
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
            if (!isUpdatingRulesTree)
                UpdateSelectedSoundscapeRule();
        }

        private void dockManager_Load(object sender, EventArgs e)
        {
            updateSoundscapesTree();
        }

        private void SoundscapeEditor_Load(object sender, EventArgs e)
        {
            if (PackageFile != null)
            {
                LoadSoundscape(PackageFile);
            }
        }

        #endregion

        private void wavesTree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            UpdateSelectedWave();
        }
    }
}

/*

SPECIAL CHARACTERS
* - CHAR_STREAM
    Streams from the disc, get flushed soon after. Use for one-off dialogue files or music.
# - CHAR_DRYMIX
    Bypasses DSP and affected by the user's music volume setting.

Warning.png Warning:  Using this character on a sound entry utilizing an update_stack where the volume is zeroed out, whether by the update_stack or by the sound entry itself(volume 0.0), will cause an update_stack loop.Not only does this have nagative performance implications, the sound will consume an audio channel until the loop is interrupted or the sound is stopped. [confirm]

@ - CHAR_OMNI
    Non-directional sound; plays "everywhere", similar to SNDLVL_NONE, except it fades with distance from its source based on its sound level.
> - CHAR_DOPPLER
    Doppler encoded stereo: left for heading towards the listener and right for heading away.
< - CHAR_DIRECTIONAL
    Stereo with direction: left channel for front facing, right channel for rear facing. Mixed based on listener's direction. To do: Relationship with CHAR_DIRSTEREO in Counter-Strike: Global Offensive?
^ - CHAR_DISTVARIANT
    Distance-variant stereo. Left channel is close, right channel is far. Transition distance is hard-coded; see below.
) - CHAR_SPATIALSTEREO
    Spatializes both channels, allowing them to be placed at specific locations within the world; see below.
    Note.png Note: Sometimes "(" must be used instead; see below.
} - CHAR_FAST_PITCH
*/