using System.Numerics;

namespace xblah_modding_lib.Materials.VTFFile
{
    public class VtfHeader
    {
        public decimal Version { get; set; }
        public VtfImageFlag Flags { get; set; }
        public Vector3 Reflectivity { get; set; }
        public float BumpmapScale { get; set; }
    }
}