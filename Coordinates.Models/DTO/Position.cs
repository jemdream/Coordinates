using System;

namespace Coordinates.Models.DTO
{
    public abstract class Position
    {
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        public double X { get; }
        public double Z { get; }
        public double Y { get; }

        protected Position() : this(0.0, 0.0, 0.0) { }

        protected Position(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        //public override bool Equals(object obj)
        //{
        //    var objAsPosition = obj as Position;

        //    if (objAsPosition == null)
        //        return false;

        //    return
        //        (Math.Abs(X - objAsPosition.X) < double.Epsilon) &&
        //        (Math.Abs(Y - objAsPosition.Y) < double.Epsilon) &&
        //        (Math.Abs(Z - objAsPosition.Z) < double.Epsilon);
        //}

        //public override int GetHashCode()
        //{
        //    return Convert.ToInt32(X * Y * Z);
        //}
    }
}
