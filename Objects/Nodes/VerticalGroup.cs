using System.Collections.Generic;

namespace DeadDog.PDF
{
    public class VerticalGroup : VerticalGroup<PDFObject>
    {
        public VerticalGroup()
            : base()
        {
        }
        public VerticalGroup(Vector1D spacer, params PDFObject[] objects)
            : base(spacer, objects)
        {
        }
        public VerticalGroup(Vector1D spacer)
            : base(spacer)
        {
        }
    }

    public class VerticalGroup<T> : PDFGroup<T> where T : PDFObject
    {
        private Vector1D spacer;
        private bool useHeight;
        private HorizontalAlignment alignment;

        private List<T> objects;

        public VerticalGroup()
            : this(Vector1D.Zero)
        {
        }
        public VerticalGroup(Vector1D spacer)
            : this(spacer, new T[0])
        {
        }
        public VerticalGroup(Vector1D spacer, params T[] objects)
            : base(Vector2D.Zero, Vector2D.Zero)
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

        public Vector1D Spacer
        {
            get { return spacer; }
            set { spacer = value; }
        }
        public bool UseHeight
        {
            get { return useHeight; }
            set { useHeight = value; }
        }
        public HorizontalAlignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        protected override Vector2D getSize()
        {
            if (objects.Count == 0)
                return Vector2D.Zero;

            Vector2D size = objects[0].Size;
            for (int i = 1; i < objects.Count; i++)
            {
                if (objects[i].Size.X > size.X) size.X = objects[i].Size.X;
                if (useHeight)
                    size.Y += objects[i].Size.Y + spacer;
                else
                    size.Y += spacer;
            }

            return size;
        }

        protected internal override IEnumerable<T> GetPDFObjects()
        {
            foreach (T obj in objects)
                yield return obj;
        }

        protected internal override Vector2D GetGroupingOffset(T obj)
        {
            return getLocation(objects.IndexOf(obj));
        }
        private Vector2D getLocation(int index)
        {
            var width = this.Size.X;

            Vector2D p = Vector2D.Zero;

            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    //Do nothing because p.X == this.X
                    break;
                case HorizontalAlignment.Center:
                    p.X = (width - objects[index].Size.X) / 2;
                    break;
                case HorizontalAlignment.Right:
                    p.X = width - objects[index].Size.X;
                    break;
            }
            p.Y = spacer * index;
            if (useHeight)
                for (int i = 0; i < index; i++)
                    p.Y += objects[i].Size.Y;
            return p;
        }
    }
}
