using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Defines properties and methods required by all pdf groups. Groups are collections of <see cref="IPDFObject"/> objects.
    /// </summary>
    public interface IPDFGroup : IPDFObject
    {
        /// <summary>
        /// Returns the location of any element within this pdf group.
        /// </summary>
        /// <param name="obj">The element whichs location is returned.</param>
        /// <returns>The location of obj.</returns>
        PointF GetLocation(IPDFObject obj);
    }
    /// <summary>
    /// Defines properties and methods required by all pdf groups. Groups are collections of <see cref="IPDFObject"/> objects.
    /// </summary>
    /// <typeparam name="T">The type of elements in the pdf group.</typeparam>
    public interface IPDFGroup<T> : IPDFGroup, IPDFObject where T : IPDFObject
    {
        /// <summary>
        /// Returns the location of any element within this pdf group.
        /// </summary>
        /// <param name="obj">The element whichs location is returned.</param>
        /// <returns>The location of obj.</returns>
        PointF GetLocation(T obj);
    }
}
