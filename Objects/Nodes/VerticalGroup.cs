using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    public class VerticalGroup<T> : PDFGroup<T> where T : IPDFObject
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
            : base()
        {
            this.spacer = spacer;
            useHeight = true;
            alignment = HorizontalAlignment.Center;
        }

        public PDFList<T> Objects
        {
            get { return list; }
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

        public override float Width
        {
            get
            {
                if (list.Count == 0)
                    return 0;
                float w = list[0].Width;
                for (int i = 1; i < list.Count; i++)
                    w = list[i].Width > w ? list[i].Width : w;
                return w;
            }
        }
        public override float Height
        {
            get
            {
                if (list.Count == 0)
                    return 0;
                float h = list[0].Height;
                for (int i = 1; i < list.Count; i++)
                    if (useHeight)
                        h += list[i].Height + spacer;
                    else
                        h += spacer;
                return h;
            }
        }

        public override System.Drawing.PointF GetLocation(T obj)
        {
            return GetLocation(list.IndexOf(obj));
        }
        public System.Drawing.PointF GetLocation(int index)
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

    public class VerticalGroup : VerticalGroup<IPDFObject>
    {
        public VerticalGroup()
            : base()
        { }
        public VerticalGroup(float spacer, params IPDFObject[] objects)
            : base(spacer, objects)
        { }
        public VerticalGroup(float spacer)
            : base(spacer)
        { }
    }
}
