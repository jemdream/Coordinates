﻿using System;
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

            double t0, t1;

            if (firstElement.Plane == PlaneEnum.XY && secondElement.Plane == PlaneEnum.YZ || firstElement.Plane == PlaneEnum.YZ && secondElement.Plane == PlaneEnum.XY)
            {
                t0 = ((SurfaceResult)firstElementCalculation).A2;
                t1 = ((SurfaceResult)secondElementCalculation).A3;
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

            var result = Math.Atan(Math.Abs((t1 - t0) / (1 + t0 * t1)));

            return new SurfacePerpendicularityResult
            {
                Result = $"{result}"
            };
        }

        public override string ToString() => "Płaszczyzny - prostopadłość";
    }
}
