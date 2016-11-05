using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Coordinates.Measurements;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Helpers.Serialization;
using Coordinates.Measurements.Models;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;
using Coordinates.UI.Services;
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
        DelegateCommand ExportData { get; }

        bool CanCalculate();
        ICalculationResult Calculate { get; }
    }

    public class MeasurementMethodViewModel : ViewModelBase, IMeasurementMethodViewModel
    {
        private readonly IMeasurementManager _measurementManager;
        private readonly IDataExportService _dataExportService;
        private readonly object _lock = new object();
        
        private IMeasurementMethod _measurementMethod;
        private IEnumerable<IElementViewModel> _elementsViewModels;

        private DelegateCommand<PlaneEnum> _setMeasurementPlane;
        private DelegateCommand<Position> _setInitialPosition;
        private DelegateCommand _releaseDialog;
        private DelegateCommand _exportData;

        private IElementViewModel _activeElementViewModel;
        private Position _presentPosition;
        private AxisMovementDialog _axisMovementDialog;

        public MeasurementMethodViewModel(IMeasurementManager measurementManager, IDataExportService dataExportService)
        {
            _measurementManager = measurementManager;
            _dataExportService = dataExportService;

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
        }, () => ActiveElementViewModel != null && ActiveElementViewModel.Element.AxisMovementValidation(PresentPosition)));

        public DelegateCommand ExportData => _exportData ?? (_exportData = new DelegateCommand(async () =>
        {
            await _dataExportService.SaveToFile(_measurementMethod as BaseMeasurementMethod);
        }));

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
            var element = ActivateNextElement(measurementMethod);

            ElementsViewModels.ForEach(evm => evm.Update());

            ActiveElementViewModel = ElementsViewModels.FirstOrDefault(x => x.Element == element);

            if (element == null) return;

            if (element.Plane == null)
                await ShowPlaneSelectionDialog();

            // Check if should show blocking dialog
            if (MeasurementMethod.SetupInitialPosition(null))
                await ShowAxisBlockDialog();

            Subscribe(measurementMethod, element);

            _measurementManager.GatherData = true;
        }

        private void Subscribe(IMeasurementMethod measurementMethod, IElement element)
        {
            // Positions to register
            measurementMethod.Subscriptions.Add(
                _measurementManager.PositionSource
                    .Where(_ => _measurementManager.GatherData)
                    .Where(element.AxisMovementValidation)
                    // TODO [bug-contact] here to validate if initial contact made
                    .Where(position => position.Contact)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(position => element.Positions.Add(position))
                );

            // Axis moved prompt
            measurementMethod.Subscriptions.Add(
                _measurementManager.PositionSource
                    .Where(_ => _measurementManager.GatherData)
                    .Where(pos => !element.AxisMovementValidation(pos))
                    .ObserveOn(SynchronizationContext.Current)
                    .Where(_ => !_isAxisLocked)
                    .SelectMany(async _ => await ShowAxisMovementDialog())
                    .Subscribe()
                );
        }

        private IElement ActivateNextElement(IMeasurementMethod measurementMethod)
        {
            _measurementManager.GatherData = false;
            measurementMethod.Subscriptions.Clear();

            return measurementMethod.ActivateNextElement();
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