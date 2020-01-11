using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using TGASharpLib;

namespace SourceModdingTool.SourceSDK
{
    class VTF
    {
        public static byte[] fromBitmap(Bitmap bitmap, Steam sourceSDK)
        {
            string gamePath = sourceSDK.GetGamePath();
            string modPath = sourceSDK.GetModPath();
            string modFolder = new DirectoryInfo(modPath).Name;
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

        public static Bitmap toBitmap(byte[] vtf, Steam sourceSDK)
        {
            string gamePath = sourceSDK.GetGamePath();
            string modPath = sourceSDK.GetModPath();
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

            if(!File.Exists(filePath + "\\temp.tga"))
                return null;

            Bitmap src = TGA.FromFile(filePath + "\\temp.tga").ToBitmap();
            File.Delete(filePath + "\\temp.tga");
            File.Delete(filePath + "\\temp.vtf");

            return src;
        }
    }
}
