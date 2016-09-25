using System.Collections.Generic;
using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public interface IMeasurementMethod
    {
        IEnumerable<IElement> Elements { get; }
        IElement ActiveElement { get; }
        bool CanCalculate();
        object Calculate();
    }
}