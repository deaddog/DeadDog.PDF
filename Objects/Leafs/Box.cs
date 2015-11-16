using System.Drawing;

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
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        public Box(float width, float height)
            : this(0, 0, width, height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        /// <param name="size">The size of the box.</param>
        public Box(SizeF size)
            : this(new RectangleF(PointF.Empty, size))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        /// <param name="offsetX">The x offset for the box.</param>
        /// <param name="offsetY">The y offset for the box.</param>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        public Box(float offsetX, float offsetY, float width, float height)
            : this(RectangleF.FromLTRB(offsetX, offsetY, offsetX + width, offsetY + height))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        /// <param name="rectangle">A rectangle describing the offset and size of the box.</param>
        public Box(RectangleF rectangle)
            : base(true, rectangle)
        {
        }
    }
}
