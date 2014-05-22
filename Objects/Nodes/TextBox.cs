using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information required to draw a TextBox in a pdf document. A TextBox consists of several lines of text confined within a <see cref="Box"/> object.
    /// </summary>
    public class TextBox : PDFGroupSizeable
    {
        /// <summary>
        /// Defines constants for determining TextBox sizes.
        /// </summary>
        public enum SizingMethod
        {
            /// <summary>
            /// TextBox height is fixed.
            /// </summary>
            FixedHeight,
            /// <summary>
            /// TextBox width is fixed.
            /// </summary>
            FixedWidth,
            /// <summary>
            /// Both width and height of TextBox are fixed.
            /// </summary>
            FixedSize
        }
        private SizingMethod sizing = SizingMethod.FixedWidth;
        private SizeF size;
        private FontInfo font;
        private TextAlignment align;
        private VerticalAlignment valign;
        private Color color;

        private Box box;
        private VerticalGroup<TextLine> textGroup;

        private float linespacing = 1f;
        private float[] margins = new float[] { 0, 0, 0, 0 };
        private string text = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class with the selected properties.
        /// </summary>
        /// <param name="text">The text displayed in this textbox.</param>
        /// <param name="font">The font used to display text in this textbox.</param>
        /// <param name="size">The size of the <see cref="Box"/> containing the text.</param>
        public TextBox(string text, FontInfo font, SizeF size)
            : this(text, font, new RectangleF(new PointF(0, 0), size), SizingMethod.FixedWidth, TextAlignment.Left, VerticalAlignment.Middle, Color.Black)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class with the selected properties.
        /// </summary>
        /// <param name="text">The text displayed in this textbox.</param>
        /// <param name="font">The font used to display text in this textbox.</param>
        /// <param name="rectangle">The size and location of this textbox.</param>
        /// <param name="sizing">The method used for resizing this textbox according to text and font.</param>
        /// <param name="alignment">The horizontal alignment of text within this textbox.</param>
        /// <param name="valign">The vertical alignment of text within this textbox.</param>
        /// <param name="color">The color of the text.</param>
        public TextBox(string text, FontInfo font, RectangleF rectangle, SizingMethod sizing, TextAlignment alignment, VerticalAlignment valign, Color color)
            : base()
        {
            box = new Box(rectangle, true);
            textGroup = new VerticalGroup<TextLine>(0);

            switch (alignment)
            {
                case TextAlignment.Left: this.textGroup.Alignment = HorizontalAlignment.Left;
                    break;
                case TextAlignment.Right: this.textGroup.Alignment = HorizontalAlignment.Right;
                    break;
                case TextAlignment.Center: this.textGroup.Alignment = HorizontalAlignment.Center;
                    break;
            }

            this.text = text;
            this.font = font;
            this.Location = rectangle.Location;
            this.size = rectangle.Size;
            this.sizing = sizing;
            this.align = alignment;
            this.valign = valign;
            this.color = color;

            list.Add(box);
            list.Add(textGroup);

            ResetStrings();
        }

        #region Box properties
        /// <summary>
        /// Gets or sets whether the border of this <see cref="TextBox"/> is displayed.
        /// </summary>
        public bool HasBorder
        {
            get { return (box.HasBorder); }
            set { box.HasBorder = value; }
        }
        /// <summary>
        /// Gets or sets whether the filling of this <see cref="TextBox"/> is displayed.
        /// </summary>
        public bool HasFill
        {
            get { return box.HasFill; }
            set { box.HasFill = value; }
        }
        /// <summary>
        /// Gets or sets the color used for filling this <see cref="TextBox"/>.
        /// </summary>
        public Color FillColor
        {
            get { return box.FillColor; }
            set { box.FillColor = value; }
        }
        /// <summary>
        /// Gets or sets the color used for drawing the border of this <see cref="TextBox"/>.
        /// </summary>
        public Color BorderColor
        {
            get { return box.BorderColor; }
            set { box.BorderColor = value; }
        }
        /// <summary>
        /// Gets or sets the width (thickness) of the border of this <see cref="TextBox"/>.
        /// </summary>
        public float BorderWidth
        {
            get { return box.BorderWidth; }
            set { box.BorderWidth = value; }
        }
        #endregion

        /// <summary>
        /// Gets the text in this <see cref="TextBox"/> at the given line.
        /// </summary>
        /// <param name="lineIndex">The index of the line. The first line is index 0.</param>
        /// <returns>The text currently displayed at this line index.</returns>
        public string GetLine(int lineIndex)
        {
            return textGroup.Objects[lineIndex].Text;
        }
        /// <summary>
        /// Gets or sets the text displayed by this <see cref="TextBox"/>.
        /// </summary>
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                ResetStrings();
            }
        }
        /// <summary>
        /// Gets or sets the font used by this <see cref="TextBox"/>.
        /// </summary>
        public FontInfo Font
        {
            get { return font; }
            set
            {
                font = value;
                foreach (TextLine t in textGroup.Objects)
                    t.Font = font;
                ResetStrings();
            }
        }
        /// <summary>
        /// Gets or sets the color of the text in this <see cref="TextBox"/>.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                foreach (TextLine t in textGroup.Objects)
                    t.Color = color;
            }
        }
        /// <summary>
        /// Gets or sets the vertical alignment of text in this <see cref="TextBox"/>.
        /// </summary>
        public VerticalAlignment VerticalAlignment
        {
            get { return valign; }
            set { valign = value; }
        }
        /// <summary>
        /// Gets or sets the horizontal alignment of text in this <see cref="TextBox"/>.
        /// </summary>
        public TextAlignment Alignment
        {
            get { return align; }
            set
            {
                switch (value)
                {
                    case TextAlignment.Left: this.textGroup.Alignment = HorizontalAlignment.Left;
                        break;
                    case TextAlignment.Right: this.textGroup.Alignment = HorizontalAlignment.Right;
                        break;
                    case TextAlignment.Center: this.textGroup.Alignment = HorizontalAlignment.Center;
                        break;
                }

                this.align = value;
                foreach (TextLine t in textGroup.Objects)
                    t.Alignment = align;
            }
        }

        /// <summary>
        /// Gets the current amount of lines in this <see cref="TextBox"/>.
        /// </summary>
        public int Lines
        {
            get
            {
                ResetStrings();
                if (textGroup.Objects.Count == 1 && textGroup.Objects[0].Text.Length == 0)
                    return 0;
                else
                    return textGroup.Objects.Count;
            }
        }
        /// <summary>
        /// Gets the distance between the Y property of this <see cref="TextBox"/> and the baseline of the selected line index.
        /// </summary>
        /// <param name="lineIndex">The line index which baseline is returned. The line index does not have to exist.</param>
        /// <returns>The calculated distance from the Y property to the baseline.</returns>
        public float Baseline(int lineIndex)
        {
            return textY + font.AscenderHeight + font.BaseHeight + font.Height * ((float)lineIndex);
        }
        /// <summary>
        /// Gets or sets the linespacing within this TextBox. The value represent the amount of line heights between baselines.
        /// </summary>
        public float LineSpacing
        {
            get { return linespacing; }
            set { this.linespacing = value; textGroup.Spacer = (value - 1) * font.Height; }
        }

        /// <summary>
        /// Sets the margin of this <see cref="TextBox"/> to the same value on all four sides.
        /// </summary>
        public float Margin
        {
            set
            {
                if (value < 0)
                    throw new ArgumentException("Margin cannot be lower than 0", "value");
                margins[0] = margins[1] = margins[2] = margins[3] = value; ResetStrings();
            }
        }
        /// <summary>
        /// Gets or sets the margin at the top of this <see cref="TextBox"/>.
        /// </summary>
        public float MarginTop
        {
            get { return margins[0]; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Margin cannot be lower than 0", "value");
                margins[0] = value; ResetStrings();
            }
        }
        /// <summary>
        /// Gets or sets the margin at the right of this <see cref="TextBox"/>.
        /// </summary>
        public float MarginRight
        {
            get { return margins[1]; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Margin cannot be lower than 0", "value");
                margins[1] = value; ResetStrings();
            }
        }
        /// <summary>
        /// Gets or sets the margin at the bottom of this <see cref="TextBox"/>.
        /// </summary>
        public float MarginBottom
        {
            get { return margins[2]; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Margin cannot be lower than 0", "value");
                margins[2] = value; ResetStrings();
            }
        }
        /// <summary>
        /// Gets or sets the margin at the left of this <see cref="TextBox"/>.
        /// </summary>
        public float MarginLeft
        {
            get { return margins[3]; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Margin cannot be lower than 0", "value");
                margins[3] = value; ResetStrings();
            }
        }

        /// <summary>
        /// Gets or sets a value describing the method used for resizing this <see cref="TextBox"/>.
        /// </summary>
        public SizingMethod Sizing
        {
            get { return sizing; }
            set { sizing = value; ResetStrings(); }
        }
        /// <summary>
        /// Gets a value indication whether or not the height of this textbox is fixed.
        /// </summary>
        public bool HeightIsFixed
        {
            get { return sizing == SizingMethod.FixedHeight || sizing == SizingMethod.FixedSize; }
        }
        /// <summary>
        /// Gets a value indicating whether or not the width of this textbox is fixed.
        /// </summary>
        public bool WidthIsFixed
        {
            get { return sizing == SizingMethod.FixedWidth || sizing == SizingMethod.FixedSize; }
        }

        /// <summary>
        /// Gets or sets the size of the <see cref="TextBox"/>. If either height or width (or both) are fixed this is their actual size. If not this is their minimal size. Margins are inclusive.
        /// </summary>
        public override SizeF Size
        {
            get
            {
                switch (sizing)
                {
                    case SizingMethod.FixedHeight:
                        return new SizeF(Width, size.Height);
                    case SizingMethod.FixedWidth:
                        return new SizeF(size.Width, Height);
                    case SizingMethod.FixedSize:
                        return size;
                }
                throw new NotImplementedException();
            }
            set { this.size = value; }
        }
        /// <summary>
        /// Gets or sets the width of the <see cref="TextBox"/>. If the width is fixed this is the actual width of the <see cref="TextBox"/>. If not this is the minimal width. Margins are inclusive.
        /// </summary>
        public override float Width
        {
            get
            {
                switch (sizing)
                {
                    case SizingMethod.FixedHeight:
                        break;
                    case SizingMethod.FixedWidth:
                        return size.Width;
                    case SizingMethod.FixedSize:
                        return size.Width;
                }
                throw new NotImplementedException();
            }
            set { this.size.Width = value; }
        }
        /// <summary>
        /// Gets or sets the height of the <see cref="TextBox"/>. If the height is fixed this is the actual height of the <see cref="TextBox"/>. If not this is the minimal height. Margins are inclusive.
        /// </summary>
        public override float Height
        {
            get
            {
                switch (sizing)
                {
                    case SizingMethod.FixedHeight:
                        return size.Height;
                    case SizingMethod.FixedWidth:
                        float f = MarginTop + MarginBottom + textGroup.Height;
                        return f > this.size.Height ? f : this.size.Height;
                    case SizingMethod.FixedSize:
                        return size.Height;
                }
                throw new NotImplementedException();
            }
            set { this.size.Height = value; }
        }

        /// <summary>
        /// Returns the location of the individual elements within this TextBox.
        /// </summary>
        /// <param name="obj">The element whichs location is returned.</param>
        /// <returns>The location of obj.</returns>
        public override System.Drawing.PointF GetLocation(IPDFObject obj)
        {
            if (obj == box)
                return this.Location;
            else if (obj == textGroup)
                return new PointF(textX, textY);
            else
                throw new ArgumentException("Object not found", "obj");
        }

        #region Private methods
        private float textX
        {
            get
            {
                switch (align)
                {
                    case TextAlignment.Left:
                        return this.X + MarginLeft;
                    case TextAlignment.Right:
                        return this.X + this.Width - MarginRight;
                    case TextAlignment.Center:
                        return this.X + MarginLeft - MarginRight + this.Width / 2f;
                }
                throw new NotImplementedException();
            }
        }
        private float textY
        {
            get
            {
                switch (valign)
                {
                    case VerticalAlignment.Top:
                        return this.Y + MarginTop;
                    case VerticalAlignment.Middle:
                        return this.Y + MarginTop + (this.Height - MarginTop - MarginBottom - textGroup.Height) / 2;
                    case VerticalAlignment.Bottom:
                        return this.Y + this.Height - MarginBottom;
                }
                throw new NotImplementedException();
            }
        }

        private void ResetStrings()
        {
            switch (sizing)
            {
                case SizingMethod.FixedHeight:
                    throw new NotImplementedException();
                case SizingMethod.FixedWidth:
                    break;
                case SizingMethod.FixedSize:
                    throw new NotImplementedException();
            }
            string[] ss = split(text, box.Width - margins[1] - margins[3]);

            //Set a correct amount of elements 
            if (ss.Length > textGroup.Objects.Count)
                for (int i = textGroup.Objects.Count; i < ss.Length; i++)
                    textGroup.Objects.Add(new TextLine(font)
                    {
                        Alignment = align,
                        Color = color
                    });
            else if (ss.Length < textGroup.Objects.Count)
                textGroup.Objects.RemoveRange(0, textGroup.Objects.Count - ss.Length);

            for (int i = 0; i < ss.Length; i++)
                textGroup.Objects[i].Text = ss[i];
            textGroup.Y = textY - textGroup.Height / 2; textGroup.X = textX;
        }

        private string[] split(string s, float maxWidth)
        {
            List<string> list = new List<string>();
            list.AddRange(s.Replace("\r\n", "\n").Replace('\r', '\n').Split('\r', '\n'));
            int index = 0;
            while (index < list.Count)
            {
                int i = check(list[index], 0, list[index].Length, maxWidth);

                string tempS = list[index].Substring(0, i);
                if (list[index].Length > i && i > 0)
                {// Might "-" come before " "? Gain both indexes and examine.
                    if (tempS.Contains(" "))
                    {
                        tempS = tempS.Substring(0, tempS.LastIndexOf(' '));
                        list[index] = list[index].Substring(tempS.Length + 1);
                    }
                    else if (tempS.Contains("-"))
                    {
                        tempS = tempS.Substring(0, tempS.LastIndexOf('-') + 1);
                        list[index] = list[index].Substring(tempS.Length);
                    }
                    else
                    {
                        list[index] = list[index].Substring(tempS.Length);
                    }
                    list.Insert(index, tempS);
                }

                index++;
            }
            return list.ToArray();
        }
        private int check(string s, int offset, int length, float maxWidth)
        {
            if (length == 1)
            {
                if (validlength(s, offset + length, maxWidth))
                    return offset + 1;
                else
                    return offset;
            }

            int a = (length + 1) / 2;
            if (a == 0)
                return 0;
            else if (validlength(s, offset + a, maxWidth))
                return check(s, offset + a, length - a, maxWidth);
            else
                return check(s, offset, a, maxWidth);
        }
        private bool validlength(string s, int length, float maxWidth)
        {
            float f = font.MeasureStringWidth(s.Substring(0, length).Trim());
            return f <= maxWidth;
        }
        #endregion
    }
}
