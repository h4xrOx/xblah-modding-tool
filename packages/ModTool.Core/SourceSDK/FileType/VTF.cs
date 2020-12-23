//-----------------------------------------------------------------------
// <copyright file="D:\Development\CS\windows-source-modding-tool\SourceSDK\FileType\VTF.cs" company="">
//     Author: Jean XBLAH Knapp
//     Copyright (c) 2019-2020. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Diagnostics;
using System.Drawing;
using System.IO;
using TGASharpLib;

namespace source_modding_tool.SourceSDK
{
    public class VTF
    {
        /// <summary>
        /// Converts a bitmap var into a byte array that can be saved as a VTF
        /// </summary>
        /// <param name="bitmap">The image to be converted</param>
        /// <param name="launcher">An instance of the Source SDK lib</param>
        /// <returns></returns>
        public static byte[] FromBitmap(Bitmap bitmap, Launcher launcher)
        {
            if (bitmap == null || launcher == null)
                return null;

            string gamePath = launcher.GetCurrentGame().installPath;
            string modPath = launcher.GetCurrentMod().installPath;
            string filePath = modPath + "\\materialsrc";

            string vtexPath = gamePath + "\\bin\\vtex.exe";

            var tga = new TGA(bitmap);
            Directory.CreateDirectory(filePath);
            tga.Save(filePath + "\\temp.tga");
            File.WriteAllText(filePath + "\\temp.txt", "nolod 1\r\nnomip 1");

            Process process = new Process();
            process.StartInfo.FileName = vtexPath;
            process.StartInfo.Arguments = "-mkdir -quiet -nopause -shader UnlitGeneric temp.tga";
            process.StartInfo.WorkingDirectory = filePath;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();

            byte[] result = File.ReadAllBytes(modPath + "\\materials\\temp.vtf");

            File.Delete(filePath + "\\temp.tga");
            File.Delete(filePath + "\\temp.txt");
            File.Delete(modPath + "\\materials\\temp.vtf");
            File.Delete(modPath + "\\materials\\temp.vmt");

            return result;
        }

        /// <summary>
        /// Converts a byte array of a VTF file into a bitmap var
        /// </summary>
        /// <param name="vtf">The byte array read from a VTF</param>
        /// <param name="launcher">An instance of the Source SDK lib</param>
        /// <returns></returns>
        public static Bitmap ToBitmap(byte[] vtf, Launcher launcher)
        {
            if (vtf == null || vtf.Length == 0 || launcher == null)
                return null;

            string gamePath = launcher.GetCurrentGame().installPath;
            string modPath = launcher.GetCurrentMod().installPath;
            string filePath = modPath + "\\materials";

            string vtf2tgaPath = gamePath + "\\bin\\vtf2tga.exe";

            Directory.CreateDirectory(filePath);
            File.WriteAllBytes(filePath + "\\temp.vtf", vtf);

            Process process = new Process();
            process.StartInfo.FileName = vtf2tgaPath;
            process.StartInfo.Arguments = "-i temp -o temp";
            process.StartInfo.WorkingDirectory = filePath;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();

            if (!File.Exists(filePath + "\\temp.tga"))
                return null;

            Bitmap src = TGA.FromFile(filePath + "\\temp.tga").ToBitmap();
            File.Delete(filePath + "\\temp.tga");
            File.Delete(filePath + "\\temp.vtf");

            return src;
        }
    }
}
