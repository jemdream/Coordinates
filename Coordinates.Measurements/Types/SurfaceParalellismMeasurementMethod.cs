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

        public override bool SetupInitialPosition(Position position) => false;

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

            var t0 = ((SurfaceResult)firstElementCalculation).A1;
            var t1 = ((SurfaceResult)secondElementCalculation).A1;

            return new SurfaceParalellismResult
            {
                Result = Math.Atan(Math.Abs((t1 - t0) / (1 + t0 * t1)))
            };
        }

        public override string ToString() => $"Płaszczyzny - równoległość";
    }
}
