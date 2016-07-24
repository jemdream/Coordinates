using System;
using System.Threading.Tasks;
using Coordinates.Services.Events.ConnectionEvents;

namespace Coordinates.Services.Connections
{
    public interface IConnectionService<T> : IDisposable
    {
        T ConnectionConfiguration { get; set; }
        ConnectionState ConnectionState { get; }
        IObservable<DiagnosticEvent> DiagnosticEventsStream { get; }
        Task<ConnectionState> Open();
        Task<ConnectionState> Close();
    }
}