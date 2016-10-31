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
                return new ErrorResult { Message = "Nie można policzyć jednego lub obu elementów." };

            var firstElement = BaseElements[0];
            var secondElement = BaseElements[1];

            var firstElementCalculation = firstElement.Calculate();
            var secondElementCalculation = secondElement.Calculate();

            if (firstElementCalculation is ErrorResult && secondElementCalculation is ErrorResult)
                return new ErrorResult { Message = $"Pierwsza płaszczyzna: {firstElementCalculation} Druga płaszczyzna: {secondElementCalculation}" };
            if (firstElementCalculation is ErrorResult)
                return new ErrorResult { Message = $"Pierwsza płaszczyzna: {firstElementCalculation}" };
            if (secondElementCalculation is ErrorResult)
                return new ErrorResult { Message = $"Druga płaszczyzna: {secondElementCalculation}" };

            if (firstElement.Plane == PlaneEnum.XY && secondElement.Plane == PlaneEnum.YZ)
            {
                return new SurfacePerpendicularityResult
                {
                    Result = DeterminedResult(((SurfaceResult)firstElementCalculation).A1,
                    ((SurfaceResult)secondElementCalculation).A2)
                };
            }
            if (firstElement.Plane == PlaneEnum.YZ && secondElement.Plane == PlaneEnum.XY)
            {
                return new SurfacePerpendicularityResult
                {
                    Result = DeterminedResult(((SurfaceResult)firstElementCalculation).A2,
                        ((SurfaceResult)secondElementCalculation).A1)
                };
            }
            if ((firstElement.Plane == PlaneEnum.XY && secondElement.Plane == PlaneEnum.ZX) ||
                (firstElement.Plane == PlaneEnum.ZX && secondElement.Plane == PlaneEnum.XY))
            {
                return new SurfacePerpendicularityResult
                {
                    Result = DeterminedResult(((SurfaceResult)firstElementCalculation).A2,
                    ((SurfaceResult)secondElementCalculation).A2)
                };
            }
            if ((firstElement.Plane == PlaneEnum.YZ && secondElement.Plane == PlaneEnum.ZX) ||
                (firstElement.Plane == PlaneEnum.ZX && secondElement.Plane == PlaneEnum.YZ))
            {
                return new SurfacePerpendicularityResult
                {
                    Result = DeterminedResult(((SurfaceResult)firstElementCalculation).A1,
                    ((SurfaceResult)secondElementCalculation).A1)
                };
            }
            return new ErrorResult
            {
                Message = "Wybrano dwie te same płaszczyzny przy pomiarze prostopadłości."
            };
        }

        private double DeterminedResult(double sr0, double sr1)
        {
            double t0, t1;
            if (sr0.Equals(0.0) && sr1.Equals(0.0))
            {
                return 1.57;
            }
            if (sr1.Equals(0.0))
            {
                t0 = -1 / sr0;
                t1 = sr1;
            }
            else
            {
                t1 = -1 / sr1;
                t0 = sr0;
            }
            return Math.Atan(Math.Abs((t1 - t0) / (1 + t0 * t1)));
        }

        public override string ToString() => "Płaszczyzny - prostopadłość";
    }
}
