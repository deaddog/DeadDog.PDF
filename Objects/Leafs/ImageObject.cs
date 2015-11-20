namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw an image in a pdf document.
    /// </summary>
    public class ImageObject : PDFObject
    {
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
    }
}
