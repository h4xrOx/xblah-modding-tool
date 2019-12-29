using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_source1ide.SourceSDK
{
    class PCF
    {
        public static bool containsEffect(List<string> effects, string fullPath)
        {
            List<string> strings = ListStrings(fullPath);

            foreach(string effect in effects)
            {
                if (strings.Contains(effect))
                    return true;
            }
            return false;
        }

        private static List<string> ListStrings(string fullPath)
        {
            byte[] byteArray = File.ReadAllBytes(fullPath);
            List<string> list = new List<string>();

            List<char> chars = new List<char>();
            foreach (byte b in byteArray)
            {
                if (b == 0 && chars.Count > 0)
                {
                    string word = new String(chars.ToArray());
                    list.Add(word);
                    chars.Clear();
                }
                else if (b > 0)
                    chars.Add(Convert.ToChar(b));
            }

            return list;
        }

        public static List<string> getAllFiles(Steam sourceSDK)
        {
            List<string> searchPaths = sourceSDK.getModSearchPaths();

            List<string> files = new List<string>();

            

            foreach(string path in searchPaths)
                if (Directory.Exists(path + "\\particles"))
                    files.AddRange(Directory.GetFiles(path + "\\particles", "*.pcf", SearchOption.AllDirectories));

            return files;
        }


        public static void read(string relativePath, string game, string mod, Steam sourceSDK)
        {

            //List<string> particles = ListStrings(fullPath).Where(x => x.Contains("particle")).ToList();


            Debugger.Break();
        }
    }
}
