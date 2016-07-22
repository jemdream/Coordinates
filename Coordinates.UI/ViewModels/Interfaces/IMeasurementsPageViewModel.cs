using System.Windows.Input;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface IMeasurementsPageViewModel
    {
        ICoordsOriginPartViewModel CoordsOriginPartViewModel { get; }
        ICoordsComputationPartViewModel CoordsComputationPartViewModel { get; }
        int SelectedTabIndex { get; set; }
    }
}