namespace DeadDog.PDF
{
    public class Arc : FillObject
    {
        private float startangle, extent;

        public Arc()
            : this(Vector2D.Zero)
        {
        }
        public Arc(Vector2D size)
            : base(Vector2D.Zero, size)
        {
            this.startangle = 0;
            this.extent = 350;
        }

        public float StartAngle
        {
            get { return startangle; }
            set { startangle = value; }
        }
        public float Extent
        {
            get { return extent; }
            set { extent = value; }
        }

        protected override void Render(ContentWriter cw, Vector2D offset)
        {
            cw.Arc(offset, offset + Size, startangle, extent);
        }
    }
}
