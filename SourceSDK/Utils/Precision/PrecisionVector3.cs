using System;
using System.Globalization;

namespace SourceSDK.Utils.Precision
{
    /// <summary>
    /// A 3-dimensional immutable vector that uses high-precision value types.
    /// </summary>
    [Serializable]
    public struct PrecisionVector3
    {
        public static readonly PrecisionVector3 MaxValue = new PrecisionVector3(double.MaxValue, double.MaxValue, double.MaxValue);
        public static readonly PrecisionVector3 MinValue = new PrecisionVector3(double.MinValue, double.MinValue, double.MinValue);
        public static readonly PrecisionVector3 Zero = new PrecisionVector3(0, 0, 0);
        public static readonly PrecisionVector3 One = new PrecisionVector3(1, 1, 1);
        public static readonly PrecisionVector3 UnitX = new PrecisionVector3(1, 0, 0);
        public static readonly PrecisionVector3 UnitY = new PrecisionVector3(0, 1, 0);
        public static readonly PrecisionVector3 UnitZ = new PrecisionVector3(0, 0, 1);

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public PrecisionVector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool EquivalentTo(PrecisionVector3 test, double delta = 0.0001d)
        {
            var xd = Math.Abs(X - test.X);
            var yd = Math.Abs(Y - test.Y);
            var zd = Math.Abs(Z - test.Z);
            return xd < delta && yd < delta && zd < delta;
        }

        public double Dot(PrecisionVector3 c)
        {
            return X * c.X + Y * c.Y + Z * c.Z;
        }

        public PrecisionVector3 Cross(PrecisionVector3 that)
        {
            var xv = Y * that.Z - Z * that.Y;
            var yv = Z * that.X - X * that.Z;
            var zv = X * that.Y - Y * that.X;
            return new PrecisionVector3(xv, yv, zv);
        }

        public PrecisionVector3 Round(int num = 8)
        {
            return new PrecisionVector3(Math.Round(X, num), Math.Round(Y, num), Math.Round(Z, num));
        }

        public PrecisionVector3 Snap(double snapTo)
        {
            return new PrecisionVector3(
                Math.Round(X / snapTo) * snapTo,
                Math.Round(Y / snapTo) * snapTo,
                Math.Round(Z / snapTo) * snapTo
            );
        }

        public double Length()
        {
            return (double)Math.Sqrt((double)LengthSquared());
        }

        public double LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public PrecisionVector3 Normalise()
        {
            var len = Length();
            return Math.Abs(len) < 0.0001 ? new PrecisionVector3(0, 0, 0) : new PrecisionVector3(X / len, Y / len, Z / len);
        }

        public PrecisionVector3 Absolute()
        {
            return new PrecisionVector3(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
        }

        public static PrecisionVector3 operator +(PrecisionVector3 c1, PrecisionVector3 c2)
        {
            return new PrecisionVector3(c1.X + c2.X, c1.Y + c2.Y, c1.Z + c2.Z);
        }

        public static PrecisionVector3 operator -(PrecisionVector3 c1, PrecisionVector3 c2)
        {
            return new PrecisionVector3(c1.X - c2.X, c1.Y - c2.Y, c1.Z - c2.Z);
        }

        public static PrecisionVector3 operator -(PrecisionVector3 c1)
        {
            return new PrecisionVector3(-c1.X, -c1.Y, -c1.Z);
        }

        public static PrecisionVector3 operator /(PrecisionVector3 c, double f)
        {
            return Math.Abs(f) < 0.0001 ? new PrecisionVector3(0, 0, 0) : new PrecisionVector3(c.X / f, c.Y / f, c.Z / f);
        }

        public static PrecisionVector3 operator *(PrecisionVector3 c, double f)
        {
            return new PrecisionVector3(c.X * f, c.Y * f, c.Z * f);
        }

        public static PrecisionVector3 operator *(PrecisionVector3 c, PrecisionVector3 f)
        {
            return new PrecisionVector3(c.X * f.X, c.Y * f.Y, c.Z * f.Z);
        }

        public static PrecisionVector3 operator /(PrecisionVector3 c, PrecisionVector3 f)
        {
            return new PrecisionVector3(c.X / f.X, c.Y / f.Y, c.Z / f.Z);
        }

        public static PrecisionVector3 operator *(double f, PrecisionVector3 c)
        {
            return c * f;
        }

        public bool Equals(PrecisionVector3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is PrecisionVector3 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = hashCode * 397 ^ Y.GetHashCode();
                hashCode = hashCode * 397 ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(PrecisionVector3 left, PrecisionVector3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PrecisionVector3 left, PrecisionVector3 right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return "(" + X.ToString("0.0000", CultureInfo.InvariantCulture) + " " + Y.ToString("0.0000", CultureInfo.InvariantCulture) + " " + Z.ToString("0.0000", CultureInfo.InvariantCulture) + ")";
        }

        public PrecisionVector3 Clone()
        {
            return new PrecisionVector3(X, Y, Z);
        }

        public static PrecisionVector3 Parse(string x, string y, string z)
        {
            const NumberStyles ns = NumberStyles.Float;
            return new PrecisionVector3(double.Parse(x, ns, CultureInfo.InvariantCulture), double.Parse(y, ns, CultureInfo.InvariantCulture), double.Parse(z, ns, CultureInfo.InvariantCulture));
        }

        public System.Numerics.Vector3 ToStandardVector3()
        {
            const int rounding = 2;
            return new System.Numerics.Vector3((float)Math.Round(X, rounding), (float)Math.Round(Y, rounding), (float)Math.Round(Z, rounding));
        }
    }
}