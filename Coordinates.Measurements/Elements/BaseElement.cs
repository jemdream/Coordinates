using System;
using Coordinates.Measurements.Helpers;
using Coordinates.Measurements.Models;
using Coordinates.Models.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Coordinates.Measurements.Elements
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class BaseElement : IElement
    {
        private const double Resolution = 0.00001;

        public abstract int RequiredMeasurementCount { get; }
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
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
        [JsonProperty("Result")]
        public ICalculationResult ResultGetter => Calculate();
        public virtual ICalculationResult Calculate() => new ErrorResult { Message = "Not implemented." };

        [JsonProperty]
        public ObservableList<Position> SelectedPositions { get; } = new ObservableList<Position>();
        [JsonProperty]
        public ObservableList<Position> Positions { get; } = new ObservableList<Position>();
    }
}
