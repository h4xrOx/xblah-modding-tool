using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSDK.Packages
{
    public abstract class PackageDirectory
    {
        public List<PackageFile> Entries { get; set; }
        public string Path { get; set; }
        public PackageArchive ParentArchive { get; set; }

        public PackageDirectory(PackageArchive parentArchive, string path, List<PackageFile> entries)
        {
            ParentArchive = parentArchive;
            Path = path.ToLower();
            Entries = entries;

            foreach(PackageFile entry in Entries)
            {
                entry.Directory = this;
            }
        }
    }
}
