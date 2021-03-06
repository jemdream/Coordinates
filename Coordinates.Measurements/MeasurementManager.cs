﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Coordinates.DataSources;
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

        private Position _lastRawPosition = Position.Default;
        private Position _compensationPosition = Position.Default;
        private IMeasurementMethod _selectedMeasurementMethod;

        public MeasurementManager(IDataSource<GaugePositionDTO> measurementDataSource)
        {
            // Compensating and mapping
            var compensatedPositions = measurementDataSource.DataStream
                .Select(RawPositionModification)
                .Select(CompensatePosition);

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
        public bool GatherData { get; set; }

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
            GatherData = false;

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
        /// Stores raw position and checks for initial contact - calibration purposes
        /// </summary>
        private Position RawPositionModification(GaugePositionDTO pos)
        {
            // Checks if contact was initial, or sequential
            var firstContact = pos.Contact && !_lastRawPosition.Contact;
            return _lastRawPosition = new Position(pos.X, pos.Y, pos.Z, pos.Contact, firstContact);
        }

        /// <summary>
        /// Compensates position value with CompensationPosition
        /// </summary>
        private Position CompensatePosition(Position position)
        {
            return new Position(
                (position.X - _compensationPosition.X).Round(), 
                (position.Y - _compensationPosition.Y).Round(), 
                (position.Z - _compensationPosition.Z).Round(), 
                position.Contact, 
                position.FirstContact
            );
        }
    }
}
