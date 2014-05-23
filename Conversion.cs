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
        public static float ToPoints(this float cm)
        {
            //float a = (cm * 72f) / 2.54f;
            return (cm * 72f) / 2.54f;
        }
        /// <summary>
        /// Converts a number from points to centimeters.
        /// </summary>
        /// <param name="p">The value to convert.</param>
        /// <returns>The value converted to centimeters.</returns>
        public static float ToCentimeters(this float p)
        {
            return (p * 2.54f) / 72f;
        }

        /// <summary>
        /// Converts a <see cref="PointF"/> from centimeters to points.
        /// </summary>
        /// <param name="cm">The <see cref="PointF"/> to convert.</param>
        /// <returns>The <see cref="PointF"/> converted to points.</returns>
        public static PointF ToPoints(this PointF cm)
        {
            return new PointF(ToPoints(cm.X), ToPoints(cm.Y));
        }
        /// <summary>
        /// Converts a <see cref="PointF"/> from points to centimeters.
        /// </summary>
        /// <param name="p">The <see cref="PointF"/> to convert.</param>
        /// <returns>The <see cref="PointF"/> converted to centimeters.</returns>
        public static PointF ToCentimeters(this PointF p)
        {
            return new PointF(ToCentimeters(p.X), ToCentimeters(p.Y));
        }

        /// <summary>
        /// Converts a <see cref="SizeF"/> from centimeters to points.
        /// </summary>
        /// <param name="cm">The <see cref="SizeF"/> to convert.</param>
        /// <returns>The <see cref="SizeF"/> converted to points.</returns>
        public static SizeF ToPoints(this SizeF cm)
        {
            return new SizeF(ToPoints(cm.Width), ToPoints(cm.Height));
        }
        /// <summary>
        /// Converts a <see cref="SizeF"/> from points to centimeters.
        /// </summary>
        /// <param name="p">The <see cref="SizeF"/> to convert.</param>
        /// <returns>The <see cref="SizeF"/> converted to centimeters.</returns>
        public static SizeF ToCentimeters(this SizeF p)
        {
            return new SizeF(ToCentimeters(p.Width), ToCentimeters(p.Height));
        }

        /// <summary>
        /// Converts a <see cref="RectangleF"/> from centimeters to points.
        /// </summary>
        /// <param name="cm">The <see cref="RectangleF"/> to convert.</param>
        /// <returns>The <see cref="RectangleF"/> converted to points.</returns>
        public static RectangleF ToPoints(this RectangleF cm)
        {
            return new RectangleF(ToPoints(cm.Location), ToPoints(cm.Size));
        }
        /// <summary>
        /// Converts a <see cref="RectangleF"/> from points to centimeters.
        /// </summary>
        /// <param name="p">The <see cref="RectangleF"/> to convert.</param>
        /// <returns>The <see cref="RectangleF"/> converted to centimeters.</returns>
        public static RectangleF ToCentimeters(this RectangleF p)
        {
            return new RectangleF(ToCentimeters(p.Location), ToCentimeters(p.Size));
        }
    }
}
