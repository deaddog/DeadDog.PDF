using System.Drawing;
using System.Linq;
using iTextSharp.text.pdf;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a text line in a pdf document.
    /// </summary>
    public sealed class TextLine : LeafObject
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
            : base(Vector2D.Zero, Vector2D.Zero)
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
            return CalculateSize(text, font);
        }

        public static Vector2D CalculateSize(string text, FontInfo font)
        {
            var arr = text.Split('\n');
            Vector1D w = arr.Max(x => font.MeasureStringWidth(x));
            return new Vector2D(w, font.Height * arr.Length);
        }

        /// <summary>
        /// Gets the distance between the Y property of this <see cref="TextLine"/> and the baseline of the text.
        /// </summary>
        public Vector1D Baseline
        {
            get { return this.Offset.Y + font.AscenderHeight + font.BaseHeight; }
            set { this.Offset = new Vector2D(this.Offset.X, value - font.AscenderHeight - font.BaseHeight); }
        }

        protected internal override void Render(PdfContentByte cb, Vector2D offset)
        {
            offset.Y += Size.Y + Offset.Y - Baseline;

            cb.BeginText();
            cb.SetColorFill(new iTextSharp.text.Color(color));
            cb.SetFontAndSize(font.iTextSharpFont.BaseFont, font.Size);
            foreach (var s in text.Split('\n'))
            {
                cb.ShowTextAligned(textAlignment(alignment), s, (float)offset.X.Value(UnitsOfMeasure.Points), (float)offset.Y.Value(UnitsOfMeasure.Points), 0);
                offset.Y -= font.Height;
            }
            cb.EndText();
        }
        private int textAlignment(TextAlignment ta)
        {
            switch (ta)
            {
                case TextAlignment.Left: return 0;
                case TextAlignment.Center: return 1;
                default: return 2;
            }
        }
    }
}
