using System.Runtime.InteropServices.ComTypes;
using Coordinates.UI.Models;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICoordsComputationPartViewModel
    {
        MeasurementTypeModel SelectedMeasurementType { get; set; }
        string XAxisCurrentValue { get; }
        string YAxisCurrentValue { get; }
        string ZAxisCurrentValue { get; }
    }
}