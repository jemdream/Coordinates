using System;
using Newtonsoft.Json;

namespace Coordinates.Models.DTO
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Position
    {
        private readonly int _id;

        public Position() : this(0.0, 0.0, 0.0, false, false) { }
        public Position(double x, double y, double z, bool contact) : this(x, y, z, contact, false) { }
        public Position(double x, double y, double z, bool contact, bool firstContact)
        {
            X = x;
            Y = y;
            Z = z;
            Contact = contact;
            FirstContact = firstContact;
            _id = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
        }

        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        [JsonProperty]
        public double X { get; }
        [JsonProperty]
        public double Z { get; }
        [JsonProperty]
        public double Y { get; }
        public bool Contact { get; }
        public bool FirstContact { get; }

        public static Position Default => new Position();
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var fooItem = obj as Position;

            return fooItem != null && fooItem._id == _id;
        }
        public override int GetHashCode() => _id;
        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }
    }
}