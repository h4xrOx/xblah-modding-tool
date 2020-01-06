using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_source1ide
{
    public class VPKManager
    {
        public Dictionary<string, VPK> vpks;
        private Steam sourceSDK;

        public VPKManager(Steam sourceSDK)
        {
            this.sourceSDK = sourceSDK;
            vpks =  new Dictionary<string, VPK>();

            foreach (string vpk in sourceSDK.getModMountedVPKs())
                vpks.Add(vpk, new VPK(vpk, sourceSDK));
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

            return files;
        }

        public void extractFile(string filePath)
        {
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            foreach (KeyValuePair<string, VPK> vpk in vpks)
            {
                if (vpk.Value.files.ContainsKey(filePath))
                {
                    vpk.Value.extractFile(filePath, startupPath);

                    
                    return;
                }
            }
        }
    }
}
