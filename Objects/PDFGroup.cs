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
        private List<T> privatelist;

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFGroup{T}"/> class.
        /// </summary>
        /// <param name="offsetX">The x offset for this object.</param>
        /// <param name="offsetY">The y offset for this object.</param>
        public PDFGroup(bool canResize)
            : base(canResize, RectangleF.Empty)
        {
            this.privatelist = new List<T>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFGroup{T}"/> class.
        /// </summary>
        /// <param name="offset">The offset for this object.</param>
        public PDFGroup(bool canResize, RectangleF rectangle)
            : base(canResize, rectangle)
        {
            this.privatelist = new List<T>();
        }
        public List<T> Objects
        {
            get { return privatelist; }
        }

        /// <summary>
        /// Collects all pdf objects contained within this group.
        /// </summary>
        /// <param name="collector">An <see cref="ObjectCollector"/> to which all pdf objects are added.</param>
        public void Collect(ObjectCollector collector)
        {
            foreach (T t in privatelist)
                collector.Add(t);
        }

        #region IPDFGroup<T> Members

        /// <summary>
        /// Returns the location of any element within this <see cref="PDFGroup{T}"/>.
        /// </summary>
        /// <param name="obj">The element whichs location is returned.</param>
        /// <returns>The location of obj.</returns>
        public abstract PointF GetLocation(T obj);

        #endregion
    }
}
