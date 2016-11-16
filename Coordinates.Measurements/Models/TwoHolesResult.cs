namespace Coordinates.Measurements.Models
{
    public class TwoHolesResult : ICalculationResult
    {
        public double Result { get; set; }
        public override string ToString() => $"L = {Result} [mm]";
    }
}