using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        bool CanCalculate();
        ICalculationResult Calculate { get; }
    }

    public class MeasurementMethodViewModel : ViewModelBase, IMeasurementMethodViewModel
    {
        private readonly IMeasurementManager _measurementManager;
        private readonly object _lock = new object();

        private IMeasurementMethod _measurementMethod;
        private IEnumerable<IElementViewModel> _elementsViewModels;
        private DelegateCommand<PlaneEnum> _setMeasurementPlane;
        private DelegateCommand<Position> _setInitialPosition;
        private IElementViewModel _activeElementViewModel;
        private Position _presentPosition;

        public MeasurementMethodViewModel(IMeasurementManager measurementManager)
        {
            _measurementManager = measurementManager;

            _measurementManager.MeasurementSource
                .Subscribe(InitializeMeasurement);

            _measurementManager.PositionSource
                .Subscribe(pos => PresentPosition = pos);
        }

        public IEnumerable<PlaneEnum> SurfaceEnums => Enum.GetValues(typeof(PlaneEnum)).Cast<PlaneEnum>();

        public DelegateCommand<PlaneEnum> SetMeasurementPlane => _setMeasurementPlane ?? (_setMeasurementPlane = new DelegateCommand<PlaneEnum>(async x =>
        {
            await Task.CompletedTask;
            MeasurementMethod.SetupPlane(x);
            ElementsViewModels.ForEach(evm => evm.RefreshUi());
        }, x => true));

        public DelegateCommand<Position> SetInitialPosition => _setInitialPosition ?? (_setInitialPosition = new DelegateCommand<Position>(async x =>
        {
            await Task.CompletedTask;
            MeasurementMethod.SetupInitialPosition(x);
        }, x => true));

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
            _measurementManager.GatherData = false;

            measurementMethod.Subscriptions.Clear();

            var element = measurementMethod.ActivateNextElement();

            ElementsViewModels.ForEach(evm => evm.RefreshUi());

            ActiveElementViewModel = ElementsViewModels.FirstOrDefault(x => x.Element == element);

            if (element == null) return;

            if (element.Plane == null)
                await ShowPlaneSelectionDialog();

            await ShowAxisBlockDialog();

            measurementMethod.Subscriptions.Add(
                _measurementManager.PositionSource
                    .Where(_ => _measurementManager.GatherData)
                    .Where(pos => (element.InitialPosition == null) || Math.Abs(pos.GetBlockedAxisValue(element.Plane).Value - element.InitialPosition.GetBlockedAxisValue(element.Plane).Value) < 0.00001)
                    .Where(position => position.Contact)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(position => element.Positions.Add(position))
            );

            measurementMethod.Subscriptions.Add(
                _measurementManager.PositionSource
                    .Where(_ => _measurementManager.GatherData)
                    .Where(pos => !((element.InitialPosition == null) || Math.Abs(pos.GetBlockedAxisValue(element.Plane).Value - element.InitialPosition.GetBlockedAxisValue(element.Plane).Value) < 0.00001))
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(position => { Debugger.Break(); })
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

        private async Task ShowPlaneSelectionDialog()
        {
            var planeSelectionDialog = new PlaneSelectionDialog(this);
            await planeSelectionDialog.ShowAsync();
            planeSelectionDialog.Hide();
        }
    }
}