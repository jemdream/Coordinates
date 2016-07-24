using System;
using System.Threading.Tasks;
using Coordinates.ExternalDevices.Events.ConnectionEvents;

namespace Coordinates.ExternalDevices.Connections
{
    public interface IConnectionService<T> : IDisposable
    {
        T ConnectionConfiguration { get; set; }
        ConnectionStatus ConnectionStatus { get; }
        IObservable<DiagnosticEvent> DiagnosticEventsStream { get; }
        Task<ConnectionStatus> Open();
        Task<ConnectionStatus> Close();
    }
}