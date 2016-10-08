namespace Coordinates.Measurements.Elements
{
    public class Hole : BaseElement
    {
        public override int RequiredMeasurementCount { get; } = 5;

        public SurfaceEnum Plane { get; set; } = SurfaceEnum.XY;

        public override bool CanCalculate()
        {
            throw new System.NotImplementedException();
        }

        public override object Calculate()
        {
            if (Plane == SurfaceEnum.XY)
            {
            }

            throw new System.NotImplementedException();
        }
    }
}
