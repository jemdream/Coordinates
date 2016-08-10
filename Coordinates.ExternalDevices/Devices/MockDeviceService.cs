using System;
using System.Reactive.Disposables;
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
        private CompositeDisposable _mockingDataSource = new CompositeDisposable();

        public MockDeviceService(Subject<GaugePositionDTO> mockSource)
        {
            _mockSource = mockSource;
            _connectionConfiguration = new MockConnectionConfiguration();
            DataStream = _mockSource.AsObservable();
        }

        public void StartTimerOnUiThread()
        {
            _mockingDataSource = new CompositeDisposable
            {
                Observable
                    .Interval(TimeSpan.FromMilliseconds(350))
                    .Subscribe(x => { _mockSource.OnNext(GenerateNextPosition()); }),
                Observable
                    .Interval(TimeSpan.FromMilliseconds(1000))
                    .Subscribe(x => { _mockSource.OnNext(new GaugePositionDTO {Contact = true}); })
            };
        }

        private readonly Random _randomGenerator = new Random();
        private readonly double[] _steps = new double[3];
        private bool _sign;
        private GaugePositionDTO GenerateNextPosition()
        {
            var contact = _randomGenerator.Next(100) < 50;

            // Triangle signal
            var x = _steps[0];
            var y = _steps[1];
            var z = _steps[2];

            if (!_sign) for (var i = 0; i < 3; i++) { _steps[i] = _steps[i] + 0.01; }
            else for (var i = 0; i < 3; i++) { _steps[i] = _steps[i] - 0.01; }

            if (Math.Abs(_steps[0]) < 0.01 || Math.Abs(_steps[0]) > 0.99) _sign = !_sign;
            // ------

            return new GaugePositionDTO
            {
                Contact = contact,
                X = x,
                Y = y,
                Z = z
            };
        }

        public override IConnection ConnectionConfiguration => _connectionConfiguration;

        protected override async Task<bool> OnOpeningAsync()
        {
            await Task.Delay(750);
            StartTimerOnUiThread();

            return await Task.FromResult(true);
        }

        protected override async Task<bool> OnClosingAsync()
        {
            await Task.Delay(750);
            _mockingDataSource.Dispose();

            return await Task.FromResult(true);
        }

        public IObservable<GaugePositionDTO> DataStream { get; set; }
    }
}