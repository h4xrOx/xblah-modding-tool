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
using source_modding_tool.SourceSDK;

namespace source_modding_tool
{
    public partial class FogForm : DevExpress.XtraEditors.XtraForm
    {
        Instance instance;

        Launcher launcher;
        public FogForm(Launcher launcher)
        {
            InitializeComponent();

            this.launcher = launcher;
        }

        private void FogForm_Load(object sender, EventArgs e)
        {
            startPreview();
        }

        private void startPreview()
        {
            string sourcePath = AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\IngamePreviews";
            string modPath = launcher.GetCurrentMod().installPath;

            // Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, modPath));

            // Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, modPath), true);

            string fog_override = "1";
            string fog_enabled = fogEnableCheck.Checked ? "1" : "0";
            string fog_color = fogColor.Color.R + " " + fogColor.Color.G + " " + fogColor.Color.B;
            string fog_colorskybox = fogSkyboxColor.Color.R + " " + fogSkyboxColor.Color.G + " " + fogSkyboxColor.Color.B;
            string fog_enable_water_fog = "1";
            string fog_enableskybox = fogEnableCheck.Checked ? "1" : "0";
            string fog_end = getTrackValue(fogTrack.Value.Maximum).ToString();
            string fog_endskybox = getTrackValue(fogSkyboxTrack.Value.Maximum).ToString();
            string fog_start = getTrackValue(fogTrack.Value.Minimum).ToString();
            string fog_startskybox = getTrackValue(fogSkyboxTrack.Value.Minimum).ToString();
            string fog_maxdensity = ((float)densityTrack.Value / densityTrack.Properties.Maximum).ToString();
            string fog_maxdensityskybox = ((float)skyboxDensityTrack.Value / skyboxDensityTrack.Properties.Maximum).ToString();

            instance = new Instance(launcher, panelControl1);
            instance.Start("-nomouse +map fog_preview +crosshair 0" +
                " +fog_override " + fog_override +
                " +fog_enable " + fog_enabled +
                " +fog_start " + fog_start +
                " +fog_end " + fog_end +
                " +fog_startskybox " + fog_startskybox +
                " +fog_endskybox " + fog_endskybox +
                " +fog_enableskybox " + fog_enableskybox +
                " +fog_color \"" + fog_color + "\"" +
                " +fog_colorskybox \"" + fog_colorskybox + "\"" +
                " +fog_maxdensity " + fog_maxdensity +
                " +fog_maxdensityskybox " + fog_maxdensityskybox +
                " +fog_enable_water_fog " + fog_enable_water_fog
            );
            this.ActiveControl = null;

            updatePreview();
        }

        private int getTrackValue(int value)
        {
            int remainder = value % 2;
            value = (value - remainder) / 2;

            value = (int)(Math.Pow(2, value) * (1 + (float)remainder / 2));

            return value;
        }

        private void updatePreview()
        {
            string fog_override = "1";
            string fog_enabled = fogEnableCheck.Checked ? "1" : "0";
            string fog_color = fogColor.Color.R + " " + fogColor.Color.G + " " + fogColor.Color.B;
            string fog_colorskybox = fogSkyboxColor.Color.R + " " + fogSkyboxColor.Color.G + " " + fogSkyboxColor.Color.B;
            string fog_enable_water_fog = "1";
            string fog_enableskybox = fogEnableCheck.Checked ? "1" : "0";
            string fog_end = getTrackValue(fogTrack.Value.Maximum).ToString();
            string fog_endskybox = getTrackValue(fogSkyboxTrack.Value.Maximum).ToString();
            string fog_start = getTrackValue(fogTrack.Value.Minimum).ToString();
            string fog_startskybox = getTrackValue(fogSkyboxTrack.Value.Minimum).ToString();
            string fog_maxdensity = ((float)densityTrack.Value / densityTrack.Properties.Maximum).ToString();
            string fog_maxdensityskybox = ((float)skyboxDensityTrack.Value / skyboxDensityTrack.Properties.Maximum).ToString();

            instance.Command("+fog_override " + fog_override + 
                " +fog_enable " + fog_enabled +
                " +fog_start " + fog_start +
                " +fog_end " + fog_end + 
                " +fog_startskybox " + fog_startskybox + 
                " +fog_endskybox " + fog_endskybox + 
                " +fog_enableskybox " + fog_enableskybox +
                " +fog_color \"" + fog_color + "\"" + 
                " +fog_colorskybox \"" + fog_colorskybox + "\"" +
                " +fog_maxdensity " + fog_maxdensity + 
                " +fog_maxdensityskybox " + fog_maxdensityskybox +
                " +fog_enable_water_fog " + fog_enable_water_fog
                );
        }

        private void fogTrack_ValueChanged(object sender, EventArgs e)
        {
            updatePreview();
        }

        private void fogTrack_MouseUp(object sender, MouseEventArgs e)
        {
            updatePreview();
        }

        private void FogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopPreview();
        }

        private void stopPreview()
        {
            if (instance != null && instance.modProcess != null)
            {
                instance.modProcess.Kill();
            }
        }

        private void fogTrack_ValueChanged_1(object sender, EventArgs e)
        {
            fogValueMax.EditValue = getTrackValue(fogTrack.Value.Maximum).ToString();
            fogValueMin.EditValue = getTrackValue(fogTrack.Value.Minimum).ToString();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            updatePreview();
        }

        private void densityTrack_ValueChanged(object sender, EventArgs e)
        {
            fogDensitiyValue.EditValue = ((float)densityTrack.Value / densityTrack.Properties.Maximum).ToString();
        }

        private void fogSkyboxTrack_ValueChanged(object sender, EventArgs e)
        {
            fogSkyboxValueMax.EditValue = getTrackValue(fogSkyboxTrack.Value.Maximum).ToString();
            fogSkyboxValueMin.EditValue = getTrackValue(fogSkyboxTrack.Value.Minimum).ToString();
        }

        private void trackBarControl2_ValueChanged(object sender, EventArgs e)
        {
            fogSkyboxDensityValue.EditValue = ((float)skyboxDensityTrack.Value / skyboxDensityTrack.Properties.Maximum).ToString();
        }
    }
}