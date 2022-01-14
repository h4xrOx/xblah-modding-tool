using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using xblah_modding_lib;
using xblah_modding_lib.Packages;
using xblah_modding_lib.Packages.UnpackedPackage;
using xblah_modding_lib.Packages.VPKPackage;

namespace xblah_modding_tool.Modding
{
    public partial class RemoveDuplicatesForm : DevExpress.XtraEditors.XtraForm
    {
        Launcher launcher;
        PackageManager packageManager;

        Dictionary<string, List<PackageFile>> duplicateEntries;
        public RemoveDuplicatesForm(Launcher launcher)
        {
            this.launcher = launcher;

            InitializeComponent();

            FindDuplicates();
            PopulateDuplicatesTree();
        }

        public void FindDuplicates()
        {
            Dictionary<string, List<PackageFile>> entries = new Dictionary<string, List<PackageFile>>();

            packageManager = new PackageManager(launcher, "");

            foreach (PackageArchive archive in packageManager.Archives)
            {
                foreach(PackageDirectory directory in archive.Directories)
                {
                    foreach(PackageFile entry in directory.Entries)
                    {
                        if (!entries.ContainsKey(entry.FullPath))
                        {
                            entries.Add(entry.FullPath, new List<PackageFile>(new PackageFile[] { entry }));
                        } else
                        {
                            entries[entry.FullPath].Add(entry);
                        }
                    }
                }
            }

            duplicateEntries = entries.Where(e => e.Value.Count > 1).ToDictionary(e => e.Key, e => e.Value);
        }

        public void PopulateDuplicatesTree()
        {
            duplicatesTree.BeginUnboundLoad();
            duplicatesTree.Nodes.Clear();

            int count = 0;

            foreach(KeyValuePair<string, List<PackageFile>> keyValuePair in duplicateEntries)
            {
                int vpkCount = keyValuePair.Value.Where(e => e is VpkFile).Count();
                int unpackedCount = keyValuePair.Value.Where(e => e is UnpackedFile).Count();

                // If everything is in VPKs, we can't delete it.
                if (unpackedCount == 0)
                {
                    continue;
                }

                byte[] data = null;
                bool different = false;
                foreach (PackageFile k in keyValuePair.Value)
                {
                    //if (k is VpkFile)
                        //continue;

                    var data2 = k.Data;
                    if (data != null && !ByteArrayCompare(data2, data))
                    {
                        different = true;
                        //System.Diagnostics.Debugger.Break();
                        break;
                    }
                    data = data2;
                }
                if (different)
                {
                    continue;
                }
                
                TreeListNode node = duplicatesTree.AppendNode(new object[] { keyValuePair.Key, vpkCount, unpackedCount }, null);
                node.Tag = keyValuePair.Value;
                node.Checked = true;

                count++;
            }

            duplicatesCountLabel.Text = count + " item(s)";

            duplicatesTree.EndUnboundLoad();
        }

        // byte[] is implicitly convertible to ReadOnlySpan<byte>
        static bool ByteArrayCompare(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
        {
            return a1.SequenceEqual(a2);
        }

        private void duplicatesTree_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            List<PackageFile> entries = e.Node.Tag as List<PackageFile>;

            if (entries == null)
                return;

            packageTree.BeginUnboundLoad();
            packageTree.Nodes.Clear();

            foreach (PackageFile entry in entries)
            {
                packageTree.AppendNode(new object[] { entry.Directory.ParentArchive.Name, (entry.Directory.ParentArchive is VpkArchive ? "VPK" : "Unpacked") }, null);
            }

            packageTree.EndUnboundLoad();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            foreach(TreeListNode node in duplicatesTree.GetAllCheckedNodes())
            {
                List<PackageFile> entries = node.Tag as List<PackageFile>;

                for (int i = 0; i < entries.Count - 1; i++)
                {
                    PackageFile entry = entries[i];
                    if (entry is UnpackedFile)
                    {
                        var archive = entry.Directory.ParentArchive;
                        var path = archive.ArchivePath + "\\" + entry.FullPath.Replace("/", "\\");
                        File.Delete(path);
                    }

                }
            }
        }
    }
}