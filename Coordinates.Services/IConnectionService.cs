using System;
using System.Threading.Tasks;
using Coordinates.Services.Args;
using System.Collections.Generic;
using System.Data;

namespace Coordinates.Services
{
    public interface IConnectionService : IDisposable
    {
        /// <summary>
        /// Connection state
        /// </summary>
        ConnectionState ConnectionState { get; }

        IObservable<ConnectionEvent> ConnectionMessages { get; }
        IEnumerable<DiagnosticEvent> DiagnosticMessages { get; }

        Task<bool> Connect();
        Task<bool> Disconnect();
    }

    public class DiagnosticEvent
    {
        public DateTime TimeStamp { get; set; }
    }
}
