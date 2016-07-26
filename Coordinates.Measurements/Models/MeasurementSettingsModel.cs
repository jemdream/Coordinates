using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Models
{
    public class MeasurementSettingsModel
    {
        public IMeasurement MeasurementType { get; set; }
        public GaugePosition AxisBaseValuesModel { get; set; }
    }
}