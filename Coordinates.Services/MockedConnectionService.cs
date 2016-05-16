using System;
using System.Threading.Tasks;
using Coordinates.Services.Args;

namespace Coordinates.Services
{
    public class MockedConnectionService : IConnectionService
    {
        private bool _isConnected;
        //private void Subject<ConnectionArgs> 

        public MockedConnectionService()
        {

            IsConnected = true;
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                
                _isConnected = value;
            }
        }

        public IObservable<ConnectionArgs> ConnectionEvents { get; set; }
        public IObservable<ConnectionArgs> Disconnected { get; set; }
        public Task<bool> Connect()
        {

            // ConnectionEvents
            return Task.FromResult(true);
        }

        public Task<bool> Disconnect()
        {
            return Task.FromResult(true);
        }

        public void Dispose()
        {

        }
    }
}
