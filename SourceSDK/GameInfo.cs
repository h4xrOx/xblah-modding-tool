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

            switch(launcher.GetCurrentGame().engine)
            {
                case Engine.SOURCE:
                    root = KeyValue.readChunkfile(launcher.GetCurrentMod().installPath + "\\gameinfo.txt");
                    break;
                case Engine.SOURCE2:
                    root = KeyValue.readChunkfile(launcher.GetCurrentMod().installPath + "\\gameinfo.gi");
                    break;
            }
            
        }

        public string getValue(string key)
        {
            return root.getValue(key);
        }
    }
}
