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
        public FillObject(bool canResize) : this(canResize, RectangleF.Empty)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FillObject"/> class.
        /// </summary>
        /// <param name="rectangle">A rectangle describing the offset and size of the object.</param>
        public FillObject(bool canResize, RectangleF rectangle) : base(canResize, rectangle)
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
