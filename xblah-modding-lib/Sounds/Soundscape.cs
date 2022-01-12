using System.Collections.Generic;

namespace xblah_modding_lib.Sounds
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
    }
}
