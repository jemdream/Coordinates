using System.Collections.Generic;
using Coordinates.UI.ViewModels.MeasurementViewModels;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICoordsOriginPartViewModel
    {
        ICollection<IMeasurementTypeViewModel> MeasurementTypes { get; }
        IMeasurementTypeViewModel SelectedMeasurementTypeViewModel { get; set; }
    }
}