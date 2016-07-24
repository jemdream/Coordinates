using System;
using System.Threading.Tasks;
using Coordinates.ExternalDevices.Connections;
using Coordinates.Models.DTO;

namespace Coordinates.ExternalDevices.Devices
{
    public class MockConnectionService : BaseConnectionService<object>, IDeviceService<GaugePosition, object>
    {
        public MockConnectionService()
        {
            // konfiguracja połączenia 
        }

        public override object ConnectionConfiguration { get; set; }

        protected override async Task<bool> OnOpeningAsync()
        {
            await Task.Delay(2000);

            return true;
        }

        protected override async Task<bool> OnClosingAsync()
        {
            await Task.Delay(2000);

            return true;
        }

        public IObservable<GaugePosition> DataStream { get; set; }
    }
}