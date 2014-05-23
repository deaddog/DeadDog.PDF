using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw an image in a pdf document.
    /// </summary>
    public class ImageObject : PDFObjectSizeable, IPDFObject
    {
        private string filepath;
        private SizeF size;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObject"/> class with the selected properties.
        /// </summary>
        /// <param name="filepath">The filepath of the imagefile associated with this <see cref="ImageObject"/>.</param>
        /// <param name="size">The size of this image.</param>
        public ImageObject(string filepath, SizeF size)
            : this(filepath, new RectangleF(new PointF(0, 0), size))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObject"/> class with the selected properties.
        /// </summary>
        /// <param name="filepath">The filepath of the imagefile associated with this <see cref="ImageObject"/>.</param>
        /// <param name="width">The width of this image.</param>
        /// <param name="height">The height of this image.</param>
        public ImageObject(string filepath, float width, float height)
            : this(filepath, new RectangleF(0, 0, width, height))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageObject"/> class with the selected properties.
        /// </summary>
        /// <param name="filepath">The filepath of the imagefile associated with this <see cref="ImageObject"/>.</param>
        /// <param name="rectangle">The location and size of this image.</param>
        public ImageObject(string filepath, RectangleF rectangle)
            : base()
        {
            this.filepath = filepath;
            this.Location = rectangle.Location;
            this.Size = rectangle.Size;
        }

        /// <summary>
        /// Gets the filepath of the imagefile associated with this <see cref="ImageObject"/>.
        /// </summary>
        public string Filepath
        {
            get { return filepath; }
        }

        /// <summary>
        /// Gets or sets the size of this <see cref="ImageObject"/> object.
        /// </summary>
        public override SizeF Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// Gets or sets the height of this <see cref="ImageObject"/> object.
        /// </summary>
        public override float Height
        {
            get { return size.Height; }
            set { size.Height = value; }
        }
        /// <summary>
        /// Gets or sets the width of this <see cref="ImageObject"/> object.
        /// </summary>
        public override float Width
        {
            get { return size.Width; }
            set { size.Width = value; }
        }
    }
}
