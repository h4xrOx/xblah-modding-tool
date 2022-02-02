using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xblah_modding_lib;
using xblah_modding_lib.Packages;
using xblah_modding_lib.Packages.UnpackedPackage;

namespace xblah_modding_tool.Materials
{
    public partial class ShaderConverterDialog : DevExpress.XtraEditors.XtraForm
    {
        Launcher launcher;
        PackageManager packageManager;
        Dictionary<string, List<PackageFile>> shaders;

        public ShaderConverterDialog(Launcher launcher)
        {
            this.launcher = launcher;
            InitializeComponent();

            GetAllMaterials();
            PopulateShadersTree();
        }

        private void GetAllMaterials()
        {
            packageManager = new PackageManager(launcher, "materials");
            shaders = new Dictionary<string, List<PackageFile>> ();

            foreach (PackageArchive archive in packageManager.Archives)
            {
                foreach (PackageDirectory directory in archive.Directories)
                {
                    foreach (PackageFile entry in directory.Entries)
                    {
                        if (entry.Extension != "vmt")
                            continue;

                        KeyValue data = KeyValue.ReadChunk(entry.Data);

                        if (data == null)
                            continue;

                        string shader = data.getKey();
                        //System.Diagnostics.Debugger.Break();

                        if (!shaders.ContainsKey(shader))
                            shaders[shader] = new List<PackageFile>();

                        shaders[shader].Add(entry);
                    }
                }
            }
        }

        private void PopulateShadersTree()
        {
            treeList1.BeginUnboundLoad();
            treeList1.Nodes.Clear();
            foreach(KeyValuePair<string, List<PackageFile>> shader in shaders)
            {
                treeList1.AppendNode(new object[] { shader.Key, shader.Key }, null);
            }

            treeList1.EndUnboundLoad();
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> shaderConversion = new Dictionary<string, string>();

            foreach(TreeListNode node in treeList1.Nodes)
            {
                string oldShader = node["oldName"].ToString().ToLower();
                string newShader = node["newName"].ToString().ToLower();

                if (oldShader == newShader)
                    continue;

                shaderConversion.Add(oldShader, newShader);
            }

            string customFolderDirectory = launcher.GetCurrentMod().InstallPath + "\\custom\\shader_refactor\\";
            Directory.CreateDirectory(customFolderDirectory);

            string modPath = launcher.GetCurrentMod().InstallPath;

            foreach (var shader in shaderConversion)
            {
                foreach(var entry in shaders[shader.Key])
                {
                    KeyValue keyValue = KeyValue.ReadChunk(entry.Data);
                    keyValue.setKey(shader.Value);

                    PackageArchive archive = entry.Directory.ParentArchive;
                    string archivePath = archive.ArchivePath;

                    if (archivePath.StartsWith(modPath) && archive is UnpackedArchive)
                    {
                        // In mod folder
                        KeyValue.writeChunkFile(archivePath + "\\" + entry.FullPath, keyValue, true, new UTF8Encoding(false));
                        
                    } else
                    {
                        // Mounted from other folders
                        Directory.CreateDirectory(customFolderDirectory + entry.Path);
                        KeyValue.writeChunkFile(customFolderDirectory + entry.FullPath, keyValue, true, new UTF8Encoding(false));
                    }
                }
            }
        }
    }
}