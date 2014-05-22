using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information on the size of a box. The box is not drawn but only used as a space buffer.
    /// </summary>
    public sealed class EmptyBox : PDFObjectSizeable, IPDFObject
    {
        private SizeF size;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyBox"/> class with the selected properties.
        /// </summary>
        /// <param name="width">The width of this box.</param>
        /// <param name="height">The height of this box.</param>
        public EmptyBox(float width, float height)
            : this(new RectangleF(0, 0, width, height))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyBox"/> class with the selected properties.
        /// </summary>
        /// <param name="rectangle">The location and size of this box.</param>
        public EmptyBox(RectangleF rectangle)
        {
            this.Location = rectangle.Location;
            this.size = rectangle.Size;
        }

        /// <summary>
        /// Gets or sets the size of this <see cref="EmptyBox"/> object.
        /// </summary>
        public override SizeF Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// Gets or sets the height of this <see cref="EmptyBox"/> object.
        /// </summary>
        public override float Height
        {
            get { return size.Height; }
            set { size.Height = value; }
        }
        /// <summary>
        /// Gets or sets the width of this <see cref="EmptyBox"/> object.
        /// </summary>
        public override float Width
        {
            get { return size.Width; }
            set { size.Width = value; }
        }

        /// <summary>
        /// Adds nothing to an object collector since an <see cref="EmptyBox"/> is not visible.
        /// </summary>
        /// <param name="collector">An <see cref="ObjectCollector"/> to which nothing is added.</param>
        public override void Collect(ObjectCollector collector)
        {
            //Add nothing.
        }
    }
}
