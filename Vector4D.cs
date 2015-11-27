namespace DeadDog.PDF
{
    public struct Vector4D
    {
        private Vector2D offset, size;

        private static Vector4D zero = new Vector4D(0, 0, 0, 0, default(UnitsOfMeasure));
        public static Vector4D Zero => zero;

        public Vector4D(double x, double y, double width, double height, UnitsOfMeasure unit)
        {
            this.offset = new PDF.Vector2D(x, y, unit);
            this.size = new PDF.Vector2D(width, height, unit);
        }
        public Vector4D(Vector1D x, Vector1D y, Vector1D width,Vector1D height)
        {
            this.offset = new Vector2D(x, y);
            this.size = new Vector2D(width, height);
        }
        public Vector4D(Vector2D offset, Vector2D size)
        {
            this.offset = offset;
            this.size = size;
        }

        public Vector2D Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        public Vector1D X
        {
            get { return offset.X; }
            set { offset.X = value; }
        }
        public Vector1D Y
        {
            get { return offset.Y; }
            set { offset.Y = value; }
        }

        public Vector2D Size
        {
            get { return size; }
            set { size = value; }
        }
        public Vector1D Width
        {
            get { return size.X; }
            set { size.X = value; }
        }
        public Vector1D Height
        {
            get { return size.Y; }
            set { size.Y = value; }
        }

        public Vector1D Top => Y;
        public Vector1D Left => X;
        public Vector1D Bottom => Y + Height;
        public Vector1D Right => X + Width;

        public static Vector4D operator *(Vector4D v, double s)
        {
            return new Vector4D(v.offset * s, v.size * s);
        }
        public static Vector4D operator *(double s, Vector4D v)
        {
            return v * s;
        }

        public static Vector4D operator /(Vector4D v, double s)
        {
            return new Vector4D(v.offset / s, v.size / s);
        }

        public override string ToString() => $"{{x: {offset.X}, y: {offset.Y}, width: {size.X}, height: {size.Y}}}";
    }
}
