using source_modding_tool.SourceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace source_modding_tool
{
    class GameInfo
    {
        Launcher launcher;

        KeyValue root;

        public GameInfo(Launcher launcher)
        {
            this.launcher = launcher;

            root = KeyValue.readChunkfile(launcher.GetCurrentMod().installPath + "\\gameinfo.txt");
        }

        public string getValue(string key)
        {
            return root.getValue(key);
        }
    }
}
