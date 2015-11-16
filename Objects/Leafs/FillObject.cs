using System.Drawing;

namespace DeadDog.PDF
{
    /// <summary>
    /// Provides properties for managing objects with a border/stroke and fill color.
    /// </summary>
    public abstract class FillObject : StrokeObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FillObject"/> class.
        /// </summary>
        public FillObject(bool canResize) : base(canResize)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FillObject"/> class.
        /// </summary>
        /// <param name="rectangle">A rectangle describing the offset and size of the object.</param>
        public FillObject(bool canResize, RectangleF rectangle) : base(canResize, rectangle)
        {
        }
    }
}
