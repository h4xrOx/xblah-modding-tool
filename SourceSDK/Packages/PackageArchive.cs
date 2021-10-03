using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSDK.Packages
{
    public abstract class PackageArchive
    {
        public List<PackageDirectory> Directories { get; set; }
        public string ArchivePath { get; set; }

        public string Name { get; set; }

        public PackageArchive()
        {
            Directories = new List<PackageDirectory>();
        }

        public abstract void Load(string filename, string rootPath);

        /// <summary>
        /// Returns the name of the package relative to the source dirs (i.e. |gameinfo_path|file.vpk
        /// </summary>
        /// <returns></returns>
        internal string GetPackName(Launcher launcher)
        {
            string gamePath = launcher.GetCurrentGame().InstallPath;
            string modPath = launcher.GetCurrentMod().InstallPath;

            string packName;
            try
            {
                if (ArchivePath.Contains(modPath))
                {
                    Uri path1 = new Uri(modPath + "\\");
                    Uri path2 = new Uri(ArchivePath);
                    Uri diff = path1.MakeRelativeUri(path2);
                    packName = "|gameinfo_path|" + diff.OriginalString;
                }
                else
                {
                    Uri path1 = new Uri(gamePath + "\\");
                    Uri path2 = new Uri(ArchivePath);
                    Uri diff = path1.MakeRelativeUri(path2);
                    packName = "|all_source_engine_paths|" + diff.OriginalString;
                }
            }
            catch (Exception)
            {
                packName = Path.GetFileName(ArchivePath);
            }

            return packName;
        }
    }
}
