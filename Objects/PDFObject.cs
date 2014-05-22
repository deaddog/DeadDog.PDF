using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Implements basic functionalities for pdf objects.
    /// </summary>
    public abstract class PDFObject : IPDFObject
    {
        private LocationHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFObject"/> class.
        /// </summary>
        public PDFObject()
        {
            this.handler = new LocationHandler(this);
        }

        #region IPDFObject Members

        /// <summary>
        /// Gets a <see cref="LocationHandler"/> for this object.
        /// </summary>
        public LocationHandler Handler
        {
            get { return handler; }
        }

        /// <summary>
        /// Gets or sets the coordinates of this <see cref="PDFObject" />.
        /// </summary>
        public System.Drawing.PointF Location
        {
            get { return handler.Location; }
            set { handler.Location = value; }
        }
        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="PDFObject" />.
        /// </summary>
        public float X
        {
            get { return handler.X; }
            set { handler.X = value; }
        }
        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="PDFObject" />.
        /// </summary>
        public float Y
        {
            get { return handler.Y; }
            set { handler.Y = value; }
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

        #endregion
    }
}
