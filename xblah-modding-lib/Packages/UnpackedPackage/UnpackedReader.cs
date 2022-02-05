using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace xblah_modding_lib.Packages.UnpackedPackage
{
    class UnpackedReader
    {
        internal List<PackageDirectory> ReadDirectories(UnpackedArchive parentArchive, string rootPath)
        {
            List<PackageDirectory> result = new List<PackageDirectory>();

            if (Directory.Exists(parentArchive.ArchivePath + "\\" + rootPath))
            {
                List<string> directories = Directory.GetDirectories(parentArchive.ArchivePath + "\\" + rootPath, "*", SearchOption.AllDirectories).ToList();
                directories.Add(parentArchive.ArchivePath + "\\" + rootPath);

                foreach (string directory in directories)
                {
                    var entries = ReadEntries(parentArchive, directory);

                    Uri path1 = new Uri(parentArchive.ArchivePath + "\\");
                    Uri path2 = new Uri(directory);
                    Uri diff = path1.MakeRelativeUri(path2);

                    PackageDirectory packageDirectory = new UnpackedDirectory(parentArchive, diff.OriginalString.ToLower().Replace("\\", "/").ToLower(), entries);
                    result.Add(packageDirectory);
                }
            }

            return result;
        }

        private List<PackageFile> ReadEntries(UnpackedArchive parentArchive, string path)
        {
            List<PackageFile> result = new List<PackageFile>();

            foreach (string filePath in Directory.GetFiles(path))
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                if (filePath.EndsWith(".vpk") && int.TryParse(fileName.Substring(fileName.Length - 3), out _))
                    continue;

                string extension = new FileInfo(filePath).Extension;
                if (extension.StartsWith("."))
                    extension = extension.Substring(1);

                Uri path1 = new Uri(parentArchive.ArchivePath + "\\");
                Uri path2 = new Uri(filePath.Replace("_dir.vpk", ".vpk"));
                Uri diff = path1.MakeRelativeUri(path2);

                PackageFile file = new UnpackedFile()
                {
                    Path = Path.GetDirectoryName(diff.OriginalString.ToLower()).Replace("\\", "/").ToLower(),
                    Extension = extension.ToLower(),
                    Filename = Path.GetFileNameWithoutExtension(filePath).ToLower(),
                };

                result.Add(file);
            }

            return result;
        }
    }
}
