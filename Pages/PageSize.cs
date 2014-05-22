using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DeadDog.PDF
{
    /// <summary>
    /// Stores the size of a piece of paper.
    /// </summary>
    public struct PageSize
    {
        #region Conversion Methods
        private static float getP(float cm)
        {
            return (cm * 72f) / 2.54f;
        }
        private static float getC(float p)
        {
            return (p * 2.54f) / 72f;
        }

        private static PointF getP(PointF cm)
        {
            return new PointF(getP(cm.X), getP(cm.Y));
        }
        private static PointF getC(PointF p)
        {
            return new PointF(getC(p.X), getC(p.Y));
        }

        private static SizeF getP(SizeF cm)
        {
            return new SizeF(getP(cm.Width), getP(cm.Height));
        }
        private static SizeF getC(SizeF p)
        {
            return new SizeF(getC(p.Width), getC(p.Height));
        }

        private static RectangleF getP(RectangleF cm)
        {
            return new RectangleF(getP(cm.Location), getP(cm.Size));
        }
        private static RectangleF getC(RectangleF p)
        {
            return new RectangleF(getC(p.Location), getC(p.Size));
        }
        #endregion

        private iTextSharp.text.Rectangle pointRect;
        private SizeF cmRect;

        private PageSize(iTextSharp.text.Rectangle pointRect)
        {

            this.pointRect = pointRect;
            this.cmRect = new SizeF(pointRect.Width, pointRect.Height);
            this.cmRect = getC(this.cmRect);
        }
        /// <summary>
        /// Initializes a new instance of the PageSize with the given size.
        /// </summary>
        /// <param name="cmRect">The size of the PageSize.</param>
        public PageSize(SizeF cmRect)
        {
            this.cmRect = cmRect;
            pointRect = new iTextSharp.text.Rectangle(0, 0, getP(cmRect.Width), getP(cmRect.Height));
        }
        private PageSize(iTextSharp.text.Rectangle pointRect, SizeF cmRect)
        {
            this.pointRect = pointRect;
            this.cmRect = cmRect;
        }

        /// <summary>
        /// Rotates the PageSize 90 degrees (switches between vertical and horizontal).
        /// </summary>
        /// <returns>A copy of this PageSize rotated 90 degrees.</returns>
        public PageSize Rotate()
        {
            return new PageSize(pointRect.Rotate(), new SizeF(cmRect.Height, cmRect.Width));
        }

        /// <summary>
        /// Gets the size of this PageSize.
        /// </summary>
        public SizeF Size
        {
            get { return cmRect; }
        }
        /// <summary>
        /// Gets the width of this PageSize.
        /// </summary>
        public float Width
        {
            get { return cmRect.Width; }
        }
        /// <summary>
        /// Gets the height of this PageSize.
        /// </summary>
        public float Height
        {
            get { return cmRect.Height; }
        }

        /// <summary>
        /// Gets the width of this PageSize measured in Points.
        /// </summary>
        public float WidthPoint
        {
            get { return pointRect.Width; }
        }
        /// <summary>
        /// Gets the height of this PageSize measured in Points.
        /// </summary>
        public float HeightPoint
        {
            get { return pointRect.Height; }
        }
        /// <summary>
        /// Gets the instance of <see cref="iTextSharp.text.Rectangle"/> used to describe this PageSize.
        /// </summary>
        public iTextSharp.text.Rectangle SizePoint
        {
            get { return pointRect; }
        }

        #region Static sizes
        /// <summary>
        /// A standard A0 PageSize in vertical orientation.
        /// </summary>
        public static PageSize A0 = new PageSize(iTextSharp.text.PageSize.A0, new SizeF(84.1f, 118.9f));
        /// <summary>
        /// A standard A1 PageSize in vertical orientation.
        /// </summary>
        public static PageSize A1 = new PageSize(iTextSharp.text.PageSize.A1, new SizeF(59.4f, 84.1f));
        /// <summary>
        /// A standard A2 PageSize in vertical orientation.
        /// </summary>
        public static PageSize A2 = new PageSize(iTextSharp.text.PageSize.A2, new SizeF(42, 59.4f));
        /// <summary>
        /// A standard A3 PageSize in vertical orientation.
        /// </summary>
        public static PageSize A3 = new PageSize(iTextSharp.text.PageSize.A3, new SizeF(29.7f, 42));
        /// <summary>
        /// A standard A4 PageSize in vertical orientation.
        /// </summary>
        public static PageSize A4 = new PageSize(iTextSharp.text.PageSize.A4, new SizeF(21, 29.7f));

        /// <summary>
        /// A standard A5 PageSize in vertical orientation.
        /// </summary>
        public static PageSize A5 = new PageSize(iTextSharp.text.PageSize.A5, new SizeF(14.8f, 21));
        /// <summary>
        /// A standard A6 PageSize in vertical orientation.
        /// </summary>
        public static PageSize A6 = new PageSize(iTextSharp.text.PageSize.A6, new SizeF(10.5f, 14.8f));
        /// <summary>
        /// A standard A7 PageSize in vertical orientation.
        /// </summary>
        public static PageSize A7 = new PageSize(iTextSharp.text.PageSize.A7, new SizeF(0.74f, 10.5f));
        /// <summary>
        /// A standard A8 PageSize in vertical orientation.
        /// </summary>
        public static PageSize A8 = new PageSize(iTextSharp.text.PageSize.A8, new SizeF(0.52f, 0.74f));
        /// <summary>
        /// A standard A9 PageSize in vertical orientation.
        /// </summary>
        public static PageSize A9 = new PageSize(iTextSharp.text.PageSize.A9, new SizeF(0.37f, 0.52f));
        /// <summary>
        /// A standard A10 PageSize in vertical orientation.
        /// </summary>
        public static PageSize A10 = new PageSize(iTextSharp.text.PageSize.A10, new SizeF(0.26f, 0.37f));
        #endregion
    }

}
