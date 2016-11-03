using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Models;

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

        public override ICalculationResult Calculate()
        {
            if (!CanCalculate())
                return new ErrorResult { Message = "Wybierz odpowiednią ilość pomiarów." };

            var firstElement = BaseElements[0];

            return firstElement.Calculate();
        }

        public override string ToString() => $"Jeden otwór";
    }
}
