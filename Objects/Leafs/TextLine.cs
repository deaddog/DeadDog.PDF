using System;
using System.Drawing;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a text line in a pdf document.
    /// </summary>
    public sealed class TextLine : PDFObject
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
            : base(false, Vector2D.Zero, Vector2D.Zero)
        {
            this.text = string.Empty;
            this.font = font;
            this.color = Color.Black;
            this.alignment = TextAlignment.Left;
        }

        /// <summary>
        /// Gets or sets the text displayed by this <see cref="TextLine"/>. New line characters are not displayed - see <see cref="TextBox"/>.
        /// </summary>
        public string Text
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
        /// Gets the size of the string contained by this <see cref="TextLine"/>.
        /// </summary>
        /// <returns>
        /// The size of the <see cref="TextLine" />.
        /// </returns>
        protected override Vector2D getSize()
        {
            return new Vector2D(font.MeasureStringWidth(text), font.Height);
        }

        /// <summary>
        /// Gets the distance between the Y property of this <see cref="TextLine"/> and the baseline of the text.
        /// </summary>
        public Vector1D Baseline
        {
            get { return this.Offset.Y + font.AscenderHeight + font.BaseHeight; }
            set { this.Offset = new Vector2D(this.Offset.X, value - font.AscenderHeight - font.BaseHeight); }
        }
    }
}
