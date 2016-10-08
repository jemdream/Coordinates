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
            var firstElement = BaseElements[0];

            return !firstElement.CanCalculate() ?
                null :
                firstElement.Calculate();
        }

        public override string ToString() { return "Jeden otwór"; }
    }
}
