using System;
using System.Threading.Tasks;
using Coordinates.ExternalDevices.Connections;
using Coordinates.Models.DTO;
using GaugePositionDTO = Coordinates.ExternalDevices.Models.GaugePositionDTO;

namespace Coordinates.ExternalDevices.Devices
{
    public class MockConnectionService : BaseConnectionService<object>, IDeviceService<GaugePositionDTO, object>
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

        public IObservable<GaugePositionDTO> DataStream { get; set; }
    }
}