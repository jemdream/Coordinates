using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.Models.DTO;
using Coordinates.UI.Messages;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementCalibrationViewModel : IMeasurementViewModelBase
    {
        Position CurrentGaugePosition { get; }
        ICommand SetupInitialCoordinates { get; }
    }

    public class MeasurementCalibrationViewModel : MeasurementViewModelBase, IMeasurementCalibrationViewModel
    {
        private readonly IMeasurementManager _measurementManager;
        private AwaitableDelegateCommand _setupInitialCoordinates;

        private Position _currentGaugePosition = Position.Default;

        public MeasurementCalibrationViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
            : base(eventAggregator)
        {
            _measurementManager = measurementManager;

            _measurementManager.PositionSource
                .Subscribe(pos => CurrentGaugePosition = pos);
        }

        public override string Title { get; } = "Kalibracja";

        public Position CurrentGaugePosition
        {
            get { return _currentGaugePosition; }
            private set { Set(ref _currentGaugePosition, value); }
        }

        public ICommand SetupInitialCoordinates => _setupInitialCoordinates ?? (_setupInitialCoordinates = new AwaitableDelegateCommand(async x =>
        {
            await Task.Run(() => _measurementManager.Calibrate());
        }, x => _currentGaugePosition != null));

        protected override async Task<bool> OnPrevious()
        {
            _measurementManager.ResetMeasurementData();
            return await Task.FromResult(true);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (_measurementManager.SelectedMeasurementMethod == null)
                EventAggregator
                        .GetEvent<ResetMeasurement>()
                        .Publish(null);

            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
