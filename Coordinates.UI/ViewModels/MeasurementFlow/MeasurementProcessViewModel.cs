using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.Models.DTO;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementProcessViewModel : IMeasurementViewModelBase
    {
        // TODO [MultiMeasure] move out
        //ObservableCollection<Position> ContactPositions { get; }
        int RequiredMeasurementCount { get; }
        int LeftMeasurementCount { get; }
    }

    // TODO M delete all those subscriptions and fields, and just have IMeasurementMethod here. bind from XAML to model
    public class MeasurementProcessViewModel : MeasurementViewModelBase, IMeasurementProcessViewModel
    {
        private readonly IMeasurementManager _measurementManager;

        // TODO [MultiMeasure] move out
        //private ObservableCollection<Position> _contactPositions;
        private int _requiredMeasurementCount;

        public MeasurementProcessViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
            : base(eventAggregator)
        {
            _measurementManager = measurementManager;

            // TODO [MultiMeasure] move out
            //measurementManager.RawContactPositions
            //    .OnAdd
            //    .ObserveOn(SynchronizationContext.Current)
            //    .Subscribe(position =>
            //    {
            //        ContactPositions.Insert(0, position);
            //        ModelsUpdated();
            //    });

            // TODO [MultiMeasure] move out
            //measurementManager.RawContactPositions
            //    .OnRemove
            //    .ObserveOn(SynchronizationContext.Current)
            //    .Subscribe(position =>
            //    {
            //        ContactPositions.Remove(position);
            //        ModelsUpdated();
            //    });

            // TODO [MultiMeasure] move out
            //ContactPositions = new ObservableCollection<Position>(MeasurementManager.RawContactPositions);
        }

        public override string Title { get; } = "Pomiar";

        //public ObservableCollection<Position> ContactPositions
        //{
        //    get { return _contactPositions; }
        //    private set { Set(ref _contactPositions, value); }
        //}

        public int RequiredMeasurementCount
        {
            get { return _requiredMeasurementCount; }
            private set { Set(ref _requiredMeasurementCount, value); }
        }

        public int LeftMeasurementCount => 0; // TODO [MultiMeasure] move out//ContactPositions.Count;

        protected override bool CanOnNext()
        {
            return true;
            // TODO [MultiMeasure] move out
            //ContactPositions.Count >= MeasurementManager.SelectedMeasurementMethod.Elements.FirstOrDefault().RequiredMeasurementCount;
        }

        protected override async Task<bool> OnPrevious()
        {
            var dialog = new ContentDialog
            {
                Title = "Uwaga",
                Content = "Wszystkie pomiary zostaną usunięte.",
                PrimaryButtonText = "OK",
                SecondaryButtonText = "Anuluj"
            };

            var returnConfirmed = (await dialog.ShowAsync()).Equals(ContentDialogResult.Primary);

            if (returnConfirmed)
            {
                _measurementManager.ResetMeasurementData();
                // TODO [MultiMeasure] move out
                //ContactPositions = new ObservableCollection<Position>(MeasurementManager.RawContactPositions);
            }

            return returnConfirmed;
        }
        private void ModelsUpdated()
        {
            UpdateNavigationCommands();
            RaisePropertyChanged(() => LeftMeasurementCount);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            // TODO [MultiMeasure] move out
            RequiredMeasurementCount = 0; //MeasurementManager.SelectedMeasurementMethod.Elements.FirstOrDefault().RequiredMeasurementCount;

            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}