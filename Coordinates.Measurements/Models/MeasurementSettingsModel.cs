using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Models
{
    public class MeasurementSettingsModel
    {
        public IMeasurementMethod MeasurementMethodType { get; set; }
        public GaugePosition AxisBaseValuesModel { get; set; }
    }
}