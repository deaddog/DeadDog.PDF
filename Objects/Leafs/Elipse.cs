using System.Drawing;

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
        /// <param name="width">The width of the elipse.</param>
        /// <param name="height">The height of the elipse.</param>
        public Elipse(float width, float height)
            : this(0, 0, width, height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Elipse"/> class.
        /// </summary>
        /// <param name="size">The size of the elipse.</param>
        public Elipse(SizeF size)
            : this(new RectangleF(PointF.Empty, size))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Elipse"/> class.
        /// </summary>
        /// <param name="offsetX">The x offset for the elipse.</param>
        /// <param name="offsetY">The y offset for the elipse.</param>
        /// <param name="width">The width of the elipse.</param>
        /// <param name="height">The height of the elipse.</param>
        public Elipse(float offsetX, float offsetY, float width, float height)
            : this(RectangleF.FromLTRB(offsetX, offsetY, offsetX + width, offsetY + height))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Elipse"/> class.
        /// </summary>
        /// <param name="rectangle">A rectangle describing the offset and size of the elipse.</param>
        public Elipse(RectangleF rectangle)
            : base(true, rectangle)
        {
        }
    }
}
