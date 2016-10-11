namespace Coordinates.ExternalDevices.Models
{
    public class GaugePositionDTO
    {
        public GaugePositionDTO(double x, double y, double z, bool contact = false)
        {
            X = x;
            Y = y;
            Z = z;
            Contact = contact;
        }

        public double X { get; }
        public double Z { get; }
        public double Y { get; }
        public bool Contact { get; }

        public static GaugePositionDTO Default => new GaugePositionDTO(0, 0, 0);
    }
}
