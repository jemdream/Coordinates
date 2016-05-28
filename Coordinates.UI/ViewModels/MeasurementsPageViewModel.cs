using System;
using System.Collections;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;
using System.Collections.Generic;

namespace Coordinates.UI.ViewModels
{
    public class MeasurementsPageViewModel : ViewModelBase, IMeasurementsPageViewModel
    {
        public MeasurementsPageViewModel(ICoordsOriginPartViewModel coordsOriginPartViewModel, ICoordsComputationPartViewModel coordsComputationPartViewModel)
        {
            CoordsOriginPartViewModel = coordsOriginPartViewModel;
            CoordsComputationPartViewModel = coordsComputationPartViewModel;
        }

        public ICoordsComputationPartViewModel CoordsComputationPartViewModel { get; }

        public ICoordsOriginPartViewModel CoordsOriginPartViewModel { get; }

        #region Navigation

        #endregion
    }
}