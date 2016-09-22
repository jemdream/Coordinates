using System.Collections.Generic;
using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Types
{
    public class OneHoleMeasurementMethod : IMeasurementMethod
    {
        public int[] RequiredMeasurementCount { get; } = { 5 };

        public bool CanExecute()
        {
            return true;
        }

        public object Execute(IEnumerable<Position> measurements)
        {
            throw new System.NotImplementedException();
        }
        
        public ObservableList<Position> SelectedPositions { get; }
        public ObservableList<Position> RawGaugePositions { get; }
        public ObservableList<Position> RawContactPositions { get; }

        public override string ToString()
        {
            return "Jeden otwór";
        }
    }
}
