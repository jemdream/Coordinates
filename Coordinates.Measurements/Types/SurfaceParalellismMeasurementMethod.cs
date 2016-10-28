using System;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Models;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Types
{
    public class SurfaceParalellismMeasurementMethod : BaseMeasurementMethod
    {
        public SurfaceParalellismMeasurementMethod()
        {
            BaseElements.Add(new Surface());
            BaseElements.Add(new Surface());
        }

        public override bool SetupInitialPosition(Position position) => true;

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
                return new ErrorResult { Message = "(!firstElement.CanCalculate() || !secondElement.CanCalculate())" };

            var firstElementCalculation = firstElement.Calculate();
            var secondElementCalculation = secondElement.Calculate();

            if (firstElementCalculation is ErrorResult || secondElementCalculation is ErrorResult)
                return new ErrorResult { Message = "(firstElementCalculation is ErrorResult || secondElementCalculation is ErrorResult)" };

            var t0 = ((SurfaceResult)firstElementCalculation).A2;
            var t1 = ((SurfaceResult)secondElementCalculation).A2;

            var result = Math.Atan(Math.Abs((t1 - t0) / (1 + t0 * t1)));

            return new SurfaceParalellismResult
            {
                Result = $"{result}"
            };
        }

        public override string ToString() => $"Płaszczyzny - równoległość";
    }
}
