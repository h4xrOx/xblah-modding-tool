using System.Collections.Generic;

namespace SourceSDK.Packages.UnpackedPackage
{
    public class UnpackedDirectory : PackageDirectory
    {
        public UnpackedDirectory(PackageArchive parentArchive, string path, List<PackageFile> entries) : base(parentArchive, path, entries)
        {
        }
    }
}
