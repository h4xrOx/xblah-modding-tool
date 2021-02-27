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

        public string FullPath
        {
            get
            {
                return Path + "/" + Filename + "." + Extension;
            }
        }

        protected abstract byte[] ReadData();

        public abstract void CopyTo(string destinationPath);
    }
}
