using DevExpress.XtraEditors;
using NAudio.Wave;
using source_modding_tool.Modding;
using SourceSDK;
using SourceSDK.Packages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace source_modding_tool.Sound
{
    public partial class SoundscapeEditor : DevExpress.XtraEditors.XtraForm
    {
        PackageManager packageManager;
        Launcher launcher;

        List<Soundscape> soundscapes = new List<Soundscape>();

        private Soundscape selectedSoundscape = null;
        private SoundscapeRule selectedSoundscapeRule = null;

        public SoundscapeEditor(Launcher launcher)
        {
            InitializeComponent();

            this.launcher = launcher;
            LoadPackageManager();
        }

        private void LoadPackageManager()
        {
            packageManager = new PackageManager(launcher, "sound");
        }

        private void soundPreviewButton_Click(object sender, EventArgs e)
        {
            /*DevExpress.XtraTreeList.Nodes.TreeListNode node = soundsTree.FocusedNode;
            if (node != null)
            {
                using (Stream s = new MemoryStream((node.Tag as PackageFile).Data))
                {
                    // http://msdn.microsoft.com/en-us/library/ms143770%28v=VS.100%29.aspx
                    System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer(s);
                    myPlayer.Play();
                }
            }*/
        }

        private void browseSoundButton_Click(object sender, EventArgs e)
        {
            FileExplorer fileExplorer = new FileExplorer(launcher, FileExplorer.Mode.OPEN);
            fileExplorer.packageManager = packageManager;
            if (fileExplorer.ShowDialog() == DialogResult.OK)
            {
                fileEdit.EditValue = fileExplorer.Selection[0].FullPath;
                selectedSoundscapeRule.wave = fileExplorer.Selection[0];
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            soundscapes.Add(new Soundscape());
            updateSoundscapesTree();
        }

        private void updateSoundscapesTree()
        {
            soundscapesTree.BeginUnboundLoad();
            soundscapesTree.Nodes.Clear();

            foreach(Soundscape soundscape in soundscapes)
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode node = soundscapesTree.AppendNode(new object[] { soundscape.name }, null);
                node.Tag = soundscape;
            }
            soundscapesTree.EndUnboundLoad();

            if (soundscapesTree.Selection.Count > 0)
            {
                selectedSoundscape = soundscapesTree.Selection[0].Tag as Soundscape;
            }
        }

        private void updateSoundscapeRulesTree()
        {
            soundscapeRulesTree.BeginUnboundLoad();
            soundscapeRulesTree.Nodes.Clear();

            if (selectedSoundscape != null)
            {

                foreach (SoundscapeRule soundscapeRule in selectedSoundscape.rules)
                {
                    DevExpress.XtraTreeList.Nodes.TreeListNode node = soundscapeRulesTree.AppendNode(new object[] { soundscapeRule.rule }, null);
                    node.Tag = soundscapeRule;
                }
            }

            soundscapeRulesTree.EndUnboundLoad();

            if (soundscapeRulesTree.Selection.Count > 0)
            {
                selectedSoundscapeRule = soundscapeRulesTree.Selection[0].Tag as SoundscapeRule;
            }
        }

        private void soundscapesTree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            UpdateSelectedSoundscape();
        }

        private void UpdateSelectedSoundscape()
        {
            if (soundscapesTree.Selection.Count > 0 && soundscapesTree.Selection[0].Tag != null)
            {
                selectedSoundscape = soundscapesTree.Selection[0].Tag as Soundscape;

                soundscapeNameEdit.Enabled = true;
                soundscapeNameEdit.EditValue = selectedSoundscape.name;

                addRuleButton.Enabled = true;
            }
            else
            {
                selectedSoundscape = null;

                soundscapeNameEdit.Enabled = false;
                soundscapeNameEdit.EditValue = "";

                addRuleButton.Enabled = false;
            }

            updateSoundscapeRulesTree();
        }

        private void soundscapeNameEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (selectedSoundscape != null)
            {
                selectedSoundscape.name = soundscapeNameEdit.EditValue.ToString();
                soundscapesTree.Selection[0].SetValue("name", soundscapeNameEdit.EditValue.ToString());
            }
        }

        private void soundscapesTree_Click(object sender, EventArgs e)
        {
            UpdateSelectedSoundscape();
        }

        private void addRuleButton_Click(object sender, EventArgs e)
        {
            if (selectedSoundscape != null)
            {
                selectedSoundscape.rules.Add(new SoundscapeRule());
                updateSoundscapeRulesTree();
            }
        }

        private void soundscapeRulesTree_Click(object sender, EventArgs e)
        {
            UpdateSelectedSoundscapeRule();
        }

        private void soundscapeRulesTree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            UpdateSelectedSoundscapeRule();
        }

        private void UpdateSelectedSoundscapeRule()
        {
            if (soundscapeRulesTree.Selection.Count > 0 && soundscapeRulesTree.Selection[0].Tag != null)
            {
                selectedSoundscapeRule = soundscapeRulesTree.Selection[0].Tag as SoundscapeRule;

                switch (selectedSoundscapeRule.rule)
                {
                    case Soundscape.Rule.LOOPING:
                        ruleCombo.EditValue = "Looping";
                        break;
                    case Soundscape.Rule.RANDOM:
                        ruleCombo.EditValue = "Random";
                        break;
                }
                fileEdit.EditValue = (selectedSoundscapeRule.wave != null ? selectedSoundscapeRule.wave.FullPath : "");
                minVolumeSpin.Value = (decimal)(selectedSoundscapeRule.volume.Item1);
                maxVolumeSpin.Value = (decimal)(selectedSoundscapeRule.volume.Item2);

                ruleCombo.Enabled = true;
                browseSoundButton.Enabled = true;
            }
            else
            {
                selectedSoundscapeRule = null;

                ruleCombo.EditValue = "";
                fileEdit.EditValue = "";
                minVolumeSpin.Value = 0;
                maxVolumeSpin.Value = 0;

                ruleCombo.Enabled = false;
                browseSoundButton.Enabled = false;

            }
        }

        private void ruleCombo_EditValueChanged(object sender, EventArgs e)
        {
            if (selectedSoundscapeRule != null)
            {
                switch(ruleCombo.EditValue)
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

                soundscapeRulesTree.Selection[0].SetValue("type", ruleCombo.EditValue.ToString());
            }
        }

        List<WaveOutEvent> soundPlayers = new List<WaveOutEvent>();
        List<System.Timers.Timer> timers = new List<System.Timers.Timer>();

        private void playButton_Click(object sender, EventArgs e)
        {
            StartPreview();
        }

        private void StartPreview()
        {
            StopPreview();

            soundPlayers = new List<WaveOutEvent>();
            timers = new List<System.Timers.Timer>();

            if (selectedSoundscape != null)
            {
                foreach (SoundscapeRule rule in selectedSoundscape.rules)
                {
                    if (rule.rule == Soundscape.Rule.LOOPING)
                    {
                        WaveOutEvent outputDevice = new WaveOutEvent();

                        //IWaveProvider provider = new RawSourceWaveStream(new MemoryStream(rule.wave.Data), new WaveFormat());
                        //outputDevice.Init(provider);
                        outputDevice.Init(new LoopStream(new RawSourceWaveStream(new MemoryStream(rule.wave.Data), new WaveFormat())));

                        //Random rand = new Random();
                        //outputDevice.Volume = (float)(rule.volume.Item1 + rand.NextDouble() * (rule.volume.Item2 - rule.volume.Item1));
                        outputDevice.Play();
                        

                        soundPlayers.Add(outputDevice);
                    } else if(rule.rule == Soundscape.Rule.RANDOM)
                    {
                        System.Timers.Timer timer = new System.Timers.Timer(2 * 1000);
                        timer.Elapsed += (s, e) =>
                        {
                            WaveOutEvent outputDevice = new WaveOutEvent();

                            IWaveProvider provider = new RawSourceWaveStream(new MemoryStream(rule.wave.Data), new WaveFormat());
                            outputDevice.Init(provider);

                            Random rand = new Random();
                            //outputDevice.Volume = (float)(rule.volume.Item1 + rand.NextDouble() * (rule.volume.Item2 - rule.volume.Item1));
                            outputDevice.Play();

                            timer.Interval = rand.Next(1, 5) * 1000;
                        };
                        timers.Add(timer);
                        timer.Start();
                    }
                }
            }
        }

        private void StopPreview()
        {
            foreach (WaveOutEvent outputDevice in soundPlayers)
            {
                outputDevice.Stop();
            }

            foreach (System.Timers.Timer timer in timers)
            {
                timer.Stop();
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            StopPreview();
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
    }
}