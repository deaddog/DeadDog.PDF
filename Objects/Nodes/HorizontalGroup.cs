using System;
using System.Collections.Generic;
using System.Text;

namespace DeadDog.PDF
{
    public class HorizontalGroup<T> : PDFGroup<T> where T : IPDFObject
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
            : base()
        {
            this.spacer = spacer;
            useWidth = true;
            alignment = VerticalAlignment.Middle;
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

        public override float Width
        {
            get
            {
                if (list.Count == 0)
                    return 0;
                float w = list[0].Width;
                for (int i = 1; i < list.Count; i++)
                    if (useWidth)
                        w += list[i].Width + spacer;
                    else
                        w += spacer;
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
                    h = list[i].Height > h ? list[i].Height : h;
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

    public class HorizontalGroup : HorizontalGroup<IPDFObject>
    {
        public HorizontalGroup()
            : base()
        { }
        public HorizontalGroup(float spacer, params IPDFObject[] objects)
            : base(spacer, objects)
        { }
        public HorizontalGroup(float spacer)
            : base(spacer)
        { }
    }
}
