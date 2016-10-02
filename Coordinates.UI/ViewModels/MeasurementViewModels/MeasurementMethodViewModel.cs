﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        private void InitializeMeasurement(IMeasurementMethod measurementMethod)
        {
            lock (_lock)
            {
                // TODO i think this should be deleted
                //ElementsViewModels = MeasurementMethod.Elements
                //    .Select(el => new ElementViewModel(el));

                // Select element and subscribe it to datasource
                var element = measurementMethod.ActivateNext();
                _measurementManager.SubscribeToDataSource(element);

                // Expose measurement method
                MeasurementMethod = measurementMethod;
            }
        }
    }
}