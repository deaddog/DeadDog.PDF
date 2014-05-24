using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw an elipse in a pdf document.
    /// </summary>
    public class Elipse : PDFObject
    {
        private Color fill;
        private Color border;
        private float width;

        private bool hasBorder;
        private bool hasFill = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Elipse"/> class.
        /// </summary>
        /// <param name="width">The width of the elipse.</param>
        /// <param name="height">The height of the elipse.</param>
        public Elipse(float width, float height)
            : this(0, 0, width, height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Elipse"/> class.
        /// </summary>
        /// <param name="size">The size of the elipse.</param>
        public Elipse(SizeF size)
            : this(new RectangleF(PointF.Empty, size))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Elipse"/> class.
        /// </summary>
        /// <param name="offsetX">The x offset for the elipse.</param>
        /// <param name="offsetY">The y offset for the elipse.</param>
        /// <param name="width">The width of the elipse.</param>
        /// <param name="height">The height of the elipse.</param>
        public Elipse(float offsetX, float offsetY, float width, float height)
            : this(RectangleF.FromLTRB(offsetX, offsetY, offsetX + width, offsetY + height))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Elipse"/> class.
        /// </summary>
        /// <param name="rectangle">A rectangle describing the offset and size of the elipse.</param>
        public Elipse(RectangleF rectangle)
            : base(true, rectangle)
        {
            this.hasFill = true;
            this.fill = Color.White;

            this.hasBorder = true;
            this.border = Color.Black;

            this.width = 0.5f;
        }

        /// <summary>
        /// Gets or sets whether the border of this <see cref="Elipse"/> is displayed.
        /// </summary>
        public bool HasBorder
        {
            get { return (hasBorder); }
            set { hasBorder = value; }
        }
        /// <summary>
        /// Gets or sets whether the filling of this <see cref="Elipse"/> is displayed.
        /// </summary>
        public bool HasFill
        {
            get { return hasFill; }
            set { hasFill = value; }
        }
        /// <summary>
        /// Gets or sets the color used for filling this elipse.
        /// </summary>
        public Color FillColor
        {
            get { return fill; }
            set { fill = value; hasFill = true; }
        }
        /// <summary>
        /// Gets or sets the color used for drawing the border of this elipse.
        /// </summary>
        public Color BorderColor
        {
            get { return border; }
            set { border = value; hasBorder = true; }
        }
        /// <summary>
        /// Gets or sets the width (thickness) of the border of this elipse.
        /// </summary>
        public float BorderWidth
        {
            get { return width; }
            set { width = value; }
        }
    }
}
