using SourceModdingTool.SourceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceModdingTool
{
    class GameInfo
    {
        Steam sourceSDK;

        KeyValue root;

        public GameInfo(Steam sourceSDK)
        {
            this.sourceSDK = sourceSDK;

            root = KeyValue.readChunkfile(sourceSDK.GetModPath() + "\\gameinfo.txt");
        }

        public string getValue(string key)
        {
            return root.getValue(key);
        }
    }
}
