using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace source_modding_tool.SourceSDK
{
    public class RunPreset
    {
        public string name = "";

        public string engine = "";
        public string game = "";
        public string mod = "";

        public int runMode = SourceSDK.RunMode.DEFAULT;
        public string exePath = "";
        public string command;

        public string GetArguments(Launcher launcher, Control parent)
        {
            Game game = launcher.GetCurrentGame();
            Mod mod = launcher.GetCurrentMod();
            string arguments = "";
            switch(runMode)
            {
                case SourceSDK.RunMode.DEFAULT:
                    {
                        switch (game.engine)
                        {
                            case Engine.SOURCE:
                                arguments = "-game \"" + mod.installPath + "\" -windowed -noborder" +
                                " -width " + parent.Width +
                                " -height " + parent.Height +
                                " -multirun" +
                                " " + command;
                                break;
                            case Engine.SOURCE2:
                                arguments = " -game " + new DirectoryInfo(mod.installPath).Name + " -windowed -noborder -vr_enable_fake_vr_test" +
                                " -width " + parent.Width +
                                " -height " + parent.Height +
                                " " + command;
                                break;
                        }
                    }
                    break;
                case SourceSDK.RunMode.FULLSCREEN:
                    {
                        switch (game.engine)
                        {
                            case Engine.SOURCE:
                                arguments = "-game \"" + mod.installPath + "\" -fullscreen" +
                                " -width " + Screen.PrimaryScreen.Bounds.Width +
                                " -height " + Screen.PrimaryScreen.Bounds.Height +
                                " " + command;
                                break;
                            case Engine.SOURCE2:
                                arguments = "-game " + new DirectoryInfo(mod.installPath).Name + " -fullscreen -vr_enable_fake_vr_test" +
                                " -width " + Screen.PrimaryScreen.Bounds.Width +
                                " -height " + Screen.PrimaryScreen.Bounds.Height +
                                " " + command;
                                break;
                        }
                    }
                    break;
                case SourceSDK.RunMode.WINDOWED:
                    switch(game.engine)
                    {
                        case Engine.SOURCE:
                            arguments = "-game \"" + mod.installPath + "\" -windowed -noborder -multirun" +
                            " -width " + parent.Width +
                            " -height " + parent.Height +
                            " " + command;
                            break;
                        case Engine.SOURCE2:
                            arguments = " -game " + new DirectoryInfo(mod.installPath).Name + " -windowed -noborder -vr_enable_fake_vr_test" +
                            " -width " + parent.Width +
                            " -height " + parent.Height +
                            " " + command;
                            break;
                    }
                    break;
                case SourceSDK.RunMode.VR:
                    switch(game.engine)
                    {
                        case Engine.SOURCE:
                            arguments = "-game \"" + mod.installPath + "\" -vr" +
                            " " + command;
                            break;
                        case Engine.SOURCE2:
                            arguments = "-game " + new DirectoryInfo(mod.installPath).Name + " -vr" +
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
