using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Coordinates.Measurements;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementProcessViewModel : IMeasurementViewModelBase
    {
    }
    public class MeasurementProcessViewModel : MeasurementViewModelBase, IMeasurementProcessViewModel
    {
        public MeasurementProcessViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
            : base(eventAggregator, measurementManager)
        {
        }
        public override string Title { get; } = "Pomiar";

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