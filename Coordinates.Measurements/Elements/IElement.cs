using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Elements
{
    public interface IElement
    {
        int RequiredMeasurementCount { get; }

        bool CanCalculate();
        object Calculate();

        ObservableList<Position> SelectedPositions { get; }
        ObservableList<Position> Positions { get; }
    }
}
