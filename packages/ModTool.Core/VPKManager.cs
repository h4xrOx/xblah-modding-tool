using SourceSDK.Packages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SourceSDK
{
    public class VPKManager
    {
        private Launcher launcher;
        public Dictionary<string, VPK> vpks;

        public VPKManager(Launcher launcher)
        {
            this.launcher = launcher;
            Reload();
        }

        public void extractFile(string filePath)
        {
            foreach (KeyValuePair<string, VPK> vpk in vpks)
            {
                if (vpk.Value.files.ContainsKey(filePath))
                {
                    vpk.Value.ExtractFile(filePath);


                    return;
                }
            }
        }

        public List<VPK.File> getAllFiles()
        {
            List<VPK.File> files = new List<VPK.File>();
            foreach (VPK vpk in vpks.Values)
                files.AddRange(vpk.files.Values);
            files = files
                .GroupBy(x => x.path)
                .Select(y => y.First())
                .OrderBy(x => x.path)
                .ToList();

            List<VPK.File> vpkFiles = files.Where(f => f.type == ".vpk").ToList();
            foreach (VPK.File file in vpkFiles)
            {
                string extractedPath = getExtractedPath(file.path).Replace("_dir.vpk", ".vpk");


                if (vpks.ContainsKey(extractedPath))
                {
                    files.Remove(file);
                    //XtraMessageBox.Show(extractedPath + " is already mounted");
                }

            }

            return files;
        }

        public string getExtractedPath(string filePath)
        {
            foreach (string searchPath in launcher.GetCurrentMod().GetSearchPaths())
            {
                if (File.Exists(searchPath + "\\" + filePath))
                    return searchPath + "\\" + filePath;

                if (filePath.EndsWith(".vpk") && File.Exists(searchPath + "\\" + filePath.Replace(".vpk", "_dir.vpk")))
                    return searchPath + "\\" + filePath.Replace(".vpk", "_dir.vpk");
            }

            return string.Empty;
        }

        public void Reload()
        {
            vpks = new Dictionary<string, VPK>();

            foreach (string searchPath in launcher.GetCurrentMod().GetMountedPaths())
            {
                if (searchPath.EndsWith(".vpk"))
                    vpks.Add(searchPath, new VPK(searchPath, launcher));
                else
                    vpks.Add(searchPath, new MountedFolder(searchPath, launcher));
            }
        }
    }
}
