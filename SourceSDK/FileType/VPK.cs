//-----------------------------------------------------------------------
// <copyright file="D:\Development\CS\windows-source-modding-tool\SourceSDK\FileType\VPK.cs" company="">
//     Author:  
//     Copyright (c) . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace source_modding_tool
{
    public class VPK
    {
        internal Launcher launcher;
        public Dictionary<string, File> files;
        public string fullPath;

        public VPK() { }

        /// <summary>
        /// Creates an instance of the VPK, reads and stores its content for later usage
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="launcher"></param>
        public VPK(string fullPath, Launcher launcher)
        {
            this.fullPath = fullPath;
            this.launcher = launcher;

            ListFiles();
        }

        /// <summary>
        /// Returns the name of the package relative to the source dirs (i.e. |gameinfo_path|file.vpk
        /// </summary>
        /// <returns></returns>
        internal string GetPackName()
        {
            string gamePath = launcher.GetCurrentGame().installPath;
            string modPath = launcher.GetCurrentMod().installPath;

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

        /// <summary>
        /// Stores a list of all the files packed into the VPK
        /// </summary>
        internal virtual void ListFiles()
        {
            string gamePath = launcher.GetCurrentGame().installPath;
            string toolPath = gamePath + "\\bin\\vpk.exe";

            files = new Dictionary<string, File>();

            Process process = new Process
            {
                StartInfo =
                new ProcessStartInfo
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

                File file = new File() { path = line, pack = packName, type = extension };
                files.Add(line, file);
            }
        }

        /// <summary>
        /// Extracts a file from the VPK to the respective relative path in the mod folder
        /// </summary>
        /// <param name="filePath">Path of the asset relative to the root of the VPK</param>
        public virtual void ExtractFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            string modPath = launcher.GetCurrentMod().installPath;
            string toolPath = AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\HLExtract\\HLExtract.exe";

            string vpkPath = fullPath;
            if (!System.IO.File.Exists(vpkPath))
                vpkPath = vpkPath.Replace(".vpk", "_dir.vpk");

            if (!System.IO.File.Exists(vpkPath))
                return;

            Directory.CreateDirectory(modPath +
                "/" +
                (filePath.Contains("/") ? filePath.Substring(0, filePath.LastIndexOf("/")) : string.Empty));
            string args = "-p \"" +
                vpkPath +
                "\" -d \"" +
                modPath +
                "/" +
                (filePath.Contains("/") ? filePath.Substring(0, filePath.LastIndexOf("/")) : string.Empty) +
                "\" -e \"" +
                filePath +
                "\" -s";
            Process process = new Process();
            process.StartInfo.FileName = toolPath;
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }

        public class File
        {
            public string pack = string.Empty;
            public string path = string.Empty;
            public string type = string.Empty;
        }
    }
}
