using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Coordinates.Measurements;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Types;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public interface IMeasurementMethodViewModel
    {
        IMeasurementMethod MeasurementMethod { get; }
        IEnumerable<IElementViewModel> ElementsViewModels { get; }
        void SetNextElement();
        IEnumerable<SurfaceEnum> SurfaceEnums { get; }
    }

    public class MeasurementMethodViewModel : ViewModelBase, IMeasurementMethodViewModel
    {
        private readonly IMeasurementManager _measurementManager;
        private IMeasurementMethod _measurementMethod;
        private IEnumerable<IElementViewModel> _elementsViewModels;

        public MeasurementMethodViewModel(IMeasurementManager measurementManager)
        {
            _measurementManager = measurementManager;
            _measurementManager.MeasurementSource
                .Subscribe(InitializeMeasurement);
        }

        public IEnumerable<SurfaceEnum> SurfaceEnums => Enum.GetValues(typeof(SurfaceEnum)).Cast<SurfaceEnum>();

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

        private readonly object _lock = new object();

        public void SetNextElement()
        {
            if (_measurementMethod != null)
                SetNextElement(_measurementMethod);
        }

        private void SetNextElement(IMeasurementMethod measurementMethod)
        {
            measurementMethod.Subscriptions.Clear();

            var element = measurementMethod.ActivateNextElement();

            var subscriptionToData = _measurementManager.PositionSource
                .Where(_ => _measurementManager.GatherData)
                .Where(position => position.Contact)
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(position => element.Positions.Add(position));

            measurementMethod.Subscriptions.Add(subscriptionToData);
        }

        private void InitializeMeasurement(IMeasurementMethod measurementMethod)
        {
            lock (_lock)
            {
                // Wrapping in VM
                ElementsViewModels = measurementMethod.Elements
                    .Select((el, i) => new ElementViewModel(el))
                    .ToArray();

                // Select next element and manage subscriptions 
                SetNextElement(measurementMethod);

                // Expose measurement method
                MeasurementMethod = measurementMethod;
            }
        }
    }
}