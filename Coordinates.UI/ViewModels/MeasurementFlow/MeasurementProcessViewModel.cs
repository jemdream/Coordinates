using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Coordinates.UI.Views.Dialogs;
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
        private bool _showModal;

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
        public bool ShowModal { get { return _showModal; } set { Set(ref _showModal, value); } }

        public IMeasurementMethodViewModel MeasurementMethodViewModel { get; }

        public ICommand NextElementCommand => _nextElementCommand ?? (_nextElementCommand = new AwaitableDelegateCommand(async x =>
        {
            await Task.CompletedTask;
            MeasurementMethodViewModel.SetNextElement();
            (NextElementCommand as AwaitableDelegateCommand)?.RaiseCanExecuteChanged();
            ViewHack = !ViewHack;
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

        protected override Task<bool> OnNext()
        {
            // TODO HERE TO UNHIGHLIGHT THE DATAGRID
            _measurementManager.GatherData = false;
            return base.OnNext();
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

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            UpdateNavigationCommands();

            ShowModal = !ShowModal;

            if (parameter is MeasurementCalibrationViewModel)
            {
                // TODO condition - if measurement requires surface selection
                var contentDialog = new PlaneSelectionDialog(this);
                await contentDialog.ShowAsync();
                contentDialog.Hide();

                _measurementManager.GatherData = true;
            }

            ViewHack = !ViewHack;
        }
    }
}