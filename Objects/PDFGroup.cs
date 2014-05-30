using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Implements basic functionalities for pdf groups.
    /// Groups are collections of <see cref="PDFObject"/> objects
    /// In a <see cref="PDFGroup"/> objects are collected in the protected list property.
    /// </summary>
    public abstract class PDFGroup : PDFGroup<PDFObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFGroup"/> class.
        /// </summary>
        /// <param name="offsetX">The x offset for this object.</param>
        /// <param name="offsetY">The y offset for this object.</param>
        public PDFGroup(bool canResize)
            : base(canResize)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFGroup"/> class.
        /// </summary>
        /// <param name="offset">The offset for this object.</param>
        public PDFGroup(bool canResize, RectangleF rectangle)
            : base(canResize, rectangle)
        {
        }
    }

    /// <summary>
    /// Implements basic functionalities for pdf groups using generic types.
    /// Groups are collections of <see cref="PDFObject"/> objects
    /// In a <see cref="PDFGroup{T}"/> objects are collected in the protected list property.
    /// </summary>
    /// <typeparam name="T">The type of elements in the pdf group</typeparam>
    public abstract class PDFGroup<T> : PDFObject where T : PDFObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFGroup{T}"/> class.
        /// </summary>
        /// <param name="offsetX">The x offset for this object.</param>
        /// <param name="offsetY">The y offset for this object.</param>
        public PDFGroup(bool canResize)
            : base(canResize, RectangleF.Empty)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFGroup{T}"/> class.
        /// </summary>
        /// <param name="offset">The offset for this object.</param>
        public PDFGroup(bool canResize, RectangleF rectangle)
            : base(canResize, rectangle)
        {
        }

        /// <summary>
        /// When implemented in a deriving class, non-recursively gets the PDF objects contained by this <see cref="PDFGroup"/>.
        /// </summary>
        /// <returns>A collection of the <see cref="PDFObject"/>s contained by this <see cref="PDFGroup."/></returns>
        protected internal abstract IEnumerable<T> GetPDFObjects();

        /// <summary>
        /// Returns the offset of any element within this <see cref="PDFGroup{T}"/>.
        /// </summary>
        /// <param name="obj">The element whichs offset is returned.</param>
        /// <returns>The offset of <paramref name="obj"/>.</returns>
        protected internal virtual PointF GetGroupingOffset(T obj)
        {
            return PointF.Empty;
        }
    }
}
