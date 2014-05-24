using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a line in a pdf document.
    /// </summary>
    public class Line : PDFObject
    {
        private Color color;
        private float width;

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="width">The width of the line.</param>
        /// <param name="height">The height of the line.</param>
        public Line(float width, float height)
            : this(0, 0, width, height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="size">The size of the line.</param>
        public Line(SizeF size)
            : this(new RectangleF(PointF.Empty, size))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="offsetX">The x offset for the line.</param>
        /// <param name="offsetY">The y offset for the line.</param>
        /// <param name="width">The width of the line.</param>
        /// <param name="height">The height of the line.</param>
        public Line(float offsetX, float offsetY, float width, float height)
            : this(RectangleF.FromLTRB(offsetX, offsetY, offsetX + width, offsetY + height))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="rectangle">A rectangle describing the offset and size of the line.</param>
        public Line(RectangleF rectangle)
            : base(true, rectangle)
        {
            this.color = Color.Black;

            this.width = 0.5f;
        }

        /// <summary>
        /// Gets or sets the color of this <see cref="Line"/> object.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        /// <summary>
        /// Gets or sets the width (thickness) of this <see cref="Line"/> object.
        /// </summary>
        public float LineWidth
        {
            get { return width; }
            set { width = value; }
        }
    }
}
