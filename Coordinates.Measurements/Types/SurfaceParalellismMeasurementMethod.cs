using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Models;

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

        public override ICalculationResult Calculate()
        {
            if (!CanCalculate())
                return null;

            var firstElement = BaseElements[0];
            var secondElement = BaseElements[1];

            if (!firstElement.CanCalculate() || !secondElement.CanCalculate())
                return null;

            var firstElementCalculation = firstElement.Calculate();
            var secondElementCalculation = secondElement.Calculate();

            return new SurfaceParalellismResult
            {
                Result = 0
            };
        }

        public override string ToString() { return "Płaszczyzny - równoległość"; }
    }
}
