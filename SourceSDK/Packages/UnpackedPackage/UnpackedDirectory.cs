using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSDK.Packages.UnpackedPackage
{
    public class UnpackedDirectory : PackageDirectory
    {
        public UnpackedDirectory(PackageArchive parentArchive, string path, List<PackageFile> entries) : base(parentArchive, path, entries)
        {
        }
    }
}
