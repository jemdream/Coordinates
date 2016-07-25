using Coordinates.Models.DTO;
using Coordinates.UI.ViewModels.MeasurementViewModels;

namespace Coordinates.UI.Models
{
    public class MeasurementSettingsModel
    {
        public IMeasurementTypeViewModel MeasurementType { get; set; }
        public GaugePosition AxisBaseValuesModel { get; set; }
    }
}