using SourceSDK.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSDK.Maps
{
    public class BSP
    {
        /// <summary>
        /// Decompiles a map file and copies it to the mapsrc folder, preserving its name.
        /// Does not work for Source2 yet.
        /// </summary>
        /// <param name="packageFile">The map to be decompiled.</param>
        /// <param name="launcher">The launcher.</param>
        public static void Decompile(PackageFile packageFile, Launcher launcher)
        {
            switch (launcher.GetCurrentGame().engine)
            {
                case Engine.GOLDSRC:
                    {
                        byte[] map = MAP.FromBSP(packageFile, launcher);
                        File.WriteAllBytes(launcher.GetCurrentMod().InstallPath + "\\mapsrc\\" + packageFile.Filename + ".map", map);
                    }
                    break;
                case Engine.SOURCE:
                    {
                        byte[] vmf = VMF.FromBSP(packageFile, launcher);
                        File.WriteAllBytes(launcher.GetCurrentMod().InstallPath + "\\mapsrc\\" + packageFile.Filename + ".vmf", vmf);
                    }
                    break;
                case Engine.SOURCE2:
                    {
                        throw new NotImplementedException();
                    }
                    break;
            }
        }
    }
}
