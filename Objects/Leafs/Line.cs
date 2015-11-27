using iTextSharp.text.pdf;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a line in a pdf document.
    /// </summary>
    public class Line : StrokeObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="size">The size of the line.</param>
        public Line(Vector2D size)
            : base(Vector2D.Zero, size)
        {
        }

        protected internal override void Render(PdfContentByte cb, Vector2D offset)
        {
            var p2 = offset + Size;

            cb.MoveTo(
                (float)offset.X.Value(UnitsOfMeasure.Points),
                (float)p2.Y.Value(UnitsOfMeasure.Points));
            cb.LineTo(
                (float)p2.X.Value(UnitsOfMeasure.Points),
                (float)offset.Y.Value(UnitsOfMeasure.Points));
        }
    }
}
