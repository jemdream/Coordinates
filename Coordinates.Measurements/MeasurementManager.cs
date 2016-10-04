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
    public class MeasurementManager : IMeasurementManager
    {
        private readonly ReplaySubject<Position> _positionSource = new ReplaySubject<Position>(1);
        private readonly ReplaySubject<IMeasurementMethod> _measurementSource = new ReplaySubject<IMeasurementMethod>(1);
        private readonly MeasurementMethodFactory _measurementMethodFactory = new MeasurementMethodFactory();

        private GaugePositionDTO _lastRawPosition = GaugePositionDTO.Default;
        private GaugePositionDTO _compensationPosition = GaugePositionDTO.Default;
        private IMeasurementMethod _selectedMeasurementMethod;

        public MeasurementManager(IDataSource<GaugePositionDTO> measurementDataSource)
        {
            // Store raw position
            measurementDataSource.DataStream
                .Subscribe(lastRaw => _lastRawPosition = lastRaw);

            // Compensating and mapping
            var compensatedPositions = measurementDataSource.DataStream
                .Select(CompensatePosition)
                .Select(pos => new Position(pos.X, pos.Y, pos.Z, pos.Contact));

            compensatedPositions
                .Subscribe(pos =>
                {
                    // Storing all points
                    PositionBuffer.Add(pos);
                    // Bubbling compensated position
                    _positionSource.OnNext(pos);
                });

            // Initialize 
            AvailableMeasurementMethods = Enum.GetValues(typeof(MeasurementMethodEnum)).Cast<MeasurementMethodEnum>();
        }

        // Measurement
        public IEnumerable<MeasurementMethodEnum> AvailableMeasurementMethods { get; }
        public MeasurementMethodEnum? SelectedMeasurementMethod { get; private set; }
        public IObservable<IMeasurementMethod> MeasurementSource => _measurementSource.AsObservable();

        // Positions
        public IObservable<Position> PositionSource => _positionSource.AsObservable();
        public ObservableList<Position> PositionBuffer { get; } = new ObservableList<Position>();

        /// <summary>
        /// Sets up 
        /// </summary>
        /// <param name="selectedMeasurementMethod"></param>
        /// <returns></returns>
        public bool SetupMeasurementMethod(MeasurementMethodEnum selectedMeasurementMethod)
        {
            PositionBuffer.Clear();

            _selectedMeasurementMethod?.Subscriptions.Clear();
            SelectedMeasurementMethod = selectedMeasurementMethod;
            _selectedMeasurementMethod = _measurementMethodFactory.GetMeasurementMethod(selectedMeasurementMethod);
            _measurementSource.OnNext(_selectedMeasurementMethod);

            return true;
        }

        /// <summary>
        /// Wipe all measurement data. 
        /// </summary>
        public bool ResetMeasurementData()
        {
            PositionBuffer.Clear();

            SelectedMeasurementMethod = null;
            _selectedMeasurementMethod?.Subscriptions.Clear();
            _selectedMeasurementMethod = null;

            return true;
        }

        /// <summary>
        /// Saves latest position as CompensationPosition, that will affect next measurements. Wipes the data afterwards.
        /// </summary>
        /// <returns></returns>
        public bool Calibrate()
        {
            _positionSource.OnNext(Position.Default);
            _compensationPosition = _lastRawPosition;
            PositionBuffer.Clear();

            return true;
        }
        
        /// <summary>
        /// Compensates position value with CompensationPosition
        /// </summary>
        private GaugePositionDTO CompensatePosition(GaugePositionDTO position) =>
            new GaugePositionDTO(position.X - _compensationPosition.X, position.Y - _compensationPosition.Y, position.Z - _compensationPosition.Z, position.Contact);
    }
}
