﻿using System.Collections.Generic;
using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public class SurfaceParalellismMeasurementMethod : IMeasurementMethod
    {
        private readonly List<Surface> _elements = new List<Surface> { new Surface(), new Surface() };

        internal SurfaceParalellismMeasurementMethod()
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

        public override string ToString() { return "Płaszczyzny - równoległość"; }
    }
}
