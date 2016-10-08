namespace Coordinates.Measurements.Elements
{
    public class Hole : BaseElement
    {
        public override int RequiredMeasurementCount { get; } = 5;
        
        public override bool CanCalculate()
        {
            return true;
        }

        public override object Calculate()
        {
            if (Plane == PlaneEnum.XY)
            {
            }

            return null;
        }
    }
}
