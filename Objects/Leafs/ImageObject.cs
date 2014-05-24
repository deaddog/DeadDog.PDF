using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        public ImageObject(string filepath, float width, float height)
            : this(filepath, 0, 0, width, height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObject"/> class.
        /// </summary>
        /// <param name="size">The size of the image.</param>
        public ImageObject(string filepath, SizeF size)
            : this(filepath, new RectangleF(PointF.Empty, size))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObject"/> class.
        /// </summary>
        /// <param name="offsetX">The x offset for the image.</param>
        /// <param name="offsetY">The y offset for the image.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        public ImageObject(string filepath, float offsetX, float offsetY, float width, float height)
            : this(filepath, RectangleF.FromLTRB(offsetX, offsetY, offsetX + width, offsetY + height))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObject"/> class.
        /// </summary>
        /// <param name="rectangle">A rectangle describing the offset and size of the image.</param>
        public ImageObject(string filepath, RectangleF rectangle)
            : base(true, rectangle)
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
