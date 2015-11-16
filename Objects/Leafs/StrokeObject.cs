using System.Drawing;

namespace DeadDog.PDF
{
    /// <summary>
    /// Provides properties for managing objects with a border/stroke.
    /// </summary>
    public abstract class StrokeObject : PDFObject
    {
        private Color border;
        private float width;

        private bool hasBorder;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrokeObject"/> class.
        /// </summary>
        public StrokeObject(bool canResize) : this(canResize, Rectangle.Empty)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="StrokeObject"/> class.
        /// </summary>
        /// <param name="rectangle">A rectangle describing the offset and size of the object.</param>
        public StrokeObject(bool canResize, RectangleF rectangle) : base(canResize, rectangle)
        {
            this.border = Color.Black;
            this.width = 0.5f;

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
        public float BorderWidth
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
