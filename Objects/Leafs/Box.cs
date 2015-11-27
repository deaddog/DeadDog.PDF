using iTextSharp.text.pdf;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a box in a pdf document.
    /// </summary>
    public class Box : FillObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        /// <param name="size">The size of the box.</param>
        public Box(Vector2D size)
            : base(Vector2D.Zero, size)
        {
        }

        protected internal override void Render(PdfContentByte cb, Vector2D offset)
        {
            cb.Rectangle(
                (float)offset.X.Value(UnitsOfMeasure.Points),
                (float)offset.Y.Value(UnitsOfMeasure.Points),
                (float)Size.X.Value(UnitsOfMeasure.Points),
                (float)Size.Y.Value(UnitsOfMeasure.Points));
        }
    }
}
