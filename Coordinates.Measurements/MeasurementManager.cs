using System;
using System.Collections.Generic;
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
        private readonly List<GaugePosition> _gaugePositions = new List<GaugePosition>();
        private readonly List<ContactPosition> _contactPositions = new List<ContactPosition>();
        private readonly Subject<ContactPosition> _selectedPositionsSubject = new Subject<ContactPosition>();
        private IMeasurement _selectedMeasurement;

        public MeasurementManager(IDataSource<GaugePositionDTO> measurementDataSource)
        {
            measurementDataSource.DataStream
                .Subscribe(pos => _gaugePositions.Add(new GaugePosition { Contact = pos.Contact, X = pos.X, Y = pos.Y, Z = pos.Z }));

            measurementDataSource.DataStream
                .Where(pos => pos.Contact)
                .Subscribe(pos => _contactPositions.Add(new ContactPosition { X = pos.X, Y = pos.Y, Z = pos.Z }));

            _selectedPositionsSubject.Subscribe();

            InstantiateMeasurements();
        }

        public IEnumerable<GaugePosition> GaugePositions => _gaugePositions;
        public IEnumerable<ContactPosition> ContactPositions => _contactPositions;
        public IObserver<ContactPosition> SelectedPositions => _selectedPositionsSubject.AsObserver();
        public IEnumerable<IMeasurement> AvailableMeasurements { get; private set; }

        public IMeasurement SelectedMeasurement
        {
            get { return _selectedMeasurement; }
            set { SetupMeasurement(value); }
        }

        private void SetupMeasurement(IMeasurement value)
        {
            // TODO probably delete all 
            _selectedMeasurement = value;
        }

        private void InstantiateMeasurements()
        {
            // TODO could have: can use reflections and iterate by classes in namespace Coordinates.Measurements.Types
            AvailableMeasurements = new List<IMeasurement>
            {
                new RoundnessMeasurement(),
                new FlatnessMeasurement()
            };
        }
    }
}
