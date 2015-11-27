namespace DeadDog.PDF
{
    /// <summary>
    /// Implements basic functionalities for pdf objects.
    /// </summary>
    public abstract class PDFObject
    {
        private Vector2D offset, size;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFObject"/> class.
        /// </summary>
        /// <param name="offset">The offset of the <see cref="PDFObject"/>.</param>
        /// <param name="size">The size of the <see cref="PDFObject"/>.</param>
        public PDFObject(Vector2D offset, Vector2D size)
        {
            this.offset = offset;
            this.size = size;
        }

        /// <summary>
        /// Gets or sets the offset for this <see cref="PDFObject"/>.
        /// </summary>
        public Vector2D Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        
        /// <summary>
        /// Gets or sets the size of this <see cref="PDFObject"/>.
        /// </summary>
        public Vector2D Size
        {
            get { return getSize(); }
            set { size = value; }
        }

        /// <summary>
        /// When implemented in a derived class, gets the size of the <see cref="PDFObject"/>.
        /// </summary>
        /// <returns>The size of the <see cref="PDFObject"/>.</returns>
        protected virtual Vector2D getSize()
        {
            return size;
        }
    }
}
