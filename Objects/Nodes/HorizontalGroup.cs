using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    public class HorizontalGroup<T> : PDFGroup<T> where T : PDFObject
    {
        private float spacer;
        private bool useWidth;
        private VerticalAlignment alignment;

        public HorizontalGroup()
            : this(0f)
        { }
        public HorizontalGroup(float spacer, params T[] objects)
            : this(spacer)
        {
            this.list.AddRange(objects);
        }
        public HorizontalGroup(float spacer)
            : base(false)
        {
            this.spacer = spacer;
            useWidth = true;
            alignment = VerticalAlignment.Middle;
        }

        public float Spacer
        {
            get { return spacer; }
            set { spacer = value; }
        }
        public bool UseWidth
        {
            get { return useWidth; }
            set { useWidth = false; }
        }
        public VerticalAlignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        protected override SizeF getSize()
        {
            if (Objects.Count == 0)
                return SizeF.Empty;

            SizeF size = Objects[0].Size;
            for (int i = 1; i < Objects.Count; i++)
            {
                if (Objects[i].Height > size.Height) size.Height = Objects[i].Height;
                if (useWidth)
                    size.Width += Objects[i].Width + spacer;
                else
                    size.Width += spacer;
            }

            return base.getSize();
        }

        protected internal override PointF GetGroupingOffset(T obj)
        {
            return getLocation(Objects.IndexOf(obj));
        }
        private PointF getLocation(int index)
        {
            System.Drawing.PointF p = this.Location;
            switch (alignment)
            {
                case VerticalAlignment.Top:
                    //Do nothing because p.Y == this.Y
                    break;
                case VerticalAlignment.Middle:
                    p.Y += (this.Height - list[index].Height) / 2f;
                    break;
                case VerticalAlignment.Bottom:
                    p.Y += this.Height - list[index].Height;
                    break;
            }
            p.X += spacer * index;
            if (useWidth)
                for (int i = 0; i < index; i++)
                    p.X += list[i].Width;
            return p;
        }
    }

    public class HorizontalGroup : HorizontalGroup<PDFObject>
    {
        public HorizontalGroup()
            : base()
        { }
        public HorizontalGroup(float spacer, params PDFObject[] objects)
            : base(spacer, objects)
        { }
        public HorizontalGroup(float spacer)
            : base(spacer)
        { }
    }
}
