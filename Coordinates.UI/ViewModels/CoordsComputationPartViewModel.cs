using Coordinates.UI.Models;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CoordsComputationPartViewModel : ViewModelBase, ICoordsComputationPartViewModel
    {
        public MeasurementTypeModel SelectedMeasurementType { get; set; }
    }
}