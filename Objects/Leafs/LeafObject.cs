using iTextSharp.text.pdf;

namespace DeadDog.PDF
{
    /// <summary>
    /// Represents a leaf-object in the PDF composite structure.
    /// A <see cref="LeafObject"/> is non-composite and is "rendered" directly in PDF documents.
    /// </summary>
    public abstract class LeafObject : PDFObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeafObject"/> class.
        /// </summary>
        /// <param name="canResize">if set to <c>true</c> the <see cref="LeafObject" /> can be resized using its <see cref="PDFObject.Size" /> property.</param>
        internal LeafObject(bool canResize)
            : base(canResize)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LeafObject"/> class.
        /// </summary>
        /// <param name="canResize">if set to <c>true</c> the <see cref="LeafObject"/> can be resized using its <see cref="PDFObject.Size" /> property.</param>
        /// <param name="offset">The offset of the <see cref="LeafObject"/>.</param>
        /// <param name="size">The size of the <see cref="LeafObject"/>.</param>
        internal LeafObject(bool canResize, Vector2D offset, Vector2D size)
            : base(canResize, offset, size)
        {
        }

        internal protected abstract void Render(PdfContentByte cb, Vector2D offset);
    }
}
