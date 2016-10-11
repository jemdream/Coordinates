using System;

namespace Coordinates.Models.DTO
{
    public class Position
    {
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        private readonly int _id;

        public double X { get; }
        public double Z { get; }
        public double Y { get; }
        public bool Contact { get; }

        public Position() : this(0.0, 0.0, 0.0, false) { }

        public Position(double x, double y, double z, bool contact)
        {
            X = x;
            Y = y;
            Z = z;
            Contact = contact;
            _id = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
        }

        public static Position Default => new Position();

        public override bool Equals(object obj)
        {
            //var objAsPosition = obj as Position;

            //if (objAsPosition == null)
            //    return false;

            //return
            //    (Math.Abs(X - objAsPosition.X) < double.Epsilon) &&
            //    (Math.Abs(Y - objAsPosition.Y) < double.Epsilon) &&
            //    (Math.Abs(Z - objAsPosition.Z) < double.Epsilon);

            if (obj == null || GetType() != obj.GetType())
                return false;

            var fooItem = obj as Position;

            return fooItem != null && fooItem._id == _id;
        }

        public override int GetHashCode()
        {
            return _id;
        }
    }
}
