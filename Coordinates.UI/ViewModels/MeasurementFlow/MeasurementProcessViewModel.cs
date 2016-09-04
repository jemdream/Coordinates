using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<ContactPosition> ContactPositions { get; }
        int RequiredMeasurementCount { get; }
        int LeftMeasurementCount { get; }

    }
    public class MeasurementProcessViewModel : MeasurementViewModelBase, IMeasurementProcessViewModel
    {
        private ObservableCollection<ContactPosition> _contactPositions;
        private int _requiredMeasurementCount;

        public MeasurementProcessViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
            : base(eventAggregator, measurementManager)
        {
            measurementManager.RawContactPositions
                .OnAdd
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(position =>
                {
                    ContactPositions.Insert(0, position);
                    ModelsUpdated();
                });

            measurementManager.RawContactPositions
                .OnRemove
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(position =>
                {
                    ContactPositions.Remove(position);
                    ModelsUpdated();
                });

            ContactPositions = new ObservableCollection<ContactPosition>(MeasurementManager.RawContactPositions);
        }
        public override string Title { get; } = "Pomiar";

        public ObservableCollection<ContactPosition> ContactPositions
        {
            get { return _contactPositions; }
            private set { Set(ref _contactPositions, value); }
        }

        public int RequiredMeasurementCount
        {
            get { return _requiredMeasurementCount; }
            private set { Set(ref _requiredMeasurementCount, value); }
        }

        public int LeftMeasurementCount => ContactPositions.Count;

        protected override bool CanOnNext()
        {
            return ContactPositions.Count >= MeasurementManager.SelectedMeasurementMethod?.RequiredMeasurementCount[0];
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
                MeasurementManager.ResetMeasurementData();
                ContactPositions = new ObservableCollection<ContactPosition>(MeasurementManager.RawContactPositions);
            }

            return returnConfirmed;
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