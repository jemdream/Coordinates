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
    }
}
