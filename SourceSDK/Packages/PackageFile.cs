namespace SourceSDK.Packages
{
    public abstract class PackageFile
    {
        /// <summary>
        /// Gets or sets the file extension.
        /// If the file has no extension, this is an empty string.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the name of the directory this file is in.
        /// '/' is always used as a dictionary separator in Valve's implementation.
        /// Directory names are also always lower cased in Valve's implementation.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets file name of this entry.
        /// </summary>
        public string Filename { get; set; }
        public PackageDirectory Directory { get; set; }
        public byte[] Data { get { return ReadData(); } }

        public string FullPath
        {
            get
            {
                return Path + "/" + Filename + "." + Extension;
            }
        }

        protected abstract byte[] ReadData();

        public abstract void CopyTo(string destinationPath);
    }
}
