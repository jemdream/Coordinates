using System;
using System.Collections.Generic;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;
using Coordinates.Measurements.Helpers;

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
        ObservableCollectionRx<ContactPosition> SelectedPositions { get; }
        
        // Positions
        IObservable<Position> PositionSource { get; }
        ObservableCollectionRx<GaugePosition> RawGaugePositions { get; }
        ObservableCollectionRx<ContactPosition> RawContactPositions { get; }
        bool SetupCalibration();
    }
}