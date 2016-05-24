using System;
using System.Collections.Generic;
using System.Data;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Coordinates.Services.Events;
using Coordinates.Services.Events.ConnectionEvents;

namespace Coordinates.Services.Connection
{
    public abstract class BaseConnectionService : IConnectionService
    {
        private readonly ReplaySubject<ConnectionEvent<ConnectionState>> _connectionMessagesSubject;
        private ConnectionState _connectionState;

        protected BaseConnectionService()
        {
            _connectionMessagesSubject = new ReplaySubject<ConnectionEvent<ConnectionState>>(int.MaxValue);
        }

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

        public IObservable<ConnectionEvent<ConnectionState>> ConnectionMessages => _connectionMessagesSubject.AsObservable();
        public IEnumerable<DiagnosticEvent> DiagnosticMessages { get; }

        protected abstract Task<bool> OnConnectingAsync();
        protected abstract Task<bool> OnDisconnectingAsync();

        public async Task<bool> Connect()
        {
            if (!ConnectionState.Equals(ConnectionState.Closed))
                return true;

            ConnectionState = ConnectionState.Connecting;

            var result = await OnConnectingAsync();

            ConnectionState = ConnectionState.Open;

            return result;
        }

        public async Task<bool> Disconnect()
        {
            ConnectionState = ConnectionState.Closed;

            var result = await OnDisconnectingAsync();

            return result;
        }

        private void ConnectionStateChanged()
        {
            // _ if disconnected then DisconnectedEvent(); else ConnectedEvent(); with message of type enum ConnectionState
            var connectionEvent = ConnectionState.Equals(ConnectionState.Closed) || _connectionState.Equals(ConnectionState.Broken) ?
                (ConnectionEvent<ConnectionState>)new DisconnectedEvent() : new ConnectedEvent();

            connectionEvent.Message = ConnectionState;
            connectionEvent.TimeStamp = DateTime.UtcNow;

            _connectionMessagesSubject.OnNext(connectionEvent);
        }

        public void Dispose()
        {
            if (!ConnectionState.Equals(ConnectionState.Closed))
                Disconnect().RunSynchronously();
        }
    }
}
