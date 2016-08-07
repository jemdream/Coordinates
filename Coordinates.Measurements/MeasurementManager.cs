using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Coordinates.ExternalDevices.DataSources;
using Coordinates.ExternalDevices.Models;
using Coordinates.Measurements.Types;
using Coordinates.Models;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements
{
    public class MeasurementManager : IMeasurementManager
    {
        private List<GaugePosition> _rawGaugePositions = new List<GaugePosition>();
        private List<ContactPosition> _rawContactPositions = new List<ContactPosition>();

        private IMeasurementMethod _selectedMeasurementMethod;

        public MeasurementManager(IDataSource<GaugePositionDTO> measurementDataSource)
        {
            measurementDataSource.DataStream
                .Where(_ => SelectedMeasurementMethod != null)
                .Subscribe(pos => _rawGaugePositions.Add(new GaugePosition { X = pos.X, Y = pos.Y, Z = pos.Z }));

            measurementDataSource.DataStream
                .Where(pos => pos.Contact)
                .Where(_ => SelectedMeasurementMethod != null)
                .Subscribe(pos => _rawContactPositions.Add(new ContactPosition { X = pos.X, Y = pos.Y, Z = pos.Z }));

            WatchSelectedPositionsChange();

            InstantiateMeasurements();
        }

        private void WatchSelectedPositionsChange()
        {
            var test = Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                h => SelectedPositions.CollectionChanged += h,
                h => SelectedPositions.CollectionChanged -= h);

            test.Where(ev => ev.EventArgs.Action != NotifyCollectionChangedAction.Replace)
                .Subscribe(_ =>
                {
                    // TODO 01-08-2016 maybe execute and return result
                    SelectedMeasurementMethod.CanExecute();
                });
        }

        // TODO 01-08-2016 instead of exposing IEnumerable, provide with DataStreams;
        public IObservable<GaugePosition> GaugePositionSource => _rawGaugePositions.ToObservable();
        public IObservable<ContactPosition> ContactPositionsSource => _rawContactPositions.ToObservable();

        public ObservableCollection<ContactPosition> SelectedPositions { get; set; } = new ObservableCollection<ContactPosition>();
        public IEnumerable<IMeasurementMethod> AvailableMeasurements { get; private set; }

        public IMeasurementMethod SelectedMeasurementMethod
        {
            get { return _selectedMeasurementMethod; }
            set { SetupMeasurement(value); }
        }

        private void SetupMeasurement(IMeasurementMethod value)
        {
            // TODO probably reset everything, wipe selection etc. (ask Yes/No on UI site) 
            _selectedMeasurementMethod = value;
            SelectedPositions.Clear();

            // TODO remember that VM should be aware of GaugePositionSource/ContactPositionsSource change
            _rawGaugePositions = new List<GaugePosition>();
            _rawContactPositions = new List<ContactPosition>();
        }

        private void InstantiateMeasurements()
        {
            // TODO COULD HAVE: can use reflections and iterate by classes in namespace Coordinates.Measurements.Types
            AvailableMeasurements = new List<IMeasurementMethod>
            {
                new RoundnessMeasurementMethod(),
                new FlatnessMeasurementMethod()
            };
        }
    }
}
