namespace Coordinates.Models.DTO
{
    public class GaugePosition : Position
    {
        public GaugePosition() { }
        public GaugePosition(double x, double y, double z) : base(x, y, z) { }
        public static GaugePosition Default => new GaugePosition();
    }
}
