using iTextSharp.text;
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
        /// <param name="offset">The offset of the <see cref="LeafObject"/>.</param>
        /// <param name="size">The size of the <see cref="LeafObject"/>.</param>
        public LeafObject(Vector2D offset, Vector2D size)
            : base(offset, size)
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
            {
                var stroke = this as StrokeObject;
                var fill = this as FillObject;

                bool hasstroke = stroke?.BorderColor.HasValue ?? false;
                bool hasfill = fill?.FillColor.HasValue ?? false;

                if (hasstroke)
                {
                    cb.SetLineWidth((float)stroke.BorderWidth.Value(UnitsOfMeasure.Points));
                    cb.SetColorStroke(new Color(stroke.BorderColor.Value));
                }
                if (hasfill)
                    cb.SetColorFill(new Color(fill.FillColor.Value));

                Render(writer, offset);

                if (hasstroke && hasfill)
                {
                    if (writer.CloseShape)
                        cb.ClosePathFillStroke();
                    else
                        cb.FillStroke();
                }
                else if (hasstroke)
                {
                    if (writer.CloseShape)
                        cb.ClosePathStroke();
                    else
                        cb.Stroke();
                }
                else if (hasfill)
                    cb.Fill();
            }
        }
    }
}
