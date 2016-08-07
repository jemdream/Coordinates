using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Coordinates.Measurements.Types;
using Coordinates.Models;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements
{
    public interface IMeasurementManager
    {
        IEnumerable<IMeasurementMethod> AvailableMeasurements { get; }
        IMeasurementMethod SelectedMeasurementMethod { get; set; }
        IObservable<GaugePosition> GaugePositionSource { get; }
        IObservable<ContactPosition> ContactPositionsSource { get; }
        ObservableCollection<ContactPosition> SelectedPositions { get; set; }
    }
}