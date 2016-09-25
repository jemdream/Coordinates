using System.Collections.Generic;
using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public class OneHoleMeasurementMethod : IMeasurementMethod
    {
        private readonly List<Hole> _elements = new List<Hole> { new Hole() };

        public OneHoleMeasurementMethod()
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

        public static string Title = "Jeden otwór";
        //public override string ToString()
        //{
        //    return "Jeden otwór";
        //}
    }
}
