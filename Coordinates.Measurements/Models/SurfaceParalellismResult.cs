namespace Coordinates.Measurements.Models
{
    public class SurfaceParalellismResult : ICalculationResult
    {
        public string Result { get; set; }
        public override string ToString() => $"Result = {Result}";
    }
}
