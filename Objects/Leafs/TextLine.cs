using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a text line in a pdf document.
    /// </summary>
    public sealed class TextLine : LeafObject, IPDFObject
    {
        private string text;
        private Color color;
        private FontInfo font;
        private TextAlignment alignment;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextLine"/> class with the selected properties.
        /// </summary>
        /// <param name="text">The text displayed in this textline.</param>
        /// <param name="font">The font used to display text in this textline.</param>
        public TextLine(string text, FontInfo font)
            : this(text, font, new PointF(0, 0), TextAlignment.Left, Color.Black)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextLine"/> class with the selected properties.
        /// </summary>
        /// <param name="text">The text displayed in this textline.</param>
        /// <param name="font">The font used to display text in this textline.</param>
        /// <param name="p">The location of this textline.</param>
        public TextLine(string text, FontInfo font, PointF p)
            : this(text, font, p, TextAlignment.Left, Color.Black)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextLine"/> class with the selected properties.
        /// </summary>
        /// <param name="text">The text displayed in this textline.</param>
        /// <param name="font">The font used to display text in this textline.</param>
        /// <param name="p">The location of this textline.</param>
        /// <param name="alignment">The horizontal alignment of text in this textline.</param>
        public TextLine(string text, FontInfo font, PointF p, TextAlignment alignment)
            : this(text, font, p, alignment, Color.Black)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextLine"/> class with the selected properties.
        /// </summary>
        /// <param name="text">The text displayed in this textline.</param>
        /// <param name="font">The font used to display text in this textline.</param>
        /// <param name="p">The location of this textline.</param>
        /// <param name="alignment">The horizontal alignment of text in this textline.</param>
        /// <param name="color">The color of the text.</param>
        public TextLine(string text, FontInfo font, PointF p, TextAlignment alignment, Color color)
            : base()
        {
            this.Location = p;
            this.text = text;
            this.font = font;
            this.color = color;
            this.alignment = alignment;
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
            set { font = value;}
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
