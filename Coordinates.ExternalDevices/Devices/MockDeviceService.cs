using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Windows.Foundation.Diagnostics;
using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.Models;

namespace Coordinates.ExternalDevices.Devices
{
    public class MockDeviceService : BaseConnectionService, IDeviceService<GaugePositionDTO>
    {
        private readonly Subject<GaugePositionDTO> _mockSource;
        private CompositeDisposable _mockingDataSource = new CompositeDisposable();

        private readonly LoggingChannel _loggingChannel = new LoggingChannel(nameof(MockDeviceService), new LoggingChannelOptions(Guid.NewGuid()));

        public MockDeviceService(Subject<GaugePositionDTO> mockSource, ILoggingSession loggingSession) : base(loggingSession)
        {
            loggingSession.AddLoggingChannel(_loggingChannel);

            _mockSource = mockSource;
            DataStream = _mockSource.AsObservable();
        }

        public void StartTimerOnUiThread()
        {
            _mockingDataSource = new CompositeDisposable
            {
                //Observable
                //    .Interval(TimeSpan.FromMilliseconds(350))
                //    .Subscribe(x => { _mockSource.OnNext(GenerateNextPosition()); }),
                //Observable
                //    .Interval(TimeSpan.FromMilliseconds(1000))
                //    .Subscribe(x => { _mockSource.OnNext(new GaugePositionDTO {Contact = true}); })
            };
        }

        public string X { get; set; }
        public string Y { get; set; }
        public string Z { get; set; }
        public bool Contact { get; set; }

        public void PushMockedValues()
        {
            double x, y, z;
            double.TryParse(X, out x);
            double.TryParse(Y, out y);
            double.TryParse(Z, out z);

            _mockSource.OnNext(new GaugePositionDTO(x, y, z, Contact));
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

            return new GaugePositionDTO(x, y, z, contact);
        }
        
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