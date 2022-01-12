using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xblah_modding_lib
{
    public class Engine
    {
        public const int SOURCE = 1;
        public const int GOLDSRC = 2;
        public const int SOURCE2 = 4;

        public static string ToString(int engine)
        {
            switch (engine)
            {
                case SOURCE:
                    return "source";
                case GOLDSRC:
                    return "goldsrc";
                case SOURCE2:
                    return "source2";
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
                case "goldsrc":
                    return GOLDSRC;
                case "source2":
                    return SOURCE2;
                default:
                    return 0;
            }
        }
    }
}
