using System;
using System.Threading.Tasks;
using Coordinates.DataSources.Events;

namespace Coordinates.DataSources.Connections
{
    public interface IConnectionService : IDisposable
    {
        IConnectionConfiguration ConnectionConfiguration { get; }

        ConnectionStatus ConnectionStatus { get; }
        IObservable<DiagnosticEvent> DiagnosticEventsStream { get; }

        Task<ConnectionStatus> Open();
        Task<ConnectionStatus> Close();
    }
}