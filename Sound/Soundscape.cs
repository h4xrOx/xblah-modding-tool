using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace source_modding_tool.Sound
{
    public class Soundscape
    {
        public enum Rule
        {
            LOOPING = 0,
            RANDOM = 1
        };

        public string name = "";
        public List<SoundscapeRule> rules = new List<SoundscapeRule>();
    }
}
