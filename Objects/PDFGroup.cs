using System.Collections.Generic;

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
        /// <param name="canResize">if set to <c>true</c> the <see cref="PDFGroup"/> can be resized using its <see cref="PDFObject.Size" /> property.</param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        public PDFGroup(bool canResize, Vector2D offset, Vector2D size)
            : base(canResize, offset, size)
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
        /// <param name="canResize">if set to <c>true</c> the <see cref="PDFGroup{T}"/> can be resized using its <see cref="PDFObject.Size" /> property.</param>
        /// <param name="offset">The offset of the <see cref="PDFGroup{T}"/>.</param>
        /// <param name="size">The size of the <see cref="PDFGroup{T}"/>.</param>
        public PDFGroup(bool canResize, Vector2D offset, Vector2D size)
            : base(canResize, offset, size)
        {
        }

        /// <summary>
        /// When implemented in a deriving class, non-recursively gets the PDF objects contained by this <see cref="PDFGroup{T}"/>.
        /// </summary>
        /// <returns>A collection of the <see cref="PDFObject"/>s contained by this <see cref="PDFGroup{T}."/></returns>
        protected internal abstract IEnumerable<T> GetPDFObjects();

        /// <summary>
        /// Returns the offset of any element within this <see cref="PDFGroup{T}"/>.
        /// </summary>
        /// <param name="obj">The element whichs offset is returned.</param>
        /// <returns>The offset of <paramref name="obj"/>.</returns>
        protected internal virtual Vector2D GetGroupingOffset(T obj)
        {
            return Vector2D.Zero;
        }
    }
}
