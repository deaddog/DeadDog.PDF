using iTextSharp.text.pdf;
using System;
using System.Drawing;

namespace DeadDog.PDF
{
    public class ContentWriter : IDisposable
    {
        private PdfContentByte cb;
        private bool closeShape = false;

        internal ContentWriter(PdfContentByte cb)
        {
            if (cb == null)
                throw new ArgumentNullException(nameof(cb));

            this.cb = cb;
        }

        public bool CloseShape
        {
            get { return closeShape; }
            set { closeShape = value; }
        }

        public void Arc(Vector2D v1, Vector2D v2, float startAngle, float extentAngle)
        {
            Arc(v1.X, v1.Y, v2.X, v2.Y, startAngle, extentAngle);
        }
        public void Arc(Vector1D x1, Vector1D y1, Vector1D x2, Vector1D y2, float startAngle, float extentAngle)
        {
            cb.Arc(
                (float)x1.Value(UnitsOfMeasure.Points),
                (float)y1.Value(UnitsOfMeasure.Points),
                (float)x2.Value(UnitsOfMeasure.Points),
                (float)y2.Value(UnitsOfMeasure.Points), startAngle, extentAngle);
        }
        public void Arc(double x1, double y1, double x2, double y2, UnitsOfMeasure unit, float startAngle, float extentAngle)
        {
            Arc(
               new Vector1D(x1, unit),
               new Vector1D(y1, unit),
               new Vector1D(x2, unit),
               new Vector1D(y2, unit), startAngle, extentAngle);
        }

        public void CurveFromTo(Vector2D v1, Vector2D v3)
        {
            CurveFromTo(v1.X, v1.Y, v3.X, v3.Y);
        }
        public void CurveFromTo(Vector1D x1, Vector1D y1, Vector1D x3, Vector1D y3)
        {
            cb.CurveFromTo(
                (float)x1.Value(UnitsOfMeasure.Points),
                (float)y1.Value(UnitsOfMeasure.Points),
                (float)x3.Value(UnitsOfMeasure.Points),
                (float)y3.Value(UnitsOfMeasure.Points));
        }
        public void CurveFromTo(double x1, double y1, double x3, double y3, UnitsOfMeasure unit)
        {
            CurveFromTo(
               new Vector1D(x1, unit),
               new Vector1D(y1, unit),
               new Vector1D(x3, unit),
               new Vector1D(y3, unit));
        }

        public void CurveTo(Vector2D v2, Vector2D v3)
        {
            CurveTo(v2.X, v2.Y, v3.X, v3.Y);
        }
        public void CurveTo(Vector1D x2, Vector1D y2, Vector1D x3, Vector1D y3)
        {
            cb.CurveTo(
                (float)x2.Value(UnitsOfMeasure.Points),
                (float)y2.Value(UnitsOfMeasure.Points),
                (float)x3.Value(UnitsOfMeasure.Points),
                (float)y3.Value(UnitsOfMeasure.Points));
        }
        public void CurveTo(double x2, double y2, double x3, double y3, UnitsOfMeasure unit)
        {
            CurveTo(
               new Vector1D(x2, unit),
               new Vector1D(y2, unit),
               new Vector1D(x3, unit),
               new Vector1D(y3, unit));
        }

        public void CurveTo(Vector2D v1, Vector2D v2, Vector2D v3)
        {
            CurveTo(v1.X, v1.Y, v2.X, v2.Y, v3.X, v3.Y);
        }
        public void CurveTo(Vector1D x1, Vector1D y1, Vector1D x2, Vector1D y2, Vector1D x3, Vector1D y3)
        {
            cb.CurveTo(
                (float)x1.Value(UnitsOfMeasure.Points),
                (float)y1.Value(UnitsOfMeasure.Points),
                (float)x2.Value(UnitsOfMeasure.Points),
                (float)y2.Value(UnitsOfMeasure.Points),
                (float)x3.Value(UnitsOfMeasure.Points),
                (float)y3.Value(UnitsOfMeasure.Points));
        }
        public void CurveTo(double x1, double y1, double x2, double y2, double x3, double y3, UnitsOfMeasure unit)
        {
            CurveTo(
               new Vector1D(x1, unit),
               new Vector1D(y1, unit),
               new Vector1D(x2, unit),
               new Vector1D(y2, unit),
               new Vector1D(x3, unit),
               new Vector1D(y3, unit));
        }

