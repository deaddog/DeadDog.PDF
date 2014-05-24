using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Defines properties and methods required by all pdf objects.
    /// </summary>
    public interface IPDFObject
    {
        /// <summary>
        /// Gets or sets the x offset for this object.
        /// </summary>
        float OffsetX
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the y offset for this object.
        /// </summary>
        float OffsetY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the width of this object.
        /// </summary>
        float Width
        {
            get;
        }
        /// <summary>
        /// Gets the height of this object.
        /// </summary>
        float Height
        {
            get;
        }
    }
}
