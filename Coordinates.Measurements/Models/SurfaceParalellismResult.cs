namespace Coordinates.Measurements.Models
{
    public class SurfaceParalellismResult : ICalculationResult
    {
        public object Result { get; set; }
        public override string ToString() => $"Result = {Result}";
    }
}
