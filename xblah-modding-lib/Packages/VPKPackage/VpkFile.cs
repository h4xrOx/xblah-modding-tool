using System;
using System.IO;

namespace xblah_modding_lib.Packages.VPKPackage
{
    public class VpkFile : PackageFile
    {
        /// <summary>
        /// Gets or sets the CRC32 checksum of this entry.
        /// </summary>
        public uint CRC32 { get; set; }

        /// <summary>
        /// Gets or sets the length in bytes.
        /// </summary>
        public uint Length { get; set; }

        /// <summary>
        /// Gets or sets the offset in the package.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Gets or sets which archive this entry is in.
        /// </summary>
        public ushort ArchiveIndex { get; set; }

        /// <summary>
        /// Gets the length in bytes by adding Length and length of SmallData.
        /// </summary>
        public uint TotalLength
        {
            get
            {
                var totalLength = Length;

                if (SmallData != null)
                {
                    totalLength += (uint)SmallData.Length;
                }

                return totalLength;
            }
        }

        /// <summary>
        /// Gets or sets the preloaded bytes.
        /// </summary>
        public byte[] SmallData { get; set; }

        /// <summary>
        /// Returns the file name and extension.
        /// </summary>
        /// <returns>File name and extension.</returns>
        public string GetFileName()
        {
            var fileName = Filename;

            if (Extension == " ")
            {
                return fileName;
            }

            return fileName + "." + Extension;
        }

        /// <summary>
        /// Returns the absolute path of the file in the package.
        /// </summary>
        /// <returns>Absolute path.</returns>
        public string GetFullPath()
        {
            if (Path == " ")
            {
                return GetFileName();
            }

            return Path + VpkArchive.DirectorySeparatorChar + GetFileName();
        }

        public override string ToString()
        {
            return $"{GetFullPath()} crc=0x{CRC32:x2} metadatasz={SmallData.Length} fnumber={ArchiveIndex} ofs=0x{Offset:x2} sz={Length}";
        }

        protected override byte[] ReadData()
        {
            byte[] output = new byte[SmallData.Length + Length];

            if (SmallData.Length > 0)
            {
                SmallData.CopyTo(output, 0);
            }

            if (Length > 0)
            {
                Stream fs = null;

                try
                {
                    var offset = Offset;

                    if (ArchiveIndex != 0x7FFF)
                    {

                        if (!(Directory.ParentArchive as VpkArchive).IsDirVPK)
                        {
                            throw new InvalidOperationException("Given VPK is not a _dir, but entry is referencing an external archive.");
                        }

                        var fileName = $"{(Directory.ParentArchive as VpkArchive).FileName}_{ArchiveIndex:D3}.vpk";

                        fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    }
                    else
                    {
                        var fileName = $"{(Directory.ParentArchive as VpkArchive).FileName}.vpk";

                        fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                        offset += (Directory.ParentArchive as VpkArchive).HeaderSize + (Directory.ParentArchive as VpkArchive).TreeSize;
                    }

                    fs.Seek(offset, SeekOrigin.Begin);

                    int length = (int)Length;
                    int readOffset = SmallData.Length;
                    int bytesRead;
                    int totalRead = 0;
                    while ((bytesRead = fs.Read(output, readOffset + totalRead, length - totalRead)) != 0)
                    {
                        totalRead += bytesRead;
                    }
                }
                finally
                {
                    if (ArchiveIndex != 0x7FFF)
                    {
                        fs?.Close();
                    }
                }
            }

            return output;
        }

        public override void CopyTo(string destinationPath)
        {
            File.WriteAllBytes(destinationPath, Data);
        }
    }
}
