using System;

namespace DeadDog.PDF
{
    public struct Vector1D : IEquatable<Vector1D>, IComparable<Vector1D>
    {
        private double value;
        private UnitsOfMeasure unit;

        private static Vector1D zero = new Vector1D(0, default(UnitsOfMeasure));
        public static Vector1D Zero => zero;

        public Vector1D(double value, UnitsOfMeasure unit)
        {
            this.value = value;
            this.unit = unit;
        }

        public double Value(UnitsOfMeasure unit) => Convert(value, this.unit, unit);
        public Vector1D ToUnit(UnitsOfMeasure unit)
        {
            return new Vector1D(Convert(value, this.unit, unit), unit);
        }

        public UnitsOfMeasure Unit => unit;

        public static double Convert(double value, UnitsOfMeasure from, UnitsOfMeasure to)
        {
            if (from == to)
                return value;

            switch (from)
            {
                case UnitsOfMeasure.Points:
                    switch (to)
                    {
                        case UnitsOfMeasure.Centimeters: return (value * 2.54) / 72.0;
                        case UnitsOfMeasure.Inches: return value / 72.0;
                    }
                    break;
                case UnitsOfMeasure.Centimeters:
                    switch (to)
                    {
                        case UnitsOfMeasure.Inches: return value / 2.54;
                        case UnitsOfMeasure.Points: return (value * 72.0) / 2.54;
                    }
                    break;
                case UnitsOfMeasure.Inches:
                    switch (to)
                    {
                        case UnitsOfMeasure.Centimeters: return value * 2.54;
                        case UnitsOfMeasure.Points: return value * 72;
                    }
                    break;
            }

            throw new InvalidOperationException("Unknown unit of measure.");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            else if (obj.GetType() == typeof(Vector1D))
                return Equals((Vector1D)obj);
            else
                return false;
        }
        public bool Equals(Vector1D obj)
        {
            return this == obj;
        }
        public override int GetHashCode()
        {
            return value.GetHashCode() ^ unit.GetHashCode();
        }

        public static bool operator ==(Vector1D v1, Vector1D v2)
        {
            if (v1.unit == v2.unit)
                return v1.value == v2.value;
            else
                return v1.value == v2.Value(v1.unit);
        }
        public static bool operator !=(Vector1D v1, Vector1D v2)
        {
            if (v1.unit == v2.unit)
                return v1.value != v2.value;
            else
                return v1.value != v2.Value(v1.unit);
        }
        public static bool operator <(Vector1D v1, Vector1D v2)
        {
            if (v1.unit == v2.unit)
                return v1.value < v2.value;
            else
                return v1.value < v2.Value(v1.unit);
        }
        public static bool operator >(Vector1D v1, Vector1D v2)
        {
            if (v1.unit == v2.unit)
                return v1.value > v2.value;
            else
                return v1.value > v2.Value(v1.unit);
        }
        public static bool operator <=(Vector1D v1, Vector1D v2)
        {
            if (v1.unit == v2.unit)
                return v1.value <= v2.value;
            else
                return v1.value <= v2.Value(v1.unit);
        }
        public static bool operator >=(Vector1D v1, Vector1D v2)
        {
            if (v1.unit == v2.unit)
                return v1.value >= v2.value;
            else
                return v1.value >= v2.Value(v1.unit);
        }

        public int CompareTo(Vector1D obj)
        {
            obj = obj.ToUnit(unit);
            return this.value.CompareTo(obj.value);
        }

        public static Vector1D operator +(Vector1D v1, Vector1D v2)
        {
            if (v1.unit == v2.unit)
                return new Vector1D(v1.value + v2.value, v1.unit);
            else
                return v1 + v2.ToUnit(v1.unit);
        }
        public static Vector1D operator -(Vector1D v1, Vector1D v2)
        {
            if (v1.unit == v2.unit)
                return new Vector1D(v1.value - v2.value, v1.unit);
            else
                return v1 - v2.ToUnit(v1.unit);
        }
        public static Vector1D operator *(Vector1D v1, Vector1D v2)
        {
            if (v1.unit == v2.unit)
                return new Vector1D(v1.value * v2.value, v1.unit);
            else
                return v1 * v2.ToUnit(v1.unit);
        }
        public static Vector1D operator /(Vector1D v1, Vector1D v2)
        {
            if (v1.unit == v2.unit)
                return new Vector1D(v1.value / v2.value, v1.unit);
            else
                return v1 / v2.ToUnit(v1.unit);
        }

        public static Vector1D operator *(Vector1D v, double s)
        {
            return new Vector1D(v.value * s, v.unit);
        }
        public static Vector1D operator *(double s, Vector1D v)
        {
            return v * s;
        }

        public static Vector1D operator /(Vector1D v, double s)
        {
            return new Vector1D(v.value / s, v.unit);
        }

        public override string ToString() => $"{{{value} {unit}}}";
    }
}
