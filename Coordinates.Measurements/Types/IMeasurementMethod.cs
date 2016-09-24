using System.Collections.Generic;
using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public interface IMeasurementMethod
    {
        // TODO Place backing field in IMeasurementMethod implementation with real IElement implementation and operate !!
        IEnumerable<IElement> Elements { get; }
        IElement ActiveElement { get; }
        bool CanCalculate();
        object Calculate();
    }
}