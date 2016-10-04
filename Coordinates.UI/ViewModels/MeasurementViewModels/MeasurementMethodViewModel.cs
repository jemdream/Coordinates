﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using Coordinates.Measurements;
using Coordinates.Measurements.Types;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public interface IMeasurementMethodViewModel
    {
        IMeasurementMethod MeasurementMethod { get; }
        IEnumerable<IElementViewModel> ElementsViewModels { get; }
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
            
            #region trash
            // todo this is from selectionmv \/
            //measurementManager.RawContactPositions
            //    .OnAdd
            //    .ObserveOn(SynchronizationContext.Current)
            //    .Subscribe(position => ContactPositions.Insert(0, position));

            //measurementManager.RawContactPositions
            //    .OnRemove
            //    .ObserveOn(SynchronizationContext.Current)
            //    .Subscribe(position => ContactPositions.Remove(position));

            //SelectedPositions = measurementManager.SelectedPositions;

            // On add and on remove, update controls
            //SelectedPositions
            //    .OnAdd
            //    .Concat(SelectedPositions.OnRemove)
            //    .ObserveOn(SynchronizationContext.Current)
            //    .Subscribe(_ => ModelsUpdated());

            //ContactPositions = new ObservableCollection<Position>(MeasurementManager.RawContactPositions);

            // todo this is from manager \/
            // Storing contact points
            //compensatedPositions
            //    .Where(pos => pos.Contact)
            //    .Subscribe(pos => RawContactPositions.Add(pos));

            // Selected positions change
            //SelectedPositions.OnAdd
            //    .Where(_ => SelectedMeasurementMethod != null)
            //    .Subscribe(_ => { var test = SelectedMeasurementMethod.CanCalculate(); });
            //SelectedPositions.OnRemove
            //    .Where(_ => SelectedMeasurementMethod != null)
            //    .Subscribe(_ => { var test = SelectedMeasurementMethod.CanCalculate(); });
            #endregion
        }

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
        private CompositeDisposable _actualElementDisposables;
        
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

        private void SetNextElement(IMeasurementMethod measurementMethod)
        {
            if (_actualElementDisposables != null && !_actualElementDisposables.IsDisposed)
                _actualElementDisposables.Dispose();

            _actualElementDisposables = new CompositeDisposable();

            var element = measurementMethod.ActivateNextElement();

            var subscriptionToData = _measurementManager.PositionSource
                .Where(position => position.Contact)
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(position => element.Positions.Add(position));

            _actualElementDisposables.Add(subscriptionToData);
        }
    }
}