namespace Coordinates.Measurements.Models
{
    public class SurfacePerpendicularityResult : ICalculationResult
    {
        public object Result { get; set; }
        public override string ToString() => $"{Result}";
    }
}
