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
    public interface IMeasurementSelectionViewModel : IMeasurementViewModelBase
    {
        ObservableCollection<ContactPosition> ContactPositions { get; }
        ObservableList<ContactPosition> SelectedPositions { get; }
        int RequiredMeasurementCount { get; }
        int LeftMeasurementCount { get; }
    }
    public class MeasurementSelectionViewModel : MeasurementViewModelBase, IMeasurementSelectionViewModel
    {
        private int _requiredMeasurementCount;

        public MeasurementSelectionViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
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
        public override string Title { get; } = "Wybór pomiarów";

        public ObservableCollection<ContactPosition> ContactPositions { get; } = new ObservableCollection<ContactPosition>();
        public ObservableList<ContactPosition> SelectedPositions { get; }

        public int LeftMeasurementCount => SelectedPositions.Count;

        public int RequiredMeasurementCount
        {
            get { return _requiredMeasurementCount; }
            private set { Set(ref _requiredMeasurementCount, value); }
        }

        protected override bool CanOnNext()
        {
            return SelectedPositions.Count >= MeasurementManager.SelectedMeasurementMethod?.RequiredMeasurementCount[0];
        }

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
