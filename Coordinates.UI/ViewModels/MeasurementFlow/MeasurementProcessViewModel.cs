using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementProcessViewModel : IMeasurementViewModelBase
    {
        IMeasurementMethodViewModel MeasurementMethodViewModel { get; }
        ICommand NextElementCommand { get; }
    }

    public class MeasurementProcessViewModel : MeasurementViewModelBase, IMeasurementProcessViewModel
    {
        private readonly IMeasurementManager _measurementManager;
        private AwaitableDelegateCommand _nextElementCommand;

        public MeasurementProcessViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager,
            IMeasurementMethodViewModel measurementMethodViewModel)
            : base(eventAggregator)
        {
            MeasurementMethodViewModel = measurementMethodViewModel;
            _measurementManager = measurementManager;

            // Update all UI elements
            _measurementManager.PositionSource
                .Subscribe(_ =>
                {
                    (NextElementCommand as AwaitableDelegateCommand)?.RaiseCanExecuteChanged();
                    UpdateNavigationCommands();
                });
        }

        public override string Title { get; } = "Pomiar";

        public IMeasurementMethodViewModel MeasurementMethodViewModel { get; }

        public ICommand NextElementCommand => _nextElementCommand ?? (_nextElementCommand = new AwaitableDelegateCommand(async x =>
        {

            MeasurementMethodViewModel.SetNextElement();
            await Task.CompletedTask;
            (NextElementCommand as AwaitableDelegateCommand)?.RaiseCanExecuteChanged();
        }, x =>
        {
            var mm = MeasurementMethodViewModel.MeasurementMethod;
            return (mm.ActiveElement.Positions.Count >= mm.ActiveElement.RequiredMeasurementCount) && mm.IsNextElementAvailable;
        }));

        protected override bool CanOnNext()
        {
            /* TODO M place validation here basing on MeasurementMethodViewModel */
            return true;
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

            if (returnConfirmed) _measurementManager.ResetMeasurementData();

            return returnConfirmed;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            UpdateNavigationCommands();

            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}