﻿using System;
using System.Threading.Tasks;
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
        ICommand SetupInitialCoordinates { get; }
    }

    public class MeasurementCalibrationViewModel : MeasurementViewModelBase, IMeasurementCalibrationViewModel
    {
        private AwaitableDelegateCommand _setupInitialCoordinates;

        private Position _currentGaugePosition = new GaugePosition();

        public MeasurementCalibrationViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
            : base(eventAggregator, measurementManager)
        {
            SetupMeasurementManager();
        }
        public override string Title { get; } = "Kalibracja";

        public Position CurrentGaugePosition
        {
            get { return _currentGaugePosition; }
            private set { Set(ref _currentGaugePosition, value); }
        }

        public ICommand SetupInitialCoordinates => _setupInitialCoordinates ?? (_setupInitialCoordinates = new AwaitableDelegateCommand(async x =>
        {
            await Task.Run(() => MeasurementManager.SetupCalibration());
        }, x => _currentGaugePosition != null));

        private void SetupMeasurementManager()
        {
            MeasurementManager.PositionSource
                .Subscribe(pos => CurrentGaugePosition = pos);
        }
    }
}
