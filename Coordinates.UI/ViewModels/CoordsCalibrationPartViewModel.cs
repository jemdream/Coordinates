using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;
using Coordinates.UI.Messages;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CoordsCalibrationPartViewModel : ViewModelBase, ICoordsCalibrationPartViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeasurementManager _measurementManager;

        private ICommand _goToMeasurement;
        private ICommand _setupInitialCoordinates;

        private Position _currentGaugePosition = new GaugePosition();
        private Position _initialGaugePosition = new GaugePosition();

        private IMeasurementMethod _selectedMeasurementMethod;

        public CoordsCalibrationPartViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
        {
            _eventAggregator = eventAggregator;
            _measurementManager = measurementManager;

            SetupMeasurementManager();
        }

        private void SetupMeasurementManager()
        {
            AvailableMeasurementMethods = _measurementManager.AvailableMeasurementMethods;

            _measurementManager.PositionSource
                .Subscribe(pos => CurrentGaugePosition = pos);
        }

        public IEnumerable<IMeasurementMethod> AvailableMeasurementMethods { get; set; }
        public IMeasurementMethod SelectedMeasurementMethod
        {
            get { return _selectedMeasurementMethod; }
            set
            {
                if (Set(ref _selectedMeasurementMethod, value))
                    ((DelegateCommand)GoToMeasurement).RaiseCanExecuteChanged();
            }
        }

        public Position CurrentGaugePosition
        {
            get { return _currentGaugePosition; }
            private set { Set(ref _currentGaugePosition, value); }
        }

        public Position InitialGaugePosition
        {
            get { return _initialGaugePosition; }
            private set { Set(ref _initialGaugePosition, value); }
        }

        public ICommand GoToMeasurement => _goToMeasurement ?? (_goToMeasurement = new DelegateCommand(() =>
        {
            // TODO Check for dirty and prompt if so
            _measurementManager.SetupNewMeasurement(SelectedMeasurementMethod, InitialGaugePosition);

            _eventAggregator
                .GetEvent<NewMeasurementMessage>()
                .Publish(Unit.Default); // Unit.Default is just an object - no data. just to publish something. a good practice.

        }, () => SelectedMeasurementMethod != null));

        public ICommand SetupInitialCoordinates => _setupInitialCoordinates ?? (_setupInitialCoordinates = new DelegateCommand(() =>
        {
            _initialGaugePosition = _currentGaugePosition;
        }, () => _currentGaugePosition != null));

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            SelectedMeasurementMethod = _measurementManager.SelectedMeasurementMethod; 

            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}