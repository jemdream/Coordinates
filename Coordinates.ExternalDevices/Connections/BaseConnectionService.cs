using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Coordinates.ExternalDevices.Events.ConnectionEvents;

namespace Coordinates.ExternalDevices.Connections
{
    public abstract class BaseConnectionService<T> : IConnectionService<T>
    {
        private readonly ReplaySubject<DiagnosticEvent> _connectionMessagesSubject;
        private ConnectionStatus _connectionStatus;
        private readonly List<DiagnosticEvent> _diagnosticEvents;

        protected BaseConnectionService()
        {
            _connectionMessagesSubject = new ReplaySubject<DiagnosticEvent>(int.MaxValue);
            _diagnosticEvents = new List<DiagnosticEvent>();

            _connectionMessagesSubject.Subscribe(_diagnosticEvents.Add);
        }

        public abstract T ConnectionConfiguration { get; set; }

        public ConnectionStatus ConnectionStatus
        {
            get { return _connectionStatus; }
            private set
            {
                if (_connectionStatus == value) return;
                _connectionStatus = value;
                ConnectionStateChanged();
            }
        }

        protected abstract Task<bool> OnOpeningAsync();
        protected abstract Task<bool> OnClosingAsync();

        public IEnumerable<DiagnosticEvent> DiagnosticEvents => _diagnosticEvents;
        public IObservable<DiagnosticEvent> DiagnosticEventsStream => _connectionMessagesSubject.AsObservable();

        // TODO requires flag, so you can't hammer toggling (on/off/on/off)
        public async Task<ConnectionStatus> Open()
        {
            if (ConnectionStatus.Equals(ConnectionStatus.Open))
                return ConnectionStatus;

            ConnectionStatus = ConnectionStatus.Opening;

            var result = await OnOpeningAsync();
            
            ConnectionStatus = result ? ConnectionStatus.Open : ConnectionStatus.Broken;

            return ConnectionStatus;
        }

        public async Task<ConnectionStatus> Close()
        {
            if (ConnectionStatus.Equals(ConnectionStatus.Closed))
                return ConnectionStatus;

            ConnectionStatus = ConnectionStatus.Closing;

            var result = await OnClosingAsync();
            
            ConnectionStatus = result ? ConnectionStatus.Closed : ConnectionStatus.Broken;

            return ConnectionStatus;
        }

        private void ConnectionStateChanged()
        {
            var connectionEvent = new DiagnosticEvent
            {
                Message = ConnectionStatus,
                TimeStamp = DateTime.UtcNow
            };

            _connectionMessagesSubject.OnNext(connectionEvent);
        }
        public void Dispose()
        {
            if (!ConnectionStatus.Equals(ConnectionStatus.Closed))
                Close().RunSynchronously();
        }
    }
}