using System.Collections.Generic;
using Windows.Foundation;
using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Types
{
    public class TwoHolesMeasurementMethod : IMeasurementMethod
    {
        public int[] RequiredMeasurementCount { get; } = { 5 };

        public bool CanExecute()
        {
            return true;
        }

        public object Execute(IEnumerable<Point> measurements)
        {
            throw new System.NotImplementedException();
        }

        public ObservableList<Position> SelectedPositions { get; }
        public ObservableList<Position> RawGaugePositions { get; }
        public ObservableList<Position> RawContactPositions { get; }

        public override string ToString()
        {
            return "Dwa otwory";
        }
    }
}
