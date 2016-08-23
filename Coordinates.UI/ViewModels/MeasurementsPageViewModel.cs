using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.Devices;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;
using Coordinates.UI.Messages;
using Prism.Events;

namespace Coordinates.UI.ViewModels
{
    public class MeasurementsPageViewModel : ViewModelBase, IMeasurementsPageViewModel
    {
        private int _selectedTabIndex;

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { Set(ref _selectedTabIndex, value); }
        }

        public MeasurementsPageViewModel(ICoordsCalibrationPartViewModel coordsCalibrationPartViewModel,
            ICoordsMeasurementPartViewModel coordsMeasurementPartViewModel, IEventAggregator eventAggregator)
            //, IConnectionService mockConnectionService/* TODO MOCK CONNECTION */)
        {
            // Ignore parameter, just invoke index change
            eventAggregator
                .GetEvent<NewMeasurementMessage>()
                .Subscribe(_ => NavigateToComputation());

            //MockingDataService = (MockDeviceService)mockConnectionService; // TODO MOCK CONNECTION

            CoordsCalibrationPartViewModel = coordsCalibrationPartViewModel;
            CoordsMeasurementPartViewModel = coordsMeasurementPartViewModel;
        }

        //public MockDeviceService MockingDataService { get; set; } // TODO MOCK CONNECTION

        public ICoordsMeasurementPartViewModel CoordsMeasurementPartViewModel { get; }
        public ICoordsCalibrationPartViewModel CoordsCalibrationPartViewModel { get; }
        private void NavigateToComputation() => SelectedTabIndex = 1;
    }
}