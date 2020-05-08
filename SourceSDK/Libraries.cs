using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace source_modding_tool
{
    public class Libraries
    {
        private List<string> steamLibs = new List<string>();
        private List<string> userLibs = new List<string>();

        public Libraries()
        {
            steamLibs = new List<string>();
            userLibs = new List<string>();
        }

        public List<string> GetList()
        {
            List<string> result = new List<string>();
            result.AddRange(steamLibs);
            result.AddRange(userLibs);
            return result;
        }

        public void SaveUserLibraries()
        {
            string userPath = AppDomain.CurrentDomain.BaseDirectory + "/libraryfolders.vdf";

            SourceSDK.KeyValue root = new SourceSDK.KeyValue("LibraryFolders");

            for (int i = 0; i < userLibs.Count; i++)
            {
                root.addChild(new SourceSDK.KeyValue(i.ToString(), userLibs[i]));
            }

            SourceSDK.KeyValue.writeChunkFile(userPath, root);
        }

        public void AddUserLibrary(string path)
        {
            userLibs.Add(path);
            SaveUserLibraries();
        }

        public List<string> GetSteamLibraries() { return steamLibs; }

        public List<string> GetUserLibraries() { return userLibs; }

        public void Load()
        {
            LoadSteamLibraries();
            LoadUserLibraries();
        }

        public void LoadSteamLibraries()
        {
            String steamPath = Launcher.GetInstallPath();

            steamLibs = new List<string>();
            if (Directory.Exists(steamPath))
            {
                steamLibs.Add(steamPath);

                if (File.Exists(steamPath + "\\steamapps\\libraryfolders.vdf"))
                {
                    SourceSDK.KeyValue root = SourceSDK.KeyValue
                        .readChunkfile(steamPath + "\\steamapps\\libraryfolders.vdf");

                    foreach (SourceSDK.KeyValue child in root.getChildren())
                    {
                        string dir = child.getValue().Replace("\\\\", "\\");
                        if (Directory.Exists(dir))
                            steamLibs.Add(dir);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Could not find file \"" + steamPath + "\\steamapps\\libraryfolders.vdf\".");
                }
            }
            else
            {
                //XtraMessageBox.Show("Could not find Steam install directory.");
            }
        }

        public void LoadUserLibraries()
        {
            userLibs = new List<string>();
            string userPath = AppDomain.CurrentDomain.BaseDirectory + "/libraryfolders.vdf";
            if (File.Exists(userPath))
            {
                SourceSDK.KeyValue root = SourceSDK.KeyValue.readChunkfile(userPath);

                foreach (SourceSDK.KeyValue child in root.getChildren())
                {
                    string dir = child.getValue().Replace("\\\\", "\\");
                    if (Directory.Exists(dir))
                        userLibs.Add(dir);
                }
            }
        }

        public void RemoveUserLibrary(string path)
        {
            userLibs.Remove(path);
            SaveUserLibraries();
        }
    }
}
