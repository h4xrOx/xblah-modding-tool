using DevExpress.XtraDiagram;
using SourceModdingTool.SourceSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceModdingTool
{
    public class HudItem
    {
        private string fieldName;
        public Point? Location {
            get {
                if (xpos != -1 && ypos != -1)
                    return new Point(xpos, ypos);
                else
                    return null;
                 }
            set {
                if (value != null)
                {
                    xpos = value.Value.X;
                    ypos = value.Value.Y;
                }
                else
                {
                    xpos = -1;
                    ypos = -1;
                }
            }
        }
        private int xpos = -1;
        private int ypos = -1;
        private int wide = -1;
        private int tall = -1;
        public Point? Size
        {
            get
            {
                if (wide != -1 && tall != -1)
                    return new Point(wide, tall);
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    wide = value.Value.X;
                    tall = value.Value.Y;
                }
                else
                {
                    wide = -1;
                    tall = -1;
                }
            }
        }
        public bool? Visible{ get; set; }
        public bool? Enabled{ get; set; }
        public int? PaintBackgroundType { get; set; }
        private int text_xpos = -1;
        private int text_ypos = -1;
        public Point? TextLocation
        {
            get
            {
                if (text_xpos != -1 && text_ypos != -1)
                    return new Point(text_xpos, text_ypos);
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    text_xpos = value.Value.X;
                    text_ypos = value.Value.Y;
                }
                else
                {
                    text_xpos = -1;
                    text_ypos = -1;
                }
            }
        }
        private int digit_xpos = -1;
        private int digit_ypos = -1;
        public Point? DigitLocation
        {
            get
            {
                if (digit_xpos != -1 && digit_ypos != -1)
                    return new Point(digit_xpos, digit_ypos);
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    digit_xpos = value.Value.X;
                    digit_ypos = value.Value.Y;
                } else
                {
                    digit_xpos = -1;
                    digit_ypos = -1;
                }
            }
        }

        public KeyValue keyValue;

        public HudItem(KeyValue keyValue)
        {
            this.keyValue = keyValue;

            fieldName = keyValue.getValue("fieldname");
            int xpos; if (int.TryParse(keyValue.getValue("xpos"), out xpos)) this.xpos = xpos;
            int ypos; if (int.TryParse(keyValue.getValue("ypos"), out ypos)) this.ypos = ypos;
            int wide; if (int.TryParse(keyValue.getValue("wide"), out wide)) this.wide = wide;
            int tall; if (int.TryParse(keyValue.getValue("tall"), out tall)) this.tall = tall;
            int visible; if (int.TryParse(keyValue.getValue("visible"), out visible)) this.Visible = (visible == 1);
            int enabled; if (int.TryParse(keyValue.getValue("enabled"), out enabled)) this.Enabled = (enabled == 1);
            int PaintBackgroundType; if (int.TryParse(keyValue.getValue("paintbackgroundtype"), out PaintBackgroundType)) this.PaintBackgroundType = PaintBackgroundType;
            int text_xpos; if (int.TryParse(keyValue.getValue("text_xpos"), out text_xpos)) this.text_xpos = text_xpos;
            int text_ypos; if (int.TryParse(keyValue.getValue("text_ypos"), out text_ypos)) this.text_ypos = text_ypos;
            int digit_xpos; if (int.TryParse(keyValue.getValue("digit_xpos"), out digit_xpos)) this.digit_xpos = digit_xpos;
            int digit_ypos; if (int.TryParse(keyValue.getValue("digit_ypos"), out digit_ypos)) this.digit_ypos = digit_ypos;
        }

        public KeyValue getKeyValue()
        {
            this.keyValue.clearChildren();

            if (fieldName != null) keyValue.setValue("fieldname", fieldName);
            if (xpos != -1) keyValue.setValue("xpos", xpos.ToString());
            if (ypos != -1) keyValue.setValue("ypos", ypos.ToString());
            if (wide != null) keyValue.setValue("wide", wide.ToString());
            if (tall != null) keyValue.setValue("tall", tall.ToString());
            if (Visible != null) keyValue.setValue("visible", (Visible == true ? 1 : 0).ToString());
            if (Enabled != null) keyValue.setValue("enabled", (Enabled == true ? 1 : 0).ToString());
            if (PaintBackgroundType != null) keyValue.setValue("paintbackgroundtype", PaintBackgroundType.ToString());
            if (text_xpos != -1) keyValue.setValue("text_xpos", text_xpos.ToString());
            if (text_ypos != -1) keyValue.setValue("text_ypos", text_ypos.ToString());
            if (digit_xpos != -1) keyValue.setValue("digit_xpos", digit_xpos.ToString());
            if (digit_ypos != -1) keyValue.setValue("digit_ypos", digit_ypos.ToString());

            return this.keyValue;
        }

    }
}
