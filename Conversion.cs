using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Provides methods for converting between points and centimeters.
    /// </summary>
    internal static class Conversion
    {
        /// <summary>
        /// Converts a number from centimeters to points.
        /// </summary>
        /// <param name="cm">The value to convert.</param>
        /// <returns>The value converted to points.</returns>
        public static float getP(this float cm)
        {
            //float a = (cm * 72f) / 2.54f;
            return (cm * 72f) / 2.54f;
        }
        /// <summary>
        /// Converts a number from points to centimeters.
        /// </summary>
        /// <param name="p">The value to convert.</param>
        /// <returns>The value converted to centimeters.</returns>
        public static float getC(this float p)
        {
            return (p * 2.54f) / 72f;
        }

        /// <summary>
        /// Converts a <see cref="PointF"/> from centimeters to points.
        /// </summary>
        /// <param name="cm">The <see cref="PointF"/> to convert.</param>
        /// <returns>The <see cref="PointF"/> converted to points.</returns>
        public static PointF getP(this PointF cm)
        {
            return new PointF(getP(cm.X), getP(cm.Y));
        }
        /// <summary>
        /// Converts a <see cref="PointF"/> from points to centimeters.
        /// </summary>
        /// <param name="p">The <see cref="PointF"/> to convert.</param>
        /// <returns>The <see cref="PointF"/> converted to centimeters.</returns>
        public static PointF getC(this PointF p)
        {
            return new PointF(getC(p.X), getC(p.Y));
        }

        /// <summary>
        /// Converts a <see cref="SizeF"/> from centimeters to points.
        /// </summary>
        /// <param name="cm">The <see cref="SizeF"/> to convert.</param>
        /// <returns>The <see cref="SizeF"/> converted to points.</returns>
        public static SizeF getP(this SizeF cm)
        {
            return new SizeF(getP(cm.Width), getP(cm.Height));
        }
        /// <summary>
        /// Converts a <see cref="SizeF"/> from points to centimeters.
        /// </summary>
        /// <param name="p">The <see cref="SizeF"/> to convert.</param>
        /// <returns>The <see cref="SizeF"/> converted to centimeters.</returns>
        public static SizeF getC(this SizeF p)
        {
            return new SizeF(getC(p.Width), getC(p.Height));
        }

        /// <summary>
        /// Converts a <see cref="RectangleF"/> from centimeters to points.
        /// </summary>
        /// <param name="cm">The <see cref="RectangleF"/> to convert.</param>
        /// <returns>The <see cref="RectangleF"/> converted to points.</returns>
        public static RectangleF getP(this RectangleF cm)
        {
            return new RectangleF(getP(cm.Location), getP(cm.Size));
        }
        /// <summary>
        /// Converts a <see cref="RectangleF"/> from points to centimeters.
        /// </summary>
        /// <param name="p">The <see cref="RectangleF"/> to convert.</param>
        /// <returns>The <see cref="RectangleF"/> converted to centimeters.</returns>
        public static RectangleF getC(this RectangleF p)
        {
            return new RectangleF(getC(p.Location), getC(p.Size));
        }
    }
}
