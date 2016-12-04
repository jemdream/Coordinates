namespace Coordinates.Measurements.Models
{
    public class SurfaceParalellismResult : ICalculationResult
    {
        public double Result { get; set; }
        public override string ToString() => $"ψ = {Result} [rad]";
    }
}