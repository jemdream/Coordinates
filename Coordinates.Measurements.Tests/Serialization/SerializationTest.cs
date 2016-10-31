using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Helpers.Serialization;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json;
using static Coordinates.Measurements.Tests.TestHelpers.TestHelpers;

namespace Coordinates.Measurements.Tests.Serialization
{
    [TestClass]
    public class SerializationTest
    {
        [TestMethod]
        public void When_JsonSerializingParallel_Expect_StringOutput()
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
            var measurement = PrepareMeasurementMethodModel<SurfaceParalellismMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);
            
            var serialized = JsonConvert.SerializeObject(measurement, Formatting.Indented);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(serialized));
        }

        [TestMethod]
        public void When_CsvSerializingParallel_Expect_StringOutput()
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
            var measurement = PrepareMeasurementMethodModel<SurfaceParalellismMeasurementMethod>(mockDataFirstElement, firstElementPlane, mockDataSecondElement, secondElementPlane);

            var serialized = measurement.AsReadableString();

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(serialized.ToString()));
        }
    }
}