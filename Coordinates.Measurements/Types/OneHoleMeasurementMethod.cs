using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public class OneHoleMeasurementMethod : BaseMeasurementMethod
    {
        public OneHoleMeasurementMethod()
        {
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

        public override string ToString() { return "Jeden otwór"; }
    }
}
