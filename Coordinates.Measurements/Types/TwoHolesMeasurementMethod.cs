using System;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Models;

namespace Coordinates.Measurements.Types
{
    public class TwoHolesMeasurementMethod : BaseMeasurementMethod
    {
        public TwoHolesMeasurementMethod()
        {
            BaseElements.Add(new Hole());
            BaseElements.Add(new Hole());
        }

        public override ICalculationResult Calculate()
        {
            if (!CanCalculate())
                return new ErrorResult { Message = "Nie można policzyć jednego lub obu elementów." };

            var firstElement = BaseElements[0];
            var secondElement = BaseElements[1];

            var firstElementCalculation = firstElement.Calculate();
            var secondElementCalculation = secondElement.Calculate();

            if (firstElementCalculation is ErrorResult || secondElementCalculation is ErrorResult)
                return new ErrorResult { Message = "Wystąpił błąd podczas obliczania elementu." };

            var x10 = ((HoleResult)firstElementCalculation).X0;
            var y10 = ((HoleResult)firstElementCalculation).Y0;
            var x20 = ((HoleResult)secondElementCalculation).X0;
            var y20 = ((HoleResult)secondElementCalculation).Y0;

            return new TwoHolesResult
            {
                Result = Math.Sqrt((x10 - x20) * (x10 - x20) + (y10 - y20) * (y10 - y20))
            };
        }

        public override string ToString() => "Dwa otwory";
    }
}
