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

        public override object Calculate()
        {
            var firstElement = BaseElements[0];
            //var firstElementPlane = firstElement.SelectedPlane;
            var secondElement = BaseElements[1];
            //var secondElementPlane = secondElement.SelectedPlane;
            // Jak nie da sie policzyc dwóch
            if (!firstElement.CanCalculate() || !secondElement.CanCalculate())
                return null;
            var firstElementCalculation = firstElement.Calculate();
            var secondElementCalculation = secondElement.Calculate();
            double x10 = ((HoleResult)firstElementCalculation).X0;
            double y10 = ((HoleResult)firstElementCalculation).Y0;
            double x20 = ((HoleResult)secondElementCalculation).X0;
            double y20 = ((HoleResult)secondElementCalculation).Y0;
            var result = Math.Sqrt((x10-x20)*(x10-x20) + (y10 - y20) * (y10 - y20));
            return result;
        }

        public override string ToString() { return "Dwa otwory"; }
    }
}
