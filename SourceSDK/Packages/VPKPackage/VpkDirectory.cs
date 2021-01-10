using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSDK.Packages.VPKPackage
{
    public class VpkDirectory : PackageDirectory
    {
        public VpkDirectory(PackageArchive parentArchive, string path, List<PackageFile> entries) : base(parentArchive, path, entries)
        {
        }
    }
}
