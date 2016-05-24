using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Coordinates.Services.Events;
using Coordinates.Services.Events.ConnectionEvents;

namespace Coordinates.Services.Connection
{
    public interface IConnectionService : IDisposable
    {
        /// <summary>
        /// Connection state
        /// </summary>
        ConnectionState ConnectionState { get; }

        IObservable<ConnectionEvent<ConnectionState>> ConnectionMessages { get; }
        IEnumerable<DiagnosticEvent> DiagnosticMessages { get; }

        Task<bool> Connect();
        Task<bool> Disconnect();
    }
}
