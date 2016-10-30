namespace Coordinates.Measurements.Models
{
    public class SurfaceResult : ICalculationResult
    {
        public double A0 { get; set; }
        public double A1 { get; set; }
        public double A2 { get; set; }
        public override string ToString() => $"A1 = {A0}, A2 = {A1}, A3 = {A2}";
    }
}
