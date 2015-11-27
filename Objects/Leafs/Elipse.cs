using iTextSharp.text.pdf;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw an elipse in a pdf document.
    /// </summary>
    public class Elipse : FillObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Elipse"/> class.
        /// </summary>
        /// <param name="size">The size of the elipse.</param>
        public Elipse(Vector2D size)
            : base(Vector2D.Zero, size)
        {
        }

        protected internal override void Render(PdfContentByte cb, Vector2D offset)
        {
            float x1 = (float)offset.X.Value(UnitsOfMeasure.Points);
            float y1 = (float)offset.Y.Value(UnitsOfMeasure.Points);
            float x2 = x1 + (float)Size.X.Value(UnitsOfMeasure.Points);
            float y2 = y1 + (float)Size.Y.Value(UnitsOfMeasure.Points);

            cb.Ellipse(x1, y1, x2, y2);
        }
    }
}
