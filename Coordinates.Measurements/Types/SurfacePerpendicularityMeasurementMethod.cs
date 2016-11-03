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
                return new ErrorResult
                {
                    Message =
                        $"Pierwsza płaszczyzna: {firstElementCalculation} Druga płaszczyzna: {secondElementCalculation}"
                };
            if (firstElementCalculation is ErrorResult)
                return new ErrorResult { Message = $"Pierwsza płaszczyzna: {firstElementCalculation}" };
            if (secondElementCalculation is ErrorResult)
                return new ErrorResult { Message = $"Druga płaszczyzna: {secondElementCalculation}" };
            if (firstElement.Plane == secondElement.Plane)
            {
                return new ErrorResult
                {
                    Message = "Wybrano dwie te same płaszczyzny przy pomiarze prostopadłości."
                };
            }

            return new SurfacePerpendicularityResult
            {
                Result = PlanesAngleGeneralFormula(((SurfaceResult)firstElementCalculation).A1,
                    ((SurfaceResult)firstElementCalculation).A2, ((SurfaceResult)secondElementCalculation).A1,
                    ((SurfaceResult)secondElementCalculation).A2, firstElement, secondElement)
            };
        }


        private double PlanesAngleGeneralFormula(double s00, double s01, double s10, double s11, IElement firstElement, IElement secondElement)
        {
            double a0 = 0.0, b0 = 0.0, c0 = 0.0;
            double a1 = 0.0, b1 = 0.0, c1 = 0.0;

            switch (firstElement.Plane)
            {
                case PlaneEnum.XY:
                    a0 = s00;
                    b0 = s01;
                    c0 = -1.0;
                    break;
                case PlaneEnum.YZ:
                    a0 = -1.0;
                    b0 = s00;
                    c0 = s01;
                    break;
                case PlaneEnum.ZX:
                    a0 = s00;
                    b0 = -1.0;
                    c0 = s01;
                    break;
            }
            switch (secondElement.Plane)
            {
                case PlaneEnum.XY:
                    a1 = s10;
                    b1 = s11;
                    c1 = -1.0;
                    break;
                case PlaneEnum.YZ:
                    a1 = -1.0;
                    b1 = s10;
                    c1 = s11;
                    break;
                case PlaneEnum.ZX:
                    a1 = s10;
                    b1 = -1.0;
                    c1 = s11;
                    break;
            }

            return Math.Acos(Math.Abs((a0 * a1 + b0 * b1 + c0 * c1) / (Math.Sqrt(a0 * a0 + b0 * b0 + c0 * c0) * Math.Sqrt(a1 * a1 + b1 * b1 + c1 * c1))));
        }


        public override string ToString() => "Płaszczyzny - prostopadłość";
    }
}
