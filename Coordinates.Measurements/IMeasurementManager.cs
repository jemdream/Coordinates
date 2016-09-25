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
        IEnumerable<KeyValuePair<string, Type>> AvailableMeasurementMethods { get; }
        IMeasurementMethod SelectedMeasurementMethod { get; }

        bool SetupMeasurementMethod(IMeasurementMethod selectedMeasurementMethod);
        bool ResetMeasurementData();


        // Positions
        IObservable<Position> PositionSource { get; }
        ObservableList<Position> RawGaugePositions { get; }
        GaugePositionDTO CompensationPosition { get; }

        bool SetupCalibration();
    }
}