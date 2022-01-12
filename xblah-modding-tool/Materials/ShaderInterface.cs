using DevExpress.XtraEditors;
using xblah_modding_lib;
using xblah_modding_lib.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static xblah_modding_tool.MaterialEditor;

namespace xblah_modding_tool.Materials
{
    public interface ShaderInterface
    {
        Launcher Launcher { get; set; }
        PackageManager PackageManager { get; set; }


        Dictionary<string, PictureEdit> PictureEdits { get; set; }
        Dictionary<string, Texture> Textures { get; set; }



        event EventHandler OnUpdated;

        string VMT { get; }

        string Shader { get; }

        string RelativePath { get; set; }

        /// <summary>
        /// Adds all the picture edits to a Dictionary, so they can be accessed by key.
        /// </summary>
        void PopulatePictureEdits();

        void LoadMaterial(PackageFile file);

        KeyValue GetVMT();

    }
}
