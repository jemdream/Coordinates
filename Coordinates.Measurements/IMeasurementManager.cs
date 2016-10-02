using System;
using System.Collections.Generic;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Helpers;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements
{
    public interface IMeasurementManager
    {
        // Measurement methods/types
        IEnumerable<MeasurementMethodEnum> AvailableMeasurementMethods { get; }
        MeasurementMethodEnum? SelectedMeasurementMethod { get; }
        IObservable<IMeasurementMethod> MeasurementSource { get; }

        bool SetupMeasurementMethod(MeasurementMethodEnum selectedMeasurementMethod);
        bool ResetMeasurementData();
        bool SubscribeToDataSource(IElement element);

        // Positions
        IObservable<Position> PositionSource { get; }
        ObservableList<Position> PositionBuffer { get; }

        bool Calibrate();
    }
}