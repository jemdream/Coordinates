using System;
using System.Threading.Tasks;
using Coordinates.Services.Args;

namespace Coordinates.Services
{
    public interface IConnectionService : IDisposable
    {
        bool IsConnected { get; set; }
        IObservable<ConnectionArgs> ConnectionEvents { get; set; }
        IObservable<ConnectionArgs> Disconnected { get; set; }

        Task<bool> Connect();
        Task<bool> Disconnect();
    }
}
