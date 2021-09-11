using SourceSDK.Packages;

namespace SourceSDK.Sounds
{
    public class SoundscapeRule
    {

        public Soundscape.Rule rule = Soundscape.Rule.LOOPING;

        public PackageFile wave = null;
        public (double, double) volume = (1, 1);
        public (int, int) time = (1, 1);
        public (int, int) pitch = (0, 0);
        public int position = -1;
        public bool positionRandom = false;
        public float attenuation = 1;
        public string soundlevel = "";

    }
}
