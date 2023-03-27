using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NissayaEditor
{
    public class MyToolTip : ToolTip
    {
        Font font;
        int ttpWidth, ttpHeight;
#pragma warning disable CA1416
        public MyToolTip(Font font, int ttpWidth = 6, int ttpHeight = 4)
        {
            font = new Font("Myanmar Text", 10.0f);
            this.OwnerDraw = true;
            this.Draw += new DrawToolTipEventHandler(toolTip1_Draw);
            this.Popup += new PopupEventHandler(toolTip1_Popup);
            this.BackColor = Color.Yellow; this.ForeColor = Color.Black;
            this.ttpHeight = ttpHeight; this.ttpWidth = ttpWidth;
        }
        void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            font = new Font("Myanmar Text", 10.0f);
            ToolTip ttp = (ToolTip)sender;
            ttp.BackColor = System.Drawing.Color.Yellow;
            e.DrawBackground();
            e.DrawBorder();
            e.Graphics.DrawString(e.ToolTipText, font, Brushes.Black, new PointF(6, 4));
        }
        void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            ToolTip ttp = (ToolTip)sender;
            string s = ttp.GetToolTip(e.AssociatedControl);
            // on popip set the size of tool tip
            font = new Font("Myanmar Text", 10.0f);
            e.ToolTipSize = TextRenderer.MeasureText(s, font);
        }
#pragma warning restore CA1416
    }
}
