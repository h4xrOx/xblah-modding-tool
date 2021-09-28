using SourceSDK.Packages;
using System;
using System.Diagnostics;
using System.IO;

namespace SourceSDK.Maps
{
    public class MAP
    {
        public static byte[] FromBSP(PackageFile packageFile, Launcher launcher)
        {
            string modPath = launcher.GetCurrentMod().InstallPath;
            string filePath = modPath + "\\mapsrc";

            string toolPath = AppDomain.CurrentDomain.BaseDirectory + "\\Tools\\BSP2Map\\BSPTwoMAP.exe";

            Directory.CreateDirectory(filePath);
            File.WriteAllBytes(filePath + "\\temp.bsp", packageFile.Data);

            Process process = new Process();
            process.StartInfo.FileName = toolPath;
            process.StartInfo.WorkingDirectory = filePath;
            process.StartInfo.Arguments = filePath + "\\temp.bsp\"";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();


            byte[] result = File.ReadAllBytes(filePath + "\\temp.map");

            File.Delete(filePath + "\\temp.bsp");
            File.Delete(filePath + "\\temp.map");
            File.Delete(filePath + "\\temp.log");
            File.Delete(filePath + "\\temp_btm.wad");

            return result;

        }
    }
}
