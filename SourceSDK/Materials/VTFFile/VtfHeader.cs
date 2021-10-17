using System.Numerics;

namespace SourceSDK.Materials.VTFFile
{
    public class VtfHeader
    {
        public decimal Version { get; set; }
        public VtfImageFlag Flags { get; set; }
        public Vector3 Reflectivity { get; set; }
        public float BumpmapScale { get; set; }
    }
}