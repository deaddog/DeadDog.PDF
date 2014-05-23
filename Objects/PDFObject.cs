using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Implements basic functionalities for pdf objects.
    /// </summary>
    public abstract class PDFObject : IPDFObject
    {
        private PointF offset;

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFObject" /> class.
        /// </summary>
        /// <param name="offsetX">The x offset for this object.</param>
        /// <param name="offsetY">The y offset for this object.</param>
        public PDFObject(float offsetX, float offsetY)
            : this(new PointF(offsetX, offsetY))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFObject"/> class.
        /// </summary>
        /// <param name="offset">The offset for this object.</param>
        public PDFObject(PointF offset)
        {
            this.offset = offset;
        }

        /// <summary>
        /// Gets or sets the offset for this <see cref="PDFObject"/>.
        /// </summary>
        public PointF Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        /// <summary>
        /// Gets or sets the x offset for this <see cref="PDFObject"/>.
        /// </summary>
        public float OffsetX
        {
            get { return offset.X; }
            set { offset.X = value; }
        }
        /// <summary>
        /// Gets or sets the y offset for this <see cref="PDFObject"/>.
        /// </summary>
        public float OffsetY
        {
            get { return offset.Y; }
            set { offset.Y = value; }
        }

        /// <summary>
        /// Gets the width of this <see cref="PDFObject"/>.
        /// </summary>
        public abstract float Width
        {
            get;
        }
        /// <summary>
        /// Gets the height of this <see cref="PDFObject"/>.
        /// </summary>
        public abstract float Height
        {
            get;
        }

        /// <summary>
        /// Collects all pdf objects contained within this object.
        /// </summary>
        /// <param name="collector">An <see cref="ObjectCollector"/> to which all pdf objects are added.</param>
        public abstract void Collect(ObjectCollector collector);
    }
}
