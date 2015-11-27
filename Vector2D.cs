using System;

namespace DeadDog.PDF
{
    public struct Vector2D : IEquatable<Vector2D>
    {
        private Vector1D x, y;

        private static Vector2D zero = new Vector2D(0, 0, default(UnitsOfMeasure));
        public static Vector2D Zero => zero;

        public Vector2D(double x, double y, UnitsOfMeasure unit)
        {
            this.x = new Vector1D(x, unit);
            this.y = new Vector1D(y, unit);
        }
        public Vector2D(Vector1D x, Vector1D y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector1D X
        {
            get { return x; }
            set { x = value; }
        }
        public Vector1D Y
        {
            get { return y; }
            set { y = value; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            else if (obj.GetType() == typeof(Vector2D))
                return Equals((Vector2D)obj);
            else
                return false;
        }
        public bool Equals(Vector2D obj)
        {
            return this == obj;
        }
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public static bool operator ==(Vector2D v1, Vector2D v2)
        {
            return v1.x == v2.x && v1.y == v2.y;
        }
        public static bool operator !=(Vector2D v1, Vector2D v2)
        {
            return v1.x != v2.x || v1.y != v2.y;
        }

        public static Vector2D operator -(Vector2D v)
        {
            return new Vector2D(-v.x, -v.y);
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.x + v2.x, v1.y + v2.y);
        }
        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2D operator *(Vector2D v, double s)
        {
            return new Vector2D(v.x * s, v.y * s);
        }
        public static Vector2D operator *(double s, Vector2D v)
        {
            return v * s;
        }

        public static Vector2D operator /(Vector2D v, double s)
        {
            return new Vector2D(v.x / s, v.y / s);
        }

        public override string ToString() => $"{{x: {x}, y: {y}}}";
    }
}
