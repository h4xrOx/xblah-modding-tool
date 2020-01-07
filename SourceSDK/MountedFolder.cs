using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_source1ide
{
    public class MountedFolder : VPK
    {
        public MountedFolder(string fullPath, Steam sourceSDK) : base()
        {
            this.fullPath = fullPath;
            this.sourceSDK = sourceSDK;

            /*if (this.fullPath.EndsWith("."))
                this.fullPath = this.fullPath.Substring(0, this.fullPath.Length - 1);

            if (this.fullPath.EndsWith("\\"))
                this.fullPath = this.fullPath.Substring(0, this.fullPath.Length - 1);*/

            ListFiles();
        }

        internal override void ListFiles()
        {
            files = new Dictionary<string, File>();

            string packName = GetPackName();

            foreach (string filePath in Directory.GetFiles(fullPath, "*", SearchOption.AllDirectories).Where(x => !x.EndsWith(".vpk")).ToArray())
            {

                string extension = new FileInfo(filePath).Extension;

                Uri path1 = new Uri(fullPath + "\\");
                Uri path2 = new Uri(filePath);
                Uri diff = path1.MakeRelativeUri(path2);

                File file = new File()
                {
                    path = diff.OriginalString.ToLower(),
                    pack = packName,
                    type = extension
                };
                files.Add(file.path, file);
            }

            
        }

        public override void extractFile(string filePath, string startupPath)
        {

        }
    }
}
