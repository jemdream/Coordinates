using System.Collections.Generic;
using Windows.Foundation;

namespace Coordinates.Measurements.Types
{
    public interface IMeasurementMethod
    {
        int[] RequiredMeasurementCount { get; }
        bool CanExecute();
        object Execute(IEnumerable<Point> measurements); // TODO MeasurementCalculations
    }
}
