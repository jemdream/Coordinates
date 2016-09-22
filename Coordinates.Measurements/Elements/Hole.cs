using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Elements
{
    public class Hole : IElement
    {
        public int RequiredMeasurementCount { get; } = 5;

        public bool CanCalculate()
        {
            throw new System.NotImplementedException();
        }

        public object Calculate()
        {
            throw new System.NotImplementedException();
        }

        public ObservableList<Position> SelectedPositions { get; } = new ObservableList<Position>();
        public ObservableList<Position> RawContactPositions { get; } = new ObservableList<Position>();
    }
}
