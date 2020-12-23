namespace SourceSDK
{
    public class RunMode
    {
        public const int DEFAULT = 0;
        public const int FULLSCREEN = 1;
        public const int WINDOWED = 2;
        public const int VR = 3;

        public static string ToString(int engine)
        {
            switch (engine)
            {
                case DEFAULT:
                    return "Default";
                case FULLSCREEN:
                    return "Fullscreen";
                case WINDOWED:
                    return "Windowed";
                case VR:
                    return "VR";
                default:
                    return "";
            }
        }

        public static int FromString(string engine)
        {
            switch (engine.ToLower())
            {
                case "default":
                    return DEFAULT;
                case "fullscreen":
                    return FULLSCREEN;
                case "windowed":
                    return WINDOWED;
                case "vr":
                    return VR;
                default:
                    return 0;
            }
        }
    }
}
