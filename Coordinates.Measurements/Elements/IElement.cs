﻿using Coordinates.Measurements.Helpers;
using Coordinates.Measurements.Models;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Elements
{
    public interface IElement
    {
        int RequiredMeasurementCount { get; }
        PlaneEnum? Plane { get; set; }

        bool CanCalculate();
        ICalculationResult Calculate();

        ObservableList<Position> SelectedPositions { get; }
        ObservableList<Position> Positions { get; }
    }
}
