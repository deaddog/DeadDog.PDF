using System.Drawing;

namespace DeadDog.PDF
{
    /// <summary>
    /// Provides properties for managing objects with a border/stroke and fill color.
    /// </summary>
    public abstract class FillObject : StrokeObject
    {
        private Color fill;
        private bool hasFill;

        /// <summary>
        /// Initializes a new instance of the <see cref="FillObject"/> class.
        /// </summary>
        /// <param name="canResize">if set to <c>true</c> the <see cref="FillObject"/> can be resized using its <see cref="PDFObject.Size"/> property.</param>
        public FillObject(bool canResize) : this(canResize, Vector2D.Zero, Vector2D.Zero)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StrokeObject"/> class.
        /// </summary>
        /// <param name="canResize">if set to <c>true</c> the <see cref="StrokeObject"/> can be resized using its <see cref="PDFObject.Size"/> property.</param>
        /// <param name="offset">The offset of the <see cref="StrokeObject" />.</param>
        /// <param name="size">The size of the <see cref="StrokeObject" />.</param>
        public FillObject(bool canResize, Vector2D offset, Vector2D size) : base(canResize, offset, size)
        {
            this.fill = Color.White;
            this.hasFill = true;
        }

        /// <summary>
        /// Gets or sets whether the filling of this object is displayed.
        /// </summary>
        public bool HasFill
        {
            get { return hasFill; }
            set { hasFill = value; }
        }
        /// <summary>
        /// Gets or sets the color used for filling this object.
        /// </summary>
        public Color FillColor
        {
            get { return fill; }
            set { fill = value; hasFill = true; }
        }
    }
}
