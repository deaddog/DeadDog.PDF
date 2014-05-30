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

        private List<T> objects;

        public HorizontalGroup()
            : this(0f)
        { }
        public HorizontalGroup(float spacer)
            : this(spacer, new T[0])
        {
        }
        public HorizontalGroup(float spacer, params T[] objects)
            : base(false)
        {
            this.spacer = spacer;
            useWidth = true;
            alignment = VerticalAlignment.Middle;

            this.objects = new List<T>(objects);
        }

        public List<T> Objects
        {
            get { return objects; }
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
            if (objects.Count == 0)
                return SizeF.Empty;

            SizeF size = objects[0].Size;
            for (int i = 1; i < objects.Count; i++)
            {
                if (objects[i].Height > size.Height) size.Height = objects[i].Height;
                if (useWidth)
                    size.Width += objects[i].Width + spacer;
                else
                    size.Width += spacer;
            }

            return size;
        }

        protected internal override IEnumerable<T> GetPDFObjects()
        {
            foreach (T obj in objects)
                yield return obj;
        }

        protected internal override PointF GetGroupingOffset(T obj)
        {
            return getLocation(objects.IndexOf(obj));
        }
        private PointF getLocation(int index)
        {
            float height = this.Height;
            PointF p = PointF.Empty;

            switch (alignment)
            {
                case VerticalAlignment.Top:
                    //Do nothing because p.Y == this.Y
                    break;
                case VerticalAlignment.Middle:
                    p.Y = (height - objects[index].Height) / 2f;
                    break;
                case VerticalAlignment.Bottom:
                    p.Y = height - objects[index].Height;
                    break;
            }
            p.X = spacer * index;
            if (useWidth)
                for (int i = 0; i < index; i++)
                    p.X += objects[i].Width;
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
