using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceModdingTool
{
    class MapFolder
    {
        public List<Map> maps = new List<Map>();
        public Dictionary<string, MapFolder> subdirs = new Dictionary<string, MapFolder>();
    }
}
