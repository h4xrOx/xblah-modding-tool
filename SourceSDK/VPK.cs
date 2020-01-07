using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_source1ide
{
    public class VPK
    {
        public string fullPath;
        public Dictionary<string, File> files;

        internal Steam sourceSDK;

        public class File
        {
            public string path = "";
            public string pack = "";
            public string type = "";
        }

        public VPK()
        {

        }

        public VPK(string fullPath, Steam sourceSDK)
        {
            this.fullPath = fullPath;
            this.sourceSDK = sourceSDK;

            ListFiles();
        }

        internal string GetPackName()
        {
            string gamePath = sourceSDK.GetGamePath();
            string modPath = sourceSDK.GetModPath();

            string packName;
            try
            {
                if (fullPath.Contains(modPath))
                {
                    Uri path1 = new Uri(modPath + "\\");
                    Uri path2 = new Uri(fullPath);
                    Uri diff = path1.MakeRelativeUri(path2);
                    packName = "|gameinfo_path|" + diff.OriginalString;
                }
                else
                {
                    Uri path1 = new Uri(gamePath + "\\");
                    Uri path2 = new Uri(fullPath);
                    Uri diff = path1.MakeRelativeUri(path2);
                    packName = "|all_source_engine_paths|" + diff.OriginalString;
                }

            }
            catch (Exception)
            {
                packName = Path.GetFileName(fullPath);
            }

            return packName;
        }

        internal virtual void ListFiles()
        {
            string gamePath = sourceSDK.GetGamePath();
            string toolPath = gamePath + "\\bin\\vpk.exe";

            files = new Dictionary<string, File>();

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = toolPath,
                    Arguments = "l \"" + fullPath + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();

            string packName = GetPackName();

            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine().ToLower();

                string extension = new FileInfo(line).Extension;

                File file = new File()
                {
                    path = line,
                    pack = packName,
                    type = extension
                };
                files.Add(line, file);
            }
        }

        public virtual void extractFile(string filePath, string startupPath)
        {
            string modPath = sourceSDK.GetModPath();
            string toolPath = startupPath + "\\Tools\\HLExtract\\HLExtract.exe";

            string vpkPath = fullPath;
            if (!System.IO.File.Exists(vpkPath))
                vpkPath = vpkPath.Replace(".vpk", "_dir.vpk");

            if (!System.IO.File.Exists(vpkPath))
                return;

            Directory.CreateDirectory(modPath + "/" + (filePath.Contains("/") ? filePath.Substring(0, filePath.LastIndexOf("/")) : ""));
            string args = "-p \"" + vpkPath + "\" -d \"" + modPath + "/" + (filePath.Contains("/") ? filePath.Substring(0, filePath.LastIndexOf("/")) : "") + "\" -e \"" + filePath + "\" -s";
            Process process = new Process();
            process.StartInfo.FileName = toolPath;
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }
    }
}
