namespace xblah_modding_lib
{
    public class Engine
    {
        public const int SOURCE = 1;

        public static string ToString(int engine)
        {
            switch (engine)
            {
                case SOURCE:
                    return "source";
                default:
                    return "";
            }
        }

        public static int FromString(string engine)
        {
            switch (engine)
            {
                case "source":
                    return SOURCE;
                default:
                    return 0;
            }
        }
    }
}
