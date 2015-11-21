using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw an image in a pdf document.
    /// </summary>
    public class ImageObject : LeafObject
    {
        private Image image;
        private string filepath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObject"/> class.
        /// </summary>
        /// <param name="size">The size of the image.</param>
        public ImageObject(string filepath, Vector2D size)
            : base(true, Vector2D.Zero, size)
        {
            this.filepath = filepath;
        }

        /// <summary>
        /// Gets the filepath of the imagefile associated with this <see cref="ImageObject"/>.
        /// </summary>
        public string Filepath
        {
            get { return filepath; }
        }

        protected internal override void Render(PdfContentByte cb, Vector2D offset)
        {
            if (image == null)
                image = Image.GetInstance(filepath);

            image.ScaleAbsolute((float)Size.X.Value(UnitsOfMeasure.Points), (float)Size.Y.Value(UnitsOfMeasure.Points));
            image.SetAbsolutePosition((float)offset.X.Value(UnitsOfMeasure.Points), (float)offset.Y.Value(UnitsOfMeasure.Points));

            cb.AddImage(image);
        }
    }
}
