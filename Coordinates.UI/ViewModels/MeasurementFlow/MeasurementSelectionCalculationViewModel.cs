﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementSelectionCalculationViewModel : IMeasurementViewModelBase
    {
        ObservableCollection<Position> ContactPositions { get; }
        ObservableList<Position> SelectedPositions { get; }
        int RequiredMeasurementCount { get; }
        int LeftMeasurementCount { get; }
    }
    public class MeasurementSelectionCalculationViewModel : MeasurementViewModelBase, IMeasurementSelectionCalculationViewModel
    {
        private readonly IMeasurementMethodViewModel _measurementMethodViewModel;
        private int _requiredMeasurementCount;

        public MeasurementSelectionCalculationViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager,
            IMeasurementMethodViewModel measurementMethodViewModel) : base(eventAggregator, measurementManager)
        {
            _measurementMethodViewModel = measurementMethodViewModel;

            measurementManager.RawContactPositions
                .OnAdd
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(position => ContactPositions.Insert(0, position));

            measurementManager.RawContactPositions
                .OnRemove
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(position => ContactPositions.Remove(position));

            SelectedPositions = measurementManager.SelectedPositions;

            // On add and on remove, update controls
            SelectedPositions
                .OnAdd
                .Concat(SelectedPositions.OnRemove)
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(_ => ModelsUpdated());

            ContactPositions = new ObservableCollection<Position>(MeasurementManager.RawContactPositions);
        }
        public override string Title { get; } = "Wybór pomiarów i obliczenia";

        public ObservableCollection<Position> ContactPositions { get; }
        public ObservableList<Position> SelectedPositions { get; }

        public int LeftMeasurementCount => SelectedPositions.Count;

        public int RequiredMeasurementCount
        {
            get { return _requiredMeasurementCount; }
            private set { Set(ref _requiredMeasurementCount, value); }
        }

        protected override bool CanOnNext() => false;
        
        //return SelectedPositions.Count >= MeasurementManager.SelectedMeasurementMethod?.RequiredMeasurementCount[0];
        
        private void ModelsUpdated()
        {
            UpdateCommands();
            RaisePropertyChanged(() => LeftMeasurementCount);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            RequiredMeasurementCount = MeasurementManager.SelectedMeasurementMethod.Elements.FirstOrDefault().RequiredMeasurementCount;

            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
