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
        public override void CopyTo(string destinationPath)
        {
            string fullPath = GetFullPath();
            File.Copy(fullPath, destinationPath);
        }

        protected override byte[] ReadData()
        {
            byte[] data = null;
            string fullPath = GetFullPath();

            if (File.Exists(fullPath))
                data = File.ReadAllBytes(fullPath);

            return data;
        }

        private string GetFullPath()
        {
            return (this.Directory.ParentArchive.ArchivePath + "\\" + this.Path + "\\" + this.Filename + "." + this.Extension).Replace("/", "\\");
        }


    }
}
