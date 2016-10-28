using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.UI.Messages;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementSelectionCalculationViewModel : IMeasurementViewModelBase
    {
        IMeasurementMethodViewModel MeasurementMethodViewModel { get; }
    }

    public class MeasurementSelectionCalculationViewModel : MeasurementViewModelBase, IMeasurementSelectionCalculationViewModel
    {
        private readonly IMeasurementManager _measurementManager;

        public MeasurementSelectionCalculationViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager,
            IMeasurementMethodViewModel measurementMethodViewModel) : base(eventAggregator)
        {
            _measurementManager = measurementManager;
            MeasurementMethodViewModel = measurementMethodViewModel;
        }

        public override string Title { get; } = "Wybór pomiarów i obliczenia";

        public IMeasurementMethodViewModel MeasurementMethodViewModel { get; }

        protected override bool CanOnNext() => false;

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
                EventAggregator
                        .GetEvent<ResetMeasurement>()
                        .Publish(null);
            }

            return false;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            RaisePropertyChanged(() => MeasurementMethodViewModel);

            UpdateNavigationCommands();

            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
