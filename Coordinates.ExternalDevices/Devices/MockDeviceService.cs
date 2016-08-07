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
        private IConnection _connectionConfiguration; // TODO Change IConnection to implementation
        private readonly Subject<GaugePositionDTO> mockSource = new Subject<GaugePositionDTO>();

        public MockDeviceService()
        {
            DataStream = mockSource.AsObservable();
        }
        public override IConnection ConnectionConfiguration => _connectionConfiguration;

        protected override async Task<bool> OnOpeningAsync()
        {
            //Debugger.Break();
            await Task.Delay(2000);

            return await Task.FromResult(true);
        }

        protected override async Task<bool> OnClosingAsync()
        {
            //Debugger.Break();
            await Task.Delay(2000);

            return await Task.FromResult(true);
        }

        public IObservable<GaugePositionDTO> DataStream { get; set; }
    }
}