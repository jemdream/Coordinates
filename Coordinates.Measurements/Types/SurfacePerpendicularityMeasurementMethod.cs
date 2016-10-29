using System;
using System.Linq;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Models;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Types
{
    public class SurfacePerpendicularityMeasurementMethod : BaseMeasurementMethod
    {
        public SurfacePerpendicularityMeasurementMethod()
        {
            BaseElements.Add(new Surface());
            BaseElements.Add(new Surface());
        }

        public override bool SetupInitialPosition(Position position) => false;

        public override bool SetupPlane(PlaneEnum? plane)
        {
            if (ActiveElement == null) return false;
            ActiveElement.Plane = plane;
            return true;
        }

        public override ICalculationResult Calculate()
        {
            if (!CanCalculate())
                return new ErrorResult { Message = "Wybierz odpowiednią liczbę pomiarów." };

            var firstElement = BaseElements[0];
            var secondElement = BaseElements[1];

            var firstElementCalculation = firstElement.Calculate();
            var secondElementCalculation = secondElement.Calculate();

            if (firstElementCalculation is ErrorResult || secondElementCalculation is ErrorResult)
                return new ErrorResult { Message = "Wystąpił błąd podczas obliczania elementu." };

            double t0, t1;

            if (firstElement.Plane == PlaneEnum.XY && secondElement.Plane == PlaneEnum.YZ)
            {
                t0 = ((SurfaceResult)firstElementCalculation).A2;
                t1 = ((SurfaceResult)secondElementCalculation).A3;
            }
            else if (firstElement.Plane == PlaneEnum.YZ && secondElement.Plane == PlaneEnum.XY)
            {
                t0 = ((SurfaceResult)firstElementCalculation).A3;
                t1 = ((SurfaceResult)secondElementCalculation).A2;
            }
            else if (firstElement.Plane == PlaneEnum.XY && secondElement.Plane == PlaneEnum.ZX || firstElement.Plane == PlaneEnum.ZX && secondElement.Plane == PlaneEnum.XY)
            {
                t0 = ((SurfaceResult)firstElementCalculation).A3;
                t1 = ((SurfaceResult)secondElementCalculation).A3;
            }
            else if (firstElement.Plane == PlaneEnum.YZ && secondElement.Plane == PlaneEnum.ZX || firstElement.Plane == PlaneEnum.ZX && secondElement.Plane == PlaneEnum.YZ)
            {
                t0 = ((SurfaceResult)firstElementCalculation).A2;
                t1 = ((SurfaceResult)secondElementCalculation).A2;
            }
            else
            {
                return new ErrorResult { Message = "Wybrano dwie te same płaszczyzny przy pomiarze prostopadłości" };
            }

            return new SurfacePerpendicularityResult
            {
                Result = Math.Atan(Math.Abs((t1 - t0) / (1 + t0 * t1)))
            };
        }

        public override string ToString() => "Płaszczyzny - prostopadłość";
    }
}
