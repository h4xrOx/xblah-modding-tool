using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace xblah_modding_lib.Utils
{
    public static class SteamShortcuts
    {
        private static Dictionary<int, string> GetUserShortcutFiles()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach(string directory in Directory.GetDirectories(Launcher.GetInstallPath() + "\\userdata\\", "*config", SearchOption.AllDirectories))
            {
                if (int.TryParse(directory.Replace(Launcher.GetInstallPath() + "\\userdata\\", "").Replace("\\config", ""), out int userId))
                {
                    result.Add(userId, directory + "\\shortcuts.vdf");
                }
            }
            /*foreach (string file in Directory.GetFiles(Launcher.GetInstallPath() + "\\userdata\\", "shortcuts.vdf", SearchOption.AllDirectories))
            {
                int userId = int.Parse(file.Replace(Launcher.GetInstallPath() + "\\userdata\\", "").Replace("\\config\\shortcuts.vdf", ""));
                result.Add(userId, file);
            }*/
            return result;
        }

        public static Dictionary<int, List<Shortcut>> Read()
        {
            Dictionary<int, List<Shortcut>> shortcuts = new Dictionary<int, List<Shortcut>>();
            foreach (KeyValuePair<int, string> kvp in GetUserShortcutFiles())
            {
                List<Shortcut> userShortcuts = new List<Shortcut>();
                if (File.Exists(kvp.Value))
                    userShortcuts.AddRange(ParseShortcuts(File.ReadAllText(kvp.Value)));
                    
                shortcuts.Add(kvp.Key, userShortcuts);
            }

            return shortcuts;
        }

        public class Shortcut
        {
            public string AppName { get; set; } = "";
            public string Exe { get; set; } = "";
            public string StartDir { get; set; } = "";
            public string Icon { get; set; } = "";
            public string ShortcutPath { get; set; } = "";
            public string LaunchOptions { get; set; } = "";
            public bool Hidden { get; set; } = false;
            public List<string> Tags { get; set; } = new List<string>();

            public Shortcut()
            {

            }

            private string GetSHA1()
            {
                var data = Encoding.ASCII.GetBytes(Exe);
                var hashData = new SHA1Managed().ComputeHash(data);
                var hash = string.Empty;
                foreach (var b in hashData)
                {
                    hash += b.ToString("X2");
                }
                return hash;
            }

            /// <summary>
            /// Not working
            /// </summary>
            /// <returns></returns>
            public string GetAppID()
            {
                //var longValue = new Long(crcValue, crcValue, true);
                //longValue = longValue.or(0x80000000);
                //longValue = longValue.shl(32);
                //longValue = longValue.or(0x02000000);
                //return result.ToString();

                return "";
            }
        }

        public static void Write(Dictionary<int, List<Shortcut>> shortcuts)
        {
            foreach (KeyValuePair<int, string> kvp in GetUserShortcutFiles())
            {
                List<Shortcut> userShortcuts = shortcuts[kvp.Key];

                File.WriteAllText(kvp.Value, BuildShortcuts(userShortcuts));
            }
        }

        private static List<Shortcut> ParseShortcuts(string shortcutsString)
        {
            List<Shortcut> result = new List<Shortcut>();

            int start = shortcutsString.IndexOf("\u0000shortcuts\u0000") + "\u0000shortcuts\u0000".Length;
            int end = shortcutsString.LastIndexOf("\u0008\u0008");
            shortcutsString = shortcutsString.Substring(start, end - start);

            Shortcut shortcut = null;

            string word = "";
            string key = "";
            bool readingTags = false;
            int tagId = -1;
            
            foreach (char c in shortcutsString.ToCharArray())
            {
                if (c == '\u0000')
                {
                    if (word.EndsWith("\u0001appname"))
                    {
                        if (shortcut != null)
                            result.Add(shortcut);

                        // New shortcut
                        shortcut = new Shortcut();
                        key = "\u0001appname";
                    }
                    else if (
                      word == "\u0001exe" ||
                      word == "\u0001StartDir" ||
                      word == "\u0001icon" ||
                      word == "\u0001ShortcutPath" ||
                      word == "\u0001LaunchOptions" ||
                      word == "\u0002hidden"
                      )
                    {
                        key = word;
                    }
                    else if (word == "tags")
                    {
                        readingTags = true;
                    }
                    else if (key != "")
                    {
                        switch (key)
                        {
                            case "\u0001appname":
                                shortcut.AppName = word;
                                break;
                            case "\u0001exe":
                                shortcut.Exe = word.Trim('"');
                                break;
                            case "\u0001StartDir":
                                shortcut.StartDir = word.Trim('"');
                                break;
                            case "\u0001icon":
                                shortcut.Icon = word;
                                break;
                            case "\u0001ShortcutPath":
                                shortcut.ShortcutPath = word;
                                break;
                            case "\u0001LaunchOptions":
                                shortcut.LaunchOptions = word;
                                break;
                            case "\u0002hidden":
                                shortcut.Hidden = (word == "\u0001");
                                break;
                            default:

                                break;
                        }

                        key = "";
                    }
                    else if (readingTags)
                    {
                        if (word.StartsWith("\u0001"))
                        {
                            tagId = int.Parse(word.Substring("\u0001".Length));
                        }
                        else if (tagId >= 0)
                        {
                            shortcut.Tags.Add(word);
                            tagId = -1;
                        }
                        else
                        {
                            readingTags = false;
                        }
                    }

                    word = "";
                }
                else
                {
                    word += c;
                }
            }

            if (shortcut != null)
                result.Add(shortcut);

            return result;
        }

        private static string BuildShortcuts(List<Shortcut> shortcuts)
        {
            string shortcutsString = "\u0000shortcuts\u0000";

            for (int i = 0; i < shortcuts.Count; i++)
            {
                shortcutsString += "\u0000" + i + "\u0000";
                shortcutsString += BuildShortcut(shortcuts[i]);
                shortcutsString += "\u0008";
            }

            shortcutsString += "\u0008\u0008";

            return shortcutsString;
        }

        private static string BuildShortcut(Shortcut shortcut)
        {
            string shortcutString = "";
            //shortcutString += "\u0002appid\u0000" + shortcut.GetAppID() + "\u0000";
            shortcutString += "\u0001appname\u0000" + shortcut.AppName + "\u0000";
            shortcutString += "\u0001exe\u0000\"" + shortcut.Exe + "\"\u0000";
            shortcutString += "\u0001StartDir\u0000\"" + shortcut.StartDir + "\"\u0000";
            shortcutString += "\u0001icon\u0000" + shortcut.Icon + "\u0000";
            shortcutString += "\u0001ShortcutPath\u0000" + shortcut.ShortcutPath + "\u0000";
            shortcutString += "\u0001LaunchOptions\u0000" + shortcut.LaunchOptions + "\u0000";
            shortcutString += "\u0002hidden\u0000" + (shortcut.Hidden ? "\u0001" : "\u0000") + "\u0000\u0000\u0000";
            shortcutString += buildTags(shortcut.Tags);

            return shortcutString;
        }

        private static string buildTags(List<string> tags)
        {
            var tagString = "\u0000tags\u0000";
            for (var i = 0; i < tags.Count; ++i)
            {
                tagString += "\u0001" + i + "\u0000" + tags[i] + "\u0000";
            }
            tagString += "\u0008";
            return tagString;
        }
    }
}
