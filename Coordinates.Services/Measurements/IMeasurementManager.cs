using System.Collections.Generic;

namespace Coordinates.Services.Measurements
{
    public interface IMeasurementManager
    {
        // Injected from constructor:
        // private IDataSource
        
        bool Reset { get; set; }
        object Process { get; set; }
        
        object SelectedMeasurement { get; set; } // interface type
        IEnumerable<object> AvailableMeasurements { get; set; }
    }
}