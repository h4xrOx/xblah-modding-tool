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
        public PackageFile wave = null;

        public (double, double) volume = (1, 1);
    }
}
