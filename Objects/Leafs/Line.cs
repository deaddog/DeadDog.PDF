using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a line in a pdf document.
    /// </summary>
    public class Line : PDFObjectSizeable, IPDFObject
    {
        private Color color;
        private SizeF size;
        private float width = 0.5f;

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class with the selected properties.
        /// </summary>
        /// <param name="width">The width of this line (x2-x1).</param>
        /// <param name="height">The height of this line (y2-y1).</param>
        /// <param name="color">The color of this line.</param>
        public Line(float width, float height, Color color)
            : this(new RectangleF(0, 0, width, height), color, 0.5f)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class with the selected properties.
        /// </summary>
        /// <param name="rectangle">A rectangle describing the location and size of this line.</param>
        /// <param name="color">The color of this line.</param>
        public Line(RectangleF rectangle, Color color)
            : this(rectangle, color, 0.5f)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class with the selected properties.
        /// </summary>
        /// <param name="width">The width of this line (x2-x1).</param>
        /// <param name="height">The height of this line (y2-y1).</param>
        /// <param name="color">The color of this line.</param>
        /// <param name="borderWidth">The width (thickness) of this line.</param>
        public Line(float width, float height, Color color, float borderWidth)
            : this(new RectangleF(0, 0, width, height), color, borderWidth)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class with the selected properties.
        /// </summary>
        /// <param name="rectangle">A rectangle describing the location and size of this line.</param>
        /// <param name="color">The color of this line.</param>
        /// <param name="borderWidth">The width (thickness) of this line.</param>
        public Line(RectangleF rectangle, Color color, float borderWidth)
            : base()
        {
            this.Location = rectangle.Location;
            this.size = rectangle.Size;
            this.color = color;
            this.width = borderWidth;
        }

        /// <summary>
        /// Gets or sets the size of this <see cref="Line"/> object.
        /// </summary>
        public override SizeF Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// Gets or sets the height (y2-y1) of this <see cref="Line"/> object.
        /// </summary>
        public override float Height
        {
            get { return size.Height; }
            set { size.Height = value; }
        }
        /// <summary>
        /// Gets or sets the width (x2-x1) of this <see cref="Line"/> object.
        /// </summary>
        public override float Width
        {
            get { return size.Width; }
            set { size.Width = value; }
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
