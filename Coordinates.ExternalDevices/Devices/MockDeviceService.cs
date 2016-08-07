using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.Models;

namespace Coordinates.ExternalDevices.Devices
{
    public class MockDeviceService : BaseConnectionService, IDeviceService<GaugePositionDTO>
    {
        private readonly MockConnectionConfiguration _connectionConfiguration;
        private readonly Subject<GaugePositionDTO> _mockSource;
        private readonly Random _randomGenerator = new Random();
        private IDisposable _mockingDataSource;

        public MockDeviceService(Subject<GaugePositionDTO> mockSource)
        {
            _mockSource = mockSource;
            _connectionConfiguration = new MockConnectionConfiguration();
            DataStream = _mockSource.AsObservable();
        }

        public void StartTimerOnUiThread()
        {
            _mockingDataSource = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Subscribe(x => { _mockSource.OnNext(GenerateNextPosition()); });
        }

        private GaugePositionDTO GenerateNextPosition()
        {
            var contact = _randomGenerator.Next(100);
            var x = _randomGenerator.NextDouble();
            var y = _randomGenerator.NextDouble();
            var z = _randomGenerator.NextDouble();

            return new GaugePositionDTO
            {
                Contact = contact < 50,
                X = x * 100,
                Y = y * 100,
                Z = z * 100
            };
        }

        public override IConnection ConnectionConfiguration => _connectionConfiguration;

        protected override async Task<bool> OnOpeningAsync()
        {
            await Task.Delay(2000);
            StartTimerOnUiThread();

            return await Task.FromResult(true);
        }

        protected override async Task<bool> OnClosingAsync()
        {
            await Task.Delay(2000);
            _mockingDataSource.Dispose();

            return await Task.FromResult(true);
        }

        public IObservable<GaugePositionDTO> DataStream { get; set; }
    }
}