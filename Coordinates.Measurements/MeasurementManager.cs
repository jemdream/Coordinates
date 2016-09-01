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
        private GaugePositionDTO _lastRawPosition = GaugePositionDTO.Default;

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

            // Store raw format data
            measurementDataSource.DataStream
                .Subscribe(lastRaw => _lastRawPosition = lastRaw);

            // Initialize 
            ResetAllCollections();
            InstantiateMeasurements();
        }

        private GaugePositionDTO CompensatePosition(GaugePositionDTO compensation)
        {
            return new GaugePositionDTO(compensation.X - _compensationPosition.X, compensation.Y - _compensationPosition.Y, compensation.Z - _compensationPosition.Z);
        }

        // Positions (Gauge / Contact)
        private GaugePositionDTO _compensationPosition = GaugePositionDTO.Default;
        public IObservable<Position> PositionSource => _positionSource.AsObservable();
        public bool SetupMeasurementMethod(IMeasurementMethod selectedMeasurementMethod)
        {
            SelectedMeasurementMethod = selectedMeasurementMethod;
            ResetAllCollections();

            return true;
        }

        // When user backs in progress, wipe not to mess the data
        public bool ResetMeasurementData()
        {
            PushZeroPosition();
            ResetAllCollections();

            return true;
        }

        public ObservableCollectionRx<ContactPosition> SelectedPositions { get; private set; }
        public ObservableCollectionRx<GaugePosition> RawGaugePositions { get; private set; }
        public ObservableCollectionRx<ContactPosition> RawContactPositions { get; private set; }
        public bool SetupCalibration()
        {
            PushZeroPosition();
            _compensationPosition = _lastRawPosition;
            ResetAllCollections();

            return true;
        }

        private void PushZeroPosition()
        {
            _positionSource.OnNext(_compensationPosition.Contact ? (Position) ContactPosition.Default : GaugePosition.Default);
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
