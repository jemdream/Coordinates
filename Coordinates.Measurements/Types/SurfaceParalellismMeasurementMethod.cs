using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public class SurfaceParalellismMeasurementMethod : BaseMeasurementMethod
    {
        public SurfaceParalellismMeasurementMethod()
        {
            BaseElements.AddLast(new Surface());
            BaseElements.AddLast(new Surface());
        }

        public override bool CanCalculate()
        {
            return true;
        }

        public override object Calculate()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString() { return "Płaszczyzny - równoległość"; }
    }
}
