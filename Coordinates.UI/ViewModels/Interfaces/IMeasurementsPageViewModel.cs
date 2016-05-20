using System;
using Windows.Devices.Bluetooth.Advertisement;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface IMeasurementsPageViewModel
    {
        ICoordsOriginPartViewModel CoordsOriginPartViewModel { get; }
        ICoordsComputationPartViewModel CoordsComputationPartViewModel { get; }
    }
}