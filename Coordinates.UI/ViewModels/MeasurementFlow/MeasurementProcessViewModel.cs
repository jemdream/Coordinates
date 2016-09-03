using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Coordinates.Measurements;
using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementProcessViewModel : IMeasurementViewModelBase
    {
        ObservableCollection<ContactPosition> ContactPositions { get; }
        ObservableList<ContactPosition> SelectedPositions { get; }
    }
    public class MeasurementProcessViewModel : MeasurementViewModelBase, IMeasurementProcessViewModel
    {
        public MeasurementProcessViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
            : base(eventAggregator, measurementManager)
        {
            measurementManager.RawContactPositions
                .OnAdd
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(position => ContactPositions.Add(position));

            measurementManager.RawContactPositions
                .OnRemove
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(position => ContactPositions.Remove(position));

            SelectedPositions = measurementManager.SelectedPositions;
        }
        public override string Title { get; } = "Pomiar";

        public ObservableCollection<ContactPosition> ContactPositions { get; } = new ObservableCollection<ContactPosition>();
        public ObservableList<ContactPosition> SelectedPositions { get; }

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

            if (returnConfirmed) MeasurementManager.ResetMeasurementData();

            return returnConfirmed;
        }
    }
}