using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSDK.Packages
{
    public abstract class PackageFile
    {
        public string Extension { get; set; }
        public string Path { get; set; }
        public string Filename { get; set; }
        public PackageDirectory Directory { get; set; }
        public byte[] Data { get { return ReadData(); } }

        protected abstract byte[] ReadData();
    }
}
