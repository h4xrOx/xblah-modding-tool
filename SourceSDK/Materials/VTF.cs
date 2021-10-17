using SourceSDK.Materials.VTFFile;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace SourceSDK.Materials
{
    public class VTF
    {
        public static byte[] FromBitmap(Bitmap bitmap, Launcher launcher)
        {
            return FromBitmap(bitmap, launcher, new string[] { "nolod 1", "nomip 1" });
        }

        /// <summary>
        /// Converts a bitmap var into a byte array that can be saved as a VTF
        /// </summary>
        /// <param name="bitmap">The image to be converted</param>
        /// <param name="launcher">An instance of the Source SDK lib</param>
        /// <returns></returns>
        public static byte[] FromBitmap(Bitmap bitmap, Launcher launcher, string[] properties)
        {
            if (bitmap == null || launcher == null)
                return null;

            string gamePath = launcher.GetCurrentGame().InstallPath;
            string modPath = launcher.GetCurrentMod().InstallPath;
            string filePath = modPath + "\\materialsrc";

            string vtexPath = gamePath + "\\bin\\vtex.exe";

            var tga = new TGA(bitmap);
            Directory.CreateDirectory(filePath);
            tga.Save(filePath + "\\temp.tga");
            File.WriteAllText(filePath + "\\temp.txt", string.Join("\r\n", properties));

            Process process = new Process();
            process.StartInfo.FileName = vtexPath;
            process.StartInfo.Arguments = "-mkdir -quiet -nopause -shader UnlitGeneric temp.tga";
            process.StartInfo.WorkingDirectory = filePath;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
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

            MemoryStream memoryStream = new MemoryStream(vtf);

            VtfFile vtfFile = new VtfFile(memoryStream);
            VtfImage vtfImage = vtfFile.Images[vtfFile.Images.Count - 1];

            var bitmap = ByteArrayToBitmap(vtfImage.GetBgra32Data(), vtfImage.Width, vtfImage.Height);
            return bitmap;
        }

        private static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            BitmapData bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int numbytes = bmpdata.Stride * bitmap.Height;
            byte[] bytedata = new byte[numbytes];
            IntPtr ptr = bmpdata.Scan0;

            Marshal.Copy(ptr, bytedata, 0, numbytes);

            bitmap.UnlockBits(bmpdata);

            return bytedata;
        }

        private static Bitmap ByteArrayToBitmap(byte[] bytedata, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int numbytes = bmpdata.Stride * bitmap.Height;
            IntPtr ptr = bmpdata.Scan0;

            Marshal.Copy(bytedata, 0, ptr, numbytes);
            bitmap.UnlockBits(bmpdata);
            return bitmap;
        }
    }
}
