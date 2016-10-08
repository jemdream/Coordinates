using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public class SurfaceParalellismMeasurementMethod : BaseMeasurementMethod
    {
        public SurfaceParalellismMeasurementMethod()
        {
            BaseElements.Add(new Surface());
            BaseElements.Add(new Surface());
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

            // TODO obliczenia wspólne
            var wynik = 0; // mock

            return wynik;
        }

        public override string ToString() { return "Płaszczyzny - równoległość"; }
    }
}
