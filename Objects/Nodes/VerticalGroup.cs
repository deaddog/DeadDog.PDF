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

        private List<T> objects;

        public VerticalGroup()
            : this(0f)
        { }
        public VerticalGroup(float spacer)
            : this(spacer, new T[0])
        {
        }
        public VerticalGroup(float spacer, params T[] objects)
            : base(false)
        {
            this.spacer = spacer;
            useHeight = true;
            alignment = HorizontalAlignment.Center;

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
            if (objects.Count == 0)
                return SizeF.Empty;

            SizeF size = objects[0].Size;
            for (int i = 1; i < objects.Count; i++)
            {
                if (objects[i].Width > size.Width) size.Width = objects[i].Width;
                if (useHeight)
                    size.Height += objects[i].Height + spacer;
                else
                    size.Height += spacer;
            }

            return base.getSize();
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
            float width = this.Width;
            PointF p = PointF.Empty;

            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    //Do nothing because p.X == this.X
                    break;
                case HorizontalAlignment.Center:
                    p.X = (width - objects[index].Width) / 2f;
                    break;
                case HorizontalAlignment.Right:
                    p.X = width - objects[index].Width;
                    break;
            }
            p.Y = spacer * index;
            if (useHeight)
                for (int i = 0; i < index; i++)
                    p.Y += objects[i].Height;
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
