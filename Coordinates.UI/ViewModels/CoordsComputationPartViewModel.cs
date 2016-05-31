using Coordinates.UI.Models;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CoordsComputationPartViewModel : ViewModelBase, ICoordsComputationPartViewModel
    {
        private string _xAxisCurrentValue = "0.000";
        private string _yAxisCurrentValue = "0.000";
        private string _zAxisCurrentValue = "0.000";

        public CoordsComputationPartViewModel()
        {
            
        }

        public MeasurementTypeModel SelectedMeasurementType { get; set; }

        public string XAxisCurrentValue
        {
            get { return _xAxisCurrentValue; }
        }
        public string YAxisCurrentValue
        {
            get { return _yAxisCurrentValue; }
        }
        public string ZAxisCurrentValue
        {
            get { return _zAxisCurrentValue; }
        }
    }
}