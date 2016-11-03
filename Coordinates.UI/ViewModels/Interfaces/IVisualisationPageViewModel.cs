using Coordinates.UI.ViewModels.MeasurementViewModels;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface IVisualisationPageViewModel
    {
        bool IsAvailable { get; }
        IMeasurementMethodViewModel MeasurementMethodViewModel { get; }
        bool RenderCharts { get; }
    }
}