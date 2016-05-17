using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Coordinates.Services.Args;
using Coordinates.Services.Events.ConnectionEvents;

namespace Coordinates.Services
{
    public class MockedConnectionService : IConnectionService
    {
        private readonly ReplaySubject<ConnectionEvent> _subject;
        private ConnectionState _connectionState;

        public MockedConnectionService()
        {
            _subject = new ReplaySubject<ConnectionEvent>(int.MaxValue);
        }

        public ConnectionState ConnectionState
        {
            get { return _connectionState; }
            private set
            {
                _connectionState = value;
                _subject.OnNext(new ConnectedEvent { Message = _connectionState.ToString(), TimeStamp = DateTime.UtcNow });
            }
        }

        public IObservable<ConnectionEvent> ConnectionMessages => _subject.AsObservable();

        public IEnumerable<DiagnosticEvent> DiagnosticMessages { get; }

        public async Task<bool> Connect()
        {
            Debug.WriteLine($"{nameof(Connect)} invoked");

            ConnectionState = ConnectionState.Connecting;

            await Task.Delay(2000);

            ConnectionState = ConnectionState.Open;

            return await Task.FromResult(true);
        }

        public async Task<bool> Disconnect()
        {
            Debug.WriteLine($"{nameof(Disconnect)} invoked");

            ConnectionState = ConnectionState.Closed;

            return await Task.FromResult(true);
        }

        public void Dispose() { }
    }
}
