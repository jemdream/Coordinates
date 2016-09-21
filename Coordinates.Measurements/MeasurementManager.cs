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
            // Storing data from DataSource
            // TODO [MultiMeasure]
            // TODO pass (pos) to SelectedMeasurement
            measurementDataSource.DataStream
                .Select(CompensatePosition)
                .Subscribe(pos => RawGaugePositions.Add(new GaugePosition(pos.X, pos.Y, pos.Z)));

            // TODO [MultiMeasure]
            measurementDataSource.DataStream
                .Where(pos => pos.Contact)
                .Select(CompensatePosition)
                .Subscribe(pos => RawContactPositions.Add(new ContactPosition(pos.X, pos.Y, pos.Z)));

            measurementDataSource.DataStream // Raw last position
                .Subscribe(lastRaw => _lastRawPosition = lastRaw);

            // After position is stored, bubble it
            // TODO OVER-ENGINEERING, add simple pushing just like upper
            RawGaugePositions.OnAdd
                .Subscribe(x => _positionSource.OnNext(x));

            RawContactPositions.OnAdd
                .Subscribe(x => _positionSource.OnNext(x));

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
            new GaugePositionDTO(position.X - CompensationPosition.X, position.Y - CompensationPosition.Y, position.Z - CompensationPosition.Z);

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

        public ObservableList<ContactPosition> SelectedPositions { get; } = new ObservableList<ContactPosition>();
        public ObservableList<GaugePosition> RawGaugePositions { get; } = new ObservableList<GaugePosition>();
        public ObservableList<ContactPosition> RawContactPositions { get; } = new ObservableList<ContactPosition>();

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

        private void PushZeroPosition()
        {
            _positionSource.OnNext(CompensationPosition.Contact ? (Position)ContactPosition.Default : GaugePosition.Default);
        }

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
