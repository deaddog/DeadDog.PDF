using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    /// <summary>
    /// Implements basic functionalities for leaf objects. <see cref="LeafObject"/> and <see cref="LeafObjectSizeable"/> are the only objects that can actually be drawn.
    /// </summary>
    public abstract class LeafObject : PDFObject
    {
        internal LeafObject()
        {
        }

        /// <summary>
        /// Converts a number from centimeters to points.
        /// </summary>
        /// <param name="cm">The value to convert.</param>
        /// <returns>The value converted to points.</returns>
        protected internal static float getP(float cm)
        {
            //float a = (cm * 72f) / 2.54f;
            return (cm * 72f) / 2.54f;
        }
        /// <summary>
        /// Converts a number from points to centimeters.
        /// </summary>
        /// <param name="p">The value to convert.</param>
        /// <returns>The value converted to centimeters.</returns>
        protected internal static float getC(float p)
        {
            return (p * 2.54f) / 72f;
        }

        /// <summary>
        /// Converts a <see cref="PointF"/> from centimeters to points.
        /// </summary>
        /// <param name="cm">The <see cref="PointF"/> to convert.</param>
        /// <returns>The <see cref="PointF"/> converted to points.</returns>
        protected internal static PointF getP(PointF cm)
        {
            return new PointF(getP(cm.X), getP(cm.Y));
        }
        /// <summary>
        /// Converts a <see cref="PointF"/> from points to centimeters.
        /// </summary>
        /// <param name="p">The <see cref="PointF"/> to convert.</param>
        /// <returns>The <see cref="PointF"/> converted to centimeters.</returns>
        protected internal static PointF getC(PointF p)
        {
            return new PointF(getC(p.X), getC(p.Y));
        }

        /// <summary>
        /// Converts a <see cref="SizeF"/> from centimeters to points.
        /// </summary>
        /// <param name="cm">The <see cref="SizeF"/> to convert.</param>
        /// <returns>The <see cref="SizeF"/> converted to points.</returns>
        protected internal static SizeF getP(SizeF cm)
        {
            return new SizeF(getP(cm.Width), getP(cm.Height));
        }
        /// <summary>
        /// Converts a <see cref="SizeF"/> from points to centimeters.
        /// </summary>
        /// <param name="p">The <see cref="SizeF"/> to convert.</param>
        /// <returns>The <see cref="SizeF"/> converted to centimeters.</returns>
        protected internal static SizeF getC(SizeF p)
        {
            return new SizeF(getC(p.Width), getC(p.Height));
        }

        /// <summary>
        /// Converts a <see cref="RectangleF"/> from centimeters to points.
        /// </summary>
        /// <param name="cm">The <see cref="RectangleF"/> to convert.</param>
        /// <returns>The <see cref="RectangleF"/> converted to points.</returns>
        protected internal static RectangleF getP(RectangleF cm)
        {
            return new RectangleF(getP(cm.Location), getP(cm.Size));
        }
        /// <summary>
        /// Converts a <see cref="RectangleF"/> from points to centimeters.
        /// </summary>
        /// <param name="p">The <see cref="RectangleF"/> to convert.</param>
        /// <returns>The <see cref="RectangleF"/> converted to centimeters.</returns>
        protected internal static RectangleF getC(RectangleF p)
        {
            return new RectangleF(getC(p.Location), getC(p.Size));
        }

        /// <summary>
        /// Adds this <see cref="LeafObject"/> to an object collector.
        /// </summary>
        /// <param name="collector">An <see cref="ObjectCollector"/> to which this <see cref="LeafObject"/> is added.</param>
        public sealed override void Collect(ObjectCollector collector)
        {
            collector.Add(this);
        }
    }
}
