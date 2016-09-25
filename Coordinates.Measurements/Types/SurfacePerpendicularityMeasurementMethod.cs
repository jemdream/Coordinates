using System.Collections.Generic;
using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public class SurfacePerpendicularityMeasurementMethod : IMeasurementMethod
    {
        private readonly List<Surface> _elements = new List<Surface> { new Surface(), new Surface() };

        public SurfacePerpendicularityMeasurementMethod()
        {
            ActiveElement = _elements[0];
        }

        public IEnumerable<IElement> Elements => _elements;
        public IElement ActiveElement { get; }

        public bool CanCalculate()
        {
            return true;
        }

        public object Calculate()
        {
            throw new System.NotImplementedException();
        }
        public static string Title = "Płaszczyzny - prostopadłość";
        //public override string ToString()
        //{
        //    return "Płaszczyzny - prostopadłość";
        //}
    }
}
