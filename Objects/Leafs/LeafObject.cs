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
        /// <param name="canResize">if set to <c>true</c> the <see cref="LeafObject"/> can be resized using its <see cref="PDFObject.Size" /> property.</param>
        /// <param name="offset">The offset of the <see cref="LeafObject"/>.</param>
        /// <param name="size">The size of the <see cref="LeafObject"/>.</param>
        public LeafObject(bool canResize, Vector2D offset, Vector2D size)
            : base(canResize, offset, size)
        {
        }

        /// <summary>
        /// Renders the object in a document using a <see cref="ContentWriter"/>.
        /// This method can be overridden in derived classes to specify rendering.
        /// </summary>
        /// <param name="cw">The <see cref="ContentWriter"/> used for rendering.</param>
        /// <param name="offset">The calculated offset for the rendered item.</param>
        protected virtual void Render(ContentWriter cw, Vector2D offset)
        {
        }
        /// <summary>
        /// Renders the object in a document using a <see cref="PdfContentByte"/> by invoking the <see cref="Render(ContentWriter, Vector2D)"/> method.
        /// This method can be overridden in derived classes to specify rendering.
        /// </summary>
        /// <param name="cb">The <see cref="PdfContentByte"/> used for rendering.</param>
        /// <param name="offset">The calculated offset for the rendered item.</param>
        protected internal virtual void Render(PdfContentByte cb, Vector2D offset)
        {
            using (var writer = new ContentWriter(cb))
                Render(writer, offset);
        }
    }
}
