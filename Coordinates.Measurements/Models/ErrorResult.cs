using System;
using System.Collections.Generic;
using System.Linq;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Models
{
    public class ErrorResult : ICalculationResult
    {
        public string Message { get; set; }
        public IList<Position> FaultyPositions { get; } = new List<Position>();

        public override string ToString()
        {
            var faultyPositions = FaultyPositions.Any()
                ? $"; {string.Join(", ", FaultyPositions?.Select(x => x))}"
                : string.Empty;

            return string.Format("{0}{1}", Message, faultyPositions);
        }
    }
}
