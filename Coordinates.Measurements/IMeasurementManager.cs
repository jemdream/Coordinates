using System.Collections.Generic;
using Coordinates.Measurements.Types;
using Coordinates.Models;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements
{
    public interface IMeasurementManager
    {
        IEnumerable<IMeasurement> AvailableMeasurements { get; }
        IMeasurement SelectedMeasurement { get; set; }

        IEnumerable<GaugePosition> GaugePositions { get; }
        IEnumerable<ContactPosition> ContactPositions { get; }
        IList<ContactPosition> SelectedPositions { get; set; }
    }
}