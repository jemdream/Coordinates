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
        private int _selectedTabIndex;
        public ICommand GoToMeasurement { get; set; }
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { Set(ref _selectedTabIndex, value); }
        }

        public MeasurementsPageViewModel(ICoordsOriginPartViewModel coordsOriginPartViewModel, ICoordsComputationPartViewModel coordsComputationPartViewModel)
        {
            CoordsOriginPartViewModel = coordsOriginPartViewModel;
            CoordsComputationPartViewModel = coordsComputationPartViewModel;

            GoToMeasurement = new DelegateCommand(() =>
            {
                CoordsComputationPartViewModel.SelectedMeasurementType = CoordsOriginPartViewModel.SelectedMeasurementType;
                SelectedTabIndex = 1;
            });
        }

        public ICoordsComputationPartViewModel CoordsComputationPartViewModel { get; }

        public ICoordsOriginPartViewModel CoordsOriginPartViewModel { get; }

        #region Navigation

        #endregion
    }
}