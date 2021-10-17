namespace SourceSDK.Packages.UnpackedPackage
{
    public class UnpackedArchive : PackageArchive
    {
        private UnpackedReader _reader;

        public override void Load(string filename, string rootPath)
        {
            ArchivePath = filename;

            _reader = new UnpackedReader();
            Directories.AddRange(_reader.ReadDirectories(this, rootPath));
        }
    }
}
