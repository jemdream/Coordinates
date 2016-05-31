using Coordinates.UI.ViewModels.MeasurementViewModels;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICoordsComputationPartViewModel
    {
        IMeasurementTypeViewModel SelectedMeasurementTypeViewModel { get; set; }
        double XAxisCurrentValue { get; }
        double YAxisCurrentValue { get; }
        double ZAxisCurrentValue { get; }
    }
}