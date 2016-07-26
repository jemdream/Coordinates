﻿using System.Collections.Generic;
using System.Windows.Input;
using Coordinates.Measurements.Models;
using Coordinates.Models.DTO;
using Coordinates.UI.Messages;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CoordsOriginPartViewModel : ViewModelBase, ICoordsOriginPartViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private ICommand _goToMeasurement;
        private IMeasurementTypeViewModel _selectedMeasurementTypeViewModel;
        private GaugePosition _initialCoordinates = new GaugePosition();

        public CoordsOriginPartViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            MeasurementTypes = new List<IMeasurementTypeViewModel>
            {
                new FlatnessMeasurementViewModel(),
                new RoundnessMeasurementViewModel()
            };
        }

        public ICollection<IMeasurementTypeViewModel> MeasurementTypes { get; set; }

        public IMeasurementTypeViewModel SelectedMeasurementTypeViewModel
        {
            get { return _selectedMeasurementTypeViewModel; }
            set
            {
                if (Set(ref _selectedMeasurementTypeViewModel, value))
                    ((DelegateCommand) GoToMeasurement).RaiseCanExecuteChanged();
            }
        }

        public GaugePosition InitialCoordinates
        {
            get { return _initialCoordinates; }
            set { Set(ref _initialCoordinates, value); }
        }

        public ICommand GoToMeasurement => _goToMeasurement ?? (_goToMeasurement = new DelegateCommand(() =>
        {
            _eventAggregator
                .GetEvent<NewMeasurementMessage>()
                .Publish(new MeasurementSettingsModel
                {
                    // TODO: Replace with enum / [Service Project]
                    //MeasurementType = SelectedMeasurementTypeViewModel,
                    // Rewriting values into new instance
                    AxisBaseValuesModel = new GaugePosition
                    {
                        X = InitialCoordinates.X,
                        Y = InitialCoordinates.Y,
                        Z = InitialCoordinates.Z,
                    }
                });
        }, () => SelectedMeasurementTypeViewModel != null));
    }
}