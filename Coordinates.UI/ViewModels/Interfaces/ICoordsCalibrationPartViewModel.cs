using System.Collections.Generic;
using System.Windows.Input;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICoordsCalibrationPartViewModel
    {
        Position CurrentGaugePosition { get; }
        Position InitialGaugePosition { get; }
        ICommand GoNextCommand { get; }
        ICommand SetupInitialCoordinates { get; }
        IEnumerable<IMeasurementMethod> AvailableMeasurementMethods { get; }
        IMeasurementMethod SelectedMeasurementMethod { get; set; }
    }
}