﻿using System.Collections.Generic;
using Windows.Foundation;
using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Types
{
    public interface IMeasurementMethod
    {
        int[] RequiredMeasurementCount { get; }
        bool CanExecute();
        object Execute(IEnumerable<Point> measurements); // TODO MeasurementCalculations
        
        // Selected positions for measurement
        ObservableList<ContactPosition> SelectedPositions { get; }
        ObservableList<GaugePosition> RawGaugePositions { get; }
        ObservableList<ContactPosition> RawContactPositions { get; }
    }
}