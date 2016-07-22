using Coordinates.UI.ViewModels.MeasurementViewModels;

namespace Coordinates.UI.Models
{
    public class MeasurementSettingsModel
    {
        public IMeasurementTypeViewModel MeasurementType { get; set; }
        public CoordinatesModel AxisBaseValuesModel { get; set; }
    }
}