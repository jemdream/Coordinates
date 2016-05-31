using System;
using System.Collections;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;
using System.Collections.Generic;
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

        public MeasurementsPageViewModel(ICoordsOriginPartViewModel coordsOriginPartViewModel, ICoordsComputationPartViewModel coordsComputationPartViewModel)
        {
            CoordsOriginPartViewModel = coordsOriginPartViewModel;
            CoordsComputationPartViewModel = coordsComputationPartViewModel;
        }

        public ICoordsComputationPartViewModel CoordsComputationPartViewModel { get; }
        public ICoordsOriginPartViewModel CoordsOriginPartViewModel { get; }

        public ICommand GoToMeasurement => _goToMeasurement ?? (_goToMeasurement = new DelegateCommand(() =>
        {
            CoordsComputationPartViewModel.SelectedMeasurementType = CoordsOriginPartViewModel.SelectedMeasurementType;
            SelectedTabIndex = 1;
        }));

    }
}