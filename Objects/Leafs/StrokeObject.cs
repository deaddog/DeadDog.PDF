using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DeadDog.PDF
{
    public abstract class StrokeObject : PDFObject
    {
        public StrokeObject(bool canResize) : base(canResize)
        {
        }

        public StrokeObject(bool canResize, RectangleF rectangle) : base(canResize, rectangle)
        {
        }
    }
}
