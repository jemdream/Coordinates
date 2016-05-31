using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CoordsComputationPartViewModel : ViewModelBase, ICoordsComputationPartViewModel
    {
        private double _xAxisCurrentValue;
        private double _yAxisCurrentValue;
        private double _zAxisCurrentValue;
        private IMeasurementTypeViewModel _selectedMeasurementTypeViewModel;

        public IMeasurementTypeViewModel SelectedMeasurementTypeViewModel
        {
            get { return _selectedMeasurementTypeViewModel; }
            set { Set(ref _selectedMeasurementTypeViewModel, value); }
        }
        public double XAxisCurrentValue { get { return _xAxisCurrentValue; } set { Set(ref _xAxisCurrentValue, value); } }
        public double YAxisCurrentValue { get { return _yAxisCurrentValue; } set { Set(ref _yAxisCurrentValue, value); } }
        public double ZAxisCurrentValue { get { return _zAxisCurrentValue; } set { Set(ref _zAxisCurrentValue, value); } }
    }
}