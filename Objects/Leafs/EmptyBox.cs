using System.Drawing;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information on the size of a box. The box is not drawn but only used as a space buffer.
    /// </summary>
    public sealed class EmptyBox : PDFObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyBox"/> class.
        /// </summary>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        public EmptyBox(float width, float height)
            : this(0, 0, width, height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyBox"/> class.
        /// </summary>
        /// <param name="size">The size of the box.</param>
        public EmptyBox(SizeF size)
            : this(new RectangleF(PointF.Empty, size))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyBox"/> class.
        /// </summary>
        /// <param name="offsetX">The x offset for the box.</param>
        /// <param name="offsetY">The y offset for the box.</param>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        public EmptyBox(float offsetX, float offsetY, float width, float height)
            : this(RectangleF.FromLTRB(offsetX, offsetY, offsetX + width, offsetY + height))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyBox"/> class.
        /// </summary>
        /// <param name="rectangle">A rectangle describing the offset and size of the box.</param>
        public EmptyBox(RectangleF rectangle)
            : base(true, rectangle)
        {
        }
    }
}
