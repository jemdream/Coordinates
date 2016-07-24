using System;
using System.Threading.Tasks;
using Windows.Devices.SerialCommunication;
using Coordinates.ExternalDevices.Connections;
using Coordinates.Models.DTO;

namespace Coordinates.ExternalDevices.Devices
{
    public class SerialPortConnectionService : BaseConnectionService<SerialDevice>, IDeviceService<GaugePosition, SerialDevice> // IDataSource<GaugePosition>
    {
        public SerialPortConnectionService()
        {
        }

        public override SerialDevice ConnectionConfiguration { get; set; }

        protected override async Task<bool> OnOpeningAsync()
        {
            // implement connecting to SerialDevice
            throw new NotImplementedException();
        }

        protected override async Task<bool> OnClosingAsync()
        {
            // implement disconnecting to SerialDevice
            throw new NotImplementedException();
        }

        public IObservable<GaugePosition> DataStream { get; set; }
    }
}