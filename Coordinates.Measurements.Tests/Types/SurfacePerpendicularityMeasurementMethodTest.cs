using System.Linq;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Models;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using static Coordinates.Measurements.Tests.TestHelpers.TestHelpers;

namespace Coordinates.Measurements.Tests.Types
{
    [TestClass]
    public class SurfacePerpendicularityMeasurementMethodTest
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
            var secondElementPlane = PlaneEnum.XY;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfacePerpendicularityMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
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
            var secondElementPlane = PlaneEnum.XY;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfacePerpendicularityMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
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
        public void When_TheSamePlane_Expect_ErrorResults()
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
            var measurements = PrepareMeasurementMethodModel<SurfacePerpendicularityMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((ErrorResult)calculate).Message.Equals("Wybrano dwie te same płaszczyzny przy pomiarze prostopadłości."));
        }

        [TestMethod]
        public void When_FirstElementErrorResult_Expect_ErrorResults()
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
                new Position(1, 1, 1, true), new Position(1, 2, 100, true),
                new Position(100, 3, 1, true), new Position(50, 4, 50, true),
                new Position(100, 5, 100, true)
            };
            var secondElementPlane = PlaneEnum.ZX;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfaceParalellismMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((ErrorResult)calculate).Message.Equals("Pierwsza płaszczyzna: Wybrane pomiary są zbyt blisko siebie lub wykonane w linii prostej."));
        }

        [TestMethod]
        public void When_SecondElementErrorResult_Expect_ErrorResults()
        {
            // Arrange
            var mockDataFirstElement = new[]
             {
                new Position(1, 1, 1, true), new Position(1, 2, 100, true),
                new Position(100, 3, 1, true), new Position(50, 4, 50, true),
                new Position(100, 5, 100, true)
            };
            var firstElementPlane = PlaneEnum.ZX;

            var mockDataSecondElement = new[]
            {
                new Position(6, 6, 6, true), new Position(6, 6, 6, true),
                new Position(6, 6, 6, true), new Position(6, 6, 6, true),
                new Position(6, 6, 6, true)
            };
            var secondElementPlane = PlaneEnum.XY;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfaceParalellismMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((ErrorResult)calculate).Message.Equals("Druga płaszczyzna: Wybrane pomiary są zbyt blisko siebie lub wykonane w linii prostej."));
        }

        [TestMethod]
        public void When_BothElementsErrorResult_Expect_ErrorResults()
        {
            // Arrange
            var mockDataFirstElement = new[]
             {
                new Position(6, 6, 6, true), new Position(6, 6, 6, true),
                new Position(6, 6, 6, true), new Position(6, 6, 6, true),
                new Position(6, 6, 6, true)
             };
            var firstElementPlane = PlaneEnum.XY;

            var mockDataSecondElement = new[]
            {
                new Position(6, 6, 6, true), new Position(6, 6, 6, true),
                new Position(6, 6, 6, true), new Position(6, 6, 6, true),
                new Position(6, 6, 6, true)
            };
            var secondElementPlane = PlaneEnum.YZ;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfaceParalellismMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((ErrorResult)calculate).Message.Equals("Pierwsza płaszczyzna: Wybrane pomiary są zbyt blisko siebie lub wykonane w linii prostej. Druga płaszczyzna: Wybrane pomiary są zbyt blisko siebie lub wykonane w linii prostej."));
        }

        [TestMethod]
        public void When_VeryClose_Expect_ErrorResults()
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
            var secondElementPlane = PlaneEnum.XY;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfacePerpendicularityMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
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
        public void When_A1A2Zero_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(1, 1, 1, true), new Position(100, 1, 1, true),
                new Position(1, 100, 1, true), new Position(50, 50, 1, true),
                new Position(100, 100, 1, true)
            };
            var firstElementPlane = PlaneEnum.XY;

            var mockDataSecondElement = new[]
            {
                new Position(1, 1, 1, true), new Position(1, 100, 1, true),
                new Position(1, 1, 100, true), new Position(1, 50, 50, true),
                new Position(1, 100, 100, true)
            };
            var secondElementPlane = PlaneEnum.YZ;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfacePerpendicularityMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A0 > 0.99 && ((SurfaceResult)calculateFirstElement).A0 < 1.01);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A1 > -0.01 && ((SurfaceResult)calculateFirstElement).A1 < 0.01);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A2 > -0.01 && ((SurfaceResult)calculateFirstElement).A2 < 0.01);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A0 > 0.99 && ((SurfaceResult)calculateSecondElement).A0 < 1.01);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A1 > -0.01 && ((SurfaceResult)calculateSecondElement).A1 < 0.01);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A2 > -0.01 && ((SurfaceResult)calculateSecondElement).A2 < 0.01);
            Assert.IsTrue(((SurfacePerpendicularityResult)calculate).Result > 1.56 && ((SurfacePerpendicularityResult)calculate).Result < 1.58);
        }

        [TestMethod]
        public void When_A2ZeroA1Not_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(1, 1, 1, true), new Position(100, 1, 2, true),
                new Position(1, 100, 3, true), new Position(50, 50, 4, true),
                new Position(100, 100, 5, true)
            };
            var firstElementPlane = PlaneEnum.XY;

            var mockDataSecondElement = new[]
            {
                new Position(1, 1, 1, true), new Position(1, 100, 1, true),
                new Position(1, 1, 100, true), new Position(1, 50, 50, true),
                new Position(1, 100, 100, true)
            };
            var secondElementPlane = PlaneEnum.YZ;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfacePerpendicularityMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((SurfacePerpendicularityResult)calculate).Result > 1.54 && ((SurfacePerpendicularityResult)calculate).Result < 1.56);
        }

        [TestMethod]
        public void When_ResultsPerpendicularXYYZ_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(1, 1, 1, true), new Position(100, 1, 2, true),
                new Position(1, 100, 3, true), new Position(50, 50, 4, true),
                new Position(100, 100, 5, true)
            };
            var firstElementPlane = PlaneEnum.XY;

            var mockDataSecondElement = new[]
            {
                new Position(1, 1, 1, true), new Position(2, 100, 1, true),
                new Position(3, 1, 100, true), new Position(4, 50, 50, true),
                new Position(5, 100, 100, true)
            };
            var secondElementPlane = PlaneEnum.YZ;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfacePerpendicularityMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A0 > 0.96 && ((SurfaceResult)calculateFirstElement).A0 < 0.98);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A1 > 0.01 && ((SurfaceResult)calculateFirstElement).A1 < 0.02);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A2 > 0.02 && ((SurfaceResult)calculateFirstElement).A2 < 0.03);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A0 > 0.96 && ((SurfaceResult)calculateSecondElement).A0 < 0.98);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A1 > 0.01 && ((SurfaceResult)calculateSecondElement).A1 < 0.02);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A2 > 0.02 && ((SurfaceResult)calculateSecondElement).A2 < 0.03);
            Assert.IsTrue(((SurfacePerpendicularityResult)calculate).Result > 1.52 && ((SurfacePerpendicularityResult)calculate).Result < 1.54);
        }

        [TestMethod]
        public void When_ResultsPerpendicularYZXY_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(1, 1, 1, true), new Position(2, 100, 1, true),
                new Position(3, 1, 100, true), new Position(4, 50, 50, true),
                new Position(5, 100, 100, true)
            };
            var firstElementPlane = PlaneEnum.YZ;

            var mockDataSecondElement = new[]
            {
                new Position(1, 1, 1, true), new Position(100, 1, 2, true),
                new Position(1, 100, 3, true), new Position(50, 50, 4, true),
                new Position(100, 100, 5, true)
            };
            var secondElementPlane = PlaneEnum.XY;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfacePerpendicularityMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A0 > 0.96 && ((SurfaceResult)calculateFirstElement).A0 < 0.98);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A1 > 0.01 && ((SurfaceResult)calculateFirstElement).A1 < 0.02);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A2 > 0.02 && ((SurfaceResult)calculateFirstElement).A2 < 0.03);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A0 > 0.96 && ((SurfaceResult)calculateSecondElement).A0 < 0.98);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A1 > 0.01 && ((SurfaceResult)calculateSecondElement).A1 < 0.02);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A2 > 0.02 && ((SurfaceResult)calculateSecondElement).A2 < 0.03);
            Assert.IsTrue(((SurfacePerpendicularityResult)calculate).Result > 1.52 && ((SurfacePerpendicularityResult)calculate).Result < 1.54);
        }
        [TestMethod]
        public void When_ResultsPerpendicularXYZX_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(1, 1, 1, true), new Position(100, 1, 2, true),
                new Position(1, 100, 3, true), new Position(50, 50, 4, true),
                new Position(100, 100, 5, true)
            };
            var firstElementPlane = PlaneEnum.XY;

            var mockDataSecondElement = new[]
            {
                new Position(1, 1, 1, true), new Position(1, 2, 100, true),
                new Position(100, 3, 1, true), new Position(50, 4, 50, true),
                new Position(100, 5, 100, true)
            };
            var secondElementPlane = PlaneEnum.ZX;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfacePerpendicularityMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A0 > 0.96 && ((SurfaceResult)calculateFirstElement).A0 < 0.98);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A1 > 0.01 && ((SurfaceResult)calculateFirstElement).A1 < 0.02);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A2 > 0.02 && ((SurfaceResult)calculateFirstElement).A2 < 0.03);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A0 > 0.96 && ((SurfaceResult)calculateSecondElement).A0 < 0.98);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A1 > 0.01 && ((SurfaceResult)calculateSecondElement).A1 < 0.02);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A2 > 0.02 && ((SurfaceResult)calculateSecondElement).A2 < 0.03);
            Assert.IsTrue(((SurfacePerpendicularityResult)calculate).Result > 1.51 && ((SurfacePerpendicularityResult)calculate).Result < 1.53);
        }

        [TestMethod]
        public void When_ResultsPerpendicularYZZX_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(1, 1, 1, true), new Position(2, 100, 1, true),
                new Position(3, 1, 100, true), new Position(4, 50, 50, true),
                new Position(5, 100, 100, true)
            };
            var firstElementPlane = PlaneEnum.YZ;

            var mockDataSecondElement = new[]
            {
                new Position(1, 1, 1, true), new Position(1, 2, 100, true),
                new Position(100, 3, 1, true), new Position(50, 4, 50, true),
                new Position(100, 5, 100, true)
            };
            var secondElementPlane = PlaneEnum.ZX;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<SurfacePerpendicularityMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A0 > 0.96 && ((SurfaceResult)calculateFirstElement).A0 < 0.98);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A1 > 0.01 && ((SurfaceResult)calculateFirstElement).A1 < 0.02);
            Assert.IsTrue(((SurfaceResult)calculateFirstElement).A2 > 0.02 && ((SurfaceResult)calculateFirstElement).A2 < 0.03);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A0 > 0.96 && ((SurfaceResult)calculateSecondElement).A0 < 0.98);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A1 > 0.01 && ((SurfaceResult)calculateSecondElement).A1 < 0.02);
            Assert.IsTrue(((SurfaceResult)calculateSecondElement).A2 > 0.02 && ((SurfaceResult)calculateSecondElement).A2 < 0.03);
            Assert.IsTrue(((SurfacePerpendicularityResult)calculate).Result > 1.53 && ((SurfacePerpendicularityResult)calculate).Result < 1.55);
        }
    }
}
