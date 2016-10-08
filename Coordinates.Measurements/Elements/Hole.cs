namespace Coordinates.Measurements.Elements
{
    public class Hole : BaseElement
    {
        public override int RequiredMeasurementCount { get; } = 5;

        public SurfaceEnum Plane { get; set; } = SurfaceEnum.XY;

        public override bool CanCalculate()
        {
            return true;
        }

        public override object Calculate()
        {
            if (Plane == SurfaceEnum.XY)
            {
            }

            return null;
        }
    }
}
