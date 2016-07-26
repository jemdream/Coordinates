using System.Collections.Generic;
using System.Windows.Input;
using Coordinates.Models.DTO;
using Coordinates.UI.ViewModels.MeasurementViewModels;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICoordsCalibrationPartViewModel
    {
        GaugePosition InitialCoordinates { get; set; }
        ICommand GoToMeasurement { get; }
        ICollection<IMeasurementTypeViewModel> MeasurementTypes { get; }
        IMeasurementTypeViewModel SelectedMeasurementTypeViewModel { get; set; }
    }
}