﻿using System;
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

            // Bubbling compensated position
            compensatedPositions
                .Subscribe(pos => _positionSource.OnNext(pos));

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
            //_measurementSource.OnNext();
            /* TODO here to set up MeasurementSource and push it */
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
        
        public ObservableList<Position> PositionBuffer { get; } = new ObservableList<Position>();

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
        
        private void Wipe()
        {
            // TODO [MultiMeasure] Foreach on all or rather delete elements from list
            PositionBuffer.Clear();
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
