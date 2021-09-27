using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SourceSDK
{
    public class RunPreset
    {
        public string name = "";

        public string engine = "";
        public string game = "";
        public string mod = "";

        public int runMode = RunMode.DEFAULT;
        public string exePath = "";
        public string command;

        public RunPreset()
        {

        }

        public RunPreset(int runMode) : this()
        {
            this.runMode = runMode;
        }

        public string GetArguments(Launcher launcher, Control parent)
        {
            Game game = launcher.GetCurrentGame();
            Mod mod = launcher.GetCurrentMod();
            string arguments = "";

            Point location = parent.PointToScreen(Point.Empty);

            if (runMode == RunMode.DEFAULT && game.engine == Engine.SOURCE)
                runMode = RunMode.WINDOWED;

            if (runMode == RunMode.DEFAULT && game.engine == Engine.GOLDSRC)
                runMode = RunMode.WINDOWED;

            if (runMode == RunMode.DEFAULT && game.engine == Engine.SOURCE2)
                runMode = RunMode.VR;

            switch (runMode)
            {
                case RunMode.FULLSCREEN:
                    {
                        switch (game.engine)
                        {
                            case Engine.SOURCE:
                                arguments = "-game \"" + mod.InstallPath + "\" -fullscreen" +
                                " -x 0" +
                                " -y 0" +
                                " -width " + Screen.PrimaryScreen.Bounds.Width +
                                " -height " + Screen.PrimaryScreen.Bounds.Height +
                                " " + command;
                                break;
                            case Engine.SOURCE2:
                                arguments = "-game " + new DirectoryInfo(mod.InstallPath).Name + " -fullscreen -vr_enable_fake_vr_test" +
                                " -x 0" +
                                " -y 0" +
                                " -width " + Screen.PrimaryScreen.Bounds.Width +
                                " -height " + Screen.PrimaryScreen.Bounds.Height +
                                " " + command;
                                break;
                            case Engine.GOLDSRC:
                                arguments = "-game " + new DirectoryInfo(mod.InstallPath).Name + " -fullscreen" +
                                " -x 0" +
                                " -y 0" +
                                " -width " + Screen.PrimaryScreen.Bounds.Width +
                                " -height " + Screen.PrimaryScreen.Bounds.Height +
                                " " + command;
                                break;
                        }
                    }
                    break;
                case RunMode.WINDOWED:
                    switch (game.engine)
                    {
                        case Engine.SOURCE:
                            arguments = "-game \"" + mod.InstallPath + "\" -windowed -noborder -multirun" +
                            " -x " + location.X +
                            " -y " + location.Y +
                            " -width " + parent.Width +
                            " -height " + parent.Height +
                            " " + command;
                            break;
                        case Engine.SOURCE2:
                            arguments = " -game " + new DirectoryInfo(mod.InstallPath).Name + " -windowed -noborder -vr_enable_fake_vr_test" +
                            " -x " + location.X +
                            " -y " + location.Y +
                            " -width " + parent.Width +
                            " -height " + parent.Height +
                            " " + command;
                            break;
                        case Engine.GOLDSRC:
                            arguments = "-game " + new DirectoryInfo(mod.InstallPath).Name + " -windowed -noborder" +
                            " -x " + location.X +
                            " -y " + location.Y +
                            " -width " + parent.Width +
                            " -height " + parent.Height +
                            " " + command;
                            break;
                    }
                    break;
                case RunMode.VR:
                    switch (game.engine)
                    {
                        case Engine.SOURCE:
                            arguments = "-game \"" + mod.InstallPath + "\" -vr" +
                            " " + command;
                            break;
                        case Engine.SOURCE2:
                            arguments = "-game " + new DirectoryInfo(mod.InstallPath).Name + " -vr" +
                            " " + command;
                            break;
                    }
                    break;
            }
            return arguments;
        }

        public static class CoverageType
        {
            public static int GLOBAL = 0;
            public static int GAME_SPECIFIC = 1;
            public static int MOD_SPECIFIC = 2;
        }
    }
}
