﻿using System.IO;
using System.Text;

namespace SourceSDK.Packages.VPKPackage.Utilities
{
    internal static class StreamHelpers
    {
        /// <summary>
        /// Reads a null terminated string.
        /// </summary>
        /// <returns>String.</returns>
        /// <param name="stream">Stream.</param>
        /// <param name="encoding">Encoding.</param>
        public static string ReadNullTermString(this BinaryReader stream, Encoding encoding)
        {
            var characterSize = encoding.GetByteCount("e");

            var ms = new MemoryStream();

            while (true)
            {
                var data = new byte[characterSize];

                int bytesRead;
                int totalRead = 0;
                while ((bytesRead = stream.Read(data, totalRead, characterSize - totalRead)) != 0)
                {
                    totalRead += bytesRead;
                }

                if (encoding.GetString(data, 0, characterSize) == "\0")
                {
                    break;
                }

                ms.Write(data, 0, data.Length);
            }

            return encoding.GetString(ms.ToArray());
        }
    }
}
