using System;
using System.Threading.Tasks;
using Windows.Devices.SerialCommunication;
using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.Models;

namespace Coordinates.ExternalDevices.Devices
{
    public class TestConnection : IConnection
    {
        
    }

    public class SerialDeviceService : BaseConnectionService, IDeviceService<GaugePositionDTO> // IDataSource<GaugePositionDTO>
    {
        private SerialDevice _device;
        private readonly TestConnection _connectionConfiguration = new TestConnection(); // TODO Change IConnection to implementation

        public SerialDeviceService()
        {
        }

        public override IConnection ConnectionConfiguration => _connectionConfiguration;

        protected override async Task<bool> OnOpeningAsync()
        {
            if (_device == null)
                return false; // TODO validate 

            // implement connecting to SerialDevice
            throw new NotImplementedException();

            return await Task.FromResult(true);
        }

        protected override async Task<bool> OnClosingAsync()
        {
            if (_device == null)
                return false; // TODO validate 

            // implement disconnecting to SerialDevice
            throw new NotImplementedException();

            return await Task.FromResult(true);
        }

        public IObservable<GaugePositionDTO> DataStream { get; set; }
    }
}