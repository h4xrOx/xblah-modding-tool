using DevExpress.XtraEditors;
using xblah_modding_lib;
using xblah_modding_lib.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xblah_modding_tool.Sound
{
    public class Soundscape
    {
        public enum Rule
        {
            LOOPING = 0,
            RANDOM = 1,
            SOUNDSCAPE = 2
        };

        public string name = "";
        public List<SoundscapeRule> rules = new List<SoundscapeRule>();
        public int dsp = 0;
        public int dsp_spatial = 0;
        public float dsp_volume = 1;

        /// <summary>
        /// Creates a particles_manifest.txt with the particles included in the base game plus the ones found in the particles folder.
        /// </summary>
        /// <param name="launcher">An instance of the Source SDK lib</param>
        public static void CreateManifest(Launcher launcher)
        {
            if (launcher == null)
                return;

            PackageManager packageManager = new PackageManager(launcher,"scripts");

            KeyValue manifest = new KeyValue("soundscapes_manifest");

            List<string> soundscapesList = new List<string>();
            foreach(PackageDirectory directory in packageManager.Directories)
            {
                foreach(PackageFile file in directory.Entries.Where(f => f.Filename.StartsWith("soundscapes_") && f.Filename != "soundscapes_manifest" && f.Extension == "txt"))
                {
                    if (!soundscapesList.Contains(file.FullPath))
                    {
                        soundscapesList.Add(file.FullPath);
                        manifest.addChild(new KeyValue("file", file.FullPath));
                    }
                }
            }

            KeyValue.writeChunkFile(launcher.GetCurrentMod().InstallPath + "\\scripts\\soundscapes_manifest.txt", manifest, false, Encoding.UTF8, false);
            XtraMessageBox.Show("Soundscapes manifest created.");
        }
    }
}
