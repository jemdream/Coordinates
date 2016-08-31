namespace Coordinates.Models.DTO
{
    public class ContactPosition : Position
    {
        public ContactPosition() { }
        public ContactPosition(double x, double y, double z) : base(x, y, z) { }
        public static ContactPosition Default => new ContactPosition();
    }
}
