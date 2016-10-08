﻿using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Elements
{
    public abstract class BaseElement : IElement
    {
        public abstract int RequiredMeasurementCount { get; }
        public PlaneEnum? Plane { get; set; }

        public virtual bool CanCalculate() => true;
        public virtual object Calculate() => null;

        public ObservableList<Position> SelectedPositions { get; } = new ObservableList<Position>();
        public ObservableList<Position> Positions { get; } = new ObservableList<Position>();
    }
}
