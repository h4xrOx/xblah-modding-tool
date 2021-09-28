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
        private string rootPath;

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

            this.rootPath = rootPath;

            Load(rootPath);
        }

        public void Refresh()
        {
            Load(rootPath);
        }

        public void Load(string rootPath)
        {
            Archives.Clear();

            List<string> mountedPaths = launcher.GetCurrentMod().GetMountedPaths();
            foreach (string mountedPath in mountedPaths)
            {
                string searchPath = mountedPath;
                if (searchPath.EndsWith("\\."))
                    searchPath = searchPath.Substring(0, searchPath.Length - "\\.".Length);

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

                    // In Portal 2, the gameinfo also loads the unlisted vpks in the search dirs.
                    foreach (string loseVPKfile in Directory.GetFiles(searchPath, "*.vpk"))
                    {
                        string fileName = loseVPKfile;

                        if (loseVPKfile.Contains("_"))
                        {
                            string partIdxString = loseVPKfile.Substring(loseVPKfile.LastIndexOf("_") + 1, loseVPKfile.Length - loseVPKfile.LastIndexOf("_") - ".vpk".Length - 1);

                            if (int.TryParse(partIdxString, out int ignore))
                                continue;

                            if (partIdxString == "dir")
                                fileName = loseVPKfile.Replace("_dir.vpk", ".vpk");
                        }

                        if (Archives.Where(a => a.ArchivePath == fileName).ToList().Count == 0)
                        {
                            VpkArchive vpkArchive = new VpkArchive();
                            vpkArchive.Load(fileName, rootPath);
                            vpkArchive.Name = vpkArchive.GetPackName(launcher);
                            Archives.Add(vpkArchive);
                        }

                    }
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
