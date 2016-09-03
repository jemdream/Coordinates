using System;
using System.Collections.Generic;
using Coordinates.ExternalDevices.Models;
using Coordinates.Measurements.Helpers;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements
{
    public interface IMeasurementManager
    {
        // Measurement methods/types
        IEnumerable<IMeasurementMethod> AvailableMeasurementMethods { get; }
        IMeasurementMethod SelectedMeasurementMethod { get; }
        bool SetupMeasurementMethod(IMeasurementMethod selectedMeasurementMethod);
        bool ResetMeasurementData();

        // Selected positions for measurement
        ObservableList<ContactPosition> SelectedPositions { get; }
        
        // Positions
        IObservable<Position> PositionSource { get; }
        ObservableList<GaugePosition> RawGaugePositions { get; }
        ObservableList<ContactPosition> RawContactPositions { get; }
        GaugePositionDTO CompensationPosition { get; }
        bool SetupCalibration();
    }
}