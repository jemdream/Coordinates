using System;
using Coordinates.Measurements.Helpers;
using Coordinates.Measurements.Models;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Elements
{
    public abstract class BaseElement : IElement
    {
        private const double Resolution = 0.00001;

        public abstract int RequiredMeasurementCount { get; }
        public PlaneEnum? Plane { get; set; }
        public Position InitialPosition { get; set; }
        
        /// <summary>
        /// Validates Axis Movement against initial position
        /// </summary>
        public virtual bool AxisMovementValidation(Position incomingPosition)
        {
            return (InitialPosition == null) || Math.Abs(incomingPosition.GetBlockedAxisValue(Plane).Value - InitialPosition.GetBlockedAxisValue(Plane).Value) < Resolution;
        }

        public virtual bool CanCalculate() => true;
        public virtual ICalculationResult Calculate() => new ErrorResult { Message = "Not implemented." };

        public ObservableList<Position> SelectedPositions { get; } = new ObservableList<Position>();
        public ObservableList<Position> Positions { get; } = new ObservableList<Position>();
    }
}
