using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DeadDog.PDF
{
    /// <summary>
    /// Handles location of objects via internal communication with <see cref="PDFList{T}" />
    /// </summary>
    public sealed class LocationHandler
    {
        private PointF location = new PointF(0, 0);
        internal IPDFGroup parent = null;
        internal IPDFObject owner = null;
        //internal DeadDog.PDF.Pages.IPDFpage page = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationHandler" /> class.
        /// </summary>
        /// <param name="owner">The object which location will be controlled by this instance.</param>
        public LocationHandler(IPDFObject owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="LocationHandler" />.
        /// </summary>
        public float X
        {
            get
            {
                float f = 0;
                if (owner is IOffsetPosition)
                    f = ((IOffsetPosition)owner).OffsetX;
                if (parent != null)
                    return parent.GetLocation(owner).X + f;
                else
                    return location.X - f;
            }
            set
            {
                if (parent == null)
                {
                    if (owner is IOffsetPosition)
                        location.X = value + ((IOffsetPosition)owner).OffsetX;
                    else
                        location.X = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="LocationHandler" />.
        /// </summary>
        public float Y
        {
            get
            {
                float f = 0;
                if (owner is IOffsetPosition)
                    f = ((IOffsetPosition)owner).OffsetY;
                if (parent != null)
                    return parent.GetLocation(owner).Y + f;
                else
                    return location.Y - f;
            }
            set
            {
                if (parent == null)
                {
                    if (owner is IOffsetPosition)
                        location.Y = value + ((IOffsetPosition)owner).OffsetY;
                    else
                        location.Y = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the coordinates of this <see cref="LocationHandler" />.
        /// </summary>
        public PointF Location
        {
            get
            {
                SizeF p = new SizeF(0, 0);
                if (owner is IOffsetPosition)
                {
                    IOffsetPosition ow = ((IOffsetPosition)owner);
                    p = new SizeF(ow.OffsetX, ow.OffsetY);
                }
                if (parent != null)
                    return parent.GetLocation(owner) + p;
                else
                    return location - p;
            }
            set
            {
                if (parent == null)
                    location = value;
            }
        }
        
        /*public int Page
        {
            get { return 0; }
        }
        public int PageCount
        {
            get { return 0; }
        }*/
    }
}
