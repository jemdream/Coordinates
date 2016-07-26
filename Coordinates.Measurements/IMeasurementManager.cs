using System;
using System.Collections.Generic;
using Coordinates.Measurements.Types;
using Coordinates.Models;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements
{
    public interface IMeasurementManager
    {
        IMeasurement SelectedMeasurement { get; set; }
        IObserver<ContactPosition> SelectedPositions { get; }
        IEnumerable<IMeasurement> AvailableMeasurements { get; }
        IEnumerable<GaugePosition> GaugePositions { get; }
        IEnumerable<ContactPosition> ContactPositions { get; }
    }
}