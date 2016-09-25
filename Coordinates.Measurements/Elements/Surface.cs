using System.Collections.Generic;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Elements
{
    public class Surface : IElement
    {
        public int RequiredMeasurementCount { get; } = 5;

        public bool CanCalculate()
        {
            throw new System.NotImplementedException();
        }

        public object Calculate()
        {
            throw new System.NotImplementedException();
        }

        public IList<Position> SelectedPositions { get; } = new List<Position>();
        public IList<Position> Positions { get; } = new List<Position>();
    }
}
