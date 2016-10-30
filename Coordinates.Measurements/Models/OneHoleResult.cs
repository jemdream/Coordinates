namespace Coordinates.Measurements.Models
{
    public class OneHoleResult : ICalculationResult
    {
        public double Result { get; set; }
        public override string ToString() => $"{Result}";
    }
}
