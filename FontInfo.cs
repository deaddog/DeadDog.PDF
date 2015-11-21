using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Holds information describing a specific font.
    /// </summary>
    public class FontInfo : IDisposable
    {
        static FontInfo()
        {
            // Get font-directory
            StringBuilder lpszPath = new StringBuilder(260);
            SHGetFolderPath(IntPtr.Zero, 20, IntPtr.Zero, 0, lpszPath);

            // Load all fonts in font-directory
            iTextSharp.text.FontFactory.RegisterDirectory(lpszPath.ToString());
        }

        #region iTextSharp.text.Font vs System.Drawing.Font
        private static int getStyle(System.Drawing.FontStyle style)
        {
            int i = 0;
            switch (style)
            {
                case System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic:
                    i = iTextSharp.text.Font.BOLDITALIC;
                    break;
                case System.Drawing.FontStyle.Bold:
                    i = iTextSharp.text.Font.BOLD;
                    break;
                case System.Drawing.FontStyle.Italic:
                    i = iTextSharp.text.Font.ITALIC;
                    break;
                case System.Drawing.FontStyle.Regular:
                    i = iTextSharp.text.Font.NORMAL;
                    break;
                case System.Drawing.FontStyle.Strikeout:
                    i = iTextSharp.text.Font.STRIKETHRU;
                    break;
                case System.Drawing.FontStyle.Underline:
                    i = iTextSharp.text.Font.UNDERLINE;
                    break;
                default:
                    break;
            }
            return i;
        }
        private static iTextSharp.text.Font getFont(System.Drawing.Font font)
        {
            return iTextSharp.text.FontFactory.GetFont(font.Name, font.Size, getStyle(font.Style));
        }

        [DllImport("shfolder.dll", CharSet = CharSet.Auto)]
        private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath);
        #endregion

        private static string fullHeightString = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        private static string baseHeightString = "weruoaszxcvnm";

        private iTextSharp.text.Font iFont;
        internal iTextSharp.text.Font iTextSharpFont
        {
            get { return iFont; }
        }

        private FontStyle style;
        private float size;
        private string name;

        //Fonts have the following heights:
        //
        // 1     AAAA
        // 2    AA  AA    jj
        // 3    AA  AA
        // 4    AAAAAA    jj
        // 5    AA  AA    jj
        // 6    AA  AA    jj
        // 7              jj
        // 8              jj
        // 9           jjjj
        // 
        // height = 1+2+3+4+5+6+7+8+9
        // ascentHeight = 1+2+3
        // baseHeight = 4+5+6
        // descentHeight = 7+8+9
        // 
        // Actual read values are: ascentHeight, descentHeight, baseHeight

        private float height;
        private float ascenderHeight;
        private float baseHeight;
        private float descenderHeight;

        /// <summary>
        /// Initializes a new DeadDog.PDF.FontInfo based on a System.Drawing.Font object.
        /// </summary>
        /// <param name="font">The System.Drawing.Font object from which to create the new FontInfo.</param>
        public FontInfo(System.Drawing.Font font)
            : this(font.Name, font.Size, font.Style, FontInfo.getFont(font))
        {
        }
        /// <summary>
        /// Initializes a new <see cref="FontInfo"/> using a specified size.
        /// </summary>
        /// <param name="familyName">A string representation of the <see cref="System.Drawing.FontFamily"/> for the new <see cref="FontInfo"/></param>
        /// <param name="emSize">The em-size, in points, of the new font.</param>
        public FontInfo(string familyName, float emSize)
            : this(familyName, emSize, FontStyle.Regular)
        {
        }
        /// <summary>
        /// Initializes a new <see cref="FontInfo"/> using a specified size and style.
        /// </summary>
        /// <param name="familyName">A string representation of the <see cref="System.Drawing.FontFamily"/> for the new <see cref="FontInfo"/></param>
        /// <param name="emSize">The em-size, in points, of the new font.</param>
        /// <param name="style">The System.Drawing.FontStyle of the new font.</param>
        public FontInfo(string familyName, float emSize, FontStyle style)
            : this(familyName, emSize, style, iTextSharp.text.FontFactory.GetFont(familyName, emSize, getStyle(style)))
        {
        }

        private FontInfo(string name, float size, FontStyle style, iTextSharp.text.Font iFont)
        {
            this.name = name;
            this.size = size;
            this.style = style;
            this.iFont = iFont;

            this.ascenderHeight = GetAscenderHeight(iFont);
            this.descenderHeight = GetDescenderHeight(iFont);
            this.baseHeight = GetBaseHeight(iFont);
            this.height = GetLineHeight(iFont);
        }

        /// <summary>
        /// Gets the full height of this font.
        /// </summary>
        public float Height
        {
            get { return height; }
        }
        /// <summary>
        /// Gets the ascender height of this font.
        /// </summary>
        public float AscenderHeight
        {
            get { return ascenderHeight; }
        }
        /// <summary>
        /// Gets the descender height of this font.
        /// </summary>
        public float DescenderHeight
        {
            get { return descenderHeight; }
        }
        /// <summary>
        /// Gets the x-height of this font.
        /// </summary>
        public float BaseHeight
        {
            get { return baseHeight; }
        }

        /// <summary>
        /// Gets the name of this font.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets or sets style information for this font
        /// </summary>
        public FontStyle Style
        {
            get { return this.style; }
            set
            {
                this.style = value;
                iFont.SetStyle(FontInfo.getStyle(value));
            }
        }
        /// <summary>
        /// Gets or sets the size of this font.
        /// </summary>
        public float Size
        {
            get { return this.size; }
            set
            {
                this.size = value;
                iFont.Size = value;
            }
        }

        /// <summary>
        /// Measures the width of the specified string when drawn with this font.
        /// </summary>
        /// <param name="value">String to measure.</param>
        /// <returns>The width of the measured string.</returns>
        public float MeasureStringWidth(string value)
        {
            return GetStringWidth(value, this.iFont);
        }

        #region Various Size Methods

        private static float GetBaseHeight(iTextSharp.text.Font font)
        {
            return getC(font.BaseFont.GetAscentPoint(baseHeightString, font.Size));
        }
        private static float GetLineHeight(iTextSharp.text.Font font)
        {
            return getC(font.BaseFont.GetAscentPoint(fullHeightString, font.Size) - font.BaseFont.GetDescentPoint(fullHeightString, font.Size));
        }
        private static float GetAscenderHeight(iTextSharp.text.Font font)
        {
            //Might look weird, but is this way due to error in original implementation
            return getC(font.BaseFont.GetAscentPoint(fullHeightString, font.Size)
                - font.BaseFont.GetAscentPoint(baseHeightString, font.Size));
        }
        private static float GetDescenderHeight(iTextSharp.text.Font font)
        {
            return getC(-font.BaseFont.GetDescentPoint(baseHeightString, font.Size));
        }

        private static float GetStringWidth(string value, iTextSharp.text.Font font)
        {
            return getC(font.BaseFont.GetWidthPoint(value, font.Size));
        }

        private static float getP(float cm)
        {
            //float a = (cm * 72f) / 2.54f;
            return (cm * 72f) / 2.54f;
        }
        private static float getC(float p)
        {
            return (p * 2.54f) / 72f;
        }

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
