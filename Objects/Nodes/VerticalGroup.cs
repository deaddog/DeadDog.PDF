using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DeadDog.PDF
{
    public class VerticalGroup<T> : PDFGroup<T> where T : PDFObject
    {
        private float spacer = 0f;
        private bool useHeight = true;
        private HorizontalAlignment alignment;
        
        public VerticalGroup()
            : this(0f)
        { }
        public VerticalGroup(float spacer, params T[] objects)
            : this(spacer)
        {
            this.list.AddRange(objects);
        }
        public VerticalGroup(float spacer)
            : base(false)
        {
            this.spacer = spacer;
            useHeight = true;
            alignment = HorizontalAlignment.Center;
        }

        public float Spacer
        {
            get { return spacer; }
            set { spacer = value; }
        }
        public bool UseHeight
        {
            get { return useHeight; }
            set { useHeight = false; }
        }
        public HorizontalAlignment Alignment
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
                if (Objects[i].Width > size.Width) size.Width = Objects[i].Width;
                if (useHeight)
                    size.Height += Objects[i].Height + spacer;
                else
                    size.Height += spacer;
            }

            return base.getSize();
        }

        protected override PointF GetGroupingOffset(T obj)
        {
            return getLocation(Objects.IndexOf(obj));
        }
        private PointF getLocation(int index)
        {
            System.Drawing.PointF p = this.Location;
            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    //Do nothing because p.X == this.X
                    break;
                case HorizontalAlignment.Center:
                    p.X += (this.Width - list[index].Width) / 2f;
                    break;
                case HorizontalAlignment.Right:
                    p.X += this.Width - list[index].Width;
                    break;
            }
            p.Y += spacer * index;
            if (useHeight)
                for (int i = 0; i < index; i++)
                    p.Y += list[i].Height;
            return p;
        }
    }

    public class VerticalGroup : VerticalGroup<PDFObject>
    {
        public VerticalGroup()
            : base()
        { }
        public VerticalGroup(float spacer, params PDFObject[] objects)
            : base(spacer, objects)
        { }
        public VerticalGroup(float spacer)
            : base(spacer)
        { }
    }
}
