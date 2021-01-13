using SourceSDK.Packages.UnpackedPackage;
using SourceSDK.Packages.VPKPackage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSDK.Packages
{
    public class PackageManager
    {
        private Launcher launcher;

        public List<PackageArchive> Archives { get; set; }

        public List<PackageDirectory> Directories
        {
            get
            {
                List<PackageDirectory> directories = new List<PackageDirectory>();
                new List<PackageDirectory>();
                foreach (PackageArchive archive in Archives)
                    directories.AddRange(archive.Directories);

                directories = directories.OrderBy(d => d.Path).ToList();

                return directories;
            }
        }

        public PackageManager(Launcher launcher, string rootPath)
        {
            this.launcher = launcher;
            Archives = new List<PackageArchive>();

            Load(rootPath);
        }

        public void Load(string rootPath)
        {
            Archives.Clear();

            List<string> mountedPaths = launcher.GetCurrentMod().GetMountedPaths();
            foreach (string searchPath in mountedPaths)
            {
                if (searchPath.EndsWith(".vpk"))
                {
                    VpkArchive archive = new VpkArchive();
                    archive.Load(searchPath, rootPath);
                    archive.Name = archive.GetPackName(launcher);
                    Archives.Add(archive);
                }
                    
                else if (Directory.Exists(searchPath))
                {
                    UnpackedArchive archive = new UnpackedArchive();
                    archive.Load(searchPath, rootPath);
                    archive.Name = archive.GetPackName(launcher);
                    Archives.Add(archive);
                }
                    
            }
        }

        public PackageFile GetFile(string path)
        {
            string directoryPath = Path.GetDirectoryName(path).Replace("\\", "/").ToLower();
            string fileName = Path.GetFileName(path).ToLower();

            foreach(PackageDirectory directory in Directories.Where(p => p.Path == directoryPath).ToList())
            {
                List<PackageFile> files = directory.Entries.Where(e => e.Filename + "." + e.Extension == fileName).ToList();
                if (files.Count > 0)
                    return files[0];
            }

            return null;
        }
    }
}
