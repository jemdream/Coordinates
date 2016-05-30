using System.Threading.Tasks;
using Coordinates.Services.Connection;

namespace Coordinates.Services
{
    public class SerialPortConnectionService : BaseConnectionService
    {
        public SerialPortConnectionService()
        {
            //var serialPort = new SerialDevice();
        }

        protected override async Task<bool> OnConnectingAsync()
        {
            // source of mocked data; 
            // TODO: create mocked source

            await Task.Delay(2000);
            
            return true;
        }

        protected override async Task<bool> OnDisconnectingAsync()
        {
            return await Task.FromResult(true);
        }
    }
}
