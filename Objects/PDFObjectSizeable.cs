using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Implements basic functionalities for sizeable pdf objects.
    /// </summary>
    public abstract class PDFObjectSizeable : IPDFObject
    {
        private LocationHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFObjectSizeable"/> class.
        /// </summary>
        public PDFObjectSizeable()
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
        /// Gets or sets the coordinates of this <see cref="PDFObjectSizeable" />.
        /// </summary>
        public System.Drawing.PointF Location
        {
            get { return handler.Location; }
            set { handler.Location = value; }
        }
        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="PDFObjectSizeable" />.
        /// </summary>
        public float X
        {
            get { return handler.X; }
            set { handler.X = value; }
        }
        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="PDFObjectSizeable" />.
        /// </summary>
        public float Y
        {
            get { return handler.Y; }
            set { handler.Y = value; }
        }

        /// <summary>
        /// Gets or sets the size of this <see cref="PDFObjectSizeable"/>.
        /// </summary>
        public abstract System.Drawing.SizeF Size
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the width of this <see cref="PDFObjectSizeable"/>.
        /// </summary>
        public abstract float Width
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the height of this <see cref="PDFObjectSizeable"/>.
        /// </summary>
        public abstract float Height
        {
            get;
            set;
        }

        /// <summary>
        /// Collects all pdf objects contained within this object.
        /// </summary>
        /// <param name="collector">An <see cref="ObjectCollector"/> to which all pdf objects are added.</param>
        public abstract void Collect(ObjectCollector collector);

        #endregion
    }
}
