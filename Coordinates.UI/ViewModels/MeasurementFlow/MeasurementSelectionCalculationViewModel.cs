using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;
using Coordinates.UI.ViewModels.Interfaces;
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
        private int _requiredMeasurementCount;

        public MeasurementSelectionCalculationViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
            : base(eventAggregator, measurementManager)
        {
            measurementManager.RawContactPositions
                .OnAdd
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(position => ContactPositions.Insert(0, position));

            measurementManager.RawContactPositions
                .OnRemove
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(position => ContactPositions.Remove(position));

            SelectedPositions = measurementManager.SelectedPositions;

            SelectedPositions
                .OnAdd
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(_ => ModelsUpdated());

            SelectedPositions
                .OnRemove
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(_ => ModelsUpdated());
        }
        public override string Title { get; } = "Wybór pomiarów i obliczenia";

        public ObservableCollection<Position> ContactPositions { get; } = new ObservableCollection<Position>();
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
            RequiredMeasurementCount = MeasurementManager.SelectedMeasurementMethod.RequiredMeasurementCount[0];

            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
