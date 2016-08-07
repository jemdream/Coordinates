using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Coordinates.ExternalDevices.DataSources;
using Coordinates.ExternalDevices.Models;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements
{
    public class ObservableList<T> : List<T>
    {
        private readonly ReplaySubject<T> _onAddSubject = new ReplaySubject<T>();
        public IObservable<T> OnAddObservable => _onAddSubject.AsObservable();

        public new void Add(T obj)
        {
            _onAddSubject.OnNext(obj);
            base.Add(obj);
        }
    }

    public class MeasurementManager : IMeasurementManager
    {
        private ObservableList<GaugePosition> _rawGaugePositions = new ObservableList<GaugePosition>();
        private ObservableList<ContactPosition> _rawContactPositions = new ObservableList<ContactPosition>();

        private IMeasurementMethod _selectedMeasurementMethod;
        private readonly Subject<Position> _positionSource = new Subject<Position>();

        public MeasurementManager(IDataSource<GaugePositionDTO> measurementDataSource)
        {
            _rawGaugePositions.OnAddObservable
                .Subscribe(_positionSource);

            _rawContactPositions.OnAddObservable
                .Subscribe(_positionSource);

            measurementDataSource.DataStream
                .Subscribe(pos => _rawGaugePositions.Add(new GaugePosition { X = pos.X, Y = pos.Y, Z = pos.Z }));

            measurementDataSource.DataStream
                .Where(pos => pos.Contact)
                .Subscribe(pos => _rawContactPositions.Add(new ContactPosition { X = pos.X, Y = pos.Y, Z = pos.Z }));

            WatchSelectedPositionsChanged();
            InstantiateMeasurements();
        }

        private void WatchSelectedPositionsChanged()
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

        public IObservable<Position> PositionSource => _positionSource.AsObservable();

        public ObservableCollection<ContactPosition> SelectedPositions { get; set; } = new ObservableCollection<ContactPosition>();
        public IEnumerable<IMeasurementMethod> AvailableMeasurementMethods { get; private set; }
        public IMeasurementMethod SelectedMeasurementMethod
        {
            get { return _selectedMeasurementMethod; }
            set { SetupSelectedMeasurementMethod(value); }
        }

        private void SetupSelectedMeasurementMethod(IMeasurementMethod value)
        {
            // TODO probably reset everything, wipe selection etc. (ask Yes/No on UI site) 
            _selectedMeasurementMethod = value;
            SelectedPositions.Clear();

            // TODO remember that VM should be aware of PositionSource/ContactPositionsSource change
            _rawGaugePositions = new ObservableList<GaugePosition>();

            _rawContactPositions = new ObservableList<ContactPosition>();
            
            //TODO code duplication
            _rawGaugePositions.OnAddObservable
               .Subscribe(_positionSource);

            _rawContactPositions.OnAddObservable
                .Subscribe(_positionSource);
        }

        private void InstantiateMeasurements()
        {
            // TODO COULD HAVE: can use reflections and iterate by classes in namespace Coordinates.Measurements.Types
            AvailableMeasurementMethods = new List<IMeasurementMethod>
            {
                new RoundnessMeasurementMethod(),
                new FlatnessMeasurementMethod()
            };
        }
    }
}
