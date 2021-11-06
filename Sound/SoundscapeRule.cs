using SourceSDK.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace source_modding_tool.Sound
{
    public class SoundscapeRule
    {

        public Soundscape.Rule rule = Soundscape.Rule.LOOPING;

        public List<PackageFile> wave = new List<PackageFile>();
        public (double, double) volume = (1, 1);
        public (int, int) time = (1, 1);
        public (double, double) pitch = (0, 0);
        public int position = -1;
        public bool positionRandom = false;
        public float attenuation = 1;
        public string soundlevel = "";
        internal string name = "";
    }
}
