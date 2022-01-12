using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xblah_modding_lib
{
    public static class KeyCode
    {
        private static readonly Dictionary<uint, string> Keys
            = new Dictionary<uint, string>
        {
            {0, ""},
            {1, "MOUSE1"},
            {2, "MOUSE2"},
            {4, "MOUSE3"},
            {8, "BACKSPACE"},
            {9, "TAB"},
            {12, "Clear"},
            {13, "ENTER"},
            {16, "SHIFT"},
            {17, "CTRL"},
            {20, "CAPSLOCK"},
            {27, "ESCAPE"},
            {32, "SPACE"},
            {33, "PGUP"},
            {34, "PGDN"},
            {35, "END"},
            {36, "HOME"},
            {37, "LEFTARROW"},
            {38, "UPARROW"},
            {39, "RIGHTARROW"},
            {40, "DOWNARROW"},
            {45, "INS"},
            {46, "DEL"},
            {48, "0"},
            {49, "1"},
            {50, "2"},
            {51, "3"},
            {52, "4"},
            {53, "5"},
            {54, "6"},
            {55, "7"},
            {56, "8"},
            {57, "9"},
            {65, "A"},
            {66, "B"},
            {67, "C"},
            {68, "D"},
            {69, "E"},
            {70, "F"},
            {71, "G"},
            {72, "H"},
            {73, "I"},
            {74, "J"},
            {75, "K"},
            {76, "L"},
            {77, "M"},
            {78, "N"},
            {79, "O"},
            {80, "P"},
            {81, "Q"},
            {82, "R"},
            {83, "S"},
            {84, "T"},
            {85, "U"},
            {86, "V"},
            {87, "W"},
            {88, "X"},
            {89, "Y"},
            {90, "Z"},
            {93, "KP_DEL"},
            {95, "Sleep"},
            {96, "KP_INS"},
            {97, "KP_END"},
            {98, "KP_DOWNARROW"},
            {99, "KP_PGDN"},
            {100, "KP_LEFTARROW"},
            {101, "KP_5"},
            {102, "KP_RIGHTARROW"},
            {103, "KP_HOME"},
            {104, "KP_UPARROW"},
            {105, "KP_PGUP"},
            {106, "KP_MULTIPLY"},
            {107, "KP_PLUS"},
            {108, "Separator"},
            {109, "KP_MINUS"},
            {110, "KP_DEL"},
            {111, "KP_SLASH"},
            {112, "F1"},
            {113, "F2"},
            {114, "F3"},
            {115, "F4"},
            {116, "F5"},
            {117, "F6"},
            {118, "F7"},
            {119, "F8"},
            {120, "F9"},
            {121, "F10"},
            {122, "F11"},
            {123, "F12"},
            {124, "F13"},
            {125, "F14"},
            {126, "F15"},
            {127, "F16"},
            {128, "F17"},
            {129, "F18"},
            {130, "F19"},
            {131, "F20"},
            {132, "F21"},
            {133, "F22"},
            {134, "F23"},
            {135, "F24"},
            {144, "NUMLOCK"},
            {145, "Scroll"},
            {160, "SHIFT"},
            {161, "RSHIFT"},
            {162, "CTRL"},
            {163, "RCTRL"},
            {192, "`"},
        };

        public static string GetKey(uint keycode)
        {
            if (Keys.ContainsKey(keycode))
                return Keys[keycode];

            MessageBox.Show("Key " + keycode + " is not registered. Report this bug.");

            return Keys[0];
        }

        public static string GetKey(int keycode)
        {
            return GetKey((uint)keycode);
        }
    }
}
