using SourceSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SourceSDK.Packages
{
    public class MountedFolder : VPK
    {
        public MountedFolder(string fullPath, Launcher launcher) : base()
        {
            this.fullPath = fullPath.Replace("*", string.Empty);
            this.launcher = launcher;

            ListFiles();
        }

        internal override void ListFiles()
        {
            files = new Dictionary<string, File>();

            string packName = GetPackName();

            if (Directory.Exists(fullPath))
            {
                /*string[] filePaths = Directory.GetFiles(fullPath, "*", SearchOption.AllDirectories)
                    .Where(x => !x.EndsWith(".vpk"))
                    .ToArray();*/
                string[] filePaths = Directory.GetFiles(fullPath, "*", SearchOption.AllDirectories);
                foreach (string filePath in filePaths)
                {
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    if (filePath.EndsWith(".vpk") && int.TryParse(fileName.Substring(fileName.Length - 3), out _))
                        continue;

                    string extension = new FileInfo(filePath).Extension;

                    Uri path1 = new Uri(fullPath + "\\");
                    Uri path2 = new Uri(filePath.Replace("_dir.vpk", ".vpk"));
                    Uri diff = path1.MakeRelativeUri(path2);

                    File file = new File() { path = diff.OriginalString.ToLower(), pack = packName, type = extension };
                    files.Add(file.path, file);
                }

            }
        }

        public override void ExtractFile(string filePath) { }
    }
}
