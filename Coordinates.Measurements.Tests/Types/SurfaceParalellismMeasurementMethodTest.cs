using System.Collections.Generic;
using System.Linq;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Models;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Coordinates.Measurements.Tests.Types
{
    [TestClass]
    public class SurfaceParalellismMeasurementMethodTest
    {
        [TestMethod]
        public void When_SufficientData_Expect_TrueCanCalculates()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(0.3, 0.1, 0.3, true), new Position(13.0, 5.0, 1.0, true),
                new Position(0.6, 0.4, 0.3, true), new Position(1.0, 1.0, 0.3, true),
                new Position(1.5, 11.3, 0.3, true)
            };
            var firstElementPlane = PlaneEnum.YZ;

            var mockDataSecondElement = new[]
            {
                new Position(0.3, 0.1, 1.0, true), new Position(0.6, 0.4, 0.5, true),
                new Position(1.2, 1.0, 1.0, true), new Position(13.0, 5.0, 1.0, true),
                new Position(1.5, 11.3, 0.5, true)
            };
            var secondElementPlane = PlaneEnum.YZ;

            // Prepare object with data from above
            var measurements = ArrangeSurfParalMeasureMethodModel(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act 
            var canCalculateFirstElement = arrayOfElements[0].CanCalculate();
            var canCalculateSecondElement = arrayOfElements[1].CanCalculate();
            var canCalculate = measurements.CanCalculate();

            // Assert
            Assert.IsTrue(canCalculateFirstElement);
            Assert.IsTrue(canCalculateSecondElement);
            Assert.IsTrue(canCalculate);
        }

        [TestMethod]
        public void When_InsufficientData_Expect_FalseCanCalculates()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(0.3, 0.1, 0.3, true)
            };
            var firstElementPlane = PlaneEnum.YZ;

            var mockDataSecondElement = new[]
            {
                new Position(0.3, 0.1, 1.0, true)
            };
            var secondElementPlane = PlaneEnum.YZ;

            // Prepare object with data from above
            var measurements = ArrangeSurfParalMeasureMethodModel(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act 
            var canCalculateFirstElement = arrayOfElements[0].CanCalculate();
            var canCalculateSecondElement = arrayOfElements[1].CanCalculate();
            var canCalculate = measurements.CanCalculate();

            // Assert
            Assert.IsFalse(canCalculateFirstElement);
            Assert.IsFalse(canCalculateSecondElement);
            Assert.IsFalse(canCalculate);
        }

        [TestMethod]
        public void When_TooClosePoints_Expect_ErrorResults()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                Position.Default, Position.Default, Position.Default, Position.Default, Position.Default
            };
            var firstElementPlane = PlaneEnum.YZ;

            var mockDataSecondElement = new[]
            {
                Position.Default, Position.Default, Position.Default, Position.Default, Position.Default
            };
            var secondElementPlane = PlaneEnum.YZ;

            // Prepare object with data from above
            var measurements = ArrangeSurfParalMeasureMethodModel(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act 
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsInstanceOfType(calculateFirstElement, typeof(ErrorResult));
            Assert.IsInstanceOfType(calculateSecondElement, typeof(ErrorResult));
            Assert.IsInstanceOfType(calculate, typeof(ErrorResult));
        }

        private static SurfaceParalellismMeasurementMethod ArrangeSurfParalMeasureMethodModel(IEnumerable<Position> mockoweZaznaczoneDaneFirstElement, PlaneEnum firstElementPlane,
            IEnumerable<Position> mockoweZaznaczoneDaneSecondElement, PlaneEnum secondElementPlane)
        {
            var measurements = new SurfaceParalellismMeasurementMethod();

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
