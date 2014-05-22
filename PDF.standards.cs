using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DeadDog.PDF
{
    public partial class PDFDocument
    {
        private static float getP(float cm)
        {
            return (cm * 72f) / 2.54f;
        }
        private static float getC(float p)
        {
            return (p * 2.54f) / 72f;
        }

        private static PointF getP(PointF cm)
        {
            return new PointF(getP(cm.X), getP(cm.Y));
        }
        private static PointF getC(PointF p)
        {
            return new PointF(getC(p.X), getC(p.Y));
        }

        private static SizeF getP(SizeF cm)
        {
            return new SizeF(getP(cm.Width), getP(cm.Height));
        }
        private static SizeF getC(SizeF p)
        {
            return new SizeF(getC(p.Width), getC(p.Height));
        }

        private static RectangleF getP(RectangleF cm)
        {
            return new RectangleF(getP(cm.Location), getP(cm.Size));
        }
        private static RectangleF getC(RectangleF p)
        {
            return new RectangleF(getC(p.Location), getC(p.Size));
        }
    }
}
