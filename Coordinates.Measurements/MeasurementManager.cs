using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Coordinates.ExternalDevices.DataSources;
using Coordinates.ExternalDevices.Models;
using Coordinates.Measurements.Helpers;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements
{
    // TODO [MultiMeasure]: - List<IMeasurementMethod> with measurements-each having own PositionBuffer/RawContactPositions/SelectedPositions

    public class MeasurementManager : IMeasurementManager
    {
        private readonly ReplaySubject<Position> _positionSource = new ReplaySubject<Position>(1);
        private readonly ReplaySubject<IMeasurementMethod> _measurementSource = new ReplaySubject<IMeasurementMethod>(1);

        private GaugePositionDTO _lastRawPosition = GaugePositionDTO.Default;
        private GaugePositionDTO _compensationPosition = GaugePositionDTO.Default;

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
                .Subscribe(pos => PositionBuffer.Add(pos));

            // After position is stored, bubble it
            compensatedPositions
                .Subscribe(x => _positionSource.OnNext(x));

            // TODO [MultiMeasure] move out
            // Storing contact points
            //compensatedPositions
            //    .Where(pos => pos.Contact)
            //    .Subscribe(pos => RawContactPositions.Add(pos));

            // TODO [MultiMeasure] move out
            // Selected positions change
            //SelectedPositions.OnAdd
            //    .Where(_ => SelectedMeasurementMethod != null)
            //    .Subscribe(_ => { var test = SelectedMeasurementMethod.CanCalculate(); });
            //SelectedPositions.OnRemove
            //    .Where(_ => SelectedMeasurementMethod != null)
            //    .Subscribe(_ => { var test = SelectedMeasurementMethod.CanCalculate(); });

            // Initialize 
            Wipe();
            AvailableMeasurementMethods = Enum.GetValues(typeof(MeasurementMethodEnum)).Cast<MeasurementMethodEnum>();
        }

        public IObservable<IMeasurementMethod> MeasurementSource => _measurementSource.AsObservable();

        // Positions (Gauge / Contact)
        public IObservable<Position> PositionSource => _positionSource.AsObservable();
        public bool SetupMeasurementMethod(MeasurementMethodEnum selectedMeasurementMethod)
        {
            SelectedMeasurementMethod = selectedMeasurementMethod;
            // todo here to set up MeasurementSource
            Wipe();
            return true;
        }

        /// <summary>
        /// Wipe all measurement data. 
        /// </summary>
        public bool ResetMeasurementData()
        {
            Wipe();
            return true;
        }

        // TODO [MultiMeasure] move out
        //public ObservableList<Position> SelectedPositions { get; } = new ObservableList<Position>();
        public ObservableList<Position> PositionBuffer { get; } = new ObservableList<Position>();
        // TODO [MultiMeasure] move out
        //public ObservableList<Position> RawContactPositions { get; } = new ObservableList<Position>();

        /// <summary>
        /// Saves latest position as CompensationPosition, that will affect next measurements. Wipes the data afterwards.
        /// </summary>
        /// <returns></returns>
        public bool Calibrate()
        {
            PushZeroPosition();
            _compensationPosition = _lastRawPosition;
            Wipe();

            return true;
        }

        private void PushZeroPosition() => _positionSource.OnNext(Position.Default);

        // TODO [MultiMeasure] Foreach on all or rather delete elements from list
        private void Wipe()
        {
            PositionBuffer.Clear();
            // TODO [MultiMeasure] move out
            //RawContactPositions.Clear();
            //SelectedPositions.Clear();
        }

        /// <summary>
        /// Compensates position value with CompensationPosition
        /// </summary>
        private GaugePositionDTO CompensatePosition(GaugePositionDTO position) =>
            new GaugePositionDTO(position.X - _compensationPosition.X, position.Y - _compensationPosition.Y, position.Z - _compensationPosition.Z, position.Contact);

        // Measurement (flatness etc.)
        public IEnumerable<MeasurementMethodEnum> AvailableMeasurementMethods { get; private set; }
        public MeasurementMethodEnum? SelectedMeasurementMethod { get; private set; }
    }
}
