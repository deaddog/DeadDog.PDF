using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a text line in a pdf document.
    /// </summary>
    public sealed class TextLine : PDFObject, IPDFObject
    {
        private string text;
        private Color color;
        private FontInfo font;
        private TextAlignment alignment;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextLine"/> class.
        /// </summary>
        /// <param name="font">The font used to display text in this textline.</param>
        public TextLine(FontInfo font)
            : base()
        {
            this.Location = new PointF(0, 0);
            this.text = string.Empty;
            this.font = font;
            this.color = Color.Black;
            this.alignment = TextAlignment.Left;
        }

        /// <summary>
        /// Gets or sets the text displayed by this <see cref="TextLine"/>. New line characters are not displayed - see <see cref="TextBox"/>.
        /// </summary>
        public String Text
        {
            get { return text; }
            set { text = value; }
        }
        /// <summary>
        /// Gets or sets the color of the text in this <see cref="TextLine"/>.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        /// <summary>
        /// Gets or sets the font used by this <see cref="TextLine"/>.
        /// </summary>
        public FontInfo Font
        {
            get { return font; }
            set { font = value; }
        }
        /// <summary>
        /// Gets or sets the horizontal alignment of text in this <see cref="TextLine"/>.
        /// </summary>
        public TextAlignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        /// <summary>
        /// Gets the height of this <see cref="TextLine"/> object.
        /// </summary>
        public override float Height
        {
            get { return font.Height; }
        }
        /// <summary>
        /// Gets the width of this <see cref="TextLine"/> object.
        /// </summary>
        public override float Width
        {
            get { return font.MeasureStringWidth(text); }
        }

        /// <summary>
        /// Gets the distance between the Y property of this <see cref="TextLine"/> and the baseline of the text.
        /// </summary>
        public float Baseline
        {
            get { return this.Y + font.AscenderHeight + font.BaseHeight; }
            set { this.Y = value - font.AscenderHeight - font.BaseHeight; }
        }
    }
}
