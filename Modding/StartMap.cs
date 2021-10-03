using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using source_modding_tool.Tools;
using System.IO;
 
using DevExpress.XtraLayout;
using SourceSDK.Packages;
using SourceSDK;

namespace source_modding_tool.Modding
{
    public partial class StartMap : DevExpress.XtraEditors.XtraForm
    {
        KeyValue gameinfo;
        private Launcher launcher;

        Dictionary<string, BaseLayoutItem> fields;

        public StartMap()
        {
            InitializeComponent();

            startMapText.EditValue = "";
            trainingMapText.EditValue = "";
        }

        public StartMap(Launcher launcher) : this()
        {
            this.launcher = launcher;
        }

        private void startMapClearButton_Click(object sender, EventArgs e)
        {
            startMapText.EditValue = "";
        }

        private void trainingMapClearButton_Click(object sender, EventArgs e)
        {
            trainingMapText.EditValue = "";
        }

        private void startMapButton_Click(object sender, EventArgs e)
        {
            Game game = launcher.GetCurrentGame();
            LegacyFileExplorer form = new LegacyFileExplorer(launcher);
            form.RootDirectory = "maps/";
            form.Filter = "BSP Files (*.bsp)|*.bsp|VPK Files (*.vpk)|*.vpk";
            if (form.ShowDialog() == DialogResult.OK)
            {
                VPK.File file = form.selectedFiles[0];
                if ((file.type == ".bsp" && game.EngineID == Engine.GOLDSRC) || (file.type == ".vpk" && file.path.StartsWith("maps/") && game.EngineID == Engine.SOURCE2))
                {
                    // It's a map
                    string mapName = Path.GetFileNameWithoutExtension(file.path);
                    
                    if (sender == startMapButton)
                    {
                        startMapText.EditValue = mapName;
                    }
                    else if(sender == trainingMapButton)
                    {
                        trainingMapText.EditValue = mapName;
                    }
                }
            }
        }

        private void StartMap_Load(object sender, EventArgs e)
        {
            ShowFields();
            gameinfo = new GameInfo(launcher).getRoot();
            SetFieldValues();
        }

        private void ShowFields()
        {
            fields = new Dictionary<string, BaseLayoutItem>();

            List<BaseLayoutItem> controls = new List<BaseLayoutItem>();
            controls.AddRange(layoutControl1.Items);

            foreach (BaseLayoutItem item in controls)
                if (item.Tag != null)
                    fields.Add(item.Tag.ToString(), item);

            foreach (BaseLayoutItem item in fields.Values)
                item.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            foreach (string field in launcher.GetCurrentGame().getGameinfoFields())
            {
                if (fields.ContainsKey(field))
                    fields[field].Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void SetFieldValues()
        {
            string modPath = launcher.GetCurrentMod().InstallPath;

            foreach (string field in launcher.GetCurrentGame().getGameinfoFields())
            {
                SourceSDK.KeyValue childKV = gameinfo.findChildByKey(field);
                if (childKV == null)
                    continue;

                if (!fields.ContainsKey(field))
                    continue;

                BaseEdit control = (fields[field] as LayoutControlItem).Control as BaseEdit;

                switch (field)
                {
                    default:
                        if (fields.ContainsKey(field) && (fields[field] as LayoutControlItem).Control is BaseEdit)
                            ((fields[field] as LayoutControlItem).Control as BaseEdit).EditValue = childKV.getValue();
                        break;
                }
            }
        }

        private void GetFieldValues()
        {
            string modPath = launcher.GetCurrentMod().InstallPath;

            foreach (string field in launcher.GetCurrentGame().getGameinfoFields())
            {
                SourceSDK.KeyValue childKV = gameinfo.findChildByKey(field);

                if (!fields.ContainsKey(field))
                    continue;

                BaseEdit control = (fields[field] as LayoutControlItem).Control as BaseEdit;

                switch (field)
                {
                    default:
                        if ((fields[field] as LayoutControlItem).Control is BaseEdit)
                        {
                            BaseEdit baseEdit = (fields[field] as LayoutControlItem).Control as BaseEdit;
                            gameinfo.setValue(field, (baseEdit.EditValue != null ? baseEdit.EditValue.ToString() : ""));
                        }


                        break;
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {

        }
    }
}