using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Coordinates.Services.Events.ConnectionEvents;

namespace Coordinates.Services.Connections
{
    public abstract class BaseConnectionService<T> : IConnectionService<T>
    {
        private readonly ReplaySubject<DiagnosticEvent> _connectionMessagesSubject;
        private ConnectionState _connectionState;
        private readonly List<DiagnosticEvent> _diagnosticEvents;

        protected BaseConnectionService()
        {
            _connectionMessagesSubject = new ReplaySubject<DiagnosticEvent>(int.MaxValue);
            _diagnosticEvents = new List<DiagnosticEvent>();

            _connectionMessagesSubject.Subscribe(_diagnosticEvents.Add);
        }

        public abstract T ConnectionConfiguration { get; set; }

        public ConnectionState ConnectionState
        {
            get { return _connectionState; }
            private set
            {
                if (_connectionState == value) return;
                _connectionState = value;
                ConnectionStateChanged();
            }
        }

        protected abstract Task<bool> OnOpeningAsync();
        protected abstract Task<bool> OnClosingAsync();

        public IEnumerable<DiagnosticEvent> DiagnosticEvents => _diagnosticEvents;
        public IObservable<DiagnosticEvent> DiagnosticEventsStream => _connectionMessagesSubject.AsObservable();

        public async Task<ConnectionState> Open()
        {
            if (ConnectionState.Equals(ConnectionState.Open))
                return ConnectionState;

            ConnectionState = ConnectionState.Opening;

            var result = await OnOpeningAsync();
            
            ConnectionState = result ? ConnectionState.Open : ConnectionState.Broken;

            return ConnectionState;
        }
        public async Task<ConnectionState> Close()
        {
            if (ConnectionState.Equals(ConnectionState.Closed))
                return ConnectionState;

            ConnectionState = ConnectionState.Closing;

            var result = await OnClosingAsync();
            
            ConnectionState = result ? ConnectionState.Closed : ConnectionState.Broken;

            return ConnectionState;
        }

        private void ConnectionStateChanged()
        {
            var connectionEvent = new DiagnosticEvent
            {
                Message = ConnectionState,
                TimeStamp = DateTime.UtcNow
            };

            _connectionMessagesSubject.OnNext(connectionEvent);
        }
        public void Dispose()
        {
            if (!ConnectionState.Equals(ConnectionState.Closed))
                Close().RunSynchronously();
        }
    }
}