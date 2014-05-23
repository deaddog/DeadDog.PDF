using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a box in a pdf document.
    /// </summary>
    public class Box : PDFObjectSizeable, IPDFObject
    {
        private Color fill;
        private Color border;
        private SizeF size;
        private float width = 0.5f;

        private bool hasBorder = true, hasFill = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class with the selected properties.
        /// </summary>
        /// <param name="width">The width of this box.</param>
        /// <param name="height">The height of this box.</param>
        /// <param name="visible">true if both border and fill of this box is displayed. false is not.</param>
        public Box(float width, float height, bool visible)
            : this(new RectangleF(0, 0, width, height), visible)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class with the selected properties.
        /// </summary>
        /// <param name="rectangle">The location and size of this box.</param>
        /// <param name="visible">true if both border and fill of this box is displayed. false is not.</param>
        public Box(RectangleF rectangle, bool visible)
            : this(rectangle, Color.White, Color.Black, 0.5f)
        {
            hasBorder = hasFill = visible;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class with the selected properties.
        /// </summary>
        /// <param name="width">The width of this box.</param>
        /// <param name="height">The height of this box.</param>
        /// <param name="fill">The color used for filling this box.</param>
        /// <param name="border">The color used for drawing the border of this box.</param>
        /// <param name="borderWidth">The width (thickness) of the border of this box.</param>
        public Box(float width, float height, Color fill, Color border, float borderWidth)
            : this(new RectangleF(0, 0, width, height), fill, border, borderWidth)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class with the selected properties.
        /// </summary>
        /// <param name="rectangle">The location and size of this box.</param>
        /// <param name="fill">The color used for filling this box.</param>
        /// <param name="border">The color used for drawing the border of this box.</param>
        /// <param name="borderWidth">The width (thickness) of the border of this box.</param>
        public Box(RectangleF rectangle, Color fill, Color border, float borderWidth)
            : base()
        {
            this.Location = rectangle.Location;
            this.size = rectangle.Size;
            this.fill = fill;
            this.border = border;
            this.width = borderWidth;
        }

        /// <summary>
        /// Gets or sets the size of this <see cref="Box"/> object.
        /// </summary>
        public override SizeF Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// Gets or sets the height of this <see cref="Box"/> object.
        /// </summary>
        public override float Height
        {
            get { return size.Height; }
            set { size.Height = value; }
        }
        /// <summary>
        /// Gets or sets the width of this <see cref="Box"/> object.
        /// </summary>
        public override float Width
        {
            get { return size.Width; }
            set { size.Width = value; }
        }

        /// <summary>
        /// Gets or sets whether the border of this <see cref="Box"/> is displayed.
        /// </summary>
        public bool HasBorder
        {
            get { return (hasBorder); }
            set { hasBorder = value; }
        }
        /// <summary>
        /// Gets or sets whether the filling of this <see cref="Box"/> is displayed.
        /// </summary>
        public bool HasFill
        {
            get { return hasFill; }
            set { hasFill = value; }
        }
        /// <summary>
        /// Gets or sets the color used for filling this box.
        /// </summary>
        public Color FillColor
        {
            get { return fill; }
            set { fill = value; hasFill = true; }
        }
        /// <summary>
        /// Gets or sets the color used for drawing the border of this box.
        /// </summary>
        public Color BorderColor
        {
            get { return border; }
            set { border = value; hasBorder = true; }
        }
        /// <summary>
        /// Gets or sets the width (thickness) of the border of this box.
        /// </summary>
        public float BorderWidth
        {
            get { return width; }
            set { width = value; }
        }
    }
}
