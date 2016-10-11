namespace Coordinates.Measurements.Models
{
    public class SurfacePerpendicularityResult : ICalculationResult
    {
        public string Result { get; set; }
        public override string ToString() => $"Result = {Result}";
    }
}
