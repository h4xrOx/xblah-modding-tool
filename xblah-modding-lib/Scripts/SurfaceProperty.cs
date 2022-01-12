using xblah_modding_lib.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xblah_modding_lib.Scripts
{
    public static class SurfaceProperty
    {
        public static string[] GetStringArray(PackageManager packageManager)
        {
            List<string> result = new List<string>();

            PackageFile surfacePropertiesManifestFile = packageManager.GetFile("scripts/surfaceproperties_manifest.txt");

            KeyValue surfacePropertiesManifestKV = KeyValue.ReadChunk(System.Text.Encoding.UTF8.GetString(surfacePropertiesManifestFile.Data));

            foreach (KeyValue surfacePropertiesManifestChildKV in surfacePropertiesManifestKV.getChildren())
            {
                PackageFile surfacePropertiesFile = packageManager.GetFile(surfacePropertiesManifestChildKV.getValue());

                KeyValue surfacePropertiesKV = KeyValue.ReadChunk(System.Text.Encoding.UTF8.GetString(surfacePropertiesFile.Data));


                foreach (var surfacePropertiesChildKV in surfacePropertiesKV.getChildren())
                {
                    result.Add(surfacePropertiesChildKV.getKey());
                }
            }

            return result.Distinct().OrderBy(s => s).ToArray();
        }
    }
}
