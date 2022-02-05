using System.Collections.Generic;

namespace xblah_modding_lib.Packages.VPKPackage
{
    public class VpkDirectory : PackageDirectory
    {
        public VpkDirectory(PackageArchive parentArchive, string path, List<PackageFile> entries) : base(parentArchive, path, entries)
        {
        }
    }
}
