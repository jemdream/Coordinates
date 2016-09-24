using System.Collections.Generic;
using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public class TwoHolesMeasurementMethod : IMeasurementMethod
    {
        private readonly List<Hole> _elements = new List<Hole> { new Hole(), new Hole() };
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

        public override string ToString()
        {
            return "Dwa otwory";
        }
    }
}
