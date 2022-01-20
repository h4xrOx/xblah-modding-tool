namespace xblah_modding_lib
{
    public class Engine
    {
        public const int GOLDSRC = 2;

        public static string ToString(int engine)
        {
            switch (engine)
            {
                case GOLDSRC:
                    return "goldsrc";
                default:
                    return "";
            }
        }

        public static int FromString(string engine)
        {
            switch (engine)
            {
                case "goldsrc":
                    return GOLDSRC;
                default:
                    return 0;
            }
        }
    }
}
