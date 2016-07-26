using System.Windows.Input;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface IMeasurementsPageViewModel
    {
        ICoordsCalibrationPartViewModel CoordsCalibrationPartViewModel { get; }
        ICoordsMeasurementPartViewModel CoordsMeasurementPartViewModel { get; }
        int SelectedTabIndex { get; set; }
    }
}