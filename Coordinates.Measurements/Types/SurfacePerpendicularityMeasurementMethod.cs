using System.Collections.Generic;
using Windows.Foundation;
using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Types
{
    public class SurfacePerpendicularityMeasurementMethod : IMeasurementMethod
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

        public ObservableList<ContactPosition> SelectedPositions { get; }
        public ObservableList<GaugePosition> RawGaugePositions { get; }
        public ObservableList<ContactPosition> RawContactPositions { get; }

        public override string ToString()
        {
            return "Płaszczyzny - prostopadłość";
        }
    }
}
