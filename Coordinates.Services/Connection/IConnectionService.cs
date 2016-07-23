using System;
using System.Data;
using System.Threading.Tasks;
using Coordinates.Services.Events.ConnectionEvents;

namespace Coordinates.Services.Connection
{
    public interface IConnectionService : IDisposable
    {
        ConnectionState ConnectionState { get; }
        IObservable<DiagnosticEvent> DiagnosticEventsStream { get; }
        Task<ConnectionState> Open();
        Task<ConnectionState> Close();
    }
}