using System.Collections.Generic;
using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Elements
{
    public interface IElement
    {
        int RequiredMeasurementCount { get; }

        bool CanCalculate();
        object Calculate();

        IList<Position> SelectedPositions { get; }
        IList<Position> Positions { get; }
    }
}
