/*
 * Read() function was mostly taken from Rick's Gibbed.Valve.FileFormats,
 * which is subject to this license:
 *
 * Copyright (c) 2008 Rick (rick 'at' gibbed 'dot' us)
 *
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software
 * in a product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source
 * distribution.
 */

using SourceSDK.Packages.VPKPackage.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SourceSDK.Packages.VPKPackage
{
    public class VpkArchive : PackageArchive, IDisposable
    {
        public const int MAGIC = 0x55AA1234;

        /// <summary>
        /// Always '/' as per Valve's vpk implementation.
        /// </summary>
        public const char DirectorySeparatorChar = '/';

        private BinaryReader Reader;
        public bool IsDirVPK;
        public uint HeaderSize;

        /// <summary>
        /// Gets the File Name
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the VPK version.
        /// </summary>
        public uint Version { get; private set; }

        /// <summary>
        /// Gets the size in bytes of the directory tree.
        /// </summary>
        public uint TreeSize { get; private set; }

        /// <summary>
        /// Gets how many bytes of file content are stored in this VPK file (0 in CSGO).
        /// </summary>
        public uint FileDataSectionSize { get; private set; }

        /// <summary>
        /// Gets the size in bytes of the section containing MD5 checksums for external archive content.
        /// </summary>
        public uint ArchiveMD5SectionSize { get; private set; }

        /// <summary>
        /// Gets the size in bytes of the section containing MD5 checksums for content in this file.
        /// </summary>
        public uint OtherMD5SectionSize { get; private set; }

        /// <summary>
        /// Gets the size in bytes of the section containing the public key and signature.
        /// </summary>
        public uint SignatureSectionSize { get; private set; }

        /// <summary>
        /// Gets the MD5 checksum of the file tree.
        /// </summary>
        public byte[] TreeChecksum { get; private set; }

        /// <summary>
        /// Gets the MD5 checksum of the archive MD5 checksum section entries.
        /// </summary>
        public byte[] ArchiveMD5EntriesChecksum { get; private set; }

        /// <summary>
        /// Gets the MD5 checksum of the complete package until the signature structure.
        /// </summary>
        public byte[] WholeFileChecksum { get; private set; }

        /// <summary>
        /// Gets the public key.
        /// </summary>
        public byte[] PublicKey { get; private set; }

        /// <summary>
        /// Gets the signature.
        /// </summary>
        public byte[] Signature { get; private set; }

        /// <summary>
        /// Gets the archive MD5 checksum section entries. Also known as cache line hashes.
        /// </summary>
        public List<ArchiveMD5SectionEntry> ArchiveMD5Entries { get; private set; }

        /// <summary>
        /// Releases binary reader.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && Reader != null)
            {
                Reader.Dispose();
                Reader = null;
            }
        }

        /// <summary>
        /// Sets the file name.
        /// </summary>
        /// <param name="fileName">Filename.</param>
        private void SetFileName(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (fileName.EndsWith(".vpk", StringComparison.OrdinalIgnoreCase))
            {
                fileName = fileName.Substring(0, fileName.Length - 4);
            }

            if (fileName.EndsWith("_dir", StringComparison.OrdinalIgnoreCase))
            {
                IsDirVPK = true;

                fileName = fileName.Substring(0, fileName.Length - 4);
            }

            FileName = fileName;
        }

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

            SetFileName(ArchivePath);

            var input = new FileStream($"{FileName}{(IsDirVPK ? "_dir" : string.Empty)}.vpk", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (FileName == null)
            {
                throw new InvalidOperationException("If you call Read() directly with a stream, you must call SetFileName() first.");
            }

            Reader = new BinaryReader(input);

            if (Reader.ReadUInt32() != MAGIC)
            {
                throw new InvalidDataException("Given file is not a VPK.");
            }

            Version = Reader.ReadUInt32();
            TreeSize = Reader.ReadUInt32();

            if (Version == 1)
            {
                // Nothing else
            }
            else if (Version == 2)
            {
                FileDataSectionSize = Reader.ReadUInt32();
                ArchiveMD5SectionSize = Reader.ReadUInt32();
                OtherMD5SectionSize = Reader.ReadUInt32();
                SignatureSectionSize = Reader.ReadUInt32();
            }
            else
            {
                throw new InvalidDataException($"Bad VPK version. ({Version})");
            }

            HeaderSize = (uint)input.Position;

            ReadEntries(rootPath);

            if (Version == 2)
            {
                // Skip over file data, if any
                input.Position += FileDataSectionSize;

                ReadArchiveMD5Section();
                ReadOtherMD5Section();
                ReadSignatureSection();
            }
        }

        private void ReadEntries(string rootPath)
        {
            VpkDirectory vpkDirectory = null;

            // Types
            while (true)
            {
                var typeName = Reader.ReadNullTermString(Encoding.UTF8);

                if (string.IsNullOrEmpty(typeName))
                {
                    break;
                }

                var entries = new List<VpkFile>();

                // Directories
                while (true)
                {
                    var directoryName = Reader.ReadNullTermString(Encoding.UTF8);

                    if (directoryName?.Length == 0)
                    {
                        break;
                    }

                    if (rootPath == "" || (directoryName.EndsWith("/") ? directoryName : directoryName + "/").StartsWith(rootPath + "/"))
                    {
                        if (vpkDirectory == null || vpkDirectory.Path != directoryName)
                        {
                            vpkDirectory = new VpkDirectory(this, directoryName, new List<PackageFile>());
                            Directories.Add(vpkDirectory);
                        }
                    }

                    // Files
                    while (true)
                    {
                        var fileName = Reader.ReadNullTermString(Encoding.UTF8);

                        if (fileName?.Length == 0)
                        {
                            break;
                        }

                        var entry = new VpkFile
                        {
                            Filename = fileName,
                            Path = directoryName,
                            Extension = typeName,
                            CRC32 = Reader.ReadUInt32(),
                            SmallData = new byte[Reader.ReadUInt16()],
                            ArchiveIndex = Reader.ReadUInt16(),
                            Offset = Reader.ReadUInt32(),
                            Length = Reader.ReadUInt32()
                        };

                        if (Reader.ReadUInt16() != 0xFFFF)
                        {
                            throw new FormatException("Invalid terminator.");
                        }

                        if (entry.SmallData.Length > 0)
                        {
                            int bytesRead;
                            int totalRead = 0;
                            while ((bytesRead = Reader.Read(entry.SmallData, totalRead, entry.SmallData.Length - totalRead)) != 0)
                            {
                                totalRead += bytesRead;
                            }
                        }

                        if (rootPath == "" || (directoryName.EndsWith("/") ? directoryName : directoryName + "/").StartsWith(rootPath + "/"))
                        {
                            entries.Add(entry);
                            vpkDirectory.Entries.Add(entry);
                            entry.Directory = vpkDirectory;
                        }
                        
                    }
                }
            }
        }

        /// <summary>
        /// Verify checksums and signatures provided in the VPK
        /// </summary>
        public void VerifyHashes()
        {
            if (Version != 2)
            {
                throw new InvalidDataException("Only version 2 is supported.");
            }

            using (var md5 = MD5.Create())
            {
                Reader.BaseStream.Position = 0;

                var hash = md5.ComputeHash(Reader.ReadBytes((int)(HeaderSize + TreeSize + FileDataSectionSize + ArchiveMD5SectionSize + 32)));

                if (!hash.SequenceEqual(WholeFileChecksum))
                {
                    throw new InvalidDataException($"Package checksum mismatch ({BitConverter.ToString(hash)} != expected {BitConverter.ToString(WholeFileChecksum)})");
                }

                Reader.BaseStream.Position = HeaderSize;

                hash = md5.ComputeHash(Reader.ReadBytes((int)TreeSize));

                if (!hash.SequenceEqual(TreeChecksum))
                {
                    throw new InvalidDataException($"File tree checksum mismatch ({BitConverter.ToString(hash)} != expected {BitConverter.ToString(TreeChecksum)})");
                }

                Reader.BaseStream.Position = HeaderSize + TreeSize + FileDataSectionSize;

                hash = md5.ComputeHash(Reader.ReadBytes((int)ArchiveMD5SectionSize));

                if (!hash.SequenceEqual(ArchiveMD5EntriesChecksum))
                {
                    throw new InvalidDataException($"Archive MD5 entries checksum mismatch ({BitConverter.ToString(hash)} != expected {BitConverter.ToString(ArchiveMD5EntriesChecksum)})");
                }

                // TODO: verify archive checksums
            }

            if (PublicKey == null || Signature == null)
            {
                return;
            }

            if (!IsSignatureValid())
            {
                throw new InvalidDataException("VPK signature is not valid.");
            }
        }

        /// <summary>
        /// Verifies the RSA signature.
        /// </summary>
        /// <returns>True if signature is valid, false otherwise.</returns>
        public bool IsSignatureValid()
        {
            Reader.BaseStream.Position = 0;

            var keyParser = new AsnKeyParser(PublicKey);

            var rsa = RSA.Create();
            rsa.ImportParameters(keyParser.ParseRSAPublicKey());

            var data = Reader.ReadBytes((int)(HeaderSize + TreeSize + FileDataSectionSize + ArchiveMD5SectionSize + OtherMD5SectionSize));

            return rsa.VerifyData(data, Signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        private void ReadArchiveMD5Section()
        {
            ArchiveMD5Entries = new List<ArchiveMD5SectionEntry>();

            if (ArchiveMD5SectionSize == 0)
            {
                return;
            }

            var entries = ArchiveMD5SectionSize / 28; // 28 is sizeof(VPK_MD5SectionEntry), which is int + int + int + 16 chars

            for (var i = 0; i < entries; i++)
            {
                ArchiveMD5Entries.Add(new ArchiveMD5SectionEntry
                {
                    ArchiveIndex = Reader.ReadUInt32(),
                    Offset = Reader.ReadUInt32(),
                    Length = Reader.ReadUInt32(),
                    Checksum = Reader.ReadBytes(16)
                });
            }
        }

        private void ReadOtherMD5Section()
        {
            if (OtherMD5SectionSize != 48)
            {
                throw new InvalidDataException($"Encountered OtherMD5Section with size of {OtherMD5SectionSize} (should be 48)");
            }

            TreeChecksum = Reader.ReadBytes(16);
            ArchiveMD5EntriesChecksum = Reader.ReadBytes(16);
            WholeFileChecksum = Reader.ReadBytes(16);
        }

        private void ReadSignatureSection()
        {
            if (SignatureSectionSize == 0)
            {
                return;
            }

            var publicKeySize = Reader.ReadInt32();
            PublicKey = Reader.ReadBytes(publicKeySize);

            var signatureSize = Reader.ReadInt32();
            Signature = Reader.ReadBytes(signatureSize);
        }
    }
}
