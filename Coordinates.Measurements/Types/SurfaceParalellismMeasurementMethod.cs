using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public class SurfaceParalellismMeasurementMethod : BaseMeasurementMethod
    {
        public SurfaceParalellismMeasurementMethod()
        {
            BaseElements.Add(new Surface());
            BaseElements.Add(new Surface());
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
