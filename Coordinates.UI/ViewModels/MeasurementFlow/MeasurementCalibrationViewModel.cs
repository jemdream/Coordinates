using System;
using System.Windows.Input;
using Coordinates.Measurements;
using Coordinates.Models.DTO;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementCalibrationViewModel : IMeasurementViewModelBase
    {
        Position CurrentGaugePosition { get; }
        Position InitialGaugePosition { get; }
        ICommand SetupInitialCoordinates { get; }
    }

    public class MeasurementCalibrationViewModel : MeasurementViewModelBase, IMeasurementCalibrationViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeasurementManager _measurementManager;

        private DelegateCommand _goBackCommand;
        private DelegateCommand _goNextCommand;

        private ICommand _setupInitialCoordinates;

        private Position _currentGaugePosition = new GaugePosition();
        private Position _initialGaugePosition = new GaugePosition();

        public MeasurementCalibrationViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
        {
            _eventAggregator = eventAggregator;
            _measurementManager = measurementManager;

            SetupMeasurementManager();
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

        public override string Title { get; } = "Kalibracja";
        public override ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(() =>
        {
        }, () => true));
        public override ICommand GoNextCommand => _goNextCommand ?? (_goNextCommand = new DelegateCommand(() =>
        {
            _measurementManager.SetupCalibration(InitialGaugePosition);
        }, () => true));

        public ICommand SetupInitialCoordinates => _setupInitialCoordinates ?? (_setupInitialCoordinates = new DelegateCommand(() =>
        {
            InitialGaugePosition = _currentGaugePosition;
        }, () => _currentGaugePosition != null));

        private void SetupMeasurementManager()
        {
            _measurementManager.PositionSource
                .Subscribe(pos => CurrentGaugePosition = pos);
        }
    }
}
