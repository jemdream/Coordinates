using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.UI.Messages;
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
        bool ViewHack { get; }
    }

    public class MeasurementProcessViewModel : MeasurementViewModelBase, IMeasurementProcessViewModel
    {
        private readonly IMeasurementManager _measurementManager;
        private AwaitableDelegateCommand _nextElementCommand;

        private bool _viewHack;

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
        public bool ViewHack { get { return _viewHack; } set { Set(ref _viewHack, value); } }

        public IMeasurementMethodViewModel MeasurementMethodViewModel { get; }

        public ICommand NextElementCommand => _nextElementCommand ?? (_nextElementCommand = new AwaitableDelegateCommand(async x =>
        {
            await MeasurementMethodViewModel.SetNextElement();
            (NextElementCommand as AwaitableDelegateCommand)?.RaiseCanExecuteChanged();
            ViewHack = !ViewHack;
        }, x =>
        {
            var mm = MeasurementMethodViewModel.MeasurementMethod;
            if (mm.ActiveElement == null) return false;
            return (mm.ActiveElement.Positions.Count >= mm.ActiveElement.RequiredMeasurementCount) && mm.IsNextElementAvailable;
        }));

        protected override bool CanOnNext()
        {
            return MeasurementMethodViewModel.ElementsViewModels
                .All(evm => evm.Positions.Count >= evm.RequiredMeasurementCount);
        }

        protected override async Task<bool> OnNext()
        {
            await MeasurementMethodViewModel.SetNextElement();
            return await base.OnNext();
        }

        protected override async Task<bool> OnPrevious()
        {
            if (_measurementManager.SelectedMeasurementMethod == null) return true;

            _measurementManager.GatherData = false;

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
            else
            {
                _measurementManager.GatherData = true;
            }

            return false;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (_measurementManager.SelectedMeasurementMethod == null)
                EventAggregator
                        .GetEvent<ResetMeasurement>()
                        .Publish(null);

            UpdateNavigationCommands();

            if (parameter is MeasurementCalibrationViewModel)
                await MeasurementMethodViewModel.SetNextElement();
        }
    }
}