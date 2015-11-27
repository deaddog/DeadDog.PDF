using iTextSharp.text.pdf;
using System;

namespace DeadDog.PDF
{
    public class Arc : FillObject
    {
        private float startangle, extent;

        public Arc()
            : this(Vector2D.Zero)
        {
        }
        public Arc(Vector2D size)
            : base(true, Vector2D.Zero, size)
        {
            this.startangle = 0;
            this.extent = 45;
        }

        public float StartAngle
        {
            get { return startangle; }
            set { startangle = value; }
        }
        public float Extent
        {
            get { return extent; }
            set { extent = value; }
        }

        protected internal override void Render(PdfContentByte cb, Vector2D offset)
        {
            var p1 = offset;
            var p2 = offset + Size;

            cb.Arc(
                (float)p1.X.Value(UnitsOfMeasure.Points),
                (float)p1.Y.Value(UnitsOfMeasure.Points),
                (float)p2.X.Value(UnitsOfMeasure.Points),
                (float)p2.Y.Value(UnitsOfMeasure.Points),
                startangle,
                extent);
        }
    }
}
