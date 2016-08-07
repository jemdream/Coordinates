using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements
{
    public interface IMeasurementManager
    {
        IEnumerable<IMeasurementMethod> AvailableMeasurementMethods { get; }
        IMeasurementMethod SelectedMeasurementMethod { get; set; }
        IObservable<Position> PositionSource { get; }
        ObservableCollection<ContactPosition> SelectedPositions { get; set; }
    }
}