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
            this.offset = Vector2D.Zero;
            this.size = Vector2D.Zero;

            boundsChange(offset.X, offset.Y, size.X, size.Y);
        }

        /// <summary>
        /// Gets or sets the offset for this <see cref="PDFObject"/>.
        /// </summary>
        public Vector2D Offset
        {
            get { return offset; }
            set { boundsChange(value.X, value.Y, size.X, size.Y); }
        }
        /// <summary>
        /// Gets or sets the x-coordinate for this <see cref="PDFObject"/>s offset.
        /// </summary>
        public Vector1D OffsetX
        {
            get { return offset.X; }
            set { Offset = new Vector2D(value, offset.Y); }
        }
        /// <summary>
        /// Gets or sets the y-coordinate for this <see cref="PDFObject"/>s offset.
        /// </summary>
        public Vector1D OffsetY
        {
            get { return offset.Y; }
            set { Offset = new Vector2D(offset.X, value); }
        }

        /// <summary>
        /// Gets or sets the size of this <see cref="PDFObject"/>.
        /// </summary>
        public Vector2D Size
        {
            get { return getSize(); }
            set { boundsChange(offset.X, offset.Y, value.X, value.Y); }
        }
        /// <summary>
        /// Gets or sets the width of this <see cref="PDFObject"/>.
        /// </summary>
        public Vector1D Width
        {
            get { return size.X; }
            set { Size = new Vector2D(value, size.Y); }
        }
        /// <summary>
        /// Gets or sets the height of this <see cref="PDFObject"/>.
        /// </summary>
        public Vector1D Height
        {
            get { return size.Y; }
            set { Size = new Vector2D(size.X, value); }
        }

        /// <summary>
        /// When implemented in a derived class, gets the size of the <see cref="PDFObject"/>.
        /// </summary>
        /// <returns>The size of the <see cref="PDFObject"/>.</returns>
        protected virtual Vector2D getSize()
        {
            return size;
        }

        private void boundsChange(Vector1D offsetX, Vector1D offsetY, Vector1D width, Vector1D height)
        {
            InnerBoundsChange(ref offsetX, ref offsetY, ref width, ref height);

            this.offset = new Vector2D(offsetX, offsetY);
            this.size = new Vector2D(width, height);
        }

        /// <summary>
        /// When implemented in a derived class, ensures that the offset and size values specified for an object are "acceptable".
        /// This method will be executed when changes are made to an objects size or offset, before the actual change is applied to the object.
        /// When this method exits the object is updated with the parameter values.
        /// </summary>
        /// <param name="offsetX">The new x-coordinate of the objects local offset.</param>
        /// <param name="offsetY">The new y-coordinate of the objects local offset.</param>
        /// <param name="width">The new width of the object.</param>
        /// <param name="height">The new height of the object.</param>
        protected virtual void InnerBoundsChange(ref Vector1D offsetX, ref Vector1D offsetY, ref Vector1D width, ref Vector1D height)
        {
        }
    }
}
