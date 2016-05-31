using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;
using System.Windows.Input;

namespace Coordinates.UI.ViewModels
{
    public class MeasurementsPageViewModel : ViewModelBase, IMeasurementsPageViewModel
    {
        private DelegateCommand _goToMeasurement;
        private int _selectedTabIndex;

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { Set(ref _selectedTabIndex, value); }
        }

        public MeasurementsPageViewModel(ICoordsOriginPartViewModel coordsOriginPartViewModel,
            ICoordsComputationPartViewModel coordsComputationPartViewModel)
        {
            CoordsOriginPartViewModel = coordsOriginPartViewModel;
            CoordsComputationPartViewModel = coordsComputationPartViewModel;
        }

        public ICoordsComputationPartViewModel CoordsComputationPartViewModel { get; }
        public ICoordsOriginPartViewModel CoordsOriginPartViewModel { get; }

        public ICommand GoToMeasurement => _goToMeasurement ?? (_goToMeasurement = new DelegateCommand(() =>
        {
            if (CoordsOriginPartViewModel.SelectedMeasurementTypeViewModel == null) return; // Temporary can execute

            CoordsComputationPartViewModel.SelectedMeasurementTypeViewModel =
                CoordsOriginPartViewModel.SelectedMeasurementTypeViewModel;
            SelectedTabIndex = 1;
        }));// }, () => CoordsOriginPartViewModel.SelectedMeasurementTypeViewModel != null)); // _ can execute - replace when messaging introduced. raise in selected type vm setter: ((DelegateCommand)GoToMeasurement).RaiseCanExecuteChanged();
    }
}