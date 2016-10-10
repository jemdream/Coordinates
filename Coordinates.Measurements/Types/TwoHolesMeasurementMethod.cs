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
                return new ErrorResult { Message = $"{firstElementCalculation} & {secondElementCalculation}" };

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
