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
    // TODO [MultiMeasure]: - List<IMeasurementMethod> with measurements-each having own RawGaugePositions/RawContactPositions/SelectedPositions

    public class MeasurementManager : IMeasurementManager
    {
        private readonly Subject<Position> _positionSource = new Subject<Position>();

        private GaugePositionDTO _lastRawPosition = GaugePositionDTO.Default;
        public GaugePositionDTO CompensationPosition { get; private set; } = GaugePositionDTO.Default;

        public MeasurementManager(IDataSource<GaugePositionDTO> measurementDataSource)
        {
            // Store raw position
            measurementDataSource.DataStream
                .Subscribe(lastRaw => _lastRawPosition = lastRaw);

            // Compensating and mapping
            var compensatedPositions = measurementDataSource.DataStream
                .Select(CompensatePosition)
                .Select(pos => new Position(pos.X, pos.Y, pos.Z, pos.Contact));

            // Storing all points
            compensatedPositions
                .Subscribe(pos => RawGaugePositions.Add(pos));

            // Storing contact points
            // TODO [MultiMeasure] move out
            compensatedPositions
                .Where(pos => pos.Contact)
                .Subscribe(pos => RawContactPositions.Add(pos));
            
            // After position is stored, bubble it
            compensatedPositions
                .Subscribe(x => _positionSource.OnNext(x));

            // TODO [MultiMeasure] move out
            // Selected positions change
            SelectedPositions.OnAdd
                .Where(_ => SelectedMeasurementMethod != null)
                .Subscribe(_ => { var test = SelectedMeasurementMethod.CanExecute(); });
            SelectedPositions.OnRemove
                .Where(_ => SelectedMeasurementMethod != null)
                .Subscribe(_ => { var test = SelectedMeasurementMethod.CanExecute(); });

            // Initialize 
            ResetAllCollections();
            InstantiateMeasurements();
        }

        /// <summary>
        /// Compensates position value with CompensationPosition
        /// </summary>
        private GaugePositionDTO CompensatePosition(GaugePositionDTO position) =>
            new GaugePositionDTO(position.X - CompensationPosition.X, position.Y - CompensationPosition.Y, position.Z - CompensationPosition.Z, position.Contact);

        // Positions (Gauge / Contact)

        public IObservable<Position> PositionSource => _positionSource.AsObservable();
        public bool SetupMeasurementMethod(IMeasurementMethod selectedMeasurementMethod)
        {
            SelectedMeasurementMethod = selectedMeasurementMethod;
            ResetAllCollections();
            return true;
        }

        /// <summary>
        /// Wipe all measurement data. 
        /// </summary>
        public bool ResetMeasurementData()
        {
            ResetAllCollections();
            return true;
        }

        public ObservableList<Position> SelectedPositions { get; } = new ObservableList<Position>();// TODO [MultiMeasure] move out
        public ObservableList<Position> RawGaugePositions { get; } = new ObservableList<Position>();
        public ObservableList<Position> RawContactPositions { get; } = new ObservableList<Position>();// TODO [MultiMeasure] move out

        /// <summary>
        /// Saves latest position as CompensationPosition, that will affect next measurements. Wipes the data afterwards.
        /// </summary>
        /// <returns></returns>
        public bool SetupCalibration()
        {
            PushZeroPosition();
            CompensationPosition = _lastRawPosition;
            ResetAllCollections();

            return true;
        }

        private void PushZeroPosition() => _positionSource.OnNext(Position.Default);

        // TODO [MultiMeasure] Foreach on all or rather delete elements from list
        private void ResetAllCollections()
        {
            RawGaugePositions.Clear();
            RawContactPositions.Clear();
            SelectedPositions.Clear();
        }

        // Measurement Methods (flatness etc.)
        public IEnumerable<IMeasurementMethod> AvailableMeasurementMethods { get; private set; }
        public IMeasurementMethod SelectedMeasurementMethod { get; private set; }

        private void InstantiateMeasurements()
        {
            // Could have: instead use reflections and iterate by classes in namespace Coordinates.Measurements.Types
            AvailableMeasurementMethods = new List<IMeasurementMethod>
            {
                new OneHoleMeasurementMethod(),
                new TwoHolesMeasurementMethod(),
                new SurfacePerpendicularityMeasurementMethod(),
                new SurfaceParalellismMeasurementMethod()
            };
        }
    }
}
