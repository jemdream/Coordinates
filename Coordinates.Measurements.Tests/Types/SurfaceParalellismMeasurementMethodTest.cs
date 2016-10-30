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
        public void When_InOneLine_Expect_ErrorResults()
        {
            // Arrange
            var mockDataFirstElement = new[]
             {
                new Position(6, 6, 6, true), new Position(6, 6, 6, true),
                new Position(6, 6, 6, true), new Position(6, 6, 6, true),
                new Position(6, 6, 6, true)
            };
            var firstElementPlane = PlaneEnum.YZ;

            var mockDataSecondElement = new[]
            {
                new Position(11.5, 2.1, 1.0, true), new Position(11.5, 5.4, 1.0, true),
                new Position(11.5, 0.1, 1.0, true), new Position(13.5, 5.0, 1.0, true)
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
            Assert.IsTrue(((ErrorResult)calculateFirstElement).Message.Equals("Wybrane pomiary są zbyt blisko siebie lub wykonane w linii prostej."));
            Assert.IsTrue(((ErrorResult)calculateSecondElement).Message.Equals("Wybierz odpowiednią liczbę pomiarów."));
            Assert.IsTrue(((ErrorResult)calculate).Message.Equals("Nie można policzyć jednego lub obu elementów."));
        }

        [TestMethod]
        public void When_ResultsParallelXY_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(1, 1, 1, true), new Position(100, 100, 1, true),
                new Position(50, 51, 1, true), new Position(100, 1, 1, true),
                new Position(1, 100, 1, true)
            };
            var firstElementPlane = PlaneEnum.XY;

            var mockDataSecondElement = new[]
            {
                new Position(1, 1, 10, true), new Position(100, 100, 10, true),
                new Position(50, 51, 10, true), new Position(100, 1, 10, true),
                new Position(1, 100, 10, true)
            };
            var secondElementPlane = PlaneEnum.XY;

            // Prepare object with data from above
            var measurements = ArrangeSurfParalMeasureMethodModel(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A0 > 0.99 && ((SurfaceResult)calculateFirstElement).A0 < 1.01);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A1 > -0.01 && ((SurfaceResult)calculateFirstElement).A1 < 0.01);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A2 > -0.01 && ((SurfaceResult)calculateFirstElement).A2 < 0.01);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A0 > 9.99 && ((SurfaceResult)calculateSecondElement).A0 < 10.01);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A1 > -0.01 && ((SurfaceResult)calculateSecondElement).A1 < 0.01);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A2 > -0.01 && ((SurfaceResult)calculateSecondElement).A2 < 0.01);
        }

        [TestMethod]
        public void When_ResultsParallelYZ_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(0, 1, 1, true), new Position(1, 100, 1, true),
                new Position(1, 1, 100, true), new Position(1, 50, 51, true),
                new Position(1, 100, 100, true)
            };
            var firstElementPlane = PlaneEnum.YZ;

            var mockDataSecondElement = new[]
            {
                new Position(0, 1, 1, true), new Position(100, 100, 1, true),
                new Position(100, 1, 100, true), new Position(100, 50, 51, true),
                new Position(100, 100, 100, true)
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
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A0 > 0.28 && ((SurfaceResult)calculateFirstElement).A0 < 0.30);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A1 > 0 && ((SurfaceResult)calculateFirstElement).A1 < 0.01);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A2 > 0 && ((SurfaceResult)calculateFirstElement).A2 < 0.01);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A0 > 28.98 && ((SurfaceResult)calculateSecondElement).A0 < 30);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A1 > 0.49 && ((SurfaceResult)calculateSecondElement).A1 < 0.51);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A2 > 0.49 && ((SurfaceResult)calculateSecondElement).A2 < 0.51);
        }

        [TestMethod]
        public void When_ResultsParallelZX_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(1, 1, 1, true), new Position(1, 1, 100, true),
                new Position(100, 1, 1, true), new Position(50, 2, 50, true),
                new Position(100, 3, 100, true)
            };
            var firstElementPlane = PlaneEnum.ZX;

            var mockDataSecondElement = new[]
            {
                new Position(1, 11, 1, true), new Position(1, 11, 100, true),
                new Position(100, 11, 1, true), new Position(50, 12, 50, true),
                new Position(100, 13, 100, true)
            };
            var secondElementPlane = PlaneEnum.ZX;

            // Prepare object with data from above
            var measurements = ArrangeSurfParalMeasureMethodModel(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A0 > 0.57 && ((SurfaceResult)calculateFirstElement).A0 < 0.59);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A1 > 0 && ((SurfaceResult)calculateFirstElement).A1 < 0.02);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A2 > 0 && ((SurfaceResult)calculateFirstElement).A2 < 0.02);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A0 > 10.57 && ((SurfaceResult)calculateSecondElement).A0 < 10.59);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A1 > 0 && ((SurfaceResult)calculateSecondElement).A1 < 0.02);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A2 > 0 && ((SurfaceResult)calculateSecondElement).A2 < 0.02);
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
