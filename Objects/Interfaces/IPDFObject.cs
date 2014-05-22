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
        /// Gets a <see cref="LocationHandler"/> for this object.
        /// </summary>
        LocationHandler Handler
        {
            get;
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

        /// <summary>
        /// Collects all pdf objects contained within this object.
        /// </summary>
        /// <param name="collector">An <see cref="ObjectCollector"/> to which all pdf objects are added.</param>
        void Collect(ObjectCollector collector);
    }
}
