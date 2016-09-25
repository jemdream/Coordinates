using System;
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
        // TODO [MultiMeasure] move out
        //ObservableCollection<Position> ContactPositions { get; }
        // TODO [MultiMeasure] move out
        //ObservableList<Position> SelectedPositions { get; }
        int RequiredMeasurementCount { get; }
        int MeasurementCount { get; }
    }
    public class MeasurementSelectionCalculationViewModel : MeasurementViewModelBase, IMeasurementSelectionCalculationViewModel
    {
        private readonly IMeasurementMethodViewModel _measurementMethodViewModel;
        private int _requiredMeasurementCount;

        public MeasurementSelectionCalculationViewModel(IEventAggregator eventAggregator,
            IMeasurementMethodViewModel measurementMethodViewModel) : base(eventAggregator)//, measurementManager)
        {
            _measurementMethodViewModel = measurementMethodViewModel;

            // TODO [MultiMeasure] move out
            //measurementManager.RawContactPositions
            //    .OnAdd
            //    .ObserveOn(SynchronizationContext.Current)
            //    .Subscribe(position => ContactPositions.Insert(0, position));

            // TODO [MultiMeasure] move out
            //measurementManager.RawContactPositions
            //    .OnRemove
            //    .ObserveOn(SynchronizationContext.Current)
            //    .Subscribe(position => ContactPositions.Remove(position));

            // TODO [MultiMeasure] move out
            //SelectedPositions = measurementManager.SelectedPositions;

            // TODO [MultiMeasure] move out
            // On add and on remove, update controls
            //SelectedPositions
            //    .OnAdd
            //    .Concat(SelectedPositions.OnRemove)
            //    .ObserveOn(SynchronizationContext.Current)
            //    .Subscribe(_ => ModelsUpdated());

            // TODO [MultiMeasure] move out
            //ContactPositions = new ObservableCollection<Position>(MeasurementManager.RawContactPositions);
        }
        public override string Title { get; } = "Wybór pomiarów i obliczenia";

        // TODO [MultiMeasure] move out
        //public ObservableCollection<Position> ContactPositions { get; }
        // TODO [MultiMeasure] move out
        //public ObservableList<Position> SelectedPositions { get; }

        public int MeasurementCount => 0;// TODO [MultiMeasure] move out: SelectedPositions.Count;

        public int RequiredMeasurementCount
        {
            get { return _requiredMeasurementCount; }
            private set { Set(ref _requiredMeasurementCount, value); }
        }

        protected override bool CanOnNext() => false;

        // TODO [MultiMeasure] move out
        //return SelectedPositions.Count >= MeasurementManager.SelectedMeasurementMethod?.RequiredMeasurementCount[0];

        private void ModelsUpdated()
        {
            UpdateNavigationCommands();
            RaisePropertyChanged(() => MeasurementCount);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            RequiredMeasurementCount = 5; //_measurementMethodViewModel
            // TODO [MultiMeasure] move out REPLACE WITH VM DATA
            //RequiredMeasurementCount = MeasurementManager.SelectedMeasurementMethod.Elements.FirstOrDefault().RequiredMeasurementCount;

            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
