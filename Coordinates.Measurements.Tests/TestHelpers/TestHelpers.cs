using System.Collections.Generic;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Tests.TestHelpers
{
    public static class TestHelpers
    {
        public static T PrepareMeasurementMethodModel<T>(IEnumerable<Position> mockoweZaznaczoneDaneFirstElement, PlaneEnum firstElementPlane,
            IEnumerable<Position> mockoweZaznaczoneDaneSecondElement, PlaneEnum secondElementPlane) where T : BaseMeasurementMethod, new()
        {
            var measurements = new T();

            var firstElement = measurements.ActivateNextElement();
            firstElement.Plane = firstElementPlane;

            foreach (var position in mockoweZaznaczoneDaneFirstElement)
                firstElement.SelectedPositions.Add(position);

            var secondElement = measurements.ActivateNextElement();
            secondElement.Plane = secondElementPlane;

            foreach (var position in mockoweZaznaczoneDaneSecondElement)
                secondElement.SelectedPositions.Add(position);

            return measurements;
        }
    }
}
