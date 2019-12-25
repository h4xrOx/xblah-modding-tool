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
        public static bool containsEffect(string effectName, string relativePath, string game, string mod, Steam sourceSDK)
        {
            return false;
        }

        private static List<string> ListStrings(string relativePath, string game, string mod, Steam sourceSDK)
        {
            string gamePath = sourceSDK.GetGamePath(game);
            string modPath = sourceSDK.GetModPath(game, mod);

            string fullPath = modPath + "\\" + relativePath;

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


        public static void read(string relativePath, string game, string mod, Steam sourceSDK)
        {

            List<string> particles = ListStrings(relativePath, game, mod, sourceSDK).Where(x => x.Contains("particle")).ToList();

            Debugger.Break();
        }
    }
}
