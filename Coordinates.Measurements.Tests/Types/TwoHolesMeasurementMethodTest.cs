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
    public class TwoHolesMeasurementMethodTest
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
            var measurements = PrepareMeasurementMethodModel<TwoHolesMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
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
            var measurements = PrepareMeasurementMethodModel<TwoHolesMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
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
            var secondElementPlane = PlaneEnum.YZ;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<TwoHolesMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((ErrorResult)calculateFirstElement).Message.Equals("Wybrane pomiary są zbyt blisko siebie"));
            Assert.IsTrue(((ErrorResult)calculateSecondElement).Message.Equals("Wybierz odpowiednią liczbę pomiarów."));
            Assert.IsTrue(((ErrorResult)calculate).Message.Equals("Nie można policzyć jednego lub obu elementów."));
        }

        [TestMethod]
        public void When_ResultsTwoHolesXY_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(4, 2, 1, true), new Position(2, 6, 1, true),
                new Position(6, 8, 1, true), new Position(4, 13, 1, true),
                new Position(2.5, 3, 1, true)
            };
            var firstElementPlane = PlaneEnum.XY;

            var mockDataSecondElement = new[]
            {
                new Position(5, 3, 1, true), new Position(3, 7, 1, true),
                new Position(7, 9, 1, true), new Position(5, 14, 1, true),
                new Position(3.5, 4, 1, true)
            };
            var secondElementPlane = PlaneEnum.XY;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<TwoHolesMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((HoleResult)calculateFirstElement).X0 > 3.47 && ((HoleResult)calculateFirstElement).X0 < 3.49);
            Assert.IsTrue(((HoleResult)calculateFirstElement).Y0 > 7.51 && ((HoleResult)calculateFirstElement).Y0 < 7.53);
            Assert.IsTrue(((HoleResult)calculateFirstElement).Z0 > 0.99 && ((HoleResult)calculateFirstElement).Z0 < 1.01);
            Assert.IsTrue(((HoleResult)calculateFirstElement).R > 7.31 && ((HoleResult)calculateFirstElement).R < 7.33);
            Assert.IsTrue(((HoleResult)calculateSecondElement).X0 > 4.47 && ((HoleResult)calculateSecondElement).X0 < 4.49);
            Assert.IsTrue(((HoleResult)calculateSecondElement).Y0 > 8.51 && ((HoleResult)calculateSecondElement).Y0 < 8.53);
            Assert.IsTrue(((HoleResult)calculateSecondElement).Z0 > 0.99 && ((HoleResult)calculateSecondElement).Z0 < 1.01);
            Assert.IsTrue(((HoleResult)calculateSecondElement).R > 7.31 && ((HoleResult)calculateSecondElement).R < 7.33);
        }

        [TestMethod]
        public void When_ResultsTwoHolesYZ_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(1, 4, 2, true), new Position(1, 2, 6, true),
                new Position(1, 6, 8, true), new Position(1, 4, 13, true),
                new Position(1, 2.5, 3, true)
            };
            var firstElementPlane = PlaneEnum.YZ;

            var mockDataSecondElement = new[]
            {
                new Position(1, 5, 3, true), new Position(1, 3, 7, true),
                new Position(1, 7, 9, true), new Position(1, 5, 14, true),
                new Position(1, 3.5, 4, true)
            };
            var secondElementPlane = PlaneEnum.YZ;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<TwoHolesMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            Assert.IsTrue(((HoleResult)calculateFirstElement).X0 > 0.99 && ((HoleResult)calculateFirstElement).X0 < 1.01);
            Assert.IsTrue(((HoleResult)calculateFirstElement).Y0 > 3.47 && ((HoleResult)calculateFirstElement).Y0 < 3.49);
            Assert.IsTrue(((HoleResult)calculateFirstElement).Z0 > 7.52 && ((HoleResult)calculateFirstElement).Z0 < 7.53);
            Assert.IsTrue(((HoleResult)calculateFirstElement).R > 7.31 && ((HoleResult)calculateFirstElement).R < 7.34);
            Assert.IsTrue(((HoleResult)calculateSecondElement).X0 > 0.99 && ((HoleResult)calculateSecondElement).X0 < 1.01);
            Assert.IsTrue(((HoleResult)calculateSecondElement).Y0 > 4.47 && ((HoleResult)calculateSecondElement).Y0 < 4.49);
            Assert.IsTrue(((HoleResult)calculateSecondElement).Z0 > 8.51 && ((HoleResult)calculateSecondElement).Z0 < 8.53);
            Assert.IsTrue(((HoleResult)calculateSecondElement).R > 7.31 && ((HoleResult)calculateSecondElement).R < 7.34);
        }

        [TestMethod]
        public void When_ResultsTwoHolesZX_Expect_Values()
        {
            // Arrange
            var mockDataFirstElement = new[]
            {
                new Position(2, 1, 4, true), new Position(6, 1, 2, true),
                new Position(8, 1, 6, true), new Position(13, 1, 4, true),
                new Position(3, 1, 2.5, true)
            };
            var firstElementPlane = PlaneEnum.ZX;

            var mockDataSecondElement = new[]
            {
                new Position(3, 1, 5, true), new Position(7, 1, 3, true),
                new Position(9, 1, 7, true), new Position(14, 1, 5, true),
                new Position(4, 1, 3.5, true)
            };
            var secondElementPlane = PlaneEnum.ZX;

            // Prepare object with data from above
            var measurements = PrepareMeasurementMethodModel<TwoHolesMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            var arrayOfElements = measurements.Elements.ToArray();

            // Act
            var calculateFirstElement = arrayOfElements[0].Calculate();
            var calculateSecondElement = arrayOfElements[1].Calculate();
            var calculate = measurements.Calculate();

            // Assert
            
            Assert.IsTrue(((HoleResult)calculateFirstElement).X0 > 7.51 && ((HoleResult)calculateFirstElement).X0 < 7.53);
            Assert.IsTrue(((HoleResult)calculateFirstElement).Y0 > 0.99 && ((HoleResult)calculateFirstElement).Y0 < 1.01);
            Assert.IsTrue(((HoleResult)calculateFirstElement).Z0 > 3.47 && ((HoleResult)calculateFirstElement).Z0 < 3.49);
            Assert.IsTrue(((HoleResult)calculateFirstElement).R > 7.31 && ((HoleResult)calculateFirstElement).R < 7.34);
            Assert.IsTrue(((HoleResult)calculateSecondElement).X0 > 8.51 && ((HoleResult)calculateSecondElement).X0 < 8.53);
            Assert.IsTrue(((HoleResult)calculateSecondElement).Y0 > 0.99 && ((HoleResult)calculateSecondElement).Y0 < 1.01);
            Assert.IsTrue(((HoleResult)calculateSecondElement).Z0 > 4.47 && ((HoleResult)calculateSecondElement).Z0 < 4.49);
            Assert.IsTrue(((HoleResult)calculateSecondElement).R > 7.31 && ((HoleResult)calculateSecondElement).R < 7.34);
        }
    }
}