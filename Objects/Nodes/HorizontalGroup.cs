using System.Collections.Generic;

namespace DeadDog.PDF
{
    public class HorizontalGroup : HorizontalGroup<PDFObject>
    {
        public HorizontalGroup()
            : base()
        {
        }
        public HorizontalGroup(Vector1D spacer, params PDFObject[] objects)
            : base(spacer, objects)
        {
        }
        public HorizontalGroup(Vector1D spacer)
            : base(spacer)
        {
        }
    }

    public class HorizontalGroup<T> : PDFGroup<T> where T : PDFObject
    {
        private Vector1D spacer;
        private bool useWidth;
        private VerticalAlignment alignment;

        private List<T> objects;

        public HorizontalGroup()
            : this(Vector1D.Zero)
        {
        }
        public HorizontalGroup(Vector1D spacer)
            : this(spacer, new T[0])
        {
        }
        public HorizontalGroup(Vector1D spacer, params T[] objects)
            : base(false, Vector2D.Zero, Vector2D.Zero)
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

        public Vector1D Spacer
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

        protected sealed override Vector2D getSize()
        {
            if (objects.Count == 0)
                return Vector2D.Zero;

            Vector2D size = objects[0].Size;
            for (int i = 1; i < objects.Count; i++)
            {
                if (objects[i].Size.Y > size.Y) size.Y = objects[i].Size.Y;
                if (useWidth)
                    size.X += objects[i].Size.X + spacer;
                else
                    size.X += spacer;
            }

            return size;
        }

        protected sealed internal override IEnumerable<T> GetPDFObjects()
        {
            foreach (T obj in objects)
                yield return obj;
        }

        protected sealed internal override Vector2D GetGroupingOffset(T obj)
        {
            return getLocation(objects.IndexOf(obj));
        }
        private Vector2D getLocation(int index)
        {
            var height = this.Size.Y;

            Vector2D p = Vector2D.Zero;

            switch (alignment)
            {
                case VerticalAlignment.Top:
                    //Do nothing because p.Y == this.Y
                    break;
                case VerticalAlignment.Middle:
                    p.Y = (height - objects[index].Size.Y) / 2;
                    break;
                case VerticalAlignment.Bottom:
                    p.Y = height - objects[index].Size.Y;
                    break;
            }
            p.X = spacer * index;
            if (useWidth)
                for (int i = 0; i < index; i++)
                    p.X += objects[i].Size.X;

            return p;
        }
    }
}
