using System.Collections.Generic;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Models
{
    public class ErrorResult : ICalculationResult
    {
        public string Message { get; set; }
        public IList<Position> FaultyPositions { get; set; }

        public override string ToString() => $"{Message}";
    }
}
