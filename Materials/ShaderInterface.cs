using DevExpress.XtraEditors;
using SourceSDK;
using SourceSDK.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static source_modding_tool.MaterialEditor;

namespace source_modding_tool.Materials
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
