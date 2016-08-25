using System.Collections.Generic;
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

        public IEventAggregator EventAggregator { get; private set; }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { Set(ref _selectedTabIndex, value); }
        }

        public MeasurementsPageViewModel(ICoordsCalibrationPartViewModel coordsCalibrationPartViewModel,
            ICoordsMeasurementPartViewModel coordsMeasurementPartViewModel, IEventAggregator eventAggregator)
        //, IConnectionService mockConnectionService/* TODO MOCK CONNECTION */)
        {
            EventAggregator = eventAggregator;

            // Ignore parameter, just invoke index change
            EventAggregator
                .GetEvent<GoBackMeasurementMsg>()
                .Subscribe(sender =>
                {
                    var index = _measurementViewModelBaseOrder.IndexOf(sender);
                    if (index >= 0) SelectedTabIndex--;
                });

            EventAggregator
                .GetEvent<GoNextMeasurementMsg>()
                .Subscribe(sender =>
                {
                    var index = _measurementViewModelBaseOrder.IndexOf(sender);
                    if (index < _measurementViewModelBaseOrder.Count) SelectedTabIndex++;
                });

            _measurementViewModelBaseOrder = new List<System.Type>
            {
                typeof(ICoordsCalibrationPartViewModel),
                typeof(ICoordsMeasurementPartViewModel)
            };

            //MockingDataService = (MockDeviceService)mockConnectionService; // TODO MOCK CONNECTION

            CoordsCalibrationPartViewModel = coordsCalibrationPartViewModel;
            CoordsMeasurementPartViewModel = coordsMeasurementPartViewModel;
        }
        
        //public MockDeviceService MockingDataService { get; set; } // TODO MOCK CONNECTION

        // TODO NAVIGATION in pivot selected item bind to viewmodel and introduce datatemplates; now mocked order
        private readonly List<System.Type> _measurementViewModelBaseOrder;

        public ICoordsMeasurementPartViewModel CoordsMeasurementPartViewModel { get; }
        public ICoordsCalibrationPartViewModel CoordsCalibrationPartViewModel { get; }

        //private void NavigateToComputation() => SelectedTabIndex = 1;
    }
}