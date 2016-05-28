using System.Collections.Generic;
using System.Windows.Input;
using Coordinates.UI.Models;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICoordsOriginPartViewModel
    {
        ICollection<MeasurementTypeModel> MeasurementTypes { get; }
        MeasurementTypeModel SelectedMeasurementType { get; }
    }
}