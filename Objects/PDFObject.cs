﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Implements basic functionalities for pdf objects.
    /// </summary>
    public abstract class PDFObject
    {
        private bool canResize;
        private RectangleF rectangle;

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFObject" /> class with offset (0,0) and size (0,0).
        /// </summary>
        public PDFObject(bool canResize)
            : this(canResize, RectangleF.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFObject"/> class.
        /// </summary>
        /// <param name="rectangle">The offset and size of this object.</param>
        public PDFObject(bool canResize, RectangleF rectangle)
        {
            this.canResize = canResize;
            this.rectangle = rectangle;
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
        /// Gets a value indicating whether this instance can resize.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can resize; otherwise, <c>false</c>.
        /// </value>
        public bool CanResize
        {
            get { return canResize; }
        }

        /// <summary>
        /// Gets or sets the size of this <see cref="PDFObject"/>.
        /// </summary>
        public SizeF Size
        {
            get { return canResize ? rectangle.Size : getSize(); }
            set
            {
                if (!canResize)
                    throw new InvalidOperationException(typeof(PDFObject).Name + " cannot be resized when CanResize is false.");
                else
                    rectangle.Size = value;
            }
        }
        /// <summary>
        /// Gets or sets the width of this <see cref="PDFObject"/>.
        /// </summary>
        public float Width
        {
            get { return canResize ? rectangle.Width : getSize().Width; }
            set
            {
                if (!canResize)
                    throw new InvalidOperationException(typeof(PDFObject).Name + " cannot be resized when CanResize is false.");
                else
                    rectangle.Width = value;
            }
        }
        /// <summary>
        /// Gets or sets the height of this <see cref="PDFObject"/>.
        /// </summary>
        public float Height
        {
            get { return canResize ? rectangle.Height : getSize().Height; }
            set
            {
                if (!canResize)
                    throw new InvalidOperationException(typeof(PDFObject).Name + " cannot be resized when CanResize is false.");
                else
                    rectangle.Height = value;
            }
        }

        /// <summary>
        /// When implemented in a derived class, gets the size of the <see cref="PDFObject"/>.
        /// This method will only be called when <see cref="CanResize"/> is <c>false</c>.
        /// </summary>
        /// <returns>The size of the <see cref="PDFObject"/>.</returns>
        protected virtual SizeF getSize()
        {
            return rectangle.Size;
        }

        /// <summary>
        /// Collects all pdf objects contained within this object.
        /// </summary>
        /// <param name="collector">An <see cref="ObjectCollector"/> to which all pdf objects are added.</param>
        public abstract void Collect(ObjectCollector collector);
    }
}
