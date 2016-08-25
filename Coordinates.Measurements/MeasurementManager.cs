using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Coordinates.ExternalDevices.DataSources;
using Coordinates.ExternalDevices.Models;
using Coordinates.Measurements.Helpers;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements
{
    // todo store all measurements here, public the lists, on _positionSource notifypropertychanged on ui, maybe use ObservableCollection
    // todo store all selected ms here
    public class MeasurementManager : IMeasurementManager
    {
        private readonly Subject<Position> _positionSource = new Subject<Position>();

        public MeasurementManager(IDataSource<GaugePositionDTO> measurementDataSource)
        {
            // Store from datasource
            measurementDataSource.DataStream
                .Select(CompensatePosition)
                .Subscribe(pos => RawGaugePositions.Add(new GaugePosition(pos.X, pos.Y, pos.Z)));

            measurementDataSource.DataStream
                .Where(pos => pos.Contact)
                .Select(CompensatePosition)
                .Subscribe(pos => RawContactPositions.Add(new ContactPosition(pos.X, pos.Y, pos.Z)));

            // Initialize 
            ResetAllCollections();
            InstantiateMeasurements();
        }

        private GaugePositionDTO CompensatePosition(GaugePositionDTO position)
        {
            return new GaugePositionDTO
            (
                position.X - CompensationPosition.X,
                position.Y - CompensationPosition.Y,
                position.Z - CompensationPosition.Z
            );
        }

        // Positions (Gauge / Contact)
        public Position CompensationPosition { get; private set; } = new GaugePosition();
        public IObservable<Position> PositionSource => _positionSource.AsObservable();
        public ObservableCollectionRx<ContactPosition> SelectedPositions { get; private set; }
        public ObservableCollectionRx<GaugePosition> RawGaugePositions { get; private set; }
        public ObservableCollectionRx<ContactPosition> RawContactPositions { get; private set; }

        public bool SetupCompensationPosition(IMeasurementMethod selectedMeasurementMethod, Position compensationPosition)
        {
            // TODO probably reset everything, wipe selection etc. (ask Yes/No on UI site) 
            // TODO remember that VM should be aware of PositionSource/ContactPositionsSource change

            // TODO compensation should be summed I think instead of inserting
            CompensationPosition = compensationPosition;
            SelectedMeasurementMethod = selectedMeasurementMethod;

            ResetAllCollections();
            return true;
        }

        public bool SetupNewMeasurement(IMeasurementMethod selectedMeasurementMethod, Position compensationPosition)
        {
            // TODO probably reset everything, wipe selection etc. (ask Yes/No on UI site) 
            // TODO remember that VM should be aware of PositionSource/ContactPositionsSource change
            
            // TODO compensation should be summed I think instead of inserting
            CompensationPosition = compensationPosition;
            SelectedMeasurementMethod = selectedMeasurementMethod;

            ResetAllCollections();
            return true;
        }

        private void ResetAllCollections()
        {
            RawGaugePositions = new ObservableCollectionRx<GaugePosition>();
            RawContactPositions = new ObservableCollectionRx<ContactPosition>();
            SelectedPositions = new ObservableCollectionRx<ContactPosition>();

            SelectedPositions.SelectedPositionsCollectionChanged
                .Subscribe(x => { SelectedMeasurementMethod.CanExecute(); });
            RawGaugePositions.OnAddObservable
                .Subscribe(x => _positionSource.OnNext(x));
            RawContactPositions.OnAddObservable
                .Subscribe(x => _positionSource.OnNext(x));
        }

        // Measurement Methods (flatness etc.)
        public IEnumerable<IMeasurementMethod> AvailableMeasurementMethods { get; private set; }
        public IMeasurementMethod SelectedMeasurementMethod { get; private set; }

        private void InstantiateMeasurements()
        {
            // Could have: instead use reflections and iterate by classes in namespace Coordinates.Measurements.Types
            AvailableMeasurementMethods = new List<IMeasurementMethod>
            {
                new RoundnessMeasurementMethod(),
                new FlatnessMeasurementMethod()
            };
        }
    }
}
