using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpVPK.Exceptions;
using SourceSDK.Packages.VPKPackage.V1;

namespace SourceSDK.Packages.VPKPackage
{
    public class VpkArchive : PackageArchive
    {
        public bool IsMultiPart { get; set; }
        private VpkReaderBase _reader;

        internal List<ArchivePart> Parts { get; set; }

        public override void Load(string filename, string rootPath)
        {
            ArchivePath = filename;

            if (!File.Exists(ArchivePath))
            {
                ArchivePath = ArchivePath.Remove(ArchivePath.Length - ".vpk".Length) + "_dir.vpk";

                if (!File.Exists(ArchivePath))
                {
#if DEBUG
                    //throw new FileNotFoundException("File not found", filename);
#endif
                    return;
                }
            }
            IsMultiPart = ArchivePath.EndsWith("_dir.vpk");
            if (IsMultiPart)
                LoadParts(ArchivePath);

            // Read V1 format
            _reader = new VpkReaderV1(ArchivePath);
            if (!_reader.ReadArchiveHeader().Verify())
            {
                // Read V2 format
                _reader = new V2.VpkReaderV2(ArchivePath);
                if (!_reader.ReadArchiveHeader().Verify())
                {
                    // Unknown format
                    throw new ArchiveParsingException("Invalid archive header");
                }
            }

            Directories.AddRange(_reader.ReadDirectories(this, rootPath).Where(i => i.Entries.Count > 0));

        }

        private void LoadParts(string filename)
        {
            Parts = new List<ArchivePart>();
            string fileBaseName = filename.Substring(0, filename.LastIndexOf("_")) + "_";
            foreach (var file in Directory.GetFiles(Path.GetDirectoryName(filename), Path.GetFileName(fileBaseName) + "*" + ".vpk"))
            {
                if (file == filename)
                    continue;

                string partIdxString = file.Substring(fileBaseName.Length, file.Length - fileBaseName.Length - ".vpk".Length);



                if (!int.TryParse(partIdxString, out int partIdx))
                    continue;

                Parts.Add(new ArchivePart((uint)new FileInfo(file).Length, partIdx, file));
            }
            Parts.Add(new ArchivePart((uint)new FileInfo(filename).Length, -1, filename));
            Parts = Parts.OrderBy(p => p.Index).ToList();
        }
    }
}
