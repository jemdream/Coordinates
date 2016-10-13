using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Coordinates.Measurements;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Helpers;
using Coordinates.Measurements.Models;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;
using Coordinates.UI.Views.Dialogs;
using Microsoft.Practices.ObjectBuilder2;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public interface IMeasurementMethodViewModel
    {
        IMeasurementMethod MeasurementMethod { get; }
        IEnumerable<IElementViewModel> ElementsViewModels { get; }
        Position PresentPosition { get; }
        IElementViewModel ActiveElementViewModel { get; }
        Task SetNextElement();
        IEnumerable<PlaneEnum> SurfaceEnums { get; }

        DelegateCommand<PlaneEnum> SetMeasurementPlane { get; }
        DelegateCommand<Position> SetInitialPosition { get; }
        DelegateCommand ReleaseDialog { get; }

        bool CanCalculate();
        ICalculationResult Calculate { get; }
    }

    public class MeasurementMethodViewModel : ViewModelBase, IMeasurementMethodViewModel
    {
        private readonly IMeasurementManager _measurementManager;
        private readonly object _lock = new object();
        AxisMovementDialog _axisMovementDialog;

        private IMeasurementMethod _measurementMethod;
        private IEnumerable<IElementViewModel> _elementsViewModels;
        private DelegateCommand<PlaneEnum> _setMeasurementPlane;
        private DelegateCommand<Position> _setInitialPosition;
        private DelegateCommand _releaseDialog;
        private IElementViewModel _activeElementViewModel;
        private Position _presentPosition;

        public MeasurementMethodViewModel(IMeasurementManager measurementManager)
        {
            _measurementManager = measurementManager;

            _measurementManager.MeasurementSource
                .Subscribe(InitializeMeasurement);

            _measurementManager.PositionSource
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(pos =>
                {
                    PresentPosition = pos;
                    ReleaseDialog.RaiseCanExecuteChanged();
                });
        }

        public IEnumerable<PlaneEnum> SurfaceEnums => Enum.GetValues(typeof(PlaneEnum)).Cast<PlaneEnum>();

        public DelegateCommand<PlaneEnum> SetMeasurementPlane => _setMeasurementPlane ?? (_setMeasurementPlane = new DelegateCommand<PlaneEnum>(async x =>
        {
            await Task.CompletedTask;
            MeasurementMethod.SetupPlane(x);
            ElementsViewModels.ForEach(evm => evm.Update());
        }, x => true));

        public DelegateCommand<Position> SetInitialPosition => _setInitialPosition ?? (_setInitialPosition = new DelegateCommand<Position>(async x =>
        {
            await Task.CompletedTask;
            MeasurementMethod.SetupInitialPosition(x);
        }, x => true));

        public DelegateCommand ReleaseDialog => _releaseDialog ?? (_releaseDialog = new DelegateCommand(() =>
        {
            _axisMovementDialog?.Hide();
        }, () => ActiveElementViewModel.Element.AxisMovementValidation(PresentPosition)));

        public bool CanCalculate() => MeasurementMethod != null && MeasurementMethod.CanCalculate();

        public ICalculationResult Calculate => MeasurementMethod?.Calculate();

        public Position PresentPosition
        {
            get { return _presentPosition; }
            set { Set(ref _presentPosition, value); }
        }

        public IMeasurementMethod MeasurementMethod
        {
            get { return _measurementMethod; }
            private set { Set(ref _measurementMethod, value); }
        }

        public IElementViewModel ActiveElementViewModel
        {
            get { return _activeElementViewModel; }
            set { Set(ref _activeElementViewModel, value); }
        }

        public IEnumerable<IElementViewModel> ElementsViewModels
        {
            get { return _elementsViewModels; }
            set { Set(ref _elementsViewModels, value); }
        }

        public async Task SetNextElement()
        {
            if (_measurementMethod != null)
                await SetNextElement(_measurementMethod);
        }

        // invoked every time even initially
        private async Task SetNextElement(IMeasurementMethod measurementMethod)
        {
            // TODO move this out
            _measurementManager.GatherData = false;

            measurementMethod.Subscriptions.Clear();

            var element = measurementMethod.ActivateNextElement();

            ElementsViewModels.ForEach(evm => evm.Update());

            ActiveElementViewModel = ElementsViewModels.FirstOrDefault(x => x.Element == element);

            if (element == null) return;

            if (element.Plane == null)
                await ShowPlaneSelectionDialog();

            await ShowAxisBlockDialog();

            measurementMethod.Subscriptions.Add(
                _measurementManager.PositionSource
                    .Where(_ => _measurementManager.GatherData)
                    .Where(element.AxisMovementValidation)
                    .Where(position => position.Contact)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(position => element.Positions.Add(position))
            );

            measurementMethod.Subscriptions.Add(
                _measurementManager.PositionSource
                    .Where(_ => _measurementManager.GatherData)
                    .Where(pos => !element.AxisMovementValidation(pos))
                    .ObserveOn(SynchronizationContext.Current)
                    .Where(_ => !_isAxisLocked)
                    .SelectMany(async _ => await ShowAxisMovementDialog())
                    .Subscribe()
            );

            _measurementManager.GatherData = true;
        }

        private void InitializeMeasurement(IMeasurementMethod measurementMethod)
        {
            lock (_lock)
            {
                // Wrapping in VM
                ElementsViewModels = measurementMethod.Elements
                    .Select((el, i) => new ElementViewModel(el))
                    .ToArray();

                ElementsViewModels
                    .Select(evm => evm.Element)
                    .ForEach(e =>
                    {
                        e.SelectedPositions.OnAdd.ObserveOn(SynchronizationContext.Current).Subscribe(_ => RaisePropertyChanged(() => Calculate));
                        e.SelectedPositions.OnRemove.ObserveOn(SynchronizationContext.Current).Subscribe(_ => RaisePropertyChanged(() => Calculate));
                    });

                // Expose measurement method
                MeasurementMethod = measurementMethod;
            }
        }

        private async Task ShowAxisBlockDialog()
        {
            var axisBlockDialog = new AxisBlockDialog(this);
            await axisBlockDialog.ShowAsync();
            axisBlockDialog.Hide();
        }

        private bool _isAxisLocked;
        private async Task<ContentDialogResult> ShowAxisMovementDialog()
        {
            _isAxisLocked = true;

            if (_axisMovementDialog == null)
                _axisMovementDialog = new AxisMovementDialog(this);

            var dialogResult = await _axisMovementDialog.ShowAsync();

            _isAxisLocked = false;

            return dialogResult;
        }

        private async Task ShowPlaneSelectionDialog()
        {
            var planeSelectionDialog = new PlaneSelectionDialog(this);
            await planeSelectionDialog.ShowAsync();
            planeSelectionDialog.Hide();
        }
    }
}