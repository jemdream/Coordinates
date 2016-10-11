using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Models;

namespace Coordinates.Measurements.Types
{
    public class SurfacePerpendicularityMeasurementMethod : BaseMeasurementMethod
    {
        public SurfacePerpendicularityMeasurementMethod()
        {
            BaseElements.Add(new Surface());
            BaseElements.Add(new Surface());
        }

        public override bool SetupPlane(PlaneEnum? plane)
        {
            if (ActiveElement == null) return false;
            ActiveElement.Plane = plane;
            return true;
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
            var secondElement = BaseElements[1];

            if (!firstElement.CanCalculate() || !secondElement.CanCalculate())
                return new ErrorResult { Message = "(!firstElement.CanCalculate() || !secondElement.CanCalculate())" }; ;

            var firstElementCalculation = firstElement.Calculate();
            var secondElementCalculation = secondElement.Calculate();

            if (firstElementCalculation is ErrorResult || secondElementCalculation is ErrorResult)
                return new ErrorResult { Message = "(firstElementCalculation is ErrorResult || secondElementCalculation is ErrorResult)" };

            return new SurfacePerpendicularityResult
            {
                Result = $"{firstElementCalculation} & {secondElementCalculation}"
            };
        }

        public override string ToString() => "Płaszczyzny - prostopadłość";
    }
}
