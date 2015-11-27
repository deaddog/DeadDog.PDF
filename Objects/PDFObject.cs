using System;

namespace DeadDog.PDF
{
    /// <summary>
    /// Implements basic functionalities for pdf objects.
    /// </summary>
    public abstract class PDFObject
    {
        private bool canResize;
        private Vector2D offset, size;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFObject"/> class.
        /// </summary>
        /// <param name="canResize">if set to <c>true</c> the <see cref="PDFObject"/> can be resized using its <see cref="PDFObject.Size"/> property.</param>
        /// <param name="offset">The offset of the <see cref="PDFObject"/>.</param>
        /// <param name="size">The size of the <see cref="PDFObject"/>.</param>
        public PDFObject(bool canResize, Vector2D offset, Vector2D size)
        {
            this.canResize = canResize;
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
        /// Gets a value indicating whether this instance can resize.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can resize; otherwise, <c>false</c>.
        /// </value>
        public bool CanResize
        {
            get { return canResize; }
        }

        /// <summary>
        /// Gets or sets the size of this <see cref="PDFObject"/>.
        /// </summary>
        public Vector2D Size
        {
            get { return canResize ? size : getSize(); }
            set
            {
                if (!canResize)
                    throw new InvalidOperationException(typeof(PDFObject).Name + " cannot be resized when CanResize is false.");
                else
                    size = value;
            }
        }

        /// <summary>
        /// When implemented in a derived class, gets the size of the <see cref="PDFObject"/>.
        /// This method will only be called when <see cref="CanResize"/> is <c>false</c>.
        /// </summary>
        /// <returns>The size of the <see cref="PDFObject"/>.</returns>
        protected virtual Vector2D getSize()
        {
            return size;
        }
    }
}
