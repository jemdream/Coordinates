using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public class TwoHolesMeasurementMethod : BaseMeasurementMethod
    {
        public TwoHolesMeasurementMethod()
        {
            BaseElements.Add(new Hole());
            BaseElements.Add(new Hole());
        }

        public override bool CanCalculate()
        {
            return true;
        }

        public override object Calculate()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString() { return "Dwa otwory"; }
    }
}
