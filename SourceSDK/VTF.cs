using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGASharpLib;

namespace windows_source1ide.SourceSDK
{
    class VTF
    {
        public static byte[] fromBitmap(Bitmap bitmap, string game, string mod, Steam sourceSDK)
        {
            string gamePath = sourceSDK.GetGames()[game];
            string modPath = sourceSDK.GetMods(game)[mod];
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

        public static Bitmap toBitmap(byte[] vtf, string game, string mod, Steam sourceSDK)
        {
            string gamePath = sourceSDK.GetGames()[game];
            string modPath = sourceSDK.GetMods(game)[mod];
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