        public void Ellipse(Vector4D rectangle)
        {
            Ellipse(rectangle.Offset, rectangle.Size);
        }
        public void Ellipse(Vector2D position, Vector2D size)
        {
            Ellipse(position.X, position.Y, size.X, size.Y);
        }
        public void Ellipse(Vector1D x, Vector1D y, Vector1D width, Vector1D height)
        {
            cb.Ellipse(
                (float)x.Value(UnitsOfMeasure.Points),
                (float)y.Value(UnitsOfMeasure.Points),
                (float)(x + width).Value(UnitsOfMeasure.Points),
                (float)(y + height).Value(UnitsOfMeasure.Points));
        }
        public void Ellipse(double x, double y, double width, double height, UnitsOfMeasure unit)
        {
            Ellipse(
               new Vector1D(x, unit),
               new Vector1D(y, unit),
               new Vector1D(width, unit),
               new Vector1D(height, unit));
        }

        public void LineTo(Vector2D v)
        {
            LineTo(v.X, v.Y);
        }
        public void LineTo(Vector1D x, Vector1D y)
        {
            cb.LineTo(
                (float)x.Value(UnitsOfMeasure.Points),
                (float)y.Value(UnitsOfMeasure.Points));
        }
        public void LineTo(double x, double y, UnitsOfMeasure unit)
        {
            LineTo(
               new Vector1D(x, unit),
               new Vector1D(y, unit));
        }

        public void MoveTo(Vector2D v)
        {
            MoveTo(v.X, v.Y);
        }
        public void MoveTo(Vector1D x, Vector1D y)
        {
            cb.MoveTo(
                (float)x.Value(UnitsOfMeasure.Points),
                (float)y.Value(UnitsOfMeasure.Points));
        }
        public void MoveTo(double x, double y, UnitsOfMeasure unit)
        {
            MoveTo(
               new Vector1D(x, unit),
               new Vector1D(y, unit));
        }

        public void Rectangle(Vector4D rectangle)
        {
            Rectangle(rectangle.Offset, rectangle.Size);
        }
        public void Rectangle(Vector2D position, Vector2D size)
        {
            Rectangle(position.X, position.Y, size.X, size.Y);
        }
        public void Rectangle(Vector1D x, Vector1D y, Vector1D width, Vector1D height)
        {
            cb.Rectangle(
                (float)x.Value(UnitsOfMeasure.Points),
                (float)y.Value(UnitsOfMeasure.Points),
                (float)width.Value(UnitsOfMeasure.Points),
                (float)height.Value(UnitsOfMeasure.Points));
        }
        public void Rectangle(double x, double y, double width, double height, UnitsOfMeasure unit)
        {
            Rectangle(
               new Vector1D(x, unit),
               new Vector1D(y, unit),
               new Vector1D(width, unit),
               new Vector1D(height, unit));
        }

        public void Mark(Vector2D v)
        {
            Mark(v, Color.Black);
        }
        public void Mark(Vector2D v, Color color)
        {
            Mark(v, new Vector1D(2, UnitsOfMeasure.Points), color);
        }
        public void Mark(Vector2D v, Vector1D radius)
        {
            Mark(v, radius, Color.Black);
        }
        public void Mark(Vector2D v, Vector1D radius, Color color)
        {
            cb.SetColorStroke(new iTextSharp.text.Color(color));

            MoveTo(v.X - radius, v.Y);
            LineTo(v.X + radius, v.Y);
            cb.Stroke();
            MoveTo(v.X, v.Y - radius);
            LineTo(v.X, v.Y + radius);
            cb.Stroke();
        }

        void IDisposable.Dispose()
        {
            cb = null;
        }
    }
}
