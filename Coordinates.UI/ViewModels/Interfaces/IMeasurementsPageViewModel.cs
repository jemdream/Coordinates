namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface IMeasurementsPageViewModel
    {
        ICoordsCalibrationPartViewModel CoordsCalibrationPartViewModel { get; }
        ICoordsMeasurementPartViewModel CoordsMeasurementPartViewModel { get; }
        //MockDeviceService MockingDataService { get; set; } // TODO MOCK CONNECTION
        int SelectedTabIndex { get; set; }
    }
}