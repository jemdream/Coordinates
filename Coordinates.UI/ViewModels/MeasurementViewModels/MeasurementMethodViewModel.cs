using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coordinates.Measurements;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Models;
using Coordinates.Measurements.Types;
using Coordinates.UI.Views.Dialogs;
using Microsoft.Practices.ObjectBuilder2;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public interface IMeasurementMethodViewModel
    {
        IMeasurementMethod MeasurementMethod { get; }
        IEnumerable<IElementViewModel> ElementsViewModels { get; }
        Task SetNextElement();
        IEnumerable<PlaneEnum> SurfaceEnums { get; }
        DelegateCommand<PlaneEnum> SetMeasurementPlane { get; }
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

        public MeasurementMethodViewModel(IMeasurementManager measurementManager)
        {
            _measurementManager = measurementManager;
            _measurementManager.MeasurementSource
                .Subscribe(InitializeMeasurement);
        }

        public IEnumerable<PlaneEnum> SurfaceEnums => Enum.GetValues(typeof(PlaneEnum)).Cast<PlaneEnum>();

        public DelegateCommand<PlaneEnum> SetMeasurementPlane => _setMeasurementPlane ?? (_setMeasurementPlane = new DelegateCommand<PlaneEnum>(async x =>
        {
            await Task.CompletedTask;
            MeasurementMethod.SetupPlane(x);
            ElementsViewModels.ForEach(evm => evm.RefreshUi());
        }, x => true));

        public bool CanCalculate() => MeasurementMethod != null && MeasurementMethod.CanCalculate();

        public ICalculationResult Calculate => MeasurementMethod?.Calculate();

        public IMeasurementMethod MeasurementMethod
        {
            get { return _measurementMethod; }
            private set { Set(ref _measurementMethod, value); }
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

            if (element == null) return;

            if (element.Plane == null) await ShowPlaneSelectionDialog();
            await ShowAxisBlockDialog();

            measurementMethod.Subscriptions.Add(
                _measurementManager.PositionSource
                    .Where(_ => _measurementManager.GatherData)
                    // TODO Here to add validation (that blocks wrong parameters) 
                    .Where(position => position.Contact)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(position => element.Positions.Add(position))
            );

            measurementMethod.Subscriptions.Add(
                _measurementManager.PositionSource
                    .Where(_ => _measurementManager.GatherData)
                    // TODO Here to add validation (that pops up validation dialog) 
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(position => { })
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