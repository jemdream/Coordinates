using System.Collections.Generic;
using Windows.Foundation;

namespace Coordinates.Measurements.Types
{
    public class FlatnessMeasurementMethod : IMeasurementMethod
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
        
        public override string ToString()
        {
            return "Płaskość";
        }
    }
}
