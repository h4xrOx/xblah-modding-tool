using DevExpress.Data.Linq;
using DevExpress.Internal;
using DevExpress.XtraEditors.Filtering.Templates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace source_modding_tool
{
    public class Updater
    {
        private static String PATCH_UPDATE_FILENAME = "update_patch.dll";
        private static String MINOR_UPDATE_FILENAME = "Release.7z";
        private static String USER = "jean-knapp";
        private static String REPOSITORY = "windows-source-modding-tool";

        private UpdaterInterface updaterInterface;

        UpdateType updateType;
        string newVersion = "";
        DateTime newVersionReleaseDate = DateTime.Now;

        string updateUrl = "";

        public Updater(UpdaterInterface updaterInterface)
        {
            this.updaterInterface = updaterInterface;
            CheckForUpdates();
        }
        public enum UpdateType
        {
            MAJOR = 2,
            MINOR = 1,
            PATCH = 0,
            NONE = -1
        }
        public async Task CheckForUpdates()
        {

            // Delete old update files.
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + PATCH_UPDATE_FILENAME))
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + PATCH_UPDATE_FILENAME);

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + MINOR_UPDATE_FILENAME))
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + MINOR_UPDATE_FILENAME);

            if (File.Exists(Application.ExecutablePath + ".old"))
                File.Delete(Application.ExecutablePath + ".old");

            if (Directory.Exists(Application.ExecutablePath + ".old/"))
                Directory.Delete(Application.ExecutablePath + ".old/", true);

            using (WebClient client = new WebClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                client.Headers.Add("user-agent", "Only a test!");

                string jsonPath = "https://api.github.com/repos/" + USER + "/" + REPOSITORY + "/releases/latest";

                string json = await client.DownloadStringTaskAsync(jsonPath);
                dynamic obj = JsonConvert.DeserializeObject(json);

                string tagName = obj.tag_name;
                tagName = tagName.Substring(1);

                if (tagName.Contains('-'))
                    tagName = tagName.Substring(0, tagName.IndexOf('-'));
                this.newVersion = tagName;

                int[] newVersion = this.newVersion.Split('.').Select(int.Parse).ToArray();
                int[] currentVersion = Application.ProductVersion.Split('.').Select(int.Parse).ToArray();

                this.newVersionReleaseDate = obj.created_at;

                updateType = UpdateType.NONE;

                if (newVersion[0] > currentVersion[0])
                {
                    // New product version, should check website
                    updateType = UpdateType.MAJOR;
                }
                else if (newVersion[1] > currentVersion[1])
                {
                    // New major update, requires full install (Release.7z)
                    updateType = UpdateType.MINOR;
                }
                else if (newVersion[2] > currentVersion[2])
                {
                    // New minor update, only requires updated exe (update_patch.exe)
                    updateType = UpdateType.PATCH;
                }

                dynamic assets = obj.assets;

                foreach (dynamic asset in assets)
                {

                    string fileName = asset.name;

                    if (updateType == UpdateType.MINOR && fileName == MINOR_UPDATE_FILENAME)
                    {
                        // Download and install it
                        updateUrl = asset.browser_download_url;
                        updaterInterface.OnUpdateAvailable(this.newVersion, this.newVersionReleaseDate);
                        break;
                    }
                    else if (updateType == UpdateType.PATCH && fileName == PATCH_UPDATE_FILENAME)
                    {
                        // Download and install it
                        updateUrl = asset.browser_download_url;
                        updaterInterface.OnUpdateAvailable(this.newVersion, this.newVersionReleaseDate);
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        private void DownloadPatchCompleted(object sender, AsyncCompletedEventArgs e)
        {
            updaterInterface.OnUpdateReady(newVersion, newVersionReleaseDate);
        }

        public void DownloadUpdate()
        {
            if (updateType == UpdateType.MINOR)
            {
                // Download and install it
                using (WebClient client = new WebClient())
                {
                    client.DownloadFileCompleted += DownloadMinorCompleted;
                    client.DownloadFileAsync(new Uri(updateUrl), MINOR_UPDATE_FILENAME);
                }
            }
            else if (updateType == UpdateType.PATCH)
            {
                // Download and install it
                using (WebClient client = new WebClient())
                {
                    client.DownloadFileCompleted += DownloadPatchCompleted;
                    client.DownloadFileAsync(new Uri(updateUrl), PATCH_UPDATE_FILENAME);
                }
            }
            else
            {

            }
        }

        public void ApplyUpdate()
        {
            if (updateType == UpdateType.PATCH)
            {
                ApplyPatch();
            } else if (updateType == UpdateType.MINOR)
            {
                ApplyMinor();
            }
        }

        private void ApplyMinor()
        {
            string[] oldFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory);
            string[] oldDirectories = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory);

            Directory.CreateDirectory(Application.ExecutablePath + ".new");

            new SevenZipNET.SevenZipExtractor(AppDomain.CurrentDomain.BaseDirectory + MINOR_UPDATE_FILENAME).ExtractAll(Application.ExecutablePath + ".new");

            Directory.CreateDirectory(Application.ExecutablePath + ".old");

            // Move out old files
            foreach (string file in oldFiles)
            {
                try
                {
                    File.Move(file, Application.ExecutablePath + ".old/" + new FileInfo(file).Name);
                } catch (Exception) { }
                
            }

            // Move out old directories
            foreach (string file in oldDirectories)
            {
                try
                {
                    Directory.Delete(file, true);
                }
                catch (Exception) { }
            }

            // Move in new files and directories
            foreach (string file in Directory.GetFiles(Application.ExecutablePath + ".new/", "*.*", SearchOption.AllDirectories))
            {
                try
                {
                    Directory.CreateDirectory(new FileInfo(file.Replace(Application.ExecutablePath + ".new/", AppDomain.CurrentDomain.BaseDirectory)).Directory.FullName);
                    File.Move(file, file.Replace(Application.ExecutablePath + ".new/", AppDomain.CurrentDomain.BaseDirectory));
                }
                catch (Exception) { }
            }

            // Delete unused new files and directories
            Directory.Delete(Application.ExecutablePath + ".new/", true);

            // Restart application
            Application.Restart();
        }

        private void ApplyPatch()
        {
            string currentExePath = Application.ExecutablePath;
            File.Move(currentExePath, currentExePath + ".old");
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + PATCH_UPDATE_FILENAME, currentExePath);
            Application.Restart();
        }

        private void DownloadMinorCompleted(object sender, AsyncCompletedEventArgs e)
        {
            updaterInterface.OnUpdateReady(newVersion, newVersionReleaseDate);
        }

        public interface UpdaterInterface
        {
            void OnUpdateReady(string version, DateTime date);

            void OnUpdateAvailable(string version, DateTime date);
        }
    }
}
