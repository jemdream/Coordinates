using System.Collections.Generic;
using System.Windows.Input;
using Coordinates.UI.Models;
using Coordinates.UI.ViewModels.MeasurementViewModels;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICoordsOriginPartViewModel
    {
        CoordinatesModel InitialCoordinates { get; set; }
        ICommand GoToMeasurement { get; }
        ICollection<IMeasurementTypeViewModel> MeasurementTypes { get; }
        IMeasurementTypeViewModel SelectedMeasurementTypeViewModel { get; set; }
    }
}