namespace Coordinates.Measurements.Models
{
    public class HoleResult : ICalculationResult
    {
        public double R { get; set; }
        public double Z0 { get; set; }
        public double Y0 { get; set; }
        public double X0 { get; set; }
        public override string ToString() => $"R = {R}; Z0 = {Z0}; Y0 = {Y0}; X0 = {X0}";
    }
}
