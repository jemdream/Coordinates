using System;
using System.Threading.Tasks;
using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.Models;

namespace Coordinates.ExternalDevices.Devices
{
    public class MockDeviceService : BaseConnectionService, IDeviceService<GaugePositionDTO>
    {
        private IConnection _connectionConfiguration; // TODO Change IConnection to implementation

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