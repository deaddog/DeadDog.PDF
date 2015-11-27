using System.Drawing;

namespace DeadDog.PDF
{
    /// <summary>
    /// Provides properties for managing objects with a border/stroke.
    /// </summary>
    public abstract class StrokeObject : LeafObject
    {
        private Color? border;
        private Vector1D width;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrokeObject"/> class.
        /// </summary>
        /// <param name="offset">The offset of the <see cref="StrokeObject" />.</param>
        /// <param name="size">The size of the <see cref="StrokeObject" />.</param>
        public StrokeObject(Vector2D offset, Vector2D size)
            : base(offset, size)
        {
            this.border = Color.Black;
            this.width = new Vector1D(1, UnitsOfMeasure.Points);
        }

        /// <summary>
        /// Gets or sets the color used for drawing the border of this object.
        /// <c>null</c> indicates that no border should be drawn when rendering the object.
        /// </summary>
        public Color? BorderColor
        {
            get { return border; }
            set { border = value; }
        }
        /// <summary>
        /// Gets or sets the width (thickness) of the border of this object.
        /// </summary>
        public Vector1D BorderWidth
        {
            get { return width; }
            set { width = value; }
        }
    }
}
