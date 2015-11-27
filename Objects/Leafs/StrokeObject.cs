using System.Drawing;

namespace DeadDog.PDF
{
    /// <summary>
    /// Provides properties for managing objects with a border/stroke.
    /// </summary>
    public abstract class StrokeObject : LeafObject
    {
        private Color border;
        private Vector1D width;

        private bool hasBorder;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrokeObject"/> class.
        /// </summary>
        /// <param name="canResize">if set to <c>true</c> the <see cref="StrokeObject"/> can be resized using its <see cref="PDFObject.Size"/> property.</param>
        public StrokeObject(bool canResize) : this(canResize, Vector2D.Zero, Vector2D.Zero)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StrokeObject"/> class.
        /// </summary>
        /// <param name="canResize">if set to <c>true</c> the <see cref="StrokeObject"/> can be resized using its <see cref="PDFObject.Size"/> property.</param>
        /// <param name="offset">The offset of the <see cref="StrokeObject" />.</param>
        /// <param name="size">The size of the <see cref="StrokeObject" />.</param>
        public StrokeObject(bool canResize, Vector2D offset, Vector2D size) : base(canResize, offset, size)
        {
            this.border = Color.Black;
            this.width = new Vector1D(1, UnitsOfMeasure.Points);

            this.hasBorder = true;
        }

        /// <summary>
        /// Gets or sets the color used for drawing the border of this object.
        /// </summary>
        public Color BorderColor
        {
            get { return border; }
            set { border = value; hasBorder = true; }
        }
        /// <summary>
        /// Gets or sets the width (thickness) of the border of this object.
        /// </summary>
        public Vector1D BorderWidth
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Gets or sets whether the border of this object is displayed.
        /// </summary>
        public bool HasBorder
        {
            get { return hasBorder; }
            set { hasBorder = value; }
        }
    }
}
