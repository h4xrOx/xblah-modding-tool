using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSDK.Packages.UnpackedPackage
{
    public class UnpackedFile : PackageFile
    {
        protected override byte[] ReadData()
        {
            byte[] data = null;
            string fullPath = this.Directory.ParentArchive.ArchivePath + "\\" + this.Path.Replace("/", "\\");

            if (File.Exists(fullPath))
                data = File.ReadAllBytes(fullPath);

            return data;
        }
    }
}
